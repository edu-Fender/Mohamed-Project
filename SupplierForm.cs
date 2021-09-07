using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace project
{
    public partial class SupplierForm : Form
    {
        public int refresh { get; private set; }

        public SupplierForm()
        {
            InitializeComponent();

            comboBox1.Text = comboBox1.Items[0].ToString();  //Set default value of comboBox1
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
                SupplierId = textBox1.Text,
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
            foreach (Control ctrl in panel1.Controls)  // Checks if weather of the textbox were filled
            {
                if (String.IsNullOrEmpty(ctrl.Text) == false)
                {
                    DialogResult mb = MessageBox.Show("YOU LEFT CHANGES UNSAVED. ARE YOU SURE YOU WANT TO CONTINUE?", "ATTENTION", MessageBoxButtons.YesNo);

                    if (mb == DialogResult.Yes)
                    {
                        this.Close();
                    }
                    else if (mb == DialogResult.No)
                    {
                        return;
                    }
                }
            }

            this.Close();  // In case all the Text Box were empty
        }
    }
}
