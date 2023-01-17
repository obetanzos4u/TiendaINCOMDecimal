<%@ Page Language="C#" AutoEventWireup="true"   Async="true" CodeFile="mi-cuenta.aspx.cs" MasterPageFile="~/gnCliente.master" Inherits="usuario_mi_cuenta" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <div class="is-bt-5 is-mx-6 is-border-soft is-rounded-xl is-p-8 container-mi_cuenta">
        <div class="row">
            <div class="">
                <h2 class="is-text-center is-m-0 is-bt-2 is-font-bold is-text-black-soft">Mi cuenta</h2>
                <h2 class="margin-t-2x">Bienvenido a tu cuenta INCOM,
                    <asp:Literal ID="li_h1_nombre" runat="server"></asp:Literal>.</h2>
                <!-- <h3 class="margin-b-2x">Datos de la cuenta: </h3> -->
            </div>
        </div>
        <div class="row " id="datosCliente" runat="server">
            <h4 class="margin-b-2x">Estos son los datos de tu cuenta: </h4>
            <div class="is-top-2">
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
                <div class="btn_container-mi_cuenta">
                    <asp:LinkButton ID="btn_editarDatosBasicos"
                        class="is-text-white is-btn-gray"
                        OnClick="btn_editarDatosBasicos_Click" style="text-transform: none;" runat="server">Editar datos</asp:LinkButton>
                    <asp:LinkButton ID="btn_CambiarPassword"
                        class="is-text-white is-btn-gray"
                    OnClick="btn_CambiarPassword_Click" style="text-transform: none;" runat="server">Actualizar contraseña </asp:LinkButton>
                </div>
                <br />
                <br />
            </div>
        </div>
        <div id="datosClienteEdit" visible="false" class="col datos_cliente_edicion is-m-auto is-w-fit" runat="server">
            <div class="row margin-b-2x">
                <h2 class="is-bt-2">Cambiar datos de perfil:</h2>
                <div class="input-field col s12 m6 l6 margin-b-2x hide">
                    <asp:TextBox ID="txt_id_cliente" AutoCompleteType="None"
                        Enabled="false" autocomplete="nope" runat="server"></asp:TextBox>
                    <label for="txt_id_cliente">ID Cliente</label>
                </div>
            </div>
            <div class="row is-bt-1">
                <div class="input-field margin-b-2x">
                    <asp:TextBox ID="txt_nombre" ClientIDMode="Static" class="txt-mi_cuenta" style="width: 250px" runat="server"></asp:TextBox>
                    <label for="txt_nombre">Nombre(s):</label>
                </div>
            </div>
            <div class="row is-bt-1">
                <div class="input-field">
                    <label for="txt_apellido_paterno">Apellido Paterno:</label>
                    <asp:TextBox ID="txt_apellido_paterno" class="txt-mi_cuenta" style="width: 250px" ClientIDMode="Static" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row is-bt-1">
                <div class="input-field">
                    <label for="txt_apellido_materno">Apellido Materno:</label>
                    <asp:TextBox ID="txt_apellido_materno" class="txt-mi_cuenta" style="width: 250px" ClientIDMode="Static" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row is-bt-1">
                <div class="input-field">
                    <asp:TextBox ID="txt_celular" class="txt-mi_cuenta" style="width: 250px" ClientIDMode="Static" runat="server"></asp:TextBox>
                    <label for="txt_celular">Celular:
                </div>
            </div>
            <div class="row is-bt-1">
                <div class="input-field">
                    <asp:TextBox ID="txt_telefono" class="txt-mi_cuenta" style="width: 250px" ClientIDMode="Static" runat="server"></asp:TextBox>
                    <label for="txt_telefono">Telefono fijo:</label>
                </div>
            </div>
            <div class="row">
                <div class="editar_datos-mi_cuenta">
                    <asp:LinkButton ID="btn_cancelar_edicion" OnClick="btn_cancelar_edicion_Click"
                        CssClass="is-inline-block" runat="server">
                        <div class="btn-cerrar_edicion">Cancelar</div>
                    </asp:LinkButton>
                    <asp:LinkButton ID="btn_cambiarDatos" OnClick="btn_cambiarDatos_Click" runat="server">
                        <div class="btn-guardar_datos is-text-white is-btn-blue">Guardar</div>
                    </asp:LinkButton>
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
                    <label style="min-width: 250px;">Cambiar contraseña:</label>
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
                        CssClass="is-btn-gray btn-cerrar-actualizar_contrasena is-text-white"
                        runat="server">Volver a mi cuenta</asp:LinkButton>
                    <asp:LinkButton ID="btn_cambiar_password" OnClick="btn_cambiar_password_Click"
                        runat="server">
                        <div class="is-btn-blue btn-actualizar_contrasena is-text-white" style="display: inline-block;">Cambiar contraseña</div>
                    </asp:LinkButton>
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
