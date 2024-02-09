using EmployeeMgmt.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeMgmt.Controllers
{
    [Authorize(Roles = "Manager")]
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            var employees = EmployeeData.Instance.Employees.Where(e => e.ManagerId == GetCurrentUserId()).ToList();
            return View(employees);
        }

        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            if (EmployeeData.Instance.Employees.Any(e => e.Email == employee.Email))
            {
                ModelState.AddModelError("Email", "Employee with this email already exists.");
                return View(employee);
            }

            employee.EmployeeId = EmployeeData.Instance.Employees.Count + 1;
            employee.ManagerId = GetCurrentUserId();
            EmployeeData.Instance.Employees.Add(employee);

            return RedirectToAction("Index");
        }

        private int GetCurrentUserId()
        {
            return int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}
