using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string PaymentMethod { get; set; }
        public string FullSale
        {
            get
            {
                return $"{Id}.        Item Id: {ItemId}        Customer Id: {CustomerId}        Employee Id: {EmployeeId}        Sale Date: {SaleDate}        Sale Amount: {SaleAmount}        Sale Qty: {SaleQty}        Delivery Amount: {DeliveryAmount}        Payment Amount: {PaymentMethod}";
            }
        }
    }
}
