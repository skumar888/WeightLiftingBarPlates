using System;
using System.Collections.Generic;
using System.Text;
//using WLPBlatesManager.Model;
using System.Text.Json;
using System.Text.Json.Serialization;
using WLPBlatesManager.Model;

namespace WLBApplication.Application
{
    public class JsonParser : IJsonParser
    {

        public Plate SerializeDeserializeObject(Object obj)
        {
            return JsonSerializer.Deserialize<Plate> (JsonSerializer.Serialize(obj));
        }

        public IEnumerable<Plate> SerializeDeserializeObject(IEnumerable<Object> objects)
        {
            return JsonSerializer.Deserialize<IEnumerable<Plate>>(JsonSerializer.Serialize(objects));
        }

        public string SerializeObject(Object obj)
        {
            return JsonSerializer.Serialize(obj);
        }

        public string SerializeObjects(IEnumerable<Object> objects)
        {
            return JsonSerializer.Serialize<IEnumerable<Object>>(objects);
        }
    }
}
