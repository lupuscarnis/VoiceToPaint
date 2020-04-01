using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using VoiceToPaint;
using VoiceToPaint.User_Interface;


namespace VoiceToPaint.Backend
{
    class Drawables
    {

           
        public Drawables()
        {

        }

        public delegate void UpdateViewListEventHandler(object source, EventArgs e);
        public event UpdateViewListEventHandler ListChanged;



        protected virtual void OnChangeViewList()
        {
          if(ListChanged != null)
            {
                ListChanged(this, EventArgs.Empty);
            }

        }

        public void createDrawble(string text)
        {
            string command, color, point, type, size;
            extract(text, out command,out color, out point, out type, out size);



            switch (command)
            {
                case "draw":
                    {


                        switch (type)
                        {
                            case "square":
                                {

                                    //create the object to draw 
                                    Graphics graph = Tools.getCanvas.CreateGraphics();
                                    //Send it to the from 
                                    Tools.getPen = new Pen(Commands.getColor(color));
                                    graph.DrawRectangle(Tools.getPen, Shapes.Square(point, size));
                                    //store the object in a sorted list
                                    Tools.getObjects.AddLast("Command: " + command+" Color: "+color+" Point: "+point+" Type: "+type+" Size: "+size);
                                     OnChangeViewList();
                                    //return




                                    break;
                                }
                            case "circle":
                                {

                                    //create the object to draw 
                                    //Send it to the from 
                                    //store the object in a sorted list
                                    //return




                                    break;
                                }
                            case "triangle":
                                {


                                    //create the object to draw 
                                    //Send it to the from 
                                    //store the object in a sorted list
                                    //return



                                    break;
                                }

                        }






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
            point = "50,50";
            type = "square";
            size = "10";
            if(text == null || text == "")
            {
                Console.WriteLine("An empty String");
                Console.WriteLine("Using Default");

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
       public static class Shapes 
        {

            public static Rectangle Square( string coordinate, string size)
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
