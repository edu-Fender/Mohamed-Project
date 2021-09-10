using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Dynamic;

namespace project
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();

            refresh<SaleModel>(listView1);
            refresh<InventoryModel>(listView2);
            refresh<CustomerModel>(listView3);
            refresh<EmployeeModel>(listView4);
            refresh<SupplierModel>(listView5);
        }

        public void refresh<T>(ListView lv)
        {
            dynamic table = Connection.LoadRecords<T>();  // Since the type is only known at runtime, it need to be a dynamic variable

            foreach (var row in table)
            {
                string[] subs = row.FullString.Split(' ');
                var listViewItem = new ListViewItem(subs);
                lv.Items.Add(listViewItem);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SaleForm form = new SaleForm(listBox1.SelectedIndex, true);
            //form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaleForm form = new SaleForm(null, false);
            form.ShowDialog();

            //refresh<SaleModel>(listBox1, "FullSale");
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
