using System;
using Eleks.MoovTheSphero.Moov;
using Eleks.MoovTheSphero.Server;

namespace Eleks.MoovTheSphero
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new ServerManager();

            var moov = new MoovManager("CyanMoov");

            moov.Sensors.Subscribe(e =>
            {
                Console.WriteLine($"OnSensors: G: X: {e.GyroscopeX}, Y: {e.GyroscopeY}, Z: {e.GyroscopeZ}; " +
                                  $"A: X: {e.AccelerationX}, Y: {e.AccelerationY}, Z: {e.AccelerationZ}");
            });

            moov.Sensors.Subscribe(server.Send);

            moov.Start();

            Console.ReadLine();
        }
        
        
    }
}
