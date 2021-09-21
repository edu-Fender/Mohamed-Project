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
            dynamic tables = Connection.LoadRecords<T>();  // Since the type is only known at runtime, it need to be a dynamic variable
            lv.Items.Clear();  // Cleans the ListView so the the new records wont append to the current ones

            foreach (var table in tables)
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


        //// ------------------------------------ SALE ------------------------------------
        private void button1_Click(object sender, EventArgs e)
        {           
            /*if (listView1.SelectedItems.Count > 0)
            {
                selectedIndex = listView1.Items.IndexOf(listView1.SelectedItems[0]);
                SaleForm form = new SaleForm(selectedIndex, "view");
                form.ShowDialog();
            }
            */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
            if (listView1.SelectedItems.Count > 0)
            {
                selectedIndex = listView1.Items.IndexOf(listView1.SelectedItems[0]);
                SaleForm form = new SaleForm(selectedIndex, "update");
                form.ShowDialog();
                refresh<SaleModel>(listView1);
            }     
            */
        }

        private void button3_Click(object sender, EventArgs e)
        {

            SaleForm form = new SaleForm(null, "add");
            form.ShowDialog();
            refresh<SaleModel>(listView1);
        }


        //// ------------------------------------ INVENTORY ------------------------------------
        private void button4_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                selectedIndex = listView2.Items.IndexOf(listView2.SelectedItems[0]);
                InventoryForm form = new InventoryForm(selectedIndex, "view");
                form.ShowDialog();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                selectedIndex = listView2.Items.IndexOf(listView2.SelectedItems[0]);
                InventoryForm form = new InventoryForm(selectedIndex, "update");
                form.ShowDialog();
                refresh<InventoryModel>(listView2);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            InventoryForm form = new InventoryForm(null, "add");
            form.ShowDialog();
            refresh<InventoryModel>(listView2);
        }


        //// ------------------------------------ CUSTOMER ------------------------------------
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


        //// ------------------------------------ EMPLOYEE ------------------------------------
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


        //// ------------------------------------ SUPPLIER ------------------------------------
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


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            LoginForm form = new LoginForm();
            form.ShowDialog();
            this.Close();
            return;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Welcome to Mousofa Furniture System. Here goes a brief tutorial on how to use the Program:" +
                "\n\n- First, browse through the program by clicking on the tabs located at the top of the window." +
                "\n\n- Click \"ADD NEW RECORD\" if you want to add a record. A form containing the text boxes that need to be filled will appear." +
                "\n\n- To view the added record more clearly (and to be able to copy the values), select the record then click \"VIEW RECORD\" button)." +
                "\n\n- To update the record, select the record and click on \"UPDATE RECORD\", change the fields you want to be updated." +
                "\n\n\nIMPORTANT: Although you can create a new Sale without registering the Customer, it's highly recommended that you do register the Customer. " +
                "Similarly, you can add new products to the inventory without resgistering the Supplier, but it's not recommended that you do that", "TUTORIAL");
        }
    }
}
