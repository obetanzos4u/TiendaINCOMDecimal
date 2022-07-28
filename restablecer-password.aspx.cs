﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class iniciar_sesion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            Page.Title = "Restablecer contraseña";
            if (HttpContext.Current.User.Identity.IsAuthenticated) {
                Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)+ "/usuario/mi-cuenta/mi-cuenta.aspx");
                }
            }
    }

    protected void btn_restablecer_password_Click(object sender, EventArgs e)
    {
        // Validamos que los campos sean correctos (solo email)
        if (validadCampos())
        {


            usuarios validar = new usuarios();
            // Verificamos que el usuario que quiere restablecer sea un usuario ya registrado
            byte validarExistencia = validar.validar_Existencia_Usuario(txt_email.Text);


            // Si encontró 1 registro procede a restablecer
            if (validarExistencia == 1)
            {
                // Generamos una cadena aleatorio
                string codigo_validacion = Path.GetRandomFileName();

                

                // Ciframos
                codigo_validacion = seguridad.Encriptar(codigo_validacion).Replace("/", "");
               string emailCifrado = seguridad.Encriptar(txt_email.Text);

               bool resultado = usuarios.restablecimiento_contraseña(txt_email.Text, emailCifrado, codigo_validacion);

                // Función de envío de email

                string asunto = "[Restablecimiento de contraseña] de " + txt_email.Text + " " + utilidad_fechas.obtenerCentral();
                string mensaje = string.Empty;
                string linkRestablecimiento = Request.Url.GetLeftPart(UriPartial.Authority) + "/restablecer-password-establecer.aspx?codigo=" + codigo_validacion+"&usuario="+emailCifrado;
                string filePathHTML = "/email_templates/ui/usuario_RestablecerPassword.html";


                Dictionary<string, string> datosDiccRemplazo = new Dictionary<string, string>();
                datosDiccRemplazo.Add("{link_restablecimiento}", linkRestablecimiento);

                mensaje = archivosManejador.reemplazarEnArchivo(filePathHTML, datosDiccRemplazo);

                emailTienda email = new emailTienda(asunto, txt_email.Text, mensaje, "retail@incom.mx");

                email.restablecerPassword();


                if (resultado == true && email.resultado == true) {


                   
          

                    ScriptManager.RegisterStartupScript(this, typeof(Control), "redirect",
                        "location.href = '"+Request.Url.GetLeftPart(UriPartial.Authority) +"/restablecer-password-email-enviado.aspx"+"'", true);

                    } else
                    materializeCSS.crear_toast(this, "Error al restablecer password", false);


                } else
            {
                materializeCSS.crear_toast(this, "No existe el usuario",false);
            }

         
            // Server.Transfer("~/inicio.aspx");
        }
    }
    
    bool validadCampos()
    {
        
        if (txt_email.Text.Length < 2 || txt_email.Text.Length > 60) { materializeCSS.crear_toast(this, "El campo email no cumple con los requerimientos de longitud.", false); return false; };
        if(textTools.validarEmail(txt_email.Text) == false) {materializeCSS.crear_toast(this, "El campo email no tiene el formato correcto", false); return false;}
       
     

        return true;
    }
}