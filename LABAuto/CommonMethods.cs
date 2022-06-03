using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LABAuto
{
    class CommonMethods
    {
        public bool CheckOpened(string name)
        {
            for (int i = 0; i<Application.OpenForms.Count;i++ )
            {
                if(Application.OpenForms[i].Name.Equals(name))
                {
                    Application.OpenForms[i].BringToFront();
                    return false;
                }
            }
                return true;
        }
    }
}
