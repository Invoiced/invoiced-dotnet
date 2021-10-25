using Newtonsoft.Json;

namespace Invoiced
{
    public class File : AbstractEntity<File>
    {
        internal File(Connection conn) : base(conn)
        {
            EntityName = "/files";
        }

        public File()
        {
            EntityName = "/files";
        }

        [JsonProperty("id")] public long? Id { get; set; }

        [JsonProperty("object")] public string Object { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("size")] public long? Size { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("url")] public string Url { get; set; }

        [JsonProperty("created_at")] public long? CreatedAt { get; set; }

        [JsonProperty("updated_at")] public long? UpdatedAt { get; set; }

        protected override string EntityId()
        {
            return Id.ToString();
        }

        protected override bool HasList()
        {
            return false;
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