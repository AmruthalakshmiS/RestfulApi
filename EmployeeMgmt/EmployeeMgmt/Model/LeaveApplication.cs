namespace EmployeeMgmt.Model
{
    public class LeaveApplication
    {
        public int ApplicationId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Reason { get; set; }
        public string? Status { get; set; }

        public Employee? Employee { get; set; }
    }
}
