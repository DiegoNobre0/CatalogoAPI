using APICatalogo_.Context;
using APICatalogo_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo_.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        //retorna uma lista de objetos produtos.
        //poderia usar List<>
        //ActionResult é usado para o tipo que ele quer retornar, ou tipo (IEnumerable) ou metodo (NotFound).
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _context.Produtos.ToList();
            if (produtos is null)
            {
                return NotFound();
            }
            return produtos;
        }
        //metodo para retorno de acordo com o ID do produto
        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado");
            }
            return produto;
        }
        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null)
                return BadRequest();


            //metodo vai incluir o produto no contexto.
            //metodo SaveChanges vai salvar no banco de dados.

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            //CreatedAtRouteResult que é uma actionresult que retorna um 201 e
            //aciona a rota "ObterProduto", e vai usar o metodo GET e retorna os dados do produto
            return new CreatedAtRouteResult("ObterProduto",
                new { id = produto.ProdutoId }, produto);
        }
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if(id != produto.ProdutoId)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(produto);
        }

    }
}
