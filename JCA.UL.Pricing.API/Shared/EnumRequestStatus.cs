using System.Runtime.Serialization;

namespace JCA.UL.Pricing.Shared.Enum
{
    public enum EnumRequestStatus
    {
        [EnumMember(Value = "Criado")] 
        Criado = 1,
        [EnumMember(Value = "Aguardando")] 
        Aguardando = 2,
        [EnumMember(Value = "Processando")] 
        Processando = 3,
        [EnumMember(Value = "Processado")] 
        Processado = 4,
        [EnumMember(Value = "Erro")] 
        Erro = 5
    }
}