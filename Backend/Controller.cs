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
using System.Collections;

namespace VoiceToPaint.Backend
{
    class Controller
    {
        Canvas cv;
        VoiceRecognizer vr;
        Drawables draw;
        public void run(Canvas cv, Drawables draw, VoiceRecognizer vr)
        {


            Commands.setupCommandsList();
            this.draw = draw;

            this.vr = vr;

           vr.NewCommand += PushCommand;
            InitiateCommand();
            this.cv = cv;
            
       
        
        
        }

        public void PushCommand(string command)
        {
            if (command.ToLower().Equals("done"))
            {
                Console.WriteLine("The Full Command: "+Tools.Command);
                draw.createDrawble(Tools.Command);
            }
            else
            {
                if (Commands.Commandsmap1.ContainsKey(command.ToLower()))
                {
                    string[] list;
                    Commands.Commandsmap1.TryGetValue(command, out list);
                    Tools.Command += " " + command;
                    Console.WriteLine("GotCommand: "+ command);
                    vr.understandArray(list);
                    vr.startListening("");
                }
                else
                {

                }
            }
        }

        public void InitiateCommand()
        {


            PushCommand("listen");

        }




        public void ObjectPicker(string command)
        {
            //select the list of commands that are avaiable for the selected command/object
            
            //if String is numbers, only takes array of 2, otherwise all lengths are fine. Can be called 
           

            switch (command)
            {
                case "draw":
                    {

                        string[] list = new string[1];



                        break;
                    }
                case "connect":
                    {

                        string[] list = new string[1];



                        break;
                    }
                case "rotate":
                    {

                        string[] list = new string[1];



                        break;
                    }
                case "edit":
                    {



                        break;
                    }
            }




                      
        }





















    }
}
