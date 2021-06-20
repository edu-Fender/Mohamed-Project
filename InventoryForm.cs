using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace project
{
    public partial class InventoryForm : Form
    {
        List<InventoryModel> inventory = new List<InventoryModel>();

        public InventoryForm()
        {
            InitializeComponent();

            comboBox1.Text = comboBox1.Items[0].ToString();
            Connection.LoadInvetory();
        }

        private void LoadInventoryList()
        {
            inventory = Connection.LoadInvetory();
            listBox1.DataSource = null;
            listBox1.DataSource = inventory;
            listBox1.DisplayMember = "FullInventory";
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
            foreach (Control ctrl in panel1.Controls)  //Clear all TextBoxes
            {
                ctrl.Text = String.Empty;
            }
            LoadInventoryList();
        }
    }
}
