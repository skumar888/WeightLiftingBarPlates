using System;
using System.Collections.Generic;
using System.Text;
//using WLPBlatesManager.Model;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace WLBApplication.Application
{
    public class JsonParser : IJsonParser
    {

        public string SerializeObject(Object obj)
        {
            return JsonSerializer.Serialize(obj);
        }

        public string SerializeObject(IEnumerable<Object> objects)
        {
            StringBuilder serializedObject = new StringBuilder();
            foreach (Object o in objects)
            {
                serializedObject.Append(JsonSerializer.Serialize(o));
            }
            return serializedObject.ToString();
        }
    }
}
