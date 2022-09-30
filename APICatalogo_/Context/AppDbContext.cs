using Microsoft.EntityFrameworkCore;
using APICatalogo_.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace APICatalogo_.Context
{
    //public class AppDbContext : DbContext
    public class AppDbContext : IdentityDbContext
    {
        //Class context responsavel pela conexão com o banco de dados e entidades
        public AppDbContext(DbContextOptions<AppDbContext> options) : base( options )
        {

        }

        public DbSet<Categoria>? Categorias { get; set; }
        public DbSet<Produto>? Produtos { get; set; }
    }
}
