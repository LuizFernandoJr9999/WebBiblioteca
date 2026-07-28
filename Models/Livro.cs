using System.ComponentModel.DataAnnotations;

namespace WebBiblioteca.Models {
    public class Livro {

        [Required(ErrorMessage = "O Sequencial é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O Sequencial deve ser maior que zero.")]
        public int? Sequencial { get; set; }

        [Required(ErrorMessage = "O Título é obrigatório!")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O Tombo é obrigatório!")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "O tombo deve ter entre 1 e 10 caracteres")]
        [RegularExpression(@"^\S+$", ErrorMessage = "O Tombo não pode conter espaços.")]
        public string Tombo { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "A Categoria é obrigatória!")]
        public string Cod_Categoria { get; set; }

        public String Cor_Categoria { get; set; } = string.Empty;

    }
}
