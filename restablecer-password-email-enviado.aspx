<%@ Page Language="C#" AutoEventWireup="true" Async="true" 
    CodeFile="restablecer-password-email-enviado.aspx.cs" MasterPageFile="~/general.master" Inherits="restablecer_password_email_enviado" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <div class="container center-align">
        <div class="row">
            <i class="large material-icons">check</i>

        </div>
        <div class="row">
            <h1>Se ha enviado un email con las instrucciones</h1>
        </div>
        <div class="row">
            <div class="s12 m6 l6">
                <h2>Se te enviará un email con las instrucciones para restablecer tu contraseña, revisa tu carpeta de correos no deseados 
            si no encuentras el email en bandeja de entrada.
                </h2>
            </div>
             <div class="s12 m6 l6">
                 <a href="<%=Request.Url.GetLeftPart(UriPartial.Authority)  %>" class="btn blue-grey lighten-5 blue-grey-text text-darken-4">Regresar al Inicio</a>
             </div>
        </div>

    </div>
</asp:Content>
