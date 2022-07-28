using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class enseñanza_glosario : System.Web.UI.Page {

    protected string letraActual;
    protected void Page_Load(object sender, EventArgs e) {

        if (!IsPostBack) {
            
            leerLetra();
            cargarMenuAbecedario();
            Title = "Glosario Incom";
            if (HttpContext.Current.User.Identity.IsAuthenticated && usuarios.userLogin().rango == 3)
                AdminOptions.Visible = true;
        }

    }

    protected void leerLetra() {

        if (Page.RouteData.Values["letra"] != null) {
            string letra = Page.RouteData.Values["letra"].ToString();

            // Protegiendo que no pongan más caracteres de los necesarios en el RouteData
            if (letra.Length > 1) letra = letra.Substring(0, 1);

            letraActual = letra.ToUpper() ;
            cargarTérminos(letraActual);
        }
    }

    protected void cargarTérminos(string letra) {

        lv_términos.DataSource = glosarioController.obtenerByLetra(letra);
        lv_términos.DataBind();
    }
    protected void cargarMenuAbecedario() {
        string[] abecedario = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "Ñ", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
       
        
        using (var db = new tiendaEntities())
        {

          
      
            
            
            var glosario = db.Database.SqlQuery<string>("  SELECT * FROM (SELECT   SUBSTRING(término, 1, 1) as letra FROM glosario) AS t GROUP BY letra")

              .ToList();


            HtmlGenericControl ul = new HtmlGenericControl("ul");
            ul.ID = "menu_abecedario";

            foreach (string letra in glosario)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");
                HyperLink linkLetra = new HyperLink();
                linkLetra.ID = letra;
                linkLetra.CssClass = "pagination-link";
                linkLetra.Text = letra;

                if (!string.IsNullOrEmpty(letraActual) && letra == letraActual) linkLetra.CssClass = "pagination-link is-current";
                // Request.Url.GetLeftPart(UriPartial.Authority)
                linkLetra.NavigateUrl = GetRouteUrl("glosario", new System.Web.Routing.RouteValueDictionary {
                        { "letra", letra }
                    });

                li.Controls.Add(linkLetra);
                menu_abecedario.Controls.Add(li);

            }
        }

    


     



 
    }




    protected void lv_términos_DataBound(object sender, ListViewItemEventArgs e) {

      
    }

    protected void txt_buscador_TextChanged(object sender, EventArgs e) {

        string texto = textTools.lineSimple(txt_buscador.Text);
       // if (string.IsNullOrWhiteSpace(texto)) 
        lv_términos.DataSource = glosarioController.obtenerByBusqueda(texto);
        lv_términos.DataBind();
        cargarMenuAbecedario();
    }

    protected void btn_restablecer_búsqueda_Click(object sender, EventArgs e) {
        txt_buscador.Text = "";
        cargarMenuAbecedario();
        lv_términos.DataSource = null;
        lv_términos.DataBind();
    }
}