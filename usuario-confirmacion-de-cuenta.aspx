<%@ Page Language="C#" AutoEventWireup="true" Async="true"  enableEventValidation ="false"
    CodeFile="usuario-confirmacion-de-cuenta.aspx.cs" MasterPageFile="~/general.master" 
    Inherits="usuario_confirmacion_de_cuenta" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <asp:Panel  Visible="false" id="Content_Activacion_Correcta" class="container center-align" runat="server">
        <asp:HiddenField Id="hf_usuario" runat="server"></asp:HiddenField>
        <div class="row">
            <i class="large material-icons">check</i>
            
        </div>
        <div class="row">
            <h1>Activación Correcta</h1>
        </div>
        <div class="row">
            <div class="s12 m6 l6">
                <h2>Gracias por activar tu cuenta</h2>
            </div>
             <div class="s12 m6 l6">
                 <a href="#" onclick="LoginAjaxOpenModal();"  class="btn blue-grey lighten-5 blue-grey-text text-darken-4">Iniciar sesión</a>
             </div>
        </div>
    </asp:Panel>
      <asp:Panel id="Content_Error_Activar" Visible="false" class="container center-align" runat="server">

           <div class="row">
            <i class="large material-icons">error</i>
            
        </div>
        <div class="row">
            <h1 id="Title_Error" runat="server">Error al activar tu cuenta</h1>
        </div>
        <div class="row">
            <div class="s12 m6 l6">
                <h2 id="msg_detalle_error" runat="server"></h2>
            </div>
             <div class="s12 m6 l6">
                 <asp:LinkButton id="btn_generar_nueva_liga" OnClick="btn_generar_nueva_liga_Click" class="btn blue" Visible="false" runat="server">Reenviar email</asp:LinkButton>

             </div>
        </div>


      </asp:Panel>
</asp:Content>
