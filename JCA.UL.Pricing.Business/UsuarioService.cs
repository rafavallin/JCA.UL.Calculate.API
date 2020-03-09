using System;
using JCA.UL.Pricing.Domain.DTO;
using JCA.UL.Pricing.Domain.Entities;
using JCA.UL.Pricing.Repository;
using System.Collections.Generic;
using System.Linq;

namespace JCA.UL.Pricing.Business
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _usuarioRep;

        public UsuarioService(UsuarioRepository usuarioRep)
        {
            _usuarioRep = usuarioRep;
        }
        
        public Usuario LoginPassageiro(string user, string password, string imei)
        {
            var usuario = (Usuario)_usuarioRep.Find(x => x.TipoUsuarioId == 2 && x.NomeUsuario == user
                              && x.Senha == password)
                              .FirstOrDefault();

            if (usuario.Imei == null)
            {
                usuario.Imei = imei;
                _usuarioRep.Update(usuario);
            }
            else if (usuario.Imei != imei)
            {
                throw new Exception("Aparelho diferente do Cadastrado, favor contactar Administrador");
            }
            return usuario;
        }
    }
}