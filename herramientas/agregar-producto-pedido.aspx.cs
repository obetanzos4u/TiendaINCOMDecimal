using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class herramientas_agregar_producto_pedido : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

        if (!IsPostBack) {
           
        }
    }






    protected async void btn_añadir_Click(object sender, EventArgs e) {

        string tipo_operacion = "pedido";
        string numero_operacion = txt_numero_operacion.Text;
       

        pedidosDatos pedido = new pedidosDatos();
        DataTable dt_pedido = pedido.obtenerPedidoDatos(numero_operacion);

        string moneda_pedido = dt_pedido.Rows[0]["monedaPedido"].ToString();

        operacionesProductos agregar = new operacionesProductos(tipo_operacion, "", numero_operacion, txt_numero_parte.Text, txt_cantidad.Text, moneda_pedido);
        await agregar.agregarProductoAsync();

        NotiflixJS.Message(this.Page, NotiflixJS.MessageType.info, "Resultado");
        materializeCSS.crear_toast(this.Page, agregar.mensaje_ResultadoOperacion, agregar.resultado_operacion);
    }
}