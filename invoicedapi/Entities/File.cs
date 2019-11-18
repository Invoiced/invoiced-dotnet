using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class File : Entity<File> {


		internal File(Connection conn) : base(conn) {
		}

		override public long EntityID() {
			return this.Id;
		}

		override public string EntityIDString() {
			return this.Id.ToString();
		}

		override public string EntityName() {
			return "files";
		}

		override public bool HasCRUD() {
			return true;
		}
		
		override public bool HasList() {
			return false;
		}

		override public bool HasStringID() {
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
