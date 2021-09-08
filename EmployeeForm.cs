using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace project
{
    public partial class EmployeeForm : Form
    {
        public int refresh { get; private set; }

        public EmployeeForm()
        {
            InitializeComponent();

            comboBox1.Text = comboBox1.Items[0].ToString();
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
                int.Parse(textBox7.Text);
                int.Parse(textBox8.Text);
            }
            catch
            {
                MessageBox.Show("ERROR: FIELDS 'SALARY' AND 'COMISSION' MUST BE INTEGERS");
                return;
            }             

            EmployeeModel employee = new EmployeeModel
            {
                Type = comboBox1.Text,
                FirstName = textBox1.Text,
                LastName = textBox2.Text,
                DateOfBirth = textBox3.Text,
                Number = textBox4.Text,
                Email = textBox5.Text,
                StartDate = textBox6.Text,
                Salary = textBox7.Text,
                Comission = textBox8.Text,
                Password = textBox9.Text
            };

            Connection.AddRecord(employee);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in panel1.Controls)  // Checks if weather of the textbox were filled
            {
                if (String.IsNullOrEmpty(ctrl.Text) == false)
                {
                    DialogResult mb = MessageBox.Show("YOU LEFT CHANGES UNSAVED. ARE YOU SURE YOU WANT TO CONTINUE?", "ATTENTION", MessageBoxButtons.YesNo);

                    if (mb == DialogResult.Yes)
                    {
                        this.Close();
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
