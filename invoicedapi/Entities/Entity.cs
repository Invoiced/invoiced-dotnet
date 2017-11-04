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
				var s = base.ToString() + "<" + this.GetHashCode().ToString() +">";
		    	var jsonS =  s + " JSON: " + this.toJsonString();

				return jsonS;
            }

			internal Entity(Connection conn) {
				this.connection = conn;

			}

			internal void SetConnection(Connection conn) {
				this.connection = conn;
			}

			public void create(){

				if (this.entityCreated) {
					return;
				}

				if (!this.hasCRUD()) {
					return;
				}

				string url = this.connection.baseUrl() + "/" + this.getEntityName();
				string entityJsonBody = this.ToString();
				string responseText = this.connection.post(url,null,entityJsonBody);
				
				try {
					JsonConvert.PopulateObject(responseText,this);
				} catch(Exception e) {
					throw new EntityException("",e);
				}

				this.entityCreated = true;

			}

			protected void saveAll() {

				if (!this.hasCRUD()) {
					return;
				}

				string url = this.connection.baseUrl() + "/" + this.getEntityName() + "/" + this.getEntityID().ToString();
				string entityJsonBody = this.ToString();
				string responseText = this.connection.patch(url,entityJsonBody);
				
				try {
					JsonConvert.PopulateObject(responseText,this);
				} catch(Exception e) {
					throw new EntityException("",e);
				}


			}

			public void save(string partialDataObject) {

				if (!this.hasCRUD()) {
					return;
				}

				string url = this.connection.baseUrl() + "/" + this.getEntityName() + "/" + this.getEntityID().ToString();
				string responseText = this.connection.patch(url,partialDataObject);
				
				try {
					JsonConvert.PopulateObject(responseText,this);
				} catch(Exception e) {
					throw new EntityException("",e);
				}

			}

			public T retrieve(long id) {

				string url = this.connection.baseUrl() + "/" + this.getEntityName() + "/" + id.ToString();
				string responseText = this.connection.get(url,null);
				T serializedObject;
				try {
					 serializedObject = JsonConvert.DeserializeObject<T>(responseText);
				} catch(Exception e) {
					throw new EntityException("",e);
				}

				return serializedObject;
				
			}

			public void delete() {

				if (!hasCRUD()) {
					return;
				}

				string url = this.connection.baseUrl() + "/" + this.getEntityName() + "/" + this.getEntityID().ToString();

				this.connection.delete(url);

			}


			protected EntityList<T> list(string nextURL,Dictionary<string,Object> queryParams) {

				if (!this.hasList()) {
					return null;
				}

				string url = this.connection.baseUrl() + "/" + this.getEntityName();
				
				if (!string.IsNullOrEmpty(nextURL)) {
					url = nextURL;
				}

				ListResponse response = this.connection.getList(url,queryParams);

				EntityList<T> entities;
				
				try {
					 entities = JsonConvert.DeserializeObject<EntityList<T>>(response.result);
					 entities.linkURLS = response.links;
					 entities.totalCount = response.totalCount;
				} catch(Exception e) {
					throw new EntityException("",e);
				}

				foreach (var entity in entities) {
					entity.SetConnection(connection);
				}

				return entities;

			}

	        protected EntityList<T> listAll(string nextURL,Dictionary<string,Object> queryParams) {

				EntityList<T> entities = null;

				if (!this.hasList()) {
					return null;
				}

				var tmpEntities = this.list(nextURL,queryParams);

				do {
					if (entities == null) {
						entities = tmpEntities;
					} else {
						entities.AddRange(tmpEntities);
						entities.linkURLS = tmpEntities.linkURLS;
						entities.totalCount = tmpEntities.totalCount;
					}
				} while(!(string.IsNullOrEmpty(entities.getNextURL()) && (entities.getSelfURL() != entities.getLastURL())));

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


			public string toJsonString() {
				return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented,new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore } );
			}


			public abstract long getEntityID();
			public abstract string getEntityName();
			public abstract bool hasCRUD();
			public abstract bool hasList();
	


	}



}