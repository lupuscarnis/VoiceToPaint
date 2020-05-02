using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using VoiceToPaint;
using VoiceToPaint.User_Interface;
using System.IO;
using System.Drawing.Drawing2D;

namespace VoiceToPaint.Backend
{
    class Drawables
    {

           
        public Drawables()
        {

        }

        public delegate void UpdateViewListEventHandler(object source, EventArgs e);
        public event UpdateViewListEventHandler ListChanged;
        public delegate void UpdateGraphicEventHandler(object source, EventArgs e);
        public event UpdateGraphicEventHandler GraphicsCleared;

        protected virtual void OnClear()
        {
            if (GraphicsCleared != null)
            {
                GraphicsCleared(this, EventArgs.Empty);
            }

        }
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
                                    Graphics graph = Tools.getCanvas.CreateGraphics();

                                    Tools.getPen = new Pen(Commands.getColor(color));
                                    //Send it to the from 

                                    graph.DrawPath(Tools.getPen, Shapes.Circle(point, size));

                                    //store the object in a sorted list
                                    Tools.getObjects.AddLast("Command: " + command + " Color: " + color + " Point: " + point + " Type: " + type + " Size: " + size);
                                    OnChangeViewList();
                                    //return


                                    break;
                                }
                            case "triangle":
                                {


                                    //create the object to draw 
                                    Graphics graph = Tools.getCanvas.CreateGraphics();

                                    Tools.getPen = new Pen(Commands.getColor(color));
                                    //Send it to the from 

                                    graph.DrawPath(Tools.getPen, Shapes.Triangle(point, size));

                                    //store the object in a sorted list
                                    Tools.getObjects.AddLast("Command: " + command + " Color: " + color + " Point: " + point + " Type: " + type + " Size: " + size);
                                    OnChangeViewList();
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
                case "clear":
                    {


                       
                        //create the object to draw 
                        Graphics graph = Tools.getCanvas.CreateGraphics();
                        //Send it to the from 
                        //set background color
                        graph.Clear(Color.White);


                        //clears the list of object
                        Tools.getObjects.Clear();
                        //clears the borad and redraws it with a delegate
                        OnClear();
                        //Update the List // clears it 
                        OnChangeViewList();
                      





                        break;
                    }
                



            }



        }
        private void extract(string text,out string command, out string color, out string point, out string type, out string size)
        {
            //The formating 
            //command:draw,color:blue,point:1,type:square,size:10,
            string[] list, list2;
            Dictionary<string, string> commandvalues = new Dictionary<string, string>();
            command = "draw";
            color = "red";
            point = "1";
            type = "square";
            size = "10";
           text =  text.Replace(" ", "");
            if(text == null || text == "")
            {
                Console.WriteLine("An empty String");
                Console.WriteLine("Using Default");

            }
            else
            {
              list = text.Split(',');
                foreach(string s in list)
                {
                    if(s != "") { 
                   list2 = s.Split(':');

                        if (!commandvalues.ContainsKey(list2[0].ToLower()))
                        {
                            commandvalues.Add(list2[0].ToLower(), list2[1].ToLower());

                        }


                    }

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



                int pointOnCentermap;
                Point coords;
                int.TryParse(coordinate, out pointOnCentermap);
                Tools.getCenterMap.TryGetValue(pointOnCentermap, out coords);


                int Size;
                int.TryParse(size, out Size);
            









                return new Rectangle(coords, new Size(Size, Size));
            }
            public static GraphicsPath Triangle(string coordinate, string size)
            {


                int Size;
                int.TryParse(size, out Size);


                int pointOnCentermap;
                Point coords;
                int.TryParse(coordinate, out pointOnCentermap);
                Tools.getCenterMap.TryGetValue(pointOnCentermap, out coords);
                GraphicsPath Triangle = new System.Drawing.Drawing2D.GraphicsPath();


                //int x1, int y1, int x2, int y2
                Triangle.AddLine(coords, new Point(coords.X - Size, coords.Y));
                Triangle.AddLine(coords, new Point(coords.X + Size, coords.Y));
                Triangle.AddLine(new Point(coords.X + Size, coords.Y), new Point(coords.X , coords.Y + Size));
                Triangle.AddLine(new Point(coords.X - Size, coords.Y), new Point(coords.X, coords.Y + Size));








                return Triangle;
            }

            public static GraphicsPath Circle(string coordinate, string size)
            {

                GraphicsPath Triangle = new System.Drawing.Drawing2D.GraphicsPath();
                int Size;
                int.TryParse(size, out Size);


              
             
                int pointOnCentermap;
                Point coords;
                int.TryParse(coordinate, out pointOnCentermap);
                Tools.getCenterMap.TryGetValue(pointOnCentermap, out coords);



                Rectangle set = new Rectangle(coords, new Size(Size, Size));



                Triangle.AddArc(set,0.0f,360.0f);
           







                return Triangle;
            }






        }









    }
}
