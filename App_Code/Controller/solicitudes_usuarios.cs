using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// -- Modelo llevara el control de solicitudes de contactos de diferentes medios
/// </summary>
public class solicitudes_usuarios : model_solicitudes_usuarios
    {
    private SqlConnection con { get; set; }
    private SqlCommand cmd { get; set; }
    private SqlDataAdapter da { get; set; }
    private DataSet ds { get; set; }
    private DataTable dt { get; set; }
    
    public bool resultado { get; set; }
    public string mensaje { get; set; }

    protected void dbConexion() {
        con = new SqlConnection(conexiones.conexionTienda());
        cmd = new SqlCommand();
        cmd.Connection = con;

    }

   
 
    public int  contactoSimple() {
       

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;
        using (con) {
            string query = @"SET LANGUAGE English;
                    INSERT INTO solicitudes_usuarios 
                            (fechaSolicitud,  nombre, email, telefono,  registrado, producto, asunto, comentario, activo ) 
                     VALUES (@fechaSolicitud,  @nombre, @email, @telefono,  @registrado, @producto,  @asunto, @comentario, @activo );  SELECT SCOPE_IDENTITY();";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@fechaSolicitud", SqlDbType.DateTime);
            cmd.Parameters["@fechaSolicitud"].Value = utilidad_fechas.obtenerCentral();

            cmd.Parameters.Add("@nombre", SqlDbType.NVarChar, 45);
            cmd.Parameters["@nombre"].Value = nombre;

            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 60);
            cmd.Parameters["@email"].Value = email;
           
            cmd.Parameters.Add("@telefono", SqlDbType.NVarChar, 60);
            cmd.Parameters["@telefono"].Value = telefono;

            cmd.Parameters.Add("@registrado", SqlDbType.Int);
            cmd.Parameters["@registrado"].Value = registrado;

          
            cmd.Parameters.Add("@producto", SqlDbType.NVarChar, 100);
            if (string.IsNullOrEmpty(producto)) cmd.Parameters["@producto"].Value = DBNull.Value;
            else cmd.Parameters["@producto"].Value = producto;

            cmd.Parameters.Add("@asunto", SqlDbType.NVarChar, 50);
            cmd.Parameters["@asunto"].Value = asunto;

            cmd.Parameters.Add("@comentario", SqlDbType.NVarChar, 500);
            cmd.Parameters["@comentario"].Value = comentario;

            cmd.Parameters.Add("@activo", SqlDbType.Int);
            cmd.Parameters["@activo"].Value = activo;

            try {

                con.Open();
                cmd.CommandText = query;
              int idResultado = int.Parse(  cmd.ExecuteScalar().ToString());

              
                mensaje = "Tu solicitud ha sido enviada con éxito";
                resultado = true;
                return idResultado;
            }
            catch (Exception ex) {

                devNotificaciones.error("Insertar registro de contacto " + email, ex);
                mensaje = "El error al procesar solicitud";
                resultado = false;

                return -1;
            } 

            }


        }
    
    }