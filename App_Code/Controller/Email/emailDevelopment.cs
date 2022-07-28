﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Descripción breve de email_Cotizaciones
/// </summary>
public class emailDevelopment: email {

    
    public emailDevelopment(string _asunto, string _destinatarios, string _mensaje, string _remitente) : base(_asunto, _destinatarios, _mensaje, _remitente) {
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

    public  void enviarRegistroBienvenida() {


        mm = new MailMessage();

        if (validarDestinatarios()) {
            depurarDestinatarios();


            mm.From = new MailAddress(remitenteCredenciales, "Incom Retail");
                mm.Subject = asunto + " " + utilidad_fechas.obtenerCentral().ToString("f");
                mm.IsBodyHtml = true;
                mm.Body = mensaje;
                mm.Bcc.Add("development@incom.mx");
                mm.Bcc.Add("desarrollo@incom.mx, telemarketing@incom.mx");
               mm.ReplyToList.Add("telemarketing@incom.mx, development@incom.mx, repreza@incom.mx");
            SmtpClient enviar = smtp();

              enviar.SendAsync(mm,null);

            resultado = true;
            resultadoMensaje = "Solicitud procesada";
            } else {

            resultado = false;
            resultadoMensaje = "Destinatario(s) no válido(s)";
            }

            }

    public  void enviarReporteFacturacionPedidos() {


        mm = new MailMessage();

        if (validarDestinatarios()) {
            depurarDestinatarios();

            //mm.Headers.Add("", "");
            mm.From = new MailAddress(remitenteCredencialesDevelopment, "MKT Development");
            mm.Subject = "[Reporte Facturación & Pedidos]"+asunto + " " + utilidad_fechas.obtenerCentral().ToString("f");
            mm.IsBodyHtml = true;
            mm.Body = mensaje;
            mm.Bcc.Add("cmiranda@it4u.com.mx");
          //  mm.Bcc.Add("telemarketing@incom.mx");

            SmtpClient enviar = smtpDevelopment();
            enviar.SendAsync(mm, null);

            resultado = true;
            resultadoMensaje = "Solicitud procesada";
            } else {
            resultado = false;
            resultadoMensaje = "Destinatario(s) no válido(s)";
            }

        }
    public void general() {
        mm = new MailMessage();

        if (validarDestinatarios()) {
            depurarDestinatarios();

           // mm.Headers.Add("", "");
            mm.From = new MailAddress(remitenteCredenciales, "Incom Retail");
            mm.Subject = asunto + " " + utilidad_fechas.obtenerCentral().ToString("f");
            mm.IsBodyHtml = true;
            mm.Body = mensaje;
            mm.Bcc.Add("development@incom.mx");
            mm.Bcc.Add("telemarketing@incom.mx");
          

            SmtpClient enviar = smtp();
            enviar.SendAsync(mm, null);

            resultado = true;
            resultadoMensaje = "Solicitud procesada";

            } else {
            resultado = false;
            resultadoMensaje = "Destinatario(s) no válido(s)";
            }

        }
    public void restablecerPassword() {


        mm = new MailMessage();

        if (validarDestinatarios()) {
            depurarDestinatarios();

            mm.From = new MailAddress(remitenteCredenciales, "Incom Retail");
            mm.Subject = asunto + " " + utilidad_fechas.obtenerCentral().ToString("f");
            mm.IsBodyHtml = true;
            mm.Body = mensaje;
            mm.Bcc.Add("development@incom.mx");

            SmtpClient enviar = smtp();
            enviar.SendAsync(mm, null);

            resultado = true;
            resultadoMensaje = "Solicitud procesada";
            } else {
            resultado = false;
            resultadoMensaje = "Error el enviar email de restablecimiento";
            }

        }
    }