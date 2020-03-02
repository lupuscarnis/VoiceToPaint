using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;

namespace VoiceToPaint.VR
{
   public class VoiceRecognizer
    {
        SpeechRecognitionEngine masterEngine;
        SpeechRecognitionEngine inputListener;
        public String command = "";
        Boolean type = false;
        Boolean color = false;
        Boolean coordinate = false;
        public VoiceRecognizer()
            {
           masterEngine = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
           inputListener = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));


        }
        public void startListening()
            {
            // master engine recognised lines
            Choices commands = new Choices();
            commands.Add(new String[] { "draw","b 5", "line" });
            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Append(commands);
            Grammar gram = new Grammar(gBuilder);
            masterEngine.LoadGrammarAsync(gram);
            masterEngine.SetInputToDefaultAudioDevice();
            masterEngine.SpeechRecognized += masterEngine_SpeechRecognized;
            masterEngine.RecognizeAsync(RecognizeMode.Multiple);

            //input listener recognised
            Choices inputs = new Choices();
            inputs.Add(new String[] { "red","at" });
            GrammarBuilder listenBuilder = new GrammarBuilder();
            gBuilder.Append(inputs);
            Grammar listenGrammar = new Grammar(listenBuilder);
            inputListener.LoadGrammarAsync(listenGrammar);
            inputListener.SetInputToDefaultAudioDevice();
            inputListener.SpeechRecognized += inputListener_SpeechRecognized;
          //  inputListener.RecognizeAsync(RecognizeMode.Multiple);

        }

        private void inputListener_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.Beep();
            Console.WriteLine(e.ToString());

            Console.Beep();
            switch (e.Result.Text)
            {



                case "draw":
                    inputListener.RecognizeAsync(RecognizeMode.Multiple);
                    masterEngine.RecognizeAsyncCancel();
                    break;

                case "line":
                    inputListener.RecognizeAsync(RecognizeMode.Multiple);
                    masterEngine.RecognizeAsyncCancel();
                    if (!type)
                    {
                        command += e.Result.Text+" ";
                        type = true;
                    }

                    break;

                case "b 5":
                    inputListener.RecognizeAsync(RecognizeMode.Multiple);
                    masterEngine.RecognizeAsyncCancel();
                    if(!coordinate)
                    {
                        command += e.Result.Text;
                        coordinate = true;
                    }
                    //return somehow
                    Console.WriteLine(command);

                    reset();
                    break;


                default:
                   // Console.WriteLine("i didn't get that");

                    break;
            }
        }

        private void masterEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine(e.ToString());
            Console.Beep();

            switch (e.Result.Text)
            {

                case "red":
                    masterEngine.RecognizeAsync(RecognizeMode.Multiple);
                    inputListener.RecognizeAsyncCancel();
                    if (!color)
                    {
                        command += e.Result.Text+" ";
                        color = true;
                    }
                    

                    break;

                case "at":
                    masterEngine.RecognizeAsync(RecognizeMode.Multiple);
                    inputListener.RecognizeAsyncCancel();

                    
                    break;



            }

        }

        public void reset()
        {
             command = "";
             type = false;
             color = false;
             coordinate = false;

    }

    }
}
