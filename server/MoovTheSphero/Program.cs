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

            var gestures = new GestureDetector(moov.Sensors);
            gestures.Gestures.Subscribe(Console.WriteLine);

            moov.Sensors.Subscribe(server.Send);
            moov.Start();

            //var sphero = new SpheroManager();
            //sphero.Start();

            Console.ReadLine();
        }
        
        
    }
}
