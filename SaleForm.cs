using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace project
{
    public partial class SaleForm : Form
    {
        private readonly int? selectedIndex;
        private readonly string senderButton;

        public SaleForm(int? selectedIndex, string senderButton, List<InventoryModel> inventory, List<CustomerModel> customer, List<EmployeeModel> employee)
        {
            InitializeComponent();

            refreshComboBox(inventory);
            refreshComboBox(customer);
            refreshComboBox(employee);

            this.Select();
            this.selectedIndex = selectedIndex;
            this.senderButton = senderButton;

            if (senderButton == "view" || senderButton == "update")
            {
                List<SaleModel> sale = Connection.LoadRecords<SaleModel>();

                comboBox1.SelectedIndex = int.Parse(sale[selectedIndex.Value].ItemId) - 1;  // It needs to have - 1 as ComboBox index is zero-based
                comboBox2.SelectedIndex = int.Parse(sale[selectedIndex.Value].CustomerId) - 1;
                comboBox3.SelectedIndex = int.Parse(sale[selectedIndex.Value].EmployeeId) - 1;
                textBox4.Text = sale[selectedIndex.Value].SaleDate;
                textBox5.Text = sale[selectedIndex.Value].SaleAmount;
                textBox6.Text = sale[selectedIndex.Value].SaleQty;
                textBox7.Text = sale[selectedIndex.Value].DeliveryAmount;
                textBox8.Text = sale[selectedIndex.Value].TotalSaleAmount;
                textBox9.Text = sale[selectedIndex.Value].PaymentMethod;

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
            string str;

            foreach (dynamic table in list)
            {
                switch (typeof(T).Name)
                {
                    case "InventoryModel":
                        str = $"{table.Id}.    {table.Type} {table.Colour}";
                        comboBox1.Items.Add(str);
                        break;
                    case "CustomerModel":
                        str = $"{table.Id}.    {table.FirstName} {table.LastName}";
                        comboBox2.Items.Add(str);
                        break;
                    case "EmployeeModel":
                        str = $"{table.Id}.    {table.FirstName} {table.LastName}";
                        comboBox3.Items.Add(str);
                        break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in panel1.Controls)  // Check if some field is null
            {
                if (String.IsNullOrEmpty(ctrl.Text))
                {
                    MessageBox.Show("ERROR: ALL FIELDS MUST BE FILLED!!");
                    return;
                }
            }

            try  // Check if the fields that must be integers are intergers
            {
                int.Parse(textBox5.Text);
                int.Parse(textBox6.Text);
                int.Parse(textBox7.Text);
                int.Parse(textBox8.Text);

            }
            catch
            {
                MessageBox.Show("ERROR: ALL THE FIELDS (EXCEPT 'SALE DATE' AND 'PAYMENT METHOD') SHOUD BE INTEGERS!");
                return;
            }

            SaleModel sale = new SaleModel
            {
                ItemId = comboBox1.GetItemText(comboBox1.SelectedItem).Split('.')[0],  // Use Split method to split the string by dot ".", grabing the first occurrence
                CustomerId = comboBox3.GetItemText(comboBox2.SelectedItem).Split('.')[0],
                EmployeeId = comboBox3.GetItemText(comboBox3.SelectedItem).Split('.')[0],
                SaleDate = textBox4.Text,
                SaleAmount = textBox5.Text,
                SaleQty = textBox6.Text,
                DeliveryAmount = textBox7.Text,
                TotalSaleAmount = textBox8.Text,
                PaymentMethod = textBox9.Text,
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
