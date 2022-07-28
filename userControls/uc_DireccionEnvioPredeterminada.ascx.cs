using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
public partial class userControls_uc_DireccionEnvioPredeterminada : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                direcciones_envio direccionesEnvio = new direcciones_envio();
                int idUsuario = usuarios.modoAsesor().id;
                using (var db = new tiendaEntities())
                {
                    direccionesEnvio = db.direcciones_envio
                      .Where(s => s.id_cliente == idUsuario && s.direccion_predeterminada == true)
                      .FirstOrDefault();
                }

                if (direccionesEnvio != null)
                    Info.Text = direccionesEnvio.nombre_direccion + " (" + direccionesEnvio.calle + " " + direccionesEnvio.codigo_postal + ")";
                else Info.Text = "No establecido.";
            }
            else
            {
                Info.Text = "Sesión no iniciada.";
            }
           
            
        }
    }
}