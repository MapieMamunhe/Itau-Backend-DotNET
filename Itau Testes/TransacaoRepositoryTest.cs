using Itau_Backend.Exceptions;
using Itau_Backend.Models;
using Itau_Backend.Repository;

namespace Itau_Testes
{
    public class TransacaoRepositoryTest
    {
        private readonly ITransacaoRepository _transacaoRepository;

        public TransacaoRepositoryTest()
        {
            _transacaoRepository = new TransacaoRepository();
        }

        [Fact]
        
        public void TransacaoShouldNotBeInTheFuture()
        {
            Transacao transacao = new Transacao
            {
                Id = 1,
                Valor = 100.00m,
                DataHora = DateTime.Now.AddDays(1) 
            };
            Assert
                .Throws<DataTransacaoNoFuturoException>(()=>_transacaoRepository.Nova(transacao));
        }
        [Fact]
        public void TransactionValueShouldNotBeLessThanZero()
        {
            Transacao transacao = new Transacao
            {
                Id = 1,
                Valor = -1.0m,
                DataHora = DateTime.UtcNow 
            };
            Assert
                .Throws<ValorDaTransacaoMenorQueZero>(() => _transacaoRepository.Nova(transacao));

        }
    }
}
