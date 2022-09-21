<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="true" Async="true" CodeFile="busqueda.aspx.cs" MasterPageFile="~/generalCategoria.master" Inherits="busqueda" %>
<%@ Register Src="~/userControls/productosTiendaListado.ascx" TagName="productos" TagPrefix="uc_pro" %>
<%@ Register Src="~/userControls/categoriaL1.ascx" TagName="categorias" TagPrefix="uc_cat" %>
<%@ Register Src="~/userControls/ui/catalogosSlider.ascx" TagName="sliderCatalogos" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <div class="row" style="margin: auto 0px !important;">
        <uc_pro:productos ID="ucProductos" runat="server"></uc_pro:productos>
    </div>
<%--     <div class="row">
        <uc1:sliderCatalogos runat="server"></uc1:sliderCatalogos>
    </div> --%>
</asp:Content>
