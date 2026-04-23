using System.ComponentModel.DataAnnotations;

namespace GoodHamburger.Models
{
    public class ProductFormModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Informe o nome do produto.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no maximo 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe a descricao do produto.")]
        [StringLength(200, ErrorMessage = "A descricao deve ter no maximo 200 caracteres.")]
        public string Description { get; set; } = string.Empty;

        [Range(0.01d, 9999.99d, ErrorMessage = "O preco deve ser maior que zero.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Selecione uma categoria.")]
        public Guid CategoryId { get; set; }
    }
}
