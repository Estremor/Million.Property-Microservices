using Auth.Domain.Entities;
using Auth.Domain.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Domain.Base;
using Shared.Domain.Base.IRepository;
using Shared.Infrastructure.Base;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Auth.Domain.Services
{
    public class LoginDomainService : DomainService, ILoginDomainService
    {
        #region Fields
        private readonly IRepository<User> _userRepository;
        private readonly string SecurityKey;
        #endregion

        #region C'tor
        public LoginDomainService(IRepository<User> userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            SecurityKey = configuration.GetSection(nameof(SecurityKey))?.Value ?? string.Empty;
        }
        #endregion

        #region Methods
        public async Task<ActionResult> FindUserAsync(User user)
        {
            var userEntity = await _userRepository.ListAsync(x => x.UserName == user.UserName && x.Password == user.Password);
            if (userEntity is null || userEntity.Count == 0)
            {
                return new ActionResult { IsSuccessful = false, ErrorMessage = "no se encontraron resultados" };
            }
            return new ActionResult { IsSuccessful = true, Result = userEntity?.FirstOrDefault() ?? new User() };
        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };
            SymmetricSecurityKey key = new(System.Text.Encoding.UTF8.GetBytes(SecurityKey));
            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256Signature);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(6),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        #endregion
    }
}
