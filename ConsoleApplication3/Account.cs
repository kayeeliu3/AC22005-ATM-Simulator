using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApplication3
{
    /// <summary>
    /// Encapsulates all features of a simple bank account.
    /// </summary>
    public class Account
    {
        // Attributes for account.
        private static Semaphore _pool = new Semaphore(1, 1);
        public int balance { get; private set; }
        private int pin;
        public int accountNum { get; private set; }
        public string name { get; }
        public bool useSemaphore { get; set; }

        /// <summary>
        /// Construct new bank account
        /// </summary>
        /// <param name="balance">Initial balance of the bank account.</param>
        /// <param name="pin">Pin of the bank account.</param>
        /// <param name="accountNum">Account number of the bank account.</param>
        /// <param name="accountName">Name of the bank account owner.</param>
        /// <param name="useSemaphore">Option to turn Semaphore on or off.</param>
        public Account(int balance, int pin, int accountNum, string accountName, bool useSemaphore)
        {
            this.balance = balance;
            this.pin = pin;
            this.accountNum = accountNum;
            this.name = accountName;
            this.useSemaphore = useSemaphore;
        }

        /// <summary>
        /// Decrements balance of an account.
        /// </summary>
        /// <param name="amount">Amount to deduct.</param>
        /// <returns>True if successful, false if insufficient funds in account.</returns>
        public Boolean decrementBalance(int amount)
        {
            if (useSemaphore)
            { 
                _pool.WaitOne();
            }
            if (this.balance >= amount)
            {
                int tempBalance = balance;
                Thread.Sleep(10000);
                
                balance = tempBalance - amount;
                if (useSemaphore)
                {
                    _pool.Release();
                }
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
        /// <param name="pin"></param>
        /// <returns>True if they match, false if they do not.</returns>
        public Boolean checkPin(int pin)
        {
            return pin == this.pin;
        }
    }
}
