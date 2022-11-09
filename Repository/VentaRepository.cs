using CoderHouse_SistemaGestion.Models;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Numerics;

namespace CoderHouse_SistemaGestion.Repository
{
    public class VentaRepository
    {
        public static List<Venta> TraerVentas(int pIdUsuario)
        {

            var listaVentas = new List<Venta>();

            SqlConnectionStringBuilder connectionbuilder = new();
            connectionbuilder.DataSource = "MPS001\\SQLEXPRESS";
            connectionbuilder.InitialCatalog = "SistemaGestion";
            connectionbuilder.IntegratedSecurity = true;

            var cs = connectionbuilder.ConnectionString;

            using (SqlConnection conection = new SqlConnection(cs))
            {
                var query = @"SELECT Id
                                     ,Comentarios
                                     ,IdUsuario
                                FROM Venta
                               WHERE IdUsuario = @IdUsuario";

                conection.Open();

                using (SqlCommand cm = new SqlCommand(query, conection))
                {
                    cm.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.VarChar) { Value = pIdUsuario });
                    var reader = cm.ExecuteReader();
                    while (reader.Read())
                    {
                        var venta = new Venta();
                        venta.Id = Convert.ToInt32(reader.GetValue(0));
                        venta.Comentarios = reader.GetString(1);
                        venta.IdUsuario = Convert.ToInt32(reader.GetValue(2));

                        listaVentas.Add(venta);
                    }
                }
                conection.Close();
            }
            return listaVentas;
        }

        public static void CargarVenta(List<Producto> productos, int pIdUsuario)
        {
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {

                int idVenta;
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"Insert into Venta ( Comentarios, idUsuario) OUTPUT INSERTED.id
                                    values (@Comentarios, @idUsuario); 
                                   SELECT SCOPE_IDENTITY();";

                var paramComentarios = new SqlParameter();
                paramComentarios.ParameterName = "Comentarios";
                paramComentarios.SqlDbType = SqlDbType.VarChar;
                paramComentarios.Value = "NN";

                var paramIdUsuario = new SqlParameter();
                paramIdUsuario.ParameterName = "idUsuario";
                paramIdUsuario.SqlDbType = SqlDbType.BigInt;
                paramIdUsuario.Value = pIdUsuario;


                cmd.Parameters.Add(paramComentarios);
                cmd.Parameters.Add(paramIdUsuario);

                idVenta = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();

                ProductoVendidoRepository.CargarProductoVendido(productos, idVenta);


            }
        }

        public static void EliminarVenta(int idVenta)
        {
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                ProductoVendidoRepository.ActualizarStockDeProductos(idVenta);
                ProductoVendidoRepository.EliminarProductoVendidoPorVenta(idVenta);

                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"DELETE venta WHERE id = @idVenta; ";

                var paramidVenta = new SqlParameter();
                paramidVenta.ParameterName = "idVenta";
                paramidVenta.SqlDbType = SqlDbType.BigInt;
                paramidVenta.Value = idVenta;

                cmd.Parameters.Add(paramidVenta);


                cmd.ExecuteNonQuery();
                connection.Close();

            }
        }

        public static void EliminarVentaPorUsuario(int idUsuario)
        {
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                var listaVentas = new List<Venta>();
                listaVentas = VentaRepository.TraerVentas(idUsuario);

                foreach (var item in listaVentas)
                {
                    ProductoVendidoRepository.EliminarProductoVendidoPorVenta(item.Id);
                }


                connection.Open();

                foreach (var item in listaVentas)
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = @"DELETE venta WHERE id = @idVenta; ";

                    var paramidVenta = new SqlParameter();
                    paramidVenta.ParameterName = "idVenta";
                    paramidVenta.SqlDbType = SqlDbType.BigInt;
                    paramidVenta.Value = item.Id;

                    cmd.Parameters.Add(paramidVenta);
                    cmd.ExecuteNonQuery();
                }


                connection.Close();




            }
        }
    
    }
}