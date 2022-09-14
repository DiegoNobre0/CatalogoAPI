using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo_.Models
{
    //referencia a tabela "categories" no AppDbContext para
    //modificação de parametros padrões da classe no banco de dados
    [Table("Categorias")]
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }

        //Força a aplicar dados na entidade "Name" e restringe a quantidade de strings.
        [Required]
        [StringLength(80)]
        public string? Nome { get; set; }
        
        [Required]
        [StringLength(300)]
        public string? ImagemUrl { get; set; }
        public ICollection<Produto> Produtos { get; set; }

        public Categoria()
        {
            Produtos = new Collection<Produto>();
        }
    }
}
