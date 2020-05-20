using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoiceToPaint.Backend
{
    class DrawObject : IDrawObject
    {


        public DrawObject()
        {

        }

        public DrawObject(string type, string color, int point, int size, int rotation)
        {
            this.type = type;
            this.color = color;
            this.point = point;
            this.size = size;
            this.rotation = rotation;
        }
        public DrawObject(string type, string color, int point, int size, int rotation, string inputtext)
        {
            this.type = type;
            this.color = color;
            this.point = point;
            this.size = size;
            this.rotation = rotation;
            this.inputtext = inputtext;
        }
        private string inputtext;
        private string type;
        private string color;
        private int point;
        private int size;
        private int rotation;

        public string Type { get => type; set => type = value; }
        public string Color { get => color; set => color = value; }
        public int Point { get => point; set => point = value; }
        public int Size { get => size; set => size = value; }
        public int Rotation { get => rotation; set => rotation = value; }
        public string Inputtext { get => inputtext; set => inputtext = value; }



        override public string ToString()
        {
            return "\nType: " + type + "\nColor: " + color + "\nPoint: " + point + "\nSize: " + size + "\nrotation: " + rotation;
        }

    }
}
