using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;

public partial class categorias_pag : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		Title = "Categorias";
        Page.MetaDescription = "Fibra óptica, cableado estructurado. 3M, ADC, Brady, Corning, DCD, EJ América latina, Icoptiks, Leviton, Multilink, NCS Jaguar, Norscan, PLP, Ripley, TE Connectivity, Tuliko, Werner, Manholes, Prefabricados";
        descripcion_categoria.InnerText = Page.MetaDescription;
            categoriasTodas.NavigateUrl = "productos";
    }
}