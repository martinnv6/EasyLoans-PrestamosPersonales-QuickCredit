using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuickCreditApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using QuickCreditApi.Helpers;
using QuickCreditApi.Models.ViewModels;

namespace QuickCreditApi.Services
{
    public interface ILoginService
    {
        UserTokenViewModel Authenticate(string username, string password);
    }
    public class LoginService : ILoginService
    {
        //private readonly List<UserAccount> _users = new List<UserAccount>
        //{
        //    new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
        //};

        private readonly AppSettings _appSettings;
        private readonly qcdbContext _context;

        public LoginService(IOptions<AppSettings> appSettings, qcdbContext context)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public UserTokenViewModel Authenticate(string username, string password)
        {
            var user =  _context.UserAccount.FirstOrDefault(x => x.Account == username && x.Password == password);
            

            // return null if user not found
            if (user == null)
                return null;

            //Serializing and mapping
            var serializedUser = JsonConvert.SerializeObject(user);
            var userToken = JsonConvert.DeserializeObject<UserTokenViewModel>(serializedUser);

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            userToken.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return userToken;
        }

        
    }
}
