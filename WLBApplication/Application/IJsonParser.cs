using System.Collections.Generic;

namespace WLBApplication.Application
{
    public interface IJsonParser
    {
        string SerializeObject(IEnumerable<object> objects);
        string SerializeObject(object obj);
    }
}