using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                return $"{Type}, {Quantity}, {Color}, {Dimension}, {StartPrice}, {CurrentPrice}, {Condition}, {Description}, {Warranty}\n\n";
            }
        }
    }
}
