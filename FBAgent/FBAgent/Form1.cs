using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace FBAgent
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //throw new NotImplementedException();
            Process[] ies = Process.GetProcessesByName("iexplore");

            foreach (Process ie in ies)
            {
                try
                {
                    ie.Kill();
                }
                catch { }
            }
        }

        private void chkScroll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            numScroll.Visible = cb.Checked;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Browser brw = new Browser();
            brw.Navigate("www.facebook.com");
            brw.DocUpdated += new Browser.DocUpdatedEventHandler(brw_DocUpdated);
        }

        void brw_DocUpdated()
        {
            //throw new NotImplementedException();
            Debug.Print("Doc Updated");
        }
    }
}
