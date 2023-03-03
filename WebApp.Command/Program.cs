using BaseProject.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Command.Models;

namespace BaseProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            var identityDbContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            identityDbContext.Database.Migrate();//veritabaný yoksa hem veritabaný olusturur tüm migrationlarý olusturur.

            if (!userManager.Users.Any())
            {
                userManager.CreateAsync(new AppUser() { UserName = "Test", Email = "test1@outlook.com", }, "Password12*").Wait();
                userManager.CreateAsync(new AppUser() { UserName = "Test1", Email = "test2@outlook.com", }, "Password12*").Wait();
                userManager.CreateAsync(new AppUser() { UserName = "Test2", Email = "test3@outlook.com", }, "Password12*").Wait();
                userManager.CreateAsync(new AppUser() { UserName = "Test3", Email = "test4@outlook.com", }, "Password12*").Wait();


                Enumerable.Range(1, 30).ToList().ForEach(x =>
                {
                    identityDbContext.Products.Add(

                        new Product(){ Name = $"kalem {x}", Price = x + 100, Stock = x + 50 }
                     
                    );
                });
                identityDbContext.SaveChanges();





            }


            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
