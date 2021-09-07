namespace project
{
    public class InventoryModel
    {
        public string Type { get; set; }
        public string Quantity { get; set; }
        public string Color { get; set; }
        public string Dimension { get; set; }
        public string StartPrice { get; set; }
        public string CurrentPrice { get; set; }
        public string Condition { get; set; }
        public string Description { get; set; }
        public string Warranty { get; set; }
        public string FullInventory
        {
            get
            {
                return $"Type: {Type}        Quantity: {Quantity}        Color: {Color}        Dimension: {Dimension}        Start Price: {StartPrice}        Current Price: {CurrentPrice}        Condition: {Condition}        Description: {Description}        Warranty: {Warranty}\n\n";
            }
        }
    }
}
