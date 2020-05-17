using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using VoiceToPaint;
using VoiceToPaint.User_Interface;
using System.IO;
using System.Drawing.Drawing2D;

namespace VoiceToPaint.Backend
{
    class Drawables
    {
        Control control;
        private IDictionary<int, string[]> ObjectStorage = new Dictionary<int, string[]>();

        public Drawables(Control control)
        {
            this.control = control;
        }

        public delegate void UpdateViewListEventHandler(object source, EventArgs e);
        public event UpdateViewListEventHandler ListChanged;
        public delegate void UpdateGraphicEventHandler(object source, EventArgs e);
        public event UpdateGraphicEventHandler GraphicsCleared;

        public Dictionary<int, string[]> GetObjectDict() {
            return (Dictionary<int, string[]>) ObjectStorage;
        }

        public string[] GetObject(int key) {
            string[] Output; 
            ObjectStorage.TryGetValue(key, out Output);
            return Output;
        }


        public void SetObject(int key, string[] args) {
            string[] drawing;
            if (ObjectStorage.TryGetValue(key, out drawing))
            {
                ObjectStorage[key] = args;
            }
            else
            {
            ObjectStorage.Add(key, args);
            }
        }


        public void RemoveObject(int key) {
            if(ObjectStorage.ContainsKey(key)) {
                ObjectStorage.Remove(key);
            }
        }


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

                                    DrawSquare(args);
                                     //store the object in a sorted list
                                    int ListKeyMod = 0;
                                    do{
                                        if(!ObjectStorage.ContainsKey(ObjectStorage.Count+ListKeyMod)) {ObjectStorage.Add(ObjectStorage.Count+ListKeyMod,args);}
                                        ListKeyMod++;
                                    }while(ObjectStorage.ContainsKey(ObjectStorage.Count+ListKeyMod));

                                    Tools.getObjects.AddLast("Command: " + command + " Color: " + color + " Point: " + point + " Type: " + type + " Size: " + size);

                                    OnChangeViewList();
                                   
                                    break;
                                }

                            case "circle":
                                {
                                    string type = args[1], color = args[2], point = args[3], size = args[4], rotation = args[5];

                                    DrawCircle(args);
                                    int ListKeyMod = 0;
                                    do{
                                        if(!ObjectStorage.ContainsKey(ObjectStorage.Count+ListKeyMod)) {ObjectStorage.Add(ObjectStorage.Count+ListKeyMod,args);}
                                        ListKeyMod++;
                                    }while(ObjectStorage.ContainsKey(ObjectStorage.Count+ListKeyMod));                                    
                                    Tools.getObjects.AddLast("Command: " + command + " Color: " + color + " Point: " + point + " Type: " + type + " Size: " + size);

                                    OnChangeViewList();
                                    
                                    break;
                                }

