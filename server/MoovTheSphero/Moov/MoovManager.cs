using System;
using System.Numerics;
using System.Reactive.Subjects;
using System.Reflection;
using Moov;

namespace Eleks.MoovTheSphero.Moov
{
    public class MoovManager
    {
        private readonly string _name;
        readonly Subject<MoovEvent> _sensorEvents = new Subject<MoovEvent>();
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

        public IObservable<MoovEvent> SensorEvents => _sensorEvents;
        public IObservable<SimpleKeyServiceEventArgs> Keys => _keys;

        public void Start()
        {
            var device = new Device();
            device.GetType().GetField("deviceName", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(device, _name);

            device.OnDeviceDiscovered += (sender, eventArgs) =>
            {
                Console.WriteLine($"Discovered: {eventArgs.Discovered} C:{device.IsConnected};R:{device.IsRunning}");
                // Enable Giroscope
                device.SetSensorsConf((byte)GyroscopeAxis.XYZ);                
            };

            device.OnMotiStat += (sender, e) => Console.WriteLine("OnMotiStat");
            device.OnSensorsDataAvailable += (sender, e) =>
            {
                _sensorEvents.OnNext(new MoovEvent
                {
                    Gyroscope = new Vector3((float)e.GyroscopeX, (float)e.GyroscopeY, (float)e.GyroscopeZ),
                    Accelerometer = new Vector3((float)e.AccelerationX, (float)e.AccelerationY, (float)e.AccelerationZ)
                });
            };
            device.OnKeyEvent += (sender, e) =>
            {
                //Tracer.Trace($"Key: {eventArgs.KeyState}");
                _keys.OnNext(e);
            };

            device.Discover();
        }
    }
}
