using System;
using Microsoft.AspNetCore.Mvc;
namespace RestServices.BasicAuth
{
    ///<Summary>
    /// Class for Authentication attributes
    ///</Summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class BasicAuthAttribute : TypeFilterAttribute
    {
        ///<Summary>
        /// Constructor of the Authent attribut
        ///</Summary>
        public BasicAuthAttribute(string realm = @"My Realm") : base(typeof(BasicAuthFilter))
        {
            Arguments = new object[] { realm };
        }
    }
}
