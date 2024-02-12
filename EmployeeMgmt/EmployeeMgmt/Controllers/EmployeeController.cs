using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using EmployeeMgmt.Data;
using EmployeeMgmt.Dto;
using EmployeeMgmt.Model;
using static EmployeeMgmt.Model.Employee;

namespace employeemgmt.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Applying authorization at the controller level
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeData _employeeData;

        public EmployeeController(EmployeeData employeeData)
        {
            _employeeData = employeeData;
        }

        [HttpGet]
        [Authorize(Roles = "manager")]
        public IActionResult GetAllEmployees()
        {
            var userId = GetCurrentUserId();
            var reportees = _employeeData.GetReportees(userId);
            return Ok(reportees);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,manager")]
        public IActionResult GetEmployeeById(int id)
        {
            if (!User.IsInRole(Roles.Admin.ToString()) && !User.IsInRole(Roles.Manager.ToString()) && GetCurrentUserId() != id)
            {
                return Forbid("You are not authorized to view this employee's details");
            }
            var employee = _employeeData.GetEmployeeById(id);
            return Ok(employee);
        }

        [HttpPost("managers")]
        [Authorize(Roles = "admin")]
        public IActionResult AddManager([FromBody] Manager managerRequest)
        {
            var newManager = new Employee
            {
                UserName = managerRequest.ManagerName,
                Password = managerRequest.Password,
                Role = Roles.Manager,
                ManagerId = 1,
            };
            _employeeData.AddEmployee(newManager);
            return Ok("Manager added successfully");
        }

        [HttpPost]
        [Authorize(Roles = "manager")]
        public IActionResult AddEmployee([FromBody] AddAllMembersDTO employeeDto)
        {
            var newEmployee = new Employee
            {
                UserName = employeeDto.UserName,
                Password = employeeDto.Password,
                Role = Roles.Employee,
                ManagerId = GetCurrentUserId()
            };
            _employeeData.AddEmployee(newEmployee);
            return Ok("Employee added successfully");
        }

        [HttpPost("apply-leave")]
        [Authorize(Roles = "employee")]
        public IActionResult ApplyLeave([FromBody] LeaveApplicationDTO leaveApplicationDto)
        {
            var employeeId = GetCurrentUserId();
            var success = _employeeData.ApplyLeave(employeeId, leaveApplicationDto.StartDate, leaveApplicationDto.EndDate);
            if (!success)
            {
                return BadRequest("Failed to apply leave");
            }
            return Ok("Leave applied successfully");
        }

        [HttpPost("approve-leave")]
        [Authorize(Roles = "manager")]
        public IActionResult ApproveLeave([FromBody] LeaveApprovalDTO leaveApprovalDto)
        {
            var managerId = GetCurrentUserId();
            var success = _employeeData.ApproveLeave(managerId, leaveApprovalDto.EmployeeId, leaveApprovalDto.ApplicationId, leaveApprovalDto.Reason);
            if (!success)
            {
                return BadRequest("Leave approval failed");
            }
            return Ok("Leave approved successfully");
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            return 0;
        }
    }
}





//using EmployeeMgmt.Data;
//using EmployeeMgmt.Dto;
//using EmployeeMgmt.Model;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Security.Claims;
//using static EmployeeMgmt.Model.Employee;


////namespace EmployeeMgmt.Controllers
////{
////    [Authorize(Roles = "Manager")]
////    public class EmployeeController : Controller
////    {
////        public IActionResult Index()
////        {
////            var employees = EmployeeData.Instance.Employees.Where(e => e.ManagerId == GetCurrentUserId()).ToList();
////            return View(employees);
////        }

////        [HttpPost]
////        public IActionResult AddEmployee(Employee employee)
////        {
////            if (EmployeeData.Instance.Employees.Any(e => e.Email == employee.Email))
////            {
////                ModelState.AddModelError("Email", "Employee with this email already exists.");
////                return View(employee);
////            }

////            employee.EmployeeId = EmployeeData.Instance.Employees.Count + 1;
////            employee.ManagerId = GetCurrentUserId();
////            EmployeeData.Instance.Employees.Add(employee);

