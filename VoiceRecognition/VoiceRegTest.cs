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
        public void Test() { 
      // Create an in-process speech recognizer for the en-US locale.  
      using (SpeechRecognitionEngine recognizer =  
        new SpeechRecognitionEngine(
          new System.Globalization.CultureInfo("en-US")))  
      {  
  
        // Create and load a dictation grammar.  
        recognizer.LoadGrammar(new DictationGrammar());  
  
        // Configure input to the speech recognizer.  
        recognizer.SetInputToDefaultAudioDevice();  
  
        // Modify the initial silence time-out value.  
        recognizer.InitialSilenceTimeout = TimeSpan.FromSeconds(5);  
  
        // Start synchronous speech recognition.  
        RecognitionResult result = recognizer.Recognize();  
  
        if (result != null)  
        {  
          Console.WriteLine("Recognized text = {0}", result.Text);  
        }  
        else  
        {  
          Console.WriteLine("No recognition result available.");  
        }  
      }  
  
      Console.WriteLine();  
      Console.WriteLine("Press any key to continue...");  
      Console.ReadKey();  
    }
    }
}
