using System.Numerics;

namespace Eleks.MoovTheSphero.Gestures
{
    public class MotionEvent
    {
        public Quaternion Quaternion { get; set; }
        public Vector3 Velocity { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Acceleration { get; set; }
        public Vector3 RawAccelerometer { get; set; }
        public Vector3 RawGyroscope { get; set; }

        public override string ToString() => $"Acceleration: {Acceleration}";
    }
}
