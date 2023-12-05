
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Domain;
using HrManegment.Application.Model.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HR.LeaveManagement.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IMemoryCache _memoryCache;


        public AuthService(UserManager<ApplicationUser> userManager,
            IOptions<JwtSettings> jwtSettings,
            SignInManager<ApplicationUser> signInManager,  IMemoryCache memoryCache)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _signInManager = signInManager;
            _memoryCache = memoryCache;
   
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {

            
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (_memoryCache.TryGetValue("LogOutUsers", out LogoutCachingData CurrentData))
            {

                CurrentData.UsersId.Remove(user.Id);

            }
                if (user == null)
            {
                throw new NotFoundException($"User with {request.Email} not found.", request.Email);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (result.Succeeded == false)
            {
                throw new BadRequestException($"Credentials for '{request.Email} aren't valid'.");
            }

            var jwtSecurityToken = await GenerateToken(user.Id);

            var response = new AuthResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken.Item1),
                Email = user.Email,
                UserName = user.UserName,
                RefreshToken= jwtSecurityToken.Item2

            };

          

           

            return response;
        }

    

       
public async Task<(JwtSecurityToken, string)> RefreshToken(string userRefreshToken)

        {
            var userId = getUserId(userRefreshToken);
            var storedRefreshToken = "";

            bool isValid = TokenChecker( userRefreshToken, storedRefreshToken);
          

            if (isValid)

            {

                var jwtSecurityToken = await GeneratejwtToken(userId);
                string refreshToken = RefreshTokenGen(userId);

                StoreRefreshtoken(refreshToken);

                return (jwtSecurityToken, refreshToken);

            }

            else

            {

                throw new ApplicationException("Invalid tokens provided");

            }

        }


       


        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Employee");
                return new RegistrationResponse() { UserId = user.Id };
            }
            else
            {
                StringBuilder str = new StringBuilder();
                foreach (var err in result.Errors)
                {
                    str.AppendFormat("•{0}\n", err.Description);
                }
                
                throw new BadRequestException($"{str}");
            }
        }

      


        public void AddToCash(List<string>Users)
        {

            if (!_memoryCache.TryGetValue("LogOutUsers", out LogoutCachingData CurrentData))
            {

                var CashingData = new LogoutCachingData()
                {
                    UsersId = Users
                    
                };


                var cacheEntryOptions = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.NeverRemove);

                
                _memoryCache.Set("LogOutUsers", CashingData, cacheEntryOptions);
            }
       

        }

        private string GetStoredRefreshtoken(string userId)
        {
            if (_memoryCache.TryGetValue("RefreshToken", out Dictionary<string, List<string>> Refresh))
            {
                return Refresh[userId].First();
            }
            else return "";
            
        }
        private void StoreRefreshtoken(string RefreshToken)
        {
            if (!_memoryCache.TryGetValue("RefreshToken", out Dictionary<string,string>Refresh))
            {

                var userId = getUserId(RefreshToken);

               
                Refresh[userId]=RefreshToken;

                  var cacheEntryOptions = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.Normal);


                _memoryCache.Set("RefreshToken", Refresh, cacheEntryOptions);
            }
            else
            {

                var userId = getUserId(RefreshToken);

                if (Refresh[userId]!=null)
                {
                    Refresh.Remove(userId);
                }
                else
                    Refresh[userId]=RefreshToken;

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.Normal);


                _memoryCache.Set("RefreshToken", Refresh, cacheEntryOptions);

            }

        }




        public void ClearCach()
        {
            _memoryCache.Remove("LogOutUsers");
        }



        //public async Task<string> RefreshToken(string Token)
        //{
        //    var jwt = new JwtSecurityTokenHandler().ReadJwtToken(Token);
        //    string userId = jwt.Claims.First(c => c.Type == "uid").Value;
           
           
        //    var user = await _userManager.FindByIdAsync(userId);

        //    var NewToken  =  await GenerateToken(user);
                     
        //    return  new JwtSecurityTokenHandler().WriteToken(NewToken);
        //}


         public async void LogoutAllUsers()
        {
            var filePath = Path.Combine("C:\\Projects\\git\\HRL_eave_Manegment\\HrManegment.Api", "appSettings.json");

         

            string json = File.ReadAllText(filePath);

            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);









            var x = jsonObj["JwtSettings"]["Audience"];

            jsonObj["JwtSettings"]["Audience"] = x+1;





            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);

            File.WriteAllText(filePath, output);
        }

        private string GenerateKeyForJwt()
        {
            var rng = new RNGCryptoServiceProvider();
            var keyBytes = new byte[32];
            rng.GetBytes(keyBytes);

            
            string jwtKey = Convert.ToBase64String(keyBytes);

            return jwtKey;

        }


        private string getUserId(string RefreshToken)
        {
            int dollarIndex = RefreshToken.IndexOf('$');
            if (dollarIndex != -1)
            {
                return RefreshToken.Substring(dollarIndex);
            }
            return "";
        }


        private string RefreshTokenGen(string id)
        {
            var rng = RandomNumberGenerator.Create();

            byte[] refreshTokenBytes = new byte[32];

            rng.GetBytes(refreshTokenBytes);

            string refreshToken = Convert.ToBase64String(refreshTokenBytes)

                .TrimEnd('=')

                .Replace('+', '-')

                .Replace('/', '_');
            refreshToken += '$' + id;

            return refreshToken;
        }


        private async Task<JwtSecurityToken> GeneratejwtToken(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var userClaims = await _userManager.GetClaimsAsync(user);

            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

            _memoryCache.TryGetValue("LogoutCachingData", out LogoutCachingData cachedData);


            var claims = new[]

        {

                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),

                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                new Claim(JwtRegisteredClaimNames.Email, user.Email),

                new Claim("uid", user.Id),

            }

        .Union(userClaims)

        .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(

               issuer: _jwtSettings.Issuer,

               audience: _jwtSettings.Audience,

               claims: claims,

               expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),

               signingCredentials: signingCredentials);


            return jwtSecurityToken;
        }



        private async Task<(JwtSecurityToken, string)> GenerateToken(string userId) //public?

        {

            var jwtSecurityToken = await GeneratejwtToken(userId);

            string refreshToken = RefreshTokenGen(userId);

            StoreRefreshtoken(refreshToken);

            return (jwtSecurityToken, refreshToken);

        }


        private bool TokenChecker(string userRefreshToken, string storedRefreshToken)

        {

            return userRefreshToken == storedRefreshToken;



        }


    }
}
