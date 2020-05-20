using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VoiceToPaint.Backend.Implementations.Commands;

namespace VoiceToPaint.Backend
{
   static class Draw
    {



        static public void Execute(string command, Control control)
        {
            string[] args = ExtractArgs(command);

            switch (args[1])
            {

                case "square":
                    {
                        DrawObject drawObject = new DrawObject(args[1], args[2], int.Parse(args[3]), int.Parse(args[4]), int.Parse(args[5]),command);

                        DrawSquare(drawObject, control);
                        //store the object in a sorted list
                        int ListKeyMod = 0;
                        do
                        {
                            if (!Tools.getObjects.ContainsKey(Tools.getObjects.Count + ListKeyMod)) { Tools.getObjects.Add(Tools.getObjects.Count + ListKeyMod, drawObject); }
                            ListKeyMod++;
                        } while (Tools.getObjects.ContainsKey(Tools.getObjects.Count + ListKeyMod));
                        drawObject.Id = ListKeyMod;


                     

                        break;
                    }

                case "circle":
                    {
                        DrawObject drawObject = new DrawObject(args[1], args[2], int.Parse(args[3]), int.Parse(args[4]), int.Parse(args[5]), command);


                        DrawCircle(drawObject, control);
                        int ListKeyMod = 0;
                        do
                        {
                            if (!Tools.getObjects.ContainsKey(Tools.getObjects.Count + ListKeyMod)) { Tools.getObjects.Add(Tools.getObjects.Count + ListKeyMod, drawObject); }
                            ListKeyMod++;
                        } while (Tools.getObjects.ContainsKey(Tools.getObjects.Count + ListKeyMod));
                        drawObject.Id = ListKeyMod;



                        break;
                    }

                case "triangle":
                    {
                        DrawObject drawObject = new DrawObject(args[1], args[2], int.Parse(args[3]), int.Parse(args[4]), int.Parse(args[5]), command);


                        DrawTriangle(drawObject, control);
                        int ListKeyMod = 0;
                        do
                        {
                            if (!Tools.getObjects.ContainsKey(Tools.getObjects.Count + ListKeyMod)) { Tools.getObjects.Add(Tools.getObjects.Count + ListKeyMod, drawObject); }
                            ListKeyMod++;
                        } while (Tools.getObjects.ContainsKey(Tools.getObjects.Count + ListKeyMod));
                        drawObject.Id = ListKeyMod;



                        break;
                    }




            }
        }
        static public void Execute(DrawObject drawObject, Control control)
        {
           

            switch (drawObject.Type)
            {

                case "square":
                    {
                       

                        DrawSquare(drawObject, control);
          
                       


                        break;
                    }

                case "circle":
                    {
                        


                        DrawCircle(drawObject, control);
                      


                        break;
                    }

                case "triangle":
                    {
                     


                        DrawTriangle(drawObject, control);
                       



                        break;
                    }




            }
        }
        static private string[] ExtractArgs(string text)
        {
            Dictionary<string, string> commandvalues = new Dictionary<string, string>();
            string[] arg = new string[10];
          
            string[] list = new string[10];
            string[] list2 = new string[10];


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

                if (!commandvalues.TryGetValue("command", out arg[0]))
                {
                    arg[0] = "draw";
                }

                if (!commandvalues.TryGetValue("type", out arg[1]))
                {
                    arg[1] = "circle";
                }

                if (!commandvalues.TryGetValue("color", out arg[2]))
                {
                    arg[2] = "blue";
                }

                if (!commandvalues.TryGetValue("point", out arg[3]))
                {
                    arg[3] = "33";
                }

                if (!commandvalues.TryGetValue("size", out arg[4]))
                {
                    arg[4] = "20";
                }

                if (!commandvalues.TryGetValue("rotation", out arg[5]))
                {
                    arg[5] = "180";
                }

            }
           



            return arg;

        }


      static  private void DrawSquare(DrawObject args ,Control control)
        {
            string type = args.Type, color = args.Color, point = args.Point + "", size = args.Size + "", rotation = args.Rotation + "";
            //create the object to draw 
            Graphics graph = control.CreateGraphics();
            //Send it to the form 
            Tools.getPen = new Pen(Commands.getColor(color));
            //graph.DrawRectangle(Tools.getPen, Shapes.Square(point,size));

            if ((size == null) || size.Equals("0"))
            {
                size = "20";
            }

            //Send it to the form 
            graph.DrawPath(Tools.getPen, Shapes.Square(point, size, "100", "90", rotation));



        }

        static private void DrawTriangle(DrawObject args, Control control)
        {
            string type = args.Type, color = args.Color, point = args.Point + "", size = args.Size + "", rotation = args.Rotation + "";
            //create the object to draw 
            Graphics graph = control.CreateGraphics();
            //Send it to the form 
            Tools.getPen = new Pen(Commands.getColor(color));

            if ((size == null) || size.Equals("0"))
            {
                size = "20";
            }
            //Send it to the form 
            graph.DrawPath(Tools.getPen, Shapes.Triangle(point, size, rotation));


        }

