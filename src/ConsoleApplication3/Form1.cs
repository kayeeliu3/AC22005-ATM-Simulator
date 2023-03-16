﻿/*
 * TEAM 2: Joshua Price (2481545), Ananya Bhatt (2462696), Kayee Liu (2454633)
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConsoleApplication3
{
    public partial class Form1 : Form
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
                ac[0] = new Account(300, 1111, 111111);
                ac[1] = new Account(750, 2222, 222222);
                ac[2] = new Account(3000, 3333, 333333);
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

            //constructor taking initial values for each attribute
            public Account(int balance, int pin, int accountNum)
            {
                this.balance = balance;
                this.pin = pin;
                this.accountNum = accountNum;
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
        Form2 ATMForm = new Form2(); // stores instance of new ATM form

        TextBox tbAccNum = new TextBox(); // stores account number text
        TextBox tbPin = new TextBox(); // stores PIN text

        public Form1()
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
            Label lblInstruct = new Label();
            Button btnSubmit = new Button();
            Button btnNewATM = new Button();

            lblInstruct.SetBounds(this.ClientSize.Width / 2 - 75, this.ClientSize.Height / 2 - 100, 150, 50);
            tbAccNum.SetBounds(this.ClientSize.Width / 2 - 75, this.ClientSize.Height / 2 - 50, 150, 50);
            tbPin.SetBounds(this.ClientSize.Width / 2 - 75, this.ClientSize.Height / 2, 150, 50);
            btnSubmit.SetBounds(this.ClientSize.Width / 2 - 50, this.ClientSize.Height / 2 + 50, 100, 50);
            btnNewATM.SetBounds(this.ClientSize.Width / 2 + 150, this.ClientSize.Height / 2 + 100, 100, 50);
            lblInstruct.Text = "Please enter your Account Number and PIN.";
            tbAccNum.Text = "Account Number";
            tbPin.Text = "PIN";
            btnSubmit.Text = "Submit";
            btnNewATM.Text = "New ATM";

            btnNewATM.Click += new EventHandler(this.btnNewATM_Click);
            btnSubmit.Click += new EventHandler(this.btnSubmit_Click);
            Controls.Add(lblInstruct);
            Controls.Add(tbAccNum);
            Controls.Add(tbPin);
            Controls.Add(btnSubmit);
            Controls.Add(btnNewATM);
        }

        /*
         * Check if account details and PIN are valid and match.
         * Shows error message if invalid.
         */
        private void btnSubmit_Click(object sender, EventArgs e)
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
                            MessageBox.Show("Valid! Do actual ATM stuff after this.", "Confirmation", MessageBoxButtons.OK);
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
         * Instantiate a new Form that simulates a new ATM
         */
        private void btnNewATM_Click(object sender, EventArgs e)
        {
            ATMForm.Show();
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