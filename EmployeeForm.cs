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

            if (senderButton == "view" || senderButton == "update")
            {
                List<EmployeeModel> employee = Connection.LoadRecords<EmployeeModel>();

                textBox1.Text = employee[selectedIndex.Value].Email;
                textBox2.Text = employee[selectedIndex.Value].Password;
                textBox3.Text = employee[selectedIndex.Value].Password;
                textBox4.Text = employee[selectedIndex.Value].Type;
                textBox5.Text = employee[selectedIndex.Value].FirstName;
                textBox6.Text = employee[selectedIndex.Value].LastName;
                textBox7.Text = employee[selectedIndex.Value].Gender;
                textBox8.Text = employee[selectedIndex.Value].DateOfBirth;
                textBox9.Text = employee[selectedIndex.Value].Number;                
                textBox10.Text = employee[selectedIndex.Value].StartDate;
                textBox11.Text = employee[selectedIndex.Value].Salary;
                textBox12.Text = employee[selectedIndex.Value].Comission;
                

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
                int.Parse(textBox11.Text);
                int.Parse(textBox12.Text);
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
                Type = textBox4.Text,
                FirstName = textBox5.Text,
                LastName = textBox6.Text,
                Gender = textBox7.Text,
                DateOfBirth = textBox8.Text,
                Number = textBox9.Text,                
                StartDate = textBox10.Text,
                Salary = textBox11.Text,
                Comission = textBox12.Text
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
