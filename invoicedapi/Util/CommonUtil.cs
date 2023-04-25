using System.Collections.Generic;

namespace Invoiced
{
    public static class CommonUtil
    {
        public static Dictionary<string, string> parseLinks(string links)
        {
            var LinkMap = new Dictionary<string, string>();

            var parsedLinks = links.Split(',');

            foreach (var parsedLink in parsedLinks)
            {
                var reParse = parsedLink.Split(';');
                var link = reParse[0].Trim().Replace("<", "").Replace(">", "");
                var next = reParse[1].Trim();
                var begin = next.IndexOf('"');
                var end = next.LastIndexOf('"');
                next = next.Substring(begin + 1, end - begin - 1);
                LinkMap.Add(next, link);
            }

            return LinkMap;
        }
    }
}