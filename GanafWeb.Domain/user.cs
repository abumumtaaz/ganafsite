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
    
    public partial class user
    {
        public user()
        {
            this.Comments = new HashSet<Comment>();
            this.authenticatedUsers = new HashSet<authenticatedUser>();
        }
    
        public int userId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string userType { get; set; }
    
        public virtual request request { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<authenticatedUser> authenticatedUsers { get; set; }
    }
}