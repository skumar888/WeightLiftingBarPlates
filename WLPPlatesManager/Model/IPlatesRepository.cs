using System.Collections.Generic;

namespace WLPBlatesManager.Model
{
    public interface IPlatesRepository
    {
        List<Plate> GetAllPlates();
    }
}