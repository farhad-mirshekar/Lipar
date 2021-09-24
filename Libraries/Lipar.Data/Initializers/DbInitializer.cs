using Microsoft.AspNetCore.Builder;

namespace Lipar.Data.Initializers
{
    public static class DbInitializer
    {
        public static void Initialize(IApplicationBuilder app)
        {

            //using (var serviceScope = app.ApplicationServices.CreateScope())
            //{
            //    var context = serviceScope.ServiceProvider.GetService<LiparContext>();

            //    context.Database.EnsureDeleted();
            //    context.Database.EnsureCreated();

            //    var application = new Center
            //    {
            //        Code = "1",
            //        Name = "سایت لیپار",
            //        EnabledType = Entities.EnabledType.Active
            //    };

            //    context.Add(application);
            //    context.SaveChanges();

            //    var user = new User
            //    {
            //        CellPhone = "09355588908",
            //        EnabledType = Entities.EnabledType.Active,
            //        FirstName = "farhad",
            //        LastName = "mirshekar",
            //        Username = "mirshekar",
            //        NationalCode = "0017300762"
            //    };
            //    context.Add(user);
            //    context.SaveChanges();

            //    var dept = new Department
            //    {
            //        ApplicationId = 1,
            //        CodePhone = "1231232",
            //        EnabledType = Entities.EnabledType.Active,
            //        Name = "سامانه اصلی - لیپار",
            //        Phone = "23432",
            //        PostalCode = "445343",
            //        Type = Entities.DepartmentType.سامانه_اصلی
            //    };
            //    context.Add(dept);

            //    var dept1 = new Department
            //    {
            //        ApplicationId = 1,
            //        CodePhone = "1231232",
            //        EnabledType = Entities.EnabledType.Active,
            //        Name = "مقاله نویسی",
            //        Phone = "23432",
            //        PostalCode = "445343",
            //        Type = Entities.DepartmentType.سامانه_اصلی,
            //        ParentId = 1
            //    };
            //    context.Add(dept1);
            //    context.SaveChanges();

            //    var pos = new Position
            //    {
            //        ApplicationId = 1,
            //        Default = true,
            //        DepartmentId = 1,
            //        EnabledType = Entities.EnabledType.Active,
            //        PositionType = Entities.PositionType.مدیر_سایت,
            //        UserId = 1
            //    };
            //    context.Add(pos);
            //    context.SaveChanges();
            //}

        }
    }
}
