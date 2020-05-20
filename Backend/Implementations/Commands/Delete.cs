using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoiceToPaint.Backend.Implementations.Commands
{
  static  class Delete
    {

        static public void Execute(string command, Control control)
        {



            string[] args = ExtractArgs(command);
            string object1 = args[1];
            int objectKey = int.Parse(object1);
            Tools.getObjects.Remove(objectKey);




            DrawObject s;
            IDictionaryEnumerator myEnumerator =
                  Tools.getObjects.GetEnumerator();
            while (myEnumerator.MoveNext())
            {

                s = (DrawObject)myEnumerator.Value;
                Draw.Execute(s, control);

            }



        }

        static private string[] ExtractArgs(string text)
        {
            string[] arg = new string[3];
            string[] list = new string[2];
            string[] list2 = new string[2];
            if (text == null || text == "")
            {
                Console.WriteLine("An empty String");


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
                            arg[0] = list2[1];
                        if (list2.Contains("objectkey"))
                            arg[1] = list2[1];

                    }

                }

            }

            return arg;

        }
       static private void RemoveObject(int key)
        {
            if (Tools.getObjects.ContainsKey(key))
            {
                Tools.getObjects.Remove(key);
            }
        }



    }
}
