using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;

namespace VoiceToPaint.VR
{



    
    public unsafe class VoiceRecognizer
    {
        public delegate void NewCommandEventHandler(string text);
        public event NewCommandEventHandler NewCommand;



        SpeechRecognitionEngine masterEngine = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
        SpeechRecognitionEngine inputListener;
        public String command = "";
        Boolean commandReady = false;
        /*
        private int RATE = 44100; 
        private int BUFFERSIZE = (int)Math.Pow(2, 10);
        public BufferedWaveProvider bwp;
        */
        Boolean type = false;
        Boolean typeReady = false;
        //Boolean* pTxype = &type;
        Boolean color = false;
        Boolean colorReady = false;
        //Boolean* pColor = &color;
        Boolean coordinate = false;
        //Boolean* pCoordinate = &coordinate;
        Boolean point = false;
        Boolean pointReady = false;
        Boolean listen = false;
        Boolean done = false;

        Boolean rectangle = false;
        Boolean connect = false;
        Boolean draw = false;
        Boolean size = false;
        Boolean sizeReady = false;
        Choices numbers;
        Choices pointNumbers;
        Choices commands;

        //  public event Func<String> readToReturn;

        /*
                void AudioDataAvailable(object sender, WaveInEventArgs e)
                {
                    bwp.AddSamples(e.Buffer, 0, e.BytesRecorded);
                }

                public void StartListeningToMicrophone(int audioDeviceNumber = 0)
                {
                    WaveIn wi = new WaveIn();
                    wi.DeviceNumber = audioDeviceNumber;
                    wi.WaveFormat = new NAudio.Wave.WaveFormat(RATE, 1);
                    wi.BufferMilliseconds = (int)((double)BUFFERSIZE / (double)RATE * 1000.0);
                    wi.DataAvailable += new EventHandler<WaveInEventArgs>(AudioDataAvailable);
                    bwp = new BufferedWaveProvider(wi.WaveFormat);
                    bwp.BufferLength = BUFFERSIZE * 2;
                    bwp.DiscardOnBufferOverflow = true;
                    try
                    {
                        wi.StartRecording();
                    }
                    catch
                    {
                        string msg = "Could not record from audio device!\n\n";
                        msg += "Is your microphone plugged in?\n";
                        msg += "Is it set as your default recording device?";
                        Console.WriteLine(msg, "ERROR");
                    }
                }
                */

        public VoiceRecognizer()
        {
            
            masterEngine = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
            inputListener = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
            commands = new Choices();
            /*
            numbers = new Choices();
            pointNumbers = new Choices();
            for (int i = 0; i < 100; i++)
            {
                numbers.Add("" + i);
            }
            for (int i = 101; i < 1000; i++)
            {
                pointNumbers.Add("" + i);
            }
            */

            /*
            Choices commands = new Choices();
            //not sure how to do this in a non-hardcoded manner, coordinates going to be a pain. 
            //Could use for loop + contains further down, but this part seems necessarily hardcoded.
            commands.Add(new String[] { "draw", "connect", "line", "triangle", "square", "b 5" });
            commands.Add(numbers);
            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Append(commands);
            Grammar gram = new Grammar(gBuilder);
            masterEngine.LoadGrammarAsync(gram);
            masterEngine.SetInputToDefaultAudioDevice();
            masterEngine.SpeechRecognized += masterEngine_SpeechRecognized;
            masterEngine.RecognizeAsync(RecognizeMode.Multiple);
            */


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
                    Console.WriteLine(tempString[i]);
                }
                Console.WriteLine(tempString[5]+" temp");
                Console.WriteLine(commands.ToString()+" commands");
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
            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Append(commands);
            Grammar gram = new Grammar(gBuilder);
            masterEngine.LoadGrammarAsync(gram);
            masterEngine.SetInputToDefaultAudioDevice();
            masterEngine.SpeechRecognized += masterEngine_SpeechRecognized;
            //masterEngine.RecognizeAsync(RecognizeMode.Multiple);
            masterEngine.RecognizeAsync(RecognizeMode.Single);



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
            /*
            command = "";
            pType = false;
            pColor = false;
            pCoordinate = false;
            */
            //maybe reset string elsewhere
            commands = new Choices();
            commandReady = false;
            command = "";
            draw = false;
            connect = false;
            type = false;
            typeReady = false;
            color = false;
            colorReady = false;
            point = false;
            pointReady = false;
            size = false;
            sizeReady = false;
            listen = false;
            done = false;


        }

        /*
        public void stopListening()
        {
            masterEngine.RecognizeAsyncStop();
        }
       */
       //might not need to be separate from reset
        public void stopListening()
        {
           
            masterEngine.RecognizeAsyncStop();
            commands = new Choices();
            command = "";
           


        }


    }
}
