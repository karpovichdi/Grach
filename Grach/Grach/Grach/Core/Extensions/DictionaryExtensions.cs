using System;
using System.Collections.Generic;
using System.Linq;

namespace Grach.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static void SetDefaultValues<TKey,TValue>(this IDictionary<TKey,TValue> dictionary)
        {
            foreach (var key in dictionary.Keys.ToList())
            {
                dictionary[key] = default(TValue);
            }
        }

        public static string ToKeysAndValuesString(this IDictionary<string, string> attributes)
        {
            if (attributes == null || attributes.Count == 0)
            {
                return "";
            }

            try
            {
                var members = attributes.Aggregate("", (start, pair) => start + $"[{pair.Key}: {pair.Value}], ");

                return $"{{ {members.TrimEnd(new[] { ',', ' ' })} }}";
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string ToKeysAndValuesString(this IDictionary<string, object> attributes)
        {
            if (attributes == null || !attributes.Any())
            {
                return "";
            }

            return ToKeysAndValuesString(attributes.ToDictionary(x => x.Key, y => y.Value?.ToString()));
        }
    }
}