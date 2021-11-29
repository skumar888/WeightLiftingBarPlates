using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WLBApplication.Model;
using WLBLoggingService;
using WLPBlatesManager.Model;

namespace WLBApplication.Application
{
    public class InputValidatorAndParser : IInputValidatorAndParser
    {
        private ILoggerManager _loggerManager;
        private IJsonParser _jsonParser;
        public InputValidatorAndParser(ILoggerManager loggerManager, IJsonParser jsonParser)
        {
            _loggerManager =  loggerManager;
            _jsonParser = jsonParser;
        }
        private List<InputWeight> inputWeightList = new List<InputWeight>();
        public List<InputWeight> ValidateAndParseWeight(string inputString, decimal WLBMaximumAllowedWeightIndexes, List<Plate> availablePlates, decimal equipmentWeight, decimal precision)
        {
            ValidateWeights(inputString);

            foreach (InputWeight inputWeight in inputWeightList)
            {
                if (inputWeight.isValid)
                {
                    try
                    {
                        if ((inputWeight.weight - equipmentWeight) % precision != 0 || inputWeight.weight < equipmentWeight)
                            throw new Exception("");
                        if (inputWeight.weight > (WLBMaximumAllowedWeightIndexes / precision))
                            throw new Exception($"Weight limit exceeded. Allowed limit is {WLBMaximumAllowedWeightIndexes / precision}lb");
                    }
                    catch (Exception e)
                    {
                        inputWeight.isValid = false;
                        if(e.Message != string.Empty )
                            inputWeight.error = e.Message;
                        inputWeight.weight = 0;
                    }
                }
            }

            _loggerManager.LogInfo($"Input string parsed: { _jsonParser.SerializeObjects(inputWeightList)}");
            return inputWeightList;
        }

        private void ValidateWeights(string inputWeightsString)
        {
            
            string[] stringWeights = inputWeightsString.Split(',');
            decimal inputWeight;

            for (int i = 0; i < stringWeights.Length; i++)
            {
                inputWeightList.Add(new InputWeight() { RequestedWeight = stringWeights[i] });
                try
                {
                    if (!stringWeights[i].ToLower().Contains("lb"))
                        throw new Exception("Weight not in correct format");

                    inputWeight = decimal.Parse(stringWeights[i].ToLower().Replace("lb", ""));
                    inputWeightList[i].weight = inputWeight;
                }
                catch (Exception e)
                {
                    inputWeightList[i].isValid = false;
                    inputWeightList[i].error = e.Message;
                    inputWeightList[i].weight = 0;
                }
            }
        }

        public decimal GetPrecision(decimal[] inputWeights)
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
