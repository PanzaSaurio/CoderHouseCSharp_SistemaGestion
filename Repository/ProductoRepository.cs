using CoderHouse_SistemaGestion.Models;
using System.Data.SqlClient;
using System.Data;

namespace CoderHouse_SistemaGestion.Repository
{
    public class ProductoRepository
    {
        public static List<Producto> TraerProductos(int pIdUsuario)
        {

            var listaProductos = new List<Producto>();


            SqlConnectionStringBuilder connectionbuilder = new();
            connectionbuilder.DataSource = "MPS001\\SQLEXPRESS";
            connectionbuilder.InitialCatalog = "SistemaGestion";
            connectionbuilder.IntegratedSecurity = true;

            var cs = connectionbuilder.ConnectionString;

            using (SqlConnection conection = new SqlConnection(cs))
            {
                var query = @"SELECT Id
                                     ,Descripciones
                                     ,Costo
                                     ,PrecioVenta
                                     ,Stock
                                     ,IdUsuario
                                FROM Producto
                               WHERE IdUsuario = @IdUsuario";

                conection.Open();

                using (SqlCommand cm = new SqlCommand(query, conection))
                {
                    cm.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.VarChar) { Value = pIdUsuario });
                    var reader = cm.ExecuteReader();
                    while (reader.Read())
                    {
                        var producto = new Producto();
                        producto.Id = Convert.ToInt32(reader.GetValue(0));
                        producto.Descripciones = reader.GetString(1);
                        producto.Costo = Convert.ToSingle(reader.GetValue(2));
                        producto.PrecioVenta = Convert.ToSingle(reader.GetValue(3));
                        producto.Stock = Convert.ToInt32(reader.GetValue(4));
                        producto.IdUsuario = Convert.ToInt32(reader.GetValue(5));
                        listaProductos.Add(producto);
                    }
                }
                conection.Close();
            }
            return listaProductos;
        }

        public static Producto TraerProducto(int pIdProducto)
        {
            var producto = new Producto();

            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"SELECT Id
                                     ,Descripciones
                                     ,Costo
                                     ,PrecioVenta
                                     ,Stock
                                     ,IdUsuario
                                FROM Producto
                               WHERE id = @IdProducto";

                connection.Open();

                var paramIdProducto = new SqlParameter();
                paramIdProducto.ParameterName = "IdProducto";
                paramIdProducto.SqlDbType = SqlDbType.BigInt;
                paramIdProducto.Value = pIdProducto;

                cmd.Parameters.Add(paramIdProducto);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    producto.Id = Convert.ToInt32(reader.GetValue(0));
                    producto.Descripciones = reader.GetString(1);
                    producto.Costo = Convert.ToSingle(reader.GetValue(2));
                    producto.PrecioVenta = Convert.ToSingle(reader.GetValue(3));
                    producto.Stock = Convert.ToInt32(reader.GetValue(4));
                    producto.IdUsuario = Convert.ToInt32(reader.GetValue(5));
                }
            }
            return producto;
        }


        public static void AgregarProducto(Producto producto)
        {
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"SET IDENTITY_INSERT Producto ON;
                                    Insert into Producto (id, descripciones, costo, precioVenta, stock, idUsuario ) 
                                    values (@id, @Descripciones, @Costo, @PrecioVenta, @Stock, @idUsuario);                                   
                                   SET IDENTITY_INSERT Producto OFF;";

                var paramId = new SqlParameter();
                paramId.ParameterName = "id";
                paramId.SqlDbType = SqlDbType.BigInt;
                paramId.Value = producto.Id;

                var paramDescripciones = new SqlParameter();
                paramDescripciones.ParameterName = "Descripciones";
                paramDescripciones.SqlDbType = SqlDbType.VarChar;
                paramDescripciones.Value = producto.Descripciones;

                var paramCosto = new SqlParameter();
                paramCosto.ParameterName = "Costo";
                paramCosto.SqlDbType = SqlDbType.Money;
                paramCosto.Value = producto.Costo;

                var paramNombrePrecioVenta = new SqlParameter();
                paramNombrePrecioVenta.ParameterName = "PrecioVenta";
                paramNombrePrecioVenta.SqlDbType = SqlDbType.Money;
                paramNombrePrecioVenta.Value = producto.PrecioVenta;


                var paramStock = new SqlParameter();
                paramStock.ParameterName = "Stock";
                paramStock.SqlDbType = SqlDbType.Int;
                paramStock.Value = producto.Stock;

                var paramIdUsuario = new SqlParameter();
                paramIdUsuario.ParameterName = "IdUsuario";
                paramIdUsuario.SqlDbType = SqlDbType.BigInt;
                paramIdUsuario.Value = producto.IdUsuario;

                cmd.Parameters.Add(paramId);
                cmd.Parameters.Add(paramDescripciones);
                cmd.Parameters.Add(paramCosto);
                cmd.Parameters.Add(paramNombrePrecioVenta);
                cmd.Parameters.Add(paramStock);
                cmd.Parameters.Add(paramIdUsuario);

                cmd.ExecuteNonQuery();
                connection.Close();

            }
        }

        public static void ModificarProducto(Producto producto)
        {
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE Producto 
                                       SET Descripciones = @Descripciones
                                           , Costo = @Costo
                                           , PrecioVenta = @PrecioVenta
                                           , Stock = @Stock
                                           , idUsuario = @idUsuario
                                     WHERE id = @id; ";

                var paramId = new SqlParameter();
                paramId.ParameterName = "id";
                paramId.SqlDbType = SqlDbType.BigInt;
                paramId.Value = producto.Id;

                var paramDescripciones = new SqlParameter();
                paramDescripciones.ParameterName = "Descripciones";
                paramDescripciones.SqlDbType = SqlDbType.VarChar;
                paramDescripciones.Value = producto.Descripciones;

                var paramCosto = new SqlParameter();
                paramCosto.ParameterName = "Costo";
                paramCosto.SqlDbType = SqlDbType.Money;
                paramCosto.Value = producto.Costo;

                var paramNombrePrecioVenta = new SqlParameter();
                paramNombrePrecioVenta.ParameterName = "PrecioVenta";
                paramNombrePrecioVenta.SqlDbType = SqlDbType.Money;
                paramNombrePrecioVenta.Value = producto.PrecioVenta;


                var paramStock = new SqlParameter();
                paramStock.ParameterName = "Stock";
                paramStock.SqlDbType = SqlDbType.Int;
                paramStock.Value = producto.Stock;

                var paramIdUsuario = new SqlParameter();
                paramIdUsuario.ParameterName = "IdUsuario";
                paramIdUsuario.SqlDbType = SqlDbType.BigInt;
                paramIdUsuario.Value = producto.IdUsuario;

                cmd.Parameters.Add(paramId);
                cmd.Parameters.Add(paramDescripciones);
                cmd.Parameters.Add(paramCosto);
                cmd.Parameters.Add(paramNombrePrecioVenta);
                cmd.Parameters.Add(paramStock);
                cmd.Parameters.Add(paramIdUsuario);

                cmd.ExecuteNonQuery();
                connection.Close();

            }
        }

        public static void EliminarProducto(int idProducto)
        {
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {

                ProductoVendidoRepository.EliminarProductoVendido(idProducto);

                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"DELETE Producto WHERE id = @idProducto; ";

                var paramidProducto = new SqlParameter();
                paramidProducto.ParameterName = "idProducto";
                paramidProducto.SqlDbType = SqlDbType.BigInt;
                paramidProducto.Value = idProducto;

                cmd.Parameters.Add(paramidProducto);


                cmd.ExecuteNonQuery();
                connection.Close();

            }
        }


        public static void RestarStock(int pIdProducto,int pStock)
        {
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                Producto producto = new Producto();

                producto = TraerProducto(pIdProducto);

                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE Producto 
                                       SET Stock = @Stock
                                     WHERE id = @id; ";

                var paramId = new SqlParameter();
                paramId.ParameterName = "id";
                paramId.SqlDbType = SqlDbType.BigInt;
                paramId.Value = pIdProducto;

                var paramStock = new SqlParameter();
                paramStock.ParameterName = "Stock";
                paramStock.SqlDbType = SqlDbType.Int;
                paramStock.Value = producto.Stock - pStock;


                cmd.Parameters.Add(paramId);
                cmd.Parameters.Add(paramStock);

                cmd.ExecuteNonQuery();
                connection.Close();

            }
        }



    }
}
