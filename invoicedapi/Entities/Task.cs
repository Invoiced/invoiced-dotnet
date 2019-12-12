using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Invoiced
{

	public class Task : AbstractEntity<Task>
	{

		public Task(Connection conn) : base(conn) {
			this.EntityName = "/tasks";
		}

		public Task() : base() {
			this.EntityName = "/tasks";
		}

		protected override string EntityId() {
			return this.Id.ToString();
		}

		[JsonProperty("id")]
		public long? Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("action")]
		public string Action { get; set; }

		[JsonProperty("customer_id")]
		public long? CustomerId { get; set; }

        [JsonProperty("user_id")]
		public long? UserId { get; set; }

        [JsonProperty("due_date")]
		public long? DueDate { get; set; }

        [JsonProperty("complete")]
		public bool? Complete { get; set; }

        [JsonProperty("completed_date")]
		public long? CompletedDate { get; set; }

        [JsonProperty("completed_by_user_id")]
		public long? CompletedByUserId { get; set; }

        [JsonProperty("chase_step_id")]
		public long? ChaseStepId { get; set; }

		[JsonProperty("created_at")]
		public long? CreatedAt { get; set; }

		// Conditional Serialisation

		public bool ShouldSerializeId() {
			return false;
		}

		public bool ShouldSerializeCustomerId() {
			return this.CurrentOperation == "Create";
		}

		public bool ShouldSerializeComplete() {
			return false;
		}

		public bool ShouldSerializeCompletedDate() {
			return false;
		}

		public bool ShouldSerializeCompletedByUserId() {
			return false;
		}

		public bool ShouldSerializeChaseStepId() {
			return false;
		}

		public bool ShouldSerializeCreatedAt() {
			return false;
		}

	}

}