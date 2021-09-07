using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class MainForm : Form
    {
        List<SaleModel> sale = Connection.LoadSale();
        List<InventoryModel> inventory = Connection.LoadInvetory();
        List<CustomerModel> customer = Connection.LoadCustomer();
        List<EmployeeModel> employee = Connection.LoadEmployee();
        List<SupplierModel> supplier = Connection.LoadSupplier();

        public MainForm()
        {
            InitializeComponent();

            refresh(listBox1, sale, "FullSale");
            refresh(listBox2, inventory, "FullInventory");
            refresh(listBox3, customer, "FullCustomer");
            refresh(listBox4, employee, "FullEmployee");
            refresh(listBox5, supplier, "FullSupplier");
        }

        // This function will add the database info to the List Boxes
        public void refresh<T>(ListBox lb, List<T> model, string member)  
        {
            lb.DataSource = null;
            lb.DataSource = model;
            lb.DisplayMember = member;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaleForm form = new SaleForm(listBox1.SelectedIndex);
            form.ShowDialog();

            sale = Connection.LoadSale();
            refresh(listBox1, sale, "FullSale");
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
        }

        private void button3_Click(object sender, EventArgs e)
        {

            SaleForm form = new SaleForm(null);
            form.ShowDialog();

            sale = Connection.LoadSale();
            refresh(listBox1, sale, "FullSale");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            InventoryForm form = new InventoryForm();
            form.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            CustomerFom form = new CustomerFom();
            form.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            
        }

        private void button11_Click(object sender, EventArgs e)
        {
            
        }

        private void button12_Click(object sender, EventArgs e)
        {
            EmployeeForm form = new EmployeeForm();
            form.ShowDialog();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            
        }

        private void button14_Click(object sender, EventArgs e)
        {
            
        }

        private void button15_Click(object sender, EventArgs e)
        {
            SupplierForm form = new SupplierForm();
            form.ShowDialog();
        }
    }
}
