using CoderHouse_SistemaGestion.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace CoderHouse_SistemaGestion.Repository
{
    public class UsuarioRepository
    {
        
        public static Usuario GetUsuario(string pNombreUsuario)
        {
            var usuario = new Usuario();

            SqlConnectionStringBuilder connectionbuilder = new();
            connectionbuilder.DataSource = "MPS001\\SQLEXPRESS";
            connectionbuilder.InitialCatalog = "SistemaGestion";
            connectionbuilder.IntegratedSecurity = true;

            var cs = connectionbuilder.ConnectionString;

            using (SqlConnection conection = new SqlConnection(cs))
            {
                var query = @"SELECT Id,Nombre,Apellido,NombreUsuario,Contraseña,Mail
                               FROM Usuario
                              WHERE NombreUsuario = @nombreUsuario";

                conection.Open();

                using (SqlCommand cm = new SqlCommand(query, conection))
                {
                    cm.Parameters.Add(new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = pNombreUsuario });
                    var reader = cm.ExecuteReader();
                    while (reader.Read())
                    {
                        usuario.Id = Convert.ToInt32(reader.GetValue(0));
                        usuario.Nombre = reader.GetString(1);
                        usuario.Apellido = reader.GetString(2);
                        usuario.NombreUsuario = reader.GetString(3);
                        usuario.Contrasenna = reader.GetString(4);
                        usuario.Mail = reader.GetString(5);
                    }
                }
                conection.Close();
            }
            return usuario;
        }

        public static Usuario InicioSesion(string pNombreUsuario, string pContrasena)
        {
            var usuario = new Usuario();

            SqlConnectionStringBuilder connectionbuilder = new();
            connectionbuilder.DataSource = "MPS001\\SQLEXPRESS";
            connectionbuilder.InitialCatalog = "SistemaGestion";
            connectionbuilder.IntegratedSecurity = true;

            var cs = connectionbuilder.ConnectionString;

            using (SqlConnection conection = new SqlConnection(cs))
            {
                var query = @"SELECT Id,Nombre,Apellido,NombreUsuario,Contraseña,Mail
                               FROM Usuario
                              WHERE NombreUsuario = @nombreUsuario
                                AND Contraseña = @contrasena";

                conection.Open();

                using (SqlCommand cm = new SqlCommand(query, conection))
                {
                    cm.Parameters.Add(new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = pNombreUsuario });
                    cm.Parameters.Add(new SqlParameter("contrasena", SqlDbType.VarChar) { Value = pContrasena });
                    var reader = cm.ExecuteReader();
                    while (reader.Read())
                    {
                        usuario.Id = Convert.ToInt32(reader.GetValue(0));
                        usuario.Nombre = reader.GetString(1);
                        usuario.Apellido = reader.GetString(2);
                        usuario.NombreUsuario = reader.GetString(3);
                        usuario.Contrasenna = reader.GetString(4);
                        usuario.Mail = reader.GetString(5);
                    }
                }
                conection.Close();
            }
            return usuario;
        }

        public static void AgregarUsuario(Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"SET IDENTITY_INSERT Usuario ON;
                                    Insert into Usuario (id, Nombre, Apellido, NombreUsuario, Contraseña, Mail ) 
                                    values (@idUsu, @NombreUsu, @ApellidoUsu, @NombreUsuarioUsu, @ContraseñaUsu, @MailUsu);
                                   
                                   SET IDENTITY_INSERT Usuario OFF;";

                var paramId = new SqlParameter();
                paramId.ParameterName = "idUsu";
                paramId.SqlDbType = SqlDbType.BigInt;
                paramId.Value = usuario.Id;

                var paramNombre = new SqlParameter();
                paramNombre.ParameterName = "NombreUsu";
                paramNombre.SqlDbType = SqlDbType.VarChar;
                paramNombre.Value = usuario.Nombre;

                var paramApellido = new SqlParameter();
                paramApellido.ParameterName = "ApellidoUsu";
                paramApellido.SqlDbType = SqlDbType.VarChar;
                paramApellido.Value = usuario.Apellido;

                var paramNombreUsuario = new SqlParameter();
                paramNombreUsuario.ParameterName = "NombreUsuarioUsu";
                paramNombreUsuario.SqlDbType = SqlDbType.VarChar;
                paramNombreUsuario.Value = usuario.NombreUsuario;


                var paramContraseña = new SqlParameter();
                paramContraseña.ParameterName = "ContraseñaUsu";
                paramContraseña.SqlDbType = SqlDbType.VarChar;
                paramContraseña.Value = usuario.Contrasenna;

                var paramMail = new SqlParameter();
                paramMail.ParameterName = "MailUsu";
                paramMail.SqlDbType = SqlDbType.VarChar;
                paramMail.Value = usuario.Mail;

                cmd.Parameters.Add(paramId);
                cmd.Parameters.Add(paramNombre);
                cmd.Parameters.Add(paramApellido);
                cmd.Parameters.Add(paramNombreUsuario);
                cmd.Parameters.Add(paramContraseña);
                cmd.Parameters.Add(paramMail);

                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void ModificarUsuario(Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE Usuario 
                                           SET Nombre = @NombreUsu
                                               ,Apellido = @ApellidoUsu
                                               ,NombreUsuario = @NombreUsuarioUsu
                                               ,Contraseña = @ContraseñaUsu
                                               ,Mail = @MailUsu
                                         WHERE id = @idUsu; ";

                var paramId = new SqlParameter();
                paramId.ParameterName = "idUsu";
                paramId.SqlDbType = SqlDbType.BigInt;
                paramId.Value = usuario.Id;

                var paramNombre = new SqlParameter();
                paramNombre.ParameterName = "NombreUsu";
                paramNombre.SqlDbType = SqlDbType.VarChar;
                paramNombre.Value = usuario.Nombre;

                var paramApellido = new SqlParameter();
                paramApellido.ParameterName = "ApellidoUsu";
                paramApellido.SqlDbType = SqlDbType.VarChar;
                paramApellido.Value = usuario.Apellido;

                var paramNombreUsuario = new SqlParameter();
                paramNombreUsuario.ParameterName = "NombreUsuarioUsu";
                paramNombreUsuario.SqlDbType = SqlDbType.VarChar;
                paramNombreUsuario.Value = usuario.NombreUsuario;


                var paramContraseña = new SqlParameter();
                paramContraseña.ParameterName = "ContraseñaUsu";
                paramContraseña.SqlDbType = SqlDbType.VarChar;
                paramContraseña.Value = usuario.Contrasenna;

                var paramMail = new SqlParameter();
                paramMail.ParameterName = "MailUsu";
                paramMail.SqlDbType = SqlDbType.VarChar;
                paramMail.Value = usuario.Mail;

                cmd.Parameters.Add(paramId);
                cmd.Parameters.Add(paramNombre);
                cmd.Parameters.Add(paramApellido);
                cmd.Parameters.Add(paramNombreUsuario);
                cmd.Parameters.Add(paramContraseña);
                cmd.Parameters.Add(paramMail);


                cmd.ExecuteNonQuery();
                connection.Close();


            }

        }

        public static void EliminarUsuario(int IdUsuario)
        {
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {

                VentaRepository.EliminarVentaPorUsuario(IdUsuario);
                ProductoRepository.EliminarProductoPorUsuario(IdUsuario);


                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"DELETE Usuario WHERE id = @IdUsuario; ";

                var paramIdUsuario = new SqlParameter();
                paramIdUsuario.ParameterName = "IdUsuario";
                paramIdUsuario.SqlDbType = SqlDbType.BigInt;
                paramIdUsuario.Value = IdUsuario;

                cmd.Parameters.Add(paramIdUsuario);


                cmd.ExecuteNonQuery();
                connection.Close();

            }
        }

    }
}
