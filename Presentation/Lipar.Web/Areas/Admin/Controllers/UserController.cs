using Lipar.Entities.Domain.Organization;
using Lipar.Services.Organization.Contracts;
using Lipar.Web.Areas.Admin.Factories.Organization;
using Lipar.Web.Areas.Admin.Models.Organization;
using Lipar.Web.Framework.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lipar.Web.Areas.Admin.Controllers
{
    public class UserController : BaseAdminController
    {
        private readonly IUserModelFactory _userModelFactory;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public UserController(IUserModelFactory userModelFactory
                            , IUserService userService
                            , IRoleService roleService)
        {
            _userModelFactory = userModelFactory;
            _userService = userService;
            _roleService = roleService;
        }
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            var model = new UserSearchModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult List(UserSearchModel model)
        {
            var usersResult = _userModelFactory.PrepareUserListModel(model);
            return Json(usersResult);
        }

        public IActionResult Edit(int Id)
        {
            var user = _userService.GetById(Id);
            var model = _userModelFactory.PrepareUserModel(null, user);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(UserModel model , IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                //get all role
                var roles = _roleService.List(new RoleListVM { });
                foreach (var role in roles)
                {
                    //create form key for check selected checkbox
                    var formKey = $"allowed_{role.Id}";

                    var roleSelected = !StringValues.IsNullOrEmpty(form[formKey])
                      ? form[formKey].ToString().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList()
                      : new List<string>();

                    var allowed = roleSelected.Contains(role.Id.ToString());

                    if (allowed) //کاربر انتخاب کرده
                    {

                    }
                    else
                    {

                    }
                }

            }

            model = _userModelFactory.PrepareUserModel(model, null);
            return View(model);
        }
    }
}
