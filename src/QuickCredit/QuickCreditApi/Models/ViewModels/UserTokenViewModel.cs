using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuickCreditApi.Models.Entities;

namespace QuickCreditApi.Models.ViewModels
{
    public class UserTokenViewModel: UserAccount
    {
        public string Token { get; set; }
    }
}
