using System.Collections.Generic;

namespace VoiceToPaint.Backend
{
    interface IDrawables
    {
        event Drawables.UpdateGraphicEventHandler GraphicsCleared;
        event Drawables.UpdateViewListEventHandler ListChanged;

        void createDrawble(string text);
       
    }
}