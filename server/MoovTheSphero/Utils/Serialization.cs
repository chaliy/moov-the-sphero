using System;
using System.Collections.Concurrent;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Eleks.MoovTheSphero.Utils
{
    public static class Serialization
    {
        static readonly ConcurrentDictionary<string, Type> TypeCache = new ConcurrentDictionary<string, Type>();

        private class Envelope
        {
            public object Content { get; set; }
            public string Type { get; set; }
        }

        private class AnonymousEnvelope
        {
            public JObject Content { get; set; }
            public string Type { get; set; }
        }

        public static string SerializeWithEnvelope(object content)
        {
            var type = content.GetType().Name;
            var payload = JsonConvert.SerializeObject(new Envelope
            {
                Content = content,
                Type = type
            },
                Formatting.Indented,
                CreateSerializationSettings()
            );
            return payload;
        }

       
        public static object DeserializeEnvelope<TMarker>(string payload)
        {
            try
            {
                var envelope = JsonConvert.DeserializeObject<AnonymousEnvelope>(payload,
                    CreateSerializationSettings()
                );
                Type type;
                if (!TypeCache.TryGetValue(envelope.Type, out type))
                {
                    type = typeof(TMarker).Assembly.ExportedTypes.First(t => t.Name == envelope.Type);
                    TypeCache[envelope.Type] = type;
                }
                var contentObject = envelope.Content.ToObject(type);

                return contentObject;
            }
            catch(Exception ex)
            {
                Tracer.Error(ex);
                throw;
            }
        }

        private static JsonSerializerSettings CreateSerializationSettings()
        {
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            settings.Converters.Add(new StringEnumConverter());
            return settings;
        }
    }
}
