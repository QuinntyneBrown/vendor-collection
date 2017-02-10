using VendorCollection.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendorCollection.Utilities
{
    public class CacheProvider : ICacheProvider
    {
        public ICache GetCache()
        {
            return RedisCache.Current;
        }
    }
}
