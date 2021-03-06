﻿using System;
using System.Collections.Generic;
using System.Collections;
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
        static IDictionary Objects = new Dictionary<int,DrawObject>();
        static Pen pen = new Pen(Color.Blue, 4);
        static Brush brush = null;
        static bool debug = false;
        static bool voice = true;
        static bool canDraw = true;
        static Canvas mainForm = null;
        static ListView listView = null;
        static Drawables draw = null;
        static string commandPath = "";
        static string command = "";
        static string lastCommand = "";
        static string lastAttribute ="";
        public static Dictionary<int, Point> getCenterMap { get => CenterMap; set => CenterMap = value; }
        public static Dictionary<int, DrawObject> getObjects { get => (Dictionary<int, DrawObject>)Objects; set => Objects = value; }
        public static Pen getPen { get => pen; set => pen = value; }
        public static Brush getBrush { get => brush; set => brush = value; }
        public static bool Debug { get => debug; set => debug = value; }
        public static bool Voice { get => voice; set => voice = value; }
        public static bool getcanDraw { get => canDraw; set => canDraw = value; }
        public static Canvas getCanvas { get => mainForm; set => mainForm = value; }
        public static ListView getlistView { get => listView; set => listView = value; }
        public static Drawables getDraw { get => draw; set => draw = value; }
        public static string Command { get => command; set => command = value; }
        public static string CommandPath { get => commandPath; set => commandPath = value; }
        public static string LastCommand { get => lastCommand; set => lastCommand = value; }
        public static string LastAttribute { get => lastAttribute; set => lastAttribute = value; }
    }
}
