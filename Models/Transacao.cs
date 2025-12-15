namespace Itau_Backend.Models
{
    public class Transacao
    {
        public Transacao(decimal valor, DateTime dataHora)
        {
            Valor = valor;
            DataHora = dataHora;
        }
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataHora { get; set; }
    }
}
