using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo_.Models
{
    //referencia a tabela "categories" no AppDbContext para
    //modificação de parametros padrões da classe no banco de dados
    [Table("Produtos")]
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }
        //Força a aplicar dados na propriedade "Name" e restringe a quantidade de strings.
        
        [Required(ErrorMessage ="O nome é obrigatorio")]
        [StringLength(80)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "A descrição deve ter no máximo {1} caracteres")]
        public string? Descricao { get; set; }
        //Força a aplicar dados na propriedade "Price" e restringe a quantidade de numeros e decimais.

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(8,2)")]
        [Range(1, 10000, ErrorMessage = "O preço deve estár entre {1} e {2}")]
        public decimal Preco { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 10)]
        public string? ImagemUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public int CategoriaId { get; set; }
        
        //JsonIgnore usado para bloquer a exibição da entidade "categoria" 
        [JsonIgnore]
        public Categoria? Categoria { get; set; }


    }
}
