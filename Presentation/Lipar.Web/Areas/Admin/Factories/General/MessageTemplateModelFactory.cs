using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.General;
using Lipar.Web.Framework.Models;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Factories.General
{
    public class MessageTemplateModelFactory : IMessageTemplateModelFactory
    {
        #region Ctor
        public MessageTemplateModelFactory(IMessageTemplateService messageTemplateService
                                         , IBaseAdminModelFactory baseAdminModelFactory)
        {
            _messageTemplateService = messageTemplateService;
            _baseAdminModelFactory = baseAdminModelFactory;
        }
        #endregion

        #region Fields
        private readonly IMessageTemplateService _messageTemplateService;
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        #endregion

        #region Methods
        public MessageTemplateListModel PrepareMessageTemplateListModel(MessageTemplateSearchModel searchModel)
        {
            var messageTemplates = _messageTemplateService.List(new MessageTemplateListVM
            {
                Name = searchModel.Name,
                PageIndex = searchModel.Page - 1,
                PageSize = searchModel.PageSize
            });

            if (messageTemplates == null)
            {
                return null;
            }

            var models = new MessageTemplateListModel().PrepareToGrid(searchModel, messageTemplates, () =>
            {
                return messageTemplates.Select(messageTemplate =>
                {
                    var messageTemplateModel = messageTemplate.ToModel<MessageTemplateModel>();

                    return messageTemplateModel;
                });
            });

            return models;
        }

        public MessageTemplateModel PrepareMessageTemplateModel(MessageTemplateModel model, MessageTemplate messageTemplate)
        {
            if(messageTemplate != null)
            {
                model = messageTemplate.ToModel<MessageTemplateModel>();
            }

            //preapre email account
            _baseAdminModelFactory.PrepareEmailAccounts(model.AvailableEmailAccounts);

            return model;
        }
        #endregion
    }
}
