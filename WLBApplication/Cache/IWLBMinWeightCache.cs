using WLBApplication.Model;

namespace WLBApplication.Cache
{
    public interface IWLBMinWeightCache
    {
        void AddWLBMinWeightResultCache(WLBMinResult[] WLBMinResultsArray);
        WLBMinResult[] GetWLBMinWeightResultCache();
        int PeakWLBMinWeightResultCache();
    }
}