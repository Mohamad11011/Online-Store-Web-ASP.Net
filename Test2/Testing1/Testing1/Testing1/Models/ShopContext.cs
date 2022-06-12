
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using IdentityUser = Microsoft.AspNetCore.Identity.IdentityUser;
using MySql.Data.Entity;

namespace Testing1.Models
{ 
    public class ShopContext : DbContext
       /* IdentityDbContext<IdentityUser>*/
    {
        public ShopContext() 
        { 
        }
        public ShopContext(string connectionString) : base(GetOptions(connectionString)) {}
        
        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }
        public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public DbSet<LoginViewModel> Login { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder options)=>
            options.UseSqlServer(@"Data Source = (LocalDb)\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\ShopData.mdf;Initial Catalog = ShopData; Integrated Security = True");


        //Seeding Some Data 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    AccountID = 1,
                    AccountName = "Admin",
                    AccountMail = "Master@gmail.com",
                    Password = "Admin"
                }
                ,
                new Account
                {
                    AccountID = 2,
                    AccountName = "Mohamad",
                    AccountMail = "Moh6719@gmail.com",
                    Password = "12345"
                });

            modelBuilder.Entity<Product>().HasData(
                  new Product
                  {
                      ProductID = 1,
                      Name = "Acer 2",
                      Price = 450,
                      Category = "Laptop",

                  },
                  new Product
                  {
                      ProductID = 2,
                      Name = "Lenovo Thinkpad 3",
                      Price = 580,
                      Category = "Laptop",

                  }
                  ,
                  new Product
                  {
                      ProductID = 3,
                      Name = "Lenovo X2",
                      Price = 300,
                      Category = "PC",
                  });
            modelBuilder.Entity<LoginViewModel>().HasKey(am =>
                new
                {
                    am.Email,
                    am.Password

                }
                );
            modelBuilder.Entity<LoginViewModel>().HasData(
               new LoginViewModel
               {
                   Email="Admin@gmail.com",
                   Password="Admin@1"

               }
               );




        }
    }
}
