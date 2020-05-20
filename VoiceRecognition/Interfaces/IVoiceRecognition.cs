namespace VoiceToPaint.VoiceRecognition
{
    interface IVoiceRecognition
    {
        event VoiceRegTest.NewAudioInputEventHandler NewAudioInput;
        event VoiceRegTest.NewCommandEventHandler NewCommand;
        event VoiceRegTest.NewInputEventHandler NewInput;

        string[] addNumber(string numbers);
        void SetGrammer(string[] grammer);
        void startListening();
    }
}