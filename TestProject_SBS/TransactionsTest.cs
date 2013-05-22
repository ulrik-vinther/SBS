using SBS;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;

namespace TestProject_SBS
{
    
    
    /// <summary>
    ///This is a test class for TransactionsTest and is intended
    ///to contain all TransactionsTest Unit Tests
    ///</summary>
    ///

    [TestClass()]
    public class TransactionsTest
    {

        DataSet accountsDS;
        string accountsXmlFile = "c:\\SBSAccounts.xml";
        string errorMsg = "";
        DateTime lastInterestUpdate;


        public void loadAccounts()
        {
            try
            {
                accountsDS = new DataSet();
                accountsDS.ReadXml(accountsXmlFile);
                //comboBox1.DataSource = accountsDS.Tables["Table1"];
                //comboBox1.DisplayMember = "AccountNumber";
            }
            catch (Exception ex)
            {

            }
        }



        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for calculateTransact
        ///</summary>

        [TestMethod()]
        public void calculateTransactTest_WithdrawAllowedAmount()
        {
            Transactions target = new Transactions(); 
            int transactType = 1; // WithDraw
            Decimal accountBalance = new Decimal(500); // REM
            Decimal transactAmount = new Decimal(100); // REM
            string errorMsg = string.Empty; // REM
            string errorMsgExpected = string.Empty; // REM
            Decimal expected = new Decimal(400); // REM
            Decimal actual;
            actual = target.calculateTransact(transactType, accountBalance, transactAmount, out errorMsg);
            Assert.AreEqual(errorMsgExpected, errorMsg);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void calculateTransactTest_WithdrawIllegalAmount()
        {
            Transactions target = new Transactions(); // REM
            int transactType = 1; // WithDraw
            Decimal accountBalance = new Decimal(500); // REM
            Decimal transactAmount = new Decimal(600); /**
                                                        * Withdraw more than the current balance
                                                        * This should not be possible, and must raise an error message.
                                                        */ 
            string errorMsg = string.Empty; // REM
            string errorMsgExpected = "The current balance is " + accountBalance + "\r\n\n" +
                                  "Your withdrawal of " + transactAmount +
                                  " is not possible at this time !! \r\n\n Please withdraw a lower amount.";
            Decimal expected = new Decimal(500); // REM
            Decimal actual;
            actual = target.calculateTransact(transactType, accountBalance, transactAmount, out errorMsg);
            Assert.AreEqual(errorMsgExpected, errorMsg);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }



        //[TestMethod()]
        //public void calculateTransactTest()
        //{
        //    Transactions target = new Transactions(); // REM
        //    int transactType = 0; // REM
        //    Decimal accountBalance = new Decimal(); // REM
        //    Decimal transactAmount = new Decimal(); // REM
        //    string errorMsg = string.Empty; // REM
        //    string errorMsgExpected = string.Empty; // REM
        //    Decimal expected = new Decimal(); // REM
        //    Decimal actual;
        //    actual = target.calculateTransact(transactType, accountBalance, transactAmount, out errorMsg);
        //    Assert.AreEqual(errorMsgExpected, errorMsg);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}


    }
}
