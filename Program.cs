using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using VoiceToPaint;
using System.Drawing;
using VoiceToPaint.Backend;





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
            Controller cont = new Controller();
            cont.run();






        }
    }
}