using Microsoft.AspNetCore.Mvc;
using WebBiblioteca.Models;

namespace WebBiblioteca.Controllers {
    public class LivrosController : Controller {

        private static List<Livro> _livros = new List<Livro>();

        static LivrosController() {
            _livros.Add(new Livro { Sequencial = 1, Titulo = "Dom Casmurro", Tombo = "avd56" }); 
            _livros.Add(new Livro { Sequencial = 2, Titulo = "1984", Tombo = "dff67" });
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

            
            //if (ModelState.IsValid)
            //{
                
                livro.Sequencial = _livros.Any() ? _livros.Max(l =>  l.Sequencial) + 1 : 1;
                _livros.Add(livro);

                // Por enquanto só vamos simular salvando
                return RedirectToAction("Index");
                

            //}

            //return View(livro);

            //return Content("MODEL OK");
            
        }

    }
}
