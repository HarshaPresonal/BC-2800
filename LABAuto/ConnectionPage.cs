using System;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.IO;
using portbase = System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace LABAuto
{
    public partial class ConnectionPage : Form
    {
        ErrorLog errorLog;
        DAL _dal; 
        public string DBpath = Path.GetDirectoryName(Application.ExecutablePath) + "\\DB\\EzovionMI.sqlite";
        public string S_portvalue = string.Empty;
        public string configIP = string.Empty;
        public string configPort = string.Empty;
        public string filepath = Path.GetDirectoryName(Application.ExecutablePath) + "\\ErrorLog\\";
        public string line = Environment.NewLine + Environment.NewLine;
        public string InterfaceType = string.Empty;
        private portbase.SerialPort sPort = new portbase.SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);


        public ConnectionPage()
        {
            InitializeComponent();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            filepath = filepath + DateTime.Today.ToString("dd-MM-yy") + ".txt";   //Text File Name
            if (!File.Exists(filepath))
            {
                File.Create(filepath).Dispose();
            }

            errorLog = new ErrorLog();
            _dal = new DAL();
            DeviceIdBind();
            MachineConfig();
            MaketheConnection_Click();
            //backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker1_DoWork);
            //backgroundWorker1.RunWorkerAsync();
        }
        public void DeviceIdBind()
        {
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
                            if (dt.Rows[i]["ConfigName"].ToString() == "DeviceID")
                            {
                                textBox1.Text = dt.Rows[i]["ConfigValue"].ToString();
                                textBox1.ReadOnly = true;
                            }
                            else if (dt.Rows[i]["ConfigName"].ToString() == "S_Port")
                            {
                                InterfaceType = "S_Port";
                                DetVal.Text = dt.Rows[i]["ConfigValue"].ToString();
                                S_portvalue = dt.Rows[i]["ConfigValue"].ToString();
                                ConnVal.Text = dt.Rows[i]["ConfigName"].ToString();
                            }
                            else if (dt.Rows[i]["ConfigName"].ToString() == "IPConfig")
                            {
                                InterfaceType = "IPConfig";
                                DetVal.Text = dt.Rows[i]["ConfigValue"].ToString() + ":" + dt.Rows[i]["ConfigValue2"].ToString();
                                configIP = dt.Rows[i]["ConfigValue"].ToString();
                                configPort = dt.Rows[i]["ConfigValue"].ToString();
                                ConnVal.Text = dt.Rows[i]["ConfigName"].ToString();
                            }
                            else if (dt.Rows[i]["ConfigName"].ToString() == "Comport")
                            {
                                InterfaceType = "Comport";
                                DetVal.Text = dt.Rows[i]["ConfigValue"].ToString();
                                S_portvalue = dt.Rows[i]["ConfigValue"].ToString();
                                ConnVal.Text = dt.Rows[i]["ConfigName"].ToString();
                                sPort.PortName = S_portvalue;
                            }

                            else if (dt.Rows[i]["ConfigName"].ToString() == "BranchId")
                            {
                                _dal.BranchId = dt.Rows[i]["ConfigValue"].ToString();
                            }
                            else if (dt.Rows[i]["ConfigName"].ToString() == "API")
                            {
                                _dal.APIUrl = dt.Rows[i]["ConfigValue"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorLog.ExecptionHandler(ex);
            }
        }
        public void MachineConfig()
        {

        }
        private void ConnectionPage_Load(object sender, EventArgs e)
        {
        }
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
        public void TextWriteline(string Wrttext)
        {
            using (StreamWriter sw = File.AppendText(filepath))
            {
                sw.WriteLine(Wrttext);
                sw.WriteLine(line);
            }
        }
        private void MaketheConnection_Click()
        {
            DetVal.Text = sPort.PortName;
            //string.Concat("Port Name : ", sPort.PortName, " BaudRate : ", sPort.BaudRate, " DataBits : ", sPort.DataBits, " StopBits : ", sPort.StopBits,
            //" Parity : ", sPort.Parity, " Comm. Mode ");
            serialPort1.PortName = sPort.PortName;
            serialPort1.BaudRate = sPort.BaudRate;
            serialPort1.DataBits = sPort.DataBits;
            serialPort1.StopBits = sPort.StopBits;
            serialPort1.Parity = sPort.Parity;

            if (serialPort1.IsOpen)
            {
                MessageBox.Show(serialPort1.PortName + " : Already opened !");
            }
            else
            {
                serialPort1.Open();
                TextWriteline("Connection Opened");
                anaylserData.Text = "Connection Opened";

                serialPort1.DataReceived += serialPort1_DataReceived;
                serialPort1.PinChanged += serialPort1_PinChanged;
                serialPort1.ErrorReceived += serialPort1_ErrorReceived;

            }
        }



        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            //Thread.Sleep(1000);
            //backgroundWorker1.ReportProgress(i);
            ////Check if there is a request to cancel the process
            //if (backgroundWorker1.CancellationPending)
            //{
            //    e.Cancel = true;
            //    backgroundWorker1.ReportProgress(0);
            //    return;
            //}
            TextWriteline(line);
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            for (int i = 0; i < localIPs.Length; i++)
            {
                TextWriteline(localIPs[i] + "" + Convert.ToInt32(S_portvalue));
            }
            string IP_ADD = string.Empty;
            if (InterfaceType == "S_Port")
            {
                IP_ADD = localIPs[1].ToString();
            }
            else if (InterfaceType == "IPConfig")
            {
                IP_ADD = configIP;
                S_portvalue = configPort;
            }
            IPAddress ipAd = IPAddress.Parse(IP_ADD);
            TextWriteline(ipAd.ToString() + "," + S_portvalue);
            TcpListener myList = new TcpListener(ipAd, Convert.ToInt32(S_portvalue));
            TextWriteline(ipAd + "" + Convert.ToInt32(S_portvalue));
            try
            {
                myList.Start();
                anaylserData.Text = "Waiting for a connection. ";
                Socket s = myList.AcceptSocket();
                anaylserData.Text = "Connection Accepted from Analyser";

                while (true)
                {
                    bool flag = true;
                    byte[] b = new byte[s.ReceiveBufferSize];
                    int j = s.Receive(b);
                    ASCIIEncoding encoder = new ASCIIEncoding();
                    string msg = encoder.GetString(b, 0, j);
                    if (msg.Length != 0)
                    {
                        StatusVal.Text = "Connected";
                        anaylserData.Text = msg.ToString();
                        string testhl7 = msg.ToString();
                        _dal.Hl7split(textBox1.Text.ToString(), testhl7);
                        TextWriteline(msg.ToString());
                    }
                }
            }
            catch (Exception Ex)
            {
                errorLog.ExecptionHandler(Ex);
                StatusVal.Text = "Connection Error !!";
                anaylserData.Text = "Analyser Disconnected" + Ex.Message.ToString();
                myList.Stop();
            }
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled)
                {
                    anaylserData.Text = "Process was cancelled";
                }
                else if (e.Error != null)
                {
                    TextWriteline(e.Error.ToString());
                    anaylserData.Text = "There was an error running the process. The thread aborted";
                }
                else
                {
                    anaylserData.Text = "Process was completed";
                }
            }
            catch (Exception ex)
            {
                errorLog.ExecptionHandler(ex);
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string Result = "";
            int Len = 0;
            byte[] bu;
            Len = serialPort1.BytesToRead;
            bu = new byte[Len];
            serialPort1.Read(bu, 0, Len);
            /* Testing */
            string message = serialPort1.ReadLine();


            serialPort1.WriteLine(
                    String.Format("<{0}>: {1}", "ACK", message));


            Console.WriteLine();


        }

        private void serialPort1_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {

        }

        private void serialPort1_PinChanged(object sender, SerialPinChangedEventArgs e)
        {

        }
    }
}
