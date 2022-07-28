using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

public class DireccionesEnvioController : ApiController
{
    // GET api/<controller/>
    [System.Web.Http.HttpGet]
 
 
   
     [Authorize]
    public json_respuestas Get(int id)
    {
        json_respuestas response = new json_respuestas();

        try {
         
        var   direccionesEnvio = direcciones_envio_EF.ObtenerTodas(id);
          

            response.exception = false;
            response.message = "Dirección de envío obtenidas con éxito";
            response.response = direccionesEnvio;
            response.result = true;

            return response;
        }
        catch(Exception ex)
        {
            response.exception = true;
            response.message = "Error al obtener las direcciones de envío.";
            response.response = ex;
            response.result = false;

            return response;
        }
        
    }

 

    // POST api/<controller>
      [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<controller>/5
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<controller>/5
    public void Delete(int id)
    {
    }
}
