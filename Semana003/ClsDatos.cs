using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semana003
{
    class ClsDatos
    {
        public SqlConnection LeerCadena()
        {
            SqlConnection connection =
                new SqlConnection("Data Source=sql5047.site4now.net" +
                "Initial Catalog=DB_A5A76D_leonidBD;User ID=DB_A5A76D_leonidBD_admin;Password=Pertolo123.;");
            return connection;
        }
        public DataTable ListaPedidosFechas(DateTime x, DateTime y)
        {
            SqlConnection connection = LeerCadena();
            connection.Open();
            SqlDataAdapter sqlData = new SqlDataAdapter("USP_ListarPedidosEntreFechas", connection);
            sqlData.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlData.SelectCommand.Parameters.AddWithValue("@FEC1", x);
            sqlData.SelectCommand.Parameters.AddWithValue("@FEC2", y);
            DataTable dataTable = new DataTable();
            sqlData.Fill(dataTable);
            connection.Close();
            return dataTable;
        }

        public DataTable ListarDetalle(int x)
        {
            SqlConnection connection = LeerCadena();
            connection.Open();
            SqlDataAdapter sqlData = new SqlDataAdapter("USP_ListarDetalle", connection);
            sqlData.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlData.SelectCommand.Parameters.AddWithValue("@IdPedido", x);
            DataTable dataTable = new DataTable();
            sqlData.Fill(dataTable);
            connection.Close();
            return dataTable;
        }

        public double PedidoTotal(Int32 idpedido)
        {
            SqlConnection connection = LeerCadena();
            connection.Open();
            SqlDataAdapter sqlData = new SqlDataAdapter("USP_PedidoTotal", connection);
            sqlData.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlData.SelectCommand.Parameters.AddWithValue("@IdPedido", idpedido);
            sqlData.SelectCommand.Parameters.Add(
                "Total", SqlDbType.Money).Direction =
                ParameterDirection.Output;
            sqlData.SelectCommand.ExecuteScalar();
            Int32 total = Convert.ToInt32(
                sqlData.SelectCommand.Parameters["@Total"].Value);
            connection.Close();
            return total;
        }



    }
}
