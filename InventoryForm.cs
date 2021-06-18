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
            LoadInventoryList();
        }
    }
}
