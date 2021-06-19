namespace project
{
    public class SupplierModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string FullSupplier
        {
            get
            {
                return $"{Id}, {Name}, {Type}, {Number}, {Email}, {Address}\n\n";
            }
        }
    }
}
