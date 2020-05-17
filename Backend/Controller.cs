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
        String LastAttribute = "";
        public delegate void CommandListEventHandler(string[] text);
        public event CommandListEventHandler CommandListChanged ;

        public void run(Canvas cv, Drawables draw)
        {
            
            Commands.setupCommandsList();
            this.draw = draw;

            
         InitiateCommand();
            this.cv = cv;
            
        
        }
        protected virtual void OnCommandList(string[] text)
        {
            if (CommandListChanged != null)
            {
                CommandListChanged(text);
            }

        }



        public void PushCommand(string command)
        {
            vr = new VoiceRecognizer();
            vr.NewCommand += PushCommand;
            if (command.ToLower().Equals("done"))
            {
                string[] list;
                string formCommand = "";

                Tools.CommandPath = Tools.CommandPath.Trim();
                list = Tools.CommandPath.Split(' ');
                
                for(int i = 0; i < list.Length;)
                {

                    formCommand += list[i] + ":" + list[i + 1]+ ",";
                    i +=2 ;
                }
                if (list[2] == null)
                {
                    list[2] = "";
                }

                Console.WriteLine(formCommand + Tools.Command);
                formCommand += Tools.Command;

                Console.WriteLine("The Full Command: "+formCommand);

                
                draw.createDrawble(formCommand);
                Tools.Command = "";
                Tools.CommandPath = "";
                Tools.LastCommand = "";
                InitiateCommand();
                
            }
            else
            {
                if (Commands.Commandsmap1.ContainsKey(command.ToLower()))
                {
                    string[] list;
                    Commands.Commandsmap1.TryGetValue(command.ToLower(), out list);

                                        
                      Tools.CommandPath +=  " " + command.ToLower() ;
                    Console.WriteLine(Tools.CommandPath);

                    Tools.LastCommand = command.ToLower();
                    

                    Console.WriteLine("GotCommand: "+ command);
                    OnCommandList(list);
                    vr.understandArray(list);
                    vr.startListening("");
                }
                else
                {
                  /*  if (!Tools.Command.Contains(command))*/ {
                        int index;
                        string[] list;


                        if (Tools.CommandPath.Contains("circle"))
                        {
                            Commands.Commandsmap1.TryGetValue("circle", out list);
                            vr.understandArray(list);
                            OnCommandList(list);
                        }
                        else if (Tools.CommandPath.Contains("triangle"))
                        {
                            Commands.Commandsmap1.TryGetValue("triangle", out list);
                            vr.understandArray(list);
                            OnCommandList(list);
                        }
                        else if (Tools.CommandPath.Contains("square"))
                        {
                            Commands.Commandsmap1.TryGetValue("square", out list);
                            vr.understandArray(list);
                            OnCommandList(list);
                        }

                        else if (Tools.CommandPath.Contains("type"))
                        {
                            Commands.Commandsmap1.TryGetValue("type", out list);
                            vr.understandArray(list);
                            OnCommandList(list);
                        }

                        else if (Tools.CommandPath.Contains("draw"))
                        {
                            Commands.Commandsmap1.TryGetValue("draw", out list);
                            vr.understandArray(list);
                            OnCommandList(list);

                        }

                        
                        string attribute = Tools.LastCommand + ":" + command + ",";

                                                                     
                        if (!Tools.Command.Contains(command.ToLower()))
                        {
                            if (Tools.Command.Contains(Tools.LastCommand))
                            {
                                Tools.Command = Tools.Command.Replace(LastAttribute, attribute);
                            }

                            Tools.Command += attribute;
                            LastAttribute = attribute;
                            
                        }

                        if (Tools.CommandPath.Contains(Tools.LastCommand))
                        {
                            Tools.CommandPath = Tools.CommandPath.Replace(Tools.LastCommand, "");
                        }
                            
                        Console.WriteLine(Tools.CommandPath);
                        Console.WriteLine(Tools.Command);



                        vr.startListening("");
                   }
                }
               
                   
            }
        }

        public void InitiateCommand()
        {


            PushCommand("command");
            OnCommandList(Commands.Commandsmap1);
        }




       



















    }
}
