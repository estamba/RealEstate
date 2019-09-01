using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Interfaces.Services.Utilities
{
    public interface ICacheManager
    {
        void Store(string key, object value, DateTime expirationDate);
        T Get<T>(string key);
    }
}
