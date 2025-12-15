using Itau_Backend.Models;

namespace Itau_Backend.Repository
{
    public interface ITransacaoRepository
    {
        void Nova(Transacao item);
        List<Transacao> Listar();
        void Deletar(int id);
    }
}
