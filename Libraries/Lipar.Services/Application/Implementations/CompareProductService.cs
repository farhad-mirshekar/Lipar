using Lipar.Core.Common;
using Lipar.Entities.Domain.Application;
using Lipar.Services.Application.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lipar.Services.Application.Implementations
{
    public class CompareProductService : ICompareProductService
    {
        #region Ctor
        public CompareProductService(IHttpContextAccessor httpContextAccessor
                                   , IProductService productService)
        {
            _httpContextAccessor = httpContextAccessor;
            _productService = productService;
        }
        #endregion

        #region Fields
        private readonly int CompareProductsNumber = 3;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductService _productService;
        #endregion

        public void AddProductToCompareList(int ProductId)
        {
           if(_httpContextAccessor.HttpContext?.Response == null)
            {
                return;
            }

            //get list of compared product identifiers
            var comparedProductIds = GetComparedProductIds();

            //whether product identifier to add already exist
            if (!comparedProductIds.Contains(ProductId))
                comparedProductIds.Insert(0, ProductId);

            //limit list based on the allowed number of products to be compared
            comparedProductIds = comparedProductIds.Take(CompareProductsNumber).ToList();

            //create cookie
            AddCompareProductsCookie(comparedProductIds);
        }

        public void ClearCompareProducts()
        {
            if(_httpContextAccessor.HttpContext?.Response == null)
            {
                return;
            }

            var cookieName = $"{CookieDefaults.Prefix}{CookieDefaults.ComparedProductsCookie}";
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookieName);
        }

        public IEnumerable<Product> GetCompareProducts()
        {
            //get list of compared product identifiers
            var productIds = GetComparedProductIds();

            //return list of product
            return _productService.GetByIds(productIds.ToArray())
                .Where(product => product.Published && !product.RemoverId.HasValue).ToList();
        }

        public void RemoveProductFromCompareList(int ProductId)
        {
            if(_httpContextAccessor.HttpContext?.Response == null)
            {
                return;
            }

            var comparedProductIds = GetComparedProductIds();

            if (!comparedProductIds.Contains(ProductId))
            {
                return;
            }

            //if exists, so remove it from list
            comparedProductIds.Remove(ProductId);

            //set cookie
            AddCompareProductsCookie(comparedProductIds);
        }

        #region Utilities
        private IList<int> GetComparedProductIds()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext?.Request == null)
            {
                return new List<int>();
            }

            //try to get cookie
            var cookieName = $"{CookieDefaults.Prefix}{CookieDefaults.ComparedProductsCookie}";
            if(!httpContext.Request.Cookies.TryGetValue(cookieName, out var productIdsCookie) || string.IsNullOrEmpty(productIdsCookie))
            {
                return new List<int>();
            }

            //get array of string product identifiers from cookie
            var productIds = productIdsCookie.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            return productIds.Select(int.Parse).Distinct().ToList();
        }

        private void AddCompareProductsCookie(IList<int> comparedProductIds)
        {
            var cookieName = $"{CookieDefaults.Prefix}{CookieDefaults.ComparedProductsCookie}";
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookieName);

            //create cookie value
            var comparedProductIdsCookie = string.Join(",", comparedProductIds);

            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddHours(1),
                HttpOnly = true,
                Secure = true
            };

            //create cookie
            _httpContextAccessor.HttpContext.Response.Cookies.Append(cookieName, comparedProductIdsCookie, cookieOptions);
        }
        #endregion
    }
}
