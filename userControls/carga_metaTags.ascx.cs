using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace tienda {

    
    public partial class carga_metatags : System.Web.UI.UserControl
    {
     

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
               if(usuarios.userLogin().tipo_de_usuario== "usuario") {
                    mostrarModal();
                }
            }
        }


        protected void  mostrarModal( ) {

            content_carga_metatags.Visible = true;
           
        }



        protected void btn_guardar_metatag_Click(object sender, EventArgs e) {

            string metatag = txt_carga_metatags.Text;
            if (!string.IsNullOrWhiteSpace(metatag)) { 
            if (metatag.Length > 50) metatag = metatag.Substring(0, 50);
            metatag = textTools.lineSimple(metatag);

            int idUsuario = usuarios.userLogin().id;
                string numero_parte = Page.RouteData.Values["numero_parte"].ToString();

                try {
                SqlCommand cmd = new SqlCommand();
                SqlConnection con = new SqlConnection(conexiones.conexionTienda());
                cmd.Connection = con;
                using (con) {

                    StringBuilder query = new StringBuilder();

                    query.Append("SET LANGUAGE English; " +
                                "INSERT INTO productos_metatagsColaborativos (metatag, idUsuario, fecha_creacion, numero_parte)");
                    query.Append(" VALUES  (@metatag, @idUsuario, @fecha_creacion, @numero_parte)");


                    cmd.CommandText = query.ToString();
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@metatag", SqlDbType.NVarChar, 50);
                    cmd.Parameters["@metatag"].Value = metatag;

                    cmd.Parameters.Add("@idUsuario", SqlDbType.Int);
                    cmd.Parameters["@idUsuario"].Value = idUsuario;

                    cmd.Parameters.Add("@fecha_creacion", SqlDbType.DateTime);
                    cmd.Parameters["@fecha_creacion"].Value = utilidad_fechas.obtenerCentral();

                    cmd.Parameters.Add("@numero_parte", SqlDbType.NVarChar, 100);
                    cmd.Parameters["@numero_parte"].Value = numero_parte;
                        
                    con.Open();
                    cmd.ExecuteNonQuery();
                 
                }

                materializeCSS.crear_toast(this.Page, "Meta tag guardado con éxito", true);
                txt_carga_metatags.Text = "";
            }
            catch (Exception ex) {
                devNotificaciones.error("Guardar meta tag: " + metatag, ex);
                
                materializeCSS.crear_toast(up_carga_metatags, "Error al guardar meta tag", false);
            }
            up_carga_metatags.Update();
        } else {
                materializeCSS.crear_toast(up_carga_metatags, "Debes ingresar un texto", false);
            }
        }
    }
}

