using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace DailyBreadUtil
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmDBMain : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox txtDBFile;
		private System.Windows.Forms.Label lblDBFile;
		private System.Windows.Forms.Button btnOpenDBFile;
		private System.Windows.Forms.OpenFileDialog openDBFile;
		private System.Windows.Forms.Button btnOpenFile;
		private System.Windows.Forms.Label lblDebug1;
		private System.Windows.Forms.Label lblDebug2;
        private GroupBox groupBox1;
        private Panel panel1;
        private CheckBox chkJan;
        private CheckBox chkFeb;
        private CheckBox chkDec;
        private CheckBox chkWinter;
        private TextBox txtYear;
        private Label label1;
        private Panel panel2;
        private CheckBox chkApr;
        private CheckBox chkMay;
        private CheckBox chkMar;
        private CheckBox chkSpring;
        private Panel panel3;
        private CheckBox chkJul;
        private CheckBox chkAug;
        private CheckBox chkJun;
        private CheckBox chkSummer;
        private Panel panel4;
        private CheckBox chkOct;
        private CheckBox chkNov;
        private CheckBox chkSep;
        private CheckBox chkFall;

        private string dbFileName;
        private int expDBNumber = 0;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmDBMain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.txtDBFile = new System.Windows.Forms.TextBox();
            this.lblDBFile = new System.Windows.Forms.Label();
            this.btnOpenDBFile = new System.Windows.Forms.Button();
            this.openDBFile = new System.Windows.Forms.OpenFileDialog();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.lblDebug1 = new System.Windows.Forms.Label();
            this.lblDebug2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkApr = new System.Windows.Forms.CheckBox();
            this.chkMay = new System.Windows.Forms.CheckBox();
            this.chkMar = new System.Windows.Forms.CheckBox();
            this.chkSpring = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkJul = new System.Windows.Forms.CheckBox();
            this.chkAug = new System.Windows.Forms.CheckBox();
            this.chkJun = new System.Windows.Forms.CheckBox();
            this.chkSummer = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.chkOct = new System.Windows.Forms.CheckBox();
            this.chkNov = new System.Windows.Forms.CheckBox();
            this.chkSep = new System.Windows.Forms.CheckBox();
            this.chkFall = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkJan = new System.Windows.Forms.CheckBox();
            this.chkFeb = new System.Windows.Forms.CheckBox();
            this.chkDec = new System.Windows.Forms.CheckBox();
            this.chkWinter = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDBFile
            // 
            this.txtDBFile.Enabled = false;
            this.txtDBFile.Location = new System.Drawing.Point(98, 13);
            this.txtDBFile.Name = "txtDBFile";
            this.txtDBFile.Size = new System.Drawing.Size(272, 20);
            this.txtDBFile.TabIndex = 0;
            this.txtDBFile.TextChanged += new System.EventHandler(this.txtDBFile_TextChanged);
            // 
            // lblDBFile
            // 
            this.lblDBFile.Location = new System.Drawing.Point(8, 16);
            this.lblDBFile.Name = "lblDBFile";
            this.lblDBFile.Size = new System.Drawing.Size(88, 16);
            this.lblDBFile.TabIndex = 1;
            this.lblDBFile.Text = "Daily Bread File:";
            // 
            // btnOpenDBFile
            // 
            this.btnOpenDBFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenDBFile.Location = new System.Drawing.Point(376, 12);
            this.btnOpenDBFile.Name = "btnOpenDBFile";
            this.btnOpenDBFile.Size = new System.Drawing.Size(32, 24);
            this.btnOpenDBFile.TabIndex = 2;
            this.btnOpenDBFile.Text = "...";
            this.btnOpenDBFile.Click += new System.EventHandler(this.btnOpenDBFile_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Enabled = false;
            this.btnOpenFile.Location = new System.Drawing.Point(414, 12);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(72, 24);
            this.btnOpenFile.TabIndex = 3;
            this.btnOpenFile.Text = "Open";
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // lblDebug1
            // 
            this.lblDebug1.Location = new System.Drawing.Point(16, 48);
            this.lblDebug1.Name = "lblDebug1";
            this.lblDebug1.Size = new System.Drawing.Size(216, 24);
            this.lblDebug1.TabIndex = 4;
            // 
            // lblDebug2
            // 
            this.lblDebug2.Location = new System.Drawing.Point(16, 80);
            this.lblDebug2.Name = "lblDebug2";
            this.lblDebug2.Size = new System.Drawing.Size(216, 24);
            this.lblDebug2.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtYear);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.panel4);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(11, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(475, 170);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Daily Bread Period";
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(129, 17);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(65, 20);
            this.txtYear.TabIndex = 6;
            this.txtYear.TextChanged += new System.EventHandler(this.txtYear_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Year of the First Month";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.chkApr);
            this.panel2.Controls.Add(this.chkMay);
            this.panel2.Controls.Add(this.chkMar);
            this.panel2.Controls.Add(this.chkSpring);
            this.panel2.Location = new System.Drawing.Point(123, 52);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(108, 109);
            this.panel2.TabIndex = 4;
            // 
            // chkApr
            // 
            this.chkApr.AutoSize = true;
            this.chkApr.Location = new System.Drawing.Point(21, 56);
            this.chkApr.Name = "chkApr";
            this.chkApr.Size = new System.Drawing.Size(46, 17);
            this.chkApr.TabIndex = 3;
            this.chkApr.Text = "April";
            this.chkApr.UseVisualStyleBackColor = true;
            this.chkApr.CheckedChanged += new System.EventHandler(this.chkApr_CheckedChanged);
            // 
            // chkMay
            // 
            this.chkMay.AutoSize = true;
            this.chkMay.Location = new System.Drawing.Point(21, 79);
            this.chkMay.Name = "chkMay";
            this.chkMay.Size = new System.Drawing.Size(46, 17);
            this.chkMay.TabIndex = 2;
            this.chkMay.Text = "May";
            this.chkMay.UseVisualStyleBackColor = true;
            this.chkMay.CheckedChanged += new System.EventHandler(this.chkMay_CheckedChanged);
            // 
            // chkMar
            // 
            this.chkMar.AutoSize = true;
            this.chkMar.Location = new System.Drawing.Point(21, 33);
            this.chkMar.Name = "chkMar";
            this.chkMar.Size = new System.Drawing.Size(56, 17);
            this.chkMar.TabIndex = 1;
            this.chkMar.Text = "March";
            this.chkMar.UseVisualStyleBackColor = true;
            this.chkMar.CheckedChanged += new System.EventHandler(this.chkMar_CheckedChanged);
            // 
            // chkSpring
            // 
            this.chkSpring.AutoSize = true;
            this.chkSpring.Location = new System.Drawing.Point(3, 6);
            this.chkSpring.Name = "chkSpring";
            this.chkSpring.Size = new System.Drawing.Size(56, 17);
            this.chkSpring.TabIndex = 0;
            this.chkSpring.Text = "Spring";
            this.chkSpring.UseVisualStyleBackColor = true;
            this.chkSpring.CheckedChanged += new System.EventHandler(this.chkSpring_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.chkJul);
            this.panel3.Controls.Add(this.chkAug);
            this.panel3.Controls.Add(this.chkJun);
            this.panel3.Controls.Add(this.chkSummer);
            this.panel3.Location = new System.Drawing.Point(237, 52);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(108, 109);
            this.panel3.TabIndex = 4;
            // 
            // chkJul
            // 
            this.chkJul.AutoSize = true;
            this.chkJul.Location = new System.Drawing.Point(21, 56);
            this.chkJul.Name = "chkJul";
            this.chkJul.Size = new System.Drawing.Size(44, 17);
            this.chkJul.TabIndex = 3;
            this.chkJul.Text = "July";
            this.chkJul.UseVisualStyleBackColor = true;
            this.chkJul.CheckedChanged += new System.EventHandler(this.chkJul_CheckedChanged);
            // 
            // chkAug
            // 
            this.chkAug.AutoSize = true;
            this.chkAug.Location = new System.Drawing.Point(21, 79);
            this.chkAug.Name = "chkAug";
            this.chkAug.Size = new System.Drawing.Size(59, 17);
            this.chkAug.TabIndex = 2;
            this.chkAug.Text = "August";
            this.chkAug.UseVisualStyleBackColor = true;
            this.chkAug.CheckedChanged += new System.EventHandler(this.chkAug_CheckedChanged);
            // 
            // chkJun
            // 
            this.chkJun.AutoSize = true;
            this.chkJun.Location = new System.Drawing.Point(21, 33);
            this.chkJun.Name = "chkJun";
            this.chkJun.Size = new System.Drawing.Size(49, 17);
            this.chkJun.TabIndex = 1;
            this.chkJun.Text = "June";
            this.chkJun.UseVisualStyleBackColor = true;
            this.chkJun.CheckedChanged += new System.EventHandler(this.chkJun_CheckedChanged);
            // 
            // chkSummer
            // 
            this.chkSummer.AutoSize = true;
            this.chkSummer.Location = new System.Drawing.Point(3, 6);
            this.chkSummer.Name = "chkSummer";
            this.chkSummer.Size = new System.Drawing.Size(64, 17);
            this.chkSummer.TabIndex = 0;
            this.chkSummer.Text = "Summer";
            this.chkSummer.UseVisualStyleBackColor = true;
            this.chkSummer.CheckedChanged += new System.EventHandler(this.chkSummer_CheckedChanged);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.chkOct);
            this.panel4.Controls.Add(this.chkNov);
            this.panel4.Controls.Add(this.chkSep);
            this.panel4.Controls.Add(this.chkFall);
            this.panel4.Location = new System.Drawing.Point(351, 52);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(108, 109);
            this.panel4.TabIndex = 4;
            // 
            // chkOct
            // 
            this.chkOct.AutoSize = true;
            this.chkOct.Location = new System.Drawing.Point(21, 56);
            this.chkOct.Name = "chkOct";
            this.chkOct.Size = new System.Drawing.Size(64, 17);
            this.chkOct.TabIndex = 3;
            this.chkOct.Text = "October";
            this.chkOct.UseVisualStyleBackColor = true;
            this.chkOct.CheckedChanged += new System.EventHandler(this.chkOct_CheckedChanged);
            // 
            // chkNov
            // 
            this.chkNov.AutoSize = true;
            this.chkNov.Location = new System.Drawing.Point(21, 79);
            this.chkNov.Name = "chkNov";
            this.chkNov.Size = new System.Drawing.Size(78, 17);
            this.chkNov.TabIndex = 2;
            this.chkNov.Text = "Novermber";
            this.chkNov.UseVisualStyleBackColor = true;
            this.chkNov.CheckedChanged += new System.EventHandler(this.chkNov_CheckedChanged);
            // 
            // chkSep
            // 
            this.chkSep.AutoSize = true;
            this.chkSep.Location = new System.Drawing.Point(21, 33);
            this.chkSep.Name = "chkSep";
            this.chkSep.Size = new System.Drawing.Size(77, 17);
            this.chkSep.TabIndex = 1;
            this.chkSep.Text = "September";
            this.chkSep.UseVisualStyleBackColor = true;
            this.chkSep.CheckedChanged += new System.EventHandler(this.chkSep_CheckedChanged);
            // 
            // chkFall
            // 
            this.chkFall.AutoSize = true;
            this.chkFall.Location = new System.Drawing.Point(3, 6);
            this.chkFall.Name = "chkFall";
            this.chkFall.Size = new System.Drawing.Size(42, 17);
            this.chkFall.TabIndex = 0;
            this.chkFall.Text = "Fall";
            this.chkFall.UseVisualStyleBackColor = true;
            this.chkFall.CheckedChanged += new System.EventHandler(this.chkFall_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.chkJan);
            this.panel1.Controls.Add(this.chkFeb);
            this.panel1.Controls.Add(this.chkDec);
            this.panel1.Controls.Add(this.chkWinter);
            this.panel1.Location = new System.Drawing.Point(9, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(108, 109);
            this.panel1.TabIndex = 1;
            // 
            // chkJan
            // 
            this.chkJan.AutoSize = true;
            this.chkJan.Location = new System.Drawing.Point(21, 56);
            this.chkJan.Name = "chkJan";
            this.chkJan.Size = new System.Drawing.Size(63, 17);
            this.chkJan.TabIndex = 3;
            this.chkJan.Text = "January";
            this.chkJan.UseVisualStyleBackColor = true;
            this.chkJan.CheckedChanged += new System.EventHandler(this.chkJan_CheckedChanged);
            // 
            // chkFeb
            // 
            this.chkFeb.AutoSize = true;
            this.chkFeb.Location = new System.Drawing.Point(21, 79);
            this.chkFeb.Name = "chkFeb";
            this.chkFeb.Size = new System.Drawing.Size(67, 17);
            this.chkFeb.TabIndex = 2;
            this.chkFeb.Text = "February";
            this.chkFeb.UseVisualStyleBackColor = true;
            this.chkFeb.CheckedChanged += new System.EventHandler(this.chkFeb_CheckedChanged);
            // 
            // chkDec
            // 
            this.chkDec.AutoSize = true;
            this.chkDec.Location = new System.Drawing.Point(21, 33);
            this.chkDec.Name = "chkDec";
            this.chkDec.Size = new System.Drawing.Size(75, 17);
            this.chkDec.TabIndex = 1;
            this.chkDec.Text = "December";
            this.chkDec.UseVisualStyleBackColor = true;
            this.chkDec.CheckedChanged += new System.EventHandler(this.chkDec_CheckedChanged);
            // 
            // chkWinter
            // 
            this.chkWinter.AutoSize = true;
            this.chkWinter.Location = new System.Drawing.Point(3, 6);
            this.chkWinter.Name = "chkWinter";
            this.chkWinter.Size = new System.Drawing.Size(57, 17);
            this.chkWinter.TabIndex = 0;
            this.chkWinter.Text = "Winter";
            this.chkWinter.UseVisualStyleBackColor = true;
            this.chkWinter.CheckedChanged += new System.EventHandler(this.chkWinter_CheckedChanged);
            // 
            // frmDBMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(500, 222);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblDebug2);
            this.Controls.Add(this.lblDebug1);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.btnOpenDBFile);
            this.Controls.Add(this.lblDBFile);
            this.Controls.Add(this.txtDBFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmDBMain";
            this.Text = "Daily Bread Utilities";
            this.TransparencyKey = System.Drawing.Color.Gray;
            this.Load += new System.EventHandler(this.frmDBMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmDBMain());
		}

		private void frmDBMain_Load(object sender, System.EventArgs e)
		{
            txtYear.Text = DateTime.Now.Year.ToString();
		}

		private void btnOpenDBFile_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog openDBFile = new OpenFileDialog ();

			//openDBFile.InitialDirectory = "C:\\Documents and Settings\\ilya\\My Documents\\Visual Studio 2005\\Projects\\DailyBreadUtil\\bin\\Debug";
            openDBFile.InitialDirectory = "G:\\DailyBread";
			openDBFile.Filter = "UBF Daily Bread Files (*.udb)|*.udb|All Files (*.*)|*.*|XML Daily Bread Files (*.xml)|*.xml|Text Files (*.txt)|*.txt";
			openDBFile.FilterIndex = 2;
			openDBFile.RestoreDirectory = true;

			if (openDBFile.ShowDialog () == DialogResult.OK)
			{
				if ((openDBFile.FileName) != null)
				{
					txtDBFile.Text = openDBFile.FileName.Substring(openDBFile.FileName.LastIndexOf('\\') + 1);
                    dbFileName = openDBFile.FileName.ToString().Trim();
				}
                openDBFile.Dispose();
			}

            openDBFile.Dispose();
		}

		private void btnOpenFile_Click(object sender, System.EventArgs e)
		{
            parsing_util dbParser = new parsing_util(expDBNumber);

            string[] filePages = dbParser.SplitFileByPage(dbFileName);
			
            daily_bread CurrentDailyBread = new daily_bread ();

            int pageCount = dbParser.GetPageCount;

            for (int index = 0; index <= pageCount; index++)
            {
                if (index == 45)
                {
                    CurrentDailyBread.ParseDB(filePages[index]);
                }
                else
                    CurrentDailyBread.ParseDB(filePages[index]);
            }

			lblDebug1.Text = ("Number of Pages: " + filePages.Length);
			lblDebug2.Text = ("DB Count: " + CurrentDailyBread.GetDB_Count ());

            CurrentDailyBread.UpdateDB(int.Parse(txtYear.Text.Trim()));
            CurrentDailyBread.MakeDBIndex(txtYear.Text.Trim());
			
            //CurrentDailyBread.MakeXMLTemplate();

            CurrentDailyBread.MakeDBFiles();
            CurrentDailyBread.MakeXMLTemplate(txtYear.Text);
            CurrentDailyBread.MakeRSSFiles(1);
            CurrentDailyBread.MakeRSSFiles(2);

            CurrentDailyBread.MakeTestPages();

            CurrentDailyBread.MakeXMLFile_v3();
		}

        private void chkWinter_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWinter.Checked)
            {
                chkDec.Checked = true;
                chkJan.Checked = true;
                chkFeb.Checked = true;
            }
            else
            {
                chkDec.Checked = false;
                chkJan.Checked = false;
                chkFeb.Checked = false;
            }
        }

        private void chkSpring_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSpring.Checked)
            {
                chkMar.Checked = true;
                chkApr.Checked = true;
                chkMay.Checked = true;
            }
            else
            {
                chkMar.Checked = false;
                chkApr.Checked = false;
                chkMay.Checked = false;
            }

        }

        private void chkSummer_CheckedChanged(object sender, EventArgs e)
        {
            chkJun.Checked = (chkSummer.Checked == true) ? true : false;
            chkJul.Checked = (chkSummer.Checked == true) ? true : false;
            chkAug.Checked = (chkSummer.Checked == true) ? true : false;
        }

        private void chkFall_CheckedChanged(object sender, EventArgs e)
        {
            chkSep.Checked = (chkFall.Checked == true) ? true : false;
            chkOct.Checked = (chkFall.Checked == true) ? true : false;
            chkNov.Checked = (chkFall.Checked == true) ? true : false;
        }

        private void txtDBFile_TextChanged(object sender, EventArgs e)
        {
            CheckEnable();
        }

        private void txtYear_TextChanged(object sender, EventArgs e)
        {
            CheckEnable();
        }

        private void CheckEnable()
        {
            int i;

            int.TryParse(txtYear.Text.ToString(), out i);

            if (txtDBFile.Text.ToString().Trim() != "" && i > 0)
                btnOpenFile.Enabled = true;
            else
                btnOpenFile.Enabled = false;
        }

        private void chkDec_CheckedChanged(object sender, EventArgs e)
        {
            expDBNumber = chkDec.Checked == true ? expDBNumber + 31 : expDBNumber - 31;
        }

        private void chkJan_CheckedChanged(object sender, EventArgs e)
        {
            expDBNumber = chkJan.Checked == true ? expDBNumber + 31 : expDBNumber - 31;
        }

        private void chkFeb_CheckedChanged(object sender, EventArgs e)
        {
            if ((int.Parse(txtYear.Text)) % 4 == 0)
                expDBNumber = chkFeb.Checked == true ? expDBNumber + 29 : expDBNumber - 29;
            else
                expDBNumber = chkFeb.Checked == true ? expDBNumber + 28 : expDBNumber - 28;
        }

        private void chkMar_CheckedChanged(object sender, EventArgs e)
        {
            expDBNumber = chkMar.Checked == true ? expDBNumber + 31 : expDBNumber - 31;
        }

        private void chkApr_CheckedChanged(object sender, EventArgs e)
        {
            expDBNumber = chkApr.Checked == true ? expDBNumber + 30 : expDBNumber - 30;
        }

        private void chkMay_CheckedChanged(object sender, EventArgs e)
        {
            expDBNumber = chkMay.Checked == true ? expDBNumber + 31 : expDBNumber - 31;
        }

        private void chkJun_CheckedChanged(object sender, EventArgs e)
        {
            expDBNumber = chkJun.Checked == true ? expDBNumber + 30 : expDBNumber - 30;
        }

        private void chkJul_CheckedChanged(object sender, EventArgs e)
        {
            expDBNumber = chkJul.Checked == true ? expDBNumber + 31 : expDBNumber - 31;
        }

        private void chkAug_CheckedChanged(object sender, EventArgs e)
        {
            expDBNumber = chkAug.Checked == true ? expDBNumber + 31 : expDBNumber - 31;
        }

        private void chkSep_CheckedChanged(object sender, EventArgs e)
        {
            expDBNumber = chkSep.Checked == true ? expDBNumber + 30 : expDBNumber - 30;
        }

        private void chkOct_CheckedChanged(object sender, EventArgs e)
        {
            expDBNumber = chkOct.Checked == true ? expDBNumber + 31 : expDBNumber - 31;
        }

        private void chkNov_CheckedChanged(object sender, EventArgs e)
        {
            expDBNumber = chkNov.Checked == true ? expDBNumber + 30 : expDBNumber - 30;
        }
	}
}
