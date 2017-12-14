using System.IO;
using System.Runtime.Serialization.Json;

namespace TagsCloudVisualisation.Common
{
    public class JsonObjectSerializer : IObjectSerializer
    {
        public T Deserialize<T>(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
                return (T) new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
        }

        public byte[] Serialize<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                new DataContractJsonSerializer(typeof(T)).WriteObject(ms, obj);
                return ms.ToArray();
            }
        }
    }
}