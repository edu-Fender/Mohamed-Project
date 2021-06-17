using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Dapper;

namespace project
{
    public class Connection
    {   
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
                var output1 = connect.Query<EmployeeModel>("select * from tblEmployees", new DynamicParameters()).ToList();
                return output1.ToList();
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
                var output2 = connect.Query<InventoryModel>("select * from tblInventory", new DynamicParameters()).ToList();
                return output2.ToList();
            }
        }
    }
}

