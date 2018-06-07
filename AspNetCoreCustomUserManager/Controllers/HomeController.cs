// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreCustomUserManager
{
  public class HomeController : Controller
  {
    private IUserManager userManager;

    public HomeController(IUserManager userManager)
    {
      this.userManager = userManager;
    }

    [HttpGet]
    public IActionResult Index()
    {
      return this.View();
    }

    [HttpPost]
    public IActionResult Login()
    {
      ValidateResult validateResult = this.userManager.Validate("Email", "admin@example.com", "admin");

      if (validateResult.Success)
        this.userManager.SignIn(this.HttpContext, validateResult.User, false);

      return this.RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Logout()
    {
      this.userManager.SignOut(this.HttpContext);
      return this.RedirectToAction("Index");
    }
  }
}