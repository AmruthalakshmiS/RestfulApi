namespace EmployeeMgmt.Dto
{
    public class LeaveApplicationDTO

    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Reason { get; set; }
    }
}
