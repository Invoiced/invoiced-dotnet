using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{
public class Rate : Item
{
    public Rate() : base(){

	}

    [JsonProperty("id")]
	public long Id { get; set; }

    [JsonProperty("is_percent")]
	public bool IsPercentage { get; set; }

    [JsonProperty("name")]
	public string Name { get; set; }

    [JsonProperty("value")]
	public string Value { get; set; }

	[JsonProperty("object")]
	public string Object { get; set; }

    override public long EntityID() {
		return this.Id;
	}

    }

}