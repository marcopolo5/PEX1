using System;
using System.Data;
using System.Data.SqlClient;

namespace Elearning.Business
{
    public class Login
    {
        public void ValidateLogin(SqlConnection conn, string username, string password)
        {
            string query = "SELECT COUNT(*) FROM Users " +
                "WHERE (Username = @Username AND Password = @Password) OR " +
                "(Email = @Username AND Password = @Password)";

            SqlCommand command = new SqlCommand(query, conn);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);
            int count = Convert.ToInt32(command.ExecuteScalar());

            if (count != 1)
            {
                throw new Exception("Invalid credentials!");
            }
        }
    }

    
}
