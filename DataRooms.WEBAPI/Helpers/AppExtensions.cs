using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataRooms.WEBAPI
{
    public static class AppExtensions
    {
        public static object GetPropertyValue(this object obj, string propName)
        {
            return obj.GetType().GetProperty(propName).GetValue(obj, null);
        }
    }
}
