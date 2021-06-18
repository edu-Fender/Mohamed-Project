using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    public class EmployeeModel
    {
        public string Type { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string StartDate { get; set; }
        public string Salary { get; set; }
        public string Comission { get; set; }
        public string Password { get; set; }
        public string FullEmployee 
        {
            get
            {
                return $"{Type}, {FirstName}, {LastName}, {DateOfBirth}, {Number}, {Email}, {StartDate}, {Salary}, {Comission}, {Password}\n\n";
            }
        }
    }
}
