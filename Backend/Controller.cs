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
using VoiceToPaint.VR;
namespace VoiceToPaint.Backend
{
    class Controller
    {
        Canvas cv;
        VoiceRecognizer vr;
        public void run()
        {


              vr = new VoiceRecognizer();
              vr.startListening();

            vr.NewCommand += PushCommand;
            cv = new Canvas();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(cv);
         
            
       
        
        
        }

        public void PushCommand(string command)
        {



            Drawables draw = new Drawables(cv);
            draw.createDrawble(command);






        }










    }
}
