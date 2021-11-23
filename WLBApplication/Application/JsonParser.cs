using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
//using WLPBlatesManager.Model;
using System.Text.Json;
using System.Text.Json.Serialization;
using WLBApplication.Model;
using WLPBlatesManager.Model;

namespace WLBApplication.Application
{
    public class JsonParser : IJsonParser
    {

        public Plate SerializeDeserializeObject(Object obj)
        {
            return JsonConvert.DeserializeObject<Plate> (JsonConvert.SerializeObject(obj));
        }

        public IEnumerable<Plate> SerializeDeserializeObject(IEnumerable<Object> objects)
        {
            return JsonConvert.DeserializeObject<IEnumerable<Plate>>(JsonConvert.SerializeObject(objects));
        }

        public string SerializeObject(Object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public string SerializeObjects(IEnumerable<Object> objects)
        {
            return JsonConvert.SerializeObject(objects);
        }

        public IEnumerable<object> SerializeDeserializeWLBObject(IEnumerable<Object> objects)
        {
            return JsonConvert.DeserializeObject<IEnumerable<object>>(JsonConvert.SerializeObject(objects));
        }
    }
}
