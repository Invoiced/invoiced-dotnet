using Newtonsoft.Json;

namespace Invoiced
{
    public class Event : AbstractEntity<Event>
    {
        internal Event()
        {
            EntityName = "/events";
        }

        internal Event(Connection conn) : base(conn)
        {
            EntityName = "/events";
        }

        [JsonProperty("id")] public long? Id { get; set; }

        [JsonProperty("object")] public string Object { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("timestamp")] public long? Timestamp { get; set; }

        [JsonProperty("data")] public object Data { get; set; }

        protected override string EntityId()
        {
            return Id.ToString();
        }

        protected override bool HasCrud()
        {
            return false;
        }

        protected override bool HasList()
        {
            return true;
        }

        public virtual bool HasStringId()
        {
            return false;
        }
    }
}