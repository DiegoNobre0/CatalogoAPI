using Microsoft.EntityFrameworkCore;
using APICatalogo_.Models;

namespace APICatalogo_.Context
{
    public class AppDbContext : DbContext
    {
        //Class context responsavel pela conexão com o banco de dados e entidades
        public AppDbContext(DbContextOptions<AppDbContext> options) : base( options )
        {

        }

        public DbSet<Categoria>? Categorias { get; set; }
        public DbSet<Produto>? Produtos { get; set; }
    }
}
