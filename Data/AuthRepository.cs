using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace ManagementTasks.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext context;
        private readonly IConfiguration config;

        public AuthRepository(DataContext context, IConfiguration config)
        {
            this.config = config;
            this.context = context;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            var response = new ServiceResponse<int>();
            if (await UserExists(user.Name))
            {
                response.success = false;
                response.message = "User already Exists";
                return response;
            }

            CreatePassHash(password, out byte[] passSalt, out byte[] passHash);

            user.PasswordSalt = passSalt;
            user.PasswordHash = passHash;

            context.Users.Add(user);
            await context.SaveChangesAsync();

            response.data = user.Id;
            return response;
        }

        public async Task<ServiceResponse<string>> Login(string userName, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await context.Users.
                FirstOrDefaultAsync(u => u.Name.ToLower() == userName.ToLower());

            if (user is null)
            {
                response.success = false;
                response.message = "User not found.";
            }
            else if (!VerifyPassHash(password, user.PasswordSalt, user.PasswordHash))
            {
                response.success = false;
                response.message = "Wrong password";
            }
            else
            {
                response.data = GenerateToken(user);
            }
            return response;
        }

        public async Task<bool> UserExists(string userName)
        {
            if (await context.Users.AnyAsync(u => u.Name.ToLower() == userName.ToLower()))
            {
                return true;
            }
            return false;
        }

        private void CreatePassHash(string password, out byte[] passSalt, out byte[] passHash)
        {

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passSalt = hmac.Key;
                passHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));


            }
        }

        private bool VerifyPassHash(string password, byte[] passSalt, byte[] passHash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passHash);

            }

        }

        private string GenerateToken(User user)
        {

            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };

            var appSettingToken = config.GetSection("AppSittings:Token").Value;

            if (appSettingToken is null)
            {
                throw new Exception(" app setting token is null");
            }

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.
                UTF8.GetBytes(appSettingToken));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);



            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            SecurityToken token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);

        }
    }
}