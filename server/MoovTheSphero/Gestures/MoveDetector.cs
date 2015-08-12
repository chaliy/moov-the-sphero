using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Moov;

namespace Eleks.MoovTheSphero.Gestures
{
    public class MoveDetector
    {
        readonly Subject<Move> _moves = new Subject<Move>();

        readonly Queue<Event> _events = new Queue<Event>();
        double _prevZ;

        public MoveDetector(IObservable<SensorsDataEventArgs> sensor)
        {
            sensor.Buffer(TimeSpan.FromSeconds(2)).Subscribe(OnNextBuffer);
        }

        private class Event
        {
            public DateTime Time { get; set; }
            public SensorsDataEventArgs Data { get; set; }
        }

        public IObservable<Move> Moves => _moves;


        private void OnNextBuffer(IList<SensorsDataEventArgs> events)
        {
            events.Max(x => x.GyroscopeZ);
        }


        private void OnNextSensor(SensorsDataEventArgs e)
        {
            AddEvent(e);
            
                        
        }
        
        private void AddEvent(SensorsDataEventArgs e)
        {
            _events.Enqueue(new Event
            {
                Time = DateTime.Now,
                Data = e
            });

            // Drop oldies
            while (true)
            {
                if (_events.Peek().Time > DateTime.Now.AddSeconds(-1))
                {                    
                    break;                    
                }
                _events.Dequeue();
            }
        }
        
    }
}
