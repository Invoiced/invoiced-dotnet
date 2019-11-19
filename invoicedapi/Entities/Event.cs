using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Event : AbstractEntity<Event> {


		internal Event(Connection conn) : base(conn) {
		}

		protected override string EntityIdString() {
			return this.Id.ToString();
		}

		public override string EntityName() {
			return "events";
		}

		public override bool HasCRUD() {
			return false;
		}
		
		public override bool HasList() {
			return true;
		}

		public override bool HasStringId() {
			return false;
		}

		public override bool IsSubEntity() {
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
