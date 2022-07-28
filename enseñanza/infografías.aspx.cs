using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class enseñanza_infografías : System.Web.UI.Page {
    static string _URL;
    protected void Page_Load(object sender, EventArgs e) {
        if (!IsPostBack) { 
            _URL = Request.Url.GetLeftPart(UriPartial.Authority);  
    obtenerInfografias();
            Title = "Infografías Incom - Aprendizaje";
            if (HttpContext.Current.User.Identity.IsAuthenticated && usuarios.userLogin().rango == 3)
                AdminOptions.Visible = true;

        }
       
    }


    protected void obtenerInfografias()
    {

        lv_infografías.DataSource = infografíasController.obtener();
        lv_infografías.DataBind();
    }

    protected void lv_infografías_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HyperLink link_img_miniatura = (HyperLink)e.Item.FindControl("link_img_miniatura");
        HyperLink link_infografia = (HyperLink)e.Item.FindControl("link_infografia");

        HtmlGenericControl adminOptions = (HtmlGenericControl)e.Item.FindControl("admin_options");


        if (HttpContext.Current.User.Identity.IsAuthenticated && usuarios.userLogin().rango == 3)
            adminOptions.Visible = true;

            if (e.Item.ItemType == ListViewItemType.DataItem)
        {

    
            infografías infografía = e.Item.DataItem as infografías;

            link_img_miniatura.NavigateUrl =  GetRouteUrl("infografia", new System.Web.Routing.RouteValueDictionary {
                        { "titulo", utilidadURL.TextoAmigable( infografía.titulo) },
                         { "id", infografía.id },
      
                    });
             

            link_infografia.NavigateUrl = link_img_miniatura.NavigateUrl;

        }
    }

    protected void btn_eliminar_Click(object sender, EventArgs e)
    {
        LinkButton btnEliminar = (LinkButton)sender;
        // Obtenemos el contenedor del objeto que creo el evento
        ListViewItem lvItem = (ListViewItem)btnEliminar.NamingContainer;

        HiddenField hf_idInfografia = (HiddenField)lvItem.FindControl("hf_idInfografia");

        int idInfografia = int.Parse(hf_idInfografia.Value);

        infografíasController eliminar = new infografíasController();

        eliminar.eliminar(idInfografia);

        bulmaCSS.Message(this, "#contentResultado", bulmaCSS.MessageType.success, "Eliminado con éxito");
        obtenerInfografias();

      
    }
}