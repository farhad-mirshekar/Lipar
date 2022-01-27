using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.General;
using Lipar.Web.Framework.Models;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Factories.General
{
    public class EmailAccountModelFactory : IEmailAccountModelFactory
    {
        #region Ctor
        public EmailAccountModelFactory(IEmailAccountService emailAccountService)
        {
            _emailAccountService = emailAccountService;
        }
        #endregion

        #region Fields
        private readonly IEmailAccountService _emailAccountService;
        #endregion

        #region Methods
        public EmailAccountListModel PrepareEmailAccountListModel(EmailAccountSearchModel searchModel)
        {
            var emailAccounts = _emailAccountService.List(new EmailAccountListVM
            {
                Name = searchModel.Name,
                PageIndex = searchModel.Page - 1,
                PageSize = searchModel.PageSize,
            });

            if(emailAccounts == null)
            {
                return null;
            }

            var models = new EmailAccountListModel().PrepareToGrid(searchModel, emailAccounts, () =>
            {
                return emailAccounts.Select(emailAccount =>
                {
                    var emailAccountModel = emailAccount.ToModel<EmailAccountModel>();

                    return emailAccountModel;
                });
            });


            return models;
        }

        public EmailAccountModel PrepareEmailAccountModel(EmailAccountModel model, EmailAccount emailAccount)
        {
            if(emailAccount != null)
            {
                model = emailAccount.ToModel<EmailAccountModel>();
            }

            return model;
        }
        #endregion
    }
}
