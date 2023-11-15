using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using webapi.core.Constants;
using webapi.core.Entities;
using webapi.core.Models;
using webapi.core.Models.Requests;
using webapi.core.Models.Responses;
using webapi.repositories.contracts;
using webapi.services.contracts;
using webapi.services.Managers;

namespace webapi.services
{
    public class AuthorizationService : BaseService, IAuthorizationService
    {
        private readonly IUserRepository _userRepository;

        public AuthorizationService(IUserRepository userRepository,

            IHttpContextAccessor httpContextAccessor,
            IMapper mapper) : base(httpContextAccessor, mapper)
        {
            _userRepository = userRepository;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            UserEntity userEntity = await _userRepository.GetAsync(loginRequest.Login);

            if (userEntity is not null) 
            {
                HashManager hashManager = new();

                string hashedPassword = userEntity.Password.Substring(0, userEntity.Password.Length - HashManagerConstants.SaltBase64Size);
                string salt = userEntity.Password.Substring(userEntity.Password.Length - HashManagerConstants.SaltBase64Size, HashManagerConstants.SaltBase64Size);

                string hash = hashManager.GetHash(loginRequest.Password, salt);

                if (hashedPassword.Equals(hash))
                {
                    User user = Mapper.Map<User>(userEntity);

                    return new LoginResponse
                    {
                        User = user,
                        Token = GenerateToken(userEntity)
                    };
                }
                else
                {
                    throw new Exception("Wrong password");
                }
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        private string GenerateToken(UserEntity user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };

            JwtSecurityToken jwt = new JwtSecurityToken(
                AuthorizationConstants.Issuer,
                AuthorizationConstants.Audience,
                claims,
                null,
                DateTime.UtcNow.AddHours(AuthorizationConstants.TokenLifetime),
                new SigningCredentials(AuthorizationConstants.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
