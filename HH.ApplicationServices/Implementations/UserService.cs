using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HH.ApplicationServices.Services.Interfaces;
using HH.Data.SqlServer;
using HH.Identity.Constants;
using HH.Identity.Entities;
using HH.Identity.Models;
using HH.Identity.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HH.ApplicationServices.Services.Implementations
{
    public class UserService: IUserService
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly JWT jwt;
        public UserService(
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IOptions<JWT> jwt,
            ApplicationDbContext context)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwt = jwt.Value;
        }

        public async Task<string> RegisterAsync(RegisterModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            
            var userWithSameEmail = await userManager.FindByEmailAsync(model.Email);
            
            if (userWithSameEmail == null)
            {
                return $"User Registered with username {user.UserName}";
            }
            else
            {
                return $"Email {user.Email } is already registered.";
            }
        }

        public async Task<AuthenticationModel> LoginAsync(TokenRequestModel model)
        {
            var authenticationModel = new AuthenticationModel();

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                
                authenticationModel.Message = $"No Accounts Registered with {model.Email}.";
                
                return authenticationModel;
            }

            if (await userManager.CheckPasswordAsync(user, model.Password))
            {
                authenticationModel.IsAuthenticated = true;
                
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                
                authenticationModel.Token = new JwtSecurityTokenHandler()
                    .WriteToken(jwtSecurityToken);
                
                authenticationModel.Email = user.Email;
                
                authenticationModel.UserName = user.UserName;
                
                var rolesList = await userManager
                    .GetRolesAsync(user)
                    .ConfigureAwait(false);
                
                authenticationModel.Roles = rolesList.ToList();

                if (user.RefreshTokens.Any(a => a.IsActive))
                {
                    var activeRefreshToken = user.RefreshTokens
                        .Where(a => a.IsActive == true)
                        .FirstOrDefault();

                    if (activeRefreshToken != null)
                    {
                        authenticationModel.RefreshToken = activeRefreshToken.Token;
                        authenticationModel.RefreshTokenExpiration = activeRefreshToken.Expires;
                    }
                }
                else
                {
                    var refreshToken = CreateRefreshToken();
                    
                    authenticationModel.RefreshToken = refreshToken.Token;
                    authenticationModel.RefreshTokenExpiration = refreshToken.Expires;
                    
                    user.RefreshTokens.Add(refreshToken);

                    context.Update(user);
                    await context.SaveChangesAsync();
                }
                
                return authenticationModel;
            }
            
            authenticationModel.IsAuthenticated = false;
            
            authenticationModel.Message = $"Incorrect Credentials for user {user.Email}.";
            
            return authenticationModel;
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            
            var roles = await userManager.GetRolesAsync(user);
            
            var roleClaims = new List<Claim>();
            
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }
            
            var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("uid", user.Id)
                }
                .Union(userClaims)
                .Union(roleClaims);
            
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            
            return jwtSecurityToken;
        }
        
        public async Task<string> AddRoleAsync(AddRoleModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return $"No Accounts Registered with {model.Email}.";
            }

            if (await userManager.CheckPasswordAsync(user, model.Password))
            {
                var roleExist = Enum.GetNames(typeof(Authorization.Roles))
                    .Any(x => x.ToLower() == model.Role.ToLower());

                if (roleExist)
                {
                    var validRole = Enum.GetValues(typeof(Authorization.Roles))
                        .Cast<Authorization.Roles>()
                        .Where(x => x.ToString().ToLower() == model.Role.ToLower()).FirstOrDefault();
                    
                    await userManager.AddToRoleAsync(user, validRole.ToString());
                    
                    return $"Added {model.Role} to user {model.Email}.";
                }
                
                return $"Role {model.Role} not found.";
            }
            
            return $"Incorrect Credentials for user {user.Email}.";
        }

        public async Task<AuthenticationModel> RefreshTokenAsync(string token)
        {
            var authenticationModel = new AuthenticationModel();//Создание нового объекта Response
            
            var user = context 
                .Users
                .SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));//Проверка наличия подходящего прльзователя для токена
            
            if (user == null)//Если подходящего пользователя нет
            {
                authenticationModel.IsAuthenticated = false;
                
                authenticationModel.Message = $"Token did not match any users.";
                
                return authenticationModel;
            }
            
            var refreshToken = user.RefreshTokens
                .Single(x => x.Token == token);
            
            if (!refreshToken.IsActive)
            {
                authenticationModel.IsAuthenticated = false;
                
                authenticationModel.Message = $"Token Not Active.";
                
                return authenticationModel;
            }
            
            //Отменить текущий токен обновления
            refreshToken.Revoked = DateTime.UtcNow;
            
            //Сгенерируйте новый токен обновления и сохраните в базе данных
            var newRefreshToken = CreateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            
            context.Update(user);
            context.SaveChanges();
            
            //Создает новый jwt
            authenticationModel.IsAuthenticated = true;
            
            JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
            
            authenticationModel.Token = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);
            
            authenticationModel.Email = user.Email;
            authenticationModel.UserName = user.UserName;
            
            var rolesList = await userManager
                .GetRolesAsync(user)
                .ConfigureAwait(false);
            
            authenticationModel.Roles = rolesList.ToList();
            authenticationModel.RefreshToken = newRefreshToken.Token;
            authenticationModel.RefreshTokenExpiration = newRefreshToken.Expires;
            
            return authenticationModel;
        }

        public ApplicationUser GetById(string id)
        {
            return context.Users.Find(id);
        }

        public bool RevokeToken(string token)
        {
            var user = context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
            
            // вернет false, если пользователь с токеном не найден
            if (user == null) return false;
            
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);
            
            // вернет false, если токен не активен
            if (!refreshToken.IsActive) return false;
            
            refreshToken.Revoked = DateTime.UtcNow;
            
            context.Update(user);
            context.SaveChanges();
            
            return true;
        }

        private RefreshToken CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            
            using(var generator = new RNGCryptoServiceProvider())
            {
                generator.GetBytes(randomNumber);
                
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomNumber),
                    Expires = DateTime.UtcNow.AddDays(10),
                    Created = DateTime.UtcNow
                };
            }
        }
        
    }
}