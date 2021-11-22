namespace WLBApplication.Application
{
    public interface IInputValidatorAndParser
    {
        double[] ValidateAndParseWeight(string inputString);
    }
}