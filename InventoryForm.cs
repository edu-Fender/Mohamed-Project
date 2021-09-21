using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace project
{
    public partial class InventoryForm : Form
    {
        private readonly int? selectedIndex;
        private readonly string senderButton;

        public InventoryForm(int? selectedIndex, string senderButton)
        {
            InitializeComponent();

            refreshComboBox(Connection.LoadRecords<SupplierModel>());            

            this.Select();
            this.selectedIndex = selectedIndex;
            this.senderButton = senderButton;

            if (senderButton == "view" || senderButton == "update")
            {
                button1.Text = "SAVE CHANGES";

                List<InventoryModel> inventory = Connection.LoadRecords<InventoryModel>();

                comboBox1.SelectedIndex = int.Parse(inventory[selectedIndex.Value].SupplierId) - 1;  // It needs to have - 1 as ComboBox index is zero-based
                textBox1.Text = inventory[selectedIndex.Value].Type;
                textBox2.Text = inventory[selectedIndex.Value].Quantity;
                textBox3.Text = inventory[selectedIndex.Value].Colour;
                textBox4.Text = inventory[selectedIndex.Value].Dimensions;
                textBox5.Text = inventory[selectedIndex.Value].SalePrice;
                textBox6.Text = inventory[selectedIndex.Value].CostPrice;
                textBox7.Text = inventory[selectedIndex.Value].Condition;
                textBox8.Text = inventory[selectedIndex.Value].Description;
                textBox9.Text = inventory[selectedIndex.Value].Warranty;

                if (senderButton == "view")
                {
                    button1.Enabled = false;
                    foreach (dynamic ctrl in panel1.Controls)
                    {
                        if (ctrl is ComboBox)
                        {
                            ctrl.DropDownStyle = ComboBoxStyle.Simple;
                            ctrl.BackColor = System.Drawing.SystemColors.Control;
                        }
                        else if (ctrl is TextBox)
                        {
                            ctrl.ReadOnly = true;
                        }
                    }
                }
            }
        }

        private void refreshComboBox<T>(List<T> list)
        {
            foreach (dynamic table in list)
            {
                switch (typeof(T).Name)
                {
                    case "SupplierModel":
                        comboBox1.Items.Add($"{table.Id}.    {table.Name} {table.Type}");
                        break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in panel1.Controls)  //Check if some field is null
            {
                if (ctrl.Name == "comboBox1")  // Ignores the ComboBox, it means it can be empty
                {
                    continue;
                }
                else if (String.IsNullOrEmpty(ctrl.Text))
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
                SupplierId = comboBox1.Text.Split('.')[0], // Uses Split method to split the string by dot ".", grabing the first occurrence
                Type = textBox1.Text,
                Quantity = textBox2.Text,
                Colour = textBox3.Text,
                Dimensions = textBox4.Text,
                SalePrice = textBox5.Text,
                CostPrice = textBox6.Text,
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SupplierForm form = new SupplierForm(null, "add");
            form.ShowDialog();
            refreshComboBox(Connection.LoadRecords<SupplierModel>());
            return;
        }
    }
}
