using Newtonsoft.Json;

namespace Invoiced
{
    public class Note : AbstractEntity<Note>
    {
        public Note(Connection conn) : base(conn)
        {
            EntityName = "/notes";
        }

        public Note()
        {
            EntityName = "/notes";
        }

        [JsonProperty("id")] public long? Id { get; set; }

        [JsonProperty("object")] public string Object { get; set; }

        [JsonProperty("notes")] public string Notes { get; set; }

        [JsonProperty("customer")] public long? Customer { get; set; }

        [JsonProperty("user")] public object User { get; set; }

        [JsonProperty("metadata")] public Metadata Metadata { get; set; }

        [JsonProperty("created_at")] public long? CreatedAt { get; set; }

        [JsonProperty("updated_at")] public long? UpdatedAt { get; set; }

        protected override string EntityId()
        {
            return Id.ToString();
        }

        public override void Create()
        {
            var endpointBase = GetEndpointBase();
            SetEndpointBase("");
            try
            {
                base.Create();
            }
            finally
            {
                SetEndpointBase(endpointBase);
            }
        }

        public override void SaveAll()
        {
            var endpointBase = GetEndpointBase();
            SetEndpointBase("");
            try
            {
                base.SaveAll();
            }
            finally
            {
                SetEndpointBase(endpointBase);
            }
        }

        public override void Delete()
        {
            var endpointBase = GetEndpointBase();
            SetEndpointBase("");
            try
            {
                base.Delete();
            }
            finally
            {
                SetEndpointBase(endpointBase);
            }
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

        public bool ShouldSerializeCustomer()
        {
            return false;
        }

        public bool ShouldSerializeCustomerId()
        {
            return CurrentOperation == "Create";
        }

        public bool ShouldSerializeInvoiceId()
        {
            return CurrentOperation == "Create";
        }

        public bool ShouldSerializeCreatedAt()
        {
            return false;
        }
    }
}