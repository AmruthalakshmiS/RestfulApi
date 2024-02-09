//using EmployeeMgmt.Model;

//namespace EmployeeMgmt
//{
//    public class EmployeeData
//    {
//        private static EmployeeData? _instance;
//        private readonly List<Employee> _employees;
//        private readonly List<LeaveApplication> _leaves;

//        private EmployeeData()
//        {
//            _employees = new List<Employee>();
//            _leaves = new List<LeaveApplication>();
//        }

//        public static EmployeeData Instance
//        {
//            get
//            {
//                if (_instance == null)
//                {
//                    _instance = new EmployeeData();
//                }

//                return _instance;
//            }
//        }

//        public List<Employee> Employees
//        {
//            get { return _employees; }
//        }

//        public List<LeaveApplication> Leaves
//        {
//            get { return _leaves; }
//        }
//    }
//}
using EmployeeMgmt.Model;


namespace EmployeeMgmt.Data
{
    public class EmployeeData
    {
        private readonly Dictionary<int, Employee> _employees;
        private int _employeeId = 1;
        private int _leaveApplicationId = 1;
        public EmployeeData()
        {
            _employees = new Dictionary<int, Employee>
            {
                { 1, new Employee { EmployeeId = 1, UserName = "admin", Password = "admin", Role = Employee.Roles.Admin, ManagerId = 0, Leaves = new List<LeaveApplication>() } },
                { 2, new Employee { EmployeeId = 2, UserName = "manager", Password = "manager", Role = Employee.Roles.Manager, ManagerId = 1, Leaves = new List<LeaveApplication>() } },
                { 3, new Employee { EmployeeId = 3, UserName = "employee", Password = "employee", Role = Employee.Roles.Employee, ManagerId = 2, Leaves = new List<LeaveApplication>() } }
            };
        }
        public IEnumerable<Employee> GetReportees(int managerId)
        {
            return _employees.Values.Where(e => e.ManagerId == managerId);
        }
        public Employee? GetEmployeeById(int id)
        {
            _employees.TryGetValue(id, out Employee? emp);
            return emp;
        }
        public void AddEmployee(Employee emp)
        {
            emp.EmployeeId = _employeeId++;
            emp.Leaves = new List<LeaveApplication>();
            _employees.Add(emp.EmployeeId, emp);
        }
        public Employee? FindEmployee(string username, string password)
        {
            return _employees.Values.FirstOrDefault(e => e.UserName == username && e.Password == password);
        }

        public bool ApplyLeave(int employeeId, DateTime startDate, DateTime endDate)
        {
            var employee = _employees[employeeId];
            var leaveRequest = new LeaveApplication
            {
                ApplicationId = _leaveApplicationId++,
                EmployeeId = employeeId,
                StartDate = startDate,
                EndDate = endDate,
                Status = LeaveApplication.LStatus.Pending,
            };
            employee.Leaves.Add(leaveRequest);
            return true;
        }

        public bool ApproveLeave(int managerId, int employeeId, int leaveRequestId, string reason)
        {
            var employee = _employees[employeeId];
            var leaveRequest = employee.Leaves.Find(lr => lr.ApplicationId == leaveRequestId);
            if (leaveRequest == null)
            {
                return false;
            }
            if (!(employee.ManagerId == managerId))
            {
                return false;
            }
            leaveRequest.Status = LeaveApplication.LStatus.Approved;
            return true;
        }
    }
}
