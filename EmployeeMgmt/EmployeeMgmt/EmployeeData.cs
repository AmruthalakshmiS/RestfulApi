using EmployeeMgmt.Model;

namespace EmployeeMgmt
{
    public class EmployeeData
    {
        private static EmployeeData? _instance;
        private readonly List<Employee> _employees;
        private readonly List<LeaveApplication> _leaves;

        private EmployeeData()
        {
            _employees = new List<Employee>();
            _leaves = new List<LeaveApplication>();
        }

        public static EmployeeData Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EmployeeData();
                }

                return _instance;
            }
        }

        public List<Employee> Employees
        {
            get { return _employees; }
        }

        public List<LeaveApplication> Leaves
        {
            get { return _leaves; }
        }
    }
}
