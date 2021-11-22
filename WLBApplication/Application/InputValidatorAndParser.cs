using System;
using System.Collections.Generic;
using System.Text;

namespace WLBApplication.Application
{
    public class InputValidatorAndParser : IInputValidatorAndParser
    {

        public double[] ValidateAndParseWeight(string inputString)
        {
            string[] stringWeights = inputString.Split(',');
            double[] intWeights = new double[stringWeights.Length];

            for (int i = 0; i < stringWeights.Length ; i++)
            {
                try
                {
                    intWeights[i] = double.Parse(stringWeights[i].Replace("lb", ""));
                }
                catch (Exception)
                {

                    intWeights[i] = -1;
                }
                
            }
            return intWeights;

        }
    }
}
