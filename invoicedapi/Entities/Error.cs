using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Error {
		
		[JsonProperty("type")]
		public string Type { get;}

		[JsonProperty("message")]
		public string Message { get; }

		[JsonProperty("param")]
		public string Param { get; }

	}

}