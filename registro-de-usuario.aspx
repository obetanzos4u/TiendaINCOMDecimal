<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeFile="registro-de-usuario.aspx.cs" MasterPageFile="~/general.master" Inherits="registro_de_usuario" %>

<asp:Content ContentPlaceHolderID="contenido" runat="Server">
    <div class="is-container">
        <div class="is-flex is-justify-center is-items-center is-w-full">
            <div class="is-w-1_2">
                <div class="contain-form-crear_cuenta">
                    <h2 class="is-font-semibold is-select-none is-px-2">Crear cuenta</h2>
                    <div class="is-px-4 is-py-0">
                        <div class="is-flex is-flex-col is-justify-center is-items-center is-py-2">
                            <p class="is-font-medium is-select-none">Continuar con Google</p>
                            <div class="g-signin2" data-text="sfsf" data-onsuccess="onSignIn"></div>
                            <div id="ajax-register-msg-result"></div>
                        </div>
                        <div class="contain-separator is-top-2">
                            <span class="separator" style="margin-left: 10px;">O</span>
                        </div>
                        <div class="is-flex is-flex-col is-justify-center is-items-center">
                            <p class="is-font-medium is-select-none">Registrarse con el correo electrónico</p>
                            <div class="is-grid is-col-2 is-gap-5">
                                <div>
                                    <label for="txt_email" class="is-text-base is-select-none">Correo electrónico</label>
                                    <asp:TextBox ID="txt_email" ClientIDMode="Static" runat="server"></asp:TextBox>
                                </div>
                                <div>
                                    <label for="txt_phone" class="is-text-base is-select-none">Teléfono</label>
                                    <asp:TextBox ID="txt_phone" ClientIDMode="Static" runat="server"></asp:TextBox>
                                </div>
                                <div>
                                    <div>
                                        <label for="txt_nombre" class="is-text-base is-select-none">Nombre(s)</label>
                                        <asp:TextBox ID="txt_nombre" ClientIDMode="Static" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div>
                                    <div>
                                        <label for="<%= txt_apellido_paterno.ClientID %>" class="is-text-base is-select-none">Apellido</label>
                                        <asp:TextBox ID="txt_apellido_paterno" ClientIDMode="Static" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <%--                                <div>
                                    <div>
                                        <label for="<%= txt_apellido_materno.ClientID %>">Apellido materno</label>
                                        <asp:TextBox ID="txt_apellido_materno" ClientIDMode="Static" runat="server"></asp:TextBox>
                                    </div>
                                </div>--%>
                                <div>
                                    <div>
                                        <label for="txt_password" class="is-text-base is-select-none">Contraseña</label>
                                        <asp:TextBox ID="txt_password" ClientIDMode="Static" TextMode="Password" runat="server"></asp:TextBox>
                                        <%--<span toggle="#txt_password" class="field-icon toggle-password"><span class="material-icons">visibility</span></span>--%>
                                    </div>
                                    <%--                                    <div>
                                        <label for="txt_password">Contraseña</label>
                                        <span toggle="#txt_password">
                                            <span class="material-icons">visibility</span>
                                        </span>
                                        <asp:TextBox ID="txt_password" ClientIDMode="Static" TextMode="Password" runat="server"></asp:TextBox>
                                    </div>--%>
                                </div>
                                <div>
                                    <div>
                                        <label for="txt_password_confirma" class="is-text-base is-select-none">Confirmar contraseña</label>
                                        <asp:TextBox ID="txt_password_confirma" ClientIDMode="Static" TextMode="Password" runat="server"></asp:TextBox>
                                        <%--<span toggle="#txt_password_confirma">
                                            <span class="material-icons">visibility</span>
                                        </span>--%>
                                    </div>
                                </div>
                            </div>
                            <div>
                                <p class="is-p-0 is-m-0 is-text-sm is-font-normal" style="color: rgba(0,0,0,0.4)">Utiliza 8 o más caracteres con una combinación de mayúsculas, minúsculas, números y símbolos.</p>
                            </div>
                            <%--                            <div class="is-w-full is-flex is-flex-col is-justify-center is-items-center is-text-xs is-py-2">
                                <a href="/informacion/aviso-de-privacidad.aspx" target="_blank">Aviso de privacidad</a>
                                <a href="/informacion/terminos-y-condiciones-de-compra.aspx" target="_blank">Términos y condiciones de compra</a>
                            </div>--%>
                            <%--                            <div>
                                <asp:CheckBox ID="chk_politica_privacidad" runat="server" />
                                <span>Acepto los términos y condiciones de compra. Acepto el aviso de privacidad.</span>
                            </div>--%>
                            <div class="is-w-full is-flex is-justify-center is-items-center is-py-4">
                                <label>
                                    <asp:CheckBox ID="chk_politica_privacidad" runat="server" />
                                    <span>Acepto los <a href="/informacion/terminos-y-condiciones-de-compra.aspx" target="_blank">Términos y condiciones de compra</a> y el <a href="/informacion/aviso-de-privacidad.aspx" target="_blank">Aviso de privacidad</a></span>
                                </label>
                            </div>
                            <div class="btn-1">
                                <%-- <asp:LinkButton ID="btn_registrar" OnClick="btn_registrar_ClickAsync" OnClientClick="btnLoading(this);" class="is-text-white is-px-4 is-py-2 is-bg-blue is-rounded" runat="server">Crear cuenta</asp:LinkButton> --%>
                                <asp:LinkButton ID="btn_registrar" OnClick="btn_registrar_ClickAsync" OnClientClick="btnLoading(this);" class="btn-crear_cuenta" runat="server">Crear cuenta</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="https://apis.google.com/js/platform.js?onload=init" async defer></script>

    <script>

        function init() {
            gapi.load('auth2', function () {

            });
            gapi.auth2.init({ client_id: '735817781293-gj8bqpplf0jt9au330hcejcp06js20di.apps.googleusercontent.com' });
        }



        function signOut() {
            if (window.gapi) {
                const auth2 = window.gapi.auth2.getAuthInstance()
                if (auth2 != null) {
                    auth2.signOut();
                }
            }
        }

        async function RegistrarUsuario(id_token) {
            var ResultRegister = await SignWithGoogle(id_token);

            let contentMsg = document.querySelector("#ajax-register-msg-result > p");

            console.log(ResultRegister);
            if (ResultRegister.result) {
                contentMsg.textContent = ResultRegister.message + " - Redireccionando en 3 segundos...";
                contentMsg.className = 'green-text text-darken-2';

                setTimeout(function () {
                    window.location.replace('https://' + window.location.hostname + '/iniciar-sesion.aspx')
                }, 2000);
            } else {
                contentMsg.textContent = ResultRegister.message;
                contentMsg.className = 'red-text text-darken-1';
            }
        }

        function onSignIn(googleUser) {
            var profile = googleUser.getBasicProfile();
            console.log('ID: ' + profile.getId()); // Do not send to your backend! Use an ID token instead.
            console.log('Name: ' + profile.getName());
            console.log('Image URL: ' + profile.getImageUrl());
            console.log('Email: ' + profile.getEmail()); // This is null if the 'email' scope is not present.

            var id_token = googleUser.getAuthResponse().id_token;
            console.log(id_token);
            RegistrarUsuario(id_token);

        }


    </script>
</asp:Content>
