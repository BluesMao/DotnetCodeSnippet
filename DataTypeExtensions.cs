using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sov.IronPay.Core
{
    public static class DataTypeExtensions
    {
        public static R ChangeType<T, R>(this T Object, R DefaultValue = default(R))
        {
            if (Object == null || Convert.IsDBNull(Object))
                return DefaultValue;
            if ((Object as string) != null)
            {
                string ObjectValue = Object as string;
                if (typeof(R).IsEnum)
                    return (R)System.Enum.Parse(typeof(R), ObjectValue, true);
                if (string.IsNullOrEmpty(ObjectValue))
                    return DefaultValue;
            }
            if ((Object as IConvertible) != null)
            {
                Type destination =
                    typeof(R).IsGenericType && typeof(R).GetGenericTypeDefinition() == typeof(Nullable<>) ?
                        Nullable.GetUnderlyingType(typeof(R)) : typeof(R);
                return (R)Convert.ChangeType(Object, destination);
            }
            if (typeof(R).IsAssignableFrom(Object.GetType()))
                return (R)(object)Object;
            return DefaultValue;
        }
    }
}
