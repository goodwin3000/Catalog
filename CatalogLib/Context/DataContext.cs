

using CatalogLib.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogLib.DataBase
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Widget> Widgets { get; set; }
       
        public DbSet<PayOutOption> PayOutOptions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        //public DbSet<OrderProduct> OrderProducts { get; set; }
        public DataContext() : base("DataContext")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
     
            modelBuilder.Entity<Category>()
                .HasKey(c => c.CategoryLinkName)
                .HasMany<Category>(c => c.SubCategories)
               .WithOptional(c => c.ParentCategory)
                .HasForeignKey(c => c.ParentId)
                .WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);

        }

        static DataContext()
        {
            Database.SetInitializer(new DatabaseInitializer());
        }
    }
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };
                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "admin@admin.net"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var passwordHash = new PasswordHasher();
                string password = passwordHash.HashPassword("admin");
                var admin = new ApplicationUser() { UserName = "Admin@Admin.net", Email = "Admin@Admin.net", PasswordHash = password, SecurityStamp = Guid.NewGuid().ToString() };
                manager.Create(admin);
                manager.AddToRole(admin.Id, "Admin");
            }

            if (!context.Roles.Any(r => r.Name == "User"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "User" };
                manager.Create(role);
            }
            if (!context.Users.Any(u => u.UserName == "user@user.net"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var passwordHash = new PasswordHasher();
                string password = passwordHash.HashPassword("user");
                var user = new ApplicationUser() { UserName = "user@user.net", Email = "user@user.net", PasswordHash = password, SecurityStamp = Guid.NewGuid().ToString() };
                manager.Create(user);
                manager.AddToRole(user.Id, "User");
            }
        

            DateTime now = DateTime.Now;

            var cat = new List<Category>()
         {      new Category  {CreatedDate=now,Published=false,CategoryLinkName="Category1",CategoryName="Category1",ParentId=null},
                new Category {CreatedDate=now,Published=true, CategoryLinkName="Category10",CategoryName="Category10",ParentId="Category1",CategoryPictureUrl="/Content/images/category/Cat.jpg"},
                new Category {CreatedDate=now,Published=true, CategoryLinkName="Category9",CategoryName="Category9",ParentId="Category1",CategoryPictureUrl="/Content/images/category/Cat.jpg"},
                new Category {CreatedDate=now,Published=true, CategoryLinkName="Category8",CategoryName="Category8",ParentId="Category1",CategoryPictureUrl="/Content/images/category/Cat.jpg"},
                
                new Category {CreatedDate=now,Published=true, CategoryLinkName="Category2",CategoryName="Category2",ParentId="Category1",CategoryPictureUrl="/Content/images/category/Cat.jpg"},
                new Category {CreatedDate=now,Published=true, CategoryLinkName="Category3",CategoryName="Category3",ParentId="Category1",CategoryPictureUrl="/Content/images/category/Cat.jpg"},
                new Category {CreatedDate=now,Published=true, CategoryLinkName="Category4",CategoryName="Category4",ParentId="Category2",CategoryPictureUrl="/Content/images/category/Cat.jpg"},
                new Category {CreatedDate=now,Published=true, CategoryLinkName="Category5",CategoryName="Category5",ParentId="Category2",CategoryPictureUrl="/Content/images/category/Cat.jpg"},
                new Category {CreatedDate=now,Published=true, CategoryLinkName="Category6",CategoryName="Category6",ParentId="Category3",CategoryPictureUrl="/Content/images/category/Cat.jpg"},
                new Category {CreatedDate=now,Published=true, CategoryLinkName="Category7",CategoryName="Category7",ParentId="Category6",CategoryPictureUrl="/Content/images/category/Cat.jpg"}
            };
            var productList = new List<Product>();
            Random rnd = new Random();
            for (int i = 0; i < 60; i++)
            {
               
               int catNum= rnd.Next(2, 7);
                var product = new Product
                {
                    CreatedDate = now,
                    ProductName = "Item" + i.ToString(),
                    CategoryLinkName = "Category" + catNum.ToString(),
                    ProductDescription = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum",
                    ProductPictureUrl = "/Content/images/product/product" + catNum + ".jpg",
                    Published = true,
                    ProductPrice = catNum * 2 + 37
                };
                productList.Add(product);
            }
 
            StringBuilder sb = new StringBuilder();

            sb.Append(@" <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.Nam viverra euismod odio, gravida pellentesque urna varius vitae.</p>");
            sb.Append(@" <a href = ""#"" >  <img class=""img-responsive"" src=""http://placehold.it/700x400"" ></a>");
            sb.Append(@" <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit.Delectus deleniti dignissimos fugiat magnam minima neque quas.Aliquid aperiam cum ducimus expedita laboriosam, mollitia, quaerat repellat reprehenderit rerum sunt velit veritatis?</p>");
            sb.Append(@" <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit.Delectus deleniti dignissimos fugiat magnam minima neque quas.Aliquid aperiam cum ducimus expedita laboriosam, mollitia, quaerat repellat reprehenderit rerum sunt velit veritatis?</p>");



            var widgetList = new List<Widget>()
            {
                 new Widget () {Position=WidgetPosition.Middle, Title="Добро пожаловать!",ColumnNumber=2,Text=sb.ToString(), Published=true },
                 new Widget () {Position=WidgetPosition.Middle, Title="Добро пожаловать!",ColumnNumber=3,Text=sb.ToString(), Published=true },
                new Widget ()  {Position=WidgetPosition.Middle, Title="Добро пожаловать!",ColumnNumber=1,Text=sb.ToString(), Published=true },
            };

            var payOptions = new List<PayOutOption>() {
                new PayOutOption() {  OptionDescription="Оплата на расчетный счет", OptionName="Оплата на расчетный счет"},
                new PayOutOption() {  OptionDescription="Оплата на карточный счет", OptionName="Оплата на карточный счет"},
                new PayOutOption() {  OptionDescription=@"Оплата в отделении ""Новой Почты""", OptionName=@"Оплата в отделении ""Новой Почты"""},
                new PayOutOption() {  OptionDescription="Оплата наличными", OptionName="Оплата наличными"}
                };
            var orderStatus = new List<OrderStatus>() {
                 new OrderStatus() { Status="Не подтвержден"},
                new OrderStatus() {  Status="Оплачен"},
                new OrderStatus() { Status="Подтвержден"},                
                 new OrderStatus() { Status="Отправлен"},
                new OrderStatus() { Status="Отменен"}
                };
            context.PayOutOptions.AddRange(payOptions);
            context.Widgets.AddRange(widgetList);
            context.Categorys.AddRange(cat);
            context.Products.AddRange(productList);
            context.OrderStatus.AddRange(orderStatus);

            context.SaveChanges();
        }

    }
}
