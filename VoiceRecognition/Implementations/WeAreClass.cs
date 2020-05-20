using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;
using VoiceToPaint.VoiceRecognition;

namespace VoiceToPaint.VR
{

    
    public unsafe class VoiceRecognizer : IVoiceRecognition
    {
        public delegate void NewCommandEventHandler(string text);
        public event NewCommandEventHandler NewCommand;
        public delegate void NewInputEventHandler(string text);
        public event NewInputEventHandler NewInput;
        public delegate void NewAudioInputEventHandler();
        public event NewAudioInputEventHandler NewAudioInput;


        SpeechRecognitionEngine masterEngine = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
        SpeechRecognitionEngine inputListener;
        public String command = "";
        Boolean commandReady = false;
      
      
        Choices commands;
        
        public VoiceRecognizer()
        {
            
            masterEngine = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
            inputListener = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
            commands = new Choices();
            foreach (RecognizerInfo ri in SpeechRecognitionEngine.InstalledRecognizers())
            {
                Console.WriteLine(ri.Description);
            }
        }

        event VoiceRegTest.NewCommandEventHandler IVoiceRecognition.NewCommand
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        event VoiceRegTest.NewAudioInputEventHandler IVoiceRecognition.NewAudioInput
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        event VoiceRegTest.NewInputEventHandler IVoiceRecognition.NewInput
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        //probably just use startListening all the time
        private String getCommand(Boolean finishedRequired)
        {
           

            if(finishedRequired && (!commandReady))
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
                for(int i = 0; i < int.Parse(newCommands[1]); i++)
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
        
        public void startListening(String newCommands)
        {
            Console.Beep();
            Console.WriteLine("I'm listening");
            // master engine recognised lines

            String[] newNumbers; //= { "0", "0" }; String[] 
            Boolean isInputInt = false;
            //check if number
            for (int i = 0; i < 10; i++)
            {
                if (newCommands.Contains("" + i))
                {
                    isInputInt = true;
                    break;
                }
            }
            if (newCommands != "") { 
            if (isInputInt)
            {
                commands.Add(addNumber(newCommands));
            }
            else
            {
                commands.Add(newCommands.Split('|'));
            }
        }

            //System.Speech.Recognition.Choices
            GrammarBuilder gBuilder = new GrammarBuilder();
            
           /*
            if (commands.ToString == null)
            {
                commands.Add("Bananananana");
            }
            */
            gBuilder.Append(commands);
           
            Grammar gram = new Grammar(gBuilder);
            masterEngine.LoadGrammarAsync(gram);
            masterEngine.SetInputToDefaultAudioDevice();
            masterEngine.SpeechRecognized += masterEngine_SpeechRecognized;
            masterEngine.RecognizeAsync(RecognizeMode.Multiple);
            Console.WriteLine(gBuilder.DebugShowPhrases);
            //masterEngine.RecognizeAsync(RecognizeMode.Single);

        }

        public String[] addNumber(String numbers)
        {
           
           String[] wannabeNumbers = numbers.Split('|');

            //needs more space allocated if negative numbers are used
           String[] actualNumbers = new String[int.Parse(wannabeNumbers[1])];
         

            //needs to be changed if negative numbers are needed
            for (int i =0; i < int.Parse(wannabeNumbers[1]); i++)
            {
              actualNumbers[i] = ""+i;
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
        
        
        private void masterEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.Beep();

            // += not needed when only doing single words
            command += e.Result.Text;

            Console.WriteLine("i heard" + command);
            //sends new string to show on screen
               
                sendCommand(command);
              
        }

        public void sendCommand(String stringCommand)
        {
            stopListening();
            OnNewCommand(stringCommand);
           
            //reset();

        }


        public void reset()
        {
            //maybe reset string elsewhere
            commands = new Choices();
            commandReady = false;
            command = "";
          
        }

        
       //might not need to be separate from reset
        public void stopListening()
        {
            masterEngine.UnloadAllGrammars();
            masterEngine.RecognizeAsyncStop();
            commands = new Choices();
            command = "";
           
        }

        public void SetGrammer(string[] grammer)
        {
            throw new NotImplementedException();
        }

        public void startListening()
        {
            throw new NotImplementedException();
        }

        string[] IVoiceRecognition.addNumber(string numbers)
        {
            throw new NotImplementedException();
        }

        void IVoiceRecognition.SetGrammer(string[] grammer)
        {
            throw new NotImplementedException();
        }

        void IVoiceRecognition.startListening()
        {
            throw new NotImplementedException();
        }
    }
}
