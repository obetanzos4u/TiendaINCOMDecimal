using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class herramientas_logs_sql : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_fecha_desde.Text = DateTime.Now.AddDays(-1).ToString("yyy-MM-dd");
            txt_fecha_hasta.Text = DateTime.Now.ToString("yyy-MM-dd");
        }
    }
    protected void cargarLogSQL()
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;

            using (con)
            {
                cmd.CommandText = @"SELECT *, usuario = (SELECT email FROM usuarios WHERE id =idUsuario) FROM [_errores]  WHERE [fecha] BETWEEN @fechaDesde AND @fechaHasta ";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@fechaDesde", SqlDbType.DateTime);
                cmd.Parameters.Add("@fechaHasta", SqlDbType.DateTime);

                cmd.Parameters["@fechaDesde"].Value = txt_fecha_desde.Text + " 00:00:00";
                cmd.Parameters["@fechaHasta"].Value = txt_fecha_hasta.Text + " 23:59:59";

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(ds);
                lv_log_sql.DataSource = ds.Tables[0];
                lv_log_sql.DataBind();
            }
        }
        catch (Exception ex)
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.failure, ex.Message);
            //materializeCSS.crear_toast(this, "Error: " + ex.Message, false);
        }
    }
    protected void btn_CargarLog_Click(object sender, EventArgs e)
    {
        cargarLogSQL();
    }
}