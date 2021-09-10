namespace project
{
    public class SaleModel 
    {
        public string Id { get; set; }
        public string ItemId { get; set; }
        public string CustomerId { get; set; }
        public string EmployeeId { get; set; }
        public string SaleDate { get; set; }
        public string SaleAmount { get; set; }
        public string SaleQty { get; set; }
        public string DeliveryAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string FullString
        {
            get
            {
                return $"{Id} {ItemId} {CustomerId} {EmployeeId} {SaleDate} {SaleAmount} {SaleQty} {DeliveryAmount} {PaymentMethod}";
            }
        }
    }
}
