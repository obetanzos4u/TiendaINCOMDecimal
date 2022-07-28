using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

/// <summary>
/// Descripción breve de devNotificaciones
/// </summary>
public class devNotificaciones
{
   
    static string destinatarios = "cmiranda@it4u.com.mx";
    static string appPath = HostingEnvironment.ApplicationPhysicalPath;

    private static SmtpClient smtp()
    {

        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = true;
        NetworkCredential emailAcceso =  new NetworkCredential("cmiranda@it4u.com.mx", "lydwgdtzufoshycw"); // Credenciales de Usuario ktkbvtbumplmjaov
        smtp.Credentials = emailAcceso;

        smtp.Port = 587;
        return smtp;
    }
    public static async void notificacionSimpleAsync(string mensaje  )
    {
        MailMessage mm = new MailMessage(new MailAddress("development@incom.mx", "[Dev Notificación]"), new MailAddress(destinatarios));

        mm.Subject = "[Dev Notificación] " + DateTime.Now;
        mm.IsBodyHtml = true;
        mm.Body = "<span style=\"font-family: 'Segoe UI light', Verdana, sans-serif; font-size:22px\"> Usuario: " + HttpContext.Current.User.Identity.Name.Replace("@incom.mx", "") + "</span>   <br> <br>  Detalles del mensaje:  <br><br>" + mensaje;


        SmtpClient enviar = smtp();
          enviar.SendAsync(mm, null);
        enviar.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

        

    }

