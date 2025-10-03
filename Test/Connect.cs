using MySql.Data.MySqlClient;

namespace Test
{
    public class Connect
    {
        public MySqlConnection? Connection;
        private string _host;
        private string _db;
        private string _user;
        private string _password;

        private string ConnectioString;

        public Connect()
        {
            _host = "localhost";
            _db = "library";
            _user = "root";
            _password = "";

            ConnectioString = $"SERVER={_host}; DATABSASE={_db};UID={_user};PASSWORD={_password};SslMode=none";

            Connection = new MySqlConnection(ConnectioString);

        }
    }
}
