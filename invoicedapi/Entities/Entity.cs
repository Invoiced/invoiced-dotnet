using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Reflection;

namespace Invoiced
{

    public abstract class Entity<T> where T : Entity<T> {

			private Connection connection;
			private bool entityCreated;
			
        	public override string ToString(){
				var s = base.ToString() + "<" + this.EntityID().ToString() +">";
		    	var jsonS =  s + " " + this.ToJsonString();

				return jsonS;
            }

			internal Entity(Connection conn) {
				this.connection = conn;

			}

			public Entity() {

			}

			internal void SetConnection(Connection conn) {
				this.connection = conn;
			}

			public void Create(){

				if (this.entityCreated) {
					return;
				}

				if (!this.HasCRUD()) {
					return;
				}

				string url = this.connection.baseUrl() + "/" + this.EntityName();
				string entityJsonBody = this.ToString();
				string responseText = this.connection.post(url,null,entityJsonBody);
				
				try {
					JsonConvert.PopulateObject(responseText,this);
				} catch(Exception e) {
					throw new EntityException("",e);
				}

				this.entityCreated = true;

			}

			protected void SaveAll() {

				if (!this.HasCRUD()) {
					return;
				}

				string url = this.connection.baseUrl() + "/" + this.EntityName() + "/" + this.EntityID().ToString();
				string entityJsonBody = this.ToString();
				string responseText = this.connection.patch(url,entityJsonBody);
				
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

				string url = this.connection.baseUrl() + "/" + this.EntityName() + "/" + this.EntityID().ToString();
				string responseText = this.connection.patch(url,partialDataObject);
				
				try {
					JsonConvert.PopulateObject(responseText,this);
				} catch(Exception e) {
					throw new EntityException("",e);
				}

			}

			public T Retrieve(long id) {

				string url = this.connection.baseUrl() + "/" + this.EntityName() + "/" + id.ToString();
				string responseText = this.connection.get(url,null);

				Console.WriteLine("Response TExt " + responseText);
				T serializedObject;
				try {
					 serializedObject = JsonConvert.DeserializeObject<T>(responseText);
					 serializedObject.connection = this.connection;
				} catch(Exception e) {
					throw new EntityException("",e);
				}

				return serializedObject;
				
			}

			public void Delete() {

				if (!HasCRUD()) {
					return;
				}

				string url = this.connection.baseUrl() + "/" + this.EntityName() + "/" + this.EntityID().ToString();

				this.connection.delete(url);

			}


			protected EntityList<T> List(string nextURL,Dictionary<string,Object> queryParams) {

				if (!this.HasList()) {
					return null;
				}

				string url = this.connection.baseUrl() + "/" + this.EntityName();
				
				if (!string.IsNullOrEmpty(nextURL)) {
					url = nextURL;
				}

				ListResponse response = this.connection.getList(url,queryParams);

				EntityList<T> entities;
				
				try {
					 entities = JsonConvert.DeserializeObject<EntityList<T>>(response.Result);
					 entities.LinkURLS = response.Links;
					 entities.TotalCount = response.TotalCount;
				} catch(Exception e) {
					throw new EntityException("",e);
				}

				foreach (var entity in entities) {
					entity.SetConnection(connection);
				}

				return entities;

			}

	        protected EntityList<T> ListAll(string nextURL,Dictionary<string,Object> queryParams) {

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
				} while(!(string.IsNullOrEmpty(entities.GetNextURL()) && (entities.GetSelfURL() != entities.GetLastURL())));

				return entities;

			}
			// protected static void setPublicFields(Object from, Object to) {
			// 	FieldInfo[] fromFieldInfos = from.GetType().GetFields();

			// 	foreach (var fromFieldInfo in fromFieldInfos) {
			// 		var fromObjectValue = fromFieldInfo.GetValue(from);
			// 		var toFieldInfo = to.GetType().GetField(fromFieldInfo.Name);

			// 		if (fromFieldInfo.IsPublic && toFieldInfo != null && toFieldInfo.IsPublic) {

			// 			Type t = Nullable.GetUnderlyingType(toFieldInfo.FieldType) ?? toFieldInfo.FieldType;
			// 			Object safeObject = (fromObjectValue == null) ? null : Convert.ChangeType(fromObjectValue,t);
			// 			toFieldInfo.SetValue(to,safeObject);

			// 		}
			// 	}

			// }


			public string ToJsonString() {
				return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented,new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore } );
			}


			public abstract long EntityID();
			public abstract string EntityName();
			public abstract bool HasCRUD();
			public abstract bool HasList();
	


	}



}