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
            AsyncUtil.RunSync(() => CreateAsync());
        }
        
        public override System.Threading.Tasks.Task CreateAsync()
        {
            var endpointBase = GetEndpointBase();
            SetEndpointBase("");
            try
            {
                return base.CreateAsync();
            }
            finally
            {
                SetEndpointBase(endpointBase);
            }
        }

        public override void SaveAll()
        {
            AsyncUtil.RunSync(() => SaveAllAsync());
        }
        public override System.Threading.Tasks.Task SaveAllAsync()
        {
            var endpointBase = GetEndpointBase();
            SetEndpointBase("");
            try
            {
               return base.SaveAllAsync();
            }
            finally
            {
                SetEndpointBase(endpointBase);
            }
        }

        public override void Delete()
        {
            AsyncUtil.RunSync(() => DeleteAsync());
        }
        public override System.Threading.Tasks.Task DeleteAsync()
        {
            var endpointBase = GetEndpointBase();
            SetEndpointBase("");
            try
            {
                return base.DeleteAsync();
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
            return CurrentOperation == nameof(Create) || CurrentOperation == nameof(CreateAsync);
        }

        public bool ShouldSerializeInvoiceId()
        {
            return CurrentOperation == nameof(Create) || CurrentOperation == nameof(CreateAsync);
        }

        public bool ShouldSerializeCreatedAt()
        {
            return false;
        }

        public bool ShouldSerializeUpdatedAt()
        {
            return false;
        }
    }
}