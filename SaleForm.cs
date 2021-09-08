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
    public partial class SaleForm : Form
    {
        private int? _selectedIndex;

        public SaleForm(int? selectedIndex, bool flag)
        {
            InitializeComponent();

            if (flag == true)
            {
                button1.Enabled = false;

                foreach (TextBox cntrl in panel1.Controls)
                {
                    cntrl.ReadOnly = true;
                }
            }

            if (selectedIndex != null)  // If selectedIndex not null, if any item was selected on the TextBox of the MainForm when this form was called
            {
                _selectedIndex = selectedIndex;
                List<SaleModel> sale = Connection.LoadRecords<SaleModel>();
                                
                textBox1.Text = sale[selectedIndex.Value].ItemId;
                textBox2.Text = sale[selectedIndex.Value].CustomerId;
                textBox3.Text = sale[selectedIndex.Value].EmployeeId;
                textBox4.Text = sale[selectedIndex.Value].SaleDate;
                textBox5.Text = sale[selectedIndex.Value].SaleAmount;
                textBox6.Text = sale[selectedIndex.Value].SaleQty;
                textBox7.Text = sale[selectedIndex.Value].DeliveryAmount;
                textBox8.Text = sale[selectedIndex.Value].PaymentMethod;

                button1.Enabled = false;
            }  
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
                int.Parse(textBox2.Text);
                int.Parse(textBox3.Text);
                int.Parse(textBox5.Text);
                int.Parse(textBox6.Text);
                int.Parse(textBox7.Text);

            }
            catch
            {
                MessageBox.Show("ERROR: ALL THE FIELDS (EXCEPT 'SALE DATE' AND 'PAYMENT METHOD') SHOUD BE INTEGERS!");
                return;
            }

            SaleModel sale = new SaleModel
            {
                ItemId = textBox1.Text,
                CustomerId = textBox2.Text,
                EmployeeId = textBox3.Text,
                SaleDate = textBox4.Text,
                SaleAmount = textBox5.Text,
                SaleQty = textBox6.Text,
                DeliveryAmount = textBox7.Text,
                PaymentMethod = textBox8.Text,
            };

            if (_selectedIndex == null)
            {
                Connection.AddRecord(sale);
            }          
            else if (_selectedIndex != null)
            {
                Connection.UpdateRecord(sale, _selectedIndex.Value);
            }
            this.Close();
            
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
