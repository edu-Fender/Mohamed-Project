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

            foreach (Control x in this.Controls)
            {
                if (x is TextBox)
                {
                    ((TextBox)x).Text = String.Empty;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadEmployeeList();
        }
    }
}
