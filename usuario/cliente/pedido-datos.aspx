<%@ Page Language="C#" AutoEventWireup="true" Async="true" MasterPageFile="~/bootstrap/basic.master" CodeFile="pedido-datos.aspx.cs" Inherits="usuario_cliente_pedido_datos" %>

<%@ Register Src="~/usuario/cliente/cliente-header.ascx" TagPrefix="header" TagName="menuGeneral" %>

<asp:Content runat="server" ContentPlaceHolderID="body">
    <header:menuGeneral ID="menuGeneral" runat="server" />
    <asp:HiddenField ID="hf_id_pedido" runat="server" />
    <asp:HiddenField ID="hf_id_pedido_direccion_envio" runat="server" />
    <div class="container-md is-top-3">
        <div class="is-flex is-flex-col is-justify-center is-items-start">
            <div class="is-w-full is-flex is-justify-between is-items-center">
                <div class="is-flex is-justify-start is-items-center">
                    <h1 class="h5">
                        <strong>Contacto del pedido:
                        <asp:Label ID="lt_numero_pedido" class="is-select-all" runat="server"></asp:Label>
                        </strong>
                    </h1>
                    <button type="button" title="Copiar número de pedido" class="is-cursor-pointer" style="background-color: transparent; outline: none; border: none;" onclick="copiarNumeroParte('body_lt_numero_pedido', 'Pedido')">
                        <span class="is-text-gray">
                            <svg class="is-w-4 is-h-4" aria-labelledby="Clipcopy" title="Copiar elemento" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 5H6a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2v-1M8 5a2 2 0 002 2h2a2 2 0 002-2M8 5a2 2 0 012-2h2a2 2 0 012 2m0 0h2a2 2 0 012 2v3m2 4H10m0 0l3-3m-3 3l3 3"></path>
                                <title id="Clipcopy">Copiar elemento</title>
                            </svg>
                        </span>
                    </button>
                </div>
                <asp:HyperLink ID="btn_volver_resumen" runat="server">Volver al resumen</asp:HyperLink>
            </div>
            <p>Establece el contacto de quién recibe el pedido.</p>
        </div>
        <div class="row ">
            <div class="col  col-xs-12  col-sm-6   col-md-6  col-lg-6 is-w-fit">
                <div class="col">
                    <p><strong>Contactos guardados:</strong></p>
                    <asp:ListView ID="lv_contactos" OnItemCanceling="OnItemCanceling" OnItemUpdating="OnItemUpdating" OnItemEditing="OnItemEditing" runat="server">
                        <LayoutTemplate>
                            <div class="row">
                                <div id="itemPlaceholder" runat="server"></div>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <asp:HiddenField ID="hf_id_contacto" Value='<%#Eval("id") %>' runat="server" />
                            <div class="col col-sm-12 col-md-12 col-lg-12 is-bt-3" style="width: fit-content;">
                                <div id='contentCard_DireccEnvio' class="card is-p-4" runat="server">
                                    <div class="card-body">
                                        <h5 class="card-title"><%# Eval("nombre") %> <%# Eval("apellido_paterno") %> <%# Eval("apellido_materno") %></h5>
                                        <p class="card-text">
                                            Tel.: <%# Eval("celular")%>
                                            <br />
                                            Tel. Alt.: <%# Eval("telefono")%>
                                        </p>
                                        <div class="is-top-2">
                                            <asp:LinkButton class="is-btn-gray-light is-space-r-6" OnClientClick="return confirm('¿Eliminar?');" OnClick="btn_eliminarContacto_Click" ID="btn_eliminarContacto" runat="server">
                                                <i class="fas fa-trash-alt"></i>
                                            </asp:LinkButton>
                                            <a class="is-btn-gray-light is-space-r-6" href='/usuario/cliente/editar/contacto/<%#Eval("id") %>?ref=<%= seguridad.Encriptar(hf_id_pedido.Value)%>'>Editar</a>
                                            <asp:LinkButton ID="btn_usarDatos" OnClick="btn_usarDatos_Click" runat="server">
                                                <div class="is-btn-blue is-inline-block seleccionar-contacto">Seleccionar</div></asp:LinkButton>
                                        </div>
                                        <%--<div id="msg_sucess" runat="server" visible="false" class="alert alert-success mt-2" role="alert"></div>--%>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:HiddenField ID="hf_id_contacto" Value='<%#Eval("id") %>' runat="server" />
                            <div class="col col-lg-12">
                                <div id='contentCard_DireccEnvio' class="card " runat="server">
                                    <div class="card-body">
                                        <h5 class="card-title"><%# Eval("nombre") %> <%# Eval("apellido_paterno") %></h5>
                                        <div class="row g-3">
                                            <div class="col-md-12">
                                                <label>Nombre</label>
                                                <asp:TextBox ID="txt_edit_nombre" Text='<%# Eval("nombre") %>' class="form-control" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                                <label>Apellido paterno</label>
                                                <asp:TextBox ID="txt_edit_apellido_paterno" Text='<%# Eval("apellido_paterno") %>' class="form-control" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                                <label>Apellido materno</label>
                                                <asp:TextBox ID="txt_edit_apellido_materno" Text='<%# Eval("apellido_materno") %>' class="form-control" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                                <label>Celular</label>
                                                <asp:TextBox ID="txt_edit_celular" Text='<%# Eval("celular") %>' class="form-control" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="col-md-6">
                                                <label>Teléfono</label>
                                                <asp:TextBox ID="txt_edit_telefono" Text='<%# Eval("telefono") %>' class="form-control" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                                            </div>

                                            <div id='content_alert_actualizar_contacto_<%#Eval("id") %>' class="mt-0 "></div>
                                            <div class="col-md-6">
                                                <asp:LinkButton ID="btn_Cancelar"
                                                    class="btn btn-sm  " CommandName="Cancel" runat="server">Cancelar</asp:LinkButton>

                                                <asp:LinkButton ID="btn_Actualizar"
                                                    class="is-btn-blue" CommandName="Update" runat="server">Guardar</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </EditItemTemplate>
                        <EmptyDataTemplate>
                            No hay datos de contacto guardados
                        </EmptyDataTemplate>
                    </asp:ListView>
                </div>
            </div>
            <div class="col  col-xs-12  col-sm-6   col-md-6  col-lg-6 is-p-8 is-bg-gray-light is-rounded-lg" style="margin: 2rem auto; max-width: 400px;">
                <div class="d-grid gap-2 ">
                    <div class="col">
                        <div class="row">
                            <div class="col">
                                <p><strong>Agregar un contacto nuevo:</strong></p>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="form-group is-bt-1">
                                <label for="<%=txt_add_nombre.ClientID %>">Nombre(s)*:</label>
                                <asp:TextBox ID="txt_add_nombre" ClientIDMode="Static" class="form-control" data-length="20" MaxLength="40" runat="server"></asp:TextBox>
                                <%--<small id="emailHelp" class="form-text text-muted">Nombres</small>--%>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="form-group is-bt-1">
                                <label for="<%=txt_add_apellido_paterno.ClientID %>">Apellido(s)*:</label>
                                <asp:TextBox ID="txt_add_apellido_paterno" ClientIDMode="Static" class="form-control" data-length="50" MaxLength="50" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-row">
                            <%--<div class="form-group col-md-6 col-lg-5 col-xl-3">
                                <label for="<%= txt_add_apellido_materno.ClientID %>">Apellido Materno</label>
                                <asp:TextBox ID="txt_add_apellido_materno" ClientIDMode="Static" class="form-control" data-length="35" MaxLength="35" runat="server"></asp:TextBox
                            </div>--%>
                            <div class="form-group is-bt-1">
                                <label for="txt_add_celular">Teléfono*:</label>
                                <asp:TextBox ID="txt_add_celular" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txt_add_telefono">Teléfono alternativo:</label>
                                <asp:TextBox ID="txt_add_telefono" ClientIDMode="Static" class="form-control" data-length="35" MaxLength="35" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div id="content_alert_crear_contacto" class="mt-0 "></div>
                        <div class="form-group is-flex is-justify-center is-items-center is-top-1 is-py-4">
                            <asp:Button ID="btn_crear_contacto" class="is-btn-blue" OnClick="btn_crear_contacto_Click" Text="Guardar" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
