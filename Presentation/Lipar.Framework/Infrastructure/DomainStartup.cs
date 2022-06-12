using Lipar.Core;
using Lipar.Core.Infrastructure;
using Lipar.Core.Security;
using Lipar.Data;
using AppContract = Lipar.Services.Application.Contracts;
using AppImplement = Lipar.Services.Application.Implementations;
using Lipar.Services.Authentication;
using Lipar.Services.Notification;
using Lipar.Services.Organization.Contracts;
using Lipar.Services.Organization.Implementations;
using Lipar.Services.Portal.Contracts;
using Lipar.Services.Portal.Implementations;
using Lipar.Services.General.Contracts;
using Lipar.Services.General.Implementations;
using Lipar.Services.Security;
using Lipar.Web.Framework.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Lipar.Services.Core.Contracts;
using Lipar.Services.Core.Implementations;
using Lipar.Services.Financial.Contracts;
using Lipar.Services.Financial.Implementations;
using Lipar.Services.Messages;

namespace Lipar.Web.Framework.Infrastructure
{
    public class DomainStartup : ILiparStartup
    {
        public int Order => 20;

        public void Configure(IApplicationBuilder application)
        {

        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IWorkContext, WebWorkContext>();

            //add organization service
            OrganizationService(services, configuration);

            //add general service
            GeneralService(services, configuration);

            //add portal service
            PortalService(services, configuration);

            //add application service
            ApplicationService(services, configuration);

            //add core service
            CoreService(services, configuration);

            //add financial service
            FinancialService(services, configuration);

            services.AddTransient<IEncryptionService, EncryptionService>();
            services.AddSingleton<IPageHeadBuilder, PageHeadBuilder>();
            services.AddTransient<INotificationService, NotificationService>();
        }

        protected void OrganizationService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICenterService, CenterService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<ICommandService, CommandService>();
            services.AddTransient<IPositionService, PositionService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthenticationService, CookieAuthenticationService>();
            services.AddTransient<IUserPasswordService, UserPasswordService>();
            services.AddTransient<IRolePermissionService, RolePermissionService>();
            services.AddTransient<IPositionRoleService, PositionRoleService>();
            services.AddTransient<ICommandTypeService, CommandTypeService>();
            services.AddTransient<IPositionTypeService, PositionTypeService>();
            services.AddTransient<IPasswordFormatTypeService, PasswordFormatTypeService>();
            services.AddTransient<IUserAddressService, UserAddressService>();
        }

        protected void GeneralService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ILanguageService, LanguageService>();
            services.AddTransient<ILocaleStringResourceService, LocaleStringResourceService>();
            services.AddTransient<IMediaService, MediaService>();
            services.AddTransient<IUrlRecordService, UrlRecordService>();
            services.AddTransient<IFroalaEditorService, FroalaEditorService>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<IMenuItemService, MenuItemService>();
            services.AddTransient<IActivityLogService, ActivityLogService>();
            services.AddTransient<IActivityLogTypeService, ActivityLogTypeService>();
            services.AddTransient<IFaqGroupService, FaqGroupService>();
            services.AddTransient<IFaqService, FaqService>();
            services.AddTransient<ISettingService, SettingService>();
            services.AddTransient<IContactUsService, ContactUsService>();
            services.AddTransient<IContactUsTypeService, ContactUsTypeService>();
            services.AddTransient<ILanguageCultureService, LanguageCultureService>();
            services.AddTransient<IEmailAccountService, EmailAccountService>();
            services.AddTransient<IMessageTemplateService, MessageTemplateService>();
            services.AddTransient<IGenericAttributeService, GenericAttributeService>();
             services.AddTransient<IQueuedEmailService, QueuedEmailService>();
            services.AddTransient<IWorkflowMessageService, WorkflowMessageService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IProvinceService, ProvinceService>();
            services.AddTransient<ICityService, CityService>();
        }

        protected void PortalService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IBlogService, BlogService>();
            services.AddTransient<IGalleryService, GalleryService>();
            services.AddTransient<IBlogMediaService, BlogMediaService>();
            services.AddTransient<IGalleryMediaService, GalleryMediaService>();
            services.AddTransient<IBlogCommentService, BlogCommentService>();
            services.AddTransient<IStaticPageService, StaticPageService>();
            services.AddTransient<IDynamicPageService, DynamicPageService>();
            services.AddTransient<IDynamicPageDetailService, DynamicPageDetailService>();
        }

        protected void ApplicationService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<AppContract.IDeliveryDateService, AppImplement.DeliveryDateService>();
            services.AddTransient<AppContract.IShippingCostService, AppImplement.ShippingCostService>();
            services.AddTransient<AppContract.ICategoryService, AppImplement.CategoryService>();
            services.AddTransient<AppContract.IProductService, AppImplement.ProductService>();
            services.AddTransient<AppContract.IProductAttributeService, AppImplement.ProductAttributeService>();
            services.AddTransient<AppContract.IProductAttributeValueService, AppImplement.ProductAttributeValueService>();
            services.AddTransient<AppContract.IProductAttributeMappingService, AppImplement.ProductAttributeMappingService>();
            services.AddTransient<AppContract.IProductMediaService, AppImplement.ProductMediaService>();
            services.AddTransient<AppContract.IRelatedProductService, AppImplement.RelatedProductService>();
            services.AddTransient<AppContract.ICompareProductService, AppImplement.CompareProductService>();
            services.AddTransient<AppContract.IProductCommentService, AppImplement.ProductCommentService>();
            services.AddTransient<AppContract.IProductQuestionService, AppImplement.ProductQuestionService>();
            services.AddTransient<AppContract.IProductAnswersService, AppImplement.ProductAnswersService>();
            services.AddTransient<AppContract.IAttributeControlTypeService, AppImplement.AttributeControlTypeService>();
            services.AddTransient<AppContract.IDiscountTypeService, AppImplement.DiscountTypeService>();
            services.AddTransient<AppContract.IShoppingCartItemService, AppImplement.ShoppingCartItemService>();
            services.AddTransient<AppContract.IOrderService, AppImplement.OrderService>();
            services.AddTransient<AppContract.IProductTagService, AppImplement.ProductTagService>();
            services.AddTransient<AppContract.IOrderPaymentStatusService, AppImplement.OrderPaymentStatusService>();
            services.AddTransient<AppContract.IOrderTrackingFlowService, AppImplement.OrderTrackingFlowService>();
        }

        protected void CoreService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICommentStatusService, CommentStatusService>();
            services.AddTransient<IViewStatusService, ViewStatusService>();
            services.AddTransient<IEnabledTypeService, EnabledTypeService>();
            services.AddTransient<IGenderService, GenderService>();
        }

        protected void FinancialService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IBankService, BankService>();
            services.AddTransient<IBankPortService, BankPortService>();
        }
    }
}
