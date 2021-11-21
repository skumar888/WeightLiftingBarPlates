using System.Collections.Generic;
using WLPBlatesManager.Model;

namespace WLBApplication.Application
{
    public interface IJsonParser
    {
        public IEnumerable<Plate> SerializeObject(IEnumerable<object> objects);
        public Plate SerializeObject(object obj);
    }
}