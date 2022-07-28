using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class usuario_pedidos : System.Web.UI.Page
{ 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            Page.Title = "Pedidos";
            ddl_ordenTipo.SelectedValue = Request.QueryString["ordenTipo"];
            ddl_ordenBy.SelectedValue = Request.QueryString["ordenBy"];
            txt_search.Text = Request.QueryString["search"];
            cargarInfo(sender, e); }

    }

    protected void orden(object sender, EventArgs e) {

        var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
        nameValues.Set("ordenTipo", ddl_ordenTipo.SelectedValue);
        nameValues.Set("ordenBy", ddl_ordenBy.SelectedValue);
        nameValues.Set("search", txt_search.Text);
        string url = Request.Url.AbsolutePath;


        Type type = sender.GetType();
        if (sender.GetType().Name == "TextBox") {

            if ((sender as TextBox).ID == "txt_search") {
                nameValues.Set("PageId", "1");
                }
            }

        Response.Redirect(url + "?" + nameValues); // ToString() is called implicitly
        }

    protected void cargarInfo(object sender, EventArgs e) {

        usuarios datosUsuario = usuarios.modoAsesor();
        pedidosDatos obtener = new pedidosDatos();
        DataTable dtPedidos = obtener.obtenerPedidosUsuario_min(datosUsuario.email);


        // INICIO - Motor de búsqueda
        if (!string.IsNullOrEmpty(txt_search.Text)) {
            string find = txt_search.Text;
            string query = "nombre_pedido Like '%" + find + "%' OR  numero_operacion Like '%" + find + "%'";
            dtPedidos = buscador.filtrar(this, dtPedidos, query, find);
            }
        // FIN - Motor de búsqueda 

        // INICIO - ORDEN de visualización
        DataView dv = dtPedidos.DefaultView;
        dv.Sort = ddl_ordenBy.SelectedValue + " " + ddl_ordenTipo.SelectedValue;
        dtPedidos = dv.ToTable();
        // FIN - ORDEN de visualización


        lvPedidos.DataSource = dtPedidos;
        lvPedidos.DataBind();
       

    }

    protected void cambiarNombrePedido(object sender, EventArgs e) {


        TextBox txt_nombre_pedido = (TextBox)sender;
        ListViewItem lvItem = (ListViewItem)txt_nombre_pedido.NamingContainer;


        // Buscamos un objeto dentro del contenedor
        HiddenField hf_id_pedidoSQL = (HiddenField)lvItem.FindControl("hf_id_pedidoSQL");

        string nombre_pedido = txt_nombre_pedido.Text;
        pedidosDatos editar = new pedidosDatos();

        if (validarCampos.cotizacion_nombreCotizacion(nombre_pedido, this)) {
            if (editar.editarNombrePedido(int.Parse(hf_id_pedidoSQL.Value), nombre_pedido) != null) {

                materializeCSS.crear_toast(this, "Nombre actualizado con éxito.", true);
                } else {
                materializeCSS.crear_toast(this, "Error al actualizar nombre.", false);
                }
            } else {
            materializeCSS.crear_toast(this, "No cumple con la longitud requerida", false);
            }
           

        }

    protected void lvPedidos_ItemDataBound(object sender, ListViewItemEventArgs e) {
        Label lbl_numero_operacion = e.Item.FindControl("lbl_numero_operacion") as Label;
        HyperLink btn_editarPedido = e.Item.FindControl("btn_editarPedido") as HyperLink;
        HiddenField hf_id_pedidoSQL = e.Item.FindControl("hf_id_pedidoSQL") as HiddenField;
        HyperLink btn_visualizar = e.Item.FindControl("btn_visualizar") as HyperLink;

        btn_editarPedido.NavigateUrl =  

        GetRouteUrl("cliente-pedido-resumen", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion", seguridad.Encriptar(hf_id_pedidoSQL.Value) }
                    });
        ListView lvProductos = e.Item.FindControl("lvProductos") as ListView;
        pedidosProductos obtener = new pedidosProductos();

        DataTable dt_productosCotizacionMin = obtener.obtenerProductosPedido_min(lbl_numero_operacion.Text,5);

        if(dt_productosCotizacionMin != null) {
            lvProductos.DataSource = dt_productosCotizacionMin;
            lvProductos.DataBind();
            }

        btn_visualizar.NavigateUrl = GetRouteUrl("usuario-pedido-visualizar", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion", seguridad.Encriptar(hf_id_pedidoSQL.Value) },

                     });

        }




    protected void lvProductos_ItemDataBound(object sender, ListViewItemEventArgs e) {

        Literal lt_descripcion = e.Item.FindControl("lt_descripcion") as Literal;

        System.Data.DataRowView rowView = e.Item.DataItem as System.Data.DataRowView;

        string descripcion = rowView["descripcion"].ToString();

        if(descripcion.Length > 40) {
            descripcion = descripcion.Substring(0, 40);
        }
        lt_descripcion.Text = descripcion;

        }

    protected void OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e) {

        (lvPedidos.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        cargarInfo(sender, null);
        }
    }