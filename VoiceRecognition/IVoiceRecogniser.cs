using System;

public interface IVoiceRecognizer
{
    event Func<string> readToReturn;

    void reset();
    void startListening();
}