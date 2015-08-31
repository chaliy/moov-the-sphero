using System;
using System.Numerics;
using System.Reactive.Subjects;
using Eleks.MoovTheSphero.Gestures.Vendor;
using Eleks.MoovTheSphero.Moov;

namespace Eleks.MoovTheSphero.Gestures
{
    public class MotionDetector
    {
        readonly Subject<MotionEvent> _motionEvents = new Subject<MotionEvent>();

        private const float SamplePeriod = 1/23.0f;

        readonly MahonyAHRS _ahrs = new MahonyAHRS(SamplePeriod);

        public MotionDetector(IObservable<MoovEvent> sensor)
        {
            sensor.Subscribe(OnNextSensor);
        }

        public IObservable<MotionEvent> MotionEvents => _motionEvents;

        private Vector3 _prevVelocity = Vector3.Zero;
        private Vector3 _prevPosition = Vector3.Zero;

        private void OnNextSensor(MoovEvent e)
        {

            _ahrs.Update(
                Deg2Rad(e.Gyroscope.X), Deg2Rad(e.Gyroscope.Y), Deg2Rad(e.Gyroscope.Z),
                e.Accelerometer.X, e.Accelerometer.Y, e.Accelerometer.Z
            );

            var quat = new Quaternion(_ahrs.Quaternion[0], _ahrs.Quaternion[1], _ahrs.Quaternion[2], _ahrs.Quaternion[3]);
            var acc = e.Accelerometer;

            var alignedAcc = Vector3.Transform(acc, Quaternion.Conjugate(quat));

            var velocity = _prevVelocity + (alignedAcc*SamplePeriod);
            _prevVelocity = velocity;

            var position = _prevPosition + (velocity*SamplePeriod);
            _prevPosition = position;

            _motionEvents.OnNext(new MotionEvent
            {
                Quaternion = quat,
                Acceleration = alignedAcc,
                Velocity = velocity,
                Position = position,
                RawAccelerometer = e.Accelerometer,                
                RawGyroscope = e.Gyroscope
            });
        }

        
        static float Deg2Rad(float degrees)
        {
            return (float)(Math.PI / 180) * degrees;
        }

    }
}
