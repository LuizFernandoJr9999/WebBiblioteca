using System.ComponentModel.DataAnnotations;

namespace WebBiblioteca.Models {
    public class Livro {

        public int Sequencial { get; set; }

        [Required(ErrorMessage = "O Título é obrigatório!")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O Tombo é obrigatório!")]
        public string Tombo { get; set; }
        
        [Required(ErrorMessage = "A Categoria é obrigatória!")]
        public string Cod_Categoria { get; set; }
    }
}
