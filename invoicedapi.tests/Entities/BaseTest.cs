using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Invoiced;
using RichardSzalay.MockHttp;

namespace InvoicedTest
{
    abstract public class BaseTest
    {
        protected Connection CreateTestConnection(HttpClient client)
        {
            Connection testConnection = new Connection("voodoo", Invoiced.Environment.test);

            testConnection.TestClient(client);

            return testConnection;
        }
    }
}
