namespace Invoiced
{
    using System;
    using System.Net;
    using System.Net.Http;

   internal static class Client {       
    
         static Client() {
             HttpClient = new HttpClient();

         }


         internal static HttpClient HttpClient {get; set;}
    }

}