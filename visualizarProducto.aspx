<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeFile="visualizarProducto.aspx.cs" MasterPageFile="~/generalProducto.master" Inherits="visualizarProducto" %>

<%@ Register Src="~/userControls/productoVisualizar.ascx" TagName="producto" TagPrefix="uc_pro" %>
<%@ Register Src="~/userControls/ui/catalogosSlider.ascx" TagName="sliderCatalogos" TagPrefix="uc1" %>
<%@ Register Src="~/userControls/uc_productos_comentarios.ascx" TagName="comentarios" TagPrefix="uc_pro" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <div class="row">
        <uc_pro:producto runat="server"></uc_pro:producto>
         <uc_pro:comentarios runat="server"></uc_pro:comentarios>
    </div>
    <div class="row">
        <uc1:sliderCatalogos runat="server"></uc1:sliderCatalogos>
    </div>
</asp:Content>
