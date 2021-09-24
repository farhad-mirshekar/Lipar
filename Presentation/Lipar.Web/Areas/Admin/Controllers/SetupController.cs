using Lipar.Data.Seeds;
using Lipar.Web.Areas.Admin.Models;
using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lipar.Web.Areas.Admin.Controllers
{
    public class SetupController : BaseAdminController
    {
        #region Methods
        public IActionResult Index()
        {
            var type = typeof(IBaseSeed);

            //find all seed
            var baseSeedTypes = Assembly.Load("Lipar.Data").GetTypes().Where(t => type.IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface).ToList();

            //create class by all seed
            List<IBaseSeed> seedClassList = baseSeedTypes.Select(t => Activator.CreateInstance(t) as IBaseSeed).ToList();

            var seedModels = seedClassList.Select(t => new SeedModel
            {
                schema = t.Schema,
                typeName = t.GetType().FullName,
                name = t.ModelName,
                sendIndex = 0,
                Items = GetItems(t.GetType()),
                count = t.Count,
            });
            return View(seedModels);
        }

        
        public IActionResult RunSeedModel(string typeName)
        {
            try
            {
                var seedType = Type.GetType(typeName + ",Lipar.Data");

                var seedObject = Activator.CreateInstance(seedType) as IBaseSeed;
                for (int index = 0; index < seedObject.Count; index++)
                {
                    seedObject.Add(index);
                }

                return Json(new { isSuccess = true, Message = "" });
            }
            catch (Exception e)
            {
                return Json(new { isSuccess = false, Message = e.ToString() });
            }
        }

        public IActionResult RunUpdateSeedModel(string typeName)
        {
            try
            {
                var seedType = Type.GetType(typeName + ",Lipar.Data");

                var seedObject = Activator.CreateInstance(seedType) as IBaseSeed;
                for (int index = 0; index < seedObject.Count; index++)
                {
                    seedObject.Update(index);
                }

                return Json(new { isSuccess = true, Message = "" });
            }
            catch (Exception e)
            {
                return Json(new { isSuccess = false, Message = e.ToString() });
            }
        }
        #endregion

        #region Utilities
        private IList GetItems(Type t)
        {
            var instance = Activator.CreateInstance(t);
            var propInfo = instance.GetType().GetProperty("Items");
            if (propInfo != null)
            {
                var result = propInfo.GetValue(instance, null);
                return result as IList;
            }
            return new List<object>();
        }
        #endregion
    }
}
