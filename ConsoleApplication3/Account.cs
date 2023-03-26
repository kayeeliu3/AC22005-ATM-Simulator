using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    /// <summary>
    /// Encapsulates all features of a simple bank account.
    /// </summary>
    internal class Account
    {
        // Attributes for account.
        public int balance { get; private set; }
        private int pin;
        public int accountNum { get; private set; }
        public string name { get; }

        /// <summary>
        /// Construct new bank account
        /// </summary>
        /// <param name="balance">Initial balance of the bank account.</param>
        /// <param name="pin">Pin of the bank account.</param>
        /// <param name="accountNum">Account number of the bank account.</param>
        /// <param name="accountName">Name of the bank account owner.</param>
        public Account(int balance, int pin, int accountNum, string accountName)
        {
            this.balance = balance;
            this.pin = pin;
            this.accountNum = accountNum;
            this.name = accountName;
        }

        /// <summary>
        /// Decrements balance of an account.
        /// </summary>
        /// <param name="amount">Amount to deduct.</param>
        /// <returns>True if successful, false if insufficient funds in account.</returns>
        public Boolean decrementBalance(int amount)
        {
            if (this.balance > amount)
            {
                balance -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check the account pin against argument passed.
        /// </summary>
        /// <param name="pinEntered"></param>
        /// <returns>True if thye match, false if they do not.</returns>
        public Boolean checkPin(int pinEntered)
        {
            return pinEntered == pin;
        }
    }
}
