using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace project
{
    public partial class EmployeeForm : Form
    {
        List<EmployeeModel> employee = new List<EmployeeModel>();

        public EmployeeForm()
        {
            InitializeComponent();

            comboBox1.Text = comboBox1.Items[0].ToString();
            Connection.LoadEmployee();
        }

        private void LoadEmployeeList()
        {
            employee = Connection.LoadEmployee();
            listBox1.DataSource = null;
            listBox1.DataSource = employee;
            listBox1.DisplayMember = "FullEmployee";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in panel1.Controls)  //Check if TextBoxes are null
            {
                if (String.IsNullOrEmpty(ctrl.Text))
                {
                    MessageBox.Show("ALL FIELDS MUST BE FILLED!!");
                    return;
                }
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

            Connection.AddEmployee(employee);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in panel1.Controls)  //Clear all TextBoxes
            {
                ctrl.Text = String.Empty;
            }
            LoadEmployeeList();
        }
    }
}
