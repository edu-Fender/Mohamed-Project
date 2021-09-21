using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace project
{
    public partial class SaleForm : Form
    {
        private readonly int? selectedIndex;
        private readonly string senderButton;
        private readonly List<InventoryModel> inventory;
        private List<int> itemIds;
        private double saleAmount;
        private double totalSaleAmount;
        private int saleQty;

        public SaleForm(int? selectedIndex, string senderButton)
        {
            InitializeComponent();            

            this.Select();
            this.selectedIndex = selectedIndex;
            this.senderButton = senderButton;

            itemIds = new List<int>();
            inventory = Connection.LoadRecords<InventoryModel>();   

            refreshDataGridViewComboBox(dataGridView1.Rows[0]);
            refreshComboBox(Connection.LoadRecords<CustomerModel>());
            refreshComboBox(Connection.LoadRecords<EmployeeModel>());            

            DateTime datetime = DateTime.UtcNow.Date;  // Set TextBox date of today
            textBox1.Text = datetime.ToString("dd/MM/yyyy");

            /*
            if (senderButton == "view" || senderButton == "update")
            {
                //button1.Text = "SAVE CHANGES";

                //List<SaleModel> sale = Connection.LoadRecords<SaleModel>();

                comboBox1.SelectedIndex = int.Parse(sale[selectedIndex.Value].CustomerId) - 1;  // It needs to have - 1 as ComboBox index is zero-based
                comboBox2.SelectedIndex = int.Parse(sale[selectedIndex.Value].EmployeeId) - 1;
                textBox1.Text = sale[selectedIndex.Value].SaleDate;
                textBox4.Text = sale[selectedIndex.Value].SaleAmount;
                textBox3.Text = sale[selectedIndex.Value].SaleQty;
                textBox2.Text = sale[selectedIndex.Value].DeliveryAmount;
                textBox5.Text = sale[selectedIndex.Value].TotalSaleAmount;
                comboBox4.Text = sale[selectedIndex.Value].PaymentMethod;

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
            */
        }

        private void refreshDataGridViewComboBox(DataGridViewRow row)
        {
            row.Cells[1].ReadOnly = true;

            DataGridViewComboBoxCell cell = row.Cells[0] as DataGridViewComboBoxCell;
            cell.DataSource = inventory;
            cell.ValueMember = "Id";
            cell.DisplayMember = "Type";
        }

        private void refreshComboBox<T>(List<T> list)
        {
            foreach (dynamic table in list)
            {
                switch (typeof(T).Name)
                {
                    case "CustomerModel":
                        comboBox1.Items.Add($"{table.Id}.    {table.FirstName} {table.LastName}");
                        break;
                    case "EmployeeModel":
                        comboBox2.Items.Add($"{table.Id}.    {table.FirstName} {table.LastName}");
                        break;
                }
            }
        }        

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)  // Event triggered when DataGridView row is clicked
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 0 || dataGridView1.CurrentCell.ColumnIndex == 1)  // Type ComboBox or Qty ComboBox
            {
                ComboBox cb = e.Control as ComboBox;

                if (cb != null)
                {
                    cb.SelectedIndexChanged -= new EventHandler(comboBox_SelectedIndexChanged);
                    cb.SelectedIndexChanged += new EventHandler(comboBox_SelectedIndexChanged);
                }
            }
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            DataGridViewCellCollection cells = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells;

            if (cb.SelectedIndex >= 0)  // For some reason SelectedIndex returns -1 sometimes, so this condition solve that issue
            {
                if (dataGridView1.CurrentCell.ColumnIndex == 0)
                {
                    cells[1].ReadOnly = false;
                    cells[2].Value = inventory[cb.SelectedIndex].Quantity;
                    cells[3].Value = inventory[cb.SelectedIndex].SalePrice;

                    itemIds.Add(int.Parse(inventory[cb.SelectedIndex].Id));  // Add ItemId of each item to the ItemId list                                   
                }

                else if (dataGridView1.CurrentCell.ColumnIndex == 1)
                {
                    cells[4].Value = int.Parse(cb.SelectedItem.ToString()) * int.Parse(cells[3].Value.ToString());                  
                    
                }
            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            refreshDataGridViewComboBox(dataGridView1.Rows[e.RowIndex]);
        }



        ////////////////////////////////////////////////////////////////////
        private void button1_Click(object sender, EventArgs e)
        {

            foreach (Control ctrl in panel1.Controls)  // Check if some field is null
            {
                if (ctrl.Name == "comboBox1" || ctrl.Name ==  "comboBox2" || ctrl.Name == "textBox2" || ctrl.Name ==  "comboBox3")  // Ignore these Controls, meaning they can be empty
                {
                    continue;
                }
                else if (String.IsNullOrEmpty(ctrl.Text))
                {
                    MessageBox.Show("ERROR: PLEASE ADD THE ITEMS, HIT \"CALCULATE\" BUTTON AND CHOOSE A PAYMENT METHOD!");
                    return;
                }
            }

            if (String.IsNullOrEmpty(comboBox2.Text))
            {
                DialogResult mb = MessageBox.Show("NO CUSTOMER WAS SELECTED. ARE YOU SURE YOU WANT TO CONTINUE WITHOUT SELECTING THE CUSTOMER?", "ATTENTION", MessageBoxButtons.YesNo);

                if (mb == DialogResult.No)
                {
                    return;
                }
            }

            SaleModel sale = new SaleModel
            {
                ItemId = itemIds[0].ToString(),  
                CustomerId = comboBox1.Text.Split('.')[0],  // Use Split method to split the string by dot ".", grabing the first occurrence
                EmployeeId = comboBox2.Text.Split('.')[0],
                SaleDate = textBox1.Text,
                SaleAmount = saleAmount.ToString(),
                SaleQty = saleQty.ToString(),
                DeliveryAmount = textBox2.Text,
                TotalSaleAmount = totalSaleAmount.ToString(),
                PaymentMethod = comboBox4.Text,
            };


            switch (senderButton)  // Will find out the type of the list automatically 
            {
                /*
                case "view":
                    break;
                case "update":
                    Connection.UpdateRecord(sale, selectedIndex.Value);
                    break;
                */
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

        private void button3_Click(object sender, EventArgs e)
        {
            saleAmount = 0;
            totalSaleAmount = 0;
            saleQty = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[1].Value != null)  // Error check, guarantee both Type and Qty ComboBoxes are filled
                {
                    saleQty += int.Parse(row.Cells[1].Value.ToString());  // Qty needs to be int
                    saleAmount += double.Parse(row.Cells[4].Value.ToString());  // Price needs to be double
                }
            }
 
            textBox3.Text = saleQty.ToString();
            textBox4.Text = String.Format("{0:C}", saleAmount);

            totalSaleAmount += saleAmount;

            if (comboBox3.SelectedItem != null && comboBox3.SelectedIndex != 0)  // If discount exists
            {
                double coefficient = double.Parse(comboBox3.Text) / 100;
                totalSaleAmount -= totalSaleAmount * coefficient;
            }

            if (!String.IsNullOrEmpty(textBox2.Text))  // If delivery exists 
            {
                try  // Check if the field is integer
                {
                    totalSaleAmount += double.Parse(textBox2.Text);
                }
                catch
                {
                    MessageBox.Show("ERROR: FIELD 'DELIVERY' MUST BE AN INTEGER!!");
                    return;
                }
            }

            totalSaleAmount += saleAmount * 0.015 ;  // Added +15% VAT tax

            textBox5.Text = String.Format("{0:C}", totalSaleAmount) + " added 15% (VAT)";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            InventoryForm form = new InventoryForm(null, "add");
            form.ShowDialog();
            this.Close();
            return;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CustomerForm form = new CustomerForm(null, "add");
            form.ShowDialog();
            refreshComboBox(Connection.LoadRecords<CustomerModel>());
            return;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EmployeeForm form = new EmployeeForm(null, "add");
            form.ShowDialog();
            refreshComboBox(Connection.LoadRecords<EmployeeModel>());
            return;
        }
    }
}
