namespace DailyBreadUtil
{
    partial class frmDBValidator
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
            this.txtChar = new System.Windows.Forms.TextBox();
            this.btnCharReplace = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtChar
            // 
            this.txtChar.Location = new System.Drawing.Point(12, 12);
            this.txtChar.Name = "txtChar";
            this.txtChar.Size = new System.Drawing.Size(84, 20);
            this.txtChar.TabIndex = 0;
            // 
            // btnCharReplace
            // 
            this.btnCharReplace.Location = new System.Drawing.Point(102, 12);
            this.btnCharReplace.Name = "btnCharReplace";
            this.btnCharReplace.Size = new System.Drawing.Size(99, 20);
            this.btnCharReplace.TabIndex = 1;
            this.btnCharReplace.Text = "Replace";
            this.btnCharReplace.UseVisualStyleBackColor = true;
            this.btnCharReplace.Click += new System.EventHandler(this.btnCharReplace_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(226, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Location:";
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(296, 16);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(0, 13);
            this.lblLocation.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(223, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "You can replace unknown symbol with \'$\' (36)";
            // 
            // frmDBValidator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 71);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCharReplace);
            this.Controls.Add(this.txtChar);
            this.Name = "frmDBValidator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Daily Bread Validator";
            this.Load += new System.EventHandler(this.frmDBValidator_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtChar;
        private System.Windows.Forms.Button btnCharReplace;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label label2;
    }
}