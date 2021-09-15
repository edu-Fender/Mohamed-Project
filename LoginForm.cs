using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace project
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<EmployeeModel> employees = Connection.LoadRecords<EmployeeModel>();

            foreach (var employee in employees)
            {
                if (textBox1.Text == employee.Email && textBox2.Text == employee.Password)
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
            ForgotPassword form = new ForgotPassword();
            form.ShowDialog();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EmployeeForm form = new EmployeeForm(null, "add");
            form.ShowDialog();
        }
    }
}
