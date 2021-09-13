using System;
using System.Linq;
using System.Windows.Forms;

namespace project
{
    public partial class MainForm : Form
    {
        private int? selectedIndex = null;

        public MainForm()
        {
            InitializeComponent();

            refresh<SaleModel>(listView1);
            refresh<InventoryModel>(listView2);
            refresh<CustomerModel>(listView3);
            refresh<EmployeeModel>(listView4);
            refresh<SupplierModel>(listView5);
        }

        private void refresh<T>(ListView lv)
        {
            dynamic list = Connection.LoadRecords<T>();  // Since the type is only known at runtime, it need to be a dynamic variable
            lv.Items.Clear();  // Cleans the ListView so the the new records wont append to the current ones

            foreach (var table in list)
            {
                string[] subs = table.FullString.Split(' ');  // Divide the string into substrings (which contain all the Database fields)       

                if (typeof(T).Name == "EmployeeModel")  // Replace the Password chars with '*'
                {
                    subs[11] = new string('*', subs[11].Length);
                }

                var listViewItem = new ListViewItem(subs);
                lv.Items.Add(listViewItem);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var inventory = Connection.LoadRecords<InventoryModel>();
            var customer = Connection.LoadRecords<CustomerModel>();
            var employee = Connection.LoadRecords<EmployeeModel>();

            if (listView1.SelectedItems.Count > 0)
            {
                selectedIndex = listView1.Items.IndexOf(listView1.SelectedItems[0]);
                SaleForm form = new SaleForm(selectedIndex, "view", inventory, customer, employee);
                form.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var inventory = Connection.LoadRecords<InventoryModel>();
            var customer = Connection.LoadRecords<CustomerModel>();
            var employee = Connection.LoadRecords<EmployeeModel>();

            if (listView1.SelectedItems.Count > 0)
            {
                selectedIndex = listView1.Items.IndexOf(listView1.SelectedItems[0]);
                SaleForm form = new SaleForm(selectedIndex, "update", inventory, customer, employee);
                form.ShowDialog();
                refresh<SaleModel>(listView1);
            }     
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var inventory = Connection.LoadRecords<InventoryModel>();
            var customer = Connection.LoadRecords<CustomerModel>();
            var employee = Connection.LoadRecords<EmployeeModel>();

            if (!inventory.Any() || !customer.Any() || !employee.Any())  // If they are empty
            {
                MessageBox.Show("ERROR: TO CREATE A SALE, YOU FIRST NEED TO REGISTER AT LEAST ONE INVENTORY, ONE CUSTOMER, AND ONE EMPLOYEE.");
                return;
            }

            SaleForm form = new SaleForm(null, "add", inventory, customer, employee);
            form.ShowDialog();
            refresh<SaleModel>(listView1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var employee = Connection.LoadRecords<SupplierModel>();

            if (listView2.SelectedItems.Count > 0)
            {
                selectedIndex = listView2.Items.IndexOf(listView2.SelectedItems[0]);
                InventoryForm form = new InventoryForm(selectedIndex, "view", employee);
                form.ShowDialog();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var employee = Connection.LoadRecords<SupplierModel>();

            if (listView2.SelectedItems.Count > 0)
            {
                selectedIndex = listView2.Items.IndexOf(listView2.SelectedItems[0]);
                InventoryForm form = new InventoryForm(selectedIndex, "update", employee);
                form.ShowDialog();
                refresh<InventoryModel>(listView2);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var employee = Connection.LoadRecords<SupplierModel>();

            if (!employee.Any())
            {
                MessageBox.Show("ERROR: TO ADD A ITEM TO INVENTORY, YOU FIRST NEED TO REGISTER AT LEAST ONE SUPPLIER.");
                return;
            }

            InventoryForm form = new InventoryForm(null, "add", employee);
            form.ShowDialog();
            refresh<InventoryModel>(listView2);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (listView3.SelectedItems.Count > 0)
            {
                selectedIndex = listView3.Items.IndexOf(listView3.SelectedItems[0]);
                CustomerForm form = new CustomerForm(selectedIndex, "view");
                form.ShowDialog();
            } 
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (listView3.SelectedItems.Count > 0)
            {
                selectedIndex = listView3.Items.IndexOf(listView3.SelectedItems[0]);
                CustomerForm form = new CustomerForm(selectedIndex, "update");
                form.ShowDialog();
                refresh<CustomerModel>(listView3);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            CustomerForm form = new CustomerForm(null, "add");
            form.ShowDialog();
            refresh<CustomerModel>(listView3);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (listView4.SelectedItems.Count > 0)
            {
                selectedIndex = listView4.Items.IndexOf(listView4.SelectedItems[0]);
                EmployeeForm form = new EmployeeForm(selectedIndex, "view");
                form.ShowDialog();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (listView4.SelectedItems.Count > 0)
            {
                selectedIndex = listView4.Items.IndexOf(listView4.SelectedItems[0]);
                EmployeeForm form = new EmployeeForm(selectedIndex, "update");
                form.ShowDialog();
                refresh<EmployeeModel>(listView4);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            EmployeeForm form = new EmployeeForm(null, "add");
            form.ShowDialog();
            refresh<EmployeeModel>(listView4);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (listView5.SelectedItems.Count > 0)
            {
                selectedIndex = listView5.Items.IndexOf(listView5.SelectedItems[0]);
                SupplierForm form = new SupplierForm(selectedIndex, "view");
                form.ShowDialog();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (listView5.SelectedItems.Count > 0)
            {
                selectedIndex = listView5.Items.IndexOf(listView5.SelectedItems[0]);
                SupplierForm form = new SupplierForm(selectedIndex, "update");
                form.ShowDialog();
                refresh<SupplierModel>(listView5);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            SupplierForm form = new SupplierForm(null, "add");
            form.ShowDialog();
            refresh<SupplierModel>(listView5);
        }
    }
}
