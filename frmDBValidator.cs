using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DailyBreadUtil
{
    public partial class frmDBValidator : Form
    { 
        public frmDBValidator()
        {
            InitializeComponent();
        }

        private void btnCharReplace_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDBValidator_Load(object sender, EventArgs e)
        {

        }

        public int RaplaceChar(char c, int l)
        {
            txtChar.Text = ((int)c).ToString();
            lblLocation.Text = l.ToString();
            
            this.ShowDialog();
            
            return int.Parse(txtChar.Text.Trim());
        }
    }
}