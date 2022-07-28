<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/bootstrap/basic.master" CodeFile="editar-direccion-facturacion.aspx.cs"
    Inherits="usuario_cliente_editar_direccion_facturacion" %>
<%@ Register Src="~/usuario/cliente/cliente-header.ascx" TagPrefix="header" TagName="menuGeneral" %>

<%@ Register TagPrefix="uc" TagName="ddlPaises"  Src="~/userControls/ddl_paises.ascx" %>
<%@ Register TagPrefix="uc" TagName="ddlEstados"  Src="~/userControls/ddl_estados.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="body">
    <header:menuGeneral ID="menuGeneral" runat="server" />

    <div class="container mt-4">
        <div class="row">
            <div class="col col-8">
                <h1 class="h2">Editar Dirección de facturación</h1>
                  <h1 class="h2" id="titulo_nombre_direccion"  runat="server"></h1>




                <asp:HiddenField ID="hd_id_direccion" Value='<%#Eval("id") %>' runat="server" />
                <div class="form-row">
                    <div class="form-group col-md-6">
                        <label for="<%= txt_nombre_direccion.ClientID %>">Asigna un nombre a esta dirección </label>

                        <asp:TextBox ID="txt_nombre_direccion" ClientIDMode="Static" class="form-control" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                        <small id="emailHelp" class="form-text text-muted">Ejemplo: Casa, Trabajo, Bodega</small>
                    </div>
                                  <div class="form-group col-md-6">
                  <label for="<%= txt_razon_social.ClientID %>">Razón social</label>
                  <asp:TextBox ID="txt_razon_social" ClientIDMode="Static" class="form-control" data-length="150" MaxLength="150" runat="server"></asp:TextBox>

              </div>
              <div class="form-group col-md-6">
                  <label for="<%= txt_rfc.ClientID %>">Ingresa un RFC </label>
                  <asp:TextBox ID="txt_rfc" ClientIDMode="Static" class="form-control" data-length="15" MaxLength="15" runat="server"></asp:TextBox>

              </div>
                    <div class="form-group col-md-2">
                        <label for="txt_codigo_postal">Código Postal</label>
                        <asp:TextBox ID="txt_codigo_postal" AutoPostBack="true" OnTextChanged="txt_codigo_postal_TextChanged"
                            ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-group col-md-6 col-lg-5 col-xl-3">
                        <label for="<%= txt_calle.ClientID %>">Calle</label>
                        <asp:TextBox ID="txt_calle" ClientIDMode="Static" class="form-control" data-length="50" MaxLength="50" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-6  col-lg-3 col-xl-2">
                        <label for="<%= txt_numero.ClientID %>">Número</label>
                        <asp:TextBox ID="txt_numero" ClientIDMode="Static" class="form-control" data-length="20" MaxLength="20" runat="server"></asp:TextBox>

                    </div>
                </div>
                <div class="form-row">
                    <div class="form-group col-md-6">
                        <label for="<%= txt_colonia.ClientID %>">Colonia</label>
                        <asp:DropDownList ID="ddl_colonia" class="form-select" runat="server"></asp:DropDownList>

                        <asp:TextBox ID="txt_colonia" ClientIDMode="Static" Visible="false" class="form-control" runat="server"></asp:TextBox>

                    </div>
                    <div class="form-group col-md-4">
                        <label for="txt_delegacion_municipio">Delegación/Municipio</label>
                        <asp:TextBox ID="txt_delegacion_municipio" ClientIDMode="Static" class="form-control" data-length="35" MaxLength="35" runat="server"></asp:TextBox>

                    </div>
                    <div class="form-group col-md-4">
                        <label for="txt_ciudad">Ciudad</label>
                        <asp:TextBox ID="txt_ciudad" ClientIDMode="Static" CssClass="validate form-control" data-length="60" MaxLength="60" runat="server"></asp:TextBox>

                    </div>

                </div>
                <div class="form-row">
                    <div class="form-group col-md-4">
                        <label for="ddl_pais">Pais</label>
                        <uc:ddlPaises ID="ddl_pais" runat="server" />

                    </div>
                    <div id="cont_ddl_estado" class="form-group col-md-4" runat="server">
                        <label for="ddl_municipio_estado">Estado</label>
                        <uc:ddlEstados ID="ddl_estado" runat="server" />

                    </div>

                    <div id="cont_txt_estado" class="form-group col-md-4" runat="server">
                        <label for="txt_estado">Estado</label>
                        <asp:TextBox ID="txt_estado" class="form-control" ClientIDMode="Static" data-length="35" MaxLength="35" runat="server"></asp:TextBox>

                    </div>

                </div>

                <div id="content_alert"></div>
                <asp:Button ID="btn_editar_direccion" class="btn btn-primary" OnClick="btn_editar_direccion_Click" Text="Guardar" runat="server" />


            </div>
        </div>
    </div>
</asp:Content>


