using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendorCollection.Utilities
{
    public interface ICacheProvider
    {
        ICache GetCache();
    }
}
