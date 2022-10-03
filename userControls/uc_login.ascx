<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_login.ascx.cs" Inherits="userControls_uc_login" %>


        <asp:UpdatePanel ID="up_container" class="container" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
<asp:HiddenField ID="hf_urlReturn" runat="server" />
<asp:HiddenField ID="hf_idModal" runat="server" />


<div id="content_login_form" class="row" runat="server">
    <div class="col s12 m1 l3 xl4 hide-on-small-only"></div>
    <div class="col s12 m10 l5 xl4">
        <div class="row center">
            <h1>Iniciar sesión</h1>
        </div>
        <div class="row">
            <div class="centerDiv">
                <label for="txt_email">Email</label>
                <asp:TextBox ID="txt_email" ClientIDMode="Static" runat="server"></asp:TextBox>

            </div>
            <div class="centerDiv">
                <label for="txt_password">Contraseña</label>
                <asp:TextBox ID="txt_password" ClientIDMode="Static" TextMode="Password" runat="server"></asp:TextBox>

            </div>
        </div>
    </div>
    <div class=" center-align  col s12 m6 l12">

                <asp:Button ID="btn_iniciar_sesion" OnClick="btn_iniciar_sesion_Click" Style="width: 100%,"
                    class="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5" Text="Iniciar sesión" runat="server"></asp:Button>

        <br />
        <br />
        <strong><a href="/restablecer-password.aspx">Olvidé mi contraseña »</a> </strong>
    </div>

    <div class="row">
        <div class="center-align col s12 m12 l12">
            <br />
            También puedes   <a href="registro-de-usuario.aspx">«Crear una cuenta»</a>
        </div>
    </div>
</div>
                <div id="content_sucess_login" runat="server" visible="false" class="row">
                    <div class="col s12 l12 center">
                        <asp:Label runat="server">Inicio de sesión correcto... Redireccionando en 3 segundos</asp:Label>
                    </div>
                </div>
                <script>

    btnEnter("#<%= txt_password.ClientID %>", "#<%= btn_iniciar_sesion.ClientID %>");


    $(document).ready(function () {
        Materialize.updateTextFields();
    });

                </script>

                            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_iniciar_sesion" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>



