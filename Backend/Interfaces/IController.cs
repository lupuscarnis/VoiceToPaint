using VoiceToPaint.VoiceRecognition;

namespace VoiceToPaint.Backend
{
    interface IController
    {
        event Controller.CommandListEventHandler CommandListChanged;

        void PushCommand(string command);
        void run(Canvas cv, IDrawables draw, IVoiceRecognition vr);
    }
}