using System;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Linq;

namespace Invoiced
{

public static class HttpUtil
{

    public static string BasicAuth(string username, string password) {
        string encoded = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(username + ":" + password));
        return encoded;
    }

    public static string[] GetHeaders(HttpResponseMessage message, string headerKey) {
       var headers = message.Headers;
    
        if (headers.TryGetValues(headerKey, out var values))
        {

         var v = values.ToArray();
          
         return v;
        }

        return null;
        
    }

    public static string GetHeaderFirstValue(HttpResponseMessage message, string headerKey){
        var headerValue = GetHeaders(message,headerKey);
        if (headerValue != null && headerValue.Length > 0) {
            return headerValue[0];
        }

        return null;

    }

}

}