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
                return $"Type: {Type}        FirstName: {FirstName}        LastName: {LastName}        DateOfBirth: {DateOfBirth}        Number: {Number}        Email: {Email}        StartDate: {StartDate}        Salary: {Salary}        Comission: {Comission}        Password: {Password}\n\n";
            }
        }
    }
}
