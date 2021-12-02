using AutoMapper;
using Lipar.Core.Infrastructure.Mapper;
using App = Lipar.Entities.Domain.Application;
using Lipar.Entities.Domain.Organization;
using Lipar.Entities.Domain.Portal;
using Lipar.Entities.Domain.General;
using AppModel = Lipar.Web.Areas.Admin.Models.Application;
using Lipar.Web.Areas.Admin.Models.Organization;
using Lipar.Web.Areas.Admin.Models.Organization.User;
using Lipar.Web.Areas.Admin.Models.Portal;
using Lipar.Web.Areas.Admin.Models.General;
using Lipar.Entities.Domain.Application;
using Lipar.Web.Areas.Admin.Models.Application;
using Lipar.Services.Security;

namespace Lipar.Web.Areas.Admin.Infrastructure.Mapper
{
    public class AdminMapperConfiguration : Profile, IOrderedMapperProfile
    {
        #region Ctor
        public AdminMapperConfiguration()
        {
            OrganizationMapper();

            GeneralMapper();

            PortalMapper();

            ApplicationMapper();
        }
        #endregion
        public int Order => 0;

        #region Organization
        protected void OrganizationMapper()
        {
            CreateUserMap();
            CreateRoleMap();
            CreatePositionMap();
            CreateRolePermissionMap();
            CreateCommandMap();
        }

        public void CreateUserMap()
        {
            CreateMap<User, UserModel>()
                .ForMember(model => model.AvailableRoles, option => option.Ignore())
                .ForMember(model => model.AvailablePositionRole, option => option.Ignore());
        }
        public void CreateRoleMap()
        {
            CreateMap<Role, RoleModel>();

            CreateMap<RoleModel, Role>();

            CreateMap<Role, RolesModel>();

            CreateMap<RolesModel, Role>();
        }
        public void CreatePositionMap()
        {
            CreateMap<Position, PositionModel>()
                .ForMember(model => model.AvailableEnableType, option => option.Ignore())
                .ForMember(model => model.AvailablePositionType, option => option.Ignore())
                .ForMember(model => model.FirstName, option => option.MapFrom(u => u.User.FirstName))
                .ForMember(model => model.LastName, option => option.MapFrom(u => u.User.LastName))
                .ForMember(model => model.NationalCode, option => option.MapFrom(u => u.User.NationalCode))
                .ForMember(model => model.Email, option => option.MapFrom(u => u.User.Email))
                .ForMember(model => model.CellPhone, option => option.MapFrom(u => u.User.CellPhone))
                .ForMember(model => model.DepartmentId, option => option.MapFrom(u => u.DepartmentId))
                .ForMember(model => model.PositionTypeTitle, option => option.MapFrom(p => p.PositionType.Title));

            CreateMap<PositionModel, Position>();

            CreateMap<PositionRole, PositionRoleModel>();
            CreateMap<PositionRoleModel, PositionRole>();
        }
        public void CreateRolePermissionMap()
        {
            CreateMap<RolePermission, RolePermissionModel>();

            CreateMap<RolePermissionModel, RolePermission>()
                .ForMember(model => model.Command, option => option.Ignore())
                .ForMember(model => model.Role, option => option.Ignore());
        }
        public void CreateCommandMap()
        {
            CreateMap<Command, CommandModel>();

            CreateMap<CommandModel, Command>();
        }
        #endregion

