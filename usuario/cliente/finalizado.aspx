<%@ Page Language="C#" AutoEventWireup="true" Async="true" MaintainScrollPositionOnPostback="true" MasterPageFile="~/bootstrap/basic.master" CodeFile="finalizado.aspx.cs" Inherits="usuario_cliente_finalizado" %>

<%@ Register Src="~/usuario/cliente/cliente-header.ascx" TagPrefix="hdr" TagName="menu" %>
<%@ Register TagPrefix="uc" TagName="progreso" Src="~/userControls/uc_progresoCompra.ascx" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <hdr:menu runat="server" />
    <uc:progreso runat="server" />
    <div>
        <p>Finalizado</p>
    </div>
</asp:Content>