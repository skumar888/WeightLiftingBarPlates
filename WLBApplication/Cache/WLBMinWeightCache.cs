using System;
using System.Collections.Generic;
using System.Text;
using WLBApplication.Model;

namespace WLBApplication.Cache
{
    public class WLBMinWeightCache : IWLBMinWeightCache
    {
        private WLBMinResult[] WLBMinResultCache;

        public WLBMinResult[] GetWLBMinWeightResultCache()
        {
            return WLBMinResultCache;
        }

        public void AddWLBMinWeightResultCache(WLBMinResult[] WLBMinResultsArray)
        {
            this.WLBMinResultCache = WLBMinResultsArray;
        }
        public int PeakWLBMinWeightResultCache()
        {
            if (this.WLBMinResultCache == null)
                return 0;
            else
                return this.WLBMinResultCache.Length;
        }
    }
}
