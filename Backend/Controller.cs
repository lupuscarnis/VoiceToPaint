using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.Windows.Forms;
using VoiceToPaint;
using System.Drawing;
using VoiceToPaint.Backend;
namespace VoiceToPaint.Backend
{
    class Controller
    {

        public void run()
        {



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Canvas());
       
        
        
        }










    }
}
