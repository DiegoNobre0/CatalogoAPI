using APICatalogo_.Context;
using APICatalogo_.DTOs;
using APICatalogo_.Models;
using APICatalogo_.Pagination;
using APICatalogo_.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace APICatalogo_.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProdutosController : ControllerBase
    {
        //INJEÇÃO DE DEPENDENCIA
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public ProdutosController(IUnitOfWork context, IMapper mapper)
        {
            _uof = context;
            _mapper = mapper;

        }
        
        [HttpGet("menorpreco")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosPrecos()
        {
            var produto = await _uof.ProdutoRepository.GetProdutosPorPreco();
            var produtoDto = _mapper.Map<List<ProdutoDTO>>(produto);
            return produtoDto;
        }

        //retorna uma lista de objetos produtos.
        //poderia usar List<>
        //ActionResult é usado para o tipo que ele quer retornar, ou tipo (IEnumerable) ou metodo (NotFound).

        /// <summary>
        /// Exibe uma relação dos produtos
        /// </summary>
        /// <returns>Retorna uma lista de objetos Produto</returns>
        //produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>>
            Get([FromQuery] ProdutoParameters produtosParameters)
        {
            try
            {
                var produtos = await _uof.ProdutoRepository.GetProdutos(produtosParameters);

                var  produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);

                var metadata = new
                {
                    produtos.TotalCount,
                    produtos.PageSize,
                    produtos.CurrentPage,
                    produtos.TotalPages,
                    produtos.HasNext,
                    produtos.HasPrevious
                };

                Response.Headers.Add("X-Pagonation", JsonConvert.SerializeObject(metadata));

                
                if (produtos is null)
                {
                    return NotFound();
                }
                return produtosDto;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sya solicitação");
            }

        }
        /// <summary>
        /// Obtem um produto pelo seu Id
        /// </summary>
        /// <param name="id">Código do produto</param>
        /// <returns>Um objeto Produto</returns>
        //metodo para retorno de acordo com o ID do produto
        //produtos/1
        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public async Task<ActionResult<ProdutoDTO>> Get(int id)
        {
            try
            {
                var produto = await _uof.ProdutoRepository.GetById(p => p.ProdutoId == id);
                
                if (produto is null)
                {
                    return NotFound("Produto não encontrado");
                }
                
                var produtoDto = _mapper.Map<ProdutoDTO>(produto);

                return produtoDto;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sya solicitação");
            }
        }

        //produtos
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]ProdutoDTO produtoDto)
        {

            //metodo vai incluir o produto no contexto.
            //metodo SaveChanges vai salvar no banco de dados.
            var produto = _mapper.Map<Produto>(produtoDto);
            
            _uof.ProdutoRepository.Add(produto);
            await _uof.Commit();

            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

            // CreatedAtRouteResult que é uma actionresult que retorna um 201 e
            //aciona a rota "ObterProduto", e vai usar o metodo GET e retorna os dados do produto
            return new CreatedAtRouteResult("ObterProduto",
                new { id = produto.ProdutoId }, produtoDTO);
        }

        //produtos/1
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody]ProdutoDTO produtoDto)
        {
            if (id != produtoDto.ProdutoId)
            {
                return BadRequest();
            }

            var produto = _mapper.Map<Produto>(produtoDto);

            _uof.ProdutoRepository.Update(produto);

            await _uof.Commit();

            return Ok();
        }
        
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProdutoDTO>> Delete(int id)
        {
            var produto = await _uof.ProdutoRepository.GetById(p =>p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("Produto não localizado...");
            }
            _uof.ProdutoRepository.Delete(produto);
            await _uof.Commit();

            var produtoDto = _mapper.Map<ProdutoDTO>(produto);
            
            return produtoDto;
        }


    }
}
