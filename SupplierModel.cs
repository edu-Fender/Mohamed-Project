namespace project
{
    public class SupplierModel
    {
        public string SupplierId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string FullSupplier
        {
            get
            {
                return $"Supplier Id: {SupplierId}        Name: {Name}        Type: {Type}        Number: {Number}        Email: {Email}        Address: {Address}\n\n";
            }
        }
    }
}
