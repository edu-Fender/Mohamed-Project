using System;
using System.Windows.Forms;

namespace project
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EmployeeForm Form1 = new EmployeeForm();  //calls 'WinnerForm'
            Form1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InventoryForm Form2 = new InventoryForm();  //calls 'WinnerForm'
            Form2.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SupplierForm Form3 = new SupplierForm();  //calls 'WinnerForm'
            Form3.ShowDialog();
        }
    }
}
