using System;
using Newtonsoft.Json;

namespace Invoiced
{
    public class BankAccount : PaymentSource
    {
        public BankAccount()
        {
            EntityName = "/bank_accounts";
        }

        internal BankAccount(Connection conn) : base(conn)
        {
            EntityName = "/bank_accounts";
        }

        [JsonProperty("bank_name")] public string BankName { get; set; }

        [JsonProperty("last4")] public string Last4 { get; set; }

        [JsonProperty("routing_number")] public string RoutingNumber { get; set; }

        [JsonProperty("verified")] public bool? Verified { get; set; }

        [JsonProperty("currency")] public string Currency { get; set; }

        [JsonProperty("country")] public string Country { get; set; }

        public override void Delete()
        {
            try
            {
                GetConnection().Delete(GetEndpoint(true));
            }
            catch (Exception e)
            {
                throw new EntityException("", e);
            }
        }
    }
}