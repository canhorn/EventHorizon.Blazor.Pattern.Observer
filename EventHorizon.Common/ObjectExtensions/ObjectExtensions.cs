using Newtonsoft.Json.Linq;
using System;

public static class ObjectExtensions
{
    public static T Cast<T>(this object objectToCast)
    {
        if (objectToCast != null && objectToCast.GetType() == typeof(JObject))
        {
            return (objectToCast as JObject).ToObject<T>();
        }
        return (T)objectToCast;
    }
    public static void NullCheck(
        [ValidatedNotNull] this object objectToCheck,
        string paramName = ""
    )
    {
        if (objectToCheck == null)
        {
            throw new System.ArgumentNullException(
                paramName
            );
        }
    }
}
[AttributeUsage(AttributeTargets.Parameter)]
internal sealed class ValidatedNotNullAttribute : Attribute
{
}