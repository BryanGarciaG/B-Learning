using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace BLearning.Models.Datos
{
    public class Conexion
    {
        public SqlConnection  conexionServidor { get; set; }
        public SqlCommand comando { get; set; }
        public Conexion()
        {
            conexionServidor = new SqlConnection();
           comando = new SqlCommand();
        }

        public SqlConnection incializadorConexion()
        {
            //conexionServidor.ConnectionString = "Data Source=localhost; Integrated Security=SSPI;Initial Catalog=BLended";
            conexionServidor.ConnectionString = "Data Source=192.168.100.2; Integrated Security=false;Initial Catalog=BLended; user=Blended; password=b!ended2017";
            return conexionServidor;
        }

        public SqlCommand crearComandoProcedi(string procedimiento)
        {
            comando = new SqlCommand(procedimiento, incializadorConexion());
            comando.CommandType = CommandType.StoredProcedure;
            return comando;
        }

        public int ejecutarComando(SqlCommand varComand)
        {
            try
            {
                conexionServidor.Open();
                return varComand.ExecuteNonQuery(); ;
            }
            catch
            {
                throw;               
            }
            finally { conexionServidor.Close(); }
        }

        public DataTable ejecutarComandoConsultar(SqlCommand varComand)
            {
            SqlDataAdapter adapatador = new SqlDataAdapter();
            DataTable tabla = new DataTable();

            try
            {
                conexionServidor.Open();
                adapatador.SelectCommand = comando;
                adapatador.Fill(tabla);
                return tabla;
            }
            catch
            {
                throw;
            }
            finally
            {
                conexionServidor.Close();
            }
        }
        
        
    }
}