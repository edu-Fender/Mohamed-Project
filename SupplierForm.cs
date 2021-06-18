using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace project
{
    public partial class SupplierForm : Form
    {
        List<SupplierModel> supplier = new List<SupplierModel>();

        public SupplierForm()
        {
            InitializeComponent();

            comboBox1.Text = comboBox1.Items[0].ToString();
            Connection.LoadSupplier();
        }
        private void LoadSupplierList()
        {
            supplier = Connection.LoadSupplier();
            listBox1.DataSource = null;
            listBox1.DataSource = supplier;
            listBox1.DisplayMember = "FullSupplier";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SupplierModel supplier = new SupplierModel
            {
                Id = textBox1.Text,
                Name = textBox2.Text,
                Type = comboBox1.Text,
                Number = textBox3.Text,
                Email = textBox4.Text,
                Address = textBox5.Text,
            };

            Connection.AddSupplier(supplier);

            foreach (Control x in this.Controls)
            {
                if (x is TextBox || x is ComboBox || x is DateTimePicker)
                {
                    x.Text = String.Empty;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadSupplierList();
        }
    }
}
