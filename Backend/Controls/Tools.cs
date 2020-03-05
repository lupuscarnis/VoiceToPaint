using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace VoiceToPaint.Backend.Controls
{
    
    public static class Tools
    {

        private static Pen pen = new Pen(Color.Red, 4);
        private static Color color = Color.White;
      
        
        
        public static Pen Pen { get => pen; set => pen = value; }
        public static Color Color { get => color; set => color = value; }
    }
}
