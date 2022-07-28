using System;
 
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class usuario_cliente_pedido_datos : System.Web.UI.Page
{
    NumberFormatInfo myNumberFormatInfo = new CultureInfo("es-MX", true).NumberFormat;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Page.RouteData.Values["id_operacion"].ToString() != null)
            {

                string route_id_operacion = Page.RouteData.Values["id_operacion"].ToString();
                route_id_operacion = seguridad.DesEncriptar(route_id_operacion);

                lt_numero_pedido.Text = PedidosEF.ObtenerNumeroOperacion(int.Parse(route_id_operacion));

                hf_id_pedido.Value = route_id_operacion;
            }
            CargarDatosPedido();
            CargarProductos();
            cargarContactos();
            EstablecerNavegacion();

        }
       
    }

    protected void CargarDatosPedido()
    {

        int IdPedido = int.Parse(hf_id_pedido.Value);
        var DatosPedido = PedidosEF.ObtenerDatos(IdPedido);
        lbl_nombre.Text = $"{DatosPedido.cliente_nombre} {DatosPedido.cliente_apellido_paterno} {DatosPedido.cliente_apellido_materno} ";

        lbl_telefono.Text = string.IsNullOrWhiteSpace(DatosPedido.telefono)   ? "N/A" :DatosPedido.telefono;
        lbl_celular.Text = string.IsNullOrWhiteSpace(DatosPedido.celular) ? "N/A" : DatosPedido.celular;
        ;


    }
        protected void CargarProductos() {

 
     var productos =   PedidosEF.ObtenerProductos(lt_numero_pedido.Text);

        lbl_total_productos.Text = productos.Sum(t => t.precio_total).ToString("C2", myNumberFormatInfo); ;
        lv_productos.DataSource = productos;
        lv_productos.DataBind();
    }
    protected void cargarContactos()
    {

        usuarios user = usuarios.modoAsesor();


        lv_contactos.DataSource = ContactosEF.ObtenerContactos(user.id);
        lv_contactos.DataBind();

        
    }
    //text-white bg-success
    protected void btn_crear_contacto_Click(object sender, EventArgs e)
    {
        string nombre = textTools.lineSimple(txt_add_nombre.Text);
        string apellido_paterno = textTools.lineSimple(txt_add_apellido_paterno.Text);
        string apellido_materno = textTools.lineSimple(txt_add_apellido_materno.Text);
        string telefono = textTools.lineSimple(txt_add_telefono.Text);
        string celular = textTools.lineSimple(txt_add_celular.Text);

        ModelContactosValidador contactoModel = new ModelContactosValidador()
        { 
        nombre= nombre,
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
            validationResults.ForEach(n => resultmsg+=$"- {n.ErrorMessage } <br>");
            BootstrapCSS.Message(this, "#content_alert_crear_contacto", BootstrapCSS.MessageType.danger, "Error al crear contacto", resultmsg);
            return;
        }

        var mapper = new AutoMapper.Mapper(Mapeos.getContactos);
        contacto contacto = mapper.Map<contacto>(contactoModel);
        var result = ContactosEF.Guardar(contacto);
        if (result.result) {

            BootstrapCSS.Message(this, "#content_alert_crear_contacto", BootstrapCSS.MessageType.success, "Creado con éxito", result.message);
        }
        else
        {
            BootstrapCSS.Message(this, "#content_alert_crear_contacto", BootstrapCSS.MessageType.danger, "Error", result.message);
        }
       

    }


    protected void btn_usarDatos_Click(object sender, EventArgs e)
    {
        LinkButton btn_usarDatos = (LinkButton)sender;
        // Obtenemos el contenedor del objeto que creo el evento
        ListViewItem lvItem = (ListViewItem)btn_usarDatos.NamingContainer;

        HiddenField hf_id_contacto = (HiddenField)lvItem.FindControl("hf_id_contacto");

        int idContacto = int.Parse(hf_id_contacto.Value);
        int idPedido = int.Parse( hf_id_pedido.Value);

        var contacto = ContactosEF.Obtener(idContacto);

        PedidosEF.GuardarContacto(idPedido, contacto);
     



        CargarDatosPedido();
    }
    protected void EstablecerNavegacion()
    {

        link_envio.NavigateUrl = GetRouteUrl("cliente-pedido-envio", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion",seguridad.Encriptar(hf_id_pedido.Value) }
                    });

        link_datos.NavigateUrl = GetRouteUrl("cliente-pedido-datos", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion",seguridad.Encriptar(hf_id_pedido.Value) }
                    });

        link_pago.NavigateUrl = GetRouteUrl("cliente-pedido-pago", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion",seguridad.Encriptar(hf_id_pedido.Value) }
                    });
    }
    protected void OnItemUpdating(object sender, ListViewUpdateEventArgs e)
    {

        //     <asp:HiddenField ID="hf_id_direccion" Value='<%#Eval("id") %>' runat="server" />
        int IdContacto = int.Parse((lv_contactos.Items[e.ItemIndex].FindControl("hf_id_contacto") as HiddenField).Value);
        string nombre = textTools.lineSimple( (lv_contactos.Items[e.ItemIndex].FindControl("txt_edit_nombre") as TextBox).Text);
        string apellido_paterno = textTools.lineSimple((lv_contactos.Items[e.ItemIndex].FindControl("txt_edit_apellido_paterno") as TextBox).Text);
        string apellido_materno = textTools.lineSimple((lv_contactos.Items[e.ItemIndex].FindControl("txt_edit_apellido_materno") as TextBox).Text);
        string celular = textTools.lineSimple((lv_contactos.Items[e.ItemIndex].FindControl("txt_edit_celular") as TextBox).Text);
        string telefono = textTools.lineSimple((lv_contactos.Items[e.ItemIndex].FindControl("txt_edit_telefono") as TextBox).Text);


        ModelContactosValidador contactoModel = new ModelContactosValidador()
        {
            id = IdContacto,
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
            BootstrapCSS.Message(this, "#content_alert_actualizar_contacto_"+ IdContacto, BootstrapCSS.MessageType.danger, "Error al crear contacto", resultmsg);
            return;
        }

     
        var mapper = new AutoMapper.Mapper(Mapeos.getContactos);
        contacto contacto = mapper.Map<contacto>(contactoModel);

        var result = ContactosEF.Guardar(contacto);
        if (result.result)
        {
            OnItemCanceling(sender, null);
            cargarContactos();
            BootstrapCSS.Message(this, "#content_alert_actualizar_contacto_" + IdContacto, BootstrapCSS.MessageType.success, "Actualizado con éxito", result.message);
        }
        else
        {
            BootstrapCSS.Message(this, "#content_alert_actualizar_contacto_" + IdContacto, BootstrapCSS.MessageType.danger, "Error", result.message);
        }
     
    }
    protected void OnItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        lv_contactos.EditIndex = -1;
        cargarContactos();
    }
    protected void OnItemEditing(object sender, ListViewEditEventArgs e)
    {
        lv_contactos.EditIndex = e.NewEditIndex;
        cargarContactos();
    }

    protected void btn_eliminarContacto_Click(object sender, EventArgs e)
    {
        
    LinkButton btnEliminar = (LinkButton)sender;
    // Obtenemos el contenedor del objeto que creo el evento
    ListViewItem lvItem = (ListViewItem)btnEliminar.NamingContainer;

    HiddenField hf_id_contacto = (HiddenField)lvItem.FindControl("hf_id_contacto");

    int iDContacto = int.Parse(hf_id_contacto.Value);
    

    var result =  ContactosEF.eliminar(iDContacto);

        if (result.result)
        {
        
            cargarContactos();
         
        }
        else
        {
             
        }

    }

protected void lv_productos_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        dynamic producto = e.Item.DataItem as dynamic;

        Literal lt_precio_unitario = (Literal)e.Item.FindControl("lt_precio_unitario");
        lt_precio_unitario.Text = producto.precio_unitario.ToString("C2", myNumberFormatInfo);

        Literal lt_precio_total = (Literal)e.Item.FindControl("lt_precio_total");
        lt_precio_total.Text = producto.precio_total.ToString("C2", myNumberFormatInfo);


        Literal lt_cantidad = (Literal)e.Item.FindControl("lt_cantidad");
        lt_cantidad.Text = Decimal.ToInt32(producto.cantidad).ToString(); 







    }
 
}