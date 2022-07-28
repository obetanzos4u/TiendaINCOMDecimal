<%@ WebHandler Language="C#" Class="usuarios_login" %>

using System;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;

public class usuarios_login : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {


        try
        {

            

            bool login = HttpContext.Current.User.Identity.IsAuthenticated;

                json_respuestas respuesta = new json_respuestas(login, "Login estado",false);
                 HttpContext.Current.Response.Write(JsonConvert.SerializeObject(respuesta));

        }
        catch (Exception ex)
        {

            json_respuestas respuesta = new json_respuestas(false, "Error al iniciar sesión",true);

            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(respuesta));
        }

    }



    public bool IsReusable {
        get {
            return false;
        }
    }

}