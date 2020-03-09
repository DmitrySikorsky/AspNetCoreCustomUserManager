// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using AspNetCoreCustomUserManager.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNetCoreCustomUserManager
{
  public class Startup
  {
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
      this.Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<Storage>(
        options => options.UseSqlite(this.Configuration.GetConnectionString("DefaultConnection"))
      );

      services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
          {
            options.ExpireTimeSpan = TimeSpan.FromDays(7);
          }
        );

      services.AddScoped<IUserManager, UserManager>();
      services.AddMvc();
    }

    public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment webHostEnvironment)
    {
      if (webHostEnvironment.IsDevelopment())
        applicationBuilder.UseDeveloperExceptionPage();

      applicationBuilder.UseAuthentication();
      applicationBuilder.UseStaticFiles();
      applicationBuilder.UseRouting();
      applicationBuilder.UseEndpoints(builder => builder.MapDefaultControllerRoute());
    }
  }
}