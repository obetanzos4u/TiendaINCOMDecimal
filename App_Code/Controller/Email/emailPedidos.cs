using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Descripción breve de email_Cotizaciones
/// </summary>
public class emailPedidos: email {
    public emailPedidos(string _asunto, string _destinatarios, string _mensaje, string _remitente) : base(_asunto, _destinatarios, _mensaje, _remitente) {
        }
    public emailPedidos(string _asunto, string _destinatarios, string _mensaje, string _remitente, List<Attachment> _attachment) : base(_asunto, _destinatarios, _mensaje, _remitente, _attachment) {
        }
    private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e) {

        // StreamWriter fileWriter = new StreamWriter(appPath + @"/logErrores/listaCorreos.txt", true, System.Text.Encoding.UTF8);

     //   Exception ex = (Exception)e.UserState;

        if (e.Cancelled) {
            //   fileWriter.Write(e.Cancelled);
            }
        if (e.Error != null) {

            devNotificaciones.error("Enviar Email Notificaciones", e.Error);
            //   fileWriter.Write("[{0}] {1}", ex.ToString(), e.Error.ToString());

            } else {

            }
        // fileWriter.Close();
        }

    public async void enviar() {

        mm = new MailMessage();

        if (validarDestinatarios()) {
            depurarDestinatarios();


            mm.From = new MailAddress(remitenteCredenciales, "Incom Retail");
            mm.Subject = "Pedido, "+ asunto + " " + utilidad_fechas.obtenerCentral().ToString("dddd, dd MMMM yyyy");
            mm.IsBodyHtml = true;
            mm.Body = mensaje;
            mm.Bcc.Add("development@incom.mx");
            mm.To.Add("telemarketing@incom.mx");
            mm.ReplyToList.Add(destinatarios + ", " + usuarios.userLoginName() +
                ", development@incom.mx");

            if (attachment != null ) {
                foreach(Attachment att in attachment) {
                    mm.Attachments.Add(att);
                    }
                }

            SmtpClient enviar = smtp();

            enviar.SendAsync(mm,null);

            resultado = true;
            resultadoMensaje = "Solicitud procesada";
            } else {

            resultado = false;
            resultadoMensaje = "Destinatario(s) no válido(s)";
            }
      
        }
    
       
    }