using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SCGP.COA.BUSINESSLOGIC.Models;
using SCGP.COA.BUSINESSLOGIC.Services.Interface;
using SCGP.COA.COMMON;
using SCGP.COA.COMMON.Attributes;
using SCGP.COA.COMMON.Authentications;
using SCGP.COA.COMMON.Constants;
using SCGP.COA.COMMON.Exceptions;
using SCGP.COA.COMMON.Models;
using SCGP.COA.COMMON.Utilities;
using SCGP.COA.DATAACCESS.Entities.Coa.Master.Autthorization;
using SCGP.COA.DATAACCESS.Repositories.Coa.Authorization.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SCGP.COA.BUSINESSLOGIC.Services
{
    [TransientPriorityRegistration]
    public class AuthrorizationService : IAuthrorizationService
    {
        private IUserRepository _userRepository;
        public AppSettings _appSettings;

        public AuthrorizationService(IUserRepository userRepository
            , AppSettings appSettings)
        {
            _userRepository = userRepository;
            _appSettings = appSettings;
        }

        public MASTER_USER Login(TokenQuery request)
        {
            var username = request.UserName;
            var password = request.Password;
            var domain = request.Domain;

            var user = _userRepository.FindActiveByUserNameAndDomainAndClient(username, domain);

            if (user == null)
                throw new BusinessException($"Username does not exist");

          //return user;

            if (user.AccessFailedCount >= 3 && DateTime.Now.Subtract(user.UpdatedDate.Value) < TimeSpan.FromMinutes(5))
                throw new BusinessException($"User has been locked, Wrong password attempt 3 times, Please wait for 5 minutes to retry");

            //check password
            if (domain?.ToLower() == AppConstant.Domain.EXTERNAL)
            {
                var encryptPasswpord = CryptoUtilities.sha256_hash(password);
                bool isValid = user.PasswordEncrypt == encryptPasswpord;
                if (!isValid)
                {
                    user.AddAccessFailedCount();
                    throw new BusinessException($"Username or Password incorrect");
                }
            }
            else if (domain?.ToLower() == AppConstant.Domain.CEMENTHAI)
            {
                bool isValid = AuthActiveDirectory(AppConstant.Server.CEMENTHAI, request.UserName, password);
                if (!isValid)
                {
                    user.AddAccessFailedCount();
                    throw new BusinessException($"Username or Password incorrect");
                }
            }
            else
            {
                throw new BusinessException("Domain unsupported");
            }

            // reset fail count
            user.ResetAccessFailedCount();

            // Check must change password
            user.ValidateMustChangePassword();

            _userRepository.Commit();

            return user;
        }

        public bool AuthActiveDirectory(string server, string userName, string password)
        {
            userName = $"{userName}@{server}";
            Novell.Directory.Ldap.LdapConnection connection = null;
            var result = true;
            try
            {
                connection = new Novell.Directory.Ldap.LdapConnection();
                connection.Connect(server, 389);
                connection.Bind(userName, password);
                if (connection.Bound)
                    result = true;

                connection.Disconnect();
            }
            catch (Novell.Directory.Ldap.LdapException)
            {
                result = false;
            }
            catch (Exception e)
            {
                throw new BusinessException($"{e.Message}");
            }
            //connection.Disconnect();
            return result;
        }

        public string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string GenerateJWT(UserCredential user)
        {
            var claims = new List<Claim>
            {
                     new Claim(ClaimStore.name.ToString(), user.UserName),
                     //new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", user.UserName),
                     //new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", user.UserId.ToString()),
                     new Claim(ClaimStore.email.ToString(), user.Email ?? ""),
                     new Claim(ClaimStore.userId.ToString(), user.UserId),
                     new Claim(ClaimStore.domain.ToString(), user.Domain ?? ""),
                     new Claim(ClaimStore.firstName.ToString(), user.FirstName ?? ""),
                     new Claim(ClaimStore.lastName.ToString(), user.LastName ?? ""),
                     new Claim(ClaimStore.clientId.ToString(), user.ClientId ?? ""),
                     new Claim(ClaimStore.groupId.ToString(), user.GroupId?.ToString() ?? ""),
                     new Claim(ClaimStore.groupName.ToString(), user.GroupName ?? ""),
                     new Claim(ClaimStore.phoneNumber.ToString(), user.PhoneNumber ?? ""),
                     new Claim(ClaimStore.organization.ToString(), user.Organization ?? ""),
                     new Claim(ClaimStore.isAdmin.ToString(), user.IsAdmin.ToString())
            };
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimStore.role.ToString(), role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _appSettings.JwtIssuer,
                audience: _appSettings.JwtAudience,
                claims: claims,
                expires: user.Expires,
                signingCredentials: creds
            );

            string t = new JwtSecurityTokenHandler().WriteToken(token);


            return t;
        }

        public string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = AppConstant.PasswordPolicy.MINIMUM_LENGTH,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "0123456789",                   // digits
            "!@$?_-"                        // non-alphanumeric
        };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }

    }
}
