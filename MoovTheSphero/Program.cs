using System;
using MoovTheSphero.Moov;

namespace MoovTheSphero
{
    class Program
    {
        static void Main(string[] args)
        {
            var moov = new MoovService("CyanMoov");

            moov.Sensors.Subscribe(e =>
            {
                Console.WriteLine($"OnSensors: G: X: {e.GyroscopeX}, Y: {e.GyroscopeY}, Z: {e.GyroscopeZ}; " +
                                  $"A: X: {e.AccelerationX}, Y: {e.AccelerationY}, Z: {e.AccelerationZ}");
            });

            moov.Start();

            Console.ReadLine();
        }
        
        
    }
}
