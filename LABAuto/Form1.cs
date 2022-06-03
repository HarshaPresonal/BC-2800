using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LABAuto
{
   
    public partial class Form1 : Form
    {
        ErrorLog errorLog;
        public Form1()
        {
           
            InitializeComponent();
            errorLog = new ErrorLog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
           
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Assembly.LoadFrom(Path.GetDirectoryName(Application.ExecutablePath) + "\\Assm\\System.Data.SQLite.dll");
            try
            {
                LoginForm lf = new LoginForm();
                CommonMethods objcom = new CommonMethods();
                bool Isopen = objcom.CheckOpened("LoginForm");
                if (Isopen)
                {
                    lf.Show();
                }
            }
            catch (Exception ex)
            {
                errorLog.ExecptionHandler(ex);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void serialPortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SerialPort sp = new SerialPort();
                CommonMethods objcom = new CommonMethods();
                bool Isopen = objcom.CheckOpened("SerialPort");
                if (Isopen)
                {
                    sp.Show();
                }
            }
            catch (Exception ex)
            {
                errorLog.ExecptionHandler(ex);
            }
        }

        private void outPutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Output op = new Output();
                CommonMethods objcom = new CommonMethods();
                bool Isopen = objcom.CheckOpened("Output");
                if (Isopen)
                {
                    op.Show();
                }
            }
            catch (Exception ex)
            {
                errorLog.ExecptionHandler(ex);
            }
        }

        private void tCPIPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TCPIP tp = new TCPIP();
                CommonMethods objCom = new CommonMethods();
                bool isopen = objCom.CheckOpened("TCPIP");
                if (isopen)
                {
                    tp.Show();
                }
            }
            catch (Exception ex)
            {
                errorLog.ExecptionHandler(ex);
            }
        }

        private void serverPortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ServerPortForm sp = new ServerPortForm();
                CommonMethods objcom = new CommonMethods();
                bool Isopen = objcom.CheckOpened("ServerPortForm");
                sp.Show();
            }
            catch (Exception ex)
            {
                errorLog.ExecptionHandler(ex);
            }
        }
    }
}
