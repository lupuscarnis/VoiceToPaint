using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using VoiceToPaint;
using System.Drawing;
using VoiceToPaint.Backend;
using VoiceToPaint.VR;

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

            //VoiceRecognizer vr = new VoiceRecognizer();
            //vr.startListening("draw|draw|draw");
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Canvas cv;
            Tools.Debug = true;
            cv = new Canvas();
            Application.Run(cv);
            


        }
    }
}