//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class order
    {
        public int orderID { get; set; }
        public int packageID { get; set; }
        public int userID { get; set; }
    
        public virtual package package { get; set; }
        public virtual user user { get; set; }
    }
}
