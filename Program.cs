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
using System.Windows.Forms;
using System.Threading;
using System.Configuration;
using System.IO;

using System.Collections.Specialized;

namespace gk.MonitorAndCopy
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {


                string sleepstr = ConfigurationManager.AppSettings["Sleep"];
                string MonPath = ConfigurationManager.AppSettings["MonPath"];
                string DestPath = ConfigurationManager.AppSettings["DestPath"];
                string BakPath = ConfigurationManager.AppSettings["BakPath"];
                string mask = ConfigurationManager.AppSettings["mask"];
                int WaitSec = Convert.ToInt32(ConfigurationManager.AppSettings["WaitSec"]);
                if (!Directory.Exists(MonPath))
                {
                    MessageBox.Show(MonPath + " - Не существует MonPath! \r\n Выход из программы");
                    return;
                }
                if (!Directory.Exists(DestPath))
                {
                    MessageBox.Show(DestPath + " - Не существует DestPath! \r\n Выход из программы");
                    return;
                }
                if (!Directory.Exists(BakPath))
                {
                    MessageBox.Show(BakPath + " - Не существует BakPath! \r\n Выход из программы");
                    return;
                }

                while (true)
                {
                    string[] allFoundFiles = Directory.GetFiles(MonPath, mask, SearchOption.AllDirectories);
                    foreach (string f in allFoundFiles)
                    {
                        System.IO.FileInfo fi = new FileInfo(f);
                        if (new frmSend(WaitSec).ShowDialog() == DialogResult.OK) {
                            string destPathName = DestPath + fi.Name;
                            if (File.Exists(destPathName))
                                destPathName = destPathName.Replace(fi.Extension, "") + Guid.NewGuid().ToString() + "." + fi.Extension;
                            File.Move(f, destPathName);
                        }
                        else {
                            string destPathName = BakPath + fi.Name;
                            if (File.Exists(destPathName))
                                destPathName = destPathName.Replace(fi.Extension, "") + Guid.NewGuid().ToString() + "." + fi.Extension;
                            File.Move(f, destPathName);
                        }
                    }
                    int sleep = Convert.ToInt32(sleepstr);
                    Thread.Sleep(sleep);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+ "\r\n" +ex.StackTrace);
            }

        }

        static void GetFiles(string path, string mask)
        {

        }
    }
}
