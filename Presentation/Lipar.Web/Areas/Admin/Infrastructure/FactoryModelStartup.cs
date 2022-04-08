using Lipar.Core.Infrastructure;
using Lipar.Web.Areas.Admin.Factories;
using App = Lipar.Web.Areas.Admin.Factories.Application;
using Lipar.Web.Areas.Admin.Factories.Organization;
using Lipar.Web.Areas.Admin.Factories.Portal;
using Lipar.Web.Areas.Admin.Factories.General;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Lipar.Web.Areas.Admin.Factories.Financial;

namespace Lipar.Web.Areas.Admin.Infrastructure
{
    public class FactoryModelStartup : ILiparStartup
    {
        public int Order => 1001;

        public void Configure(IApplicationBuilder application)
        {

        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IBaseAdminModelFactory, BaseAdminModelFactory>();

            OrganizationService(services);

            GeneralService(services);

            PortalService(services);

            ApplicationService(services);

            FinancialService(services);
        }

        protected void OrganizationService(IServiceCollection services)
        {
            services.AddTransient<IUserModelFactory, UserModelFactory>();
            services.AddTransient<IRoleModelFactory, RoleModelFactory>();
            services.AddTransient<ICommandModelFactory, CommandModelFactory>();
            services.AddTransient<IPositionModelFactory, PositionModelFactory>();
        }
        protected void GeneralService(IServiceCollection services)
        {
            services.AddTransient<ILanguageModelFactory, LanguageModelFactory>();
            services.AddTransient<IMediaModelFactory, MediaModelFactory>();
            services.AddTransient<IMenuModelFactory, MenuModelFactory>();
            services.AddTransient<IActivityLogModelFactory, ActivityLogModelFactory>();
            services.AddTransient<IFaqGroupModelFactory, FaqGroupModelFactory>();
            services.AddTransient<ISettingModelFactory, SettingModelFactory>();
            services.AddTransient<IContactUsModelFactory, ContactUsModelFactory>();
            services.AddTransient<IEmailAccountModelFactory, EmailAccountModelFactory>();
            services.AddTransient<IMessageTemplateModelFactory, MessageTemplateModelFactory>();
        }

        protected void PortalService(IServiceCollection services)
        {
            services.AddTransient<ICategoryModelFactory, CategoryModelFactory>();
            services.AddTransient<IBlogModelFactory, BlogModelFactory>();
            services.AddTransient<IGalleryModelFactory, GalleryModelFactory>();
            services.AddTransient<IStaticPageModelFactory, StaticPageModelFactory>();
            services.AddTransient<IDynamicPageModelFactory, DynamicPageModelFactory>();
        }

        protected void ApplicationService(IServiceCollection services)
        {
            services.AddTransient<App.IDeliveryDateModelFactory, App.DeliveryDateModelFactory>();
            services.AddTransient<App.IShippingCostModelFactory, App.ShippingCostModelFactory>();
            services.AddTransient<App.ICategoryModelFactory, App.CategoryModelFactory>();
            services.AddTransient<App.IProductModelFactory, App.ProductModelFactory>();
            services.AddTransient<App.IProductAttributeModelFactory, App.ProductAttributeModelFactory>();
            services.AddTransient<App.IProductCommentModelFactory, App.ProductCommentModelFactory>();
            services.AddTransient<App.IProductQuestionModelFactory, App.ProductQuestionModelFactory>();
            services.AddTransient<App.IProductTagModelFactory, App.ProductTagModelFactory>();
        }

        protected void FinancialService(IServiceCollection services)
        {
            services.AddTransient<IBankModelFactory, BankModelFactory>();
        }
    }
}
