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


     
            
            InitiateCommand();
            this.cv = cv;
            
       
        
        
        }
        
        public void PushCommand(string command)
        {
            vr = new VoiceRecognizer();
            vr.NewCommand += PushCommand;
            if (command.ToLower().Equals("done"))
            {
                Console.WriteLine("The Full Command: "+Tools.Command);
                draw.createDrawble(Tools.Command);
                Tools.Command = "";
                Tools.CommandPath = "";
                InitiateCommand();
                
            }
            else
            {
                if (Commands.Commandsmap1.ContainsKey(command.ToLower()))
                {
                    string[] list;
                    Commands.Commandsmap1.TryGetValue(command.ToLower(), out list);
                   
                   
                    if(!Tools.CommandPath.Contains(command))
                        Tools.CommandPath += " " + command.ToLower();
                    Tools.LastCommand = command.ToLower();
                    

                    Console.WriteLine("GotCommand: "+ command);
                    vr.understandArray(list);
                    vr.startListening("");
                }
                else
                {
                    if (!Tools.Command.Contains(command)) {
                        int index;
                        string[] list;


                        if (Tools.CommandPath.Contains("circle"))
                        {
                            Commands.Commandsmap1.TryGetValue("circle", out list);
                            vr.understandArray(list);
                        }
                        else if (Tools.CommandPath.Contains("triangle"))
                        {
                            Commands.Commandsmap1.TryGetValue("triangle", out list);
                            vr.understandArray(list);
                        }
                        else if (Tools.CommandPath.Contains("square"))
                        {
                            Commands.Commandsmap1.TryGetValue("square", out list);
                            vr.understandArray(list);
                        }

                        else if (Tools.CommandPath.Contains("type"))
                        {
                            Commands.Commandsmap1.TryGetValue("type", out list);
                            vr.understandArray(list);
                        }

                        else if (Tools.CommandPath.Contains("draw"))
                        {
                            Commands.Commandsmap1.TryGetValue("draw", out list);
                            vr.understandArray(list);

                        }
                        

                     

                     string atrribute = Tools.LastCommand + ":" + command + ";";
                    if (!Tools.Command.Contains(atrribute))
                    Tools.Command += atrribute;
                    if(Tools.CommandPath.Contains(Tools.LastCommand))
                    Tools.CommandPath = Tools.CommandPath.Replace(Tools.LastCommand, "");
                    Console.WriteLine(Tools.CommandPath);
                    Console.WriteLine(Tools.Command);
                  
                    vr.startListening("");
                   }
                }
            }
        }

        public void InitiateCommand()
        {


            PushCommand("listen");

        }




       



















    }
}
