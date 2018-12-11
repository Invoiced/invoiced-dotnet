namespace Invoiced
{
    using System;
    using System.Net;
    using System.Net.Http;

   public static class Client {       
    
         static Client() {
             httpClient = new HttpClient();

         }


         internal static HttpClient httpClient {get; set;}

         public static void setHttpClientTest(HttpClient httpClientTest) {
             httpClient= httpClientTest;
         }
    }

}