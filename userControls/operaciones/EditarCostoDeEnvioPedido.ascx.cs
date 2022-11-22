using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class userControls_operaciones_EditarCostoDeEnvio : System.Web.UI.UserControl
{
    public string numero_operacion
    {
        get { return this.hf_numero_operacion.Value; }
        set { this.hf_numero_operacion.Value = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var UserLogin = usuarios.userLogin();
            if (UserLogin.tipo_de_usuario == "usuario")
            {
                up_ContentDetallesEnvioPedido.Visible = true;
            }
        }
    }
    protected void btn_ModificarDetallesEnvio_Click(object sender, EventArgs e)
    {
        ContentDetallesEnvioPedido.Visible = true;
        Content_btn_ModificarDetallesEnvio.Visible = false;
    }
    protected void btn_CerrarModificarDetallesDeEnvio_Click(object sender, EventArgs e)
    {
        ContentDetallesEnvioPedido.Visible = false;
        Content_btn_ModificarDetallesEnvio.Visible = true; ;
    }
    protected void btn_guardarCostoDeEnvio_Click(object sender, EventArgs e)
    {
        decimal envio = textTools.soloNumerosD(txt_MontoCostoEnvio.Text);
        string resultado = pedidosDatos.actualizarEnvio(envio, "Estándar", hf_numero_operacion.Value);
        bool resultadoTotales = pedidosProductos.actualizarTotalStatic(hf_numero_operacion.Value);
        if (resultadoTotales && resultado != null && resultado != "0")
        {
            NotiflixJS.Message(up_ContentDetallesEnvioPedido, NotiflixJS.MessageType.success, "Costo de envío actualizado");
            string script = @"setTimeout(function () { window.location.reload(1); }, 1500);";
            ScriptManager.RegisterStartupScript(up_ContentDetallesEnvioPedido, typeof(UpdatePanel), "redirección", script, true);
            //BootstrapCSS.Message(up_ContentDetallesEnvioPedido, "#Conteng_msg_envio", BootstrapCSS.MessageType.success, "Actualizado con éxito", "Envío actualizado con éxito");
            up_ContentDetallesEnvioPedido.Update();
        }
        else
        {
            //BootstrapCSS.Message(up_ContentDetallesEnvioPedido, "#Conteng_msg_envio", BootstrapCSS.MessageType.danger, "Error", "Error al actualizar método de envío");
            NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "Error al actualizar");
            up_ContentDetallesEnvioPedido.Update();
        }
    }
}