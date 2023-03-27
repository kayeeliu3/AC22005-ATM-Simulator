/*
 * TEAM 2: Joshua Price (2481545), Ananya Bhatt (2462696), Kayee Liu (2454633)
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ConsoleApplication3
{
    public partial class ATMForm : Form
    {
        /// <summary>
        /// Bank class containing array of account objects.
        /// </summary>
        class Bank
        {
            private Account[] Accounts = new Account[3];

            /// <summary>
            /// Initialises 3 accounts.
            /// </summary>
            public Bank()
            {
                Accounts[0] = new Account(300, 1111, 111111, "Ananya");
                Accounts[1] = new Account(750, 2222, 222222, "Josh");
                Accounts[2] = new Account(3000, 3333, 333333, "Kae");
            }

            /// <summary>
            /// Check if account number exists in Bank.
            /// </summary>
            /// <param name="accountNumber">Account number to check.</param>
            /// <returns>Returns account object if the account number exists, else returns null.</returns>
            public Account checkAccountExists(int accountNumber)
            {
                foreach (Account Account in this.Accounts)
                {
                    if (Account.accountNum == accountNumber)
                        return Account;
                }

                return null;
            }
        }

        static bool fileExists = false;
        int errorCountdown = 5; // blocks card once reaches 0
        Bank bank = new Bank(); // generic bank instance
        Account activeAccount; // stores active account details
        string transactionLog = @"../../transaction_log.txt"; //text file containing transaction log

        public ATMForm()
        {
            //create new empty transaction log
            if (!fileExists)
            {
                File.Create(transactionLog);
                fileExists = true;
            }
            InitializeComponent();
        }

        /// <summary>
        /// Check if accont details and PIN are valid and match.
        /// Shows error message if invalid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int i, j;
            if (!int.TryParse(tbAccNum.Text, out i)
                || !int.TryParse(tbPin.Text, out j))
            {
                MessageBox.Show("Please only use numbers for account number and PIN.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            activeAccount = bank.checkAccountExists(i);
            if (activeAccount == null)
            {
                MessageBox.Show("Account number not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!activeAccount.checkPin(j))
            {
                if (errorCountdown == 0)
                {
                    MessageBox.Show("Too many incorrect attempts. Blocking card (closing ATM).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
                MessageBox.Show("Wrong PIN. Remaining attempts: " + errorCountdown, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorCountdown--;
                return;
            }

            accountMenu();
            btnNewATM.Visible = false;
        }

        /// <summary>
        /// Create a new thread to handle separate ATMs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewATM_Click(object sender, EventArgs e)
        {
            Thread threadATM = new Thread(new ThreadStart(newFormATM));
            threadATM.Start();
        }

        /// <summary>
        /// Create a new form for the newly made thread.
        /// </summary>
        private void newFormATM()
        {
            ATMForm newATMForm = new ATMForm(); // stores instance of new ATM form
            newATMForm.ShowDialog();
        }

        /// <summary>
        /// Menu display after account num and PIN are valid - shows balance and other relevant details and option to withdraw cash and logout.
        /// </summary>
        private void accountMenu()
        {
            Controls.Clear();
            this.BackgroundImage = Properties.Resources.AccountMenu;
            Label lblAccountName = new Label();
            Label lblAccountNum = new Label();
            Label lblBalance = new Label();
            Button btnWithdraw = new Button();
            Button btnCheckBalance = new Button();
            Button btnLogout = new Button();
            Button btnLog = new Button();


            lblAccountName.SetBounds(this.ClientSize.Width / 2 - 5, this.ClientSize.Height / 2 - 110, 100, 50);
            btnWithdraw.SetBounds(this.ClientSize.Width / 2 + 150, this.ClientSize.Height / 2 - 30, 40, 40);
            btnCheckBalance.SetBounds(this.ClientSize.Width / 2 + 150, this.ClientSize.Height / 2 + 10, 40, 40);
            btnLogout.SetBounds(this.ClientSize.Width / 2 + 150, this.ClientSize.Height / 2 + 50, 40, 40);
            btnLog.SetBounds(this.ClientSize.Width / 2 + 150, this.ClientSize.Height / 2 + 120, 80, 40);
            lblAccountName.Text = activeAccount.name;
            lblAccountName.TextAlign = ContentAlignment.MiddleCenter;
            lblBalance.Text = "Balance: " + activeAccount.balance;
            btnLog.Text = "Transaction Log";
            btnWithdraw.Text = "1";
            btnCheckBalance.Text = "2";
            btnLogout.Text = "3";

            btnWithdraw.Click += new EventHandler(this.btnWithdraw_Click);
            btnLogout.Click += new EventHandler(this.btnLogout_Click);
            btnCheckBalance.Click += new EventHandler(this.btnCheckBalance_Click);
            btnLog.Click += new EventHandler(this.btnLog_Click);

            Controls.Add(lblAccountName);
            Controls.Add(lblAccountNum);
            Controls.Add(lblBalance);
            Controls.Add(btnWithdraw);
            Controls.Add(btnCheckBalance);
            Controls.Add(btnLogout);
            Controls.Add(btnLog);
        }

        /// <summary>
        /// Opens up transaction log for the active account.
        /// </summary>
        private void btnLog_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe", transactionLog);
        }

        /// <summary>
        /// Logout of account menu and return to main menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogout_Click(object sender, EventArgs e)
        {
            Controls.Clear();
            btnNewATM.Visible = false;
            this.BackgroundImage = Properties.Resources.End;
            //initMenu();
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            Controls.Clear();
            btnNewATM.Visible = false;
            this.BackgroundImage = Properties.Resources.Cash;

            Button[] btnAmounts = new Button[5];
            Button btnCustomAmount = new Button();
            Button btnMenu = new Button();

            for (int i = 0; i < btnAmounts.Length; i++)
                btnAmounts[i] = new Button();
            
            btnAmounts[0].Text = "£10";
            btnAmounts[1].Text = "£20";
            btnAmounts[2].Text = "£40";
            btnAmounts[3].Text = "£100";
            btnAmounts[4].Text = "£500";

            for (int i = 0; i < btnAmounts.Length; i++)
            {
                btnAmounts[i].SetBounds(this.ClientSize.Width / 2 - 225 + (i * 100), this.ClientSize.Height / 2 + 25, 50, 50);
            }
            btnAmounts[0].Click += new EventHandler((s, ev) => btnWithdrawAmount_Click(s, ev, btnAmounts[0].Text));
            btnAmounts[1].Click += new EventHandler((s, ev) => btnWithdrawAmount_Click(s, ev, btnAmounts[1].Text));
            btnAmounts[2].Click += new EventHandler((s, ev) => btnWithdrawAmount_Click(s, ev, btnAmounts[2].Text));
            btnAmounts[3].Click += new EventHandler((s, ev) => btnWithdrawAmount_Click(s, ev, btnAmounts[3].Text));
            btnAmounts[4].Click += new EventHandler((s, ev) => btnWithdrawAmount_Click(s, ev, btnAmounts[4].Text));

            btnCustomAmount.SetBounds(this.ClientSize.Width / 2 + 100, this.ClientSize.Height / 2 + 120, 100, 50);
            btnMenu.SetBounds(this.ClientSize.Width / 2 - 200, this.ClientSize.Height / 2 + 120, 100, 50);
            btnMenu.Click += new EventHandler(this.btnMenu_Click);
            btnMenu.Text = "Return to main menu";
            btnCustomAmount.Click += new EventHandler(this.btnCustomAmount_Click);
            btnCustomAmount.Text = "Custom Amount";

            foreach (var btn in btnAmounts)
            {
                Controls.Add(btn);
            }
            Controls.Add(btnCustomAmount);
            Controls.Add(btnMenu);
        }

        /*
         * Withdraw a certain amount and take this away from the account balance
         */
        private void btnWithdrawAmount_Click(object s, EventArgs e, string amount)
        {
            amount = amount.Substring(1);

            if (!activeAccount.decrementBalance(Int32.Parse(amount)))
            {
                MessageBox.Show("Insufficient funds.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Transaction success!");
                logTransaction(Int32.Parse(amount));
                accountMenu();
            }
        }

        private void btnCustomAmount_Click(object sender, EventArgs e)
        {
            Controls.Clear();
            btnNewATM.Visible = false;
            this.BackgroundImage = Properties.Resources.CustomWithdraw;
            TextBox tbAmount = new TextBox();
            Button btnGoBack = new Button();
            Button btnWithdrawCustomAmount = new Button();
            Button btnSubmitCustomAmount = new Button();

            tbAmount.SetBounds(this.ClientSize.Width / 2, this.ClientSize.Height / 2 + 40, 200, 600);
            btnGoBack.SetBounds(this.ClientSize.Width / 2 - 150, this.ClientSize.Height / 2 + 120, 80, 40);
            btnSubmitCustomAmount.SetBounds(this.ClientSize.Width / 2 + 160, this.ClientSize.Height / 2 + 120, 80, 40);
            btnSubmitCustomAmount.Text = "Submit";
            btnGoBack.Text = "Go Back";

            btnGoBack.Click += new EventHandler(this.btnGoBack_Click);
            btnSubmitCustomAmount.Click += new EventHandler((s, ev) => btnSubmitCustomAmount_Click(s, ev, tbAmount.Text));

            Controls.Add(btnGoBack);
            Controls.Add(tbAmount);
            Controls.Add(btnSubmitCustomAmount);
        }

        private void btnSubmitCustomAmount_Click(object sender, EventArgs e, string amount)
        {
            int parse;
            if ((!int.TryParse(amount, out parse)) || ((parse % 5) != 0))
            {
                MessageBox.Show("Please enter a valid amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!activeAccount.decrementBalance(parse))
            {
                MessageBox.Show("Insufficient funds.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Transaction success!");
                logTransaction(parse);
                accountMenu();
            }
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            btnWithdraw_Click( sender, e);
        }

        private void btnCheckBalance_Click(object sender, EventArgs e) 
        {
            Controls.Clear();
            this.BackgroundImage = Properties.Resources.Balance;
            Label lblAccountName = new Label();
            Label lblBalance = new Label();
            Button btnLogout = new Button();
            Button btnMenu = new Button();

            lblAccountName.SetBounds(this.ClientSize.Width / 2 - 5, this.ClientSize.Height / 2 - 70, 100, 50);
            lblBalance.SetBounds(this.ClientSize.Width / 2 + 150, this.ClientSize.Height / 2 + 60, 50, 25);
            btnLogout.SetBounds(this.ClientSize.Width / 2 + 90, this.ClientSize.Height / 2 + 120, 70, 40);
            btnMenu.SetBounds(this.ClientSize.Width / 2 - 150, this.ClientSize.Height / 2 + 120, 150, 40);
            lblAccountName.Text = activeAccount.name;
            lblAccountName.TextAlign = ContentAlignment.MiddleCenter;
            lblBalance.TextAlign = ContentAlignment.MiddleCenter;
            lblBalance.Text = activeAccount.balance.ToString();
            btnLogout.Text = "Logout";
            btnMenu.Text = "Return to main menu";
            btnLogout.Click += new EventHandler(this.btnLogout_Click);
            btnMenu.Click += new EventHandler(this.btnMenu_Click);

            Controls.Add(lblAccountName);
            Controls.Add(lblBalance);
            Controls.Add(btnLogout);
            Controls.Add(btnMenu);
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            accountMenu();

        }

        /// <summary>
        /// Logs transaction into the transaction_log text file
        /// Referenced from Microsoft
        /// </summary>
        private void logTransaction(int amount)
        {
            using (StreamWriter sw = File.AppendText(transactionLog))
            {
                sw.WriteLine("Account " + activeAccount.name + " withdrew £" + amount);
            }
        }
    }
}
