using System;
using System.Linq;
using System.Threading.Tasks;
using Eleks.MoovTheSphero.Utils;
using Sphero.Communication;
using Sphero.Core;
using Sphero.Devices;
using Sphero.Macros;
using Sphero.Macros.Commands;

namespace Eleks.MoovTheSphero.Sphero
{
    public class SpheroManager
    {
        private SpheroDevice _device;

        private const int FLIP_MACRO = 112;

        public async Task Start()
        {
            var spheros = await SpheroConnectionProvider.DiscoverSpheros();
            var sphero = spheros.FirstOrDefault();

            if (sphero != null)
            {
                var connection = await SpheroConnectionProvider.CreateConnection(sphero);

                if (connection != null)
                {
                    connection.OnDisconnection += () => Tracer.Trace("Sphero disconnected");
                    _device = new SpheroDevice(connection);
                    Tracer.Trace("Sphero connected");

                    _device.ReinitMacroExecutive(response =>
                    {
                        if (response == MessageResponseCode.ORBOTIX_RSP_CODE_OK)
                        {
                            var flipMacro = new Macro(MacroType.Permanent, FLIP_MACRO);
                            flipMacro.Commands.Add(new SendRawMotorMacroCommand
                            {
                                LeftMode = MotorMode.Forward,
                                LeftPower = 255,
                                RightMode = MotorMode.Forward,
                                RightPower = 255,
                                PCD = 255
                            });
                            flipMacro.Commands.Add(new DelayMacroCommand { Time = 150 });
                            flipMacro.Commands.Add(new SendRawMotorMacroCommand
                            {
                                LeftMode = MotorMode.Off,
                                LeftPower = 0,
                                RightMode = MotorMode.Off,
                                RightPower = 0,
                                PCD = 255
                            });
                            flipMacro.Commands.Add(new SetStabilizationMacroCommand
                            {
                                Flag = StabilizationStatus.OnWithoutReset,
                                PCD = 255
                            });
                            _device.SaveMacro(flipMacro, null);

                            Tracer.Trace("Flip macro stored");
                        }
                    });


                    return;
                }                                
            }

            Tracer.Error("Sphero not found");
        }

        public void Roll(int angle, float speed)
        {                        
            _device.Roll(angle, speed);            
        }

        public void Spin()
        {
            if (_device == null)
            {
                throw new InvalidOperationException("Sphero not connected yet...");
            }

            //_device.RunMacro(FLIP_MACRO, null);

            var spinMacro = new Macro(MacroType.Temporary, 255);
            spinMacro.Commands.Add(new LoopStartMacroCommand
            {
                Count = 30
            });
            spinMacro.Commands.Add(new RotateOverTimeMacroCommand
            {
                Angle = 360,
                Time = 200
            });
            spinMacro.Commands.Add(new DelayMacroCommand
            {
                Time = 200
            });
            spinMacro.Commands.Add(new LoopEndMacroCommand { });


            _device.SaveTemporaryMacro(spinMacro, response =>
            {
                if (response == MessageResponseCode.ORBOTIX_RSP_CODE_OK)
                {
                    _device.RunTemporaryMacro(code => Tracer.Trace($"RunTemporaryMacro {code}"));
                }
            });
        }
    }
}
