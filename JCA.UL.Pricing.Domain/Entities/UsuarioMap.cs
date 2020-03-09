using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JCA.UL.Pricing.Domain.DTO;

namespace JCA.UL.Pricing.Domain.Entities
{
    [Table("tb_usuarios")]
    public class UsuarioMap
    {
        [Key]
        [Column("usuarioId")]
        public int UsuarioId { get; set; }

        [Column("tipoUsuarioId")]
        public int TipoUsuarioId { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("nomeUsuario")]
        public string NomeUsuario { get; set; }

        [Column("senha")]
        public string Senha { get; set; }

        [Column("imei")]
        public string Imei { get; set; }
        [Column("relacionamentoId")]
        public int RelacionamentoId { get; set; }

        public static implicit operator UsuarioMap(Usuario entity)
        {
            return new UsuarioMap()
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