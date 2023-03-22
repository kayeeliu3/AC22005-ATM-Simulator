/*
 * TEAM 2: Joshua Price (2481545), Ananya Bhatt (2462696), Kayee Liu (2454633)
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ConsoleApplication3
{
    public partial class WelcomeScreen : Form
    {
        /// <summary>
        /// Bank class containing array of account objects.
        /// </summary>
        class Bank
        {
            private Account[] ac = new Account[3];

            /// <summary>
            /// Initialises 3 accounts.
            /// </summary>
            public Bank()
            {
                ac[0] = new Account(300, 1111, 111111, "Ananya");
                ac[1] = new Account(750, 2222, 222222, "Josh");
                ac[2] = new Account(3000, 3333, 333333, "Kae");
            }

            /// <summary>
            /// Check if account number exists in Bank.
            /// </summary>
            /// <param name="accNumEntered">Account number to check.</param>
            /// <returns>Returns account object if the account number exists, else returns null.</returns>
            public Account checkAccExists(int accNumEntered)
            {
                foreach (Account acc in this.ac)
                {
                    if (acc.getAccountNum() == accNumEntered)
                        return acc;
                }

                return null;
            }
        }

        /// <summary>
        /// Encapsulates all features of a simple bank account.
        /// </summary>
        class Account
        {
            // Attributes for account.
            private int balance;
            private int pin;
            private int accountNum;
            private string accountName;

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
                this.accountName = accountName;
            }

            // Getter and setter functions for balance.
            public int getBalance()
            {
                return balance;
            }
            public void setBalance(int newBalance)
            {
                this.balance = newBalance;
            }

            public string getName()
            {
                return accountName;
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
                if (pinEntered == pin)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public int getAccountNum()
            {
                return accountNum;
            }
        }

        Bank bank = new Bank(); // generic bank instance
        Account activeAccount; // stores active account details

        //TextBox tbAccNum = new TextBox(); // stores account number text
        //TextBox tbPin = new TextBox(); // stores PIN text
        //TextBox tbAccName = new TextBox(); // stores name of the account holder

        public WelcomeScreen()
        {
            InitializeComponent();
            initMenu();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /*
         * Display main menu for Bank system
         */
        private void initMenu()
        {
            //Button btnSubmit = new Button();
            //Button btnNewATM = new Button();

            //Controls.Add(tbAccNum);
            //Controls.Add(tbPin);
            //Controls.Add(btnSubmit);
            //Controls.Add(btnNewATM);
        }

        /*
         * Check if account details and PIN are valid and match.
         * Shows error message if invalid.
         */

        /// <summary>
        /// Check if accont details and PIN are valid and match.
        /// Shows error message if invalid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click_1(object sender, EventArgs e)
        {
            int i, j;
            if (int.TryParse(tbAccNum.Text, out i))
            {
                if (int.TryParse(tbPin.Text, out j))
                {
                    activeAccount = bank.checkAccExists(i);
                    if (activeAccount != null)
                    {
                        if (activeAccount.checkPin(j))
                        {
                            accountMenu();
                            btnNewATM.Visible = false;
                        }
                        else
                            MessageBox.Show("Wrong PIN.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("Account number not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Please only use numbers for account number and PIN.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Please only use numbers for account number and PIN.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Create a new thread to handle separate ATMs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewATM_Click_1(object sender, EventArgs e)
        {
            Thread threadATM = new Thread(new ThreadStart(newFormATM));
            threadATM.Start();
        }

        /// <summary>
        /// Create a new form for the newly made thread.
        /// </summary>
        private void newFormATM()
        {
            WelcomeScreen newATMForm = new WelcomeScreen(); // stores instance of new ATM form
            newATMForm.ShowDialog();
        }

        /// <summary>
        /// Menu display after account num and PIN are valid - shows balance and other relevant details and option to withdraw cash and logout.
        /// </summary>
        private void accountMenu()
        {
            clearForm();
            this.BackgroundImage = Properties.Resources.Options;
            Label lblAccountName = new Label();
            Label lblAccountNum = new Label();
            Label lblBalance = new Label();
            Button btnWithdraw = new Button();
            Button btnCheckBalance = new Button();
            Button btnLogout = new Button();


            lblAccountName.SetBounds(this.ClientSize.Width / 2 - 5, this.ClientSize.Height / 2 - 110, 100, 50);
            btnWithdraw.SetBounds(this.ClientSize.Width / 2 + 150, this.ClientSize.Height / 2 - 30, 40, 40);
            btnCheckBalance.SetBounds(this.ClientSize.Width / 2 + 150, this.ClientSize.Height / 2 + 10, 40, 40);
            btnLogout.SetBounds(this.ClientSize.Width / 2 + 150, this.ClientSize.Height / 2 + 50, 40, 40);
            lblAccountName.Text = activeAccount.getName();
            lblAccountName.TextAlign = ContentAlignment.MiddleCenter;
            //lblAccountName.BackgroundImage = Properties.Resources.Options; 
            lblBalance.Text = "Balance: " + activeAccount.getBalance();
            //lblAccountNum.Text = "Account Num: " + activeAccount.getAccountNum();
            btnWithdraw.Text = "1";
            btnCheckBalance.Text = "2";
            btnLogout.Text = "3";

            btnWithdraw.Click += new EventHandler(this.btnWithdraw_Click);
            btnLogout.Click += new EventHandler(this.btnLogout_Click);
            btnCheckBalance.Click += new EventHandler(this.btnCheckBalance_Click);
            Controls.Add(lblAccountName);
            Controls.Add(lblAccountNum);
            Controls.Add(lblBalance);
            Controls.Add(btnWithdraw);
            Controls.Add(btnCheckBalance);
            Controls.Add(btnLogout);
        }

        /// <summary>
        /// Logout of account menu and return to main menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogout_Click(object sender, EventArgs e)
        {
            clearForm();
            btnNewATM.Visible = false;
            this.BackgroundImage = Properties.Resources.End;
            //initMenu();
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            clearForm();
            btnNewATM.Visible = false;
            this.BackgroundImage = Properties.Resources.Cash;

            Button[] btnAmounts = new Button[5];
            Button btnCustomAmount = new Button();
            Button btnMenu = new Button();

            // Currently the ordering of the following code is a tad odd - a bug occured with timing due to event handler having to run AFTER
            // the text has been set - will try to fix later if possible! (Kae)

            for (int i = 0; i < btnAmounts.Length; i++)
                btnAmounts[i] = new Button();
            
            btnAmounts[0].Text = "£10";
            btnAmounts[1].Text = "£20";
            btnAmounts[2].Text = "£40";
            btnAmounts[3].Text = "£100";
            btnAmounts[4].Text = "£500";

            for (int i = 0; i < btnAmounts.Length; i++)
            {
                int tempText;
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
            //btnCustomAmount.Click += new EventHandler((s, ev) => btnWithdrawAmount_Click(s, ev, ""));
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
            if (amount == "")
                customAmountMenu();

            amount = amount.Substring(1);

            if (!activeAccount.decrementBalance(Int32.Parse(amount)))
            {
                MessageBox.Show("Insufficient funds.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Transaction success!");
                accountMenu();
            }
        }

        private void btnCustomAmount_Click(object sender, EventArgs e)
        {
            clearForm();
            btnNewATM.Visible = false;
            this.BackgroundImage = Properties.Resources.CustomWithdraw;
            TextBox tbAmount = new TextBox();
            Button btnGoBack = new Button();
            Button btnWithdrawCustomAmount = new Button();

            tbAmount.SetBounds(this.ClientSize.Width / 2, this.ClientSize.Height / 2 + 40, 200, 600);
            btnGoBack.SetBounds(this.ClientSize.Width / 2 - 150, this.ClientSize.Height / 2 + 120, 80, 40);
            btnGoBack.Text = "Go Back";

            btnGoBack.Click += new EventHandler(this.btnGoBack_Click);

            Controls.Add(btnGoBack);
            Controls.Add(tbAmount);
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            btnWithdraw_Click( sender, e);
        }

        private void customAmountMenu()
        {
            Label lblInstructions = new Label();
            lblInstructions.Text = "Please enter an amount divisible by £5.";

            //Insert code

            clearForm();
        }

        private void btnCheckBalance_Click(object sender, EventArgs e) 
        { 
            clearForm();
            this.BackgroundImage = Properties.Resources.Balance;
            Label lblAccountName = new Label();
            Label lblBalance = new Label();
            Button btnLogout = new Button();
            Button btnMenu = new Button();

            lblAccountName.SetBounds(this.ClientSize.Width / 2 - 5, this.ClientSize.Height / 2 - 70, 100, 50);
            lblBalance.SetBounds(this.ClientSize.Width / 2 + 150, this.ClientSize.Height / 2 + 60, 50, 25);
            btnLogout.SetBounds(this.ClientSize.Width / 2 + 90, this.ClientSize.Height / 2 + 120, 70, 40);
            btnMenu.SetBounds(this.ClientSize.Width / 2 - 150, this.ClientSize.Height / 2 + 120, 150, 40);
            lblAccountName.Text = activeAccount.getName();
            lblAccountName.TextAlign = ContentAlignment.MiddleCenter;
            lblBalance.TextAlign = ContentAlignment.MiddleCenter;
            lblBalance.Text = activeAccount.getBalance().ToString();
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
        /// Clears the form, allowing for switching between screens.
        /// (Kayee: This function has been taken and reused from the grid game assignment.)
        /// </summary>
        private void clearForm()
        {
            // Remove all buttons.
            foreach (var btn in Controls.OfType<Button>().ToList())
            {
                if (btn.Name == "btnNewATM")
                    continue;

                Controls.Remove(btn);
            }
            // Remove all labels.
            foreach (var lbl in Controls.OfType<Label>().ToList())
            {
                Controls.Remove(lbl);
            }

            // Remove all text boxes.
            foreach (var tBox in Controls.OfType<TextBox>().ToList())
            {
                Controls.Remove(tBox);
            }
            // Remove all images.
            foreach (var image in Controls.OfType<PictureBox>().ToList())
            {
                Controls.Remove(image);
            }
        }

    }
}
