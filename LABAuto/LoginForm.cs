using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace LABAuto
{
    public partial class LoginForm : Form
    {
        ErrorLog error; 
        public string DBpath = Path.GetDirectoryName(Application.ExecutablePath) + "\\DB\\EzovionMI.sqlite";
        public LoginForm()
        {
            InitializeComponent();
            error = new ErrorLog();
            LoadLoginName();
            
        }

        public void LoadLoginName()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + DBpath + ""))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("Select * from Config where ConfigName='Auth'", conn))
                    {
                        DataTable dt = new DataTable();
                        SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                        da.Fill(dt);

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            comboBox1.Items.Add(dt.Rows[i]["ConfigValue"].ToString());
                        }

                    }
                }
            }
            catch(Exception ex)
            {
                error.ExecptionHandler(ex);
            }            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConnectionPage cn = new ConnectionPage();
            cn.Show();
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //string DBpath = System.IO.Path.GetFullPath("~/DB/EzovionMI.sqlite");
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + DBpath + ""))
                {
                    using (SQLiteCommand Cmd = new SQLiteCommand("Select * from Config where ConfigName='Auth' and ConfigValue = '" + comboBox1.Text + "' and ConfigValue2 ='" + textBox1.Text.ToString() + "'", conn))
                    {
                        DataTable dt = new DataTable();
                        SQLiteDataAdapter da = new SQLiteDataAdapter(Cmd);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            ConnectionPage cn = new ConnectionPage();
                            cn.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Login !!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                error.ExecptionHandler(ex);
            }
        }
    }
}
