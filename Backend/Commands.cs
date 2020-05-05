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
        static Dictionary<string, string[]> Commandsmap = new Dictionary<string, string[]>();
        static public Color getColor(string color)
        {
            color = color.ToLower();
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
                        output = Color.Black;
                        Console.WriteLine("Could not find the color:" + color);
                        break;
                    }







            }







            return output;
        }

        public static void setupCommandsList()
        {

            //Ofc Everything is strings but those are the ranges that they can appear if they have ranges
            // if it just says int i don't know the assume max 100 for now
            //strings
            Commandsmap.Add("listen", new string[] { "draw", "connect", "edit", "delete", "done" });
            //type = string, Size =  0-100, color = string, point = int rotation  = 0 - 360, done = string 
            Commandsmap.Add("draw", new string[] {"type","size", "color", "point", "rotation", "done" });
            //Object1 = int, Object2 = int
            Commandsmap.Add("connect", new string[] { "Object1", "Object2", "done" });
            //Object = int
            Commandsmap.Add("edit", new string[] { "Object", "done" });
            //Object = int
            Commandsmap.Add("delete", new string[] { "Object", "done" });
        }

        






    }
}
