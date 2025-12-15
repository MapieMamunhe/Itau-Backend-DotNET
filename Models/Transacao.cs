namespace Itau_Backend.Models
{
    public class Transacao
    {
        public Transacao()
        {
        }

        public Transacao(decimal valor, DateTime dataHora)
        {
            Valor = valor;
            DataHora = dataHora;
        }

        public Transacao(int id, decimal valor, DateTime dataHora)
        {
            Id = id;
            Valor = valor;
            DataHora = dataHora;
        }

        public int Id { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataHora { get; set; }
    }
}
