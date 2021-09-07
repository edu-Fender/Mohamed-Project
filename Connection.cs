// ==================================================
// File to deal with connections to SQLite Database
// ==================================================

using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace project
{
    public class Connection
    {
        public static void AddCustomer(CustomerModel Customer)
        {
            using (IDbConnection connect = new SQLiteConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString))
            {
                connect.Execute("insert into tblEmployees (CustomerId, FirstName, LastName, Email, Number, Address) values (@CustomerId, @FirstName, @LastName, @Email, @Number, @Address)", Customer);
            }
        }
        public static List<CustomerModel> LoadCustomer()
        {
            using (IDbConnection connect = new SQLiteConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString))
            {
                var output = connect.Query<CustomerModel>("select * from tblCustomers", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void AddEmployee(EmployeeModel Employee)
        {
            using (IDbConnection connect = new SQLiteConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString))
            {
                connect.Execute("insert into tblEmployees (Type, FirstName, LastName, DateOfBirth, Number, Email, StartDate, Salary, Comission, Password) values (@Type, @FirstName, @LastName, @DateOfBirth, @Number, @Email, @StartDate, @Salary, @Comission, @Password)", Employee);

            }
        }
        public static List<EmployeeModel> LoadEmployee()
        {
            using (IDbConnection connect = new SQLiteConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString))
            {
                var output = connect.Query<EmployeeModel>("select * from tblEmployees", new DynamicParameters());
                return output.ToList();
            }
        }
        public static void AddInventory(InventoryModel Inventory)
        {
            using (IDbConnection connect = new SQLiteConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString))
            {
                connect.Execute("insert into tblInventory (Type, Quantity, Color, Dimension, StartPrice, CurrentPrice, Condition, Description, Warranty) values (@Type, @Quantity, @Color, @Dimension, @StartPrice, @CurrentPrice, @Condition, @Description, @Warranty)", Inventory);
            }
        }
        public static List<InventoryModel> LoadInvetory()
        {
            using (IDbConnection connect = new SQLiteConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString))
            {
                var output = connect.Query<InventoryModel>("select * from tblInventory", new DynamicParameters());
                return output.ToList();
            }
        }
        public static void AddSale(SaleModel Sale)
        {
            using (IDbConnection connect = new SQLiteConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString))
            {
                connect.Execute("insert into tblSales (ItemId, CustomerId, EmployeeId, SaleDate, SaleAmount, SaleQty, DeliveryAmount, PaymentMethod) values (@ItemId, @CustomerId, @EmployeeId, @SaleDate, @SaleAmount, @SaleQty, @DeliveryAmount, @PaymentMethod)", Sale);
            }
        }
        public static List<SaleModel> LoadSale()
        {
            using (IDbConnection connect = new SQLiteConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString))
            {
                var output = connect.Query<SaleModel>("select * from tblSales", new DynamicParameters());
                return output.ToList();
            }
        }
        public static void UpdateSale(SaleModel Sale, int id)
        {
            using (IDbConnection connect = new SQLiteConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString))
            {
                connect.Execute($"update tblSales set ItemId = @ItemId, CustomerId = @CustomerId, EmployeeId = @EmployeeId, SaleDate = @SaleDate, SaleAmount = @SaleAmount, SaleQty = @SaleQty, DeliveryAmount = @DeliveryAmount, PaymentMethod = @PaymentMethod where Id = {id+1}", Sale);
            }
        }
        public static void AddSupplier(SupplierModel Supplier)
        {
            using (IDbConnection connect = new SQLiteConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString))
            {
                connect.Execute("insert into tblSuppliers (SupplierId, Name, Type, Number, Email, Address) values (@SupplierId, @Name, @Type, @Number, @Email, @Address)", Supplier);
            }
        }
        public static List<SupplierModel> LoadSupplier()
        {
            using (IDbConnection connect = new SQLiteConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString))
            {
                var output = connect.Query<SupplierModel>("select * from tblSuppliers", new DynamicParameters()).ToList();
                return output.ToList();
            }
        }
    }
}

