using Itau_Backend.Models;

namespace Itau_Backend.Repository
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private List<Transacao> transacoes = new List<Transacao>();
        public void Deletar(int id)
        {
            var transacao = transacoes.FirstOrDefault(item=>item.Id==id);
            if(transacao != null)
            {
                transacoes.Remove(transacao);
            }
        }

        public List<Transacao> Listar()
        {
           return transacoes;
        }

        public void Nova(Transacao transacao)
        {
            transacoes.Add(transacao);
        }
    }
}
