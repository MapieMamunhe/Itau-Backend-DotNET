using Itau_Backend.Models;
using Itau_Backend.Models.DTO;
using Itau_Backend.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Itau_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransacaoController : Controller
    {
        private readonly ILogger<TransacaoController> _logger;
        private readonly ITransacaoRepository _transacaoRepository;
        public TransacaoController(ILogger<TransacaoController> logger,
            ITransacaoRepository transacaoRepository)
        {
            _logger = logger;
            _transacaoRepository = transacaoRepository;
        }
        [HttpPost]
        public IActionResult Post(TransacaoRequest transacao)
        {
            _transacaoRepository.Nova (transacao.ToEntity());
            return Created();
        }
        [HttpGet]
        public ActionResult<List<TransacaoResponse>> Get()
        {
            var transactions = _transacaoRepository.Listar().Select(item=>item.ToDto());
            return Ok(transactions);
        }
    }
}
