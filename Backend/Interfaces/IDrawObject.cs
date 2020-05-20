namespace VoiceToPaint.Backend
{
    interface IDrawObject
    {
        string Color { get; set; }
        string Inputtext { get; set; }
        int Point { get; set; }
        int Rotation { get; set; }
        int Size { get; set; }
        string Type { get; set; }

        string ToString();
    }
}