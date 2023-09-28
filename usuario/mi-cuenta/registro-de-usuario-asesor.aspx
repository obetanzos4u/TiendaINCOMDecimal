<%@ Page Language="C#" AutoEventWireup="true" Async="true"
CodeFile="registro-de-usuario-asesor.aspx.cs"
MasterPageFile="~/gnCliente.master" Inherits="usuario_mi_cuenta" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
  <input
    type="text"
    id="username"
    style="
      width: 0;
      height: 0;
      visibility: hidden;
      position: absolute;
      left: 0;
      top: 0;
    " />
  <input
    type="password"
    style="
      width: 0;
      height: 0;
      visibility: hidden;
      position: absolute;
      left: 0;
      top: 0;
    " />

  <div class="container z-depth-3" style="margin-bottom: 6rem">
    <div
      class="space-container-registro_cliente"
      style="width: 92%; margin: auto">
      <div class="row">
        <div class="col l12">
          <h1 class="center-align" style="font-size: 1.56rem; font-weight: 600">
            Registro de usuario
          </h1>
        </div>
      </div>

      <div class="row" style="margin-bottom: 0">
        <div class="input-field col s12 m6 l6">
          <asp:TextBox
            ID="txt_id_cliente"
            AutoCompleteType="None"
            Text=" "
            autocomplete="off"
            style="height: 2rem; width: 96%"
            runat="server"></asp:TextBox>
          <label for="txt_email" style="padding-left: 1rem">ID Cliente</label>
        </div>
      </div>
      <div class="row" style="margin-bottom: 0">
        <div class="input-field col s12 m6 l6">
          <asp:TextBox
            ID="txt_email"
            ClientIDMode="Static"
            style="width: 94% !important"
            runat="server"></asp:TextBox>
          <label for="txt_email" style="padding-left: 1rem">Email</label>
        </div>
        <div class="input-field col s12 m6 l6">
          <asp:TextBox
            ID="txt_nombre"
            ClientIDMode="Static"
            autocomplete="off"
            style="width: 98%"
            runat="server"></asp:TextBox>
          <label for="txt_nombre" style="padding-left: 1rem">Nombre(s)</label>
        </div>
      </div>
      <div class="row" style="margin-bottom: 0">
        <div class="input-field col s12 m6 l6">
          <asp:TextBox
            ID="txt_apellido_paterno"
            ClientIDMode="Static"
            style="width: 94%"
            runat="server"></asp:TextBox>
          <label
            for="<%= txt_apellido_paterno.ClientID%>"
            style="padding-left: 1rem; width: 94% !important"
            >Apellido Paterno</label
          >
        </div>
        <div class="input-field col s12 m6 l6">
          <asp:TextBox
            ID="txt_apellido_materno"
            ClientIDMode="Static"
            style="width: 98%"
            runat="server"></asp:TextBox>
          <label
            for="<%= txt_apellido_materno.ClientID%>"
            style="padding-left: 1rem"
            >Apellido Materno</label
          >
        </div>
      </div>
      <div class="row" style="margin-bottom: 0">
        <div class="input-field col s12 m6 l6">
          <asp:TextBox
            ID="txt_password"
            ClientIDMode="Static"
            TextMode="Password"
            autocomplete="new-password"
            style="width: 94%"
            runat="server"></asp:TextBox>
          <label for="txt_password" style="padding-left: 1rem"
            >Contraseña</label
          >
        </div>
        <div class="input-field col s12 m6 l6">
          <asp:TextBox
            ID="txt_password_confirma"
            ClientIDMode="Static"
            TextMode="Password"
            style="width: 96%; height: 2rem; padding: 0 0.75rem"
            runat="server"></asp:TextBox>
          <label for="txt_password_confirma" style="padding-left: 1rem"
            >Confirmar Contraseña</label
          >
        </div>
        <div class="row">
          <div class="col s12 m12 l12">
            <a href="/informacion/aviso-de-privacidad.aspx" target="_blank"
              >Aviso de Privacidad</a
            >
            y
            <a
              href="/informacion/terminos-y-condiciones-de-compra.aspx"
              target="_blank"
              >Términos y condiciones de compra</a
            >
          </div>
          <div class="col s12 m12 l12">
            <p>
              <label>
                <asp:CheckBox ID="chk_politica_privacidad" runat="server" />
                <span
                  >"Acepto los términos y condiciones de compra y aviso de
                  privacidad</span
                >
              </label>
            </p>
          </div>
        </div>
        <div class="row">
          <div
            class="input-field col s12 m6 l6"
            style="
              width: 100%;
              display: flex;
              justify-content: center;
              margin-bottom: 3rem;
            ">
            <asp:LinkButton
              ID="btn_registrar"
              OnClick="btn_registrar_Click"
              OnClientClick="btnLoading(this);"
              class=""
              style="
                color: #fff;
                background-color: #06c;
                border-radius: 4px;
                text-transform: none;
                padding: 8px 18px;
              "
              runat="server"
              >Registrar</asp:LinkButton
            >
          </div>
        </div>
      </div>
    </div>
  </div>
</asp:Content>
