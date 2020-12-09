using Newtonsoft.Json;

namespace Invoiced
{
    public abstract class AbstractItem
    {
        public override string ToString()
        {
            var s = base.ToString() + "<" + EntityId() + ">";
            return s + " " + ToJsonString();
        }

        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented,
                new JsonSerializerSettings
                    {NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore});
        }

        protected abstract string EntityId();
    }
}