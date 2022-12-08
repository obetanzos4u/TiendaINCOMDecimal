<%@ Page Language="C#" AutoEventWireup="true" Async="true"  enableEventValidation ="false"
    CodeFile="usuario-confirmacion-de-cuenta.aspx.cs" MasterPageFile="~/general.master" 
    Inherits="usuario_confirmacion_de_cuenta" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <asp:Panel  Visible="false" id="Content_Activacion_Correcta" class="container center-align" runat="server">
        <asp:HiddenField Id="hf_usuario" runat="server"></asp:HiddenField>
        <div style="margin: 5vh auto 10vh auto;">
            <div class="row">
                <img style="width: 10%;" src="/img/webUI/newdesign/verificado.png"/>
            </div>
            <div class="row">
                <h1 style="font-size: 2.5rem;">Activación correcta</h1>
            </div>
            <div class="row">
                <div class="s12 m6 l6" style="margin-bottom: 3rem;">
                    <h3>Gracias por activar tu cuenta</h3>
                </div>
                <div class="s12 m6 l6">
                    <a href="#" onclick="LoginAjaxOpenModal();">
                        <div class="btn-1">Iniciar sesión</div>
                    </a>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel id="Content_Error_Activar" Visible="false" class="container center-align" style="margin-top: 10vh; margin-bottom: 15vh;" runat="server">
    <div class="row">
        <img class="" style="width: 10%" src="/img/webUI/newdesign/no_verificado.png" />
    </div>
    <div class="row">
        <h1 id="Title_Error" style="font-size: 2.5rem;" runat="server">Ha ocurrio un error al activar tu cuenta</h1>
    </div>
    <div class="row">
        <div class="s12 m6 l6">
            <h3 id="msg_detalle_error" runat="server"></h3>
        </div>
            <div class="s12 m6 l6">
                <asp:LinkButton id="btn_generar_nueva_liga" OnClick="btn_generar_nueva_liga_Click" class="btn blue" Visible="false" runat="server">Reenviar email</asp:LinkButton>
            </div>
    </div>
    </asp:Panel>
</asp:Content>