        #region General
        protected void GeneralMapper()
        {
            CreateLanguageMap();
            CreateMediaMap();
            CreateMenuMap();
            CreateActivityLogMap();
            CreateFaqGroupMap();
            CreateSettingMap();
            CreateContactUsMap();
        }
        protected void CreateLanguageMap()
        {
            CreateMap<Language, LanguageModel>()
                .ForMember(model => model.AvailableLanguageCultureType, option => option.Ignore())
                .ForMember(model => model.AvailableViewStatusType, option => option.Ignore());

            CreateMap<LanguageModel, Language>();

            CreateMap<LocaleStringResource, LocaleStringResourceModel>();
            CreateMap<LocaleStringResourceModel, LocaleStringResource>();
        }
        protected void CreateMediaMap()
        {
            CreateMap<Media, MediaModel>();

            CreateMap<MediaModel, Media>();
        }
        protected void CreateMenuMap()
        {
            CreateMap<Menu, MenuModel>()
                .ForMember(model => model.AvailableLanguageType, option => option.Ignore());

            CreateMap<MenuModel, Menu>();

            CreateMap<MenuItem, MenuItemModel>();
            CreateMap<MenuItemModel, MenuItem>();
        }
        protected void CreateActivityLogMap()
        {
            CreateMap<ActivityLog, ActivityLogModel>()
                .ForMember(a => a.ActivityLogType, option => option.Ignore());

            CreateMap<ActivityLogModel, ActivityLog>();
        }
        protected void CreateFaqGroupMap()
        {
            CreateMap<FaqGroup, FaqGroupModel>();
            CreateMap<FaqGroupModel, FaqGroup>();

            CreateMap<Faq, FaqModel>();
            CreateMap<FaqModel, Faq>();
        }
        protected void CreateSettingMap()
        {
            CreateMap<BlogSetting, BlogSettingModel>();
            CreateMap<BlogSettingModel, BlogSetting>();

            CreateMap<OrderSetting, OrderSettingModel>();
            CreateMap<OrderSettingModel, OrderSetting>();

            CreateMap<CommonSetting, CommonSettingModel>();
            CreateMap<CommonSettingModel, CommonSetting>();

            CreateMap<SecuritySetting, SecuritySettingModel>();
            CreateMap<SecuritySettingModel, SecuritySetting>();
        }
        protected void CreateContactUsMap()
        {
            CreateMap<ContactUs, ContactUsModel>();
            CreateMap<ContactUsModel, ContactUs>();
        }

        #endregion

        #region Portal
        protected void PortalMapper()
        {
            CreateCategoryPortalMap();
            CreateBlogMap();
            CreateGalleryMap();
            CreateStaticPageMap();
            CreateDynamicPageMap();
        }
        protected void CreateCategoryPortalMap()
        {
            CreateMap<Entities.Domain.Portal.Category, Web.Areas.Admin.Models.Portal.CategoryModel>()
                .ForMember(model => model.AvailableCategories, option => option.Ignore());

            CreateMap<Web.Areas.Admin.Models.Portal.CategoryModel, Entities.Domain.Portal.Category>();
        }
        protected void CreateBlogMap()
        {
            CreateMap<Blog, BlogModel>()
                .ForMember(model => model.AvailableCommentStatusType, option => option.Ignore())
                .ForMember(model => model.BlogMediaSearchModel, option => option.Ignore());

            CreateMap<BlogModel, Blog>();

            CreateMap<BlogMedia, BlogMediaModel>();
            CreateMap<BlogMediaModel, BlogMedia>();

            CreateMap<BlogComment, BlogCommentModel>();
            CreateMap<BlogCommentModel, BlogComment>();
        }
        protected void CreateGalleryMap()
        {
            CreateMap<Gallery, GalleryModel>();
            CreateMap<GalleryModel, Gallery>();

            CreateMap<GalleryMedia, GalleryMediaModel>();
            CreateMap<GalleryMediaModel, GalleryMedia>();
        }
        protected void CreateStaticPageMap()
        {
            CreateMap<StaticPage, StaticPageModel>()
                .ForMember(model => model.AvailableViewStatusType, option => option.Ignore());

            CreateMap<StaticPageModel, StaticPage>();
        }

        protected void CreateDynamicPageMap()
        {
            CreateMap<DynamicPage, DynamicPageModel>();
            CreateMap<DynamicPageModel, DynamicPage>();

            CreateMap<DynamicPageDetail, DynamicPageDetailModel>()
                .ForMember(model => model.AvailableViewStatusType, option => option.Ignore());

            CreateMap<DynamicPageDetailModel, DynamicPageDetail>();
        }
        #endregion

        #region Application
        protected void ApplicationMapper()
        {
            CreateDeliveryDateMapper();
            CreateShippingCostMapper();
            CreateCategoryProdcutMap();
            CreateProductMap();
            CreateProductAttributeMap();
            CreateProductQuestionMap();
        }

