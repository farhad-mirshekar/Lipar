using Lipar.Core.Infrastructure;
using Lipar.Services.General.Contracts;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lipar.Web.Framework.MVC.Attributes
{
    /// <summary>
    /// جهت الزام کردن یک فیلد برای فرم ها و نمایش پیغام اخطار بر اساس زبان
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class RequiredAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string _resourceKey;
        private ILocaleStringResourceService _resourceService;
        public RequiredAttribute(string resourceKey)
        {
            _resourceKey = resourceKey;
        }
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;
            return true;
        }
        public override string FormatErrorMessage(string name)
        {
            if (_resourceService == null)
            {
                _resourceService = EngineContext.Current.Resolve<ILocaleStringResourceService>();
            }

            //var resourceResult = _resourceService.GetResource(_resourceKey);
            //if (resourceResult.Success && !string.IsNullOrEmpty(resourceResult.Data))
            //    return string.Format("{0}", resourceResult.Data);

            return string.Format("{0}", "*");
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            //MergeAttribute(context.Attributes, "required", "true");
            MergeAttribute(context.Attributes, "data-val", "true");
            var errorMessage = FormatErrorMessage(_resourceKey);
            MergeAttribute(context.Attributes, "data-val-required", errorMessage);
        }

        private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);
            return true;
        }
    }
}
