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
    public partial class homeSlider : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable slides = configuracion_sliders.obtenerImagenesSlider("sliderHome");

                int i = 0;
                for (i = 0; slides.Rows.Count > i; i++)
                {
                    int activo = int.Parse(slides.Rows[i]["activo"].ToString());
                    if (activo == 0) continue;
                    string nombreArchivo = "/img/webUI/sliderHome/" + slides.Rows[i]["nombreArchivo"].ToString();
                    string link = slides.Rows[i]["link"].ToString();
                    string titulo = slides.Rows[i]["titulo"].ToString();
                    string opciones = slides.Rows[i]["opciones"].ToString();

                    HtmlGenericControl control = new HtmlGenericControl();
                    control.TagName = "li";
                    control.Attributes.Add("class", "splide__slide");
                    HyperLink hpLink = new HyperLink();
                    Image imgSlider = new Image();

                    hpLink.ToolTip = titulo;
                    imgSlider.AlternateText = titulo;
                    imgSlider.ToolTip = titulo;
                    imgSlider.ImageUrl = nombreArchivo;
                    imgSlider.CssClass = "responsive-img IncomWebpToJpg";
                    if (!string.IsNullOrWhiteSpace(link))
                    {
                        hpLink.NavigateUrl = link;
                        hpLink.Controls.Add(imgSlider);

                        if (!string.IsNullOrWhiteSpace(opciones))
                        {
                            hpLink.Target = opciones;
                        }
                        control.Controls.Add(hpLink);
                    }
                    else
                    {
                        control.Controls.Add(imgSlider);
                    }
                    bxsliderHome.Controls.Add(control);
                }
            }
        }
    }
}

