<%@ Page Language="C#" AutoEventWireup="true"   Async="true" CodeFile="iniciar-sesion.aspx.cs" MasterPageFile="~/general.master" Inherits="iniciar_sesion" %>

<asp:Content ContentPlaceHolderID="contenido" runat="Server">
    <div class="container">
       
        <div class="row">
            <div class="col s12 m1 l3 xl4 hide-on-small-only"></div>
            <div class="col s12 m10 l5 xl4">
                <div class="row center">
                    <img src="/img/webUI/incom_retail_logo_header.png" style="max-height: 3.5rem;" class="responsive-img">
                    <h1 class="">Iniciar sesión</h1>
                </div>
                <div class="row center-align">
                    <p><strong>Inica sesion con tu cuenta de Google</strong></p>
                    <div class="g-signin2" style="display: inline-block;" data-longtitle="true" data-onsuccess="onSignIn"></div>
                    <div id="ajax-login-msg-result" class="col s12 ">
                        <p></p>
                    </div>
                </div>
                <div class="row">
                    <hr />
                    <p class="center-align"><strong>O ingresa tus datos</strong></p>
                    <div class="w350 centerDiv">
                        <label for="txt_email">Email</label>
                        <asp:TextBox ID="txt_email" ClientIDMode="Static"  runat="server"></asp:TextBox>

                    </div>
                    <div class="w350 centerDiv">
                        <label for="txt_password">Contraseña</label>
                        <asp:TextBox ID="txt_password" ClientIDMode="Static" TextMode="Password" runat="server"></asp:TextBox>
                       
                    </div>
                </div>
            </div>

            <div class=" center-align  col s12 m6 l12">

                <asp:Button ID="btn_iniciar_sesion" ClientIDMode="Static" OnClick="btn_iniciar_sesion_Click" Style="width: 100%,"
                    class="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5" Text="Iniciar Sesión" runat="server"></asp:Button>
                <p><asp:Label ID="lbl_msg" runat="server" Visible="false" ></asp:Label></p>
                <br />
                <br />
                <strong><a href="restablecer-password.aspx">Olvidé mi contraseña »</a> </strong>
            </div>

            <div class="row">
                <div class="center-align col s12 m12 l12">
                    <br />
                    También puedes   <a href="registro-de-usuario.aspx">«Crear una cuenta»</a>
                </div>
            </div>
        </div>
    </div>

      
    <script>
        document.addEventListener("DOMContentLoaded", function (event) {
            btnEnter("#txt_password", "#btn_iniciar_sesion")
        });
       
    </script>
</asp:Content>
