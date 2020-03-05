using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using VoiceToPaint;
//using VoiceToPaint.VR;


namespace VoiceToPaint
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Canvas());
           // VoiceRecognizer vr = new VoiceRecognizer();
            //vr.startListening();
        }
    }
}
