using System.Collections.Generic;

namespace project
{
    public class SaleModel
    {
        public string Id { get; private set; }
        public string ItemId { get; set; }
        public string CustomerId { get; set; }
        public string EmployeeId { get; set; }
        public string SaleDate { get; set; }
        public string SaleAmount { get; set; }
        public string SaleQty { get; set; }
        public string DeliveryAmount { get; set; }
        public string TotalSaleAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string FullString
        {
            get
            {
                return $"{Id} {ItemId} {CustomerId} {EmployeeId} {SaleDate} {SaleAmount} {SaleQty} {DeliveryAmount} {TotalSaleAmount} {PaymentMethod}";
            }
        }
    }

    public class InventoryModel
    {
        public string Id { get; private set; }
        public string SupplierId { get; set; }
        public string Type { get; set; }
        public string Quantity { get; set; }
        public string Colour { get; set; }
        public string Dimensions { get; set; }
        public string SalePrice { get; set; }
        public string CostPrice { get; set; }
        public string Condition { get; set; }
        public string Description { get; set; }
        public string Warranty { get; set; }
        public string FullString
        {
            get
            {
                return $"{Id} {SupplierId} {Type} {Quantity} {Colour} {Dimensions} {SalePrice} {CostPrice} {Condition} {Description} {Warranty}";
            }
        }
    }

    public class CustomerModel
    {
        public string Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public string Address { get; set; }
        public string FullString
        {
            get
            {
                return $"{Id} {FirstName} {LastName} {Email} {Number} {Address}";
            }
        }
    }

    public class EmployeeModel
    {
        public string Id { get; private set; }
        public string Type { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string StartDate { get; set; }
        public string Salary { get; set; }
        public string Comission { get; set; }
        public string Password { get; set; }
        public string FullString
        {
            get
            {
                return $"{Id} {Type} {FirstName} {LastName} {Gender} {DateOfBirth} {Number} {Email} {StartDate} {Salary} {Comission} {Password}";
            }
        }
    }

    public class SupplierModel
    {
        public string Id { get; private set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string FullString
        {
            get
            {
                return $"{Id} {Name} {Type} {Number} {Email} {Address}";
            }
        }
    }
}
