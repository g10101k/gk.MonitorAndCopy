using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gk.MonitorAndCopy
{
    public partial class frmSend : Form
    {
        Timer t = new Timer();
        int sec = 60;
        string frmt = "Отправить в ЛИМС ({0})";

        public frmSend()
        {
            this.Visible = false;
            InitializeComponent();
            TimerActive(1000);
        }

        public frmSend(int waitSec)
        {
            this.Visible = false;
            sec = waitSec;
            InitializeComponent();
            TimerActive(1000);
        }

        void TimerActive(int ws)
        {
            T_Tick(null, null);
            t.Interval = ws;
            t.Tick += T_Tick;
            t.Start();

        }

        private void T_Tick(object sender, EventArgs e)
        {
            if (sec > 0)
            {
                sec--;
                btnOk.Text = string.Format(frmt, sec);                
            }
            else
            {
                button1_Click(null, null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
