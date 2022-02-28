using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.TokenOperations.Model;

namespace WebApi.TokenOperations
{
    public class TokenHandler{

        private IConfiguration Configuration{get;set;}

        public TokenHandler(IConfiguration configuration){
            Configuration = configuration;
        }

        public Token CreateAccessToken (User user)
        {
            Token tokenModel = new Token();
             SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));
            SigningCredentials credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            tokenModel.Expiration = System.DateTime.Now.AddMinutes(15);

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer:Configuration["Token:Issuer"],
                audience:Configuration["Token:Audience"],
                expires:tokenModel.Expiration,
                notBefore: System.DateTime.Now,
                signingCredentials:credentials
            );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            
            tokenModel.AccessToken = tokenHandler.WriteToken(securityToken);
            tokenModel.RefreshToken = CreateRefreshToken();

            return tokenModel;

        }

        public string CreateRefreshToken(){

            return Guid.NewGuid().ToString();
        }
    }
}