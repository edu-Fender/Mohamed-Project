namespace project
{
    public class CustomerModel
    {
        public string Id { get; private set; }
        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public string Address { get; set; }
        public string FullString
        {
            get
            {
                return $"{Id} {CustomerId} {FirstName} {LastName} {Email} {Number} {Address}";
            }
        }
    }
}
