﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class CustomerForm : Form
    {
        private int? selectedIndex;
        private string senderButton;
        public int refresh { get; private set; }

        public CustomerForm(int? selectedIndex, string senderButton)
        {
            InitializeComponent();

            this.Select();
            this.selectedIndex = selectedIndex;
            this.senderButton = senderButton;

            if (senderButton == "view" || senderButton == "update")
            {
                List<CustomerModel> customer = Connection.LoadRecords<CustomerModel>();

                textBox1.Text = customer[selectedIndex.Value].CustomerId;
                textBox2.Text = customer[selectedIndex.Value].FirstName;
                textBox3.Text = customer[selectedIndex.Value].LastName;
                textBox4.Text = customer[selectedIndex.Value].Email;
                textBox5.Text = customer[selectedIndex.Value].Number;
                textBox6.Text = customer[selectedIndex.Value].Address;

                if (senderButton == "view")
                {
                    button1.Enabled = false;  // Disable "SAVE" button
                    foreach (TextBox ctrl in panel1.Controls)  // Makes everything read-only
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
                int.Parse(textBox1.Text);
            }
            catch
            {
                MessageBox.Show("ERROR: FIELD 'CUSTOMER ID' MUST BE INTEGER!");
                return;
            }

            CustomerModel customer = new CustomerModel
            {
                CustomerId = textBox1.Text,
                FirstName = textBox2.Text,
                LastName = textBox3.Text,
                Email = textBox4.Text,
                Number = textBox5.Text,
                Address = textBox6.Text,
            };

            switch (senderButton)  // will find out the type of the list automatically 
            {
                case "view":
                    break;
                case "update":
                    Connection.UpdateRecord(customer, selectedIndex.Value);
                    break;
                case "add":
                    Connection.AddRecord(customer);
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

            foreach (Control ctrl in panel1.Controls)  // Checks if any of the textboxes was filled
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