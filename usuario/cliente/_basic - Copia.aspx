<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/bootstrap/basic.master" CodeFile="_basic - Copia.aspx.cs" Inherits="usuario_cliente_basic" %>
<%@ Register Src="~/usuario/cliente/cliente-header.ascx" TagPrefix="header" TagName="menuGeneral" %>

<asp:Content runat="server" ContentPlaceHolderID="body">
    <header:menuGeneral ID="menuGeneral" runat="server" />

    <div class="container-fluid">
        <div class="row">
            <div class="col">
                <h1 class="h2">Carrito de productos</h1>
            </div>

        </div>
    </div>
</asp:Content>


