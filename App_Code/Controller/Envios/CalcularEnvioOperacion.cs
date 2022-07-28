 using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

/// <summary>
/// Descripción breve de CalcularEnvioOperacion
/// </summary>
public class CalcularEnvioOperacion : ICalcularEnvio
{
    public string host   { get; set; }
private string urlAPI  = "https://apiweb.incom.mx";
    private string urlAPI_test = "https://localhost:44393";
    public string Tipo { get; set; }
    public string Numero_Operacion { get; set; }

    public decimal MontoDeclarado { get; set; }
    public List<object> Productos { get; set; }
    public dynamic DireccionEnvio { get; set; }

    public decimal? CostoEnvio { get; set; }

    /// <summary>
    /// Descripción breve de CalcularEnvioOperacion
    /// </summary>
    public bool IsValidCalculo { get; set; }

    /// <summary>
    /// Mensaje del cálculo
    /// </summary>
    public string MessageCalculo { get; set; }

    public CalcularEnvioOperacion(string _Tipo, string _Numero_Operacion, dynamic _DireccionEnvio)
    {
        if(_DireccionEnvio == null)
        {
            IsValidCalculo = false; MessageCalculo = "No se ha establecido una dirección de envío."; throw new Exception(MessageCalculo);
        }
        if (string.IsNullOrWhiteSpace(_Numero_Operacion) && _Tipo != "consulta")
        {
            IsValidCalculo = false; MessageCalculo = $"No se ha recibido un número de operación válido. No. Operación: ${_Numero_Operacion}"; throw new Exception(MessageCalculo);
        }
        Tipo = _Tipo;
        Numero_Operacion = _Numero_Operacion;
        DireccionEnvio = _DireccionEnvio;
        DireccionEnvio = new
        {
            Calle = _DireccionEnvio.calle,
            Numero = _DireccionEnvio.numero,
            CodigoPostal = _DireccionEnvio.codigo_postal,
            Colonia_Asentamiento = _DireccionEnvio.colonia,
            Municipio = _DireccionEnvio.delegacion_municipio,
            Estado = _DireccionEnvio.estado,
            Ciudad = _DireccionEnvio.ciudad,
            Pais = _DireccionEnvio.pais,
            Referencia = _DireccionEnvio.referencias,

        };
        Productos = new List<dynamic>();
        ValidarDireccion();
        ValidarTipo();


    }


    public void AgregarProducto(string Numero_Parte, decimal? PesoKg, decimal? Largo,
        decimal? Ancho, decimal? Alto, decimal? Cantidad, bool? RotacionVertical, bool? RotacionHorizontal)
    {

        IsValidCalculo = true;

        if (string.IsNullOrEmpty(Numero_Parte)) { IsValidCalculo = false; MessageCalculo = "Un producto no se ha establecido número de parte"; }

        if (PesoKg == null) { IsValidCalculo  = false; MessageCalculo = "Un producto no se ha establecido el peso: " + Numero_Parte; throw new Exception(MessageCalculo); }
        if (Largo == null) { IsValidCalculo  = false; MessageCalculo = "Un producto no se ha establecido el largo: " + Numero_Parte; throw new Exception(MessageCalculo); }
        if (Ancho == null) { IsValidCalculo = false; MessageCalculo = "Un producto no se ha establecido el ancho: " + Numero_Parte; throw new Exception(MessageCalculo); }
        if (Alto == null) { IsValidCalculo  = false; MessageCalculo = "Un producto no se ha establecido el alto: " + Numero_Parte; throw new Exception(MessageCalculo); }

        if (RotacionVertical == null) {MessageCalculo = "Un producto no se ha establecido RotacionVertical: " + Numero_Parte; throw new Exception(MessageCalculo); }
        if (RotacionHorizontal == null){ MessageCalculo = "Un producto no se ha establecido RotacionHorizontal: " + Numero_Parte; throw new Exception(MessageCalculo); }

        if (Cantidad < 1) { IsValidCalculo = false;  MessageCalculo = "La cantidad no puede ser negativa"; throw new Exception(MessageCalculo); }

   
        try
        {
            // Importante, los productos de la página tienen medidas en centimetros, es necesario convertir a metros
            var producto = new
            {
                ProductoID = Productos.Count + 1,
                Numero_Parte = Numero_Parte,
                PesoKg =  PesoKg,
                Largo = Largo * 0.01m,
                Ancho = Ancho * 0.01m,
                Alto = Alto * 0.01m,
                Cantidad = Cantidad,
                RotacionVertical = RotacionVertical,
                RotacionHorizontal = RotacionHorizontal
            };

            Productos.Add(producto);
            IsValidCalculo = true;
        }
        catch (Exception ex)
        {

            MessageCalculo = $"El producto {Numero_Parte} no tiene una dimensión en el formato correcto";
            throw new Exception(MessageCalculo, ex);
        }
     
    }
    public void ValidarDireccion() {

        if (string.IsNullOrEmpty(DireccionEnvio.CodigoPostal)) {
            IsValidCalculo = false; MessageCalculo = "El código postal no puede estar vacio"; throw new Exception(MessageCalculo);
        }

        if (string.IsNullOrEmpty(DireccionEnvio.Municipio))
        {
            IsValidCalculo = false; MessageCalculo = "El municipio no puede estar vacio"; throw new Exception(MessageCalculo);
        }

    }
    public void ValidarTipo() { }

    public async System.Threading.Tasks.Task<json_respuestas> CalcularEnvio() {

        json_respuestas res = await DireccionesServiceCP.GetCodigoPostalAsync(DireccionEnvio.CodigoPostal);
        if (res.result == false) {
            MessageCalculo = "El código postal establecido parece no estar correcto.";
            IsValidCalculo = false;
            return new json_respuestas(false,MessageCalculo,false);
        }


        var message = new
        {
            Direccion = DireccionEnvio,
            Productos = Productos,
            TipoOperacion = Tipo,
            NumeroOperacion = Numero_Operacion,
            MontoDeclarado = MontoDeclarado
        };

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();


        string str_message = serializer.Serialize(message);

        try
        {
            using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders
                .Accept
          .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

            string BaseAddress = "";

           
            if (host == "localhost") BaseAddress = urlAPI_test; else BaseAddress = urlAPI;


            var stringContent = new System.Net.Http.StringContent(str_message
                   , System.Text.Encoding.UTF8,
                                    "application/json");

            client.BaseAddress = new Uri(BaseAddress);

            var responseTask = await client.PostAsync("api/CalcularFlete/", stringContent);
 


            if (responseTask.IsSuccessStatusCode)
            {
               
                var response = await responseTask.Content.ReadAsStringAsync();
           
            
                    string str_Result = response.Trim('"').Replace("\\", "");
               

                    var Respuesta = Newtonsoft.Json.Linq.JObject.Parse(str_Result);

                var precio = Newtonsoft.Json.Linq.JObject.Parse(Respuesta["response"].ToString());


               CostoEnvio = decimal.Parse(precio["Precio"].ToString());

                IsValidCalculo = true;
                MessageCalculo = "Cálculo realizado con éxito";
               
            }
            else///web api sent error response 
            {


            }
            }

            return new json_respuestas(true, MessageCalculo,false);
        }
        catch (Exception ex)
        {
            IsValidCalculo = false;
            MessageCalculo = " Un asesor se contactará para revisar los detalles del envío.";//Error al consultar/leer respuesta API envios:
            //devNotificaciones.ErrorSQL(MessageCalculo, ex, ex.ToString());

            return new json_respuestas(false, ex.ToString(), true,ex);
        }
    }
}