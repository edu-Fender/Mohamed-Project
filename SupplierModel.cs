namespace project
{
    public class SupplierModel
    {
        public string Id { get; private set; }
        public string SupplierId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string FullString
        {
            get
            {
                return $"{Id} {SupplierId} {Name} {Type} {Number} {Email} {Address}";
            }
        }
    }
}
