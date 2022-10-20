<%@ Page Language="C#" AutoEventWireup="true"   Async="true" CodeFile="restablecer-password.aspx.cs" MasterPageFile="~/general.master" Inherits="iniciar_sesion" %>

<asp:Content ContentPlaceHolderID="contenido" runat="Server">
    <div class="container" style="margin-top: 4rem; margin-bottom: 4rem;">
        <asp:HiddenField ID="hf_restablecer" runat="server"/> 
        <div class="contain-form-recuperar-contrasena is-m-auto" style="width: 540px;">
            <div class="row">
                <div class="row">
                    <h2 class="center-align"><strong>Restablecer contraseña</strong></h2>
                    <h5 class="is-top-3">Solo necesitamos confirmar tu correo electrónico con el que te registraste.</h5>
                </div>
                <div class="row">
                    <div class="input-field" style="width: 70%;">
                        <asp:TextBox ID="txt_email" 
                        style="border-radius: 8px; padding-left: 1rem;" ClientIDMode="Static" runat="server">
                        </asp:TextBox>
                        <label class="active" style="left: 1rem;" for="txt_email">
                        Correo electrónico
                        </label>
                    </div>
                    <div class="input-field">
                        <p class="is-text-xs">Si tienes una cuenta en INCOM.mx, te enviaremos un mensaje. Sigue las instrucciones del correo electrónico para restablecer tu contraseña.
                        </p>
                    </div>
                </div>
                <div class="center-align">
                    <asp:UpdatePanel runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <asp:LinkButton ID="btn_restablecer_password" OnClientClick="btnLoading(this);" OnClick="btn_restablecer_password_Click" Style="width: 100%,"
                                class=""
                                Text="Restablecer" runat="server">
                                <div class="waves-effect waves-light btn-1 is-bg-footer is-text-white margin-btn lighten-5 center-align waves-input-wrapper is-m-auto">Restablecer</div>
                            </asp:LinkButton>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
      </div>
    </div>
 
        <script type="text/javascript">
        $(document).ready(function () {
            Materialize.updateTextFields();
            });

        </script>
</asp:Content>
