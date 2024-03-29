﻿namespace EmployeeMgmt.Model
{
    public class Employee
    {
        public enum Roles
        {
            Admin,
            Manager,
            Employee
        }
        public int EmployeeId { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        //public string? Role { get; set; }

        public string? Email { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public Roles Role {  get; set; }

        public int ManagerId { get; set; }
        //Navigation Property
        public List<LeaveApplication>? Leaves { get; set; }

    }
}
