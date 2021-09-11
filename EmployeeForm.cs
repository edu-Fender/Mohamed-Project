using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace project
{
    public partial class EmployeeForm : Form
    {
        private int? selectedIndex;
        private string senderButton;

        public EmployeeForm(int? selectedIndex, string senderButton)
        {
            InitializeComponent();

            this.Select();
            this.selectedIndex = selectedIndex;
            this.senderButton = senderButton;

            if (senderButton == "view" || senderButton == "update")
            {
                List<EmployeeModel> employee = Connection.LoadRecords<EmployeeModel>();

                textBox1.Text = employee[selectedIndex.Value].Type;
                textBox2.Text = employee[selectedIndex.Value].FirstName;
                textBox3.Text = employee[selectedIndex.Value].LastName;
                textBox4.Text = employee[selectedIndex.Value].DateOfBirth;
                textBox5.Text = employee[selectedIndex.Value].Number;
                textBox6.Text = employee[selectedIndex.Value].Email;
                textBox7.Text = employee[selectedIndex.Value].StartDate;
                textBox8.Text = employee[selectedIndex.Value].Salary;
                textBox9.Text = employee[selectedIndex.Value].Comission;
                textBox10.Text = employee[selectedIndex.Value].Password;

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
                int.Parse(textBox8.Text);
                int.Parse(textBox9.Text);
            }
            catch
            {
                MessageBox.Show("ERROR: FIELDS 'SALARY' AND 'COMISSION' MUST BE INTEGERS");
                return;
            }             

            EmployeeModel employee = new EmployeeModel
            {
                Type = textBox1.Text,
                FirstName = textBox2.Text,
                LastName = textBox3.Text,
                DateOfBirth = textBox4.Text,
                Number = textBox5.Text,
                Email = textBox6.Text,
                StartDate = textBox7.Text,
                Salary = textBox8.Text,
                Comission = textBox9.Text,
                Password = textBox10.Text
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
