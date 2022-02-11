using Lipar.Core;
using Lipar.Entities.Domain.General;
using Lipar.Entities.Domain.General.Enums;
using Lipar.Entities.Domain.Organization;
using Lipar.Services.General.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Net;

namespace Lipar.Services.Messages
{
    public class WorkflowMessageService : IWorkflowMessageService
    {
        #region Ctor
        public WorkflowMessageService(IMessageTemplateService messageTemplateService
                                    , IGenericAttributeService genericAttributeService
                                    , IEmailAccountService emailAccountService
                                    , IQueuedEmailService queuedEmailService
                                    , IWorkContext workContext
                                    , IActionContextAccessor actionContextAccessor
                                    , IUrlHelperFactory urlHelperFactory)
        {
            _messageTemplateService = messageTemplateService;
            _genericAttributeService = genericAttributeService;
            _emailAccountService = emailAccountService;
            _queuedEmailService = queuedEmailService;
            _workContext = workContext;
            _actionContextAccessor = actionContextAccessor;
            _urlHelperFactory = urlHelperFactory;
        }
        #endregion

        #region Fields
        private readonly IMessageTemplateService _messageTemplateService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IEmailAccountService _emailAccountService;
        private readonly IQueuedEmailService _queuedEmailService;
        private readonly IWorkContext _workContext;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private IList<Token> _tokens;
        #endregion

        #region Methods
        public void SendPasswordRecoveryMessage(User user, int languageId)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var messageTemplate = GetActiveMessageTemplate(LiparMessageTemplateSystemNames.PasswordRecoveryMessage);

            var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate);

            var toEmail = user.Email;
            var toName = $"{user.FirstName} {user.LastName}";

            var tokens = new List<Token>();
            AddUserTokens(tokens, user);

            SendNotification(messageTemplate, emailAccount, tokens, toEmail, toName);
        }
        #endregion


        #region Utilities
        protected MessageTemplate GetActiveMessageTemplate(string name)
        {
            var model = _messageTemplateService.GetMessageTemplateByName(name);

            return model;
        }

        protected EmailAccount GetEmailAccountOfMessageTemplate(MessageTemplate messageTemplate)
        {
            var emailAccount = _emailAccountService.GetById(messageTemplate.EmailAccountId, true);

            return emailAccount;
        }

        protected int SendNotification(MessageTemplate messageTemplate, EmailAccount emailAccount,
                                        IList<Token> tokens, string toEmail, string toName, string subject = null,
                                        string body = null, string fromEmail = null, string fromName = null)
        {
            if (string.IsNullOrEmpty(subject))
            {
                subject = messageTemplate.Subject;
            }

            if (string.IsNullOrWhiteSpace(body))
            {
                body = messageTemplate.Template;
            }

            if (string.IsNullOrWhiteSpace(fromEmail))
            {
                fromEmail = emailAccount.Email;
            }

            if (string.IsNullOrWhiteSpace(fromName))
            {
                fromName = emailAccount.Name;
            }

            var bodyReplaced = Replace(messageTemplate.Template, tokens);

            var email = new QueuedEmail
            {
                Body = bodyReplaced,
                EmailAccountId = emailAccount.Id,
                PriorityId = (int)QueuedEmailEnum.High,
                Subject = subject,
                To = toEmail,
                ToName = toName,
                From = fromEmail,
                FromName = fromName,
            };

            //_queuedEmailService.Add(email);
            return 0;
            //return email.Id;
        }

        protected string Replace(string template, IList<Token> tokens)
        {
            if (string.IsNullOrEmpty(template))
            {
                throw new ArgumentNullException(nameof(template));
            }

            if (tokens == null)
            {
                throw new ArgumentNullException(nameof(tokens));
            }

            //replace tokens
            template = ReplaceTokens(template, tokens);

            return template;
        }

        protected string ReplaceTokens(string template, IList<Token> tokens)
        {
            foreach (var token in tokens)
            {
                var tokenValue = token.Value ?? string.Empty;
                return template.Replace($@"%{token.Key}%", tokenValue.ToString());
            }

            return template;
        }

        protected string RouteUrl(string routeName = null, object routeValues = null)
        {
            var center = _workContext.CurrentCenter;
            //ensure that the store URL is specified
            if (string.IsNullOrEmpty(center.Url))
                throw new Exception("URL cannot be null");

            //generate a URL with an absolute path
            var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);
            var url = new PathString(urlHelper.RouteUrl(routeName, routeValues));

            //remove the application path from the generated URL if exists
            var pathBase = _actionContextAccessor.ActionContext?.HttpContext?.Request?.PathBase ?? PathString.Empty;
            url.StartsWithSegments(pathBase, out url);

            //compose the result
            return Uri.EscapeUriString(WebUtility.UrlDecode($"{center.Url.TrimEnd('/')}{url}"));
        }

        protected IList<Token> AddUserTokens(IList<Token> tokens, User user)
        {
            var passwordRecoveryUrl = RouteUrl(routeName: "PasswordRecoveryConfirm", routeValues: new { token = _genericAttributeService.GetAttribute<User,Guid>(user, LiparMessageTemplateSystemNames.PasswordRecoveryToken), guid = user.UserGuid });

            tokens.Add(new Token
            {
                Key = "User.PasswordRecoveryURL",
                Value = passwordRecoveryUrl
            });

            return tokens;
        }
        #endregion
    }
}
