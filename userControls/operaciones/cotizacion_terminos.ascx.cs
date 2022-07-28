using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
 
    public partial class uc_cotizacion_terminos : System.Web.UI.UserControl {

        public string numero_operacion {
            get {
                return this.hf_numero_operacion.Value;
            }
            set {
                this.hf_numero_operacion.Value = value;
            }
        }
       //<summary>Determina si es visible el listado (Etiquetas Label-) </summary>
        public bool visibleInfo {
            get {
                return bool.Parse(this.hf_visibleInfo.Value);
            }
            set {
                this.hf_visibleInfo.Value =  value.ToString();
            }
        }
 
        protected void Page_Load(object sender, EventArgs e) {
            validarModoAsesor();
 
    }
            protected void mostrarModal() {
            string script = @" $(document).ready(function(){   $('#modal_editar_cotizacion_terminos').modal('open');    });";
            ScriptManager.RegisterStartupScript(up_cotizacion_terminos, typeof(UpdatePanel), "modal_editar_cotizacion_terminos", script, true);
        
    }
        protected void validarModoAsesor() {

        if (usuarios.userLogin().tipo_de_usuario == "cliente") {
            btn_GuardarTerminosCotizacion.Visible = false;
            btn_cotizacionTerminos.Visible = false;
            }
        }
    public void cargarDatos() {

        List<cotizaciones_terminos> terminos = cotizaciones_terminos.ObtenerTerminosCotizacion(hf_numero_operacion.Value);

        if (terminos != null && terminos.Count >= 1) {

            foreach (cotizaciones_terminos term in terminos) {

                switch (term.idTipoTermino) {
                    case cotizaciones_terminos.tipoTermino.TiempoDeEntrega: cargarTiempoEntrega(term); break;
                    case cotizaciones_terminos.tipoTermino.FormaDePago: cargarFormaDePago(term); break;
                    case cotizaciones_terminos.tipoTermino.Entrega: cargarEntrega(term); break;
                }
                     

            }
            up_cotizacion_terminos.Update();
            up_visibleInfo.Update();
        } else {
            lbl_TerminoTiempoEntrega.Text = "<strong>Tiempo de entrega:</strong> Aún no establecido";
            lbl_TerminoEntrega.Text = "<strong>Forma de pago:</strong> Aún no establecido";
            lbl_TerminoFormaDePago.Text = "<strong>Entrega:</strong> Aún no establecido";
        }
        }

        protected void cargarTiempoEntrega(cotizaciones_terminos term) {

            if (term.termino.Contains(",")) {
                string[] tiempoValores = term.termino.Split(',');
                int indice = int.Parse(tiempoValores[0]);
                string termino;


            switch (indice) {
                case 0:
                    chk_FechaTiempoEntrega.Items[indice].Selected = true;
                    termino = tiempoValores[1];
                    lbl_TerminoTiempoEntrega.Text = "<strong>Tiempo de entrega:</strong> " + termino;
                    break;
                case 1:
                    string numeroFechaTiempoEntrega = tiempoValores[1];
                    string TipoFecha = tiempoValores[2];

                    chk_FechaTiempoEntrega.Items[indice].Selected = true;
                    txt_numeroFechaTiempoEntrega.Text = numeroFechaTiempoEntrega;
                    ddlTipoFecha.SelectedValue = TipoFecha;
                    txt_numeroFechaTiempoEntrega.Visible = true;
                    ddlTipoFecha.Visible = true;

                    termino = numeroFechaTiempoEntrega + " " + TipoFecha;
                    lbl_TerminoTiempoEntrega.Text = "<strong>Tiempo de entrega:</strong> " + termino;

                    break;
                case 2:
                    chk_FechaTiempoEntrega.Items[indice].Selected = true;
                    termino = tiempoValores[1];
                    lbl_TerminoTiempoEntrega.Text = "<strong>Tiempo de entrega:</strong> " + termino;


                    break;
            }
 
             
                hf_TiempoDeEntrega.Value = term.idTermino.ToString();
            } else {
                chk_FechaTiempoEntrega.Items[0].Selected = true;
            lbl_TerminoTiempoEntrega.Text = "<strong>Tiempo de entrega:</strong> Aún no establecido";
             
        }

        }

        protected void cargarFormaDePago(cotizaciones_terminos term) {
         
            if (term.termino.Contains(",")) {
                string[] terminoValores = term.termino.Split(',');
                int indice = int.Parse(terminoValores[0]);
                ddl_FormaDePago.Items[indice].Selected = true;
                hf_FormaDePago.Value = term.idTermino.ToString() ;

                string termino = terminoValores[1];
                lbl_TerminoFormaDePago.Text = "<strong>Forma de pago:</strong> " + termino;
        } else {
            lbl_TerminoFormaDePago.Text = "<strong>Forma de pago:</strong> Aún no establecido";
        }


    }

        protected void cargarEntrega(cotizaciones_terminos term) {
            if (term.termino.Contains(",")) {
                string[] terminoValores = term.termino.Split(',');

                int indice = int.Parse(terminoValores[0]);
                ddl_Entrega.Items[indice].Selected = true;
                hf_Entrega.Value = term.idTermino.ToString();

                string termino = terminoValores[1];
                lbl_TerminoEntrega.Text = "<strong>Entrega:</strong> " + termino;
            } else {
            lbl_TerminoEntrega.Text = "<strong>Entrega:</strong> Aún no establecido";
        }

    }
        protected void btn_GuardarTerminosCotizacion_Click(object sender, EventArgs e) {
            string numero_operacion = hf_numero_operacion.Value;

            // INICIO - Tiempo de entrega
            string idTerminoTiempoDeEntrega = hf_TiempoDeEntrega.Value;
            string tipo_FechaTiempoEntrega = chk_FechaTiempoEntrega.SelectedValue;
            string termino = "";

        switch (tipo_FechaTiempoEntrega) {
            case "0": termino = "0,Inmediato en almacén"; break;
            case "1": termino = "1," + txt_numeroFechaTiempoEntrega.Text + "," + ddlTipoFecha.SelectedValue; break;
            case "2": termino = "2,Por confirmar";  break;
        }
            
            if (string.IsNullOrWhiteSpace(idTerminoTiempoDeEntrega)) {
                cotizaciones_terminos term = new cotizaciones_terminos(
                    null,
                    numero_operacion,
                    cotizaciones_terminos.tipoTermino.TiempoDeEntrega, termino);
                term.GuardarTermino(); } else {
                cotizaciones_terminos.ActualizarTermino(int.Parse(hf_TiempoDeEntrega.Value), termino);
            }

            // FIN - Tiempo de entrega



            // INICIO - Forma de pago
            string idFormaDePago = hf_FormaDePago.Value;

            string terminoFormaDePago = ddl_FormaDePago.SelectedIndex + "," + ddl_FormaDePago.Items[ddl_FormaDePago.SelectedIndex].Text;

            if (string.IsNullOrWhiteSpace(idFormaDePago)) {
                cotizaciones_terminos term = new cotizaciones_terminos(
                     null,
                     numero_operacion,
                     cotizaciones_terminos.tipoTermino.FormaDePago, terminoFormaDePago);
                term.GuardarTermino();
            } else {
                cotizaciones_terminos.ActualizarTermino(int.Parse(hf_FormaDePago.Value), terminoFormaDePago);
            }
            // FIN - Forma de pago


            // INICIO - Entrega
            string idEntrega = hf_Entrega.Value;

            string terminoEntrega = ddl_Entrega.SelectedIndex + "," + ddl_Entrega.Items[ddl_Entrega.SelectedIndex].Text;

            if (string.IsNullOrWhiteSpace(idEntrega)) {
                cotizaciones_terminos term = new cotizaciones_terminos(
                  null,
                  numero_operacion,
                  cotizaciones_terminos.tipoTermino.Entrega, terminoEntrega);
                term.GuardarTermino();
            }
               
            else {
                cotizaciones_terminos.ActualizarTermino(int.Parse(hf_Entrega.Value), terminoEntrega);
              
            }
        // FIN - Entrega

        cargarDatos();
    }

        protected void chk_FechaTiempoEntrega_SelectedIndexChanged(object sender, EventArgs e) {

            if(chk_FechaTiempoEntrega.SelectedValue== "1") {
                txt_numeroFechaTiempoEntrega.Visible = true;
                ddlTipoFecha.Visible = true;
                   
                               } 
            else {
                txt_numeroFechaTiempoEntrega.Visible = false;
                ddlTipoFecha.Visible = false;
            }

            up_cotizacion_terminos.Update();
            mostrarModal();
        }
    }
 