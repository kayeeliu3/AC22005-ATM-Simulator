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
        /*
        * Class Bank containing array of account objects
        */
        class Bank
        {
            private Account[] ac = new Account[3];

            /*
             * This function initialises the 3 accounts
             */
            public Bank()
            {
                ac[0] = new Account(300, 1111, 111111, "Ananya");
                ac[1] = new Account(750, 2222, 222222, "Josh");
                ac[2] = new Account(3000, 3333, 333333, "Kae");
            }

            /*
             * Check if account number exists in Bank
             *
             * Returns:
             * Account object if that account number that exists
             * null if they do not
             */
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
        /*
         * The Account class encapusulates all features of a simple bank account
         */
        class Account
        {
            //attributes for account
            private int balance;
            private int pin;
            private int accountNum;
            private string accountName;

            //constructor taking initial values for each attribute
            public Account(int balance, int pin, int accountNum, string accountName)
            {
                this.balance = balance;
                this.pin = pin;
                this.accountNum = accountNum;
                this.accountName = accountName;
            }

            //getter and setter functions for balance
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

            /*
             *   This funciton allows us to decrement the balance of an account
             *   it performs a simple check to ensure the balance is greater than
             *   the amount being withdrawn
             *   
             *   Returns:
             *   true if the transactions if possible
             *   false if there are insufficent funds in the account
             */
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

            /*
             * This function check the account pin against the argument passed to it
             *
             * Returns:
             * true if they match
             * false if they do not
             */
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

        /*
         * Creation of a new thread to handle separate ATMs
         */
        private void btnNewATM_Click_1(object sender, EventArgs e)
        {
            Thread threadATM = new Thread(new ThreadStart(newFormATM));
            threadATM.Start();
        }

        /*
         * Creation of a new form for the newly made thread
         */
        private void newFormATM()
        {
            WelcomeScreen newATMForm = new WelcomeScreen(); // stores instance of new ATM form
            newATMForm.ShowDialog();
        }

        /*
         * Menu display after account num and PIN are valid - shows balance
         * and other relevant details and option to withdraw cash and logout
         */
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
            //lblAccountNum.SetBounds(this.ClientSize.Width / 2 - 250, this.ClientSize.Height / 2 - 150, 150, 25);
            //lblBalance.SetBounds(this.ClientSize.Width / 2 - 250, this.ClientSize.Height / 2 - 125, 150, 25);
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

        /* 
         * Logout of account menu and return to main menu
         */
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
                btnAmounts[i].SetBounds(this.ClientSize.Width / 2 - 225 + (i * 100), this.ClientSize.Height / 2 + 75, 50, 50);
            }
            btnAmounts[0].Click += new EventHandler((s, ev) => btnWithdrawAmount_Click(s, ev, btnAmounts[0].Text));
            btnAmounts[1].Click += new EventHandler((s, ev) => btnWithdrawAmount_Click(s, ev, btnAmounts[1].Text));
            btnAmounts[2].Click += new EventHandler((s, ev) => btnWithdrawAmount_Click(s, ev, btnAmounts[2].Text));
            btnAmounts[3].Click += new EventHandler((s, ev) => btnWithdrawAmount_Click(s, ev, btnAmounts[3].Text));
            btnAmounts[4].Click += new EventHandler((s, ev) => btnWithdrawAmount_Click(s, ev, btnAmounts[4].Text));

            btnCustomAmount.SetBounds(this.ClientSize.Width / 2 - 40, this.ClientSize.Height / 2 + 125, 75, 50);
            btnCustomAmount.Click += new EventHandler((s, ev) => btnWithdrawAmount_Click(s, ev, ""));
            btnCustomAmount.Text = "Custom Amount";

            foreach (var btn in btnAmounts)
            {
                Controls.Add(btn);
            }
            Controls.Add(btnCustomAmount);
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

        /* 
         * Clears the form, allowing for switching between screens
         * (Kayee: This function has been taken and reused from previous assignment
         * being the Grid Game)
         */
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
