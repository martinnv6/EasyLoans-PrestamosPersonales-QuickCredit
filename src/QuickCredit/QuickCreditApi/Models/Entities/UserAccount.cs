using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace QuickCreditApi.Models.Entities
{
    public partial class UserAccount
    {
        public UserAccount()
        {
            UserContact = new HashSet<UserContact>();
        }

        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserType { get; set; }

        public virtual ICollection<UserContact> UserContact { get; set; }
        
        }
}
