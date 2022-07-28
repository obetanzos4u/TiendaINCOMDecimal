using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


/// <summary>
/// Descripción breve de cotizaciones
/// </summary>
public class clientes {
    private SqlConnection con { get; set; }
    private SqlCommand cmd { get; set; }
    private SqlDataAdapter da { get; set; }
    private DataSet ds { get; set; }
    private DataTable dt { get; set; }

    protected void dbConexion()
    {

        con = new SqlConnection(conexiones.conexionTienda());
        cmd = new SqlCommand();
        cmd.Connection = con;

    }

    public clientes() {
        // 
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public static DataTable obtenerClientesMin(model_usuarios asesor, string termino)
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;
        using (con)
        {
            StringBuilder sel = new StringBuilder();

            sel.Append(@" SELECT id, nombre, apellido_paterno, email, asesor_base, grupo_asesor, asesor_adicional, grupo_asesores_adicional, grupoPrivacidad, grupo_usuario FROM usuarios WHERE nombre LIKE '%'+@termino+'%' ");
            sel.Append(@" OR apellido_paterno LIKE '%'+@termino+'%' ");
            sel.Append(@" OR email LIKE '%'+@termino+'%' ");

            string query = sel.ToString(); ;
            cmd.CommandText = query;

            cmd.CommandType = CommandType.Text;

            /*  cmd.Parameters.Add("@usuario", SqlDbType.NVarChar, 60);
              cmd.Parameters["@usuario"].Value = asesor.email;

              cmd.Parameters.Add("@grupoAsesor", SqlDbType.NVarChar, 60);
              cmd.Parameters["@grupoAsesor"].Value = grupoAsesor;
               */
            cmd.Parameters.Add("@termino", SqlDbType.NVarChar, 60);
            cmd.Parameters["@termino"].Value = textTools.lineSimple(termino);
           
            con.Open();

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
         
            da.Fill(ds);


            return privacidadAsesores.validarListadoClientes(ds.Tables[0], asesor);
            }


    }
   
    }