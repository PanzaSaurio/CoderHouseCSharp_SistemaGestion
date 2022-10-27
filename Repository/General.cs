using System.Data.SqlClient;

namespace CoderHouse_SistemaGestion.Repository
{
    public class General
    {

        public static string connectionString()
        {
            SqlConnectionStringBuilder connectionbuilder = new();
            connectionbuilder.DataSource = "MPS001\\SQLEXPRESS";
            connectionbuilder.InitialCatalog = "SistemaGestion";
            connectionbuilder.IntegratedSecurity = true;
            var cs = connectionbuilder.ConnectionString;
            return cs;
        }

    }
}
