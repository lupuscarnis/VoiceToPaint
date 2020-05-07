﻿using System;
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
            int counter = 0;

            for (int i = 0; i <= ((this.Size.Height - 100) / 50); i++)
            {
                for (int j = 0; j <= ((this.Size.Width - 100) / 50); j++)
                {
                    g.DrawString("" + counter, new Font("Times New Roman", 10, FontStyle.Bold), new SolidBrush(Color.Black), (i * 50) + 20, (j * 50) + 20);
                   
                    Tools.getCenterMap.Add(counter, new Point((i * 50) + 20, (j * 50) + 20));
                    counter++;


                }

            }



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

            VoiceRecognizer vr = new VoiceRecognizer();

            cont.run(this, draw, vr);
           


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
            if (Tools.Debug == false)
            this.textBox1.Visible = false;
           

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string text;
            text = textBox1.Text;
            if (text == "")
            {
                Console.WriteLine("You din't input text");
            } 
          

            if (e.KeyChar == (char)13) {
                draw.createDrawble(text);

                textBox1.Text = "";
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

      

        private void OnListViewChange(object source, EventArgs e)
        {

           
            listView1.Clear();
            int i = 0;
            foreach (string s in Tools.getObjects)
            {

                listView1.Items.Add(s + " Number: " + i);
                i++;
            }
          

        }
    }
}
