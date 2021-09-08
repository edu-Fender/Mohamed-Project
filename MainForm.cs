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

        public MainForm()
        {
            InitializeComponent();

            refresh<SaleModel>(listBox1, "FullSale");
            refresh<InventoryModel>(listBox2, "FullInventory");
            refresh<CustomerModel>(listBox3, "FullCustomer");
            refresh<EmployeeModel>(listBox4, "FullEmployee");
            refresh<SupplierModel>(listBox5, "FullSupplier");
        }

        // This function will add the database info to the List Boxes
        public void refresh<T>(ListBox lb, string displayMember)  
        {
            var list = Connection.LoadRecords<T>();
            lb.DataSource = null;
            lb.DataSource = list;
            lb.DisplayMember = displayMember;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaleForm form = new SaleForm(listBox1.SelectedIndex, true);
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {

            SaleForm form = new SaleForm(null, false);
            form.ShowDialog();

            refresh<SaleModel>(listBox1, "FullSale");
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

            refresh<InventoryModel>(listBox2, "FullInventory");
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

            refresh<CustomerModel>(listBox2, "FullCustomer");
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

            refresh<EmployeeModel>(listBox2, "FullEmployee");
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

            refresh<SupplierModel>(listBox2, "FullSupplier");
        }
    }
}
