using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Descripción breve de QuejasSugerencias_Incom
/// </summary>
public class QuejasSugerencias_Incom
{
    public QuejasSugerencias_Incom()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    static public void guardarComentario(quejas comentario)
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con)
        {
            cmd.Parameters.Add("@idUsuario", SqlDbType.Int);

            if (comentario.idUsuario == null) cmd.Parameters["@idUsuario"].Value = DBNull.Value;
            else cmd.Parameters["@idUsuario"].Value = comentario.idUsuario;

            cmd.Parameters.Add("@tipoComentario", SqlDbType.NVarChar, 30);
            cmd.Parameters["@tipoComentario"].Value = comentario.tipoComentario;

            cmd.Parameters.Add("@modoComentario", SqlDbType.Bit);
            cmd.Parameters["@modoComentario"].Value = comentario.modoComentario;

            cmd.Parameters.Add("@comentario", SqlDbType.NVarChar, 1500);
            cmd.Parameters["@comentario"].Value = comentario.comentario;

            cmd.Parameters.Add("@fecha", SqlDbType.DateTime);
            cmd.Parameters["@fecha"].Value = comentario.fecha;

            cmd.Parameters.Add("@direccion_ip", SqlDbType.NVarChar, 30);
            cmd.Parameters["@direccion_ip"].Value = comentario.direccion_ip;


            cmd.CommandType = CommandType.Text;

            cmd.CommandText = @"INSERT INTO quejas_incom( idUsuario, tipoComentario,modoComentario, comentario, fecha, direccion_ip)
                                VALUES(@idUsuario, @tipoComentario, @modoComentario, @comentario, @fecha, @direccion_ip)";

            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
public class quejas
{
    public int id { get; set; }
    public int? idUsuario { get; set; }
    public string tipoComentario { get; set; }
    public int modoComentario { get; set; }
    public string comentario { get; set; }
    public DateTime fecha { get; set; }

    public string direccion_ip { get; set; }

    public void email_quejasIncom()
    {
        string name;
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            usuarios datosUsuario = usuarios.recuperar_DatosUsuario(HttpContext.Current.User.Identity.Name);
            name = datosUsuario.nombre + "  " + datosUsuario.apellido_paterno;
        }
        else
        {
            name = "ANÓNIMO";
        }

        string filePath = "/email_templates/quejas/comentarios_quejas_incom.html";

        Dictionary<string, string> datos = new Dictionary<string, string>
        {
            { "{fecha}", utilidad_fechas.DDMMAAAA() },
            { "{tipoComentario}", tipoComentario.ToString() },
            { "{nombre}", name.ToString() },
            { "{comentario}", comentario.ToString() },
            { "{fechaComentario}", fecha.ToString() }
        };

        string mensaje = archivosManejador.reemplazarEnArchivo(filePath, datos);

        using (MailMessage mm = new MailMessage(new MailAddress("serviciosweb@incom.mx", "INCOM.MX [BUZÓN WEB]"), new MailAddress("fgarcia@incom.mx")))
        {
            mm.Subject = "Quejas y sugerencias INCOM.MX" + " " + utilidad_fechas.obtenerCentral().ToString("d");
            mm.IsBodyHtml = true;
            mm.Body = mensaje;

            mm.Bcc.Add("fgarcia@incom.mx");
            mm.Bcc.Add("ralbert@incom.mx");
            mm.Bcc.Add("adominguez@incom.mx");
            mm.Bcc.Add("ppadron@iquo.mx");
            mm.Bcc.Add("serviciosweb@incom.mx");

            SmtpClient enviar = conexiones.smtp();
            enviar.Send(mm);
        }
    }
}