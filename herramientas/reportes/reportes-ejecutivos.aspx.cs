using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class herramientas_reportes_ejecutivos : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

        if (!IsPostBack) {
            actualizacionAutomaticaTipoCambio();
        }
    }
    protected void actualizacionAutomaticaTipoCambio() {
 
    }
       
}