using System;
using System.Reactive.Subjects;
using System.Reflection;
using Eleks.MoovTheSphero.Utils;
using Moov;

namespace Eleks.MoovTheSphero.Moov
{
    public class MoovManager
    {
        private readonly string _name;
        readonly Subject<SensorsDataEventArgs> _sensors = new Subject<SensorsDataEventArgs>();
        readonly Subject<SimpleKeyServiceEventArgs> _keys = new Subject<SimpleKeyServiceEventArgs>();

        public MoovManager(string name = "Moov")
        {
            _name = name;
        }

        private enum GyroscopeAxis
        {
            X = 1,
            Y = 2,
            XY = 3,
            Z = 4,
            XZ = 5,
            YZ = 6,
            XYZ = 7
        }

        public IObservable<SensorsDataEventArgs> Sensors => _sensors;
        public IObservable<SimpleKeyServiceEventArgs> Keys => _keys;

        public void Start()
        {
            var device = new Device();
            device.GetType().GetField("deviceName", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(device, _name);

            device.OnDeviceDiscovered += (sender, eventArgs) =>
            {
                Console.WriteLine($"Discovered: {eventArgs.Discovered} C:{device.IsConnected};R:{device.IsRunning}");
                // Enable Girocope
                device.SetSensorsConf((byte)GyroscopeAxis.XYZ);
            };            

            device.OnMotiStat += (sender, eventArgs) => Console.WriteLine("OnMotiStat");
            device.OnSensorsDataAvailable += (sender, eventArgs) => _sensors.OnNext(eventArgs);            
            device.OnKeyEvent += (sender, eventArgs) =>
            {
                //Tracer.Trace($"Key: {eventArgs.KeyState}");
                _keys.OnNext(eventArgs);
            };

            device.Discover();
        }
    }
}
