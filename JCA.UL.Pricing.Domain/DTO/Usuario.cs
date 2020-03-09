using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JCA.UL.Pricing.Domain.Entities;

namespace JCA.UL.Pricing.Domain.DTO
{

    public class Usuario
    {

        public int UsuarioId { get; set; }


        public int TipoUsuarioId { get; set; }


        public string Nome { get; set; }


        public string NomeUsuario { get; set; }


        public string Senha { get; set; }


        public string Imei { get; set; }

        public int RelacionamentoId { get; set; }

        // public Parceiro Parceiro { get; set; }
        // public Passageiro Passageiro { get; set; }
        // public Combo Motorista { get; set; }
        // public UsuarioTTBus UsuMotorista{ get; set; }
        // public UsuarioTTBus UsuOperador{ get; set; }

        public static implicit operator Usuario(UsuarioMap entity)
        {
            return entity == null ? null : new Usuario()
            {
                UsuarioId = entity.UsuarioId,
                TipoUsuarioId = entity.TipoUsuarioId,
                Nome = entity.Nome,
                NomeUsuario = entity.NomeUsuario,
                Senha = entity.Senha,
                Imei = entity.Imei,
                RelacionamentoId = entity.RelacionamentoId
            };
        }
    }
}