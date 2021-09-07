using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string FullCustomer
        {
            get
            {
                return $"{Id}        Customer Id: {CustomerId}        First Name: {FirstName}        Last Name: {LastName}        Email: {Email}        Number: {Number}        Address: {Address}\n\n";
            }
        }
    }
}
