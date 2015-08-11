using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using Moov;

namespace Eleks.MoovTheSphero.Gestures
{
    public class GestureDetector
    {
        readonly Subject<Gesture> _gestures = new Subject<Gesture>();

        readonly Queue<Event> _events = new Queue<Event>();
        double _prevZ;

        public GestureDetector(IObservable<SensorsDataEventArgs> sensor)
        {
            sensor.Subscribe(OnNextSensor);
        }

        private class Event
        {
            public DateTime Time { get; set; }
            public SensorsDataEventArgs Data { get; set; }
        }

        public IObservable<Gesture> Gestures => _gestures;

        
        private void OnNextSensor(SensorsDataEventArgs e)
        {
            AddEvent(e);

            DetectMoveLeftRight();
        }

        private void DetectMoveLeftRight()
        {
            var newZ = _events.Select(x => x.Data.GyroscopeZ).Average();
            var deltaZ = newZ - _prevZ;
            if (Math.Abs(deltaZ) > 10)
            {
                var message = $"{deltaZ} {_prevZ} {newZ}";
                if (deltaZ > 0)
                {
                    _gestures.OnNext(new Gesture
                    {
                        Type = GestureType.MoveRight,
                        Value = deltaZ,
                        Message = message
                    });
                }
                else
                {
                    _gestures.OnNext(new Gesture
                    {
                        Type = GestureType.MoveLeft,
                        Value = -deltaZ,
                        Message = message
                    });
                }
            }
            _prevZ = newZ;
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
