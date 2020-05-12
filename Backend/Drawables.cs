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
        Control control;

        public Drawables(Control control)
        {
            this.control = control;
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
            if (ListChanged != null)
            {
                ListChanged(this, EventArgs.Empty);
            }

        }

        public void createDrawble(string text)
        {
            string[] args;
            
            extract(text, out args);
            string command = args[0];


            switch (command)
            {
                case "draw":
                    {
                        

                        switch (args[1])
                        {
                           
                            case "square":
                                {
                                    string type = args[1], color = args[2], point = args[3], size = args[4], rotation = args[5];
                                    //create the object to draw 
                                    Graphics graph = control.CreateGraphics();
                                    //Send it to the from 
                                    Tools.getPen = new Pen(Commands.getColor(color));
                                    graph.DrawRectangle(Tools.getPen, Shapes.Square(point, size));
                                    //store the object in a sorted list
                                    Tools.getObjects.AddLast("Command: " + command + " Color: " + color + " Point: " + point + " Type: " + type + " Size: " + size);
                                    OnChangeViewList();
                                    //return




                                    break;
                                }
                            case "circle":
                                {
                                    string type = args[1], color = args[2], point = args[3], size = args[4], rotation = args[5];
                                    //create the object to draw 
                                    Graphics graph = control.CreateGraphics();

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

                                    string type = args[1], color = args[2], point = args[3], size = args[4], rotation = args[5];
                                    //create the object to draw 
                                    Graphics graph = control.CreateGraphics();

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
                        string object1= args[1], object2 = args[2];






                        break;
                    }


                case "Rotate":
                    {
                        string object1 = args[1], rotation = args[2];




                        break;
                    }
                case "clear":
                    {



                        //create the object to draw 
                        Graphics graph = control.CreateGraphics();
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
                case "delete":
                    {


                        string object1 = args[1];




                       //Update the List // clears it 
                       OnChangeViewList();






                        break;
                    }




            }



        }
        private void extract(string text, out string[] args)
        {
            //The formating 
            //command:draw,color:blue,point:1,type:square,size:10,
            string[] list, list2, arg = new string[1];
            Dictionary<string, string> commandvalues = new Dictionary<string, string>();
            string command;
           text =  text.Replace(" ", "");


            //getting command
            command = extractCommand(text);

            switch (command)
            {

                case "draw":
                    {
                        arg = new string[10];

                        arg[0] = "draw";
                        arg[1] = "red";
                        arg[2] = "1";
                        arg[3] = "square";
                        arg[4] = "10";
                        arg[5] = "0";
                        if (text == null || text == "")
                        {
                            Console.WriteLine("An empty String");
                            Console.WriteLine("Using Default");

                        }
                        else
                        {
                            list = text.Split(',');
                            foreach (string s in list)
                            {
                                if (s != "")
                                {
                                    list2 = s.Split(':');

                                    if (!commandvalues.ContainsKey(list2[0].ToLower()))
                                    {
                                        commandvalues.Add(list2[0].ToLower(), list2[1].ToLower());

                                    }


                                }

                            }
                          
                            commandvalues.TryGetValue("command", out arg[0]);

                            commandvalues.TryGetValue("type", out arg[1]);

                            commandvalues.TryGetValue("color", out arg[2]);


                            commandvalues.TryGetValue("point", out arg[3]);

                            commandvalues.TryGetValue("size", out arg[4]);

                            commandvalues.TryGetValue("rotation", out arg[5]);



                        }


                        break;
                    }
                case "delete":
                    {
                        arg = new string[10];
                        if (text == null || text == "")
                        {
                            Console.WriteLine("An empty String");
                            Console.WriteLine("Using Default");

                        }
                        else
                        {
                            list = text.Split(',');
                            foreach (string s in list)
                            {
                                if (s != "")
                                {

                                    list2 = s.Split(':');
                                    if (list2.Contains("command"))
                                    arg[0] = list2[1];
                                    if(list2.Contains("object1"))
                                     arg[1] = list2[1];
                                    


                                }

                            }



                        }


                        break;
                    }
                case "rotate":
                    {
                        arg = new string[10];
                        if (text == null || text == "")
                        {
                            Console.WriteLine("An empty String");
                            Console.WriteLine("Using Default");

                        }
                        else
                        {
                            list = text.Split(',');
                            foreach (string s in list)
                            {
                                if (s != "")
                                {

                                    list2 = s.Split(':');
                                    if (list2.Contains("command"))
                                        arg[0] = list2[1];
                                    if (list2.Contains("object1"))
                                        arg[1] = list2[1];
                                    if (list2.Contains("rotation"))
                                        arg[2] = list2[1];



                                }

                            }



                        }


                        break;
                    }
                case "connect":
                    {
                        arg = new string[10];
                        if (text == null || text == "")
                        {
                            Console.WriteLine("An empty String");
                            Console.WriteLine("Using Default");

                        }
                        else
                        {
                            list = text.Split(',');
                            foreach (string s in list)
                            {
                                if (s != "")
                                {

                                    list2 = s.Split(':');
                                    if (list2.Contains("command"))
                                        arg[0] = list2[1];
                                    if (list2.Contains("object1"))
                                        arg[1] = list2[1];
                                    if (list2.Contains("object2"))
                                        arg[2] = list2[1];



                                }

                            }



                        }


                        break;
                    }


            };
            args = arg;





        }


        private string extractCommand(string text)
        {
            string[] list, list2;

            if (text == null || text == "")
            {
                Console.WriteLine("An empty String");
                Console.WriteLine("Using Default");

            }
            else
            {
                list = text.Split(',');
                foreach (string s in list)
                {
                    if (s != "")
                    {
                        list2 = s.Split(':');

                        if (list2.Contains("command"))
                        {
                            return list2[1];
                        }


                    }

                }



            }
            return "";
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
                
                Point center = new Point(coords.X-Size/2,coords.Y-Size/2);

                return new Rectangle(center, new Size(Size, Size));
            }
            public static GraphicsPath Triangle(string coordinate, string size)
            {


                Double Size;
                Double.TryParse(size, out Size);


                int pointOnCentermap;
                Point coords;
                int.TryParse(coordinate, out pointOnCentermap);
              
                if(Tools.getCenterMap.TryGetValue(pointOnCentermap, out coords)){
                    Console.WriteLine("You did it it was there");

                }
                else
                {
                    Console.WriteLine("failed to get coords");
                }
                
                GraphicsPath Triangle = new GraphicsPath();
                
                Point P1 = CirclePointCoordinate(90,Size,coords);
                Point P2 = CirclePointCoordinate(210,Size,coords);
                Point P3 = CirclePointCoordinate(330,Size,coords);
                //int x1, int y1, int x2, int y2
                
                Triangle.AddLine(P1, P2);
                Triangle.AddLine(P2, P3);
                Triangle.AddLine(P3, P1);








                return Triangle;
            }

            public static GraphicsPath Circle(string coordinate, string size)
            {

                GraphicsPath Triangle = new GraphicsPath();
                int Size;
                int.TryParse(size, out Size);


              
             
                int pointOnCentermap;
                Point coords;
                int.TryParse(coordinate, out pointOnCentermap);
                Tools.getCenterMap.TryGetValue(pointOnCentermap, out coords);

                //int SideLength = Convert.ToInt32(Size*Math.Sqrt(2));
                Point center = new Point(coords.X-Size/2,coords.Y-Size/2);
                Rectangle set = new Rectangle(center, new Size(Size, Size));



                Triangle.AddArc(set,0.0f,360.0f);
           







                return Triangle;
            }


            private static Point CirclePointCoordinate(int angle, double size, Point center) {
                
                return  new Point(center.X + Convert.ToInt32(Math.Cos((double)angle/360*2*Math.PI)*size/2), center.Y + Convert.ToInt32(Math.Sin((double)angle/360*2*Math.PI)*size/2));
            }



        }









    }
}
