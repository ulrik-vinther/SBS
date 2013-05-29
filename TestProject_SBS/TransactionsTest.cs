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

        // Stubs
        DataSet accountsDS;
        string accountsXmlFile = "c:\\SBSAccounts.xml";
        //string errorMsg = "";
        //DateTime lastInterestUpdate;


        public void loadAccounts()
        {
            try
            {
                accountsDS = new DataSet();
                accountsDS.ReadXml(accountsXmlFile);
                //comboBox1.DataSource = accountsDS.Tables["Table1"];
                //comboBox1.DisplayMember = "AccountNumber";
            }
            catch (Exception)
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
            Console.WriteLine("TestID 1");
            Transactions target = new Transactions(); 
            int transactType = 1; // WithDraw
            Decimal accountBalance = new Decimal(10000); // REM
            Decimal transactAmount = new Decimal(9000); // REM
            string errorMsg = string.Empty; // REM
            string errorMsgExpected = string.Empty; // REM
            Decimal expected = new Decimal(1000); // REM
            Decimal actual;
            actual = target.calculateTransact(transactType, accountBalance, transactAmount, out errorMsg);
            Assert.AreEqual(errorMsgExpected, errorMsg);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void calculateTransactTest_WithdrawIllegalAmount()
        {
            Console.WriteLine("TestID 2");
            Transactions target = new Transactions(); // REM
            int transactType = 1; // WithDraw
            Decimal accountBalance = new Decimal(10000); // REM
            Decimal transactAmount = new Decimal(11000); /**
                                                        * Withdraw more than the current balance
                                                        * This should not be possible, and must raise an error message.
                                                        */ 
            string errorMsg = string.Empty; // REM
            string errorMsgExpected = "The current balance is " + accountBalance + "\r\n\n" +
                                  "Your withdrawal of " + transactAmount +
                                  " is not possible at this time !! \r\n\n Please withdraw a lower amount.";
            Console.WriteLine(errorMsg);
            Decimal expected = new Decimal(10000); // REM
            Decimal actual;
            actual = target.calculateTransact(transactType, accountBalance, transactAmount, out errorMsg);
            Assert.AreEqual(errorMsgExpected, errorMsg);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }


        [TestMethod()]
        public void calculateTransactTest_WithdrawNegativeAmount()
        {
            Console.WriteLine("TestID 3");
            Transactions target = new Transactions(); // REM
            int transactType = 1; // REM
            Decimal accountBalance = new Decimal(10000); // REM
            Decimal transactAmount = new Decimal(-1000); // REM
            string errorMsg = string.Empty; // REM
            string errorMsgExpected = string.Empty; // REM
            Decimal expected = new Decimal(10000); // REM
            Decimal actual;
            actual = target.calculateTransact(transactType, accountBalance, transactAmount, out errorMsg);
            Assert.AreEqual(errorMsgExpected, errorMsg);
            Assert.AreEqual(expected, actual);
        }

        /**
         * The next two tests are Black-box boundary tests 
         */
        
        [TestMethod()]
        public void calculateTransactTest_WithdrawBoundaryAmount()
        {
            Console.WriteLine("TestID 4:boundary");
            Transactions target = new Transactions(); // REM
            int transactType = 1; // REM
            Decimal accountBalance = new Decimal(); // REM
            Decimal transactAmount = new Decimal(); // REM
            accountBalance = 0.01m;
            transactAmount = 0.01m;
            string errorMsg = string.Empty; // REM
            string errorMsgExpected = string.Empty; // REM
            Decimal expected = new Decimal(0); // REM
            Decimal actual;
            actual = target.calculateTransact(transactType, accountBalance, transactAmount, out errorMsg);
            Assert.AreEqual(errorMsgExpected, errorMsg);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void calculateTransactTest_WithdrawIllegalBoundaryAmount()
        {
            Console.WriteLine("TestID 5:boundary");
            Transactions target = new Transactions(); // REM
            int transactType = 1; // REM
            Decimal accountBalance = new Decimal(); // REM
            Decimal transactAmount = new Decimal(); // REM
            accountBalance = 0;
            transactAmount = 0.01m;
            string errorMsg = string.Empty; // REM
            string errorMsgExpected = "The current balance is " + accountBalance + "\r\n\n" +
                                  "Your withdrawal of " + transactAmount +
                                  " is not possible at this time !! \r\n\n Please withdraw a lower amount.";
            Decimal expected = new Decimal(0); // REM
            Decimal actual;
            actual = target.calculateTransact(transactType, accountBalance, transactAmount, out errorMsg);
            Assert.AreEqual(errorMsgExpected, errorMsg);
            Assert.AreEqual(expected, actual);
        }

        /**
         * the end of black-box boundary test
         */

        [TestMethod()]
        public void calculateTransactTest_DepositPositiveAmount()
        {
            Console.WriteLine("TestID 4");
            Transactions target = new Transactions(); // REM
            int transactType = 2; // REM
            Decimal accountBalance = new Decimal(10000); // REM
            Decimal transactAmount = new Decimal(1000); // REM
            string errorMsg = string.Empty; // REM
            string errorMsgExpected = string.Empty; // REM
            Decimal expected = new Decimal(11000); // REM
            Decimal actual;
            actual = target.calculateTransact(transactType, accountBalance, transactAmount, out errorMsg);
            Assert.AreEqual(errorMsgExpected, errorMsg);
            Assert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void calculateTransactTest_DepositNegativeAmount()
        {
            Console.WriteLine("TestID 5");
            Transactions target = new Transactions(); // REM
            int transactType = 2; // REM
            Decimal accountBalance = new Decimal(10000); // REM
            Decimal transactAmount = new Decimal(-1000); // REM
            string errorMsg = string.Empty; // REM
            string errorMsgExpected = string.Empty; // REM
            Decimal expected = new Decimal(10000); // REM
            Decimal actual;
            actual = target.calculateTransact(transactType, accountBalance, transactAmount, out errorMsg);
            Assert.AreEqual(errorMsgExpected, errorMsg);
            Assert.AreEqual(expected, actual);
        }



        /// <summary>
        ///A test for calculateTransact
        ///</summary>
        [TestMethod()]
        public void calculateTransactTest_interesAddingAfterAYear()
        {
            Console.WriteLine("TestID 6");
            Transactions target = new Transactions(); // TODO: Initialize to an appropriate value
            int transactType = 3; // TODO: Initialize to an appropriate value
            Decimal accountBalance = new Decimal(10000); // TODO: Initialize to an appropriate value
            Decimal transactAmount = new Decimal(0.1); // TODO: Initialize to an appropriate value
            string errorMsg = string.Empty; // TODO: Initialize to an appropriate value
            string errorMsgExpected = string.Empty; // TODO: Initialize to an appropriate value
            string lastInterestDate = Convert.ToString(DateTime.Today.AddDays(-365).Date); // TODO: Initialize to an appropriate value
            Decimal expected = new Decimal(11000); // TODO: Initialize to an appropriate value
            Decimal actual;
            actual = target.calculateTransact(transactType, accountBalance, transactAmount, out errorMsg, lastInterestDate);
            Assert.AreEqual(errorMsgExpected, errorMsg);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }


        [TestMethod()]
        public void calculateTransactTest_interesAddingUnderAYear()
        {
            Console.WriteLine("TestID 7");
            Transactions target = new Transactions(); 
            int transactType = 3; 
            Decimal accountBalance = new Decimal(10000); 
            Decimal transactAmount = new Decimal(0.1); 
            string errorMsg = string.Empty; 
            string errorMsgExpected = "It has not been a year since last interest adding !!";
            string lastInterestDate = Convert.ToString(DateTime.Today.AddDays(-364).Date); 
            Decimal expected = new Decimal(10000); 
            Decimal actual;
            actual = target.calculateTransact(transactType, accountBalance, transactAmount, out errorMsg, lastInterestDate);
            Assert.AreEqual(errorMsgExpected, errorMsg);
            Assert.AreEqual(expected, actual);
            Console.WriteLine(errorMsg);

          }

        [TestMethod()]
        //[ExpectedException(typeof(interestAddingException))]  
        public void calculateTransactTest_interesAddingWithNoDate()
        {
            Console.WriteLine("TestID 8");
            Transactions target = new Transactions(); 
            int transactType = 3; 
            Decimal accountBalance = new Decimal(10000); 
            Decimal transactAmount = new Decimal(0.1); 
            string errorMsg = string.Empty;
            string errorMsgExpected = "The last date of interest adding is missing !!";
            string lastInterestDate = string.Empty; 
            Decimal expected = new Decimal(10000); 
            Decimal actual;
            /// We expect the accountBalance to remain unchanged, as we cannot see the last date of interest update
            /// The method must throw the interestAddingException
            actual = target.calculateTransact(transactType, accountBalance, transactAmount, out errorMsg, lastInterestDate);
            Assert.AreEqual(errorMsgExpected, errorMsg);
            Assert.AreEqual(expected, actual);
            Console.WriteLine(errorMsg);

          }

    }
}
