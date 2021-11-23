using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WLBApplication.Model;

namespace WLBApplication.Application
{
    public class InputValidatorAndParser : IInputValidatorAndParser
    {
        public InputValidatorAndParser()
        {

        }
        List<InputWeight> inputWeightList = new List<InputWeight>();
        public List<InputWeight> ValidateAndParseWeight(string inputString, decimal precision, decimal maxAllowedWeight)
        {
            string[] stringWeights = inputString.Split(',');
            decimal inputWeight;

            for (int i = 0; i < stringWeights.Length ; i++)
            {
                inputWeightList.Add(new InputWeight() { weightName = stringWeights[i] });
                try
                {
                    if (!stringWeights[i].ToLower().Contains("lb"))
                        throw new Exception("Weight not in correct format, missing lb");

                    inputWeight = decimal.Parse(stringWeights[i].Replace("lb", ""));

                    if(inputWeight%precision !=0)
                        throw new Exception($"Weight is not of required precision: {precision}");
                    if(inputWeight> maxAllowedWeight)
                        throw new Exception($"Weight limit exceeded. Allowed limit is {maxAllowedWeight}lb");
                    inputWeightList[i].weight = decimal.Parse(stringWeights[i].Replace("lb", ""));

                }
                catch (Exception e)
                {
                    inputWeightList[i].isValid = false;
                    inputWeightList[i].error = e.Message;
                    inputWeightList[i].weight = 0;
                }
                
            }
            return inputWeightList;

        }
    }
}
