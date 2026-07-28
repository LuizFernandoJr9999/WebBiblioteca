using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations;

namespace WebBiblioteca.Models {
    public class Categoria {
        [Required(ErrorMessage = "O código da categoria é obrigatório!")]
        [StringLength(10, ErrorMessage = "O código deve ter no máximo 10 caracteres")]
        public string Cod_Categoria { get; set; } = string.Empty;


        [Required(ErrorMessage = "A categoria é obrigatória!")]
        [StringLength(40, ErrorMessage = "A categoria deve ter no máximo 40 caracteres")]
        public string CategoriaNome { get; set; } = string.Empty;


        [Required(ErrorMessage = "A cor é obrigatória!")]
        [StringLength(30, ErrorMessage = "A cor deve ter no máximo 30 caracteres")]
        public string Cor { get; set; } = string.Empty;
    }
}