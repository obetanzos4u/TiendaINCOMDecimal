using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class enseñanza_base : System.Web.UI.Page {

 
    protected void Page_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            
 
            CargarInfografía();

            link_infografias.NavigateUrl =GetRouteUrl("infografias",null);
            link_infografias.Text = "Infografías";

        }
    }
    protected void CargarInfografía (){

        if (Page.RouteData.Values["id"] == null) {
            return;
        }

       var id = int.Parse( Page.RouteData.Values["id"].ToString());
      
        var infografia = infografíasController.obtener(id);


        if(infografia == null) {
            
            return;
        }
        Title = infografia.titulo + " - Enciclopedico";
        MetaDescription = infografia.descripción;
        TituloInfografia.InnerText = infografia.titulo;
        lt_descripción.Text = infografia.descripción;

        link_infografiaActual.NavigateUrl = GetRouteUrl("infografia", new System.Web.Routing.RouteValueDictionary {
                        { "titulo", utilidadURL.TextoAmigable( infografia.titulo) },
                         { "id", infografia.id },

                    });

        link_infografiaActual.Text = infografia.titulo;

        string InfografiaFileName = infografia.nombreArchivo;

        switch (InfografiaFileName.Substring(InfografiaFileName.Length-3,3)) {

            case "pdf": MostrarPDF(infografia);  break;
            case "gif": MostrarImagen(infografia);  break;
            case "jpg": MostrarImagen(infografia); break;
        }


    }
    private void MostrarPDF(infografías Infografia) {

         var iFrame = new System.Web.UI.HtmlControls.HtmlGenericControl("iframe");
         iFrame.Attributes.Add("src", $"/img/infografías/{Infografia.nombreArchivo}");
 
        Content_Infografia.Controls.Add(iFrame);

    }
    private void MostrarImagen(infografías Infografia) {

        var img = new System.Web.UI.HtmlControls.HtmlGenericControl("img");
        img.Attributes.Add("title", Infografia.titulo);
        img.Attributes.Add("alt", Infografia.titulo);

        img.Attributes.Add("src", $"/img/infografías/{Infografia.nombreArchivo}");
        Content_Infografia.Controls.Add(img);
    }
}