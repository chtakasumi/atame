using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace api.libs
{
    public static class ExtendsMetods
    {

        public static T ToEntity<T>(this IEnumerable<T> list)
        {
            if (list.Count() > 0)
                return list.First();
            else
                return default(T);
        }
    }
}
