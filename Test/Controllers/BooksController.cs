using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Test.Controllers
{
    [Route("books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        Connect conn = new Connect();
        [HttpGet]
        public List<Book> GetAllBooks()
        {
            conn.Connection.Open();

            List<Book> books = new List<Book>();

            string sql = "SELECT * FROM books";

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var book = new Book
                {
                    id = dr.GetInt32(0),
                    title = dr.GetString(1),
                    author = dr.GetString(2),
                    releaseDate = dr.GetDateTime(3)
                };

                books.Add(book);
            }

            conn.Connection.Close();
            return books;
        }

        [HttpGet("getById")]
        public Book GetById(int id)
        {
            conn.Connection.Open();
            string sql = "SELECT * FROM books WHERE id = @id";

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            cmd.Parameters.AddWithValue("@id", id);

            MySqlDataReader dr = cmd.ExecuteReader();

            dr.Read();
            var book = new Book
            {
                id = dr.GetInt32(0),
                title = dr.GetString(1),
                author = dr.GetString(2),
                releaseDate = dr.GetDateTime(3)
            };
            conn.Connection.Close();
            return book;
        }
    }
}
