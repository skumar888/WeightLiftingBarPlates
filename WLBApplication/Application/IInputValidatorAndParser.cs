using System.Collections.Generic;
using WLBApplication.Model;

namespace WLBApplication.Application
{
    public interface IInputValidatorAndParser
    {
        List<InputWeight> ValidateAndParseWeight(string inputString, decimal precision, decimal maxAllowedWeight);
    }
}