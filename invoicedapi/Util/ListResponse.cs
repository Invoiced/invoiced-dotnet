namespace Invoiced
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Collections.Generic;

    public class ListResponse {   

       public string Result {get;}
       public Dictionary<string,string> Links{get;}
       public int TotalCount {get;}

       public ListResponse(string result, Dictionary<string,string> links, int totalCount) {
           this.Result = result;
           this.Links = links;
           this.TotalCount = totalCount;
       }

    }

}