                            case "triangle":
                                {
                                    string type = args[1], color = args[2], point = args[3], size = args[4], rotation = args[5];

                                    DrawTriangle(args);
                                    int ListKeyMod = 0;
                                    do{
                                        if(!ObjectStorage.ContainsKey(ObjectStorage.Count+ListKeyMod)) {ObjectStorage.Add(ObjectStorage.Count+ListKeyMod,args);}
                                        ListKeyMod++;
                                    }while(ObjectStorage.ContainsKey(ObjectStorage.Count+ListKeyMod));
                                    Tools.getObjects.AddLast("Command: " + command + " Color: " + color + " Point: " + point + " Type: " + type + " Size: " + size);

                                    OnChangeViewList();
                                    
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


                case "rotate":
                    {
                        string object1 = args[1], rotation = args[2];
                        //requires object info, because to rotate you must redraw the object with new rotation.
                        OnClear();
                        int objectKey = int.Parse(object1);
                        string[] drawCommand = GetObject(objectKey);
                        drawCommand[5] = rotation;
                        SetObject(objectKey, drawCommand);

                        Dictionary<int,string[]> Drawings = GetObjectDict();
                        string[] s;
                        int counter = 0;
                        for(int i=0;i<200;i++)  {
                            if(Drawings.ContainsKey(i)) {
                                counter++;
                                s = GetObject(i);
                                if (s[1].Equals("square")) {
                                    DrawSquare(s);
                                }

                                if (s[1].Equals("triangle")) {
                                    DrawTriangle(s);
                                }

                                if (s[1].Equals("circle")) {
                                    DrawCircle(s);
                                }
                            }
                        }

                        OnChangeViewList();

                        OnChangeViewList();


                        break;
                    }

                case "clear":
                    {
                                                                     
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
                        int objectKey = int.Parse(object1);
                        RemoveObject(objectKey);

                        OnClear();

                        Dictionary<int,string[]> Drawings = GetObjectDict();
                        string[] s;
                        int counter = 0;
                        for(int i=0;i<200;i++)  {
                            if(Drawings.ContainsKey(i)) {
                                counter++;
                                s = GetObject(i);
                                if (s[1].Equals("square")) {
                                    DrawSquare(s);
                                }

                                if (s[1].Equals("triangle")) {
                                    DrawTriangle(s);
                                }

                                if (s[1].Equals("circle")) {
                                    DrawCircle(s);
                                }
                            }
                        }

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
                                    if (list2.Contains("objectkey"))
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
                case "clear":
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


        private void DrawSquare(string[] args) {
            string type = args[1], color = args[2], point = args[3], size = args[4], rotation = args[5];
            //create the object to draw 
            Graphics graph = control.CreateGraphics();
            //Send it to the form 
            Tools.getPen = new Pen(Commands.getColor(color));
            //graph.DrawRectangle(Tools.getPen, Shapes.Square(point,size));
                                    
            //Send it to the form 
            graph.DrawPath(Tools.getPen, Shapes.Square(point, size, "100", "90", rotation));
                              
           
            
        }

        private void DrawTriangle(string[] args) {
            string type = args[1], color = args[2], point = args[3], size = args[4], rotation = args[5];
            //create the object to draw 
            Graphics graph = control.CreateGraphics();
            //Send it to the form 
            Tools.getPen = new Pen(Commands.getColor(color));
                               
            //Send it to the form 
            graph.DrawPath(Tools.getPen, Shapes.Triangle(point, size, rotation));

            
        }

        private void DrawCircle(string[] args) {
            string type = args[1], color = args[2], point = args[3], size = args[4], rotation = args[5];
            //create the object to draw 
            Graphics graph = control.CreateGraphics();
            //Send it to the form 
            Tools.getPen = new Pen(Commands.getColor(color));
                               
            //Send it to the form 
            graph.DrawPath(Tools.getPen, Shapes.Circle(point, size));

            
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

                Point center = new Point(coords.X-Size/2,coords.Y-Size/2);

                return new Rectangle(center, new Size(Size, Size));
            }

            public static GraphicsPath Square(string coordinate, string nearRightHandSide, string ratio, string angle, string rotation) {
                
                int pointOnCentermap;
                Point coords;

                int.TryParse(coordinate, out pointOnCentermap);
                Tools.getCenterMap.TryGetValue(pointOnCentermap, out coords);

                int Size;
                int.TryParse(nearRightHandSide, out Size);

                int Rotation;
                int.TryParse(rotation, out Rotation);

                int Ratio;
                int.TryParse(ratio, out Ratio);

                int Angle;
                int.TryParse(angle, out Angle);
                
                new PointF(10,20);
                
                GraphicsPath Square = new GraphicsPath();

                Double P1deltaX = -Size/2*Ratio/100*Math.Abs(Math.Sin((double)Angle/360*2*Math.PI));
                Double P1deltaY = Size/2*Ratio/100*Math.Cos((double)Angle/360*2*Math.PI)+Size/2;
                Double P2deltaX = -P1deltaX;
                Double P2deltaY = -Size/2*Ratio/100*Math.Cos((double)Angle/360*2*Math.PI)+Size/2;

                Point P1 = CirclePointCoordinate(Convert.ToInt32((Math.PI-Math.Atan(P1deltaX/P1deltaY))*360/(2*Math.PI)+Rotation),2*Math.Sqrt(Math.Pow(P1deltaX,2)+Math.Pow(P1deltaY,2)),coords);
                Point P3 = Point.Subtract(Point.Add(coords,new Size(coords)),new Size(P1));
                Point P2 = CirclePointCoordinate(Convert.ToInt32((-Math.PI/2+Math.Atan(P2deltaX/P2deltaY))*360/(2*Math.PI)+Rotation),2*Math.Sqrt(Math.Pow(P2deltaX,2)+Math.Pow(P2deltaY,2)),coords);
                Point P4 = Point.Subtract(Point.Add(coords,new Size(coords)),new Size(P2));

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
              
                if(Tools.getCenterMap.TryGetValue(pointOnCentermap, out coords)){
                    Console.WriteLine("You did it it was there");

                }
                else
                {
                    Console.WriteLine("failed to get coords");
                }
                
                GraphicsPath Triangle = new GraphicsPath();
                
                Point P1 = CirclePointCoordinate(90+Rotation,Size,coords);
                Point P2 = CirclePointCoordinate(210+Rotation,Size,coords);
                Point P3 = CirclePointCoordinate(330+Rotation,Size,coords);
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
