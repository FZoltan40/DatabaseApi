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
                    Id = dr.GetInt32(0),
                    Title = dr.GetString(1),
                    Author = dr.GetString(2),
                    ReleaseDate = dr.GetDateTime(3)
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
            string sql = "SELECT * FROM books WHERE Id = @Id";

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            cmd.Parameters.AddWithValue("@Id", id);

            MySqlDataReader dr = cmd.ExecuteReader();

            dr.Read();
            var book = new Book
            {
                Id = dr.GetInt32(0),
                Title = dr.GetString(1),
                Author = dr.GetString(2),
                ReleaseDate = dr.GetDateTime(3)
            };
            conn.Connection.Close();
            return book;
        }

        [HttpPost]
        public object AddNewRecord(CreateBookDto book)
        {

            conn.Connection.Open();

            string sql = "INSERT INTO `books`(`Title`, `Author`, `ReleaseDate`) VALUES (@Title,@Author,@date)";

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

            cmd.Parameters.AddWithValue("@Title", book.Title);
            cmd.Parameters.AddWithValue("@Author", book.Author);
            cmd.Parameters.AddWithValue("@date", book.ReleaseDate);

            cmd.ExecuteNonQuery();

            conn.Connection.Close();
            return new { message = "Sikeres hozzáadás.", result = book };
        }

        [HttpDelete]
        public object DeleteRecord(int id)
        {
            conn.Connection.Open();

            string sql = "DELETE FROM books WHERE id = @id";

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            conn.Connection.Close();
            return new { message = "Sikeres törlés." };
        }

        [HttpPut]
        public object UpdateRecord(int id, UpdateBookDto updateBookDto)
        {
            conn.Connection.Open();

            string sql = "UPDATE `books` SET `title`= @title,`author`= @author,`releaseDate`= @date WHERE `id` = @id";

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

            cmd.Parameters.AddWithValue("@Title", updateBookDto.Title);
            cmd.Parameters.AddWithValue("@Author", updateBookDto.Author);
            cmd.Parameters.AddWithValue("@date", updateBookDto.ReleaseDate);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            conn.Connection.Close();
            return new { message = "Sikeres frissítés.", result = updateBookDto };
        }
    }
}
