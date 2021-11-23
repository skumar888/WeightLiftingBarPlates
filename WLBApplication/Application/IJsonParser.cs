using System.Collections.Generic;
using WLPBlatesManager.Model;
using System;
using WLBApplication.Model;

namespace WLBApplication.Application
{
    public interface IJsonParser
    {
        public IEnumerable<Plate> SerializeDeserializeObject(IEnumerable<Object> objects);
        public Plate SerializeDeserializeObject(Object obj);
        public string SerializeObject(Object obj);
        public string SerializeObjects(IEnumerable<Object> objects);
        public IEnumerable<object> SerializeDeserializeWLBObject(IEnumerable<Object> objects);
    }
}