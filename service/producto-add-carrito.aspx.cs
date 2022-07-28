using Newtonsoft.Json;
using System;
using System.Web;


public partial class service_productos_stock : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        obtener();

    }

    /// <summary>
    /// Devuelve [JSON] con la disponibilidad del producto
    /// </summary>
    public async void obtener()
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {


            if (Request.QueryString["numero_parte"] != null && Request.QueryString["numero_parte"].Length >= 3)
            {
                if(Request.QueryString["cantidad"] == null)
                {
                    json_respuestas respuesta = new json_respuestas(false, "Ingresa una cantidad solicitada");

                    Response.Write(JsonConvert.SerializeObject(respuesta));

                    
                }
               
                try
                {
                    string numero_parte = Request.QueryString["numero_parte"];
                    string str_cantidad = Request.QueryString["cantidad"];

              

                    string monedaTienda  = HttpContext.Current.Session["monedaTienda"].ToString();

                    decimal tipoCambio = operacionesConfiguraciones.obtenerTipoDeCambio();



                    operacionesProductos agregar = new operacionesProductos("carrito", null, null, numero_parte, str_cantidad, monedaTienda);

                  await   agregar.agregarProductoAsync();

                    //string resultado = agregar.resultado_operacion.ToString().ToLower();

                    json_respuestas respuesta = new json_respuestas(agregar.resultado_operacion, agregar.mensaje_ResultadoOperacion);

                   Response.Write(JsonConvert.SerializeObject(respuesta));




                }
                catch (Exception ex)
                {

                    devNotificaciones.error("Añadir a carrito WebService", ex);

                    json_respuestas respuesta = new json_respuestas(false, "Ocurrio un error al agregar el producto.");
                    HttpContext.Current.Response.Write(JsonConvert.SerializeObject(respuesta));
                }

            }
            else
            {
                Response.Write("null");
            }
        }
    }



    
        
    
}