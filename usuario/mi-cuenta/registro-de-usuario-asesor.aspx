<%@ Page Language="C#" AutoEventWireup="true"   Async="true" CodeFile="registro-de-usuario-asesor.aspx.cs" MasterPageFile="~/gnCliente.master" Inherits="usuario_mi_cuenta" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <input type="text" id="username" style="width:0;height:0;visibility:hidden;position:absolute;left:0;top:0" />
<input type="password" style="width:0;height:0;visibility:hidden;position:absolute;left:0;top:0" />

    <div class="container z-depth-3">
        <div class="row">
            <div class="col l12">
                <h1 class="center-align">Registro de usuario</h1>

            </div>
        </div>
 
         <div class="row">
             <div class="input-field col s12 m6 l6">
                    <asp:TextBox ID="txt_id_cliente" AutoCompleteType="None" Text=" "
                        autocomplete="off" runat="server"></asp:TextBox>
                    <label for="txt_email">ID Cliente</label>
                </div>
        </div>
         <div class="row">
        <div class="input-field col s12 m6 l6">
            <asp:TextBox ID="txt_email" ClientIDMode="Static" runat="server"></asp:TextBox>
            <label for="txt_email">Email</label>
        </div>

        <div class="input-field col s12 m6 l6">
            <asp:TextBox ID="txt_nombre" ClientIDMode="Static"    autocomplete="off" runat="server"></asp:TextBox>
            <label for="txt_nombre">Nombre(s)</label>
        </div>

    </div>
    <div class="row">
        <div class="input-field col s12 m6 l6">
            <asp:TextBox ID="txt_apellido_paterno" ClientIDMode="Static" runat="server"></asp:TextBox>
            <label for="<%= txt_apellido_paterno.ClientID%>">Apellido Paterno</label>
        </div>
          <div class="input-field col s12 m6 l6">
            <asp:TextBox ID="txt_apellido_materno" ClientIDMode="Static" runat="server"></asp:TextBox>
            <label for="<%= txt_apellido_materno.ClientID%>">Apellido Materno</label>
        </div>
    </div>
    <div class="row">
        <div class="input-field col s12 m6 l6">
            <asp:TextBox ID="txt_password" ClientIDMode="Static" TextMode="Password"    autocomplete="new-password"   runat="server"></asp:TextBox>
            <label for="txt_password">Contraseña</label>
        </div>
                <div class="input-field col s12 m6 l6">
            <asp:TextBox ID="txt_password_confirma" ClientIDMode="Static" TextMode="Password" runat="server"></asp:TextBox>
                    <label for="txt_password_confirma">Confirmar Contraseña</label>
                </div>
        <div class="row">
            <div class=" col s12 m12 l12">
                <a href="/informacion/aviso-de-privacidad.aspx" target="_blank">Aviso de Privacidad</a> y 
                   <a href="/informacion/terminos-y-condiciones-de-compra.aspx" target="_blank">Términos y condiciones de compra</a>
            </div>
            <div class=" col s12 m12 l12"><p>
                    <label>
                <asp:CheckBox ID="chk_politica_privacidad"   runat="server" />
                       <span>"Acepto los términos y condiciones de compra y aviso de privacidad</span> </label></p>
            </div>
        </div>
        <div class="row">
            <div class="input-field col s12 m6 l6">
                <asp:LinkButton ID="btn_registrar" OnClick="btn_registrar_Click" OnClientClick="btnLoading(this);"
                    class="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5" runat="server">Registrar</asp:LinkButton>

            </div>
        </div>
    </div>
       </div>

</asp:Content>
