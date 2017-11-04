
namespace Invoiced
{
    using System;
    using System.Net;

    public class EntityException : Exception {       
        
         private const long serialVersionUID = 1L;

        public EntityException(string message) : base(message) {
    
         }

        public EntityException(string message,Exception e) : base(message,e) {
    
         }

    }

}