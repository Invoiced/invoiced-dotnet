using Newtonsoft.Json;

namespace Invoiced
{
    public class Contact : AbstractEntity<Contact>
    {
        public Contact(Connection conn) : base(conn)
        {
            EntityName = "/contacts";
        }

        public Contact()
        {
            EntityName = "/contacts";
        }

        [JsonProperty("id")] public long? Id { get; set; }

        [JsonProperty("object")] public string Object { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("email")] public string Email { get; set; }

        [JsonProperty("primary")] public bool? Primary { get; set; }

        [JsonProperty("address1")] public string Address1 { get; set; }

        [JsonProperty("address2")] public string Address2 { get; set; }

        [JsonProperty("city")] public string City { get; set; }

        [JsonProperty("state")] public string State { get; set; }

        [JsonProperty("postal_code")] public string PostalCode { get; set; }

        [JsonProperty("country")] public string Country { get; set; }

        [JsonProperty("created_at")] public long? CreatedAt { get; set; }

        [JsonProperty("updated_at")] public long? UpdatedAt { get; set; }

        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("phone")] public string Phone { get; set; }

        [JsonProperty("sms_enabled")] public bool? SmsEnabled { get; set; }

        [JsonProperty("department")] public string Department { get; set; }

        protected override string EntityId()
        {
            return Id.ToString();
        }

        // Conditional Serialisation
        public bool ShouldSerializeId()
        {
            return false;
        }

        public bool ShouldSerializeObject()
        {
            return false;
        }

        public bool ShouldSerializeCreatedAt()
        {
            return false;
        }
    }
}