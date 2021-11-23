using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLBApplication.Model;
using WLPBlatesManager.Model;

namespace WLBApplication.Application
{
    public class GetMinimumPlates : IGetMinimumPlates
    {
        private readonly IPlatesRepository _platesRepository;

        public GetMinimumPlates(IPlatesRepository platesRepository )
        {
            _platesRepository = platesRepository;
        }
        public  List<WLBMinResult> GetMinimumPairedPlatesForWeights(List<InputWeight> inputWeights, decimal equipmentWeight, decimal dblPrecision)
        {
            int precision = Convert.ToInt32(1 / dblPrecision);
            var plates = _platesRepository.GetAllPlates();
            List<WLBMinResult> resultList = new List<WLBMinResult>();
            int maxRequestedWeight = (Convert.ToInt32( inputWeights.Max(x=>x.weight)) +1);

            WLBMinResult[] interimResultCache = new WLBMinResult[(maxRequestedWeight * precision)];//To DO:should gathered plates from model

            IntializeWLBMinResultArray(interimResultCache, (maxRequestedWeight * precision) + 1);

            GetMinimumPairedPlatesForWeight(maxRequestedWeight * precision, ((List<Plate>)plates.Result).OrderBy(p=>p.weight).ToList(), interimResultCache, precision);
            foreach (InputWeight inputWeight in inputWeights)
            {

                if (!inputWeight.isValid)
                {
                    WLBMinResult result = new WLBMinResult();
                    result.requestedWeight = inputWeight.weightName;
                    result.error = inputWeight.error;
                    resultList.Add(result);
                }
                else if (inputWeight.weight - equipmentWeight < 0)
                {
                    WLBMinResult result = new WLBMinResult();
                    result.requestedWeight = inputWeight.weightName;
                    result.error = "Requested weight is lower than bar weight";
                    resultList.Add(result);
                }
                else
                {
                    interimResultCache[Convert.ToInt32((inputWeight.weight - equipmentWeight) * precision)].requestedWeight = inputWeight.weightName;
                    resultList.Add(interimResultCache[Convert.ToInt32((inputWeight.weight - equipmentWeight) * precision)]);
                }
            }

            return resultList;
        }
        private void GetMinimumPairedPlatesForWeight(decimal inputWeight, List<Plate> availablePlates, WLBMinResult[] resultCache,int precision)
        {
            WLBMinResult tempResultPlaceholder;
            for (int i = 1; i < inputWeight; i++)
            {
                for (int j = 0; j < availablePlates.Count; j++)
                {
                    if (availablePlates[j].pairedWeight <= i / precision)
                    {
                        if (resultCache[i].platesCount > 2 + resultCache[i - Convert.ToInt32(availablePlates[j].pairedWeight * precision)].platesCount)
                        {
                            tempResultPlaceholder = resultCache[i - Convert.ToInt32(availablePlates[j].pairedWeight * precision)];
                            resultCache[i].platesCount = tempResultPlaceholder.platesCount + 2;
                            resultCache[i].minWeightList = tempResultPlaceholder.minWeightList.ToDictionary(entry => entry.Key,entry => entry.Value); //Clone dictionary
                            if (resultCache[i].minWeightList.ContainsKey(availablePlates[j].Name))
                            {
                                resultCache[i].minWeightList[availablePlates[j].Name] += 2;
                            }
                            else
                            {
                                resultCache[i].minWeightList.Add(availablePlates[j].Name, 2);
                            }

                        }

                    }
                    else
                        break;
                }
            }
        }

        private void IntializeWLBMinResultArray(WLBMinResult[] interimResultCache, int maxWeight)
        {
            interimResultCache[0] = new WLBMinResult()
            {
                platesCount = 0
            };

            for (int i = 1; i < interimResultCache.Length; i++)
            {
                interimResultCache[i] = new WLBMinResult()
                {
                    platesCount = maxWeight
                };
            }
        }



    }
}
