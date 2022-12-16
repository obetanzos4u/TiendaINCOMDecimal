using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class userControls_operaciones_PedidosUsuarioSeguimiento : System.Web.UI.UserControl
{
    public string numero_operacion
    {
        get { return hf_numero_operacion.Value; }   // get method
        set { hf_numero_operacion.Value = value; }  // set method
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarAsesores();
            ObtenerAsignacion();
        }
    }
    async protected void ObtenerAsignacion()
    {
        var pedidoDatos = PedidosEF.ObtenerDatos(numero_operacion);

        if (pedidoDatos.idUsuarioSeguimiento == null)
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.info, "No se ha asignado un asesor.");
            return;
        }
        else
        {
            ddl_UsuarioSeguimiento.SelectedValue = pedidoDatos.idUsuarioSeguimiento.ToString();
            btn_asignarAsesor.Text = "Re asignar";
            //BootstrapCSS.Message(this, "#Content_msgUsuarioSeguimiento", BootstrapCSS.MessageType.info, "Asesor ya asignado",
            //"Un asesor ya se ha asignado a este pedido.");
            NotiflixJS.Message(this, NotiflixJS.MessageType.info, "Asesor asignado");
            return;
        }

    }
    async protected void CargarAsesores()
    {
        var Usuarios = await UsuariosEF.ObtenerUsuariosVendedores();

        // Filtrado para mostrar solo nombre y apellido
        var List = Usuarios.Select(x => new ListItem
        {
            Text = x.nombre + " " + x.apellido_paterno,
            Value = x.id.ToString()
        })
            .OrderBy(o => o.Text)
            .ToList();

        ddl_UsuarioSeguimiento.Items.Insert(0, new ListItem("Seleccionar", ""));
        ddl_UsuarioSeguimiento.DataSource = List;
        ddl_UsuarioSeguimiento.DataTextField = "Text";
        ddl_UsuarioSeguimiento.DataValueField = "Value";
        ddl_UsuarioSeguimiento.DataBind();
        //ddl_UsuarioSeguimiento.Items.Add(new ListItem(txt_box1.Text),);

        //ddl_UsuarioSeguimiento.DataBind();
    }
    protected async void btn_asignarAsesor_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddl_UsuarioSeguimiento.SelectedValue == "")
            {
                NotiflixJS.Message(this, NotiflixJS.MessageType.warning, "No se ha seleccionado un asesor");
             //   BootstrapCSS.Message(this, "#Content_msgUsuarioSeguimiento", BootstrapCSS.MessageType.warning,
             //"Aviso", "No haz seleccionado un asesor");
                return;
            }
            var pedidoDatos = PedidosEF.ObtenerDatos(numero_operacion);
            var DatosUsuario = UsuariosEF.Obtener(int.Parse(ddl_UsuarioSeguimiento.SelectedValue));
            var ResultAsignación = await PedidosEF.ActualizarAsesorAsigando(numero_operacion, DatosUsuario.id);

            if (ResultAsignación.result == false)
            {
                NotiflixJS.Message(this, NotiflixJS.MessageType.failure, ResultAsignación.message);
                //BootstrapCSS.Message(this, "#Content_msgUsuarioSeguimiento", BootstrapCSS.MessageType.danger, "Error", ResultAsignación.message);
                return;
            }

            DateTime fechaSolicitud = utilidad_fechas.obtenerCentral();
            string asunto = "Seguimiento de tu pedido: " + pedidoDatos.numero_operacion + " en INCOM.MX " + pedidoDatos.usuario_cliente + " ";
            string mensaje = string.Empty;

            string filePathHTML = "/email_templates/operaciones/pedidos/pedido_seguimiento.html";
            string dominio = Request.Url.GetLeftPart(UriPartial.Authority);
            string id_operacion_encritado = seguridad.Encriptar(pedidoDatos.id.ToString());
            string url_operacion = dominio + "/usuario/cliente/mi-cuenta/pedidos/resumen/" + id_operacion_encritado;

            Dictionary<string, string> datosDiccRemplazo = new Dictionary<string, string>
            {
                { "{fecha}", utilidad_fechas.DDMMAAAA() },
                { "{nombre}", pedidoDatos.cliente_nombre },
                { "{numero_operacion}", numero_operacion },
                { "{FechaPedido}", pedidoDatos.fecha_creacion.ToString() },
                { "{nombreAsesor}", DatosUsuario.nombre + " " + DatosUsuario.apellido_paterno },
                { "{emailAsesor}", DatosUsuario.email },
                { "{asesor_img}", DatosUsuario.email.Split('@')[0] },
                { "{url_operacion}", url_operacion }
            };

            mensaje = archivosManejador.reemplazarEnArchivo(filePathHTML, datosDiccRemplazo);


            // emailTienda email = new emailTienda(asunto, $"cmiranda@it4u.com.mx", mensaje, "retail@incom.mx");
            emailTienda email = new emailTienda(asunto, $"tpavia@incom.mx, ralbert@incom.mx, jaraujo@incom.mx, fgarcia@incom.mx, {pedidoDatos.email}", mensaje, "serviciosweb@incom.mx"); //iamado@2rent.mx, pjuarez@incom.mx ; retail@incom.mx
            email.general();

            //BootstrapCSS.Message(up_seguimientoUsuarioPedido, "#Content_msgUsuarioSeguimiento", BootstrapCSS.MessageType.success,
            // "Asignación correcta", "Asignación realizada con éxito.");
            NotiflixJS.Message(up_seguimientoUsuarioPedido, NotiflixJS.MessageType.success, "Asesor asignado.");
        }
        catch (Exception ex)
        {
            NotiflixJS.Message(up_seguimientoUsuarioPedido, NotiflixJS.MessageType.failure, "Error al asignar.");
            //BootstrapCSS.Message(up_seguimientoUsuarioPedido, "#Content_msgUsuarioSeguimiento", BootstrapCSS.MessageType.danger,
            //    "Error", "Ocurrio un error al asignar, contacta a desarrollo. Ex: " + ex.Message);
        }
        finally
        {
            up_seguimientoUsuarioPedido.Update();
        }
    }
}