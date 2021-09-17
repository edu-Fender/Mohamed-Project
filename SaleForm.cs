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
        private double saleAmount;
        private double totalSaleAmount;
        private int totalQty;

        public SaleForm(int? selectedIndex, string senderButton)
        {
            InitializeComponent();            

            this.Select();
            this.selectedIndex = selectedIndex;
            this.senderButton = senderButton;

            inventory = Connection.LoadRecords<InventoryModel>();

            refreshDataGridViewComboBox(dataGridView1.Rows[0]);
            refreshComboBox(Connection.LoadRecords<CustomerModel>());
            refreshComboBox(Connection.LoadRecords<EmployeeModel>());            

            DateTime datetime = DateTime.UtcNow.Date;  // Set TextBox date of today
            textBox1.Text = datetime.ToString("dd/MM/yyyy");

            if (senderButton == "view" || senderButton == "update")
            {
                button1.Text = "SAVE CHANGES";

                List<SaleModel> sale = Connection.LoadRecords<SaleModel>();

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

        private void button1_Click(object sender, EventArgs e)
        {

            foreach (Control ctrl in panel1.Controls)  // Check if some field is null
            {
                if (ctrl.Name == "comboBox1" || ctrl.Name ==  "comboBox2" || ctrl.Name == "textBox2" || ctrl.Name ==  "comboBox3")  // Ignores the ComboBox, it means it can be empty
                {
                    continue;
                }
                if (String.IsNullOrEmpty(ctrl.Text))
                {
                    MessageBox.Show("ERROR: PLEASE ADD THE ITEMS AND CHOOSE A PAYMENT METHOD!!");
                    return;
                }
            }

            if (String.IsNullOrEmpty(comboBox2.Text))
            {
                DialogResult mb = MessageBox.Show("NO CUSTOMER WAS SELECTED. ARE YOU SURE YOU WANT TO CONTINUE WITHOUT A COSTUMER?", "ATTENTION", MessageBoxButtons.YesNo);

                if (mb == DialogResult.No)
                {
                    return;
                }
            }

            SaleModel sale = new SaleModel
            {
                //ItemId = comboBox1.GetItemText(comboBox1.SelectedItem).Split('.')[0],  // Use Split method to split the string by dot ".", grabing the first occurrence
                CustomerId = comboBox1.GetItemText(comboBox1.SelectedItem).Split('.')[0],
                EmployeeId = comboBox2.GetItemText(comboBox2.SelectedItem).Split('.')[0],
                SaleDate = textBox1.Text,
                SaleAmount = saleAmount.ToString(),
                SaleQty = totalQty.ToString(),
                DeliveryAmount = textBox2.Text,
                TotalSaleAmount = totalSaleAmount.ToString(),
                PaymentMethod = comboBox4.GetItemText(comboBox1.SelectedItem)
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

        private void button3_Click(object sender, EventArgs e)
        {           
            
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    totalQty += int.Parse(row.Cells[1].Value.ToString());
                    saleAmount += int.Parse(row.Cells[4].Value.ToString());
                }
            }
 
            textBox3.Text = totalQty.ToString();
            textBox4.Text = String.Format("{0:C}", Convert.ToInt32(saleAmount.ToString()));

            totalSaleAmount += saleAmount;
            if (comboBox3.SelectedItem != null)  // If discount exists
            {                
                var coefficient = int.Parse(comboBox3.Text) / 100;
                totalSaleAmount -= totalSaleAmount * coefficient;
            }

            if (!String.IsNullOrEmpty(textBox2.Text))  // If delivery exists 
            {
                try  // Check if the fields that must be integers are intergers
                {
                    totalSaleAmount += int.Parse(textBox2.Text);
                }
                catch
                {
                    MessageBox.Show("ERROR: FIELD 'DELIVERY' MUST BE AN INTEGER!!");
                    return;
                }
            }

            totalSaleAmount = saleAmount + saleAmount * 0.015;  // Added +15% VAT tax

            textBox5.Text = String.Format("{0:C}", Convert.ToInt32(totalSaleAmount)) + " added 15% (VAT) and delivery";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CustomerForm form = new CustomerForm(null, "add");
            form.ShowDialog();
            refreshComboBox(Connection.LoadRecords<CustomerModel>());
            return;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EmployeeForm form = new EmployeeForm(null, "add");
            form.ShowDialog();
            refreshComboBox(Connection.LoadRecords<EmployeeModel>());
            return;
        }
    }
}
