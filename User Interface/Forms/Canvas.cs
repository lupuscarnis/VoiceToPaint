using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using VoiceToPaint.VR;
using VoiceToPaint.Backend;
using VoiceToPaint.User_Interface.Forms;

namespace VoiceToPaint
{
    delegate string newCommandDelegate();

    public partial class Canvas : Form
    {
        Thread voice = null;
        bool drw;
        int beginX, beginY; 
        Drawables draw;
   
        public Canvas()
        {
            InitializeComponent();
          
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            drw = false;
            
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            drw = true;
            beginX = e.X;
            beginY = e.Y;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

           if(Tools.getcanDraw == true) { 
     
            
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
            }
        }
        
        private void UpdateDraw(object source, EventArgs e)
        {
            Tools.getCenterMap.Clear();
            
            Graphics g = this.CreateGraphics();
            g.Clear(Color.White);
            Pen p = new Pen(Color.Black, 2);
            for (int i = 0; i <= ((this.Size.Width - 100) / 50); i++)
            {
                g.DrawLine(p, new Point(i * 50, 0), new Point(i * 50, this.Size.Height));

            }
            for (int i = 0; i <= ((this.Size.Height - 100) / 50); i++)
            {
                g.DrawLine(p, new Point(0, i * 50), new Point(this.Size.Width, i * 50));

            }
            int counter = 1;
            int drawCounter = 0;
            //just for adding
            for (int i = 0; i <= ((this.Size.Height - 100) / 50); i++)
            {
                for (int j = 1; j <= ((this.Size.Width - 100) / 50); j++)
                {
                  // g.DrawString("" + counter, new Font("Times New Roman", 10, FontStyle.Bold), new SolidBrush(Color.Black), (j * 50) + 20, (i * 50) + 20);
                   
                    Tools.getCenterMap.Add(counter, new Point((j * 50) + 20, (i * 50) + 20));
                    counter++;


                }

            }
            //just for drawing
            
            for (int i = 0; i <= ((this.Size.Height - 100) / 50); i++)
            {
                g.DrawString("" + drawCounter, new Font("Times New Roman", 10, FontStyle.Bold), new SolidBrush(Color.Black), (i * 50) + 20, (0 * 50) + 20);
                drawCounter++;


            }
            drawCounter = 0;
            for (int j = 0; j <= ((this.Size.Width - 100) / 50); j++)
            {

                g.DrawString("" + drawCounter, new Font("Times New Roman", 10, FontStyle.Bold), new SolidBrush(Color.Black), (0 * 50) + 20, (j * 50) + 20);

               
                drawCounter++;

            }

            richTextBox1.Text = "";

        }
     
        private void z(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 'z')
            {
             
            }
        }

        private void Canvas_Load(object sender, EventArgs e)
        {
            draw = new Drawables(this);
            Controller cont = new Controller();
            Sketch scrat = new Sketch();

         

            cont.run(this, draw);


            draw.ListChanged += OnListViewChange;
            draw.GraphicsCleared += UpdateDraw;
        

        }

      
        private void Canvas_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void Canvas_Shown(object sender, EventArgs e)
        {
            
            // what happends when the Canvas is shown
            UpdateDraw(null,null);
          
           
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string text;
            text = textBox1.Text;
            
            if (e.KeyChar == (char)13) {
                if (text == "")
                {
                    Console.WriteLine("You din't input text");
                }
                else
                {

                    draw.createDrawble(text);

                    textBox1.Text = "";
                }            
            }


        }

        private void Canvas_FormClosing(object sender, FormClosingEventArgs e)
        {
                     
        }


        private void Canvas_Resize(object sender, EventArgs e)
        {
            if (this.Size.Height != this.Size.Width)
            {
                this.Size = new Size(this.Size.Width, this.Size.Width);
            }
            UpdateDraw(null,null);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void OnListViewChange(object source, EventArgs e)
        {
          

            /*
             listView1.Clear();
             int i = 0;
             foreach (string s in Tools.getObjects)
             {

                 listView1.Items.Add(s + " Number: " + i);

                 i++;
             }
              // listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
             listView1.AutoResizeColumns( ColumnHeaderAutoResizeStyle.HeaderSize);
             */
            // listView1.AutoResizeColumns( ColumnHeaderAutoResizeStyle.ColumnContent);
            changeRichTextBox1(source, e);

        }
            

        private void changeRichTextBox1(object sender, EventArgs e)
        {
            // command: draw,color: blue,point: 33,type: square,size: 55,rotation:87


            //connect should perhaps be called edit throughtout?
            richTextBox1.Text = "Draw, type, color, point, size, rotation \n \nDelete, object number, \n \nClear \n \nRotate, object number \n \n";
            richTextBox1.Text += "Connect, object number \n \n";
            
            String tempInput = "";
            String[] prettyStrings = tempInput.Split(' ');
           
            int i = 0;
            foreach (string s in Tools.getObjects)
            {
                tempInput += s.Remove(0, 14) + "\n" + "Number: " + i + "\n" + "\n ";
               
                i++;
            }
            Font drawFont = new Font("Arial", 16);
            richTextBox1.SelectionStart = 0; //Next section to format
            richTextBox1.SelectionLength = 10;
            richTextBox1.SelectionFont = new System.Drawing.Font("Arial", 16);

            this.richTextBox1.SelectionStart = 10;
            this.richTextBox1.SelectionLength = 20; // this.richTextBox1.SelectionLength;
            this.richTextBox1.SelectionFont = new System.Drawing.Font("Maiandra GD", 12);

            richTextBox1.Text += "Objects \n \n";
            richTextBox1.Text += tempInput;

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Equals(""))
            {
                richTextBox1.Text = "Draw, type, color, point, size, rotation \n \nDelete, object number, \n \nClear \n \nRotate, object number \n \n";
                richTextBox1.Text += "Connect, object number \n \n";

            }
            

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //richTextBox1.Text = "Draw, type, color, point, size, rotation \n \nDelete, object number, \n \nClear \n \nRotate, object number \n \n";
            //richTextBox1.Text += "Connect, object number \n \n";

        }

        void view_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            Font drawFont = new Font("Arial", 16);
            e.Graphics.DrawString(e.Item.Text, drawFont, Brushes.Black,
                new RectangleF(e.Item.Position.X,
                    e.Item.Position.Y,
                    20,
                    160));

        }
    }
}
