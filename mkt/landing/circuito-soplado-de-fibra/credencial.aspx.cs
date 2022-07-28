using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class mkt_circuito_soplado_de_fibra_credencial : System.Web.UI.Page {

    private string connection = System.Configuration.ConfigurationManager.ConnectionStrings["mkt_eventos"].ToString();

    protected void Page_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            ObtenerDatos();
        }
    }

    [Obsolete]
    protected void ObtenerDatos() {

    

        if (Request.QueryString["id"] == null) {
            msg_result.InnerText = "No se recibieron datos válidos.";
            msg_result.Visible = true;
            credencial.Visible = false;
            return; 
        }
        if (Request.QueryString["email"] == null) {
            msg_result.InnerText = "No se recibieron datos válidos.";
            msg_result.Visible = true;
            credencial.Visible = false;
            return; 
        }

        string email = Request.QueryString["email"];
        string id = Request["id"];

        DataTable dtDatos = new DataTable();
        try {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(connection);
            cmd.Connection = con;

            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("Select * From EventosAsistentes WHERE  id_AsistenteRegistro = @id AND Email = @email ");


                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;


                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = int.Parse(id);


                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 100);
                cmd.Parameters["@email"].Value = email;

             

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(ds);

                  dtDatos = ds.Tables[0];
                con.Close();
            }
                if(dtDatos.Rows.Count == 0) {
                    msg_result.InnerText = "No se ha encontrado un registro";
                    msg_result.Visible = true;
                    credencial.Visible = false;
                    return;
                }

                string[] Nombre = dtDatos.Rows[0]["Nombre"].ToString().Split(' ');
                string[] Apellidos = dtDatos.Rows[0]["Apellidos"].ToString().Split(' ');
                string Compañia = dtDatos.Rows[0]["Compañia"].ToString();

            if (Compañia.Length > 42) Compañia = Compañia.Substring(0, 42);

                lt_Nombre.Text = Nombre[0];
                lt_Apellidos.Text = Apellidos[0];
                lt_Empresa.Text = Compañia;

               ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "  document.addEventListener('DOMContentLoaded', () => {     generatePDF();   });", true);





        }
        catch (Exception ex) {
            msg_result.InnerText = "Ocurrió un error, contacta a tu asesor";
            msg_result.Visible = true;
            credencial.Visible = false;

        }
    }


}