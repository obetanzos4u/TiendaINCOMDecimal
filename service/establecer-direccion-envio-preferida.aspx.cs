using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;


public partial class service_establecer_direccion_envio_preferida : System.Web.UI.Page
{

    static string URLTest = "https://localhost:44393/";
    static  string URLProductivo = "https://apiweb.incom.mx";
    protected void Page_Load(object sender, EventArgs e)
    {
        establecer();

    }

    public void establecer()
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {


            if (Request.QueryString["idDireccion"] != null)
            {


                try
                {
                    int id_cliente = usuarios.modoAsesor().id;
                    int idDireccionEnvio = int.Parse(Request.QueryString["idDireccion"]);


                    direccionesEnvio DireccionEnvio = direccionesEnvio.obtenerDireccionStatic(idDireccionEnvio);
                    if (DireccionEnvio != null)
                    {

                        direccionesEnvio.EstablerDirecciónEnvioPredeterminada(id_cliente, idDireccionEnvio);


                        json_respuestas respuesta = new json_respuestas(true, "Dirección de envío establecida con éxito", false);

                        var t = System.Threading.Tasks.Task.Run(() => PreCalcular(DireccionEnvio));
                        HttpContext.Current.Response.Write(JsonConvert.SerializeObject(respuesta));
                    }


                }
                catch (Exception ex)
                {



                    json_respuestas respuesta = new json_respuestas(false, "Ocurrio un error al actualizar la dirección de envío", true);
                    HttpContext.Current.Response.Write(JsonConvert.SerializeObject(respuesta));
                }

            }
            else
            {
                Response.Write("null");
            }
        }
    }



    static void PreCalcular(direccionesEnvio DireccionEnvio)
    {

        json_respuestas response = null;

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders
        .Accept
          .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header


            string BaseAddress = "";

            string host = HttpContext.Current.Request.Url.Host;
            BaseAddress = host == "localhost" ? URLTest : URLProductivo;

            var stringContent = new StringContent("{\"CodigoPostal\" : \"" + DireccionEnvio.codigo_postal + "\", \"Colonia\": \"" + DireccionEnvio.colonia + "\"}"
                   , System.Text.Encoding.UTF8,
                                    "application/json");

            client.BaseAddress = new Uri(BaseAddress);

            var responseTask = client.PostAsync("api/CalcularUbicacion/", stringContent);
             responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<json_respuestas>();
                readTask.Wait();


                //  obtenerDistanca(origen, destino);

            }
            else///web api sent error response 
            {


            }
        }


    }
}