using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace project
{
    public partial class LoginForm : Form
    {
        List<EmployeeModel> employee;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            employee = Connection.LoadRecords<EmployeeModel>();

            foreach (var table in employee)
            {
                if (textBox1.Text == table.Email && textBox2.Text == table.Password)
                {
                    this.Hide();
                    MainForm form = new MainForm();
                    form.ShowDialog();
                    this.Close();
                    return;
                }
            }

            MessageBox.Show("ERROR: USER NOT FOUND.");
            return;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EmployeeForm form = new EmployeeForm(null, "add");
            form.ShowDialog();
        }
    }
}
