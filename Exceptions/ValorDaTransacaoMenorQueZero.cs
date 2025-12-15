namespace Itau_Backend.Exceptions
{
    public class ValorDaTransacaoMenorQueZero: Exception
    {
        public ValorDaTransacaoMenorQueZero() : base("O valor da transação não deve ser menor que zero.")
        {
        }
    }
}
