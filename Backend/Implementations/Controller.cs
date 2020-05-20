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
using VoiceToPaint.VoiceRecognition;
using System.Collections;

namespace VoiceToPaint.Backend
{
    class Controller : IController
    {
        Canvas cv;
        VoiceRegTest vr;
        Drawables draw;

        public delegate void CommandListEventHandler(string[] text);
        public event CommandListEventHandler CommandListChanged;

        public void run(Canvas cv, IDrawables draw, IVoiceRecognition vr)
        {
            //must be run first
            Commands.setupCommandsList();
            this.cv = cv;
            this.draw = (Drawables)draw;
            this.vr = (VoiceRegTest)vr;
            PushCommand("command");
            vr.startListening();
            vr.NewCommand += PushCommand;
        
          


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

            if (command.ToLower().Equals("done") || command.ToLower().Equals("return"))
            {
                if (command.ToLower().Equals("done"))
                {
                    string[] list;
                    string formCommand = "";

                    if (Tools.CommandPath.ToLower().Contains("edit"))
                    {
                        Tools.Command = Tools.Command.Replace(",", "");
                        list = Tools.Command.Split(':');
                        DrawObject Object;
                        int objectkey;
                        int.TryParse(list[1], out objectkey);
                        Tools.getObjects.TryGetValue(objectkey, out Object);
                        draw.createDrawble("command:delete,objectkey:"+objectkey+",");

                        Commands.Commandsmap.TryGetValue(Object.Type, out list);
                        Tools.Command = Object.Inputtext;
                        Tools.CommandPath = "command draw type " + Object.Type;
                        Tools.LastCommand = "" + Object.Type;
                        vr.SetGrammer(list);
                        OnCommandList(list);

                    }
                    else
                    {
                        Tools.CommandPath = Tools.CommandPath.Trim();
                        list = Tools.CommandPath.Split(' ');

                        for (int i = 0; i < list.Length;)
                        {

                            formCommand += list[i] + ":" + list[i + 1] + ",";
                            i += 2;
                        }


                        Console.WriteLine(formCommand + Tools.Command);
                        formCommand += Tools.Command;

                        Console.WriteLine("The Full Command: " + formCommand);


                        draw.createDrawble(formCommand);
                        Tools.Command = "";
                        Tools.CommandPath = "";
                        Tools.LastCommand = "";
                        PushCommand("command");


                    }
                }
                else if (command.ToLower().Equals("return"))
                {
                    string[] list;
                    Commands.Commandsmap.TryGetValue("command", out list);
                    Tools.Command = "";
                    Tools.CommandPath = "command";
                    Tools.LastCommand = "";
                    vr.SetGrammer(list);
                    OnCommandList(list);
                }


            }
            else
            {
                if (Commands.Commandsmap.ContainsKey(command.ToLower()))
                {
                    string[] list;
                    Commands.Commandsmap.TryGetValue(command.ToLower(), out list);


                    Tools.CommandPath += " " + command.ToLower();
                    Console.WriteLine(Tools.CommandPath);

                    Tools.LastCommand = command.ToLower();


                    Console.WriteLine("GotCommand: " + command);

                    vr.SetGrammer(list);
                    OnCommandList(list);
                }
                else
                {
                    /*  if (!Tools.Command.Contains(command))*/
                    {

                        string[] list = new string[0];


                        if (Tools.CommandPath.Contains("circle"))
                        {
                            Commands.Commandsmap.TryGetValue("circle", out list);
                            vr.SetGrammer(list);

                        }
                        else if (Tools.CommandPath.Contains("triangle"))
                        {
                            Commands.Commandsmap.TryGetValue("triangle", out list);
                            vr.SetGrammer(list);

                        }
                        else if (Tools.CommandPath.Contains("square"))
                        {
                            Commands.Commandsmap.TryGetValue("square", out list);
                            vr.SetGrammer(list);

                        }

                        else if (Tools.CommandPath.Contains("type"))
                        {
                            Commands.Commandsmap.TryGetValue("type", out list);
                            vr.SetGrammer(list);

                        }

                        else if (Tools.CommandPath.Contains("draw"))
                        {
                            Commands.Commandsmap.TryGetValue("draw", out list);
                            vr.SetGrammer(list);


                        }
                        else if (Tools.CommandPath.Contains("rotate"))
                        {
                            Commands.Commandsmap.TryGetValue("rotate", out list);
                            vr.SetGrammer(list); ;


                        }
                        else if (Tools.CommandPath.Contains("delete"))
                        {
                            Commands.Commandsmap.TryGetValue("delete", out list);
                            vr.SetGrammer(list);


                        }
                        else if (Tools.CommandPath.Contains("connect"))
                        {
                            Commands.Commandsmap.TryGetValue("connect", out list);
                            vr.SetGrammer(list);


                        }
                        else if (Tools.CommandPath.Contains("edit"))
                        {
                            Commands.Commandsmap.TryGetValue("edit", out list);
                            vr.SetGrammer(list);


                        }





                        string attribute = Tools.LastCommand + ":" + command + ",";


                        if (!Tools.Command.Contains(command.ToLower()))
                        {
                            string[] list1, list2;

                            if (Tools.Command.Contains(Tools.LastCommand))
                            {
                                list1 = Tools.Command.Split(',');
                                foreach (string s in list1)
                                {
                                    list2 = s.Split(':');
                                    if (Tools.LastCommand.Equals(list2[0]))
                                    {
                                        Tools.LastAttribute = Tools.LastCommand + ":" + list2[1] + ",";
                                    }
                                }


                                Tools.Command = Tools.Command.Replace(Tools.LastAttribute, attribute);
                            }
                            else
                            {
                                Tools.Command += attribute;
                            }


                            Tools.LastAttribute = attribute;

                        }

                        if (Tools.CommandPath.Contains(Tools.LastCommand))
                        {
                            Tools.CommandPath = Tools.CommandPath.Replace(Tools.LastCommand, "");
                        }

                        Console.WriteLine(Tools.CommandPath);
                        Console.WriteLine(Tools.Command);


                        OnCommandList(list);

                    }
                }


            }
        }
























    }
}
