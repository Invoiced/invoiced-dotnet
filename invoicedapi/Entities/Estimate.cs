using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Invoiced
{
    public class Estimate : AbstractDocument<Estimate>
    {
        public Estimate()
        {
            EntityName = "/estimates";
        }

        internal Estimate(Connection conn) : base(conn)
        {
            EntityName = "/estimates";
        }

        [JsonProperty("invoice")] public long? Invoice { get; set; }

        [JsonProperty("approved")] public bool? Approved { get; set; }

        [JsonProperty("expiration_date")] public long? ExpirationDate { get; set; }

        [JsonProperty("deposit")] public double? Deposit { get; set; }

        [JsonProperty("deposit_paid")] public bool? DepositPaid { get; set; }

        [JsonProperty("payment_terms")] public string PaymentTerms { get; set; }

        [JsonProperty("ship_to")] public ShippingDetail ShipTo { get; set; }

        [JsonProperty("disabled_payment_methods")]
        public IList<string> DisabledPaymentMethods { get; set; }

        public Invoice ConvertToInvoice()
        {
            return AsyncUtil.RunSync(() => ConvertToInvoiceAsync());
        }
        public async Task<Invoice> ConvertToInvoiceAsync()
        {
            var url = GetEndpoint(true) + "/invoice";

            var responseText = await GetConnection().PostAsync(url, null, "");
            Invoice serializedObject;

            try
            {
                serializedObject = JsonConvert.DeserializeObject<Invoice>(responseText,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore
                    });
                serializedObject.ChangeConnection(GetConnection());
            }
            catch (Exception e)
            {
                throw new EntityException("", e);
            }

            return serializedObject;
        }

        // Conditional Serialisation
        public bool ShouldSerializeInvoice()
        {
            return false;
        }

        public bool ShouldSerializeApproved()
        {
            return false;
        }
    }
}