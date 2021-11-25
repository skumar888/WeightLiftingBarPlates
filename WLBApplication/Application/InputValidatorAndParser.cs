using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WLBApplication.Model;
using WLPBlatesManager.Model;

namespace WLBApplication.Application
{
    public class InputValidatorAndParser : IInputValidatorAndParser
    {
        private List<InputWeight> inputWeightList = new List<InputWeight>();
        public List<InputWeight> ValidateAndParseWeight(string inputString, decimal maxAllowedWeight, List<Plate> availablePlates, decimal equipmentWeight)
        {
            ValidateWeights(inputString);

            var precision = GetPricision(availablePlates.Select(x=>x.weight).ToArray());//ToDo:Change precision to min weight

            foreach (InputWeight inputWeight in inputWeightList)
            {
                try
                {
                    if ((inputWeight.weight-equipmentWeight) % precision != 0)
                        throw new Exception("");
                    if(inputWeight.weight > maxAllowedWeight)
                        throw new Exception($"Weight limit exceeded. Allowed limit is {maxAllowedWeight}lb");
                }
                catch (Exception e)
                {
                    inputWeight.isValid = false;
                    inputWeight.error = e.Message;
                    inputWeight.weight = 0;
                }
            }


            return inputWeightList;
        }

        private void ValidateWeights(string inputWeightsString)
        {
            string[] stringWeights = inputWeightsString.Split(',');
            decimal inputWeight;

            for (int i = 0; i < stringWeights.Length; i++)
            {
                inputWeightList.Add(new InputWeight() { weightName = stringWeights[i] });
                try
                {
                    if (!stringWeights[i].ToLower().Contains("lb"))
                        throw new Exception("Weight not in correct format, missing lb");

                    inputWeight = decimal.Parse(stringWeights[i].ToLower().Replace("lb", ""));
                    inputWeightList[i].weight = inputWeight;
                }
                catch (Exception e)
                {
                    //todo:Add Logging
                    inputWeightList[i].isValid = false;
                    inputWeightList[i].error = e.Message;
                    inputWeightList[i].weight = 0;
                }
            }
        }

        public decimal GetPricision(decimal[] inputWeights)
        {
            decimal result = inputWeights[0];
            for (int i = 1; i < inputWeights.Length; i++)
            {
                result = findGCD(inputWeights[i], result);
            }
            return result;
        }

        private decimal findGCD(decimal a, decimal b)
        {
            if (a == 0)
                return b;
            return findGCD(b % a, a);
        }
    }
}
