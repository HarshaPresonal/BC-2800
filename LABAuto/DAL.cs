using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace LABAuto
{
    public class DAL
    {
        ErrorLog _errorLog;
        public string DBpath = Path.GetDirectoryName(Application.ExecutablePath) + "\\DB\\EzovionMI.sqlite";
        public string BranchId = string.Empty;
        public string APIUrl = string.Empty;
        public DAL()
        {
            _errorLog = new ErrorLog();
        }
        public void Hl7split(string DeviceId, string HL7RawData)
        {
            string[] parray = new string[0];
            string[] rarray = new string[0];
            string[] UOMarray = new string[0];
            string[] tarray = new string[0];

            string[] RowArray = HL7RawData.Split('\r');

            string MsgId = "";
            string MsgType = "";
            string LIMO_LabID = "";
            string LI_QPatientID = "";
            string DeviceId_M = "";
            string LIMT_TestID = "";
            string TestName = "";
            string TestResultValue = "";
            string TestUOMcode = "";
            string SampleBarcode = "";
            try
            {

                for (int i = 0; i <= RowArray.Length - 1; i++)
                {
                    if (RowArray[i].Length <= 4)
                    {
                        continue;
                    }
                    switch (RowArray[i].Substring(0, 3))
                    {
                        case "MSH":
                            {
                                string[] MsgTyId = new string[0];
                                MsgTyId = RowArray[i].Split('|');
                                DeviceId_M = MsgTyId[3];
                                MsgType = MsgTyId[8];
                                MsgId = MsgTyId[9];
                                break;
                            }
                        case "SPM":
                            parray = RowArray[i].Split('|');
                            LIMO_LabID = parray[2].Trim();
                            break;
                        case "QPD":
                            rarray = RowArray[i].Split('|');
                            LI_QPatientID = rarray[3].Trim().ToString();
                            //OrderList.Add(LI_QPatientID);
                            break;
                        case "OBR":
                            rarray = RowArray[i].Split('|');
                            SampleBarcode = rarray[2];
                            break;

                        case "OBX":
                            rarray = RowArray[i].Split('|');
                            tarray = rarray[3].Split('^');
                            LIMT_TestID = tarray[0].ToString();
                            TestName = rarray[4];
                            TestResultValue = rarray[5];
                            TestUOMcode = rarray[6];
                            UOMarray = rarray[6].Split('^');
                            break;
                    }
                }
                //textBox1.Text.ToString()
                int pkvalue = InsertResultValues(DeviceId, SampleBarcode, TestName, TestResultValue, TestUOMcode, HL7RawData, BranchId);
                ApiCallfromSqlLite(DeviceId, SampleBarcode, TestName, TestResultValue, TestUOMcode, HL7RawData, BranchId, pkvalue);
            }
            catch (Exception ex)
            {
                _errorLog.ExecptionHandler(ex);
            }
        }
        public int InsertResultValues(string DeviceId, string SampleId, string TestCode, string Value, string ValueUOM, string RawData, string Client)
        {
            bool Returnvalue = false;
            int newID = 0;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + DBpath + ""))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("Insert into DeviceDataUpdated(DeviceID,SampleID,TestCode,Value,ValueUOM,IsProcessed,RawData,Client,ProcessedAt,CreatedAt) " +
                           "  Values ('" + DeviceId + "' ,'" + SampleId + "','" + TestCode + "','" + Value + "','" + ValueUOM + "', 'N' ,'" + RawData + "','" + Client + "','" + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "' ); " +
                           " SELECT cast(last_insert_rowid() as int) as rowid ;",
                           conn))
                    {
                        DataTable dt = new DataTable();
                        conn.Open();
                        //cmd.ExecuteNonQuery();
                       newID = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                    }
                }
                Returnvalue = true;
            }
            catch (Exception ex)
            {
                _errorLog.ExecptionHandler(ex);
                Returnvalue = false;
            }
            return newID;
        }
        public string ApiCallfromSqlLite(string DeviceId, string SampleId, string TestCode, string Value, string ValueUOM, string RawData, string Client, int pkvalue)
        {
            string responseFromServer = "";
            //var postData = "deviceId=" + DeviceId + "&patientId=''&sampleId=" + SampleId + "&testCode=" + TestCode + "&value=" + Value +
            //                "&valueUOM=" + ValueUOM + "&abnormal=''&result=''&createdAt=" + DateTime.Now.ToString() + "&isProcessed='Y'" +
            //                "&processedAt=" + DateTime.Now.ToString() + "&rawData=" + RawData + "&branchId='8E994E07-8A9F-4F85-DCE1-08D8BC4ABE15'";

            object postDataobj = new {
                deviceId = DeviceId,
                patientId = "",
                sampleId = SampleId,
                testCode = TestCode,
                value = Value,
                valueUOM = ValueUOM,
                abnormal = "",
                result = "",
                createdAt = DateTime.Now,
                isProcessed = "Y",
                processedAt = DateTime.Now,
                rawData = RawData,
                //"8E994E07-8A9F-4F85-DCE1-08D8BC4ABE15"
                branchId = BranchId 
            };

            try
            {
                //"https://devenvironmentapi.azurewebsites.net/api/labmachinedata"
                var URL = APIUrl;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Method = "POST";
                request.Credentials = CredentialCache.DefaultCredentials;
                ((HttpWebRequest)request).UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 7.1; Trident/5.0)";
                request.Accept = "/";
                request.UseDefaultCredentials = true;
                request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = js.Serialize(postDataobj);

                byte[] byteArray = Encoding.UTF8.GetBytes(json);
                request.ContentType = "application/json";
                request.MediaType = "application/json";
                request.ContentLength = byteArray.Length;



                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
                HttpWebResponse responsenew = null;

                responsenew = (HttpWebResponse)request.GetResponse();
                int statuscode = (int)responsenew.StatusCode;

                if (statuscode == 200)
                {
                    UpdateRecordsSqllite(pkvalue);
                }

                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                _errorLog.ExecptionHandler(ex);
            }
            return responseFromServer;
        }
        public bool UpdateRecordsSqllite(int pkvalue)
        {
            bool returnvalue = false;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + DBpath + ""))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("Update DeviceDataUpdated Set IsProcessed = 'Y' where UId = @Uid",conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Uid", pkvalue);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                _errorLog.ExecptionHandler(ex);
                returnvalue = false;
            }

            return returnvalue;
        }
    }
}
