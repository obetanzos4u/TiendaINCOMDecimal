<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/herramientas/_masterConfiguraciones.master" Async="true"
    CodeFile="editar-usuario.aspx.cs"
    Inherits="herramientas_editar_producto" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <h1 class="center-align">Edición de usuarios</h1>
    <h3 class="center-align">Powered by Marketing Development</h3>
    <div class="container">
        <div class="row">
            <asp:UpdatePanel UpdateMode="Conditional" RenderMode="Block" class="input-field col s12" runat="server">
                <contenttemplate>
                    <i class="material-icons prefix">search</i>
                    <asp:TextBox ID="txt_search_usuario" OnTextChanged="txt_search_usuario_TextChanged" AutoPostBack="true"
                        placeholder="Busca por email" runat="server">
                    </asp:TextBox>
                    <label>Busca por email</label>
                </contenttemplate>

            </asp:UpdatePanel>
        </div>
        <div class="row">
            <div class="col s12">
                <ul class="tabs">
                    <li class="tab col s3 active"><a href="#up_informacion_personal">Información Personal</a></li>
                    <li class="tab col s3"><a class="" href="#up_roles_privacidad">Roles y Privacidad</a></li>
                    <li class="tab col s3"><a class="" href="#up_permisos_aplicacion">Permisos Aplicación</a></li>
                </ul>
            </div>
            <asp:UpdatePanel id="up_informacion_personal" UpdateMode="Conditional" ClientIDMode="Static" class="col s12" runat="server" RenderMode="Block">
                <triggers>
                </triggers>
                <contenttemplate>
                    <h2>Información Personal</h2>
                    <div class="row">
                        <div class="col s12 m4 l2">
                            <label>Registrado por</label>
                            <asp:Label ID="lbl_registrado_por" runat="server">--</asp:Label>
                        </div>
                        <div class="col s12 m4 l4">
                            <label>Fecha de registro</label>
                            <asp:Label ID="lbl_fecha_registro" runat="server"></asp:Label>
                        </div>
                        <div class="input-field col s12 m4 l4">
                            <asp:TextBox ID="txt_id" Enabled="false" runat="server"></asp:TextBox>
                            <label>ID</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col s12 m4 l2">
                            <label>Cuenta habilitada</label>
                            <asp:DropDownList ID="ddl_cuenta_activa" AutoPostBack="true"
                                OnSelectedIndexChanged="ddl_cuenta_activa_SelectedIndexChanged" runat="server">

                                <asp:ListItem Value="">Selecciona</asp:ListItem>
                                <asp:ListItem Value="0">Deshabilitada</asp:ListItem>
                                <asp:ListItem Value="1">Habilitada</asp:ListItem>

                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s12 m6 l6">
                            <asp:TextBox ID="txt_nombre" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                            <label>Nombre</label>
                        </div>
                        <div class="input-field col s12 m6 l6">
                            <asp:TextBox ID="txt_apellido_paterno" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <label>Apellido Paterno</label>
                        </div>
                        <div class="input-field col s12 m6 l6">
                            <asp:TextBox ID="txt_apellido_materno" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <label>Apellido Materno</label>
                        </div>
                        <div class="input-field col s12 m6 l6">
                            <asp:TextBox ID="txt_email" placeholder="Email" Enabled="false" runat="server" AutoPostBack="true"></asp:TextBox>
                            <label>Email</label>
                        </div>
                        <div class="input-field col s12 m12 l12">
                            <asp:TextBox ID="txt_fecha_nacimiento" placeholder="Fecha de nacimiento" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <label>Fecha de nacimiento</label>
                        </div>
                        <div class="input-field col s12 m12 l12">
                            <asp:TextBox ID="txt_id_cliente" placeholder="ID SAP" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <label>ID SAP</label>
                        </div>
                        <div class="input-field col s12 m6 l6">
                            <asp:TextBox ID="txt_ultimo_inicio_sesion" placeholder="Último inicio de sesión" runat="server"></asp:TextBox>
                            <label>Último inicio de sesión</label>
                        </div>


                    </div>
                </contenttemplate>
            </asp:UpdatePanel>


            <asp:UpdatePanel ID="up_roles_privacidad" UpdateMode="Conditional" ClientIDMode="Static" class="col s12" runat="server" RenderMode="Block">
                <triggers>
                </triggers>
                <contenttemplate>

                    <h2>Roles y Privacidad - Tienda</h2>
                    <div class="row">
                        <div class="input-field col s12 m6 l6">
                            <asp:DropDownList ID="ddl_tipo_de_usuario" AutoPostBack="true"
                                OnSelectedIndexChanged="ddl_actualizarCampo_SelectedIndexChanged" runat="server">
                                <asp:ListItem Value="cliente">Cliente</asp:ListItem>
                                <asp:ListItem Value="usuario">Asesor/Personal Incom</asp:ListItem>
                            </asp:DropDownList>
                            <label>Tipo de usuario</label>
                        </div>
                        <div class="input-field col s12 m6 l6">
                            <asp:DropDownList ID="ddl_rango" AutoPostBack="true"
                                OnSelectedIndexChanged="ddl_actualizarCampo_SelectedIndexChanged" runat="server">
                                <asp:ListItem Value="1">Cliente</asp:ListItem>
                                <asp:ListItem Value="2">Supervisor</asp:ListItem>
                                <asp:ListItem Value="3">Administrador</asp:ListItem>
                            </asp:DropDownList>
                            <label>Rango</label>
                        </div>
                        <div class="input-field col s12 m4 l4">
                            <asp:DropDownList ID="ddl_departamento" AutoPostBack="true"
                                OnSelectedIndexChanged="ddl_actualizarCampo_SelectedIndexChanged" runat="server">
                                <asp:ListItem Value="Telemarketing">Telemarketing</asp:ListItem>
                                <asp:ListItem Value="Marketing">Marketing</asp:ListItem>
                                <asp:ListItem Value="Ventas">Ventas</asp:ListItem>
                                <asp:ListItem Value="">Ninguno</asp:ListItem>
                            </asp:DropDownList>
                            <label>Departamento</label>
                        </div>
                        <div class="input-field col s12 m4 l4">
                            <asp:DropDownList ID="ddl_grupoPrivacidad" AutoPostBack="true"
                                OnSelectedIndexChanged="ddl_actualizarCampo_SelectedIndexChanged" runat="server">
                                <asp:ListItem Value="">Ninguno</asp:ListItem>
                            </asp:DropDownList>
                            <label>Grupo privacidad</label>
                        </div>
                        <div class="input-field col s12 m4 l4">
                            <asp:DropDownList ID="ddl_perfil_cliente"
                                OnSelectedIndexChanged="ddl_actualizarCampo_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                <asp:ListItem Value="">Ninguno</asp:ListItem>
                            </asp:DropDownList>
                            <label>Perfil Cliente</label>
                        </div>
                        <div class="input-field col s12 m4 l4">
                            <asp:TextBox ID="txt_rol_precios_multiplicador" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <label>Rol Precios Multiplicador</label>
                        </div>
                        <div class="input-field col s12 m4 l4">
                            <asp:TextBox ID="txt_rol_productos" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <label>Rol Productos </label>
                        </div>
                        <div class="input-field col s12 m4 l4">
                            <asp:TextBox ID="txt_rol_categorias" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <label>Rol Categororías</label>
                        </div>
                        <div class="input-field col s12 m4 l4">
                            <asp:TextBox ID="txt_asesor_base" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <label>Asesor Base</label>
                        </div>
                        <div class="input-field col s12 m4 l4">
                            <asp:TextBox ID="txt_grupo_asesor" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <label>Grupo Asesor</label>
                        </div>
                        <div class="input-field col s12 m4 l4">
                            <asp:TextBox ID="txt_asesor_adicional" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <label>Asesor Adicional</label>
                        </div>
                        <div class="input-field col s12 m4 l4">
                            <asp:TextBox ID="txt_grupo_asesores_adicional" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <label>Grupo Asesor Adicional</label>
                        </div>
                        <div class="input-field col s12 m4 l4">
                            <asp:TextBox ID="txt_grupo_usuario" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <label>Grupo Usuario</label>
                        </div>
                        <div class="input-field col s12 m4 l4">
                            <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <label>Máximo 5</label>
                        </div>
                        <div class="input-field col s12 m4 l4">
                            <asp:TextBox ID="TextBox2" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <label>Máximo 5</label>
                        </div>
                    </div>
                </contenttemplate>
            </asp:UpdatePanel>


            <asp:UpdatePanel ID="up_permisos_aplicacion" UpdateMode="Conditional" ClientIDMode="Static" class="col s12" runat="server" RenderMode="Block">
                <triggers>
                </triggers>
                <contenttemplate>
                    <h2>Permisos de aplicación</h2>
                    <p>
                        Esta sección permite a determinados usuarios permitir/denegar secciones y páginas de configuración. 
                        Para que los permisos tengan efecto, es necesario cerrar y abrir sesión al usuario modificado.
                    </p>
                    <div class="row margin-b-1x">
                        <div class="input-field col s12 m6 l6">
                            <asp:DropDownList ID="ddl_permisos_app_secciones_pagina"
                                runat="server">
                                <asp:ListItem Value="fast_admin_precios">Fast Admin Precios</asp:ListItem>
                                <asp:ListItem Value="admin_pedidos">Admin de pedidos/compras clientes.</asp:ListItem>
                                <asp:ListItem Value="cargar_info_productos_tienda">Carga info. productos tienda XLS</asp:ListItem>
                                <asp:ListItem Value="editar_producto_tienda">Editar producto tienda</asp:ListItem>
                                <asp:ListItem Value="establecer_tipo_de_cambio">Establecer tipo de cambio</asp:ListItem>
                                <asp:ListItem Value="admin_slider_home">Administrar Slider home</asp:ListItem>
                                <asp:ListItem Value="editar_usuario">Editar Usuario</asp:ListItem>
                                <asp:ListItem Value="agregar_producto_a_pedido">Agregar producto a pedido</asp:ListItem>
                                <asp:ListItem Value="editar_permisos_usuario_aplicacion">Editar Permisos usuario aplicacion</asp:ListItem>
                                <asp:ListItem Value="configurar_servicio_de_fletes">Configurar servicio de fletes</asp:ListItem>
                                <asp:ListItem Value="configurar_API_calculo_envios">Configurar API cálculo de envíos</asp:ListItem>
                                <asp:ListItem Value="cargar_productos_cantidad_maxima_venta">Cargar listado productos venta máxima</asp:ListItem>
                                <asp:ListItem Value="cargar_pesos_y_medidas">Cargar pesos y medidas</asp:ListItem>
                                <asp:ListItem Value="cargar_precios_fantasma">Cargar precios fantasma</asp:ListItem>
                                <asp:ListItem Value="cargar_multimedia">Cargar multimedia, fotos, fichas</asp:ListItem>
                                <asp:ListItem Value="reportes_operaciones_xls">Reportes cotizaciones XLS</asp:ListItem>
                                <asp:ListItem Value="log_errores_sql">Logs SQL</asp:ListItem>
                            </asp:DropDownList>
                            <label>Tipo de usuario</label>
                        </div>
                        <div class="input-field col s12 m6 l6">
                            <label>
                                <asp:CheckBox ID="chk_permisos_app_permitir" runat="server" />
                                <span>Permitir</span>
                            </label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s12 m6 l6">
                            <asp:LinkButton ID="btn_agregar_permiso_app" OnClick="btn_agregar_permiso_app_Click" CssClass="btn blue" runat="server">Agregar Permiso</asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s12 m12 l12">
                            <asp:ListView ID="lv_permisos_app" runat="server">
                                <itemtemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_seccion_pagina" runat="server" Text='<%# Eval("seccion_pagina") %>' />
                                        </td>
                                        <td>
                                            <label>
                                                <asp:CheckBox ID="chk_permiso_app_permitir" AutoPostBack="true"
                                                    OnCheckedChanged="chk_permiso_app_permitir_CheckedChanged" Checked='<%# Eval("permiso") %>' runat="server" />
                                                <span>Permitir</span>
                                            </label>
                                        </td>
                                    </tr>
                                </itemtemplate>
                                <emptydatatemplate>
                                    <h3>No hay permisos registrados aún.</h3>
                                </emptydatatemplate>
                                <layouttemplate>
                                    <table>
                                        <thead>
                                            <tr>
                                                <th>Nombre Permiso</th>
                                                <th>Permiso</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </layouttemplate>
                            </asp:ListView>
                        </div>
                    </div>
                </contenttemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>



