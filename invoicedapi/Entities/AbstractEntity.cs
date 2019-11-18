using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Reflection;

namespace Invoiced
{

    public abstract class AbstractEntity<T> where T : AbstractEntity<T> {

		protected Connection connection;
		private bool entityCreated;
		
		public override string ToString() {
			var s = base.ToString() + "<" + this.EntityId().ToString() +">";
			var jsonS =  s + " " + this.ToJsonString();

			return jsonS;
		}

		internal AbstractEntity(Connection conn) {
			this.connection = conn;

		}

		public AbstractEntity() {

		}

		public void ChangeConnection(Connection conn) {
			this.connection = conn;
		}

		public void Create() {

			if (this.entityCreated) {
				return;
			}

			if (!this.HasCRUD()) {
				return;
			}

			string url = this.connection.baseUrl() + "/" + this.EntityName();
			string entityJsonBody = this.ToJsonString();
			string responseText = this.connection.Post(url,null,entityJsonBody);
		
			try {
				JsonConvert.PopulateObject(responseText,this);
			} catch(Exception e) {
				throw new EntityException("",e);
			}

			this.entityCreated = true;

		}

		public void SaveAll() {

			if (!this.HasCRUD()) {
				return;
			}

			string url = this.connection.baseUrl() + "/" + this.EntityName() + "/" + this.EntityIdString();
			string entityJsonBody = this.ToJsonString();
			string responseText = this.connection.Patch(url,entityJsonBody);
			
			try {
				JsonConvert.PopulateObject(responseText,this);
			} catch(Exception e) {
				throw new EntityException("",e);
			}


		}

		public void Save(string partialDataObject) {

			if (!this.HasCRUD()) {
				return;
			}

			string url = this.connection.baseUrl() + "/" + this.EntityName() + "/" + this.EntityIdString();
			string responseText = this.connection.Patch(url,partialDataObject);
			
			try {
				JsonConvert.PopulateObject(responseText,this);
			} catch(Exception e) {
				throw new EntityException("",e);
			}

		}

		public T Retrieve(long id) {

			string url = this.connection.baseUrl() + "/" + this.EntityName() + "/" + id.ToString();
			string responseText = this.connection.Get(url,null);
			T serializedObject;
			try {
					serializedObject = JsonConvert.DeserializeObject<T>(responseText,new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
					serializedObject.connection = this.connection;
			} catch(Exception e) {
				throw new EntityException("",e);
			}

			return serializedObject;
			
		}

		public T Retrieve() {

			string url = this.connection.baseUrl() + "/" + this.EntityName();
			string responseText = this.connection.Get(url,null);
			T serializedObject;
			try {
					serializedObject = JsonConvert.DeserializeObject<T>(responseText,new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
					serializedObject.connection = this.connection;
			} catch(Exception e) {
				throw new EntityException("",e);
			}

			return serializedObject;
			
		}

		public T Retrieve(string id) {

			string url = this.connection.baseUrl() + "/" + this.EntityName() + "/" + id;
			string responseText = this.connection.Get(url,null);
			T serializedObject;
			try {
					serializedObject = JsonConvert.DeserializeObject<T>(responseText,new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
					serializedObject.connection = this.connection;
			} catch(Exception e) {
				throw new EntityException("",e);
			}

			return serializedObject;
			
		}

		public virtual void Delete() {

			if (!HasCRUD()) {
				return;
			}

			string url = this.connection.baseUrl() + "/" + this.EntityName() + "/" + this.EntityIdString();
			
			this.connection.Delete(url);

		}

		public EntityList<T> List(string nextURL,Dictionary<string,Object> queryParams) {

			if (!this.HasList()) {
				return null;
			}

			string url = this.connection.baseUrl() + "/" + this.EntityName();
			
			if (!string.IsNullOrEmpty(nextURL)) {
				url = nextURL;
			}

			ListResponse response = this.connection.GetList(url,queryParams);

			EntityList<T> entities;
			
			try {
					entities = JsonConvert.DeserializeObject<EntityList<T>>(response.Result,new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
					entities.LinkURLS = response.Links;
					entities.TotalCount = response.TotalCount;
			} catch(Exception e) {
				throw new EntityException("",e);
			}

			foreach (var entity in entities) {
				entity.ChangeConnection(connection);
			}

			return entities;

		}

		public EntityList<T> ListAll(Dictionary<string,Object> queryParams) {
				var entities = ListAll("",queryParams);
				return entities;
		}

		public EntityList<T> ListAll(string nextURL,Dictionary<string,Object> queryParams) {

			EntityList<T> entities = null;

			if (!this.HasList()) {
				return null;
			}

			var tmpEntities = this.List(nextURL,queryParams);

			do {
				if (entities == null) {
					entities = tmpEntities;
				} else {
					entities.AddRange(tmpEntities);
					entities.LinkURLS = tmpEntities.LinkURLS;
					entities.TotalCount = tmpEntities.TotalCount;
				}

			} while((!string.IsNullOrEmpty(entities.GetNextURL()) && (entities.GetSelfURL() != entities.GetLastURL())));

			return entities;

		}
	
		public string ToJsonString() {
			return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented,new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore } );
		}

		public abstract long EntityId();
		public abstract string EntityIdString();
		public abstract string EntityName();
		public abstract bool HasCRUD();
		public abstract bool HasList();
		public abstract bool HasStringId();
		public abstract bool IsSubEntity();

	}

}
