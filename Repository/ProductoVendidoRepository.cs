using CoderHouse_SistemaGestion.Models;
using System.Data.SqlClient;
using System.Data;
using System.Numerics;

namespace CoderHouse_SistemaGestion.Repository
{
    public class ProductoVendidoRepository
    {
        public static List<Producto> TraerProductosVendidos(int pIdUsuario)
        {

            var listaProductosVendidos = new List<Producto>();

            SqlConnectionStringBuilder connectionbuilder = new();
            connectionbuilder.DataSource = "MPS001\\SQLEXPRESS";
            connectionbuilder.InitialCatalog = "SistemaGestion";
            connectionbuilder.IntegratedSecurity = true;



            var listaProductos = new List<Producto>();

            listaProductos = ProductoRepository.TraerProductos(pIdUsuario);


            var cs = connectionbuilder.ConnectionString;

            using (SqlConnection conection = new SqlConnection(cs))
            {

                var query = @"SELECT pv.Id
                                     ,pv.Stock
                                     ,pv.IdProducto
                                     ,pv.IdVenta
                                     ,v.IdUsuario
                                FROM ProductoVendido pv
                                JOIN Venta v ON v.id = pv.IdVenta
                               WHERE v.IdUsuario = @IdUsuario
                                 AND pv.IdProducto = @IdProducto";

                conection.Open();

                foreach (var item in listaProductos)
                {
                    using (SqlCommand cm = new SqlCommand(query, conection))
                    {
                        cm.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.VarChar) { Value = item.IdUsuario });
                        cm.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.VarChar) { Value = item.Id });
                        var reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            if (item.Id == Convert.ToInt32(reader.GetValue(2)) && item.IdUsuario == Convert.ToInt32(reader.GetValue(4)))
                            {
                                listaProductosVendidos.Add(item);
                            }

                        }
                        reader.Close();
                    }
                }

                conection.Close();
            }
            return listaProductosVendidos;
        }

        public static void EliminarProductoVendido(int idProducto)
        {
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"DELETE ProductoVendido WHERE IdProducto = @idProducto; ";

                var paramidProducto = new SqlParameter();
                paramidProducto.ParameterName = "idProducto";
                paramidProducto.SqlDbType = SqlDbType.BigInt;
                paramidProducto.Value = idProducto;

                cmd.Parameters.Add(paramidProducto);

                cmd.ExecuteNonQuery();
                connection.Close();

            }
        }


        public static void CargarProductoVendido(List<Producto> pProductos, int pidVenta)
        {
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {

                connection.Open();

                foreach (var i in pProductos)
                {
                    

                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = @"SET IDENTITY_INSERT Venta ON;
                                Insert into ProductoVendido ( Stock, idProducto,IdVenta) 
                                values (@Stock, @idProducto,@idVenta);                                   
                                SET IDENTITY_INSERT Venta OFF;";

                    var paramStock = new SqlParameter();
                    paramStock.ParameterName = "Stock";
                    paramStock.SqlDbType = SqlDbType.Int;
                    paramStock.Value = i.Stock;

                    var paramidProducto = new SqlParameter();
                    paramidProducto.ParameterName = "idProducto";
                    paramidProducto.SqlDbType = SqlDbType.BigInt;
                    paramidProducto.Value = i.Id;

                    var parampidVenta = new SqlParameter();
                    parampidVenta.ParameterName = "idVenta";
                    parampidVenta.SqlDbType = SqlDbType.BigInt;
                    parampidVenta.Value = pidVenta;


                    cmd.Parameters.Add(paramStock);
                    cmd.Parameters.Add(paramidProducto);
                    cmd.Parameters.Add(parampidVenta);

                    cmd.ExecuteNonQuery();

                    ProductoRepository.RestarStock(i.Id, i.Stock);
                    

                }

                connection.Close();




            }


        }
    }
}
