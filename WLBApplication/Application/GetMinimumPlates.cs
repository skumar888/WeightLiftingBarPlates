using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WLBApplication.Model;
using WLPBlatesManager.Model;

namespace WLBApplication.Application
{
    public class GetMinimumPlates : IGetMinimumPlates
    {
        private readonly IPlatesRepository _platesRepository;
        public GetMinimumPlates(IPlatesRepository platesRepository)
        {
            _platesRepository = platesRepository;
        }
        public  List<WLBMinResult> GetMinimumPairedPlatesForWeights(double[] inputWeights, double equipmentWeight)
        {
            var plates = _platesRepository.GetAllPlates();
            List<WLBMinResult> resultList = new List<WLBMinResult>();

            foreach (double i in inputWeights)
            {
                WLBMinResult result = new WLBMinResult();
                result.requestedWeight = i;
                if (i - equipmentWeight < 0)
                {
                    result.minWeightList.Add(new KeyValuePair<string, int> ("0lb", 0));
                    resultList.Add(result);
                }
                else
                    resultList.Add(GetMinimumPairedPlatesForWeight(i - equipmentWeight, (List<Plate>)plates.Result, result));
            }

            return resultList;
        }
        private WLBMinResult GetMinimumPairedPlatesForWeight(double inputWeight, List<Plate> availablePlates, WLBMinResult result)
        {
            return result;
        }
    }
}
