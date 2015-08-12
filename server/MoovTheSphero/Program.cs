using System;
using System.Threading;
using Eleks.MoovTheSphero.Gestures;
using Eleks.MoovTheSphero.Moov;
using Eleks.MoovTheSphero.Server;
using Eleks.MoovTheSphero.Sphero;

namespace Eleks.MoovTheSphero
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new ServerManager();

            var moov = new MoovManager("CyanMoov");

            //var gestures = new GestureDetector(moov.Sensors);
            //gestures.Gestures.Subscribe(Console.WriteLine);

            moov.Sensors.Subscribe(server.Send);
            moov.Start();

            var sphero = new SpheroManager();
            sphero.Start().Wait(TimeSpan.FromSeconds(30));

            moov.Keys.Subscribe(e =>
            {
                if (e.KeyState == 1)
                {
                    Console.WriteLine($"Spin!");
                    sphero.Spin();
                }                
            });

            Console.ReadLine();
        }
        
        
    }
}
