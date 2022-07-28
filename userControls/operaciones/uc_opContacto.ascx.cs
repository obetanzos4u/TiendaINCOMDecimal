using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class userControls_uc_opContacto : System.Web.UI.UserControl {

    public string idSQL {
        get { return this.hf_idSQL.Value;}
        set { this.hf_idSQL.Value = value; }
        }
    public string tipo_operacion {
        get { return this.hf_tipo_operacion.Value; }
        set { this.hf_tipo_operacion.Value = value; }
        }

    protected void Page_Load(object sender, EventArgs e) {
        if (!IsPostBack) {


            } else {

            }
        }

    protected void cargarDatos(object sender, ListViewItemEventArgs e) {

        DataTable dtContacto = new DataTable();
        if(tipo_operacion == "cotización") {

            cotizaciones obtener = new cotizaciones();
            dtContacto = obtener.obtenerCotizacionDatos_min(int.Parse(idSQL));
            } else if(tipo_operacion == "pedido") {
            pedidosDatos obtener = new pedidosDatos();

            obtener.obtenerPedidoDatos(int.Parse(idSQL));
            }
        }
    }