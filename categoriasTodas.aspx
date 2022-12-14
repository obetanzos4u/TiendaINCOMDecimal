<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeFile="categoriasTodas.aspx.cs" MasterPageFile="~/generalCategoria.master" Inherits="categorias_pag" %>
<%@ Register Src="~/userControls/categoriasTodas.ascx" TagName="categoriasTodas" TagPrefix="uc_cat" %>
<%-- <%@ Register Src="~/userControls/ui/catalogosSlider.ascx" TagName="sliderCatalogos" TagPrefix="uc1" %> --%>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <div class="row">
        <h1 id="titulo_categoria" runat="server">Productos</h1>
        <h2 id="descripcion_categoria" runat="server"></h2>
        <div class="contain-nav-wrapper_producto">
            <div class="nav-wrapper">
                <div class="outname">
                    <div id="navegacion" runat="server" class=" col l12">
                        <asp:HyperLink ID="categoriasTodas" CssClass="breadcrumb" runat="server">Productos</asp:HyperLink>
                    </div>                    
                </div>
            </div>
        </div>
    </div>
    <div class="categorias_todas-1">
        <div class="categorias_todas">
        <uc_cat:categoriasTodas runat="server"></uc_cat:categoriasTodas>
    </div>
    <%--<div class="row">
        <uc1:sliderCatalogos runat="server"></uc1:sliderCatalogos>
    </div>--%>
</asp:Content>
