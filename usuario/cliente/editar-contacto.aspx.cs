using System;
 
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class usuario_cliente_editar_contacto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Page.RouteData.Values["id_contacto"] != null)
            {

                obtenerContacto();


            }
        }
    }
    protected void obtenerContacto()
    {
      
      int IdContacto = int.Parse(Page.RouteData.Values["id_contacto"].ToString());

        hf_id_contacto.Value = IdContacto.ToString();

        var Contacto = ContactosEF.Obtener(IdContacto);

        if(Contacto != null)
        {
            txt_add_nombre.Text = Contacto.nombre;
            txt_add_apellido_paterno.Text = Contacto.apellido_paterno;
            txt_add_apellido_materno.Text = Contacto.apellido_materno;
            txt_add_telefono.Text = Contacto.telefono;
            txt_add_celular.Text = Contacto.celular;

        }
        else
        {

            hf_id_contacto.Value = "";
            btn_editarcontacto.Text = "No se encontró ningún contacto.";
            btn_editarcontacto.Enabled = false;
        }

    }
    protected void btn_editar_contacto_Click(object sender, EventArgs e)
    {
        int idUsuario = usuarios.modoAsesor().id;
        string nombre = textTools.lineSimple(txt_add_nombre.Text);
        string apellido_paterno = textTools.lineSimple(txt_add_apellido_paterno.Text);
        string apellido_materno = textTools.lineSimple(txt_add_apellido_materno.Text);
        string telefono = textTools.lineSimple(txt_add_telefono.Text);
        string celular = textTools.lineSimple(txt_add_celular.Text);

        ModelContactosValidador contactoModel = new ModelContactosValidador()
        {
            id_cliente = idUsuario,
            nombre = nombre,
            apellido_paterno = apellido_paterno,
            apellido_materno = apellido_materno,
            telefono = telefono,
            celular = celular


        };

        ValidationContext context = new ValidationContext(contactoModel, null, null);
        List<ValidationResult> validationResults = new List<ValidationResult>();
        bool valid = Validator.TryValidateObject(contactoModel, context, validationResults, true);

        if (!valid)
        {
            string resultmsg = string.Empty;
            validationResults.ForEach(n => resultmsg += $"- {n.ErrorMessage } <br>");
            BootstrapCSS.Message(this, "#content_alert_crear_contacto", BootstrapCSS.MessageType.danger, "Error al crear contacto", resultmsg);
            return;
        }

        var mapper = new AutoMapper.Mapper(Mapeos.getContactos);
        contacto contacto = mapper.Map<contacto>(contactoModel);
        contacto.id = int.Parse(hf_id_contacto.Value);
        var result = ContactosEF.Guardar(contacto);
        if (result.result)
        {

            ValidarReferenciaPedido();
            BootstrapCSS.Message(this, "#content_alert_crear_contacto", BootstrapCSS.MessageType.success, "Actualizado con éxito", result.message);
        }
        else
        {
            BootstrapCSS.Message(this, "#content_alert_crear_contacto", BootstrapCSS.MessageType.danger, "Error", result.message);
        }


    }

    protected void ValidarReferenciaPedido()
    {

        if (Request.QueryString["ref"] != null)
        {
            int idPedido = int.Parse( seguridad.DesEncriptar( Request.QueryString["ref"].ToString()));
 


            int idContacto = int.Parse(hf_id_contacto.Value);
          

            var contacto = ContactosEF.Obtener(idContacto);

            var result = PedidosEF.GuardarContacto(idPedido, contacto);

          
            string redirectUrl = GetRouteUrl("cliente-pedido-resumen", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion", seguridad.Encriptar(idPedido.ToString())}
                    });
 
            BootstrapCSS.RedirectJs(this, redirectUrl, 2000);
        }
    }
}