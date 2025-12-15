using Itau_Backend.Models.DTO;

namespace Itau_Backend.Models
{
    public static class TransactionExtension
    {
        public static Transacao ToEntity(this TransacaoRequest transacao)
        {
            Transacao entity = new Transacao
            (
                transacao.valor,
                transacao.dataHora
            );
            return entity;
        }
        public static TransacaoResponse ToDto(this Transacao transacao)
        {
            TransacaoResponse dto = new TransacaoResponse
            (
                transacao.Valor,
                transacao.DataHora
            );
            return dto;
        }
        public static Transacao ToEntity(this TransacaoResponse transacao)
        {
            Transacao entity = new Transacao
            (
                transacao.valor,
                transacao.dataHora
            );
            return entity;
        }
    }
}
