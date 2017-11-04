using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

public class Error 
{
	[JsonProperty("type")]
	public string type { get;}

	[JsonProperty("message")]
	public string message { get; }

	[JsonProperty("param")]
	public string param { get; }

}

}