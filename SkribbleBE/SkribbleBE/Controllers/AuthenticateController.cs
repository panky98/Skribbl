using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Authentication;
using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace SkribbleBE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ProjekatContext _context;

        public AuthenticateController(ProjekatContext context, IConfiguration configuration)
        {
            this._config = configuration;
            this._context = context;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            //provera da li korisnik postoji
            Korisnik k = this._context.Korisnici.Where(x => x.Username == model.Username && x.Password == model.Password).FirstOrDefault();
            if (k == null)
            {
                return Unauthorized();
            }
            //generisanje tokena koji se vraca na FE
            var tokenString = GenerateJWTToken(k);
            return Ok(new
            {
                token = tokenString,
                userDetails = k,
            });

        }

        string GenerateJWTToken(Korisnik userInfo)
        {
            //IssuerSigningKey, to su bajtovi u sustini postavljenog security key-a
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            //nakon toga se vrsi kodiranje tih bajtova, i ta kodirana verzija se smeta u token
            //taj SecretKey je u sustini jedan stepen zastite, bez da se zna on, niko sa strane ne moze da generise pravi token!!
            //nakon prijave kad se salje token, prilikom authorizacije se taj proces verovatno obavlja obrnuto, vrsi se dekodiranje do niza bajtova koji predstavljaju SecretKey
            //i proverava se jel se poklapa sa ocekivanim secretkey-om
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //prateci podaci u tokenu
            var claims = new[]
            {
                   new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
                   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var token = new JwtSecurityToken(
            //drugi stepen zastite issuer
            issuer: _config["Jwt:Issuer"],
            //treci audience
            audience: _config["Jwt:Audience"],
            claims: claims,
            //duzina trajanja tokena
            expires: DateTime.Now.AddMinutes(180),
            //kljuc
            signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