        static private void DrawCircle(DrawObject args, Control control)
        {
            string type = args.Type, color = args.Color, point = args.Point + "", size = args.Size + "", rotation = args.Rotation + "";
            //create the object to draw 
            Graphics graph = control.CreateGraphics();
            //Send it to the form 
            Tools.getPen = new Pen(Commands.getColor(color));

            if ((size == null) || size.Equals("0"))
            {
                size = "20";
            }
            //Send it to the form 
            graph.DrawPath(Tools.getPen, Shapes.Circle(point, size));


        }





    }
    public static class Shapes
    {

        public static Rectangle Square(string coordinate, string size)
        {

            int pointOnCentermap;
            Point coords;
            int.TryParse(coordinate, out pointOnCentermap);
            Tools.getCenterMap.TryGetValue(pointOnCentermap, out coords);


            int Size;
            int.TryParse(size, out Size);

            Point center = new Point(coords.X - Size / 2, coords.Y - Size / 2);

            return new Rectangle(center, new Size(Size, Size));
        }

        public static GraphicsPath Square(string coordinate, string nearRightHandSide, string ratio, string angle, string rotation)
        {

            int pointOnCentermap;
            Point coords;

            int.TryParse(coordinate, out pointOnCentermap);
            Tools.getCenterMap.TryGetValue(pointOnCentermap, out coords);

            int Size;
            Console.WriteLine(nearRightHandSide + " driller");
            int.TryParse(nearRightHandSide, out Size);

            int Rotation;
            int.TryParse(rotation, out Rotation);

            int Ratio;
            int.TryParse(ratio, out Ratio);

            int Angle;
            int.TryParse(angle, out Angle);

            new PointF(10, 20);

            GraphicsPath Square = new GraphicsPath();

            Double P1deltaX = -Size / 2 * Ratio / 100 * Math.Abs(Math.Sin((double)Angle / 360 * 2 * Math.PI));
            Double P1deltaY = Size / 2 * Ratio / 100 * Math.Cos((double)Angle / 360 * 2 * Math.PI) + Size / 2;
            Double P2deltaX = -P1deltaX;
            Double P2deltaY = -Size / 2 * Ratio / 100 * Math.Cos((double)Angle / 360 * 2 * Math.PI) + Size / 2;

            Point P1 = CirclePointCoordinate(Convert.ToInt32((Math.PI - Math.Atan(P1deltaX / P1deltaY)) * 360 / (2 * Math.PI) + Rotation), 2 * Math.Sqrt(Math.Pow(P1deltaX, 2) + Math.Pow(P1deltaY, 2)), coords);
            Point P3 = Point.Subtract(Point.Add(coords, new Size(coords)), new Size(P1));
            Point P2 = CirclePointCoordinate(Convert.ToInt32((-Math.PI / 2 + Math.Atan(P2deltaX / P2deltaY)) * 360 / (2 * Math.PI) + Rotation), 2 * Math.Sqrt(Math.Pow(P2deltaX, 2) + Math.Pow(P2deltaY, 2)), coords);
            Point P4 = Point.Subtract(Point.Add(coords, new Size(coords)), new Size(P2));

            Square.AddLine(P1, P2);
            Square.AddLine(P2, P3);
            Square.AddLine(P3, P4);
            Square.AddLine(P4, P1);

            return Square;
        }

        public static GraphicsPath Triangle(string coordinate, string size, string rotation)
        {


            Double Size;
            Double.TryParse(size, out Size);

            int Rotation;
            int.TryParse(rotation, out Rotation);


            int pointOnCentermap;
            Point coords;
            int.TryParse(coordinate, out pointOnCentermap);

            if (Tools.getCenterMap.TryGetValue(pointOnCentermap, out coords))
            {
                Console.WriteLine("You did it it was there");

            }
            else
            {
                Console.WriteLine("failed to get coords");
            }

            GraphicsPath Triangle = new GraphicsPath();

            Point P1 = CirclePointCoordinate(90 + Rotation, Size, coords);
            Point P2 = CirclePointCoordinate(210 + Rotation, Size, coords);
            Point P3 = CirclePointCoordinate(330 + Rotation, Size, coords);
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

            Point center = new Point(coords.X - Size / 2, coords.Y - Size / 2);
            Rectangle set = new Rectangle(center, new Size(Size, Size));



            Triangle.AddArc(set, 0.0f, 360.0f);








            return Triangle;
        }

        private static Point CirclePointCoordinate(int angle, double size, Point center)
        {

            return new Point(center.X + Convert.ToInt32(Math.Cos((double)angle / 360 * 2 * Math.PI) * size / 2), center.Y + Convert.ToInt32(Math.Sin((double)angle / 360 * 2 * Math.PI) * size / 2));
        }
    }
    }
