using Microsoft.AspNetCore.Mvc;
using WebBiblioteca.Models;
using WebBiblioteca.Data;
//using MySql.Data.MySqlClient;
using MySqlConnector;
using Dapper;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;
using Microsoft.AspNetCore.Connections;
using ZstdSharp.Unsafe;

namespace WebBiblioteca.Controllers {
    public class LivrosController : Controller {
        private readonly MySqlConnectionFactory _connectionFactory;

        public LivrosController(MySqlConnectionFactory connectionFactory) {
            _connectionFactory = connectionFactory;
        }


        public IActionResult Delete(int id) {
            Livro livro = null;

            //using (var conn = new MySqlConnection("sua_string"))
            using var conexao = _connectionFactory.CreateConnection();


            var cmd = new MySqlCommand("SELECT * FROM livro WHERE sequencial=@sequencial", conexao);
            cmd.Parameters.AddWithValue("@sequencial", id);

            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                livro = new Livro
                {
                    Sequencial = reader.GetInt32("sequencial"),
                    Titulo = reader.GetString("titulo")
                };
            }

            return View(livro);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id) {

            using var conexao = _connectionFactory.CreateConnection();

            String sql = "DELETE FROM livro WHERE sequencial = @sequencial";

            using (var cmd = new MySqlCommand(sql, conexao))
            {
                cmd.Parameters.AddWithValue("@sequencial", id);
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");

        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Livro livro) {

            if (!ModelState.IsValid)
            {
                return View(livro);  // volta pra tela com erros
            }
            using var conexao = _connectionFactory.CreateConnection();

            String sql = "INSERT INTO livro (sequencial , titulo , tombo , cod_categoria ) values (@sequencial , @titulo , @tombo , @cod_categoria)";

            var cmd = new MySqlCommand(sql, conexao);
            {
                cmd.Parameters.AddWithValue("@sequencial", livro.Sequencial);
                cmd.Parameters.AddWithValue("@titulo", livro.Titulo);
                cmd.Parameters.AddWithValue("@tombo", livro.Tombo?.Trim());
                cmd.Parameters.AddWithValue("@cod_categoria", livro.Cod_Categoria);

                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");

        }

        public IActionResult Index(int pagina = 1) {

            int quantidadePorPagina = 50;
            int deslocamento = (pagina - 1) * quantidadePorPagina;

            using var conexao = _connectionFactory.CreateConnection();

            var livros = conexao.Query<Livro>(
                 @"SELECT 
                     l.sequencial, 
                     l.titulo, 
                     l.tombo, 
                     l.cod_categoria, 
                     c.cor AS cor_categoria 
                 FROM livro l 
                 INNER JOIN categoria c 
                 ON c.cod_categoria = l.cod_categoria 
                 ORDER BY l.sequencial
                 LIMIT @Quantidade OFFSET @Deslocamento",
             new
             {
                 Quantidade = quantidadePorPagina,
                 Deslocamento = deslocamento
             }).ToList();

            ViewBag.PaginaAtual = pagina;

            return View(livros);
        }

        public IActionResult Edit(int id) {

            Livro livro = null;

            using var conexao = _connectionFactory.CreateConnection();

            string sql = "SELECT * FROM livro WHERE sequencial = @sequencial";

            using (var cmd = new MySqlCommand(sql, conexao))
            {

                cmd.Parameters.AddWithValue("@sequencial", id);

                conexao.Open();

                using (var reader = cmd.ExecuteReader())

                {
                    if (reader.Read())
                    {
                        livro = new Livro
                        {
                            Sequencial = reader.GetInt32("sequencial"),
                            Titulo = reader.GetString("titulo"),
                            Tombo = reader.GetString("tombo"),
                            Cod_Categoria = reader.GetString("cod_categoria")
                        };
                    }
                }

                return View(livro);
            }
        }

        [HttpPost]
        public IActionResult Edit(Livro livro) {
            using var conexao = _connectionFactory.CreateConnection();

            conexao.Open();

            string sql = @"UPDATE livro 
                       SET titulo=@titulo, 
                           tombo=@tombo,
                           cod_categoria=@cod_categoria
                       WHERE sequencial = @sequencial";

            using (var cmd = new MySqlCommand(sql, conexao))
            {
                cmd.Parameters.AddWithValue("@sequencial", livro.Sequencial);
                cmd.Parameters.AddWithValue("@titulo", livro.Titulo);
                cmd.Parameters.AddWithValue("@tombo", livro.Tombo);
                cmd.Parameters.AddWithValue("@cod_categoria", livro.Cod_Categoria);

                conexao.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}