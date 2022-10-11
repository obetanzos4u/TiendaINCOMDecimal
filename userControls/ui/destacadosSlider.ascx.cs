using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace tienda
{
    public partial class destacadosSlider : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarDestacados();
            }
        }

        protected void cargarDestacados()
        {
            destacados obtener = new destacados();
            DataTable destacados = obtener.obtenerDestacados();
            foreach (DataRow destacado in destacados.Rows)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");
                string numero_parte = destacado["numero_parte"].ToString();
                string marca = destacado["marca"].ToString();
                string[] imagenes = destacado["imagenes"].ToString().Split(',');
                string img_producto;
                string titulo = destacado["titulo"].ToString();
                string descripcion = destacado["descripcion_corta"].ToString();
                string imagePath = archivosManejador.appPath + @"\img_catalog\" + imagenes[0];
                bool imageExists = archivosManejador.validarExistencia(imagePath);
                string link = Request.Url.GetLeftPart(UriPartial.Authority) + GetRouteUrl("productos", new System.Web.Routing.RouteValueDictionary
                {
                    {"numero_parte", textTools.limpiarURL_NumeroParte(numero_parte) },
                    {"marca", marca },
                    {"productoNombre", textTools.limpiarURL(titulo) }
                });

                if (imageExists)
                {
                    img_producto = "/img_catalog/" + imagenes[0];
                } else
                {
                    img_producto = "/img/webUI/producto-imagen-temporal.jpg";
                }
                li.ID = "destacadoID";
                li.ClientIDMode = ClientIDMode.Static;
                li.Attributes.Add("class", "splide__slide");
                li.InnerHtml = "<a title='" + titulo + "' href='" + link +"'><img src='" + img_producto +"' alt='" + descripcion + "' class='img-destacados'/><p>" + titulo + "</p></a>";
                contenedorDestacados.Controls.Add(li);
            }
        }
    }
}