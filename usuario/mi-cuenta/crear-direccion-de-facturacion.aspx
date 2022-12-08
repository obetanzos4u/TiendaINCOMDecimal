<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeFile="crear-direccion-de-facturacion.aspx.cs" MasterPageFile="~/gnCliente.master" Inherits="crear_direccion_facturacion" %>

<%@ Register TagPrefix="uc" TagName="cFacturacion" Src="~/userControls/uc_crearDireccionesFacturacion.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <uc:cFacturacion ID="ucCrearDireccFact" runat="server" />
</asp:Content>
