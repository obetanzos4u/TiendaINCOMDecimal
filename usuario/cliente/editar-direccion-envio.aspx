<%@ Page Language="C#" AutoEventWireup="true" Async="true" MasterPageFile="~/bootstrap/basic.master" CodeFile="editar-direccion-envio.aspx.cs" Inherits="usuario_cliente_editar_direccion_envio" %>
<%@ Register Src="~/usuario/cliente/cliente-header.ascx" TagPrefix="header" TagName="menuGeneral" %>

<%@ Register TagPrefix="uc" TagName="ddlPaises"  Src="~/userControls/ddl_paises.ascx" %>
<%@ Register TagPrefix="uc" TagName="ddlEstados"  Src="~/userControls/ddl_estados.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="body">
    <header:menuGeneral ID="menuGeneral" runat="server" />

    <div class="container mt-4 is-top-3">
        <div class="row" style="width: 400px; margin: 2rem auto 4rem auto;">
            <div class="col is-bg-gray-light is-p-8 is-top-1 is-rounded-lg is-border-gray-soft">
                <h1 class="is-text-base is-font-semibold">Editar dirección de envío</h1>
                  <h1 class="h2" id="titulo_nombre_direccion"  runat="server"></h1>
                    <asp:HiddenField ID="hd_id_direccion" Value='<%#Eval("id") %>' runat="server" />
                        <div class="form-row">
                            <div class="form-group is-top-75">
                                <label for="<%= txt_nombre_direccion.ClientID %>">Asigna un nombre a esta dirección:</label>
                                <asp:TextBox ID="txt_nombre_direccion" ClientIDMode="Static" class="form-control" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                                    <small id="emailHelp" class="form-text text-muted">Ejemplo: Casa, trabajo, bodega</small>
                            </div>
                            <div class="form-group is-top-75">
                                <label for="txt_codigo_postal">Código Postal:</label>
                                <asp:TextBox ID="txt_codigo_postal" AutoPostBack="true" OnTextChanged="txt_codigo_postal_TextChanged"
                                    ClientIDMode="Static" class="form-control" style="display: initial; width: 30%;" runat="server">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="form-group is-top-75">
                                <label for="<%= txt_calle.ClientID %>">Calle:</label>
                                <asp:TextBox ID="txt_calle" ClientIDMode="Static" class="form-control" data-length="50" MaxLength="50" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group is-top-75">
                                <label for="<%= txt_numero.ClientID %>">Número:</label>
                                <asp:TextBox ID="txt_numero" ClientIDMode="Static" class="form-control" style="width: 40%;" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="form-group is-top-75">
                                <label for="<%= txt_colonia.ClientID %>">Colonia:</label>
                                <asp:DropDownList ID="ddl_colonia" visible="false" class="form-control" runat="server"></asp:DropDownList>
                                <asp:TextBox ID="txt_colonia" ClientIDMode="Static" Visible="true" class="form-control"   runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group is-top-75">
                            <label for="txt_delegacion_municipio">Delegación/Municipio:</label>
                            <asp:TextBox ID="txt_delegacion_municipio" ClientIDMode="Static" class="form-control" data-length="35" MaxLength="35" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group is-top-75">
                            <label for="txt_ciudad" style="display: initial;">Ciudad:</label>
                            <asp:TextBox ID="txt_ciudad" ClientIDMode="Static" CssClass="validate form-control" style="width: 80%; display: 
                            initial;" data-length="60" MaxLength="60" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div id="cont_ddl_estado" class="form-group form-estado" runat="server">
                        <label for="ddl_municipio_estado" class="is-top-75 is-h-8">Estado:</label>
                        <uc:ddlEstados ID="ddl_estado" style="height: 2rem;" runat="server" />
                    </div>
                    <div id="cont_txt_estado" class="form-group is-top-75" runat="server">
                        <label for="txt_estado">Estado:</label>
                        <asp:TextBox ID="txt_estado" class="form-control" ClientIDMode="Static" data-length="35" MaxLength="35" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-row">
                        <div class="form-group is-top-75">
                            <label for="ddl_pais" class="">País:</label>
                            <uc:ddlPaises ID="ddl_pais" runat="server" />
                        </div>
                    </div>
                    <div class="form-group is-top-75">
                        <label for="txt_referencias">Referencias:</label>
                        <asp:TextBox ID="txt_referencias" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div id="content_alert">
                    </div>
                    <div class="is-text-center">
                        <asp:Button ID="btn_editar_direccion" class="btn-4 is-top-2"  OnClick="btn_editar_direccion_Click"  Text="Guardar" runat="server" /> 
                    </div>
            </div>
        </div>
    </div>

    <style>
        .btn-4 {

            border: none;
            border-radius: 6px;
            display: inline-block;
            height: 36px;
            line-height: 36px;
            padding: 0 16px;
            text-transform: none;
            vertical-align: middle;
            -webkit-tap-highlight-color: transparent;

            width: 120px;

            text-decoration: none;
            color: #fff;
            background-color: #0066cc;
            text-align: center;
            font-weight: bold;
            /* letter-spacing: .5px; */
            -webkit-transition: background-color .2s ease-out;
            transition: background-color .2s ease-out;
            cursor: pointer;

            font-size: 12px;
            outline: 0;

            box-shadow: 0 2px 2px 0 rgba(0,0,0,0.14),0 3px 1px -2px rgba(0,0,0,0.12),0 1px 5px 0 rgba(0,0,0,0.2);
        }

        .line-height-1 {
            line-height: 1.5rem
        }
        </style>
</asp:Content>


