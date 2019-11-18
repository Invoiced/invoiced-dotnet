using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class File : AbstractEntity<File> {


		internal File(Connection conn) : base(conn) {
		}

		public override long EntityId() {
			return this.Id;
		}

		public override string EntityIdString() {
			return this.Id.ToString();
		}

		public override string EntityName() {
			return "files";
		}
		
		public override bool HasList() {
			return false;
		}

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("object")]
		public string Obj { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("size")]
		public long Size { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }

        [JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("created_at")]
		public long CreatedAt { get; set; }

	}

}
