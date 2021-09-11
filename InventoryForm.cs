using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace project
{
    public partial class InventoryForm : Form
    {
        private int? selectedIndex;
        private string senderButton;

        public InventoryForm(int? selectedIndex, string senderButton)
        {
            InitializeComponent();

            this.Select();
            this.selectedIndex = selectedIndex;
            this.senderButton = senderButton;

            if (senderButton == "view" || senderButton == "update")
            {
                List<InventoryModel> inventory = Connection.LoadRecords<InventoryModel>();

                textBox1.Text = inventory[selectedIndex.Value].Type;
                textBox2.Text = inventory[selectedIndex.Value].Quantity;
                textBox3.Text = inventory[selectedIndex.Value].Color;
                textBox4.Text = inventory[selectedIndex.Value].Dimension;
                textBox5.Text = inventory[selectedIndex.Value].StartPrice;
                textBox6.Text = inventory[selectedIndex.Value].CurrentPrice;
                textBox7.Text = inventory[selectedIndex.Value].Condition;
                textBox8.Text = inventory[selectedIndex.Value].Description;
                textBox9.Text = inventory[selectedIndex.Value].Warranty;

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
                int.Parse(textBox2.Text);
                int.Parse(textBox5.Text);
                int.Parse(textBox6.Text);
            }
            catch
            {
                MessageBox.Show("ERROR: FIELDS 'QUANTITY', 'START PRICE' AND 'CURRENT PRICE' MUST BE INTEGERS");
                return;
            }

            InventoryModel inventory = new InventoryModel
            {
                Type = textBox1.Text,
                Quantity = textBox2.Text,
                Color = textBox3.Text,
                Dimension = textBox4.Text,
                StartPrice = textBox5.Text,
                CurrentPrice = textBox6.Text,
                Condition = textBox7.Text,
                Description = textBox8.Text,
                Warranty = textBox9.Text
            };

            switch (senderButton)  // will find out the type of the list automatically 
            {
                case "view":
                    break;
                case "update":
                    Connection.UpdateRecord(inventory, selectedIndex.Value);
                    break;
                case "add":
                    Connection.AddRecord(inventory);
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

            foreach (Control ctrl in panel1.Controls)  // Checks if weather of the textbox were filled
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
