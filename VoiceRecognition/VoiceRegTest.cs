using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;
using System.Collections;

namespace VoiceToPaint.VoiceRecognition
{
    class VoiceRegTest
    {
        public delegate void NewCommandEventHandler(string text);
        public event NewCommandEventHandler NewCommand;



        SpeechRecognitionEngine masterEngine;
    
     


        Choices commands;

        public VoiceRegTest()
        {

            // Select a speech recognizer that supports English.  
            RecognizerInfo info = null;
            foreach (RecognizerInfo ri in SpeechRecognitionEngine.InstalledRecognizers())
            {
                if (ri.Culture.TwoLetterISOLanguageName.Equals("en"))
                {
                    info = ri;
                    break;
                }
            }
            if (info == null) Console.WriteLine("Din't have languagepack"); ;
            Console.WriteLine("Found this Langugepack: "+ info.Description
);             masterEngine = new SpeechRecognitionEngine(info);
           
            
  
            commands = new Choices();

        }


        public void SetGrammer(string[] grammer)
        {
            bool isInt = false;
            ArrayList commands = new ArrayList();

            foreach (string s in grammer)
            {
                int b;
                commands.Add(s);
                if (int.TryParse(s, out b)) {
                    isInt = true;
                }


            }
            if (isInt)
            {


                for (int i = 0; i < int.Parse(grammer[1]); i++)
                {
                    commands.Add("" + i);

                }


            }

            GrammarBuilder gBuilder = new GrammarBuilder();
            Choices choice = new Choices();
            string[] stringArray = (string[])commands.ToArray(typeof(string));
            choice.Add(stringArray);
            gBuilder.Append(choice);
            Grammar gram = new Grammar(gBuilder);
            masterEngine.UnloadAllGrammars();
            masterEngine.RequestRecognizerUpdate();
            masterEngine.LoadGrammarAsync(gram);
            masterEngine.RequestRecognizerUpdate();


        }

        public void startListening()
        {
           
            //set input device
            masterEngine.SetInputToDefaultAudioDevice();

            // attacth event Handelders 
            masterEngine.SpeechRecognized += masterEngine_SpeechRecognized;
            masterEngine.SpeechDetected += SpeechDetectedHandler;
            masterEngine.SpeechHypothesized += SpeechHypothesizedHandler;
            masterEngine.SpeechRecognitionRejected += SpeechRecognitionRejectedHandler;

            //Indicates whether to perform one or multiple recognition operations.
            masterEngine.RecognizeAsync(RecognizeMode.Multiple);
        
        
        }
      

        // Handle the SpeechDetected event.  
        // Raised when the recognizer detects input that it can identify as speech.
        private void SpeechDetectedHandler(object sender, SpeechDetectedEventArgs e)
        {
            
           // Console.WriteLine(" In SpeechDetectedHandler:");
           // Console.WriteLine(" - AudioPosition = {0}", e.AudioPosition);
        }
        // Handle the SpeechHypothesized event.  
        //Raised when input creates an ambiguous match with one of the active grammars.
        private void SpeechHypothesizedHandler(
          object sender, SpeechHypothesizedEventArgs e)
        {
            
           // Console.WriteLine("confidence lvl: " + e.Result.Confidence);
        }
      
        
        
        
        //Raised when the recognizer finalizes a recognition operation.
        private void masterEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {

           if( e.Result.Confidence >= 0.8)
            {
                // += not needed when only doing single words
               



                //sends new string to show on screen

                OnNewCommand(e.Result.Text);
            }


        }

        private void SpeechRecognitionRejectedHandler(
      object sender, SpeechRecognitionRejectedEventArgs e)
        {
            Console.WriteLine("I don't understand you Bitch");
        }

        public String[] addNumber(String numbers)
        {

            String[] wannabeNumbers = numbers.Split('|');

            //needs more space allocated if negative numbers are used
            String[] actualNumbers = new String[int.Parse(wannabeNumbers[1])];


            //needs to be changed if negative numbers are needed
            for (int i = 0; i < int.Parse(wannabeNumbers[1]); i++)
            {
                actualNumbers[i] = "" + i;
            }

            return actualNumbers;
        }


        protected virtual void OnNewCommand(string text)
        {
            if (NewCommand != null)
            {
                NewCommand(text);
            }

        }




    }
}
