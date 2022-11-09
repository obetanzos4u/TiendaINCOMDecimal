using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Descripción breve de ServiceCP
/// </summary>
public class DireccionesServiceCP
{
    public static async Task<json_respuestas> GetCodigoPostalAsync(string CP)
    {
        var client = new RestClient("https://apiweb.incom.mx");
        var request = new RestRequest($"/api/CodigoPostals/cp/{CP}", Method.GET);
        Task<IRestResponse> t = client.ExecuteGetAsync(request);
        t.Wait();
        //var response = await client.ExecuteAsync(request);
        var response = await t;


        switch (response.StatusCode)
        {
            case System.Net.HttpStatusCode.NotFound:
                return new json_respuestas(false, "No se encontró tu código postal, favor de verificar", false, null);

            case System.Net.HttpStatusCode.BadRequest:
                return new json_respuestas(false, "Ocurrio un error, intenta en unos momentos.", true, null);

            case System.Net.HttpStatusCode.OK:
                return new json_respuestas(true, "Código postal encontrado", true, response.Content);

            default:
                return new json_respuestas(false, "Ocurrio un error, intenta en unos momentos.", true, null);

        }

    }

    public static async Task<json_respuestas> GetCodigoPostal(string CP)
    {
        var client = new RestClient("https://apiweb.incom.mx");
        var request = new RestRequest($"/api/CodigoPostals/cp/{CP}", Method.GET);
        var response = await client.ExecuteAsync(request);


        switch (response.StatusCode)
        {

            case System.Net.HttpStatusCode.NotFound:
                return new json_respuestas(false, "No se encontró tu código postal, favor de verificar", false, null);

            case System.Net.HttpStatusCode.BadRequest:
                return new json_respuestas(false, "Ocurrio un error, intenta en unos momentos.", true, null);

            case System.Net.HttpStatusCode.OK:
                return new json_respuestas(true, "Código postal encontrado", true, response.Content);

            default:
                return new json_respuestas(false, "Ocurrio un error, intenta en unos momentos.", true, null);

        }
    }
}