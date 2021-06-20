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

            comboBox1.Text = comboBox1.Items[0].ToString();  //Set default value of comboBox1
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
            foreach (Control ctrl in panel1.Controls)  //Check if some field is null
            {
                if (String.IsNullOrEmpty(ctrl.Text))
                {
                    MessageBox.Show("ERROR: ALL FIELDS MUST BE FILLED!!");
                    return;
                }
            }

            try  // Check if the fields that must be integers are intergers
            {
                int.Parse(textBox1.Text);
            }
            catch
            {
                MessageBox.Show("ERROR: FIELD 'ID' MUST BE AN INTEGER");
                return;
            }

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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in panel1.Controls)  //Clear all TextBoxes
            {
                ctrl.Text = String.Empty;
            }
            LoadSupplierList();
        }
    }
}
