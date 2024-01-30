using PracticaCore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaCore.Repositories
{
    public class RepositoryClientesPedidos
    {
        private SqlConnection cn;
        private SqlCommand com;
        private SqlDataReader reader;

        public RepositoryClientesPedidos()
        {
            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=NETCORE_EXAMEN;Persist Security Info=True;User ID=SA;Password=MCSD2023";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }
        public List<string> GetClientes()
        {
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_CLIENTES";
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            List<string> clientes = new List<string>();
            while (this.reader.Read())
            {
                clientes.Add(this.reader["EMPRESA"].ToString());
            }
            this.reader.Close();
            this.cn.Close();
            return clientes;
        }

        public Empresa GetDatosCliente(string nombreCliente)
        {
            this.com.CommandType= CommandType.StoredProcedure;
            this.com.CommandText = "SP_DATOS_CLIENTE";

            SqlParameter pamEmpresa = new SqlParameter("@EMPRESA", nombreCliente);
            this.com.Parameters.Add(pamEmpresa);
            this.cn.Open();
            this.reader =this.com.ExecuteReader();
            this.reader.Read();

            Empresa cliente = new Empresa();
            cliente.CodigoCliente = this.reader["CodigoCliente"].ToString();
            cliente.empresa = this.reader["Empresa"].ToString();
            cliente.Contacto = this.reader["Contacto"].ToString();
            cliente.Cargo = this.reader["Cargo"].ToString();
            cliente.Ciudad = this.reader["Ciudad"].ToString();
            cliente.telefono = int.Parse(this.reader["Telefono"].ToString());

            this.reader.Close();
            this.cn.Close();
            this.com.Parameters.Clear();

            return cliente;
            
        }

        public List<string> GetPedidosCliente(string nomCliente)
        {
            SqlParameter paramCliente = new SqlParameter("@NOM_CLIENTE", nomCliente);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_PEDIDOS";
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            
            List<string> pedidos = new List<string>();
            while (this.reader.Read())
            {
                pedidos.Add(this.reader["codigoPedidos"].ToString());

            }
            this.reader.Close();
            this.cn.Close();
            this.com.Parameters.Clear();
            return pedidos;

        }

        public int CreatePedido(Pedido pedido)
        {
            SqlParameter paramCodPedido = new SqlParameter("@COD_PEDIDO", pedido.CodigoPedido);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_NUEVO_PEDIDO";
            this.cn.Open();
            this.reader = this.com.ExecuteReader();

        }
    }

}
