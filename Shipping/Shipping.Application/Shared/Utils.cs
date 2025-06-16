using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Shared
{
    public static class Utils
    {
        public static void CopyProperties<TSource, TDestination>(TSource source, TDestination destination)
        {
            var sourceProperties = typeof(TSource).GetProperties();
            var destinationProperties = typeof(TDestination).GetProperties().ToDictionary(p => p.Name);

            foreach (var prop in sourceProperties)
            {
                if (destinationProperties.TryGetValue(prop.Name, out var destProp) &&
                    destProp.PropertyType == prop.PropertyType &&
                    destProp.CanWrite)
                {
                    destProp.SetValue(destination, prop.GetValue(source));
                }
            }
        }
    }
}
