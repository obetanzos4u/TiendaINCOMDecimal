<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeFile="iniciar-sesion.aspx.cs" MasterPageFile="~/general.master" Inherits="iniciar_sesion" %>

<asp:Content ContentPlaceHolderID="contenido" runat="Server">
    <div class="container-inicio_sesion">
        <div class="inicio_sesion-position">
            <div class="inicio_sesion-border">
                <div class="">
                    <div class=""></div>
                    <div class="">
                        <div class="">
                            <h1 class="txt-inicio_sesion" style="font-size: 2rem; text-align: center">Iniciar sesión</h1>
                        </div>
                        <div class="sesion-with_social">
                            <p><strong>Inica sesion con tu cuenta de Google</strong></p>
                            <div class="g-signin2" style="display: inline-block;" data-longtitle="true" data-onsuccess="onSignIn"></div>
                            <div id="ajax-login-msg-result" class="">
                                <p></p>
                            </div>
                        </div>
                        <div class="center-align">
                            <p class="center-align"><strong>O ingresa tus datos</strong></p>
                            <div class="w350 centerDiv">
                                <label for="txt_email">Email</label>
                                <asp:TextBox ID="txt_email" ClientIDMode="Static" runat="server"></asp:TextBox>
                            </div>
                            <div class="w350 centerDiv">
                                <label for="txt_password">Contraseña</label>
                                <asp:TextBox ID="txt_password" ClientIDMode="Static" TextMode="Password" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="center-align">
                        <asp:Button ID="btn_iniciar_sesion" ClientIDMode="Static" OnClick="btn_iniciar_sesion_Click" Style="width: 100%,"
                            class="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5 center-align" Text="Iniciar sesión" runat="server"></asp:Button>
                        <p>
                            <asp:Label ID="lbl_msg" runat="server" Visible="false"></asp:Label>
                        </p>
                        <br />
                        <br />
                        <strong><a href="restablecer-password.aspx">Olvidé mi contraseña »</a></strong>
                    </div>

                    <div class="center-align">
                        <div class="center-align is-bt-3">
                            <br />
                            También puedes   <a href="registro-de-usuario.aspx">«Crear una cuenta»</a>
                        </div>
                    </div>
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