    public static void notificacionSimple(string mensaje)
    {
        MailMessage mm = new MailMessage(new MailAddress("development@incom.mx", "[Dev Notificación]"), new MailAddress(destinatarios));
        
            mm.Subject = "[Dev Notificación] " + DateTime.Now;
            mm.IsBodyHtml = true;
            mm.Body = "<span style=\"font-family: 'Segoe UI light', Verdana, sans-serif; font-size:22px\"> Usuario: " 
            + "</span>   <br> <br>  Detalles del mensaje:  <br><br>" + mensaje;


            SmtpClient enviar = smtp();
            enviar.Send(mm);
 
            
    }
    private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e) {

        try
        {
            StreamWriter fileWriter = new StreamWriter(appPath + @"/logErrores/listaCorreos.txt", true, System.Text.Encoding.UTF8);

            Exception ex = (Exception)e.UserState;

            if (e.Cancelled)
            {
                fileWriter.Write(e.Cancelled);
            }
            if (e.Error != null)
            {
                fileWriter.Write("[{0}] {1}", ex.ToString(), e.Error.ToString());

            }
            else
            {

            }
            fileWriter.Close();
        }
        catch (Exception ex) {

        };
        }
    ///<summary>
    /// Envía un email notificando el error
    ///</summary>
    public static async Task error(string asunto, Exception ex)
    {
        MailMessage mm = new MailMessage(new MailAddress("development@incom.mx", "Tienda Incom"), new MailAddress(destinatarios));
        
            mm.Subject = "[Error][Tienda] - " + asunto;
            mm.IsBodyHtml = true;
            mm.Body = "<span style=\"font-family: 'Segoe UI light', Verdana, sans-serif; font-size:22px\"> Usuario: " + HttpContext.Current.User.Identity.Name.Replace("@incom.mx", "") + "</span>   <br> <br>  Detalles del error:  <br><br>" + ex.ToString();

            object userState = "test: ";
            SmtpClient enviar = smtp();
             enviar.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
            enviar.SendCompleted += (s, e) => {enviar.Dispose(); mm.Dispose();  };
            enviar.SendAsync(mm, null);

        
    }
    ///<summary>
    /// Envía un email notificando el error
    ///</summary>
    public static async Task error(string asunto, Exception ex, string usuario)
    {
        MailMessage mm = new MailMessage(new MailAddress("development@incom.mx", "Tienda Incom"), new MailAddress(destinatarios));

        mm.Subject = "[Error][Tienda] - " + asunto;
        mm.IsBodyHtml = true;
        mm.Body = "<span style=\"font-family: 'Segoe UI light', Verdana, sans-serif; font-size:22px\"> Usuario: " + usuario + "</span>   <br> <br>  Detalles del error:  <br><br>" + ex.ToString();

        object userState = "test: ";
        SmtpClient enviar = smtp();
        enviar.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
        enviar.SendCompleted += (s, e) => { enviar.Dispose(); mm.Dispose(); };
        enviar.SendAsync(mm, null);


    }

    ///<summary>
    /// Guarda un registro de error en la tabla [_errores]
    /// </summary>
    public static void ErrorSQL(string asunto, Exception ex, string valores)
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;
      
            using (con)
            {
                string query = " INSERT INTO _errores (fecha, idUsuario, asunto, valores, error) VALUES (@fecha, @idUsuario, @asunto, @valores, @error);";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@fecha", SqlDbType.DateTime);
                cmd.Parameters["@fecha"].Value = utilidad_fechas.obtenerCentral();

                cmd.Parameters.Add("@idUsuario", SqlDbType.Int);

            try
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                    cmd.Parameters["@idUsuario"].Value = usuarios.userLogin().id;
                else

                    cmd.Parameters["@idUsuario"].Value = -1;
                 
            }
            catch (Exception exx)
            {
                cmd.Parameters["@idUsuario"].Value = DBNull.Value;
            }

            cmd.Parameters.Add("@valores", SqlDbType.NVarChar, 2000);
            if (string.IsNullOrWhiteSpace(valores)) cmd.Parameters["@valores"].Value = DBNull.Value; else cmd.Parameters["@valores"].Value = valores;

            cmd.Parameters.Add("@asunto", SqlDbType.NVarChar, 2000);
            if (string.IsNullOrWhiteSpace(asunto)) cmd.Parameters["@asunto"].Value = DBNull.Value; else cmd.Parameters["@asunto"].Value = asunto;


            cmd.Parameters.Add("@error", SqlDbType.NVarChar, 4000);
                cmd.Parameters["@error"].Value = ex.ToString();

            try { 
               
                 con.Open(); cmd.ExecuteNonQuery();  
            }
            catch (Exception x)
            {
                var xx = x;
            }
            
        }
     
      


    }


    /// <summary>
    /// En lista en el cuerpo del correo parámetros de utilidad, por ejemplo: los datos que no fueron guardados.
    /// </summary>
    public static void error(string asunto, Exception ex, Dictionary<string, string> valores)
    {
        using (MailMessage mm = new MailMessage(new MailAddress("development@incom.mx", "Tienda Incom"), new MailAddress("cmiranda@it4u.com.mx")))
        {
            mm.Subject = "[Error][Tienda] -  " + asunto;
            mm.IsBodyHtml = true;

            StringBuilder sb_valores = new StringBuilder();
            foreach (KeyValuePair<string, string> entry in valores)
            {
                sb_valores.Append("<strong>" + entry.Key + "</strong> : " + entry.Value + "<br>");
            }
            mm.Body = "<span style=\"font-family: 'Segoe UI light', Verdana, sans-serif; font-size:22px\"> Usuario: "
                + HttpContext.Current.User.Identity.Name.Replace("@incom.mx", "") + "</span> " + sb_valores.ToString()
                + "<br> <br>  Detalles del error:  <br><br>" + ex.ToString();


            SmtpClient enviar = smtp();
            enviar.Send(mm);
        }
    }
    public static void error(string asunto, string error)
    {
        MailMessage mm = new MailMessage(new MailAddress("development@incom.mx", "Tienda Incom"), new MailAddress(destinatarios));
        
            mm.Subject = "[Error][Tienda] - " + asunto;
            mm.IsBodyHtml = true;
            mm.Body = "<span style=\"font-family: 'Segoe UI light', Verdana, sans-serif; font-size:22px\"> Usuario: " + HttpContext.Current.User.Identity.Name.Replace("@incom.mx", "") + "</span>   <br> <br>  Detalles del error:  <br><br>" + error;


            SmtpClient enviar =  smtp();
            enviar.Send(mm);
            

        }
    public static void error(string asunto, string error, string usuario)
    {
        MailMessage mm = new MailMessage(new MailAddress("development@incom.mx", "Tienda Incom"), new MailAddress(destinatarios));

        mm.Subject = "[Error][Tienda] - " + asunto;
        mm.IsBodyHtml = true;
        mm.Body = "<span style=\"font-family: 'Segoe UI light', Verdana, sans-serif; font-size:22px\"> Usuario: " + usuario + "</span>   <br> <br>  Detalles del error:  <br><br>" + error;


        SmtpClient enviar = smtp();
        enviar.Send(mm);


    }
}