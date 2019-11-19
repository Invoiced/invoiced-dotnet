using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Reflection;

namespace Invoiced
{

    public abstract class AbstractEntity<T> where T : AbstractEntity<T> {

		protected Connection connection;
		private bool entityCreated;

		// used to determine safe json serialisation. should always be null outside function bodies
		protected string currentOperation;

		public bool ShouldSerializecurrentOperation() {
			return false;
		}
		
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
		
		public Connection GetConnection() {
			return this.connection;
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

		// this method serialises the existing object (with respect for defined create/update safety, i.e. ShouldSerialize functions)
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

		// this method does not serialise an existing object
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

		public string ToJsonString([System.Runtime.CompilerServices.CallerMemberName] string enclosingFunction = "") {
			if (enclosingFunction != "") {
				this.currentOperation = enclosingFunction;
			}

			var output = Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented,new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore } );

			this.currentOperation = null;
			return output;
		}

		public void Void() {

			if (!this.HasVoid()) {
				return;
			}

			string url = this.connection.baseUrl() + "/" + this.EntityName() + "/" + this.EntityIdString() + "/void";

			string responseText = this.connection.Post(url,null,null);
			
			try {
				JsonConvert.PopulateObject(responseText,this);
			} catch(Exception e) {
				throw new EntityException("",e);
			}
		}

		public IList<Attachment> ListAttachments() {

			if (!this.HasAttachments()) {
				return null;
			}

			IList<Attachment> objects = null;

			string url = this.connection.baseUrl() + "/" + this.EntityName() + "/" + this.EntityIdString() + "/attachments";

			string responseText = this.connection.Get(url,null);
			objects = JsonConvert.DeserializeObject<IList<Attachment>>(responseText,new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });

			return objects;
		}

		public IList<Email> SendEmail(EmailRequest emailRequest) {

			if (!this.HasSends()) {
				return null;
			}

			IList<Email> objects = null;

			string url = this.connection.baseUrl() + "/" + this.EntityName() + "/" + this.EntityIdString() + "/emails";

			string jsonRequestBody = emailRequest.ToJsonString();

			string responseText = this.connection.Post(url,null,jsonRequestBody);
			objects = JsonConvert.DeserializeObject<IList<Email>>(responseText,new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });

			return objects;
		}

		public IList<Letter> SendLetter(LetterRequest letterRequest = null) {

			if (!this.HasSends()) {
				return null;
			}

			IList<Letter> objects = null;

			string url = this.connection.baseUrl() + "/" + this.EntityName() + "/" + this.EntityIdString() + "/letters";

			string jsonRequestBody = letterRequest.ToJsonString();

			string responseText = this.connection.Post(url,null,jsonRequestBody);
			objects = JsonConvert.DeserializeObject<IList<Letter>>(responseText,new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });

			return objects;
		}

		public IList<TextMessage> SendText(TextRequest textRequest) {

			if (!this.HasSends()) {
				return null;
			}

			IList<TextMessage> objects = null;

			string url = this.connection.baseUrl() + "/" + this.EntityName() + "/" + this.EntityIdString() + "/text_messages";

			string jsonRequestBody = textRequest.ToJsonString();

			string responseText = this.connection.Post(url,null,jsonRequestBody);
			objects = JsonConvert.DeserializeObject<IList<TextMessage>>(responseText,new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });

			return objects;
		}

		public abstract long EntityId();
		public abstract string EntityIdString();
		public abstract string EntityName();

		public virtual bool HasCRUD() {
			return true;
		}

		public virtual bool HasList() {
			return true;
		}

		public virtual bool HasVoid() {
			return false;
		}
		
		public virtual bool HasStringId() {
			return false;
		}

		public virtual bool IsSubEntity() {
			return false;
		}

		public virtual bool HasAttachments() {
			return false;
		}

		public virtual bool HasSends() {
			return false;
		}

	}

}
