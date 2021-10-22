using Newtonsoft.Json;

namespace Invoiced
{
    public class Email : AbstractItem
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("state")] public string State { get; set; }

        [JsonProperty("reject_reason")] public string RejectReason { get; set; }

        [JsonProperty("email")]
        public string
            EMail
        {
            get;
            set;
        } // must have diff name than enclosing type; accomplished through archaic capitalization of email

        [JsonProperty("template")] public string Template { get; set; }

        [JsonProperty("subject")] public string Subject { get; set; }

        [JsonProperty("message")] public string Message { get; set; }

        [JsonProperty("opens")] public int? Opens { get; set; }

        [JsonProperty("opens_detail")] public object OpensDetail { get; set; }

        [JsonProperty("created_at")] public long? CreatedAt { get; set; }

        [JsonProperty("updated_at")] public long? UpdatedAt { get; set; }

        protected override string EntityId()
        {
            return Id;
        }
    }
}