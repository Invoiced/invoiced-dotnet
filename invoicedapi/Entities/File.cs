using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class File : AbstractEntity<File> {


		internal File(Connection conn) : base(conn) {
		}

		public File() : base(){

		}

		protected override string EntityId() {
			return this.Id.ToString();
		}

		public override string EntityName() {
			return "files";
		}

		protected override bool HasList() {
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

		// Conditional Serialisation

		public bool ShouldSerializeId() {
			return false;
		}

		public bool ShouldSerializeObj() {
			return false;
		}

		public bool ShouldSerializeCreatedAt() {
			return false;
		}

	}

}
