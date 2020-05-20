using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoiceToPaint.Backend.Implementations.Commands
{
   static class Clear
    {
        static public void Execute()
        {

            //clears the list of object
            Tools.getObjects.Clear();
           
        }

    }
}
