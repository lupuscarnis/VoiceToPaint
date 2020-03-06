using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace VoiceToPaint.Backend
{
    class Drawables
    {


        public void createDrawble(string text)
        {
            string command, color, point, type, size;
            extract(text, out command,out color, out point, out type, out size);



            switch (command)
            {
                case "draw":
                    {


                         


                break;
                    }
                case "connect":
                    {




                        break;
                    }







            }



        }
        private void extract(string text,out string command, out string color, out string point, out string type, out string size)
        {
            string[] list, list2;
            Dictionary<string, string> commandvalues = new Dictionary<string, string>();
            command = "draw";
            color = "red";
            point = "0,0";
            type = "square";
            size = "10";
            if(text == null || text == "")
            {
                Console.WriteLine("An empty String");

            }
            else
            {
              list = text.Split(';');
                foreach(string s in list)
                {
                   list2 = s.Split(':');

                    commandvalues.Add(list[0].ToLower(), list2[1].ToLower());

                    
                }

                commandvalues.TryGetValue("command", out command);

                commandvalues.TryGetValue("color",out color);

                commandvalues.TryGetValue("point", out point);

                commandvalues.TryGetValue("type", out type);

                commandvalues.TryGetValue("size", out size);

            }












        }
       public class Shapes  : Drawables
        {

            public Rectangle rectangle( string coordinate, string size)
            {
                string[] coor;
                coor = coordinate.Split(',');
                int Size, x, y;
                int.TryParse(size, out Size);
                int.TryParse(coor[0], out x);
                int.TryParse(coor[1], out y);










                return new Rectangle(new Point(x, y), new Size(Size, Size));
            }








        }









    }
}
