using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Event : AbstractEntity<Event> {

		internal Event() : base() {
			
		}

		internal Event(Connection conn) : base(conn) {
		}

		protected override string EntityId() {
			return this.Id.ToString();
		}

		public override string EntityName() {
			return "events";
		}

		protected override bool HasCrud() {
			return false;
		}

		protected override bool HasList() {
			return true;
		}

		public virtual bool HasStringId() {
			return false;
		}

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("timestamp")]
		public long Timestamp { get; set; }

		[JsonProperty("data")]
		public object Data { get; set; }

	}

}
