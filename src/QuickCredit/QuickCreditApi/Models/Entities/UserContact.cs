using System;
using System.Collections.Generic;

namespace QuickCreditApi.Models.Entities
{
    public partial class UserContact
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
       

        public virtual UserAccount User { get; set; }
    }
}
