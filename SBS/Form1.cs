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
                decimal interestRate = Convert.ToDecimal(textBox_InterestRate.Text);
                decimal balance = Transactions.calculateTransact(3, Convert.ToDecimal(label1.Text), interestRate, out errorMsg, Convert.ToString(lastInterestUpdate.Date));  //
                
            
                if (errorMsg != "")
                {
                    MessageBox.Show(errorMsg);
                    errorMsg = "";
                }
                else
                {
                    lastInterestUpdate = DateTime.Today;
                    label2.Text = "Last interest update: " + lastInterestUpdate.Date.ToShortDateString();
                    label1.Text = balance.ToString("0.00");
                    accountsDS.Tables["Table1"].Rows[comboBox1.SelectedIndex].SetField<string>("Balance", label1.Text);
                    writeAccounts();
                }


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

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            lastInterestUpdate = DateTime.Today.AddYears(-1);
            label2.Text = "Last interest update: " + lastInterestUpdate.Date.ToShortDateString();
        }

        private void textBox_FirstName_Leave(object sender, EventArgs e)
        {
            accountsDS.Tables["Table1"].Rows[comboBox1.SelectedIndex].SetField<string>("FirstName", textBox_FirstName.Text);
            writeAccounts();
        }

        private void textBox_LastName_Leave(object sender, EventArgs e)
        {
            accountsDS.Tables["Table1"].Rows[comboBox1.SelectedIndex].SetField<string>("LastName", textBox_LastName.Text);
            writeAccounts();
        }

        private void textBox_InterestRate_Leave(object sender, EventArgs e)
        {
            accountsDS.Tables["Table1"].Rows[comboBox1.SelectedIndex].SetField<string>("InterestRate", textBox_InterestRate.Text);
            writeAccounts();
        }





    }
}
