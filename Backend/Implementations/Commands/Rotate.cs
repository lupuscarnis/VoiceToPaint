using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoiceToPaint.Backend.Implementations.Commands
{
    static class Rotate
    {

        static public void Execute(string command, Control control)
        {

            string[] args = ExtractArgs(command);
            string object1 = args[1], rotation = args[2];
            //requires object info, because to rotate you must redraw the object with new rotation.
            int objectKey = int.Parse(object1);
            DrawObject drawCommand;
            Tools.getObjects.TryGetValue(objectKey, out drawCommand);
            drawCommand.Rotation = int.Parse(rotation);
            Tools.getObjects.Remove(objectKey);
            Tools.getObjects.Add(objectKey, drawCommand);

            DrawObject s;
            for (int i = 0; i < Tools.getObjects.Count; i++)
            {
                if (Tools.getObjects.ContainsKey(i))
                {

                    if (Tools.getObjects.TryGetValue(i, out s))
                        Draw.Execute(s, control);
                }
            }

        }
        static private string[] ExtractArgs(string text)
        {

            string[] arg = new string[10];
            string[] list = new string[10];
            string[] list2 = new string[10];
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
                            arg[0] = list2[1];
                        if (list2.Contains("objectkey"))
                            arg[1] = list2[1];
                        if (list2.Contains("rotation"))
                            arg[2] = list2[1];

                    }

                }

            }



            return arg;

        }


    }
}
