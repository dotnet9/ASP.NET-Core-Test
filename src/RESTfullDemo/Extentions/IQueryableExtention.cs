using RESTfullDemo.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace RESTfullDemo.Extentions
{
    public static class IQueryableExtention
    {
        const string OrderSequence_Asc = "asc";
        const string OrderSequence_Desc = "desc";

        public static IQueryable<T> Sort<T>(this IQueryable<T> source,
            string orderBy,
            Dictionary<string, PropertyMapping> mapping) where T:class
        {
            var allQueryParts = orderBy.Split(",");
            List<string> sortParts = new List<string>();
            foreach(var item in allQueryParts)
            {
                string property = string.Empty;
                bool isDescending = false;
                if(item.ToLower().EndsWith(OrderSequence_Desc))
                {
                    property = item.Substring(0, item.Length - OrderSequence_Desc.Length).Trim();
                }
                else
                {
                    property = item.Trim();
                }

                if(mapping.ContainsKey(property))
                {
                    if(mapping[property].IsRevert)
                    {
                        isDescending = !isDescending;
                    }

                    if(isDescending)
                    {
                        sortParts.Add($"{mapping[property].TargetProperty} {OrderSequence_Desc}");
                    }
                    else
                    {
                        sortParts.Add($"{mapping[property].TargetProperty} {OrderSequence_Asc}");
                    }
                }
            }

            string finalExpression = string.Join(',', sortParts);
            source = source.OrderBy(finalExpression);
            return source;
        }
    }
}
