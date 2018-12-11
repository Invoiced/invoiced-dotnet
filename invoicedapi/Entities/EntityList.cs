using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Reflection;

namespace Invoiced
{
    public class EntityList<T> : List<T>  where T : Entity<T>{

        public Dictionary<string,string> LinkURLS { get; set; }
        public int TotalCount {get; set;}


       public string GetNextURL() {
           return GetURL("next");

        }

       public string GetSelfURL() {
           return GetURL("self");

        }

        public string GetLastURL() {
            return GetURL("last");
         
       }

       private string GetURL(string key) {

        string value = "";

        if (LinkURLS == null) {
            return value;
        }

        LinkURLS.TryGetValue("self",out value);
        return value;


       }

    }

} 