<%@ Page Language="C#" AutoEventWireup="true"   Async="true" CodeFile="restablecer-password.aspx.cs" MasterPageFile="~/general.master" Inherits="iniciar_sesion" %>

<asp:Content ContentPlaceHolderID="contenido" runat="Server">
    <div class="container">
        <asp:HiddenField ID="hf_restablecer" runat="server" />
        <div class="row">
            <div class="col s12 m1 l3 xl4 hide-on-small-only"></div>
            <div class="col s12 m10 l5 xl4">
                <div class="row center">
                    <img src="/img/webUI/incom_retail_logo_header.png" style="max-height: 3.5rem;" class="responsive-img">
                    <h1 class="">Restablecer Contraseña</h1>
                    <h3 class="">Ingresa tu usuario (email con el que te registraste)</h3>
                </div>
                <div class="row">
                    <div class="input-field col s12 m12 l12">
                        <asp:TextBox ID="txt_email" ClientIDMode="Static" runat="server"></asp:TextBox>
                        <label class="active" for="txt_email">Email</label>
                    </div>
 <div class="input-field col s12 m12 l12"><p class="center-align">Se te enviará un email con las instrucciones para restablecer tu contraseña,
     revisa tu carpeta de correos no deseados si no encuentras el email en bandeja de entrada.
     </p>
 </div>
                </div>
            </div>

            <div class=" center-align  col s12 m6 l12">
                <asp:UpdatePanel runat="server" RenderMode="Inline">
                    <ContentTemplate>
                        <asp:LinkButton ID="btn_restablecer_password" OnClientClick="btnLoading(this);" OnClick="btn_restablecer_password_Click" Style="width: 100%,"
                            class="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5"
                            Text="Restablecer" runat="server"></asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>



            </div>
                 
           
        
      </div>
    </div>
 
        <script type="text/javascript">
        $(document).ready(function () {
            Materialize.updateTextFields();
            });

        </script>
</asp:Content>
