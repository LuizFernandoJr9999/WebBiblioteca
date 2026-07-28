using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using WebBiblioteca.Data;
using WebBiblioteca.Models;
using System.Configuration;

namespace WebBiblioteca.Controllers {
    public class CategoriasController : Controller {
        private readonly MySqlContext _context;

        public CategoriasController(MySqlContext context) {
           _context = context;
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria) {
            if (!ModelState.IsValid)
            {
                return View(categoria);
            }


            using (var conn = _context.GetConnection())
            {
                conn.Open();

                string sql = @"INSERT INTO categoria
                              (Cod_Categoria, Categoria, Cor)
                              VALUES
                              (@codigo, @categoria, @cor)";


                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@codigo",
                        categoria.Cod_Categoria.Trim());

                    cmd.Parameters.AddWithValue("@categoria",
                        categoria.CategoriaNome.Trim());

                    cmd.Parameters.AddWithValue("@cor",
                        categoria.Cor.Trim());

                    cmd.ExecuteNonQuery();
                }
            }


            return RedirectToAction("Index");
        }

        public IActionResult Index() 
            {
            List<Categoria> categorias = new List<Categoria>();

            using (var conn = _context.GetConnection())
            {
                conn.Open();

                //string sql = "SELECT 'Teste' as cod_categoria , 'CATEGORIA' as categoria , 'VERDE' as cor FROM categoria";
                string sql = "SELECT cod_categoria , categoria , cor FROM categoria";

                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categorias.Add(new Categoria
                        {
                            Cod_Categoria = reader.GetString("cod_categoria"),
                            CategoriaNome = reader.GetString("categoria"),
                            Cor = reader.GetString("cor"),
                        });
                    }
                }
            }

            return View("~/Views/Categorias/Index.cshtml", categorias);
        }


    }

}
