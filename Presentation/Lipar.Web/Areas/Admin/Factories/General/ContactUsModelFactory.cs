using Lipar.Entities.Domain.General;
using Lipar.Services.General.Contracts;
using Lipar.Web.Areas.Admin.Infrastructure.Mapper;
using Lipar.Web.Areas.Admin.Models.General;
using Lipar.Web.Framework.Models;
using System.Linq;

namespace Lipar.Web.Areas.Admin.Factories.General
{
    public class ContactUsModelFactory : IContactUsModelFactory
    {
        #region Fields
        private readonly IContactUsService _contactUsService;
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        #endregion

        #region Ctor
        public ContactUsModelFactory(IContactUsService contactUsService
                                   , IBaseAdminModelFactory baseAdminModelFactory)
        {
            _contactUsService = contactUsService;
            _baseAdminModelFactory = baseAdminModelFactory;
        }
        #endregion

        #region Methods
        public ContactUsListModel PrepareContactUsListModel(ContactUsSearchModel searchModel)
        {
            var contactUsList = _contactUsService.List(new ContactUsListVM
            {
                ContactUsTypeId = searchModel.ContactUsTypeId,
                PageIndex = searchModel.Page - 1,
                PageSize = searchModel.PageSize,
            });

            var model = new ContactUsListModel().PrepareToGrid(searchModel, contactUsList, () =>
            {
                return contactUsList.Select(contactUs =>
                {
                    var contactUsModel = contactUs.ToModel<ContactUsModel>();

                    return contactUsModel;
                });
            });

            return model;
        }

        public ContactUsModel PrepareContactUsModel(ContactUsModel model, ContactUs contactUs)
        {
            if(contactUs != null)
            {
                model = contactUs.ToModel<ContactUsModel>();
            }

            return model;
        }

        public ContactUsSearchModel PrepareContactUsSearchModel()
        {
            var contactUsSearchModel = new ContactUsSearchModel();
            _baseAdminModelFactory.PrepareContactUsType(contactUsSearchModel.AvailableContactUsType);

            return contactUsSearchModel;
        }
        #endregion
    }
}
