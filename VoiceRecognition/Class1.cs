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
        
        SpeechRecognitionEngine masterEngine = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
        SpeechRecognitionEngine inputListener;
        public String command = "";
        

        Boolean type = false;
        //Boolean* pTxype = &type;
        Boolean color = false;
        //Boolean* pColor = &color;
        Boolean coordinate = false;
        //Boolean* pCoordinate = &coordinate;

        Boolean rectangle = false;
        Boolean connect = false;
        Boolean draw = false;
        Boolean size = false;
        Choices numbers; 

      //  public event Func<String> readToReturn;


        public VoiceRecognizer()
        {
            
            masterEngine = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
            inputListener = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
            numbers = new Choices();
            for (int i = 0; i < 100; i++)
            {
                numbers.Add("" + i);
            }
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
        public void startListening()
        {
            
            // master engine recognised lines
            Choices commands = new Choices();
            //not sure how to do this in a non-hardcoded manner, coordinates going to be a pain. 
            //Could use for loop + contains further down, but this part seems necessarily hardcoded.
            commands.Add(new String[] { "draw", "connect", "line", "triangle", "square","b 5", "red", "blue", "yellow", "size", "50" });
            commands.Add(numbers);
            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Append(commands);
            Grammar gram = new Grammar(gBuilder);
            masterEngine.LoadGrammarAsync(gram);
            masterEngine.SetInputToDefaultAudioDevice();
            masterEngine.SpeechRecognized += masterEngine_SpeechRecognized;
            masterEngine.RecognizeAsync(RecognizeMode.Multiple);
                            



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
                    if (!draw)
                    {
                        command +="command:" +e.Result.Text+", ";
                        draw = true;
                    }
                    break;

                case "connect":
                    inputListener.RecognizeAsync(RecognizeMode.Multiple);
                    masterEngine.RecognizeAsyncCancel();
                    if (!connect)
                    {
                        command += command += "command:" + e.Result.Text + ", ";
                        connect = true;
                    }
                    break;

                case "line":
                    inputListener.RecognizeAsync(RecognizeMode.Multiple);
                    masterEngine.RecognizeAsyncCancel();
                    if (!type)
                    {
                        command += "type: "+ e.Result.Text + ", ";
                        type = true;
                    }

                    break;

                case "triangle":
                    inputListener.RecognizeAsync(RecognizeMode.Multiple);
                    masterEngine.RecognizeAsyncCancel();
                    if (!type)
                    {
                        command += "type: " + e.Result.Text + ", ";
                        type = true;
                    }

                    break;

                case "square":
                    inputListener.RecognizeAsync(RecognizeMode.Multiple);
                    masterEngine.RecognizeAsyncCancel();
                    if (!type)
                    {
                        command += "type: "+ e.Result.Text + ", ";
                        type = true;
                    }

                    break;

                //case "b 5":  listens for positions
                default:
                    inputListener.RecognizeAsync(RecognizeMode.Multiple);
                    masterEngine.RecognizeAsyncCancel();
                    if (!coordinate)
                    {
                        command += "coordinate: " + e.Result.Text + ", ";
                        coordinate = true;
                    }
                    //return somehow and/or start listening for tone/volume
                   
                    //can use != notation instead, this is just for style points
                    //   readToReturn?.Invoke(command);
                    {
                        //maybe needs to be set apart from connect
                    }

                    if ((draw ^ connect) && type && color && coordinate && size)
                    {
                        Console.WriteLine(command);
                        reset();
                    }
                    else { inputListener.RecognizeAsync(RecognizeMode.Multiple); }
                    
                    break;


               
            }
        }

        private void masterEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.Beep();
            Console.WriteLine(e.ToString());

            String enpot = e.ToString();

            switch (e.Result.Text)
            {

                case "draw":
                    
                    if (!draw)
                    {
                        command += "command:" + e.Result.Text + ", ";
                        draw = true;
                    }
                    break;

                case "connect":
                  
                    if (!connect)
                    {
                        command += command += "command:" + e.Result.Text + ", ";
                        connect = true;
                    }
                    break;

                case "line":
                   
                    if (!type)
                    {
                        command += "type: " + e.Result.Text + ", ";
                        type = true;
                    }

                    break;

                case "triangle":
                   
                    if (!type)
                    {
                        command += "type: " + e.Result.Text + ", ";
                        type = true;
                    }

                    break;

                case "square":
                  
                    if (!type)
                    {
                        command += "type: " + e.Result.Text + ", ";
                        type = true;
                    }

                    break;

                case "red":
                   
                    if (!color)
                    {
                        command += "color: " + e.Result.Text + ", ";

                        color = true;
                    }
                    break;

                case "blue":
                   
                    if (!color)
                    {
                        command += "color: " + e.Result.Text + ", ";
                        color = true;
                    }
                    break;

                case "yellow":
                   
                    if (!color)
                    {
                        command += "color: " + e.Result.Text + ", ";
                        color = true;
                    }


                    break;

                default:
                   
                    if (!size)
                    {
                        command += "size: " + e.Result.Text + ", ";
                        size = true;
                    }

                    break;



            }
            if((draw && type && coordinate &&color && size) || (connect && type && coordinate && color && size))
            {
                //return or event handling to send message
                Console.WriteLine(command);
                reset();
            }

        }


        public void reset()
        {
            /*
            command = "";
            pType = false;
            pColor = false;
            pCoordinate = false;
            */

            draw = false;
            connect = false;
            type = false;
            color = false;
            coordinate = false;
            size = false;
            
        }
    
    }
}
