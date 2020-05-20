using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing;

using System.Diagnostics;
using VoiceToPaint;
using System.IO;
using System.Drawing.Drawing2D;

namespace VoiceToPaint.Backend.Implementations.Commands
{
  static  class Connect
    {


        static public void Execute(string command, Control control)
        {
            Point onObject;
            LinkedList<Point> result = new LinkedList<Point>();
            double vectorlenght;
            int key1, key2, coordsize1, coordsize2;
            Point point1, point2, vector1, vector2;
            string[] args = ExtractArgs(command);
            int.TryParse(args[1], out key1);
            int.TryParse(args[2], out key2);




            DrawObject[] objects = new DrawObject[2];
            Tools.getObjects.TryGetValue(key1, out objects[0]);
            Tools.getObjects.TryGetValue(key2, out objects[1]);
            Tools.getCenterMap.TryGetValue(objects[0].Point, out point1);
            Tools.getCenterMap.TryGetValue(objects[1].Point, out point2);
            /*
             foreach (DrawObject s in objects)
             { 
             switch (s.Type)
             {
                 case "circle":
                     {
                         Point center;double angle;

                         vector1 = new Point(point1.X - point2.X, point1.Y - point2.Y);
                         Tools.getCenterMap.TryGetValue(s.Point, out center);
                         vector2 = new Point(center.X - (center.X + s.Size), center.Y - (center.Y + s.Size));
                         angle = Shapes.AngleBetween(vector1, vector2);
                         onObject =  Shapes.PointOnCircle(s.Size, (float)angle, center);
                         result.AddFirst(onObject);

                         break;
                     }
                 case "square":
                     {




                         break;
                     }
                 case "triangle":
                     {




                         break;
                     }


             }
             }

             //create the object to draw 

             //Send it to the form 
             Point[] points = new Point[2];
             int i = 0;
             while(i < 2)
             {
                 points[i] = result.First();
                 result.RemoveFirst();
                 i++;
             }
             */
            Graphics graph = control.CreateGraphics();
            GraphicsPath path = new GraphicsPath();
            path.AddLine(point1, point2);
            //Send it to the form 
            graph.DrawPath(Tools.getPen, path);



        }

        static private string[] ExtractArgs(string text)
        {
          string[]  arg = new string[3];
            string[] list = new string[3];
            string[] list2 = new string[3];
            if (text == null || text == "")
            {
                Console.WriteLine("An empty String");
            

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

            return arg;

        }
       


    }
}