////            return RedirectToAction("Index");
////        }

////        private int GetCurrentUserId()
////        {
////            return int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
////        }
////    }
////}



//namespace EmployeeMgmt.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class Employeecontroller : Controller
//    {
//        private EmployeeData _employeedata;

//        public Employeecontroller(EmployeeData employeedata)
//        {
//            _employeedata = employeedata;
//        }

//        [HttpGet]
//        [Authorize(Roles = "manager")]
//        public IActionResult Getallemployees()
//        {
//            var userid = getcurrentuserid();
//            var reportees = _employeedata.GetReportees(userid);
//            return Ok(reportees);
//        }


//        [HttpGet("{id}")]
//        [Authorize]
//        public IActionResult getemployeebyid(int id)
//        {
//            if (!User.IsInRole(Roles.Admin.ToString()) && !User.IsInRole(Roles.Manager.ToString()) && getcurrentuserid() != id)
//            {
//                return Forbid("you are not authorized to view this employee's details");
//            }
//            var employee = _employeedata.GetEmployeeById(id);
//            return Ok(employee);
//        }

//        [HttpPost("login")]
//        public IActionResult Login([FromBody] Login loginrequest)
//        {
//            var employee = _employeedata.FindEmployee(loginrequest.UserName, loginrequest.Password);
//            if (employee == null)
//            {
//                return Unauthorized("invaid username or password");
//            }
//            var claims = new List<Claim>
//            {
//                new Claim(ClaimTypes.NameIdentifier, employee.EmployeeId .ToString()),
//                new Claim(ClaimTypes.Name , employee.UserName),
//                new Claim(ClaimTypes.Role , employee.Role.ToString())
//            };
//            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
//            var principal = new ClaimsPrincipal(claimsIdentity);

//            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
//            return Ok("logged in successfully");
//        }

//        [HttpPost("managers")]
//        [Authorize(Roles = "admin")]
//        public IActionResult Addmanager([FromBody] Manager managerrequest)
//        {
//            var newmanager = new Employee
//            {
//                UserName = managerrequest.ManagerName,
//                Password = managerrequest.Password,
//                Role = Roles.Manager,
//                ManagerId = 1,
//            };
//            _employeedata.AddEmployee(newmanager);
//            return Ok("manager added successfully");
//        }

//        [HttpPost]
//        [Authorize(Roles = "manager")]
//        public IActionResult Addemployee([FromBody] AddAllMembersDTO employeedto)
//        {
//            var newemployee = new Employee
//            {
//                UserName = employeedto.UserName,
//                Password = employeedto.Password,
//                Role = Roles.Employee,
//                ManagerId = getcurrentuserid()
//            };
//            _employeedata.AddEmployee(newemployee);
//            return Ok("employee added successfully");
//        }

//        [HttpPost("apply-leave")]
//        [Authorize(Roles = "employee")]
//        public IActionResult Applyleave([FromBody] LeaveApplicationDTO leaveapplicationdto)
//        {
//            var employeeid = getcurrentuserid();
//            var success = _employeedata.ApplyLeave(employeeid, leaveapplicationdto.StartDate, leaveapplicationdto.EndDate);
//            if (!success)
//            {
//                return BadRequest("failed to apply leave");
//            }
//            return Ok("leave applied successfully");
//        }


//        [HttpPost("approve-leave")]
//        [Authorize(Roles = "manager")]
//        public IActionResult approveleave([FromBody] LeaveApprovalDTO leaveapprovaldto)
//        {
//            var managerid = getcurrentuserid();
//            var success = _employeedata.ApproveLeave(managerid, leaveapprovaldto.EmployeeId, leaveapprovaldto.ApplicationId, leaveapprovaldto.Reason);
//            if (!success)
//            {
//                return BadRequest("leave approval failed");
//            }
//            return Ok("leave approved successfully");
//        }

//        private int getcurrentuserid()
//        {

//            var useridclaim = User.FindFirst(ClaimTypes.NameIdentifier);
//            if (useridclaim != null && int.TryParse(useridclaim.Value, out int userid))
//            {
//                return userid;
//            }
//            return 0;
//        }
//    }
//}
