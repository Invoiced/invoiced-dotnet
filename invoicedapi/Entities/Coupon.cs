using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Coupon : Entity<Coupon> {


		internal Coupon(Connection conn) : base(conn) {
		}

		override public long EntityId() {
			throw new EntityException(this.EntityName() + " ID type is string, not long");
		}

		override public string EntityIdString() {
			return this.Id;
		}

		override public string EntityName() {
			return "coupons";
		}

		override public bool HasCRUD() {
			return true;
		}
		
		override public bool HasList() {
			return true;
		}

		override public bool HasStringId() {
			return true;
		}

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("object")]
		public string Obj { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("currency")]
		public string Currency { get; set; }

		[JsonProperty("value")]
		public long Value { get; set; }

        [JsonProperty("is_percent")]
		public bool IsPercent { get; set; }

        [JsonProperty("exclusive")]
		public bool Exclusive { get; set; }

        [JsonProperty("expiration_date")]
		public long ExpirationDate { get; set; }

        [JsonProperty("max_redemptions")]
		public long MaxRedemptions { get; set; }

		[JsonProperty("created_at")]
		public long CreatedAt { get; set; }

		[JsonProperty("metadata")]
		public Metadata Metadata { get; set; }

	}

}
