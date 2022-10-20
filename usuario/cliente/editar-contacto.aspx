<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/bootstrap/basic.master" CodeFile="editar-contacto.aspx.cs" Inherits="usuario_cliente_editar_contacto" %>

<%@ Register Src="~/usuario/cliente/cliente-header.ascx" TagPrefix="header" TagName="menuGeneral" %>

<asp:Content runat="server" ContentPlaceHolderID="body">
    <header:menuGeneral ID="menuGeneral" runat="server" />
    <asp:HiddenField ID="hf_id_contacto" Value='<%#Eval("id") %>' runat="server" />
    <div class="container mt-4">
        <div class="row">
            <div class="col col-8">
                <div class="is-flex is-flex-col is-justify-center is-items-start" style="border: 2px solid red">
                    <div class="row">
                        <div class="col">
                            <p>Editar un contacto</p>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group">
                            <label for="<%=txt_add_nombre.ClientID %>">Nombre(s)</label>
                            <asp:TextBox ID="txt_add_nombre" ClientIDMode="Static" class="form-control" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group">
                            <label for="<%=txt_add_apellido_paterno.ClientID %>">Apellido(s)</label>
                            <asp:TextBox ID="txt_add_apellido_paterno" ClientIDMode="Static" class="form-control" data-length="50" MaxLength="50" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-row">
                        <%--<div class="form-group col-md-6 col-lg-5 col-xl-3">
                            <label for="<%= txt_add_apellido_materno.ClientID %>">Apellido Materno</label>
                            <asp:TextBox ID="txt_add_apellido_materno" ClientIDMode="Static" class="form-control" data-length="35" MaxLength="35" runat="server"></asp:TextBox>
                        </div>--%>
                        <div class="form-group">
                            <label for="txt_add_celular">Teléfono:</label>
                            <asp:TextBox ID="txt_add_celular" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txt_add_telefono">Teléfono alternativo:</label>
                            <asp:TextBox ID="txt_add_telefono" ClientIDMode="Static" class="form-control" data-length="35" MaxLength="35" runat="server"></asp:TextBox>

                        </div>
                    </div>

                    <div id="content_alert_crear_contacto" class="mt-0 "></div>
                    <div class="form-group">
                        <asp:Button ID="btn_editarcontacto" class="btn btn-lg btn-primary" OnClick="btn_editar_contacto_Click" Text="Guardar" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


