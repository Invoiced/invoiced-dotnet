using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Reflection;

namespace Invoiced
{
    public class EntityList<T> : List<T>  where T : Entity<T>{

        public Dictionary<string,string> linkURLS { get; set; }
        public int totalCount {get; set;}


       public string getNextURL() {
           return getURL("next");

        }

       public string getSelfURL() {
           return getURL("self");

        }

        public string getLastURL() {
            return getURL("last");
         
       }

       private string getURL(string key) {

        string value = "";

        if (linkURLS == null) {
            return value;
        }

        linkURLS.TryGetValue("self",out value);
        return value;


       }

    }

} 