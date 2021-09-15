using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace project
{
    public partial class ForgotPassword : Form
    {
        public ForgotPassword()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<EmployeeModel>  employees = Connection.LoadRecords<EmployeeModel>();

            foreach (var employee in employees)
            {
                if (textBox1.Text == employee.Email && textBox2.Text == employee.FirstName && textBox3.Text == employee.LastName)
                {
                    MessageBox.Show($"USER RECOVERED WITH SUCCESS. TRY NOT TO FORGET YOUR PASSWORD NEXT TIME. \n\nLogin: {employee.Email} \nPassword: {employee.Password}");
                    return;
                }
            }

            MessageBox.Show("ERROR: USER NOT FOUND. MAKE SURE ALL THE FIELDS ARE CORRECT.");
            return;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
