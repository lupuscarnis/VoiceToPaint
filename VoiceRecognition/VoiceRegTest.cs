using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;
namespace VoiceToPaint.VoiceRecognition
{
    class VoiceRegTest
    {
        public delegate void NewCommandEventHandler(string text);
        public event NewCommandEventHandler NewCommand;



        SpeechRecognitionEngine masterEngine;
    
        public String command = "";
        Boolean commandReady = false;


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
            Console.WriteLine("found this Langugepack: "+ info.Description
);             masterEngine = new SpeechRecognitionEngine(info);
  
            commands = new Choices();

        }

        //probably just use startListening all the time
        private String getCommand(Boolean finishedRequired)
        {


            if (finishedRequired && (!commandReady))
            {

                return "Not ready";
            }

            return command;
        }

        public void understandArray(String[] newCommands)
        {
            Boolean isInputInt = false;

            for (int i = 0; i < 10; i++)
            {
                if (newCommands.Contains("" + i))
                {
                    isInputInt = true;
                    break;
                }
            }

            if (isInputInt)
            {
                String[] tempString = new String[int.Parse(newCommands[1])];
                commands.Add((newCommands));
                for (int i = 0; i < int.Parse(newCommands[1]); i++)
                {
                    tempString[i] = "" + i;

                }

                commands.Add(tempString);
            }
            else
            {
                commands.Add(newCommands);
            }

        }
        public void SetGrammer(string[] frammer)
        {
            
            
            GrammarBuilder gBuilder = new GrammarBuilder();
            Choices choice = new Choices();
            choice.Add(frammer);

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
            
            Console.WriteLine(" In SpeechDetectedHandler:");
            Console.WriteLine(" - AudioPosition = {0}", e.AudioPosition);
        }
        // Handle the SpeechHypothesized event.  
        //Raised when input creates an ambiguous match with one of the active grammars.
        private void SpeechHypothesizedHandler(
          object sender, SpeechHypothesizedEventArgs e)
        {
            Console.WriteLine("Think this might be a thing");
        }
        //Raised when the recognizer finalizes a recognition operation.
        private void masterEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {


            // += not needed when only doing single words
            command = e.Result.Text;
            if (command.Equals("hi"))
            {
                GrammarBuilder gBuilder = new GrammarBuilder();
                Choices choice = new Choices();
                choice.Add("hello");
                Console.WriteLine("I heard " + command);
                Console.WriteLine("Now Looking for hello");
                gBuilder.Append(choice);
                Grammar gram = new Grammar(gBuilder);
                masterEngine.UnloadAllGrammars();
                masterEngine.RequestRecognizerUpdate();
                masterEngine.LoadGrammarAsync(gram);
                masterEngine.RequestRecognizerUpdate();
            }
            if (command.Equals("hello"))
            {
                Console.WriteLine("I heard " + command);
                Console.WriteLine("Now looking for hi");
                GrammarBuilder gBuilder = new GrammarBuilder();
                Choices choice = new Choices();
                choice.Add("hi");

                gBuilder.Append(choice);
                Grammar gram = new Grammar(gBuilder);
                masterEngine.UnloadAllGrammars();
                masterEngine.RequestRecognizerUpdate();
                masterEngine.LoadGrammarAsync(gram);
                masterEngine.RequestRecognizerUpdate();

            }

            
            //sends new string to show on screen

            //sendCommand(command);

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
