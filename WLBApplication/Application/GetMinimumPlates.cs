using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLBApplication.Cache;
using WLBApplication.Model;
using WLBLoggingService;
using WLPBlatesManager.Model;

namespace WLBApplication.Application
{
    public class GetMinimumPlates : IGetMinimumPlates
    {
        private readonly IWLBMinWeightCache _WLBMinWeightCache;
        private readonly ILoggerManager _loggerManager;
        private readonly IJsonParser _jsonParser;
        public GetMinimumPlates(IWLBMinWeightCache WLBMinWeightCache, ILoggerManager loggerManager, IJsonParser jsonParser)
        {
            _WLBMinWeightCache = WLBMinWeightCache;
            _loggerManager = loggerManager;
            _jsonParser = jsonParser;
        }
        public  List<WLBMinResult> GetMinimumPairedPlatesForWeights(List<InputWeight> inputWeights, decimal equipmentWeight, decimal dblPrecision, List<Plate> plates)
        {
            _loggerManager.LogInfo($"Starting calculation for min plates using plates:{_jsonParser.SerializeObjects(plates)}");

            decimal precision = 1 / dblPrecision;
            List<WLBMinResult> resultList = new List<WLBMinResult>();
            int maxRequestedWeight = (Convert.ToInt32( inputWeights.Max(x=>x.weight)) );

            WLBMinResult[] interimResultCache = new WLBMinResult[Convert.ToInt32(maxRequestedWeight * precision)];

            if (_WLBMinWeightCache.PeakWLBMinWeightResultCache() >= (maxRequestedWeight * precision)  )
                interimResultCache = _WLBMinWeightCache.GetWLBMinWeightResultCache();
            else
            {
                IntializeWLBMinResultArray(interimResultCache, Convert.ToInt32(maxRequestedWeight * precision) + 1);
                GetMinimumPairedPlatesForWeight(maxRequestedWeight * precision, plates.OrderBy(p => p.weight).ToList(), interimResultCache, precision);
                _WLBMinWeightCache.AddWLBMinWeightResultCache(interimResultCache); 
            }
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
            _loggerManager.LogInfo($"Result calculation complete:{_jsonParser.SerializeObjects(resultList)}");
            return resultList;
        }
        private void GetMinimumPairedPlatesForWeight(decimal inputWeight, List<Plate> availablePlates, WLBMinResult[] resultCache,decimal precision)
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
                    platesCount = int.MaxValue
                };
            }
        }



    }
}
