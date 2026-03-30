using System;
using System.Linq;
using System.Reflection;

namespace ACY.MapsterPattern;

public static class SimpleMapper
{

    public static TDestination Map<TSource, TDestination>(TSource source)
        where TDestination : new()
    {
        if (source == null) throw new ArgumentNullException(nameof(source));

        var destination = new TDestination();
        var sourceProps = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var destProps = typeof(TDestination).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var sProp in sourceProps)
        {
            var dProp = destProps.FirstOrDefault(p => p.Name == sProp.Name && p.PropertyType == sProp.PropertyType);
            if (dProp != null && dProp.CanWrite)
            {
                var value = sProp.GetValue(source);
                dProp.SetValue(destination, value);
            }
        }

        return destination;
    }
}
