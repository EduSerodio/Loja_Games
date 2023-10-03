using FluentValidation;
using lojaGames.Model;
using lojaGames.Service;
using Microsoft.AspNetCore.Mvc;

namespace lojaGames.Controllers
{
    [Route("~/produtos")]
    [ApiController]
    
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly IValidator<Produto> _produtoValidator;

        public ProdutoController(IProdutoService produtoService, IValidator<Produto> produtoValidator)
        {
            _produtoService = produtoService;
            _produtoValidator = produtoValidator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _produtoService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var Resposta = await _produtoService.GetById(id);

            if(Resposta == null)
            {
                return NotFound();
            }

            return Ok(Resposta);
        }

        [HttpGet("nome/{nome}/ouconsole/{console}")]
        public async Task<ActionResult> GetByNomeOuConsole(string nome, string console)
        {
            return Ok(await _produtoService.GetByNomeOuConsole(nome, console));
        }

        [HttpGet("preco_incial/{min}/preco_final{max}")]
        public async Task<ActionResult> GetByPrecoIntervalo(decimal min, decimal max)
        {
            return Ok(await _produtoService.GetByPrecoIntervalo(min, max));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Produto produto)
        {
            var validarProduto = await _produtoValidator.ValidateAsync(produto);

            if (!validarProduto.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validarProduto);
            }

            var Resposta = await _produtoService.Create(produto);

            if(Resposta is null)
            {
                return NotFound("Produto e/ou Categoria não encontrados!");
            }

            return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Produto produto)
        {
            if(produto.Id == 0)
            {
                return BadRequest("Id do produto é inválido!");
            }

            var validarProduto= await _produtoValidator.ValidateAsync(produto);

            if (!validarProduto.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validarProduto);
            }

            var Resposta = await _produtoService.Update(produto);

            if(Resposta is null)
            {
                return NotFound("Produto não encontrado!");
            }

            return Ok(Resposta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var BuscaProduto = await _produtoService.GetById(id);

            if(BuscaProduto is null)
            {
                return NotFound("Produto não encontrado!");
            }

            await _produtoService.Delete(BuscaProduto);

            return NoContent();
        }
    }
}