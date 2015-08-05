using System;
using Moov;

namespace PlayWithMoov
{
    class Program
    {
        static void Main(string[] args)
        {
            Go();

            Console.Read();
        }

        private static void Go()
        {
            var device = new Device();
            device.OnDeviceDiscovered += (sender, eventArgs) =>
            {
                Console.WriteLine("Discovered: " + eventArgs.Discovered + " C:" + device.IsConnected + ";R:" + device.IsRunning);


                // Enable Girocope
                device.SetSensorsConf((byte)GyroscopeAxis.XYZ);
            };
            device.OnMotiStat += (sender, eventArgs) => Console.WriteLine("OnMotiStat");
            device.OnSensorsDataAvailable += (sender, eventArgs) => Console.WriteLine("OnSensorsDataAvailable");
            device.OnKeyEvent += (sender, eventArgs) => Console.WriteLine("OnKeyEvent");

            for (var i = 0; i < 10; i++)
            {
                device.Discover();
                Console.Read();
            }

        }

        public enum GyroscopeAxis
        {
            X = 1,
            Y = 2,
            XY = 3,
            Z = 4,
            XZ = 5,
            YZ = 6,
            XYZ = 7
        }
    }
}
