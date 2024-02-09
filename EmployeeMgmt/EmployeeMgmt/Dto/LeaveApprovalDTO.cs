namespace EmployeeMgmt.Dto
{
    public class LeaveApprovalDTO
    {
        public int ApplicationId { get; set; }
        public int EmployeeId { get; set; }

        public string? Reason { get; set; }
    }
}
