using System;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

public static class CommonUtil
{
    public static Dictionary<string,string> parseLinks(string links) {

        Dictionary<string,string> LinkMap = new Dictionary<string, string>();

        string[] parsedLinks = links.Split(',');

        foreach (var parsedLink in parsedLinks) {
            string[] reParse = parsedLink.Split(';');
            string link = reParse[0].Trim().Replace("<","").Replace(">","");
            string next = reParse[1].Trim();
            int begin = next.IndexOf('"');
            int end = next.LastIndexOf('"');
            next = next.Substring(begin, end - begin);
            LinkMap.Add(next,link);

        }

        return LinkMap;


    }

}

}