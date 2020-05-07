using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace VoiceToPaint.Backend
{
    
    static class  Tools
    {
        
        static Dictionary<int, Point> CenterMap = new Dictionary<int, Point>();
        static LinkedList<string> Objects = new LinkedList<string>();
        static Pen pen = new Pen(Color.Blue, 4);
        static Brush brush = null;
        static bool debug = true;
        static bool voice = true;
        static bool canDraw = true;
        static Canvas mainForm = null;
        static ListView listView = null;
        static Drawables draw = null;
        static string command;
        public static Dictionary<int, Point> getCenterMap { get => CenterMap; set => CenterMap = value; }
        public static LinkedList<string> getObjects { get => Objects; set => Objects = value; }
        public static Pen getPen { get => pen; set => pen = value; }
        public static Brush getBrush { get => brush; set => brush = value; }
        public static bool Debug { get => debug; set => debug = value; }
        public static bool Voice { get => voice; set => voice = value; }
        public static bool getcanDraw { get => canDraw; set => canDraw = value; }
        public static Canvas getCanvas { get => mainForm; set => mainForm = value; }
        public static ListView getlistView { get => listView; set => listView = value; }
        public static Drawables getDraw { get => draw; set => draw = value; }
        public static string Command { get => command; set => command = value; }
    }
}
