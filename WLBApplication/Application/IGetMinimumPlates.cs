using System.Collections.Generic;
using System.Threading.Tasks;
using WLBApplication.Model;

namespace WLBApplication.Application
{
    public interface IGetMinimumPlates
    {
        List<WLBMinResult> GetMinimumPairedPlatesForWeights(double[] inputWeights, double equipmentWeight);
    }
}