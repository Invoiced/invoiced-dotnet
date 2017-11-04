namespace Invoiced {
 public enum Environment {
     sandbox,
     production,
     local
}
    public static  class ConnectionURL {

        public const string invoicedProduction = "https://api.invoiced.com";
        public const string invoicedSandbox = "https://api.sandbox.invoiced.com";
        public const string invoicedLocal = "https://localhost:8080";



    }
}