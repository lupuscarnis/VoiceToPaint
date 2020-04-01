﻿using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using VoiceToPaint.Backend;

namespace VoiceToPaint
{

    public partial class Canvas : Form
    {
        Thread backend = null;
        bool drw;
        int beginX, beginY;
     
       
         
        public Canvas()
        {
            InitializeComponent();
            Tools.getCanvas = this;

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
        
        public void UpdateDraw(string s)
        {

            Console.WriteLine(s);
            Graphics g = this.CreateGraphics();
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
        /*
        public void Draw(string text)
        {
            string[] arrayList = new string[2];
            
            int value;

            arrayList = text.Split(' ');

            this.textBox1.Text = "";
            if (int.TryParse(arrayList[0], out value))
            {
                Tools.getPen = new Pen(Commands.getColor(arrayList[1]), 4);
                Graphics r = this.CreateGraphics();
                var Center = new Point();
                Tools.getCenterMap.TryGetValue(value, out Center);
                r.DrawRectangle(Tools.getPen, new Rectangle(Center, new Size(50, 50)));
            }


        }
        */
        private void z(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 'z')
            {
             
            }
        }

        private void Canvas_Load(object sender, EventArgs e)
        {

        }

        private void Canvas_Layout(object sender, LayoutEventArgs e)
        {

        




        }

        private void Canvas_Shown(object sender, EventArgs e)
        {


            
            UpdateDraw("");
             backend = new Thread(new ThreadStart(Program.ThreadExample.ThreadProc));
            backend.Start();
           while(Tools.getDraw == null) { }
            Tools.getDraw.ListChanged += OnListViewChange;
            Tools.getDraw.createDrawble("");


            if (Backend.Tools.Debug == false)
            this.textBox1.Visible = false;

            



        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string text;
           if(e.KeyChar == (char)13) {

                
            }


        }

        private void Canvas_FormClosing(object sender, FormClosingEventArgs e)
        {
            backend.Join();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
      

        private void OnListViewChange(object source, EventArgs e)
        {
            int i = 0;
           foreach(string s in Tools.getObjects)
            {
                
                listView1.Items.Add(s+" Number: "+i);
                i++;
            }


        }
    }
}
