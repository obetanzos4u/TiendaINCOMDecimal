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



    protected async void btn_actualizar_stock_Click(object sender, EventArgs e) {

        //var result = await SAP_productos_oData.ActualizarStockAsync();

       // materializeCSS.crear_toast(this.Page, result.message, result.result);
    }
}