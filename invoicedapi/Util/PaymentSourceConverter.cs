using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Invoiced
{
    public class PaymentSourceConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return typeof(PaymentSource).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader,
            Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jo = JObject.Load(reader);

            var objType = (string) jo["object"];

            PaymentSource item;

            if (objType == "bank_account")
                item = new BankAccount();
            else if (objType == "card")
                item = new Card();
            else
                item = new PaymentSource();

            serializer.Populate(jo.CreateReader(), item);

            return item;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}