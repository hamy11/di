using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualisation
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> HandleErrors<T>(this IEnumerable<Result<T>> enumerable, Action<string> handler)
        {
            return enumerable.Where(x => x.OnFail(handler).IsSuccess).Select(x => x.Value);
        }
    }
}