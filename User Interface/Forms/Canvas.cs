using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoiceToPaint
{
    public partial class Canvas : Form
    {

        LinkedList<Graphics> s = new LinkedList<Graphics>();
        public Canvas()
        {
            InitializeComponent();

          

        }

     
        bool drw;
        int beginX, beginY;

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            drw = false;

            Graphics g = this.CreateGraphics();
            Pen p = new Pen(Color.Black, 2);
            for (int i = 0; i <= ((this.Size.Width-100)/50); i++)
            {
                g.DrawLine(p, new Point(i*50, 0), new Point(i*50, this.Size.Height));

            }
            for (int i = 0; i <= ((this.Size.Height- 100) / 50); i++)
            {
                g.DrawLine(p, new Point(0, i * 50), new Point(this.Size.Width, i * 50));

            }
            int counter = 0;
           
            for (int i = 0; i <= ((this.Size.Height - 100) / 50); i++)
            {
                for(int j = 0; j <= ((this.Size.Width - 100) / 50); j++) { 
                g.DrawString(""+ counter, new Font("Times New Roman", 10, FontStyle.Bold), new SolidBrush(Color.Black), (i*50)+20,(j * 50) + 20);
                    counter++;
                }
                
            }
            s.AddLast(g);
            /*
            Graphics g = this.CreateGraphics();

            Pen p = new Pen(Color.Red, 4);

            g.DrawRectangle(p, new Rectangle(new Point(e.X, e.Y), new Size(10, 10)));

            s.AddLast(g);
            */

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            drw = true;
            beginX = e.X;
            beginY = e.Y;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

           
     
            /*
            Graphics g = this.CreateGraphics();
            Pen p = new Pen(Color.Red, 4);
            Point point1 = new Point(beginX, beginY);
            Point point2 = new Point(e.X, e.Y);
            if (drw == true)
            {
                g.DrawLine(p, point1, point2);
                beginX = e.X;
                beginY = e.Y;
            }
            */
        }
        
        public void VoiceDraw(int x, int y)
        {
          
            
            

        }

        private void z(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 'z')
            {
                undo();
            }
        }

        private void Canvas_Load(object sender, EventArgs e)
        {

        }

        private void Canvas_Layout(object sender, LayoutEventArgs e)
        {
            

        }

        private void undo()
        {
            s.Last().Clear(Color.White);
            s.RemoveLast();



        }
    }
}
