using System.Collections.Generic;

namespace VoiceToPaint.Backend
{
    interface IDrawables
    {
        event Drawables.UpdateGraphicEventHandler GraphicsCleared;
        event Drawables.UpdateViewListEventHandler ListChanged;

        void createDrawble(string text);
        DrawObject GetObject(int key);
        Dictionary<int, DrawObject> GetObjectDict();
        void RemoveObject(int key);
        void SetObject(int key, DrawObject args);
    }
}