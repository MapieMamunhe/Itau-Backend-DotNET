namespace Itau_Backend.Models.DTO
{
   public sealed record TransacaoRequest(decimal valor, DateTime dataHora);
    public sealed record TransacaoResponse(decimal valor, DateTime dataHora);

}
