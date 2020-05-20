using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace VoiceToPaint.Backend
{
     static class Commands
    {
       private static Dictionary<string, string[]> commandsmap = new Dictionary<string, string[]>();

        public static Dictionary<string, string[]> Commandsmap { get => commandsmap; set => commandsmap = value; }

        static public Color getColor(string color)
        {
            //color = color.ToLower();
            Color output = Color.Red ;
            
            switch (color)
            {
                case "black":
                    {

                        output = Color.Black; 

                     break;
                    }

                case "blue":
                    {

                        output = Color.Blue;

                        break;
                    }

                case "white":
                    {

                        output = Color.White;

                        break;
                    }
                case "red":
                    {

                        output = Color.Red;

                        break;
                    }
                case "yellow":
                    {

                        output = Color.Yellow;

                        break;
                    }
                case "purple":
                    {


                        output = Color.Purple;
                        break;
                    }
                default:
                    {
                        output = Color.Purple;
                        Console.WriteLine("Could not find the color:" + color);
                        break;
                    }


                   




            }







            return output;
        }

        public static void setupCommandsList()
        {

            //Ofc Everything is strings but those are the ranges that they can appear if they have ranges
            //If it just says int i don't know the assume max 100 for now

            //strings
            commandsmap.Add("command", new string[] { "draw","rotate","delete", "connect", "clear","edit"});
            //type = string, Size =  0-100, color = string, point = int rotation  = 0 - 360, done = string 
            commandsmap.Add("draw", new string[] { "type", "return" });
            //
            commandsmap.Add("type", new string[] { "square", "circle", "triangle", "return" });
            commandsmap.Add("rotate", new string[] { "objectkey", "rotation", "done", "return" });
            //
            commandsmap.Add("size", new string[] { "0", "100",  });
            //Object1 = int, Object2 = int
            commandsmap.Add("connect", new string[] { "object1", "object2", "return", "done" });
            //subset
            commandsmap.Add("objectkey", new string[] { "0", "200",  });
            //subset
            commandsmap.Add("object1", new string[] { "0", "100",});
            //subset
            commandsmap.Add("object2", new string[] { "0", "100",});
            //Object = int
           
            //Object = int
            commandsmap.Add("delete", new string[] {"objectkey","done", "return" });
            //
            commandsmap.Add("rotation", new string[] { "0", "360" });
            commandsmap.Add("clear", new string[] { "return", "done" });
            //
            commandsmap.Add("point", new string[] { "0", "360" });
            //all string
            commandsmap.Add("edit", new string[] {"objectkey", "done", "return" });
            //all string
            commandsmap.Add("square", new string[] { "size", "color", "point", "rotation", "done", "return" });
            //all string
            commandsmap.Add("circle", new string[] { "size", "color", "point", "rotation", "done", "return" });
            //all string
            commandsmap.Add("triangle", new string[] { "size", "color", "point", "rotation", "done", "return" });
            //all string
            commandsmap.Add("color", new string[] { "black", "red", "blue", "green", "yellow", "green", "purple"});

        }








    }
}
