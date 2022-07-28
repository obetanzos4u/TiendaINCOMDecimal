using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tienda {
    public partial class uc_editProductoPersonalizado : System.Web.UI.UserControl {

        public string idProducto {
            get { return this.hf_idProducto.Value; }
            set { this.hf_idProducto.Value = value; }
            }

        public string numero_operacion {
            get { return this.hf_numero_operacion.Value; }
            set { this.hf_numero_operacion.Value = value; }
            }
 
        
        protected void btn_editarProductoPersonalizado_Click(object sender, EventArgs e) {
            string idProducto = hf_idProducto.Value;
            string numero_operacion = hf_numero_operacion.Value;
         

            this.Page.GetType().InvokeMember("OpenEditProdPersonalizadoModal", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { sender, hf_idProducto.Value });

            }
        }
    }