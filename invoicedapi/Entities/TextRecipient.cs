using Newtonsoft.Json;

namespace Invoiced
{
    public class TextRecipient : AbstractItem
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("phone")] public string Phone { get; set; }

        protected override string EntityId()
        {
            return "TextRecipient";
            // this is only used for json heading in ToString()
        }
    }
}