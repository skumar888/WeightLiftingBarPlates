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

        public Plate SerializeObject(Object obj)
        {
            return JsonSerializer.Deserialize<Plate> (JsonSerializer.Serialize(obj));
        }

        public IEnumerable<Plate> SerializeObject(IEnumerable<Object> objects)
        {
            return JsonSerializer.Deserialize<IEnumerable<Plate>>(JsonSerializer.Serialize(objects));
        }
    }
}
