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
        public void run(Canvas cv, Drawables draw)
        {


            Commands.setupCommandsList();
            this.draw = draw;
         //   vr = new VoiceRecognizer();
      
           // vr.startListening();

           // vr.NewCommand += PushCommand;
            
            this.cv = cv;
            
       
        
        
        }

        public void PushCommand(string command)
        {

            draw.createDrawble(command);

        }

        public void ConstructCommand(string list)
        {

            string command = "";


            if (list.Equals("done"))
            {

                //return when you have the keyword done and push Command
                if(!command.Equals(""))
                PushCommand(command);
            }
            else
            {

                //create the list of aviable commands and send it to Voice and Display what we are working on 


                command += list;
            }
     


        }
        public string InitiateCommand(string list)
        {


            //listen for the Initial KeyWords
            

                return "";
        }




        public void ObjectPicker(string command)
        {
            //select the list of commands that are avaiable for the selected command/object


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
