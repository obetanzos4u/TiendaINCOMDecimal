<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeFile="restablecer-password-establecer.aspx.cs" MasterPageFile="~/general.master"
    Inherits="restablecer_password_establecer" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <div class="container">
        <div class="row">
            <h1>Restablecer Password</h1>
        </div>
        <div class="row margin-b-2x">
            <div class="col l12 margin-t-4x">
                <asp:label id="lbl_email_usuario" runat="server"></asp:label>
            </div>
            <div class="col l6 margin-t-4x  ">

                <p><strong>Cambiar contraseña</strong> (Mínimo 6, máximo 20 caracteres)</p>
            </div> </div>
            <div class="row  margin-t-2x margin-b-2x">
                <div class="col s12 m5 l4 xl3 input-field margin-t-4x">
                  
                    <asp:textbox id="txt_password" Enabled="true" data-length="20" autocomplete="new-password" placeholder="Ingresa una nueva contraseña"
                        clientidmode="Static" textmode="Password" runat="server">
                    </asp:textbox>  <label>Password:</label>  <span toggle="#txt_password" class="field-icon toggle-password"><span class="material-icons">visibility</span></span>
                </div>
            </div>
            <div class="row margin-b-2x">
              
                <div class="col s12 m5 l4 xl3 input-field margin-t-4x">
                     
                    <asp:textbox id="txt_password_confirmacion"  textmode="Password" data-length="20" autocomplete="new-password" 
                        clientidmode="Static" Enabled="true"
                        placeholder="Confirma contraseña"
                        runat="server">
                    </asp:textbox> <label>Confirma:</label>
                    <span toggle="#txt_password_confirmacion" class="field-icon toggle-password"><span class="material-icons">visibility</span></span>
                </div>
            </div>
            <div class="col l12 margin-t-4x">
                <asp:linkbutton id="btn_cambiar_password" onclick="btn_cambiar_password_Click"
                    class="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5" runat="server">Cambiar contraseña</asp:linkbutton>
                <br />
                <br />
            </div>
        </div>
    </div>

    <script>
         document.addEventListener("DOMContentLoaded", function (event) {

             $(document).ready(function () {
                 $('input#txt_password, input#txt_password_confirmacion').characterCounter();
             });
         });
     </script>
</asp:Content>
