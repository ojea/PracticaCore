using PracticaCore.Models;
using PracticaCore.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

#region
//CREATE PROCEDURE SP_CLIENTES
//AS
//	SELECT * FROM CLIENTES
//GO

//CREATE PROCEDURE SP_DATOS_CLIENTE
//(@EMPRESA NVARCHAR(50))
//as
//    select* from clientes where Empresa = @EMPRESA
//go

//CREATE PROCEDURE SP_PEDIDOS
//(@NOM_CLIENTE NVARCHAR(100))
//AS
//    DECLARE @COD_CLIENTE NVARCHAR(100)
//	SELECT @COD_CLIENTE = CodigoCliente from clientes
//	WHERE Empresa = @NOM_CLIENTE
//	SELECT CodigoPedido from pedidos
//	WHERE CodigoCliente = @COD_CLIENTE

//GO

//CREATE PROCEDURE SP_NUEVO_PEDIDO 
//(@COD_PEDIDO NVARCHAR(50), @COD_CLIENTE NVARCHAR(50),
//@FECHA_ENTREGA DATETIME, @FORMA_ENVIO NVARCHAR(50),
//@IMPORTE INT)

//AS
//    INSERT INTO pedidos
//	VALUES(@COD_PEDIDO, @COD_CLIENTE, @FECHA_ENTREGA, @FORMA_ENVIO, @IMPORTE)
//GO

//CREATE PROCEDURE AP_DELETE
//(@COD_PEDIDO NVARCHAR(50))
//AS
//DELETE FROM pedidos
//WHERE CodigoPedido = @COD_PEDIDO
//GO
#endregion

namespace PracticaCore
{

    public partial class FormPractica : Form
    {
        RepositoryClientesPedidos repo;
        public FormPractica()
        {
            InitializeComponent();
            this.repo = new RepositoryClientesPedidos();
            this.loadClientes();
        }

        private void loadClientes()
        {
            List<string> clientes = this.repo.GetClientes();
            foreach (string data in clientes)
            {
                this.cmbclientes.Items.Add(data);
            }
        }

        private void cmbclientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nombre = this.cmbclientes.SelectedItem.ToString();
            Empresa cliente = this.repo.GetDatosCliente(nombre);

            this.txtempresa.Text = cliente.empresa;
            this.txtcontacto.Text = cliente.Contacto;
            this.txtcargo.Text = cliente.Cargo;
            this.txtciudad.Text = cliente.Ciudad;
            this.txttelefono.Text = cliente.telefono.ToString();
        }

        private void lstpedidos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lstpedidos.SelectedIndex != 1)
            {
                string pedido = this.cmbclientes.SelectedItem.ToString();
                Empresa datosCliente = this.repo.GetDatosCliente(nombreCliente);
            }
        }

        private void btnnuevopedido_Click(object sender, EventArgs e)
        {

        }
    }
}
