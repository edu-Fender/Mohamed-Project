﻿using System;
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
            EmployeeModel employee = new EmployeeModel
            {
                Type = comboBox1.Text,
                FirstName = textBox1.Text,
                LastName = textBox2.Text,
                DateOfBirth = dateTimePicker1.Text,
                Number = textBox3.Text,
                Email = textBox4.Text,
                StartDate = dateTimePicker2.Text,
                Salary = textBox7.Text,
                Comission = textBox5.Text,
                Password = textBox6.Text
            };

            Connection.AddEmployee(employee);

            foreach (Control x in this.Controls)
            {
                if (x is TextBox || x is ComboBox || x is DateTimePicker)
                {
                    x.Text = String.Empty;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadEmployeeList();
        }
    }
}
