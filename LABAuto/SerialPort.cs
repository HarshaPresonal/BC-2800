using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using port = System.IO.Ports;

namespace LABAuto
{
    public partial class SerialPort : Form
    {
        ErrorLog errorLog;
        public string DBpath = Path.GetDirectoryName(Application.ExecutablePath) + "\\DB\\EzovionMI.sqlite";
        public string S_portvalue = string.Empty;
        public SerialPort()
        {
            InitializeComponent();
            getPort();
            GridBind();
            errorLog = new ErrorLog();
        }

        public void getPort()
        {
            string[] ports = port.SerialPort.GetPortNames();
            //ComboBox mybox = new ComboBox();
            for (int i = 0; i < ports.Length; i++)
            {
                comboBox1.Items.Add(ports[i]);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Delete Code Here;
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
        private void button1_Click(object sender, EventArgs e)
        {
            string Seriport = string.Empty;
            Seriport = Convert.ToString(comboBox1.SelectedItem) == "" || Convert.ToString(comboBox1.SelectedItem) ==  null  ? comboBox1.Text.ToString() : Convert.ToString(comboBox1.SelectedItem);
//            InsertSerialComport(Seriport);
            if (!(Seriport.Trim().Length <= 0))
            {
                if (InsertSerialComport(Seriport))
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
                MessageBox.Show("Please enter the port !!");
            }

        }
        public bool InsertSerialComport(string SerialPort)
        {
            bool returnvalue = false;
            try
            {
                if (DBValidation(SerialPort))
                {
                    using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + DBpath + ""))
                    {
                        using (SQLiteCommand cmd = new SQLiteCommand("Insert into Config(ConfigName,ConfigValue,IsActive) " +
                            "  Values ('Comport'," + SerialPort + ",1)      ", conn))
                        {
                            DataTable dt = new DataTable();
                            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                            da.Fill(dt);
                        }
                    }
                    GridBind();
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
