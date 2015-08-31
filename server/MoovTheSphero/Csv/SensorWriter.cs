using System;
using System.IO;
using Eleks.MoovTheSphero.Utils;
using Moov;

namespace Eleks.MoovTheSphero.Csv
{
    public class SensorWriter
    {
        private int _counter;
        public SensorWriter(IObservable<SensorsDataEventArgs> sensor)
        {
            sensor.Subscribe(OnNextEvent);
        }

        private void OnNextEvent(SensorsDataEventArgs e)
        {
            _counter++;
            File.AppendAllText("Data.csv", $"{_counter}, {e.GyroscopeX.ToInvString()}, {e.GyroscopeY.ToInvString()}, {e.GyroscopeZ.ToInvString()}, {e.AccelerationX.ToInvString()}, {e.AccelerationY.ToInvString()}, {e.AccelerationZ.ToInvString()}\r\n");            
        }
    }
}
