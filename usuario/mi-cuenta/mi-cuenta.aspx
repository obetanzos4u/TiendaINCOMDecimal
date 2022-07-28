<%@ Page Language="C#" AutoEventWireup="true"   Async="true" CodeFile="mi-cuenta.aspx.cs" MasterPageFile="~/gnCliente.master" Inherits="usuario_mi_cuenta" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <div class="container z-depth-3">
        <div class="row ">
            <div class="col l12">
                <h1 class=" margin-t-2x margin-b-2x">Mi cuenta</h1>

       
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
                    <li class="collection-item"><strong>Email/Usuario: </strong>
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
                    OnClick="btn_editarDatosBasicos_Click" runat="server">Editar Datos</asp:LinkButton>
                &nbsp;
                  <asp:LinkButton ID="btn_CambiarPassword" 
                    class="waves-effect waves-light btn blue-grey darken-1 btn-s "
                  OnClick="btn_CambiarPassword_Click" runat="server">Actualizar contraseña </asp:LinkButton>
                <br />
                <br />
            </div>
        </div>
        <div id="datosClienteEdit" visible="false" class="col l12" runat="server">
            
             

            <div class="row margin-b-2x">
                <h2>Datos de la cuenta</h2>
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
                        CssClass="waves-effect waves-light btn btn-s  blue-grey-text text-darken-2 blue-grey lighten-5 " runat="server">Cerrar edición</asp:LinkButton>

                    <asp:LinkButton ID="btn_cambiarDatos" OnClick="btn_cambiarDatos_Click"
                        class="waves-effect waves-light btn  blue-grey darken-1 btn-s   "
                        runat="server">Guardar Datos</asp:LinkButton>
                </div>

            </div>
            <div class="row "></div>
        </div>
        <div id="content_password_edit" visible="false" runat="server" >
            <div class="row margin-b-2x">  <div class="col l12 ">
                <h2>Actualizar contraseña</h2>
                <p>(Mínimo 6, máximo 20 caracteres)</p>
            </div></div>
            <div class="row margin-b-2x">
                <div class="col   s12 m5 l4  xl3 input-field  margin-t-4x">


                    <asp:TextBox ID="txt_password" placeholder="Ingresa una nueva contraseña" ClientIDMode="Static" data-length="20"
                        autocomplete="new-password" TextMode="Password" runat="server"></asp:TextBox>
                    <label>Cambiar contraseña</label>
                    <span toggle="#txt_password" class="field-icon toggle-password"><span class="material-icons">visibility</span></span>
                </div>
                <div class="row margin-b-2x"></div>
                <div class="col s12 m5 l4 xl3 input-field margin-t-4x">

                    <asp:TextBox ID="txt_password_confirmacion" TextMode="Password" data-length="20" autocomplete="new-password" ClientIDMode="Static"
                        placeholder="Confirma contraseña"
                        runat="server">
                    </asp:TextBox>
                    <label>Confirma:</label>
                    <span toggle="#txt_password_confirmacion" class="field-icon toggle-password"><span class="material-icons">visibility</span></span>
                </div>
            </div>
            <div class="row margin-b-2x">
                <div class="col s12 l12 margin-t-4x">
                    <asp:LinkButton ID="LinkButton1" OnClick="btn_cancelar_edicion_Click"
                        CssClass="waves-effect waves-light btn  btn-s  blue-grey-text text-darken-2 blue-grey lighten-5"
                        runat="server">Cerrar edición</asp:LinkButton>
                    <asp:LinkButton ID="btn_cambiar_password" OnClick="btn_cambiar_password_Click"
                        class="waves-effect waves-light btn blue-grey darken-1 btn-s " runat="server">Cambiar contraseña</asp:LinkButton>
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
