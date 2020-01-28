﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortProject.Models
{
    public class SeedData
    {
        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            if (!context.Users.Any())
            {
                UserManager<IdentityUser> userManager = app.ApplicationServices.GetRequiredService<UserManager<IdentityUser>>();

                string password = "Qwerty123123_";

                IdentityUser appUser = new IdentityUser()
                {
                    UserName = "Administrator123_",
                    Email = "admin@admin.ru"
                };

                IdentityResult result = await userManager.CreateAsync(appUser, password);
            }

            context.SaveChanges();
        }
    }
}
