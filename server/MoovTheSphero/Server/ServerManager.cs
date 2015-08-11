using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using Eleks.MoovTheSphero.Utils;
using Fleck;

namespace Eleks.MoovTheSphero.Server
{
    public class ServerManager
    {
        readonly List<IWebSocketConnection> _clients = new List<IWebSocketConnection>();
        readonly Subject<object> _commandReceivedSubject = new Subject<object>();

        public ServerManager()
        {
            Initialize();
        }

        private void Initialize()
        {
            var server = new WebSocketServer("ws://0.0.0.0:8282");

            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    _clients.Add(socket);
                };

                socket.OnClose = () =>
                {
                    _clients.Remove(socket);
                };

                socket.OnMessage = message =>
                {
                    //var eventObject = Serialization.DeserializeEnvelope<ContractMarker>(message);
                    //_commandReceivedSubject.OnNext(eventObject);

                    Tracer.Trace(message);
                };

                socket.OnBinary = data =>
                {
                    Tracer.Trace("Binary data sent from client o_0");
                };
            });
        }

        public IObservable<object> CommandRecieved
        {
            get { return _commandReceivedSubject; }
        }

        private void SendInternal(string message)
        {
            foreach (var client in _clients)
            {
                client.Send(message);
            }
        }

        public void Send(object content)
        {
            Tracer.Info("Send: " + content.GetType().Name + " > " + content);
            var payload = Serialization.SerializeWithEnvelope(content);
            SendInternal(payload);
        }
    }
}
