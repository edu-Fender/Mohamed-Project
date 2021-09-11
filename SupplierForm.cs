using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace project
{
    public partial class SupplierForm : Form
    {
        private int? selectedIndex;
        private string senderButton;

        public SupplierForm(int? selectedIndex, string senderButton)
        {
            InitializeComponent();

            this.Select();
            this.selectedIndex = selectedIndex;
            this.senderButton = senderButton;

            if (senderButton == "view" || senderButton == "update")
            {
                List<SupplierModel> supplier = Connection.LoadRecords<SupplierModel>();

                textBox1.Text = supplier[selectedIndex.Value].SupplierId;
                textBox2.Text = supplier[selectedIndex.Value].Name;
                textBox3.Text = supplier[selectedIndex.Value].Type;
                textBox4.Text = supplier[selectedIndex.Value].Number;
                textBox5.Text = supplier[selectedIndex.Value].Email;
                textBox6.Text = supplier[selectedIndex.Value].Address;


                if (senderButton == "view")
                {
                    button1.Enabled = false;
                    foreach (TextBox ctrl in panel1.Controls)  // As the variable is dynamic, I can set the ReadOnly attribute without showing the compiler any specific type
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
                int.Parse(textBox1.Text);
            }
            catch
            {
                MessageBox.Show("ERROR: FIELD 'ID' MUST BE AN INTEGER");
                return;
            }

            SupplierModel supplier = new SupplierModel
            {
                SupplierId = textBox1.Text,
                Name = textBox2.Text,
                Number = textBox4.Text,
                Email = textBox5.Text,
                Address = textBox6.Text,
            };

            switch (senderButton)  // will find out the type of the list automatically 
            {
                case "view":
                    break;
                case "update":
                    Connection.UpdateRecord(supplier, selectedIndex.Value);
                    break;
                case "add":
                    Connection.AddRecord(supplier);
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