        private void CreateDeliveryDateMapper()
        {
            CreateMap<App.DeliveryDate, AppModel.DeliveryDateModel>()
                .ForMember(model => model.AvailableEnabledType, option => option.Ignore());

            CreateMap<AppModel.DeliveryDateModel, App.DeliveryDate>();
        }
        private void CreateShippingCostMapper()
        {
            CreateMap<App.ShippingCost, AppModel.ShippingCostModel>()
                .ForMember(model => model.AvailableEnabledType, option => option.Ignore());

            CreateMap<AppModel.ShippingCostModel, App.ShippingCost>();
        }
        protected void CreateCategoryProdcutMap()
        {
            CreateMap<App.Category, AppModel.CategoryModel>()
                .ForMember(model => model.AvailableCategories, option => option.Ignore())
                .ForMember(model => model.AvailableEnableType, option => option.Ignore());

            CreateMap<AppModel.CategoryModel, App.Category>();
        }
        protected void CreateProductMap()
        {
            CreateMap<App.Product, AppModel.ProductModel>()
                .ForMember(model => model.AvailableCategories, option => option.Ignore())
                .ForMember(model => model.AvailableDeliveryDate, option => option.Ignore())
                .ForMember(model => model.AvailableDiscountType, option => option.Ignore())
                .ForMember(model => model.AvailableShippingCost, option => option.Ignore());

            CreateMap<AppModel.ProductModel, App.Product>();

            CreateMap<App.ProductMedia, AppModel.ProductMediaModel>();
            CreateMap<AppModel.ProductMediaModel, App.ProductMedia>();

            CreateMap<App.ProductComment, AppModel.ProductCommentModel>()
                .ForMember(model => model.AvailableCommentStatusType, option => option.Ignore())
                .ForMember(model => model.ProductName, option => option.MapFrom(p => p.Product.Name))
                .ForMember(model => model.FullName, option => option.MapFrom(u => $"{u.User.FirstName} {u.User.LastName}"));
            CreateMap<AppModel.ProductCommentModel, App.ProductComment>();
        }
        protected void CreateProductAttributeMap()
        {
            CreateMap<App.ProductAttribute, AppModel.ProductAttributeModel>();
            CreateMap<AppModel.ProductAttributeModel, App.ProductAttribute>();

            CreateMap<App.ProductAttributeMapping, AppModel.ProductAttributeMappingModel>()
                .ForMember(model => model.AvailableAttributeControlType, option => option.Ignore());
            CreateMap<AppModel.ProductAttributeMappingModel, App.ProductAttributeMapping>();

            CreateMap<App.ProductAttributeValue, AppModel.ProductAttributeValueModel>();
            CreateMap<AppModel.ProductAttributeValueModel, App.ProductAttributeValue>();

            CreateMap<App.RelatedProduct, AppModel.RelatedProductModel>()
                .ForMember(model => model.ProductName2, option => option.Ignore());
            CreateMap<AppModel.RelatedProductModel, App.RelatedProduct>();
        }
        protected void CreateProductQuestionMap()
        {
            CreateMap<ProductQuestion, ProductQuestionModel>()
                .ForMember(model => model.ViewStatusTitle, option => option.MapFrom(p => p.ViewStatus.Title))
                .ForMember(model => model.ProductName, option => option.MapFrom(p => p.Product.Name))
                .ForMember(model => model.FullName, option => option.MapFrom(u => $"{u.User.FirstName} {u.User.LastName}"))
                .ForMember(model => model.AvailableViewStatus, option => option.Ignore());

            CreateMap<ProductQuestionModel, ProductQuestion>();

            CreateMap<ProductAnswers, ProductAnswersModel>()
                 .ForMember(model => model.ViewStatusTitle, option => option.MapFrom(p => p.ViewStatus.Title))
                 .ForMember(model => model.FullName, option => option.MapFrom(u => $"{u.User.FirstName} {u.User.LastName}"))
                 .ForMember(model => model.AvailableViewStatus, option => option.Ignore());

            CreateMap<ProductAnswersModel, ProductAnswers>();
        }
        #endregion
    }
}
