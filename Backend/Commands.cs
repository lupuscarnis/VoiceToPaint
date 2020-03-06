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
                        Console.WriteLine("Could Not Find the color:" + color);
                        break;
                    }







            }







            return output;
        }
        






    }
}
