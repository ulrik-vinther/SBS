using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBS
{
    class Transactions
    {


        public decimal calculateTransact(int transactType, decimal accountBalance, decimal transactAmount, out string errorMsg, string lastInterestDate = "" )
        {
            errorMsg = "";
            switch (transactType)
            {
                case 1:
                    {
                        if (transactAmount > 0)
                        {
                            decimal newBalance = accountBalance - transactAmount;
                            if (newBalance < 0)
                            {
                                // ERROR
                                errorMsg = "The current balance is " + accountBalance + "\r\n\n" +
                                  "Your withdrawal of " + transactAmount +
                                  " is not possible at this time !! \r\n\n Please withdraw a lower amount.";
                                return accountBalance;
                            }
                            else
                            {
                                accountBalance = newBalance;
                                return newBalance;
                            }
                        }
                        return accountBalance;
                    }
                case 2:
                    {
                        if (transactAmount > 0)
                        {
                            accountBalance = accountBalance + transactAmount;
                            return accountBalance;
                        }
                        return accountBalance;
                    }

                case 3:
                    {
                        if (transactAmount > 0)
                        {
                            if (lastInterestDate == "")
                            {
                                try
                                {
                                    throw new interestAddingException("The last date of interest adding is missing !!");
                                }
                                catch (interestAddingException ex)
                                {
                                    errorMsg = ex.Message;
                                    return accountBalance;
                                }
                             }
                            else
                            {
                                DateTime interestDate = Convert.ToDateTime(lastInterestDate);
                                TimeSpan diff = DateTime.Today - interestDate;
                                if (diff.TotalDays >= 365)
                                {
                                    decimal interest = accountBalance * transactAmount;
                                    accountBalance = accountBalance + interest;
                                    return accountBalance;
                                }
                                else
                                {
                                    errorMsg = "It has not been a year since last interest adding !!";
                                    return accountBalance;
                                }
                            }
                        }
                        return accountBalance;
                    }
            }
            return accountBalance;
        }


    }



    public class interestAddingException : Exception
    {
        public interestAddingException(string message)
            : base(message) { 
        }
    }
   

    public class negativeAmountException : Exception
    {
        public negativeAmountException(string message)
        :base(message){ }
    }

}
