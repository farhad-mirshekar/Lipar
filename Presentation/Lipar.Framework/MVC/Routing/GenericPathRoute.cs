using Lipar.Services.General.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;

namespace Lipar.Web.Framework.MVC.Routing
{
    public class GenericPathRoute : DynamicRouteValueTransformer
    {
        private readonly IUrlRecordService _urlRecordService;
        public GenericPathRoute(IUrlRecordService urlRecordService)
        {
            _urlRecordService = urlRecordService;
        }
        public override ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
        {
            if (values == null)
                return new ValueTask<RouteValueDictionary>(values);

            if (!values.TryGetValue("SeName", out var slugValue))
                return new ValueTask<RouteValueDictionary>(values);

            var slug = slugValue as string;

            var urlRecord = _urlRecordService.GetBySlug(slug.Trim());

            if (urlRecord == null || urlRecord.Id == Guid.Empty)
                return new ValueTask<RouteValueDictionary>(values);

            switch (urlRecord.EntityName.ToLower())
            {
                case "blog":
                    values[LiparPathRouteDefaults.ControllerFieldKey] = "blog";
                    values[LiparPathRouteDefaults.ActionFieldKey] = "Detail";
                    values[LiparPathRouteDefaults.BlogIdFieldKey] = urlRecord.EntityId;
                    values[LiparPathRouteDefaults.SeNameFieldKey] = urlRecord.Slug;
                    break;
                case "staticpage":
                    values[LiparPathRouteDefaults.ControllerFieldKey] = "staticpage";
                    values[LiparPathRouteDefaults.ActionFieldKey] = "Detail";
                    values[LiparPathRouteDefaults.StaticPageIdFieldKey] = urlRecord.EntityId;
                    values[LiparPathRouteDefaults.SeNameFieldKey] = urlRecord.Slug;
                    break;
                case "dynamicpage":
                    values[LiparPathRouteDefaults.ControllerFieldKey] = "DynamicPage";
                    values[LiparPathRouteDefaults.ActionFieldKey] = "Detail";
                    values[LiparPathRouteDefaults.DynamicPageIdFieldKey] = urlRecord.EntityId;
                    values[LiparPathRouteDefaults.SeNameFieldKey] = urlRecord.Slug;
                    break;
                case "dynamicpagedetail":
                    values[LiparPathRouteDefaults.ControllerFieldKey] = "DynamicPage";
                    values[LiparPathRouteDefaults.ActionFieldKey] = "DetailPage";
                    values[LiparPathRouteDefaults.DynamicPageDetailIdFieldKey] = urlRecord.EntityId;
                    values[LiparPathRouteDefaults.SeNameFieldKey] = urlRecord.Slug;
                    break;
                case "product":
                    values[LiparPathRouteDefaults.ControllerFieldKey] = "Product";
                    values[LiparPathRouteDefaults.ActionFieldKey] = "Detail";
                    values[LiparPathRouteDefaults.ProductIdFieldKey] = urlRecord.EntityId;
                    values[LiparPathRouteDefaults.SeNameFieldKey] = urlRecord.Slug;
                    break;
            }

            return new ValueTask<RouteValueDictionary>(values);
        }
    }
}
