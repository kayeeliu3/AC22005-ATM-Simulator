namespace ConsoleApplication3
{
    partial class ATMForm
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
            this.btnSemaphore = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
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
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnSemaphore
            // 
            this.btnSemaphore.BackColor = System.Drawing.Color.LightGreen;
            this.btnSemaphore.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnSemaphore.Location = new System.Drawing.Point(254, 476);
            this.btnSemaphore.Name = "btnSemaphore";
            this.btnSemaphore.Size = new System.Drawing.Size(129, 53);
            this.btnSemaphore.TabIndex = 3;
            this.btnSemaphore.Text = "On";
            this.btnSemaphore.UseVisualStyleBackColor = false;
            this.btnSemaphore.Click += new System.EventHandler(this.btnSemaphore_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(85, 487);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 26);
            this.label1.TabIndex = 4;
            this.label1.Text = "Semaphore is:";
            // 
            // ATMForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImage = global::ConsoleApplication3.Properties.Resources.Login;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(900, 562);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSemaphore);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.tbPin);
            this.Controls.Add(this.tbAccNum);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "ATMForm";
            this.Text = "ATM";
            this.Load += new System.EventHandler(this.ATMForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbAccNum;
        private System.Windows.Forms.TextBox tbPin;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnSemaphore;
        private System.Windows.Forms.Label label1;
    }
}