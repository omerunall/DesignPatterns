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
                userManager.CreateAsync(new AppUser() { UserName = "Test", Email = "test1@outlook.com",PictureUrl = "/pictures/user.png",Description ="omer Acýklamasý" }, "Password12*").Wait();
                userManager.CreateAsync(new AppUser() { UserName = "Test1", Email = "test2@outlook.com", PictureUrl = "/pictures/user.png", Description = "omer Acýklamasý" }, "Password12*").Wait();
                userManager.CreateAsync(new AppUser() { UserName = "Test2", Email = "test3@outlook.com", PictureUrl = "/pictures/user.png", Description = "omer Acýklamasý" }, "Password12*").Wait();
                userManager.CreateAsync(new AppUser() { UserName = "Test3", Email = "test4@outlook.com", PictureUrl = "/pictures/user.png", Description = "omer Acýklamasý" }, "Password12*").Wait();



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
