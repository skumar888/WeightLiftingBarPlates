using System.Collections.Generic;
using System.Threading.Tasks;
using WLBApplication.Model;

namespace WLBApplication.Application
{
    public interface IGetMinimumPlates
    {
        List<WLBMinResult> GetMinimumPairedPlatesForWeights(List<InputWeight> inputWeights, decimal equipmentWeight, decimal precision);
    }
}