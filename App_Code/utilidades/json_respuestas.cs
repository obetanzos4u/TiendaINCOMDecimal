using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Clase modelo para generar respuestas json
/// </summary>
/// 
public static class json_respuestaExtension  {

    public static string ToJson(this json_respuestas json) {

        return JsonConvert.SerializeObject(json);

    }


}

public class json_respuestas  {
    /// <summary>
    /// Resultado de la operación
    /// </summary>
    public bool  result { get; set; }
    /// <summary>
    /// Mensaje de la operacion
    /// </summary>
    public string message { get; set; }
    /// <summary>
    /// Clase modelo para generar respuestas json
    /// </summary>
    /// 
    /// <summary>
    /// Si ocurre un error al procesar la solicitud independiente de esta
    /// </summary>
    public bool exception { get; set; }
   
    public dynamic response { get; set; }
    public json_respuestas()
    {
     
    }
    public json_respuestas(bool _result, string _message)
    {
        result = _result;
        message = _message;
    }

    public json_respuestas(bool _result, string _message, bool _exception)
    {
        result = _result;
        message = _message;
        exception = _exception;
    }
    public json_respuestas(bool _result, string _message, bool _exception, dynamic _response)
    {
        result = _result;
        message = _message;
        exception = _exception;
        response = _response;

        if (exception)
        {
            guardarLogError(message, _response);
        }
    }

    static public async Task  guardarLogError(string messaje, dynamic exception)
    {
        var type =  (exception.GetType() as Type).Name.ToLower();
        

        if (type.Contains("exception"))
        {
              devNotificaciones.ErrorSQL(messaje, exception, null);
        }

         
    }
}