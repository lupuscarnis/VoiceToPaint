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
       private static Dictionary<string, string[]> Commandsmap = new Dictionary<string, string[]>();

        public static Dictionary<string, string[]> Commandsmap1 { get => Commandsmap; set => Commandsmap = value; }

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
            //If it just says int i don't know the assume max 100 for now

            //strings
            Commandsmap.Add("listen", new string[] { "Draw", "Connect", "Edit", "Delete", "Done" });
            //type = string, Size =  0-100, color = string, point = int rotation  = 0 - 360, done = string 
            Commandsmap.Add("draw", new string[] {"Type"});
            //
            Commandsmap.Add("rotate", new string[] { "object1" });
            //
            Commandsmap.Add("size", new string[] { "0", "100",  });
            //Object1 = int, Object2 = int
            Commandsmap.Add("connect", new string[] { "object1", "object2" });
            //subset
            Commandsmap.Add("object1", new string[] { "0", "100",});
            //subset
            Commandsmap.Add("object2", new string[] { "0", "100",});
            //Object = int
            Commandsmap.Add("edit", new string[] { "0", "100"  });
            //Object = int
            Commandsmap.Add("delete", new string[] {"0","100" });
            //all string
            Commandsmap.Add("type", new string[] { "Square", "Rectangle", "Circle", "Triangle" });
            //all string
            Commandsmap.Add("square", new string[] { "Size", "Color", "Point", "Rotation", "Done" });
            //all string
            Commandsmap.Add("circle", new string[] { "Size", "Color", "Point", "Rotation", "Done" });
            //all string
            Commandsmap.Add("triangle", new string[] { "Size", "Color", "Point", "Rotation", "Done" });
            //all string
            Commandsmap.Add("color", new string[] { "Black", "Red", "Blue", "Green", "Yellow", "Green", "Purple"});

        }








    }
}
