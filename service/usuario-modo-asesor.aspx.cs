using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class usuarios_login : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack) obtener();

    }

    public void obtener()
    {

        try
        {

            usuarios Usuario = usuarios.modoAsesor();

            json_respuestas respuesta = new json_respuestas();
            respuesta.exception = false;
            respuesta.message = "Datos de usuario obtenidos con éxito";
            respuesta.result = true;
            respuesta.response = Usuario;

            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(respuesta));

            


        }
        catch (Exception ex)
        {

            json_respuestas respuesta = new json_respuestas();
            respuesta.exception = true;
            respuesta.message = "Ocurrio un error al obtener los datos del usuario: "+ex.Message;
            respuesta.result = false;

            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(respuesta));
        }

    }

  
}