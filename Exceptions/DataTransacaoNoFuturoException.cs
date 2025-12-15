namespace Itau_Backend.Exceptions
{
    public class DataTransacaoNoFuturoException:Exception
    {
        public DataTransacaoNoFuturoException():base("A data da transação não deve ser no futuro.")
        {
        }
    }
}
