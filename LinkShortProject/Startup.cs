﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkShortProject.Models;
using LinkShortProject.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkShortProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   Configuration["DB:LocalConnectionString"]
               ));

            services.AddIdentity<IdentityUser, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<IShortLinkRepository, EFShortLinkRepository>();
            services.AddTransient<ShortLinkService>();

            services.ConfigureApplicationCookie(opts => opts.LoginPath = "/login");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                 name: "404",
                 template: "404",
                 defaults: new { controller = "Error", action = "Index" }
               );

                routes.MapRoute(
                 name: null,
                 template: "",
                 defaults: new { controller = "Cabinet", action = "Index" }
               );

                routes.MapRoute(
                   name: null,
                   template: "cabinet/{action}",
                   defaults: new { controller = "Cabinet", action = "Index" }
                 );


                routes.MapRoute(
                  name: null,
                  template: "login",
                  defaults: new { controller = "Login", action = "Index" }
                );

                routes.MapRoute(
                  name: null,
                  template: "login/{action}",
                  defaults: new { controller = "Login", action = "Index" }
                );

                routes.MapRoute(
                   name: "short",
                   template: "/{shortUrl}",
                   defaults: new { controller = "Link", action = "Index" }
                 );        
            });

            SeedData.EnsurePopulated(app);
        }
    }
}
