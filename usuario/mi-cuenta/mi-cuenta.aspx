﻿<%@ Page Language="C#" AutoEventWireup="true"   Async="true" CodeFile="mi-cuenta.aspx.cs" MasterPageFile="~/gnCliente.master" Inherits="usuario_mi_cuenta" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <div class="is-bt-5 is-mx-6 is-border-soft is-rounded-xl is-p-8 container-mi_cuenta">
        <div class="row">
            <div class="col l12">
                <h2 class="is-text-center is-m-0">Mi cuenta</h2>
                <h2 class="margin-t-2x">Bienvenido,
                    <asp:Literal ID="li_h1_nombre" runat="server"></asp:Literal></h2>
                <h3 class="margin-b-2x">Datos de la cuenta: </h3>
            </div>
        </div>
        <div class="row " id="datosCliente" runat="server">
            <div class="col s12 l12">
                <ul class="collection ">
                    <li class="collection-item"><span class="title"><strong>Nombre: </strong></span>
                        <asp:Literal ID="li_nombre" runat="server"></asp:Literal> 
                          <asp:Literal ID="li_apellidos" runat="server"></asp:Literal>
                    </li>
                    <li class="collection-item"><strong>Celular: </strong>
                        <asp:Literal ID="li_celular" runat="server"></asp:Literal> </li>
                      <li class="collection-item"><strong>Teléfono: </strong>
                       <asp:Literal ID="li_telefono" runat="server"></asp:Literal>  </li>
                    <li class="collection-item"><strong>Correo electrónico: </strong>
                        <asp:Literal ID="li_email" runat="server"></asp:Literal></li>
                    <li class="collection-item hide"><strong>ID de Cliente: </strong>
                        <asp:Literal ID="li_id_cliente" runat="server"></asp:Literal>
                        <i class="tiny material-icons tooltipped light-blue-text lighten-4" data-tooltip="
                             El ID de cliente será asignado por un asesor
                             ">help</i>
                    </li>
                </ul>
                <asp:LinkButton ID="btn_editarDatosBasicos"
                    class="waves-effect waves-light btn blue-grey darken-1 btn-s "
                    OnClick="btn_editarDatosBasicos_Click"  style="text-transform: none;" runat="server">Editar datos</asp:LinkButton>
                &nbsp;
                  <asp:LinkButton ID="btn_CambiarPassword" 
                    class="waves-effect waves-light btn blue-grey darken-1 btn-s "
                  OnClick="btn_CambiarPassword_Click" style="text-transform: none;" runat="server">Actualizar contraseña </asp:LinkButton>
                <br />
                <br />
            </div>
        </div>
        <div id="datosClienteEdit" visible="false" class="col l12 datos_cliente_edicion" runat="server">
            <div class="row margin-b-2x">
                <h2>Cambiar datos de perfil:</h2>
                <div class="input-field col s12 m6 l6 margin-b-2x hide">
                    <asp:TextBox ID="txt_id_cliente" AutoCompleteType="None"
                        Enabled="false" autocomplete="nope" runat="server"></asp:TextBox>
                    <label for="txt_id_cliente">ID Cliente</label>
                </div>
                <div class="input-field col s12 m12 l12 margin-b-2x">
                    <asp:TextBox ID="txt_nombre" ClientIDMode="Static" Style="width: 250px" runat="server"></asp:TextBox>
                    <label for="txt_nombre">Nombre(s)</label>
                </div>
            </div>
            <div class="row">
                <div class="input-field col s12 m12 l12">
                    <label for="txt_apellido_paterno">Apellido Paterno</label>
                    <asp:TextBox ID="txt_apellido_paterno" Style="width: 250px" ClientIDMode="Static" runat="server"></asp:TextBox>
                </div>
                <div class="input-field col s12 m12 l12">
                    <label for="txt_apellido_materno">Apellido Materno</label>
                    <asp:TextBox ID="txt_apellido_materno" Style="width: 250px" ClientIDMode="Static" runat="server"></asp:TextBox>
                </div>
                <div class="input-field col s12 m12 l12">
                    <asp:TextBox ID="txt_celular" Style="width: 250px" ClientIDMode="Static" runat="server"></asp:TextBox>
                    <label for="txt_celular">Celular</label>
                </div>
                <div class="input-field col s12 m12 l12">
                    <asp:TextBox ID="txt_telefono" Style="width: 250px" ClientIDMode="Static" runat="server"></asp:TextBox>
                    <label for="txt_telefono">Telefono fijo </label>
                </div>
            </div>
            <div class="row ">
                <div class="col l12 ">
                    <asp:LinkButton ID="btn_cancelar_edicion" OnClick="btn_cancelar_edicion_Click"
                        CssClass="btn-cerrar_edicion waves-effect waves-light btn btn-s  blue-grey-text text-darken-2 blue-grey lighten-5 " style="text-transform: none;" runat="server">Cerrar edición</asp:LinkButton>
                    <asp:LinkButton ID="btn_cambiarDatos" OnClick="btn_cambiarDatos_Click"
                        class="btn-guardar_datos waves-effect waves-light btn  blue-grey darken-1 btn-s" style="text-transform: none;"
                        runat="server">Guardar Datos</asp:LinkButton>
                </div>
            </div>
            <div class="row "></div>
        </div>
        <div id="content_password_edit" class="actualizar_contrasena" visible="false" runat="server" >
            <div class="row margin-b-2x">  <div class="col l12 ">
                <h3>Actualizar contraseña:</h3>
                <p>(Mínimo 6, máximo 20 caracteres)</p>
            </div></div>
            <div class="row margin-b-2x">
                <div class="col   s12 m5 l4  xl3 input-field  margin-t-4x">
                    <asp:TextBox ID="txt_password" placeholder="Ingresa una nueva contraseña" ClientIDMode="Static" data-length="20"
                        autocomplete="new-password" TextMode="Password" runat="server"></asp:TextBox>
                    <label>Cambiar contraseña</label>
                    <!-- <span toggle="#txt_password" class="field-icon toggle-password"><span class="material-icons">visibility</span></span> -->
                </div>
                <div class="row margin-b-2x"></div>
                <div class="col s12 m5 l4 xl3 input-field margin-t-4x">
                    <asp:TextBox ID="txt_password_confirmacion" TextMode="Password" data-length="20" autocomplete="new-password" ClientIDMode="Static"
                        placeholder="Confirma contraseña"
                        runat="server">
                    </asp:TextBox>
                    <label>Confirma:</label>
                    <!-- <span toggle="#txt_password_confirmacion" class="field-icon toggle-password"><span class="material-icons">visibility</span></span> -->
                </div>
            </div>
            <div class="row margin-b-2x">
                <div class="col s12 l12 margin-t-4x">
                    <asp:LinkButton ID="LinkButton1" OnClick="btn_cancelar_edicion_Click"
                        CssClass="btn-cerrar-actualizar_contrasena waves-effect waves-light btn  btn-s  blue-grey-text text-darken-2 blue-grey lighten-5"
                        runat="server">Cerrar edición</asp:LinkButton>
                    <asp:LinkButton ID="btn_cambiar_password" OnClick="btn_cambiar_password_Click"
                        class="btn-actualizar_contrasena waves-effect waves-light btn blue-grey darken-1 btn-s " runat="server">Cambiar contraseña</asp:LinkButton>
                    <br />
                    <br />
                </div>
            </div>
        </div>
    </div>

    <script> document.addEventListener("DOMContentLoaded", function (event) {

            $(document).ready(function () {
                $('input#txt_password, input#txt_password_confirmacion').characterCounter();
            });
 });</script>  
</asp:Content>
