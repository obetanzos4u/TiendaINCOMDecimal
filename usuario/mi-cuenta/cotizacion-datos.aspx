<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeFile="cotizacion-datos.aspx.cs"  EnableEventValidation="false"  
    MasterPageFile="~/usuario/masterPages/clienteCotizacion.master" Inherits="usuario_cotizacionDatos" %>

<%@ Import Namespace="System.Globalization" %>
<%@ Register TagPrefix="uc" TagName="cotizacionTerminos" Src="~/userControls/operaciones/cotizacion_terminos.ascx" %>
<%@ Register TagPrefix="uc" TagName="cotizacionEstatus" Src="~/userControls/operaciones/uc_estatus_cotizacion.ascx" %>

<%@ Register TagPrefix="uc" TagName="contactos" Src="~/userControls/uc_contactos.ascx" %>
<%@ Register TagPrefix="uc" TagName="dEnvio" Src="~/userControls/uc_direccionesEnvio.ascx" %>
<%@ Register TagPrefix="uc" TagName="dFacturacion" Src="~/userControls/uc_direccionesFacturacion.ascx" %>
<%@ Register TagPrefix="uc" TagName="ddlPaises" Src="~/userControls/ddl_paises.ascx" %>
<%@ Register TagPrefix="uc" TagName="ddlEstados" Src="~/userControls/ddl_estados.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">

    <asp:HiddenField ID="hf_id_operacion" runat="server" />
    <div id="content_mensaje" visible="false" runat="server" class="container">
        <div class="row">
            <h1>No tienes permisos para visualizar esta cotización</h1>
        </div>
    </div>
    <div id="content_cotizacion_datos" runat="server" class="container">
       <div class="row"> 
           <div class="col s12 l12 xl12">
                           <h1 class="margin-b-2x"><asp:Literal ID="lt_numero_operacion" runat="server"></asp:Literal> - Datos de 
                    "<asp:Literal ID="lt_nombre_cotizacion" runat="server"></asp:Literal>"   </h1>

           </div>
           <div class="col s12 m12 l4 xl2">

               <h2 class="margin-b-2x">Opciones</h2>


               <!-- Dropdown Trigger -->
             
                   <a class='hide dropdown-trigger btnopcionesCotizacionDatos btn waves-effect waves-light  blue-grey darken-1 ' href='#' data-target='opcionesCotizacionDatos'>
                       <i class="left material-icons">build</i> Opciones</a>
          
               <!-- Dropdown Structure -->
               <ul id="opcionesCotizacionDatos" class='dropdown-content'>
                   <li></li>
                   <li visible="false" runat="server"><a href="#!">two</a></li>
                   <li visible="false" runat="server" class="divider"></li>
                   <li visible="false" runat="server"><a href="#!">three</a></li>
                   <li visible="false" runat="server"><a href="#!"><i class="material-icons">view_module</i>four</a></li>

               </ul>







               <label class="hide">Número de operacion</label>
               <h2 class="hide margin-t-2x margin-b-2x"></h2>
               <label class="hide">Cliente</label>
               <h2 class="margin-t-2x hide">
                <asp:Literal ID="lt_cliente_nombre" runat="server"></asp:Literal>

            </h2>
                     
              <a href="#modal_tipo_cotizacion" onclick="$('.modal_tipo_cotizacion').modal('open');" 
                  class="margin-t-4x waves-effect waves-light btn-small blue-grey-text text-darken-2 blue-grey lighten-5 modal-trigger" id="btn_tipoCotizacion" >Tipo de cotización</a>

             
            <asp:HyperLink ID="hl_editarDatos" CssClass=" hide waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-4 "
                runat="server"> Contacto, envío y facturación</asp:HyperLink>
            &nbsp;
                
                      <asp:HyperLink ID="hl_editarProductos" class="margin-t-4x waves-effect waves-light btn-small blue-grey-text text-darken-2 blue-grey lighten-5"
                          runat="server"> Editar Productos</asp:HyperLink>
               

               <h2 class="margin-b-4x">Cotización condiciones</h2>
         

                     <uc:cotizacionTerminos ID="cotizacion_terminos" runat="server" />
               
                <uc:cotizacionEstatus ID="CotizacionEstatus" runat="server" />
           </div>
           <div class="col s12 m12 l8 xl8">
               <div id="contentDatos" runat="server" class="row">
                   <h2>Edita datos de contacto, dirección de envío y facturación.</h2>
                   <asp:UpdatePanel ID="up_content_comentarios" UpdateMode="Conditional" class="row" ChildrenAsTriggers="true"
                       RenderMode="Block" runat="server">
                       <ContentTemplate>
                    <div class="input-field col s12 m12">
                        <asp:TextBox ID="txt_comentarios" onchange="txtLoading(this);"
                            CssClass="materialize-textarea" OnTextChanged="txt_comentarios_TextChanged"  AutoPostBack="true"
                            MaxLength="600" TextMode="MultiLine" runat="server"></asp:TextBox>
                        <label for="<%=txt_comentarios.ClientID %>">Comentarios/Observaciones generales</label>
                    </div>
                     <div class="input-field col s12 m12">
                         <asp:LinkButton ID="btn_guardar_comentarios" OnClick="txt_comentarios_TextChanged" runat="server">Guardar comentarios</asp:LinkButton></div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txt_comentarios" EventName="TextChanged" />
                     <asp:AsyncPostBackTrigger ControlID="btn_guardar_comentarios" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <ul class="tabs tabs-fixed-width">
                <li class="tab"><a class="active blue-text" href="#test-swipe-1"><i class="material-icons tiny">people</i> Contacto</a></li>
                <li class="tab"><a href="#test-swipe-2"><i class="material-icons  tiny">local_shipping</i> Envío</a></li>
                <li class="tab"><a href="#test-swipe-3"><i class="material-icons  tiny">playlist_add_check</i>  Facturación</a></li>
            </ul>
   <!--------- INICIO contacto -------->
            <div id="test-swipe-1" class="col s12 ">
                <asp:UpdatePanel ID="up_cotizacionDatos" UpdateMode="Conditional" ChildrenAsTriggers="true" RenderMode="Block" class="col s12" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_adminContactos" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="col s12 ">
                            <h3>Selecciona de una de tus contactos guardados previamente.  O bien, llena los campos.</h3>
                        </div>

                        <div class="col s12 m7 l5 xl4">
                            <asp:DropDownList ID="ddl_contactosUser" AutoPostBack="true" OnSelectedIndexChanged="ddl_contactosUser_SelectedIndexChanged" runat="server"></asp:DropDownList>
                        </div>

                        <div class="col s12 ">
                            <asp:LinkButton ID="btn_adminContactos" OnClientClick=" $('#modalAdmin').modal('open');" OnClick="btn_modalShow"
                                runat="server"><i class="material-icons left">people</i>  Puedes administrar tus contactos aquí</asp:LinkButton>

                        </div>

                        <div class="col s12">
                            <h3>Nombre, email etc.</h3>
                        </div>
                        <div class="row">
                            <asp:HiddenField ID="hd_id_contacto" Value='<%#Eval("id") %>' runat="server" />


                            <div class="input-field col s12 m6 l6">
                                <asp:TextBox ID="txt_email" Text='<%#Eval("email") %>' runat="server"></asp:TextBox>
                                <label for="txt_email">Email</label>
                            </div>

                            <div class="input-field col s12 m6 l6">
                                <asp:TextBox ID="txt_nombre" Text='<%#Eval("nombre") %>' runat="server"></asp:TextBox>
                                <label for="txt_nombre">Nombre(s)</label>
                            </div>


                            <div class="input-field col s12 m6 l6">
                                <asp:TextBox ID="txt_apellido_paterno" Text='<%#Eval("apellido_paterno") %>' runat="server"></asp:TextBox>
                                <label for="txt_apellido_paterno">Apellido Paterno</label>
                            </div>
                            <div class="input-field col s12 m6 l6">
                                <asp:TextBox ID="txt_apellido_materno" Text='<%#Eval("apellido_materno") %>' runat="server"></asp:TextBox>
                                <label for="txt_apellido_materno">Apellido Materno</label>
                            </div>

                            <div class="input-field col s12 m6 l6">
                                <asp:TextBox ID="txt_telefono" Text='<%#Eval("telefono") %>' runat="server"></asp:TextBox>
                                <label for="txt_telefono">Teléfono</label>
                            </div>
                            <div class="input-field col s12 m6 l6">
                                <asp:TextBox ID="txt_celular" Text='<%#Eval("celular") %>' runat="server"></asp:TextBox>
                                <label for="txt_celular">Celular</label>
                            </div>

                            <div class=" col s12 m12 l12">
                                <span class="left grey  lighten-4 badge">Consejo: Activa esta opción si deseas guardar esta información de contacto para usarla en operaciones futuras.</span>
                            </div>
                            <div class=" col s12 m12 l12">
                                <label>
                                    <asp:CheckBox ID="chk_guardarNuevoContacto" ClientIDMode="Static" runat="server" />
                                    <span >Guardar como nuevo contacto</span>

                                </label>
                            </div>

                        </div>


                        <div class="col s12">
                            <br />

                            <asp:LinkButton ID="btn_cancelarDatosContacto" OnClick="btn_cancelarDatosContacto_Click" CssClass="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5 "
                                Text="Cancelar cambios de contacto" runat="server"></asp:LinkButton>
                            <asp:LinkButton ID="btn_guardarDatosContacto" OnClientClick="btnLoading(this);" runat="server"
                                OnClick="btn_guardarDatosContacto_Click" class="waves-effect waves-light btn  blue-grey darken-1" Text="Guardar cambios de contacto ✓" />

                        </div>
                    </ContentTemplate>

                </asp:UpdatePanel>
            </div>
            <!--------- INICIO envío -------->
            <div id="test-swipe-2" class="col s12 ">
                <h3>¿A dónde quieres recibir tu producto?</h3>

                <asp:UpdatePanel ID="up_AdminEnvios" UpdateMode="Conditional" ChildrenAsTriggers="true" RenderMode="Block" class="row" runat="server">
                    <ContentTemplate>
                        <div class="col s12 ">
                            <label>
                                <asp:CheckBox ID="chkEnvioEnTienda" OnCheckedChanged="chkEnvioEnTienda_CheckedChanged" AutoPostBack="true"  ClientIDMode="Static" runat="server" />
                                <span>Prefiero recoger el producto en Tienda.</span>

                            </label>
                        </div>
                        <div class="col s12 ">
                            <h3>Selecciona de una de tus direccion de envío guardadas previamente.  O bien, llena los campos.</h3>
                        </div>
                        <div class="col s12 m6 l4 xl3">
                            <asp:DropDownList ID="ddl_direccionesEnvio" AutoPostBack="true" OnSelectedIndexChanged="ddl_direccionesEnvio_SelectedIndexChanged" runat="server"></asp:DropDownList>
                        </div>
                        <div class="col s12 ">
                            <asp:LinkButton ID="btn_adminEnvios" OnClientClick=" $('#modalAdmin').modal('open'); return true;" OnClick="btn_modalShow"
                                runat="server"><i class="material-icons left">local_shipping</i>Puedes administrar tus direcciones de envío aquí</asp:LinkButton>
                        </div>
                        <div class="col s12 m12 l12">
                            <div class="input-field col s12 m12 l6">
                                <asp:TextBox ID="txt_nombre_direccion_envio" ClientIDMode="Static" CssClass="validate" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                                <label for="txt_nombre_direccion_envio">Asigna un nombre a esta dirección </label>
                                <i>(Ejemplo: Casa, Bodega, Almacén principal)</i>
                            </div>
                        </div>
                                                <div class="col s12 m12 l12">
                            <div class="input-field col s12 m12 l5">
                                <asp:TextBox ID="txt_codigo_postal_envio" onchange="txtLoading(this);" AutoPostBack="true" OnTextChanged="txt_codigo_postal_TextChanged"
                                    ClientIDMode="Static" CssClass="validate" runat="server"></asp:TextBox>
                                <label for="txt_codigo_postal_envio">Código Postal</label>

                            </div>
                        </div>

                        <div class="col s12 m12 l12">
                            <div class="input-field col s12 m12 l4">
                                <asp:TextBox ID="txt_calle_envio" ClientIDMode="Static" CssClass="validate" data-length="50" MaxLength="50" runat="server"></asp:TextBox>
                                <label for="txt_calle_envio">Calle</label>
                            </div>
                            <div class="input-field col s12 m12 l3">
                                <asp:TextBox ID="txt_numero_envio" ClientIDMode="Static" CssClass="validate" data-length="30" MaxLength="30" runat="server"></asp:TextBox>
                                <label for="txt_numero_envio">Número</label>
                            </div>

                            <div class="input-field col s12 m12 l5">
                                   <asp:DropDownList ID="ddl_colonia_envio" runat="server" Visible="false"></asp:DropDownList>
                                <asp:TextBox ID="txt_colonia_envio" ClientIDMode="Static"  CssClass="validate" data-length="70" MaxLength="70" runat="server"></asp:TextBox>
                                <label for="txt_colonia_envio">Colonia</label>
                            </div>
                        </div>
                        <div class="col s12 m12 l12">
                            <div class="input-field col s12 m12 l5">
                                <asp:TextBox ID="txt_delegacion_municipio_envio" ClientIDMode="Static" CssClass="validate" data-length="60" MaxLength="60" runat="server"></asp:TextBox>
                                <label for="txt_delegacion_municipio_envio">Delegación/Municipio</label>
                            </div>
                            
                                <div class="input-field col s12 m12 l5">
                    <asp:TextBox ID="txt_ciudad_envio" ClientIDMode="Static" CssClass="validate" data-length="60" MaxLength="60" runat="server"></asp:TextBox>
                    <label for="txt_ciudad">Ciudad</label>
                </div>
                            <div class="input-field col s12 m12 l4">
                                <uc:ddlPaises ID="ddl_pais_envio" runat="server" />
                                <label for="txt_pais_envio">Pais</label>
                            </div>
                            <div id="cont_txt_estado_envio" class="input-field col s12 m12 l3" runat="server">
                                <asp:TextBox ID="txt_estado_envio" ClientIDMode="Static" CssClass="validate" data-length="35" MaxLength="35" runat="server"></asp:TextBox>
                                <label for="txt_estado_envio">Estado</label>
                            </div>


                            <div id="cont_ddl_estado_envio" class="input-field col s12 m12 l3" visible="false" runat="server">
                                <uc:ddlEstados ID="ddl_estado_envio" runat="server" />
                                <label for="txt_pais_envio">Estado</label>

                            </div>

                        </div>

                        <div class="col s12 m12 l12">
                            <div class="input-field col s12 m12 l12">
                                <asp:TextBox ID="txt_referencias_envio" TextMode="MultiLine" ClientIDMode="Static" CssClass="materialize-textarea" data-length="100" MaxLength="100" runat="server"></asp:TextBox>
                                <label for="txt_referencias_envio">Referencias</label>
                            </div>

                        </div>
                        <div class=" col s12 m12 l12">
                            <span class="left grey  lighten-4 badge">Consejo: Activa esta opción si deseas guardar esta información de contacto para usarla en operaciones futuras.</span>
                        </div>
                        <div class=" col s12 m12 l12">
                            <label>
                                <asp:CheckBox ID="chk_guardarNuevadireccionEnvio" ClientIDMode="Static" runat="server" />
                                <span>Guardar como nueva dirección de envío</span>

                            </label>
                        </div>


                        <div class="col s12">
                            <br />

                            <asp:LinkButton ID="btn_cancelarDireccionEnvio_Click" OnClick="btn_cancelarDireccionEnvio_Click_Click" CssClass="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5 "
                                Text="Cancelar cambios  de envio" runat="server"></asp:LinkButton>
                            <asp:LinkButton ID="btn_guardarDireccionEnvio" OnClientClick="btnLoading(this);" runat="server"
                                OnClick="btn_guardarDireccionEnvio_Click" class="waves-effect waves-light btn  blue darken-1" Text="Guardar cambios de envío ✓" />

                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_guardarDireccionEnvio" EventName="Click" />
                         <asp:AsyncPostBackTrigger ControlID="ddl_direccionesEnvio" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>

            <!--------- INICIO Facturación -------->
            <div id="test-swipe-3" class="col s12 ">
                <h3>Facturación.</h3>
                <asp:UpdatePanel ID="up_AdminFacturacion" UpdateMode="Conditional" ChildrenAsTriggers="true" RenderMode="Block" class="col s12" runat="server">
                    <ContentTemplate>

                        <div class="col s12 m12 l12">
                            <h3>Selecciona de una de tus direccion de facturación guardadas previamente. O bien, llena los campos.</h3>
                        </div>
                        <div class="col s12 m6 l4 xl3">
                            <asp:DropDownList ID="ddl_direccionesFacturacion" AutoPostBack="true" OnSelectedIndexChanged="ddl_direccionesFacturacion_SelectedIndexChanged" runat="server"></asp:DropDownList>
                        </div>
                        <div class="col s12 m12 l12">
                            <div class="input-field col s12 m12 l12">

                                <asp:LinkButton ID="btn_adminFacturacion" OnClientClick="$('#modalAdmin').modal('open'); return true;" OnClick="btn_modalShow"
                                    runat="server"><i class="material-icons left">playlist_add_check</i> Puedes administrar tus direcciones de facturación aquí</asp:LinkButton>
                            </div>
                        </div>
                        <div class="row ">
                            <div class="col s12 m12 l12 ">
                                  
                                <div class="input-field col s12 m12 l6">
                                    <asp:TextBox ID="txt_nombre_direccion_facturacion" ClientIDMode="Static" CssClass="validate" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                                    <label for="txt_nombre_direccion_facturacion">Asigna un nombre corto como referencia a esta dirección </label>
                                    <i>Ejemplo: Incom</i>
                                </div>
                            </div>
                            <div class="col s12 m12 l12">
                                <div class="input-field col s12 m12 l8">
                                    <asp:TextBox ID="txt_razon_social_facturacion" ClientIDMode="Static" CssClass="validate" data-length="150" MaxLength="150" runat="server"></asp:TextBox>
                                    <label for="txt_razon_social_facturacion">Razón social</label>
                                    <i>Ejemplo: Insumos Comerciales de Occidente S.A. de C.V.</i>
                                </div>
                                <div class="input-field col s12 m12 l4">
                                    <asp:TextBox ID="txt_rfc_facturacion" ClientIDMode="Static" CssClass="validate" data-length="15" MaxLength="15" runat="server"></asp:TextBox>
                                    <label for="txt_rfc_facturacion">RFC</label>
                                </div>


                            </div>
                        </div>
                        <div class="row">

                            <div class="col s12 m12 l12">
                                <div class="input-field col s12 m12 l4">
                                    <asp:TextBox ID="txt_calle_facturacion" ClientIDMode="Static" CssClass="validate" data-length="50" MaxLength="50" runat="server"></asp:TextBox>
                                    <label for="txt_calle_facturacion">Calle</label>
                                </div>
                                <div class="input-field col s12 m12 l3">
                                    <asp:TextBox ID="txt_numero_facturacion" ClientIDMode="Static" CssClass="validate" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                                    <label for="txt_numero_facturacion">Número</label>
                                </div>

                                <div class="input-field col s12 m12 l5">
                                    <asp:TextBox ID="txt_colonia_facturacion" ClientIDMode="Static" CssClass="validate" data-length="35" MaxLength="35" runat="server"></asp:TextBox>
                                    <label for="txt_colonia_facturacion">Colonia</label>
                                </div>
                            </div>
                            <div class="col s12 m12 l12">
                                <div class="input-field col s12 m12 l5">
                                    <asp:TextBox ID="txt_delegacion_municipio_facturacion" ClientIDMode="Static" CssClass="validate" data-length="35" MaxLength="35" runat="server"></asp:TextBox>
                                    <label for="txt_delegacion_municipio_facturacion">Delegación/Municipio</label>
                                </div>

                                <div class="input-field col s12 m12 l4">
                                    <uc:ddlPaises ID="ddl_pais_facturacion" runat="server" />
                                    <label for="txt_pais_facturacion">Pais</label>
                                </div>
                                <div id="cont_txt_estado_facturacion" class="input-field col s12 m12 l3" runat="server">
                                    <asp:TextBox ID="txt_estado_facturacion" ClientIDMode="Static" CssClass="validate" data-length="35" MaxLength="35" runat="server"></asp:TextBox>
                                    <label for="txt_estado_facturacion">Estado</label>
                                </div>



                                <div id="cont_ddl_estado_facturacion" class="input-field col s12 m12 l3" visible="false" runat="server">
                                    <uc:ddlEstados ID="ddl_estado_facturacion" runat="server" />
                                    <label for="ddl_estado_facturacion">Estado</label>

                                </div>

                            </div>
                            <div class="col s12 m12 l12">
                                <div class="input-field col s12 m12 l5">
                                    <asp:TextBox ID="txt_codigo_postal_facturacion" ClientIDMode="Static" CssClass="validate" runat="server"></asp:TextBox>
                                    <label for="txt_codigo_postal_facturacion">Código Postal</label>

                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class=" col s12 m12 l12">
                                <label>
                                    <asp:CheckBox ID="chk_guardarNuevadireccionFacturacion" ClientIDMode="Static" runat="server" />
                                    <span >Guardar como nueva dirección de facturación</span>
                                </label>
                            </div>

                        </div>
                        <div class="col s12">
                            <br />

                            <asp:LinkButton ID="btn_cancelarDireccionFacturacion" OnClick="btn_cancelarDireccionFacturacion_Click" CssClass="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5"
                                Text="Cancelar cambios de facturación" runat="server"></asp:LinkButton>
                            <asp:LinkButton ID="btn_guardarDireccionFacturacion" OnClientClick="btnLoading(this);" runat="server"
                                OnClick="btn_guardarDireccionFacturacion_Click" class="waves-effect waves-light btn  blue-grey darken-1 " Text="Guardar cambios de facturación ✓" />

                        </div>
                    </ContentTemplate>

                    <Triggers>

                        <asp:AsyncPostBackTrigger ControlID="btn_adminFacturacion" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
           </div>
        <!-- Modal Structure -->
        <div id="modalAdmin" class="modal">
            <asp:UpdatePanel ID="upModal" ClientIDMode="Static" UpdateMode="Conditional" RenderMode="Block" class="modal-content" runat="server">

                <ContentTemplate>
                    <h4 id="modalTitle" runat="server"></h4>

                    <uc:contactos ID="adminContactos" Visible="false" runat="server" />
                    <uc:dEnvio ID="adminEnvios" Visible="false" runat="server" />
                    <uc:dFacturacion ID="adminFacturacion" Visible="false" runat="server" />
                </ContentTemplate>

            </asp:UpdatePanel>

            <div class="modal-footer">
                <a href="#!" class="modal-action modal-close waves-effect waves-light btn  blue-grey darken-1">Cerrar ventana</a>
            </div>
        </div>
    </div>

    <!-- Modal Structure -->
    <div id="modal_tipo_cotizacion" class="modal_tipo_cotizacion modal" runat="server" >
        <div class="modal-content">
            <h2>Selecciona el tipo de cotización</h2>
            <p>Es importante clasificar tu cotización si es que aplica a uno de los siguientes casos:</p>
            <div class="row">
                <div class=" col s12 m12 l12">
                    <asp:DropDownList ID="ddl_tipo_cotizacion" OnSelectedIndexChanged="ddl_tipo_cotizacion_SelectedIndexChanged" AutoPostBack="true" runat="server">
                        <asp:ListItem Value="" Text="Ninguno"></asp:ListItem>
                        <asp:ListItem Value="Licitación" Text="Licitación"></asp:ListItem>
                        <asp:ListItem Value="Proyecto" Text="Proyecto"></asp:ListItem>
                        <asp:ListItem Value="Comparativo" Text="Comparativo"></asp:ListItem>
                        <asp:ListItem Value="Regular" Text="Regular"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
              <div class="row">
                <div class=" col s12 m12 l12">
                    <br /><br /><br /><br />
                </div></div>
        </div>
        <div class="modal-footer">
            <a href="#!" class="modal-action modal-close waves-effect waves-green btn-flat">Cerrar</a>
        </div>
    </div>

    <script type="text/javascript">

        document.addEventListener('DOMContentLoaded', function () {
               $('.modal_tipo_cotizacion').modal();
    var elems = document.querySelectorAll('#btnopcionesCotizacionDatos');
    var instances = M.Dropdown.init(elems, null);
  });
       


</script>

</asp:Content>
