namespace Eleks.MoovTheSphero.Gestures
{
    public class Gesture
    {        
        public GestureType Type { get; set; }
        public double Value { get; set; }
        public string Message { get; set; }

        public override string ToString() => $"Gesture Type: {Type}, Value: {Value}, {Message}";
    }
}