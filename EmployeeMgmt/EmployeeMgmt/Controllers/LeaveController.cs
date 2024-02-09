//using EmployeeMgmt.Model;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace EmployeeMgmt.Controllers
//{
//    [Authorize]
//    public class LeaveController : Controller
//    {
//        public IActionResult ApplyLeave()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult ApplyLeave(LeaveApplication leave)
//        {
//            var employee = EmployeeData.Instance.Employees.FirstOrDefault(e => e.EmployeeId == GetCurrentUserId());

//            if (employee == null)
//            {
//                return BadRequest("Invalid employee id.");
//            }

//            leave.EmployeeId = EmployeeData.Instance.Leaves.Count + 1;
//            leave.EmployeeId = GetCurrentUserId();
//            leave.IsApproved = false;
//            EmployeeData.Instance.Leaves.Add(leave);

//            return RedirectToAction("Index", "Home");
//        }

//        [Authorize(Roles = "Manager")]
//        public IActionResult ApproveLeave(int id);
//}
