using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Task : Entity<Task>
	{

		public Task(Connection conn) : base(conn) {
		}

		public Task() : base(){

		}

		override public long EntityId() {
			return this.Id;
		}

		override public string EntityIdString() {
			return this.Id.ToString();
		}

		override public string EntityName() {
			return "tasks";
		}

		override public bool HasCRUD() {
			return true;
		}

		override public bool HasList() {
			return true;
		}

		override public bool HasStringId() {
			return false;
		}

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("action")]
		public string Action { get; set; }

		[JsonProperty("customer_id")]
		public long CustomerId { get; set; }

        [JsonProperty("user_id")]
		public long UserId { get; set; }

        [JsonProperty("due_date")]
		public long DueDate { get; set; }

        [JsonProperty("complete")]
		public bool Complete { get; set; }

        [JsonProperty("completed_date")]
		public long CompletedDate { get; set; }

        [JsonProperty("completed_by_user_id")]
		public long CompletedByUserId { get; set; }

        [JsonProperty("chase_step_id")]
		public long ChaseStepId { get; set; }

		[JsonProperty("created_at")]
		public long CreatedAt { get; set; }

	}

}