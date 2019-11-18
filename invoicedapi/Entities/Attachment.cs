using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Attachment : Item
	{
		
		public Attachment() : base() {

		}

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("file")]
		public File File { get; set; }

        [JsonProperty("created_at")]
		public long CreatedAt { get; set; }


		public override string EntityId() {
			return this.Id.ToString();
		}
		
	}

}
