//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GanafWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class users
    {
        public int userId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int phone { get; set; }
        public string email { get; set; }
    
        public virtual request request { get; set; }
    }
}
