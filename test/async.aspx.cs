using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.Remoting.Messaging;

public partial class inicio : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Title = "Página de pruebas";
        }
      if(usuarios.userLogin().rango  != 3)
        {
            Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority));
        }

    }

    protected async void btn_SendAsyn_Click(object sender, EventArgs e)
    {

        HttpContext ctx = HttpContext.Current;
        await Task.Run(() => { HttpContext.Current = ctx;
            devNotificaciones.notificacionSimple("Test Asyn");
        }
        ).ConfigureAwait(false); 
    }

 
    protected async void btn_GetStockRestSAP_Click(object sender, EventArgs e)
    {
        string numero_parte = textTools.lineSimple(txt_data.Text);
      
 
        var resultConvert = await ProductosStockFromSapRest.ConvertirUnidadProductoAsync(numero_parte);

        string result = resultConvert.message;

        if (resultConvert.result)
        {
            var resultStock = await ProductosStockFromSapRest.ConsultarStockV2SAPAsync();

            result = resultStock.message;
        }
        


        txt_result.Text =  result;
    }

    protected async void CrearPedidoEnSapPruebas_Click(object sender, EventArgs e)
    {

        var ListPromo = new List<PromocionesProductoModel>();
        ListPromo.Add(new PromocionesProductoModel() {
            numero_parte = "2170", 
            PromoCode= "APOLO10",
            FechaActualización = utilidad_fechas.obtenerCentral() 
        });

        var PedidoSAP = new SendOrderPromoToSAP(txt_data.Text, ListPromo);
        var result =   await PedidoSAP.Send();

        if (result.result)
        {

            txt_result.Text = result.message;
        }
    }

    protected void btn_ObtenerFecha_Click(object sender, EventArgs e)
    {
        var Fecha = utilidad_fechas.obtenerCentral();
    }
}