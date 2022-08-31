using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Lipar.Web.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetList()
        {
            var list = new List<Person>(10);
            for (int i = 0; i < 9; i++)
            {
                list.Add(new Person
                {
                    Id = i +1,
                    FirstName = $"fitstName {i}",
                    LastName = $"lastName {i}"
                });
            }

            return Json(new
            {
                data = list
            });
        }
    }
}

public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
