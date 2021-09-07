using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace project
{
    public partial class InventoryForm : Form
    {
        public int refresh { get; private set; }

        public InventoryForm()
        {
            InitializeComponent();

            comboBox1.Text = comboBox1.Items[0].ToString();
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
                int.Parse(textBox4.Text);
                int.Parse(textBox5.Text);
            }
            catch
            {
                MessageBox.Show("ERROR: FIELDS 'QUANTITY', 'START PRICE' AND 'CURRENT PRICE' MUST BE INTEGERS");
                return;
            }

            InventoryModel inventory = new InventoryModel
            {
                Type = comboBox1.Text,
                Quantity = textBox1.Text,
                Color = textBox2.Text,
                Dimension = textBox3.Text,
                StartPrice = textBox4.Text,
                CurrentPrice = textBox5.Text,
                Condition = textBox6.Text,
                Description = textBox7.Text,
                Warranty = textBox8.Text
            };

            Connection.AddInventory(inventory);
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
