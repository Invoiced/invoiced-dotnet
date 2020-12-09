using System.Collections.Generic;

namespace Invoiced
{
    public class ListResponse
    {
        public ListResponse(string result, Dictionary<string, string> links, int totalCount)
        {
            Result = result;
            Links = links;
            TotalCount = totalCount;
        }

        public string Result { get; }
        public Dictionary<string, string> Links { get; }
        public int TotalCount { get; }
    }
}