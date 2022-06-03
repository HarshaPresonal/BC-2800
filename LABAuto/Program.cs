using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LABAuto
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static string filepath = Path.GetDirectoryName(Application.ExecutablePath) + "\\ErrorLog\\";
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // string resource2 = "LABAuto.Assm.System.Data.SQLite.dll";
            // EmbeddedAssembly.Load(resource2, "System.Data.SQLite.dll");
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(Current_domain_assemblyresolve);
            Application.Run(new Form1());
        }

        static Assembly Current_domain_assemblyresolve(object sender, ResolveEventArgs args)
        {
            //TextWriteline("AssemblyCalling");
            try
            {
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("LABAuto.Assm.System.Data.SQLite.dll"))
                {
                    byte[] assemblydata = new byte[stream.Length];
                    stream.Read(assemblydata, 0, assemblydata.Length);
                    return Assembly.Load(assemblydata);
                }
                //Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("EmbedAssembly.System.Data.SQLite.dll");
                //byte[] assemblydata = new byte[stream.Length];
                //stream.Read(assemblydata, 0, assemblydata.Length);
                //return Assembly.Load(assemblydata);

            }
            catch(Exception ex)
            {
                TextWriteline(ex.Message);
                return Assembly.Load("");
            }
            TextWriteline("AssemblyEnd");
            
        }

        static void TextWriteline(string Wrttext)
        {
            using (StreamWriter sw = File.AppendText(filepath))
            {
                sw.WriteLine(Wrttext);

            }
        }



        //  private static void LoadSQLLiteAssembly()
        //  {
        //Uri dir = new Uri(Assembly.GetExecutingAssembly().CodeBase);
        //FileInfo fi = new FileInfo(dir.AbsolutePath);
        //string binFile = fi.Directory.FullName + "\\System.Data.SQLite.DLL";

        //  }

        //private static string GetAppropriateSQLLiteAssembly()
        //{ Uri dir = new Uri(Assembly.GetExecutingAssembly().CodeBase);
        //    FileInfo fi = new FileInfo(dir.AbsolutePath);
        //    string pa = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
        //    string arch = ((String.IsNullOrEmpty(pa) || String.Compare(pa, 0, "x86", 0, 3, true) == 0) ? "32" : "64");
        //    return Path.GetDirectoryName(Application.ExecutablePath) + "\\Assm\\System.Data.SQLite.dll" ;
        //}

    }
}
