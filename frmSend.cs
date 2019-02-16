/*
 *  "gk.MonitorAndCopy" - The utility for copying new files by SMB protocol 
 *
 *  Copyright (C) 2015-2019  Igor Tyulyakov aka g10101k, g101k. Contacts: <g101k@mail.ru>
 *  
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *       http://www.apache.org/licenses/LICENSE-2.0
 *
 *   Unless required by applicable law or agreed to in writing, software
 *   distributed under the License is distributed on an "AS IS" BASIS,
 *   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *   See the License for the specific language governing permissions and
 *   limitations under the License.
 */
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
