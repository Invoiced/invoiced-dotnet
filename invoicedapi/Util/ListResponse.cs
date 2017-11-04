namespace Invoiced
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Collections.Generic;

    public class ListResponse {   

       public string result {get;}
       public Dictionary<string,string> links{get;}
       public int totalCount {get;}

       public ListResponse(string result, Dictionary<string,string> links, int totalCount) {
           this.result = result;
           this.links = links;
           this.totalCount = totalCount;
       }

    }

}