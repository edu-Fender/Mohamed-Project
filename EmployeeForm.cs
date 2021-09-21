using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace project
{
    public partial class EmployeeForm : Form
    {
        private readonly int? selectedIndex;
        private readonly string senderButton;

        public EmployeeForm(int? selectedIndex, string senderButton)
        {
            InitializeComponent();

            this.Select();
            this.selectedIndex = selectedIndex;
            this.senderButton = senderButton;

            DateTime datetime = DateTime.UtcNow.Date;  // Set TextBox date of today
            textBox8.Text = datetime.ToString("dd/MM/yyyy");  


            if (senderButton == "view" || senderButton == "update")
            {
                button1.Text = "SAVE CHANGES";

                List<EmployeeModel> employee = Connection.LoadRecords<EmployeeModel>();

                textBox1.Text = employee[selectedIndex.Value].Email;
                textBox2.Text = employee[selectedIndex.Value].Password;
                textBox3.Text = employee[selectedIndex.Value].Password;                
                textBox4.Text = employee[selectedIndex.Value].FirstName;
                textBox5.Text = employee[selectedIndex.Value].LastName;
                comboBox1.Text = employee[selectedIndex.Value].Gender;
                textBox6.Text = employee[selectedIndex.Value].DateOfBirth;
                textBox7.Text = employee[selectedIndex.Value].Number;
                comboBox2.Text = employee[selectedIndex.Value].Type;
                textBox8.Text = employee[selectedIndex.Value].StartDate;
                textBox9.Text = employee[selectedIndex.Value].Salary;
                textBox10.Text = employee[selectedIndex.Value].Comission;

                if (senderButton == "view")
                {
                    button1.Enabled = false;
                    foreach (dynamic ctrl in panel1.Controls)
                    {
                        if (ctrl is ComboBox)
                        {
                            ctrl.Enabled = false;
                        }
                        else if (ctrl is TextBox)
                        {
                            ctrl.ReadOnly = true;
                        }
                    }

                    foreach (dynamic ctrl in panel4.Controls)
                    {
                        if (ctrl is ComboBox)
                        {
                            ctrl.Enabled = false;
                        }
                        else if (ctrl is TextBox)
                        {
                            ctrl.ReadOnly = true;
                        }
                    }
                }
            }
        }
        
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                textBox9.Text = "15000";
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                textBox9.Text = "10000";
            }
            else if (comboBox2.SelectedIndex == 2)
            {
                textBox9.Text = "8000";
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
                int.Parse(textBox9.Text);
                int.Parse(textBox10.Text);
            }
            catch
            {
                MessageBox.Show("ERROR: FIELDS 'SALARY' AND 'COMISSION' MUST BE INTEGERS");
                return;
            }       
            
            if (textBox2.Text != textBox3.Text)  // Check if password fields patch
            {
                MessageBox.Show("ERROR: PASSWORDS DON'T MATCH.");
                return;
            }

            EmployeeModel employee = new EmployeeModel
            {
                Email = textBox1.Text,
                Password = textBox2.Text,                
                FirstName = textBox4.Text,
                LastName = textBox5.Text,
                Gender = comboBox1.Text,
                DateOfBirth = textBox6.Text,
                Number = textBox7.Text,
                Type = comboBox2.Text,
                StartDate = textBox8.Text,
                Salary = textBox9.Text,
                Comission = textBox10.Text
            };

            switch (senderButton)  // will find out the type of the list automatically 
            {
                case "view":
                    break;
                case "update":
                    Connection.UpdateRecord(employee, selectedIndex.Value);
                    break;
                case "add":
                    Connection.AddRecord(employee);
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
