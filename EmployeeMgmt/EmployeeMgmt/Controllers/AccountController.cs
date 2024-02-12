using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using EmployeeMgmt.Data;
using EmployeeMgmt.Model;

namespace employeemgmt.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly EmployeeData _employeeData;

        public AuthController(EmployeeData employeeData)
        {
            _employeeData = employeeData;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login loginRequest)
        {
            var employee = _employeeData.FindEmployee(loginRequest.UserName, loginRequest.Password);
            if (employee == null)
            {
                return Unauthorized("Invalid username or password");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, employee.EmployeeId.ToString()),
                new Claim(ClaimTypes.Name, employee.UserName),
                new Claim(ClaimTypes.Role, employee.Role.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true // You can set whether to persist the authentication session or not
            };

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );

            return Ok("Logged in successfully");
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok("Logged out successfully");
        }
    }
}






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
