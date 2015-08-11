using System.Linq;
using System.Threading.Tasks;
using Eleks.MoovTheSphero.Utils;
using Sphero.Communication;
using Sphero.Devices;

namespace Eleks.MoovTheSphero.Sphero
{
    public class SpheroManager
    {
        private SpheroDevice _device;

        public async Task Start()
        {
            var spheros = await SpheroConnectionProvider.DiscoverSpheros();
            var sphero = spheros.FirstOrDefault();

            if (sphero != null)
            {
                var connection = await SpheroConnectionProvider.CreateConnection(sphero);

                connection.OnDisconnection += () => Tracer.Trace("Sphero disconnected");
                
                _device = new SpheroDevice(connection);
            }            
        }

        public void Roll(int angle, float speed)
        {
            _device.Roll(angle, speed);            
        }
    }
}
