using System.Data.SqlClient;

namespace Software2_API.Utilities
{
    public class DBContextUtility
    {
        static string SERVER = "OWLSCAR-MSI\\MSSQLSERVEROWL";
        static string DB_NAME = "TEST";
        static string DB_USER = "test";
        static string DB_PASSWORD = "123test";

        static string Conn = "server=" + SERVER + ";database=" + DB_NAME + ";user id=" + DB_USER + ";password=" + DB_PASSWORD + ";MultipleActiveResultSets=true";
        //mi conexion:
        SqlConnection Con = new SqlConnection(Conn);

        //procedimiento que abre la conexion sqlsever
        public void Connect()
        {
            try
            {
                Con.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //procedimiento que cierra la conexion sqlserver
        public void Disconnect()
        {
            Con.Close();
        }

        //funcion que devuelve la conexion sqlserver
        public SqlConnection CONN()
        {
            return Con;
        }
    }
}
