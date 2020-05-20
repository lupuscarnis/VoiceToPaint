using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using VoiceToPaint;
using System.IO;
using System.Drawing.Drawing2D;
using VoiceToPaint.Backend.Implementations.Commands;



namespace VoiceToPaint.Backend
{
    class Drawables : IDrawables
    {
        Control control;
        private IDictionary<int, DrawObject> ObjectStorage;

        public Drawables(Control control)
        {
            ObjectStorage = Tools.getObjects;
            this.control = control;
        }

        public delegate void UpdateViewListEventHandler(string[] list);
        public event UpdateViewListEventHandler ListChanged;
        public delegate void UpdateGraphicEventHandler(object source, EventArgs e);
        public event UpdateGraphicEventHandler GraphicsCleared;




        protected virtual void OnClear()
        {
            if (GraphicsCleared != null)
            {
                GraphicsCleared(this, EventArgs.Empty);
            }

        }


        protected virtual void OnChangeViewList()
        {
            if (ListChanged != null)
            {
                ListChanged(new string[0]);
            }

        }


        public void createDrawble(string text)
        {
            

            

            string command = extractCommand(text); 


            switch (command)
            {
                case "draw":
                    {


                        Draw.Execute(text, control);
                        OnChangeViewList();

                        break;
                    }

                case "connect":
                    {

                        Connect.Execute(text, control);
                        break;
                    }


                case "rotate":
                    {
                        
                        //requires object info, because to rotate you must redraw the object with new rotation.
                        OnClear();
                        Rotate.Execute(text, control);


                        OnChangeViewList();


                        break;
                    }

                case "clear":
                    {
                        Clear.Execute();
                        //clears the borad and redraws it with a delegate
                        OnClear();
                        //Update the List // clears it 
                        OnChangeViewList();
                        break;
                    }

                case "delete":
                    {


                        // only clears the Canvas not thing stored
                        OnClear();
                        Delete.Execute(text, control);

                        //Update the List // clears it 
                        OnChangeViewList();

                        break;
                    }

            }


        }
       


        private string extractCommand(string text)
        {
            string[] list, list2;

            if (text == null || text == "")
            {
                Console.WriteLine("An empty String");
                Console.WriteLine("Using Default");

            }
            else
            {
                list = text.Split(',');
                foreach (string s in list)
                {
                    if (s != "")
                    {
                        list2 = s.Split(':');

                        if (list2.Contains("command"))
                        {
                            return list2[1];
                        }

                    }

                }

            }
            return "";
        }








    }
}
