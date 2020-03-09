using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JCA.UL.Pricing.Business;
using JCA.UL.Pricing.Domain.DTO;
using JCA.UL.Pricing.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace UL.UTP.Pricing.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly UsuarioService _usuarioService;
        private readonly IConfiguration _configuration;
        public UsuarioController(UsuarioService usuarioService, 
                                 IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult LoginPassageiro(string user, string password, string imei)
        {
            try
            {
                Usuario usuario = _usuarioService.LoginPassageiro(user, password, imei);
                if (usuario == null)
                    return new JsonResult(new {sucesso = false, erro = "Usuário ou Senha inválida" });

                //string strToken = GetToken(usuario);

                return new JsonResult(new {sucesso = true, usuario = usuario //, token = strToken
                 });
            } 
            catch (Exception ex)
            {
                return new JsonResult(new {sucesso = false, erro = "Erro ao Logar: " + ex.Message });
            }
        }


        // private string GetToken(Usuario usuario)
        // {
        //     var tokenHandler = new JwtSecurityTokenHandler();
        //     var key = Encoding.ASCII.GetBytes(_configuration.GetSection(Startup.SysKey).Value);
        //     var tokenDescriptor = new SecurityTokenDescriptor
        //     {
        //         Subject = new ClaimsIdentity(new Claim[]
        //            {
        //                new Claim(ClaimTypes.Name, usuario.UsuarioId.ToString()),
        //            }),
        //         Expires = DateTime.UtcNow.AddMinutes(20),
        //         SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //     };
        //     var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        //     token.Payload["Email"] = usuario.NomeUsuario;
        //     return tokenHandler.WriteToken(token);
        // }
    }
}