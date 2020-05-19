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
using System.Collections.Generic;
using VoiceToPaint.VoiceRecognition;
using System.Speech.Recognition;

namespace VoiceToPaint
{
    delegate string newCommandDelegate();

    public partial class Canvas : Form
    {
        Thread voice = null;
        bool drw;
        int beginX, beginY; 
        Drawables draw;
        Size boardSize = new Size(10*50+120, 10*50);
        public Canvas()
        {
            InitializeComponent();
            this.Size = boardSize;
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
            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                  // g.DrawString("" + counter, new Font("Times New Roman", 10, FontStyle.Bold), new SolidBrush(Color.Black), (j * 50) + 20, (i * 50) + 20);
                   
                    Tools.getCenterMap.Add(counter, new Point(((j+1) * 50 + 25), (i * 50 + 25)));
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
           

         



        }


        private void Canvas_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void Canvas_Shown(object sender, EventArgs e)
        {

            Controller cont = new Controller();
            Sketch scrat = new Sketch();
            cont.run(this, draw);
          
           
    
        

            richTextBox1.Text = "Commands: \n \n";

         

            cont.CommandListChanged += OnChangeRichTextBox1;
            draw.ListChanged += OnChangeRichTextBox1;
            draw.GraphicsCleared += UpdateDraw;
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

     

        private void OnChangeRichTextBox1(string[] text)
        {
            // command: draw,color: blue,point: 33,type: square,size: 55,rotation:87
            Dictionary<string,string> values = new Dictionary<string, string>();
            //connect should perhaps be called edit throughtout?
            richTextBox1.Text = "Commands:  \n";
            string[] list, list2;
            string value;
            if(!Tools.Command.Equals("")){
                list = Tools.Command.Split(',');
                    foreach(string s in list)
                {

                    list2 = s.Split(':');
                    if(list2.Length >= 2)
                    values.Add(list2[0], list2[1]);
                }

            }
            
            foreach (string s in text)
            {

                if(values.TryGetValue(s, out value)) { 
                richTextBox1.Text += s+" Value: "+value+" \n";
                }
                else
                {
                    richTextBox1.Text += s + " \n";
                }

            }
            

           

            String tempInput = "";
           DrawObject args;


            for (int i = 0; i < Tools.getObjects.Count; i++)
            {
                
                Tools.getObjects.TryGetValue(i, out args);
                tempInput += "\nNumber: " + i+ args.ToString();
            }
            Font drawFont = new Font("Arial", 16);
                        

            richTextBox1.Text += "Objects: \n \n";
            richTextBox1.Text += tempInput.ToLower();

            this.richTextBox1.SelectionStart = 133;
            this.richTextBox1.SelectionLength = 12;//this.richTextBox1.Text.Length + 50;

            richTextBox1.SelectionFont = new Font("Arial", 10, FontStyle.Bold);

            this.richTextBox1.SelectionStart = 56;
            this.richTextBox1.SelectionLength = 13;//this.richTextBox1.Text.Length + 50;

            richTextBox1.SelectionFont = new Font("Arial", 10, FontStyle.Bold);

            richTextBox1.SelectionStart = 0;
            richTextBox1.SelectionLength = 9;
            richTextBox1.SelectionFont = new Font("Arial", 10, FontStyle.Bold);
            // richTextBox1.SelectionColor = System.Drawing.Color.Red;

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.richTextBox1.SelectionStart = 10;
            this.richTextBox1.SelectionLength = 20;
            
           
            if (richTextBox1.Text.Equals(""))
            {
               
                richTextBox1.Text = "Commands: \n \n";
                richTextBox1.Text += "Draw\nRotate\nDelete \n \n";




                this.richTextBox1.SelectionStart = 0;
                this.richTextBox1.SelectionLength = 9;
                richTextBox1.SelectionFont = new Font("Arial", 10, FontStyle.Bold);

                this.richTextBox1.SelectionStart = 150;
                this.richTextBox1.SelectionLength = 12;//this.richTextBox1.Text.Length + 50;

                richTextBox1.SelectionFont = new Font("Arial", 10, FontStyle.Bold);

                this.richTextBox1.SelectionStart = 56;
                this.richTextBox1.SelectionLength = 13;//this.richTextBox1.Text.Length + 50;

                richTextBox1.SelectionFont = new Font("Arial", 10, FontStyle.Bold);
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
