//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GanafWeb.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class request
    {
        public request()
        {
            this.users = new HashSet<user>();
        }
    
        public string username { get; set; }
        public string requestType { get; set; }
        public System.DateTime urgency { get; set; }
        public string details { get; set; }
        public int requestId { get; set; }
    
        public virtual ICollection<user> users { get; set; }
    }
}
