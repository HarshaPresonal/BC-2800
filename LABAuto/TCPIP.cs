using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LABAuto
{
    public partial class TCPIP : Form
    {
        CommonMethods objCom = new CommonMethods();
        ErrorLog errorLog;
        public string DBpath = Path.GetDirectoryName(Application.ExecutablePath) + "\\DB\\EzovionMI.sqlite";
        public string S_portvalue = string.Empty;
        public TCPIP()
        {
            InitializeComponent();
            GridBind();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void TCPIP_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            if ((MessageBox.Show("Are you sure delete the record from DB", "", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + DBpath + ""))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("Delete from Config where ConfigName = '" + row.Cells["ConfigName"].Value.ToString() + "'", conn))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Delete Sucessfully !!");
                    }
                    GridBind();
                }
            }

        }

        public bool GridBind()
        {
            bool returnvalue = false;
            try
            {
                dataGridView1.Rows.Clear();
                using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + DBpath + ""))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("Select * from Config", conn))
                    {
                        DataTable dt = new DataTable();
                        SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                dataGridView1.Rows.Add();
                                dataGridView1.AllowUserToAddRows = false;
                                dataGridView1.Rows[i].Cells[0].Value = dt.Rows[i]["ConfigName"].ToString();
                                dataGridView1.Rows[i].Cells[1].Value = dt.Rows[i]["ConfigValue"].ToString();
                                dataGridView1.Rows[i].Cells[2].Value = dt.Rows[i]["IsActive"].ToString();
                                dataGridView1.Rows[i].Cells[3].Value = dt.Rows[i]["ConfigValue2"].ToString();
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                errorLog.ExecptionHandler(ex);
                returnvalue = false;
            }
            return returnvalue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string TCPIp = string.Empty;
            string Port = string.Empty;
            TCPIp = textBox1.Text.ToString();
            Port = textBox2.Text.ToString();


            if (!(Port.Trim().Length <= 0))
            {
                if (InsertSerialComport(TCPIp,Port))
                {
                    MessageBox.Show("Config Details added in DB !!");
                    GridBind();
                }
                else
                {
                    MessageBox.Show("Please check the ErrorLog!!");
                }
            }
            else
            {
                MessageBox.Show("Please enter the server port !!");
            }
        }

        public bool InsertSerialComport(string TCPip,string S_Port)
        {
            bool returnvalue = false;
            try
            {
                if (DBValidation(S_Port))
                {
                    using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + DBpath + ""))
                    {
                        using (SQLiteCommand cmd = new SQLiteCommand("Insert into Config(ConfigName,ConfigValue,ConfigValue2,IsActive,CreatedDate) " +
                            "  Values ('IPConfig','"+ TCPip +"','" + S_Port + "',1,'" + DateTime.Now.ToString() + "')      ", conn))
                        {
                            DataTable dt = new DataTable();
                            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                            da.Fill(dt);
                        }
                    }
                    returnvalue = true;
                }
                else
                {
                    returnvalue = false;
                }
            }
            catch (Exception ex)
            {
                errorLog.ExecptionHandler(ex);
                returnvalue = false;
            }
            return returnvalue;
        }

        public bool DBValidation(string serialport)
        {
            bool returnvalue = true;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + DBpath + ""))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("Select * from Config", conn))
                    {
                        DataTable dt = new DataTable();
                        SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                        da.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i][1].ToString() == "Comport")
                            {
                                MessageBox.Show("Please remove the exisiting Comport details !!");
                                returnvalue = false;
                                break;
                            }
                            else if (dt.Rows[i][1].ToString() == "S_Port" || dt.Rows[i][1].ToString() == "IPConfig")
                            {
                                MessageBox.Show("Please remove the exisiting Connection details !!");
                                returnvalue = false;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorLog.ExecptionHandler(ex);
                returnvalue = false;
            }
            return returnvalue;
        }
    }
}
