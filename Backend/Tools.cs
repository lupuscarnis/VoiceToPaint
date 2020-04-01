using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace VoiceToPaint.Backend
{
    
    static class  Tools
    {
       static Dictionary<int, Point> CenterMap = new Dictionary<int, Point>();
        static LinkedList<string> Objects = new LinkedList<string>();
        static Pen pen = null;
        static Brush brush = null;
        static bool debug = true;
        
       
        public static Dictionary<int, Point> getCenterMap { get => CenterMap; set => CenterMap = value; }
        public static LinkedList<string> getObjects { get => Objects; set => Objects = value; }
        public static Pen getPen { get => pen; set => pen = value; }
        public static Brush getBrush { get => brush; set => brush = value; }
        public static bool Debug { get => debug; set => debug = value; }
    }
}
