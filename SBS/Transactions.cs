using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBS
{
    class Transactions
    {

       

        public decimal calculateTransact(int transactType, decimal accountBalance, decimal transactAmount, out string errorMsg)
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

                case 100:
                    {
                        if (transactAmount > 0)
                        {
                            decimal interest = accountBalance * transactAmount;
                            accountBalance = accountBalance + interest;
                            return accountBalance;
                        }

                        return accountBalance;
                    }
            }
            return accountBalance;
        }

    }
}
