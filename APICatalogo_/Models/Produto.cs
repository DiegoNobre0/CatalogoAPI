using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo_.Models
{
    //referencia a tabela "categories" no AppDbContext para
    //modificação de parametros padrões da classe no banco de dados
    [Table("Produtos")]
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }
        //Força a aplicar dados na entidade "Name" e restringe a quantidade de strings.
        [Required]
        [StringLength(80)]
        public string? Nome { get; set; }
        [Required]
        [StringLength(300)]
        public string? Descricao { get; set; }
        //Força a aplicar dados na entidade "Price" e restringe a quantidade de numeros e decimais.
        [Required]
        [Column(TypeName ="decimal(10,2)")]
        public decimal Preco { get; set; }
        [Required]
        [StringLength(300)]
        public string? ImagemUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }


    }
}
