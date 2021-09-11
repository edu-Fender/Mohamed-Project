using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace project
{
    public partial class SaleForm : Form
    {
        private int? selectedIndex;
        private string senderButton;

        public SaleForm(int? selectedIndex, string senderButton)
        {
            InitializeComponent();

            this.Select();
            this.selectedIndex = selectedIndex;
            this.senderButton = senderButton;

            if (senderButton == "view" || senderButton == "update")
            {
                List<SaleModel> sale = Connection.LoadRecords<SaleModel>();

                textBox4.Text = sale[selectedIndex.Value].SaleDate;
                textBox5.Text = sale[selectedIndex.Value].SaleAmount;
                textBox6.Text = sale[selectedIndex.Value].SaleQty;
                textBox7.Text = sale[selectedIndex.Value].DeliveryAmount;
                textBox8.Text = sale[selectedIndex.Value].PaymentMethod;

                if (senderButton == "view")
                {
                    button1.Enabled = false;
                    foreach (TextBox ctrl in panel1.Controls)
                    {
                        ctrl.ReadOnly = true;
                    }
                }
            }  
        }

        private void refreshComboBox<T>(List<T> list, ComboBox cb)
        {
            foreach (dynamic table in list)
            {
                Console.WriteLine(table.Id);
                cb.Items.Add(table.Id);
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

            
            switch (senderButton)  // will find out the type of the list automatically 
            {
                case "view":
                    break;
                case "update":
                    Connection.UpdateRecord(sale, selectedIndex.Value);
                    break;
                case "add":
                    Connection.AddRecord(sale);
                    break;
            }     
            
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (senderButton == "view")
            {
                this.Close();
                return;
            }

            foreach (Control ctrl in panel1.Controls)  // Checks if any of the textboxes was filled
            {
                if (String.IsNullOrEmpty(ctrl.Text) == false)
                {
                    DialogResult mb = MessageBox.Show("YOU LEFT CHANGES UNSAVED. ARE YOU SURE YOU WANT TO CONTINUE?", "ATTENTION", MessageBoxButtons.YesNo);

                    if (mb == DialogResult.Yes)
                    {
                        this.Close();
                        return;
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
