using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SBS;


namespace SBS
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }


        Transactions Transactions = new Transactions();
        DataSet accountsDS;
        string accountsXmlFile = "c:\\SBSAccounts.xml";
        string errorMsg = "";
        DateTime lastInterestUpdate;


        //private decimal calculateTransact(int transactType, decimal accountBalance, decimal transactAmount)
        //{
        //    switch (transactType) 
        //    {
        //        case 1:
        //            {
        //                if (transactAmount > 0)
        //                {
        //                    decimal newBalance = accountBalance - transactAmount;
        //                    if (newBalance < 0)
        //                    {
        //                        MessageBox.Show("The current balance is " + accountBalance + "\r\n\n" +
        //                          "Your withdrawal of " + transactAmount +
        //                          " is not possible at this time !! \r\n\n Please withdraw a lower amount.");
        //                        return accountBalance;
        //                    }
        //                    else
        //                    {
        //                        accountBalance = newBalance;
        //                        maskedTextBox_transactAmount.Text = "";
        //                        return newBalance;
        //                    }
        //                }
        //                return accountBalance;
        //            } 
        //        case 2:
        //            {
        //                if (transactAmount > 0)
        //                {
        //                    accountBalance = accountBalance+transactAmount;
        //                    maskedTextBox_transactAmount.Text = "";
        //                    return accountBalance;
        //                }
        //                return accountBalance;
        //            }

        //        case 100:
        //            {
        //                if (transactAmount > 0)
        //                {
        //                    decimal interest = accountBalance * transactAmount;
        //                    accountBalance = accountBalance + interest;
        //                    return accountBalance;
        //                }

        //                return accountBalance;
        //            }
        //}
        //    return accountBalance;
        //}


        private void button1_Click(object sender, EventArgs e)
        {
            if (maskedTextBox_transactAmount.Text != "")
            {
                decimal balance = Transactions.calculateTransact(1, Convert.ToDecimal(label1.Text), Convert.ToDecimal(maskedTextBox_transactAmount.Text),out errorMsg);
                label1.Text = balance.ToString("0.00");
                accountsDS.Tables["Table1"].Rows[comboBox1.SelectedIndex].SetField<string>("Balance", label1.Text);
                writeAccounts();

                if (errorMsg != "")
                { MessageBox.Show(errorMsg);
                errorMsg = "";
                }

                maskedTextBox_transactAmount.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (maskedTextBox_transactAmount.Text != "")
            {
                decimal balance = Transactions.calculateTransact(2, Convert.ToDecimal(label1.Text), Convert.ToDecimal(maskedTextBox_transactAmount.Text),out errorMsg);
                label1.Text = balance.ToString("0.00");
                accountsDS.Tables["Table1"].Rows[comboBox1.SelectedIndex].SetField<string>("Balance", label1.Text);
                writeAccounts();
                maskedTextBox_transactAmount.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            TimeSpan diffd = today - lastInterestUpdate;
            if (diffd.TotalDays >= 365)
            {
                lastInterestUpdate = DateTime.Today;
                label2.Text = "Last interest update: " + lastInterestUpdate.Date.ToShortDateString();
                decimal interestRate = 0.01m;
                decimal balance = Transactions.calculateTransact(3, Convert.ToDecimal(label1.Text), interestRate, out errorMsg);
                label1.Text = balance.ToString("0.00");

            }
            else { MessageBox.Show("It has not been a year since last interest adding !!"); }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lastInterestUpdate = DateTime.Today.AddYears(-1);
            label2.Text = "Last interest update: " + lastInterestUpdate.Date.ToShortDateString();

            loadAccounts();
        }

        private void loadAccounts()
        {
            try
            {
                accountsDS = new DataSet();
                accountsDS.ReadXml(accountsXmlFile);
                comboBox1.DataSource = accountsDS.Tables["Table1"];
                comboBox1.DisplayMember = "AccountNumber";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void maskedTextBox_transactAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keypress = e.KeyChar;
            if (char.IsDigit(keypress) || e.KeyChar == Convert.ToChar(Keys.Back) || e.KeyChar == Convert.ToChar(","))
            { }
            else
            { e.Handled = true; }
        }

        private void writeAccounts()
        {
            

            try
            {
                accountsDS.WriteXml(accountsXmlFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Text = accountsDS.Tables["Table1"].Rows[comboBox1.SelectedIndex].Field<string>("Balance");
            textBox_FirstName.Text = accountsDS.Tables["Table1"].Rows[comboBox1.SelectedIndex].Field<string>("FirstName");
            textBox_LastName.Text = accountsDS.Tables["Table1"].Rows[comboBox1.SelectedIndex].Field<string>("LastName");
            textBox_InterestRate.Text = accountsDS.Tables["Table1"].Rows[comboBox1.SelectedIndex].Field<string>("InterestRate");

        }



    }
}
