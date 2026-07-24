using Microsoft.AspNetCore.Mvc;
using WebBiblioteca.Models;

namespace WebBiblioteca.Controllers {
    public class LivrosController : Controller {

        private static List<Livro> _livros = new List<Livro>();

        static LivrosController() {
            _livros.Add(new Livro { Sequencial = 1, Titulo = "Dom Casmurro", Tombo = "avd56", Cod_Categoria = "Romance" });
            _livros.Add(new Livro { Sequencial = 2, Titulo = "1984", Tombo = "dff67", Cod_Categoria = "Ação" });
        }

        public IActionResult Index() {
            //return View();
            //var livros = new List<Livro>
            //{
            //     new Livro {Sequencial = 1 , Titulo = "Dom Casmurro" , Tombo = "avd56"} ,
            //     new Livro {Sequencial = 2 , Titulo = "1984" , Tombo = "dff67"}
            //};
            return View(_livros);
        }


        // GET: Livros/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Livros/Create
        [HttpPost]
        public IActionResult Create(Livro livro) {

            Console.WriteLine("CHEGOU AQUI"); // Teste

            //return Content("CHEGOU NO POST");


            if (!ModelState.IsValid)
                return View(livro);

            //{

            livro.Sequencial = _livros.Any() ? _livros.Max(l => l.Sequencial) + 1 : 1;
            _livros.Add(livro);

            // Por enquanto só vamos simular salvando
            return RedirectToAction("Index");


            //}

            //return View(livro);

            //return Content("MODEL OK");

        }

        public IActionResult Edit(int id) {
            var livro = _livros.FirstOrDefault(l => l.Sequencial == id);
            if (livro == null)
            {
                return NotFound();
            }
            return View(livro);
        }

        [HttpPost]
        public IActionResult Edit(Livro livro) {
            if (!ModelState.IsValid)
                return View(livro);
            var existingLivro = _livros.FirstOrDefault(l => l.Sequencial == livro.Sequencial);
            if (existingLivro == null)
            {
                return NotFound();
            }
            existingLivro.Titulo = livro.Titulo;
            existingLivro.Tombo = livro.Tombo;
            existingLivro.Cod_Categoria = livro.Cod_Categoria;
            return RedirectToAction("Index");

        }

        public IActionResult Delete(int id) {

            var livro = _livros.FirstOrDefault(l => l.Sequencial == id);
            if (livro == null)
            {
                return NotFound();
            }
            return View(livro);
        }

        [HttpPost]
        public IActionResult Delete(Livro livro) {
            var livroExistente = _livros.FirstOrDefault(l => l.Sequencial == livro.Sequencial);
            if (livroExistente == null)
            {
                return NotFound();
            }

            if (livroExistente != null)
            {
                _livros.Remove(livroExistente);
            }

            return RedirectToAction("Index");
        }
    }
}
