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
        public static void AddRecord<T>(T list)
        {
            using (IDbConnection connect = new SQLiteConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString))
            {
                switch (typeof(T).Name)  // will find out the type of the list automatically 
                {
                    case "CustomerModel":
                        connect.Execute("insert into tblCustomer (CustomerId, FirstName, LastName, Email, Number, Address) values (@CustomerId, @FirstName, @LastName, @Email, @Number, @Address)", list);
                        break;
                    case "EmployeeModel":
                        connect.Execute("insert into tblEmployees (Type, FirstName, LastName, DateOfBirth, Number, Email, StartDate, Salary, Comission, Password) values (@Type, @FirstName, @LastName, @DateOfBirth, @Number, @Email, @StartDate, @Salary, @Comission, @Password)", list);
                        break;
                    case "InventoryModel":
                        connect.Execute("insert into tblInventory (Type, Quantity, Color, Dimension, StartPrice, CurrentPrice, Condition, Description, Warranty) values (@Type, @Quantity, @Color, @Dimension, @StartPrice, @CurrentPrice, @Condition, @Description, @Warranty)", list);
                        break;
                    case "SaleModel":
                        connect.Execute("insert into tblSales (ItemId, CustomerId, EmployeeId, SaleDate, SaleAmount, SaleQty, DeliveryAmount, PaymentMethod) values (@ItemId, @CustomerId, @EmployeeId, @SaleDate, @SaleAmount, @SaleQty, @DeliveryAmount, @PaymentMethod)", list);
                        break;
                    case "SupplierModel":
                        connect.Execute("insert into tblSuppliers (SupplierId, Name, Type, Number, Email, Address) values (@SupplierId, @Name, @Type, @Number, @Email, @Address)", list);
                        break;
                }
            }
        }

        public static void UpdateRecord<T>(T list, int selectedIndex)
        {
            using (IDbConnection connect = new SQLiteConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString))
            {
                switch (typeof(T).Name)
                {
                    case "CustomerModel":
                        connect.Execute($"update tblCustomer set CustomerId = @CustomerId, FirstName = @FirstName, LastName = @LastName, Email = @Email, Number = @Number, Address= @Address  where Id = {selectedIndex + 1}", list);
                        break;
                    case "EmployeeModel":
                        connect.Execute($"update tblEmployees set Type = @update, FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, Number = @Number, Email = @Email, StartDate = @StartDate, Salary = @Salary, Comission = Comission, Password = @Password  where Id = {selectedIndex + 1}", list);
                        break;
                    case "InventoryModel":
                        connect.Execute($"update tblInventory set Type = @Type, Quantity = @Quantity, Color =  @Color, Dimension = @Dimension, StartPrice = @StartPrice, CurrentPrice = @CurrentPrice, Condition = @Condition, Description =  @Description, Warranty = @Warranty  where Id = {selectedIndex + 1}", list);
                        break;
                    case "SaleModel":
                        connect.Execute($"update tblSales set ItemId = @ItemId, CustomerId = @CustomerId, EmployeeId = @EmployeeId, SaleDate = @SaleDate, SaleAmount = @SaleAmount, SaleQty = @SaleQty, DeliveryAmount = @DeliveryAmount, PaymentMethod = @PaymentMethod where Id = {selectedIndex + 1}", list);
                        break;
                    case "SupplierModel":
                        connect.Execute($"update tblSuppliers set (SupplierId = @SupplierId, Name = @Name, Type = @Type, Number = @Number, Email = @Email, Address = @AddressId  where Id = {selectedIndex + 1}", list);
                        break;
                }
            }
        }


        public static List<T> LoadRecords<T>()
        {
            using (IDbConnection connect = new SQLiteConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString))
            {
                IEnumerable<T> output;

                switch (typeof(T).Name)
                {
                    case "CustomerModel":
                        output = connect.Query<T>("select * from tblCustomers", new DynamicParameters());
                        return output.ToList();
                    case "EmployeeModel":
                        output = connect.Query<T>("select * from tblEmployees", new DynamicParameters());
                        return output.ToList();
                    case "InventoryModel":
                        output = connect.Query<T>("select * from tblInventory", new DynamicParameters());
                        return output.ToList();
                    case "SaleModel":
                        output = connect.Query<T>("select * from tblSales", new DynamicParameters());
                        return output.ToList();
                    case "SupplierModel":
                        output = connect.Query<T>("select * from tblSuppliers", new DynamicParameters());
                        return output.ToList();
                }

                return null;
            }
        }    
    }
}

