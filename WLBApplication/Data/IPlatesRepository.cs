using System.Collections.Generic;
using System.Threading.Tasks;

namespace WLPBlatesManager.Model
{
    public interface IPlatesRepository
    {
        public Task<IEnumerable<Plate>> GetAllPlates();
        public Task<Plate> GetPlate(double weight);
    }
}