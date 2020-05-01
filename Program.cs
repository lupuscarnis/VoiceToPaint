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
            Tools.Debug = true;



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Canvas());



        }
        public class ThreadExample
        {
            // The ThreadProc method is called when the thread starts.
            // It loops ten times, writing to the console and yielding 
            // the rest of its time slice each time, and then ends.
            public static void ThreadProc()
            {
               
                
                
                Drawables draw = new Drawables();

                Tools.getDraw = draw;




               
            }




        }
        public class ThreadVoice
        {
        
            // The ThreadProc method is called when the thread starts.
            // It loops ten times, writing to the console and yielding 
            // the rest of its time slice each time, and then ends.
            public static void ThreadProc()
            {





                VoiceRecognizer vr = new VoiceRecognizer();
                vr.startListening();





            }




        }
    }
}
