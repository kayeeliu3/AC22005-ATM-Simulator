namespace ConsoleApplication3
{
    partial class WelcomeScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbAccNum = new System.Windows.Forms.TextBox();
            this.tbPin = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnNewATM = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbAccNum
            // 
            this.tbAccNum.Location = new System.Drawing.Point(507, 243);
            this.tbAccNum.Name = "tbAccNum";
            this.tbAccNum.Size = new System.Drawing.Size(314, 26);
            this.tbAccNum.TabIndex = 0;
            // 
            // tbPin
            // 
            this.tbPin.Location = new System.Drawing.Point(507, 357);
            this.tbPin.Name = "tbPin";
            this.tbPin.Size = new System.Drawing.Size(314, 26);
            this.tbPin.TabIndex = 1;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(546, 476);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(235, 53);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click_1);
            // 
            // btnNewATM
            // 
            this.btnNewATM.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnNewATM.Location = new System.Drawing.Point(133, 476);
            this.btnNewATM.Name = "btnNewATM";
            this.btnNewATM.Size = new System.Drawing.Size(226, 53);
            this.btnNewATM.TabIndex = 3;
            this.btnNewATM.Text = "New ATM Screen";
            this.btnNewATM.UseVisualStyleBackColor = true;
            this.btnNewATM.Click += new System.EventHandler(this.btnNewATM_Click_1);
            // 
            // WelcomeScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImage = global::ConsoleApplication3.Properties.Resources.MainMenu;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(900, 562);
            this.Controls.Add(this.btnNewATM);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.tbPin);
            this.Controls.Add(this.tbAccNum);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "WelcomeScreen";
            this.Text = "ATM";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbAccNum;
        private System.Windows.Forms.TextBox tbPin;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnNewATM;
    }
}