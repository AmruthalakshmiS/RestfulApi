//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;

//namespace EmployeeMgmt.Controllers
//{
//    // GET: Account/Login
//    public IActionResult Login()
//    {
//        return View();
//    }

//    // POST: Account/Login
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public IActionResult Login(string username, string password)
//    {
//        // Validate user credentials here
//        // For example, you can use a service to validate against a database

//        if (IsValidUser(username, password))
//        {
//            var identity = new ClaimsIdentity(new[] {
//                    new Claim(ClaimTypes.Name, username),
//                    new Claim(ClaimTypes.Role, "Employee"),
//                }, CookieAuthenticationDefaults.AuthenticationScheme);

//            var principal = new ClaimsPrincipal(identity);

//            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = true });

//            return RedirectToAction("Index", "Home");
//        }
//        else
//        {
//            ModelState.AddModelError("", "Invalid username or password.");
//        }

//        return View();
//    }

//    // GET: Account/Logout
//    public IActionResult Logout()
//    {
//        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(), new AuthenticationProperties { IsPersistent = false });
//        return RedirectToAction("Index", "Home");
//    }

//    private bool IsValidUser(string username, string password)
//    {
//        // Validate user credentials here
//        // For example, you can use a service to validate against a database
//        return username == "testuser" && password == "testpassword";
//    }
//}
//}
