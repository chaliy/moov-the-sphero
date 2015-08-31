using System;
using System.Reactive.Linq;
using System.Threading;
using Eleks.MoovTheSphero.Csv;
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

            //var gestures = new GestureDetector(moov.SensorEvents);
            //gestures.Gestures.Subscribe(Console.WriteLine);

            var moves = new MotionDetector(moov.SensorEvents);
            moves.MotionEvents.Subscribe(server.Send);
            moves.MotionEvents.Subscribe(Console.WriteLine);

            moov.Start();
            
            //moov.SensorEvents.Buffer(TimeSpan.FromSeconds(1)).Subscribe(x =>
            //{
            //    Console.WriteLine($"{x.Count} i/s");
            //});

            //new SensorWriter(moov.SensorEvents);

            //var sphero = new SpheroManager();
            //sphero.Start().Wait(TimeSpan.FromSeconds(30));

            //moov.Keys.Subscribe(e =>
            //{
            //    if (e.KeyState == 1)
            //    {
            //        Console.WriteLine($"Spin!");
            //        sphero.Spin();
            //    }                
            //});

            Console.ReadLine();
        }
        
        
    }
}
