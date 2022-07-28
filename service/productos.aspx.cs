using System;
using System.Collections.Generic;
 
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class service_productos : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        productoExistente();

    }

    /// <summary>
    /// Devuelve [true] o [false] si existe un producto en la tienda.
    /// </summary>
    public  void productoExistente() {

        if (Request.QueryString["numero_parte"] != null && Request.QueryString["numero_parte"].Length >= 3) {

            string numero_parte = Request.QueryString["numero_parte"].ToString();

           
            try {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conexiones.conexionTienda());
                cmd.Connection = con;

                using (con) {

               


                    cmd.CommandText = "SET LANGUAGE English; SELECT COUNT(*) FROM productos_datos WHERE numero_parte = @numero_parte ";
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@numero_parte", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters["@numero_parte"].Value = numero_parte;

                    con.Open();

                    int resultado = int.Parse( cmd.ExecuteScalar().ToString());
                    if (resultado == 1) Response.Write("true"); else  Response.Write("false");


                }

            }
            catch (Exception ex) {

                  Response.Write("false");
            }

        } else {
           
            Response.Write("false");
        } 
    }
}