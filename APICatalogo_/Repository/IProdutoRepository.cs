using APICatalogo_.Models;
using APICatalogo_.Pagination;

namespace APICatalogo_.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        //IEnumerable<Produto> GetProdutos(ProdutoParameters produtosParameters);

        Task<PagedList<Produto>> GetProdutos(ProdutoParameters parameters);
        Task<IEnumerable<Produto>> GetProdutosPorPreco();
    }
}
