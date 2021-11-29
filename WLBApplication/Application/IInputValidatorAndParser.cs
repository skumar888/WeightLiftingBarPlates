using System.Collections.Generic;
using WLBApplication.Model;
using WLPBlatesManager.Model;

namespace WLBApplication.Application
{
    public interface IInputValidatorAndParser
    {
        List<InputWeight> ValidateAndParseWeight(string inputString, decimal maxAllowedWeight, List<Plate> availablePlates, decimal equipmentWeight, decimal inputWeightsGCD);
        public decimal GetPrecision(decimal[] inputWeights);
    }
}