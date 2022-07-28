<%@ Page Language="C#" AutoEventWireup="true"   MaintainScrollPositionOnPostback="true" Async="true"  CodeFile="categoria.aspx.cs" MasterPageFile="~/generalCategoria.master" Inherits="categoria" %>
<%@ Register Src="~/userControls/productosTiendaListado.ascx" TagName="productos" TagPrefix="uc_pro" %>
<%@ Register Src="~/userControls/categoriaL1.ascx" TagName="categorias" TagPrefix="uc_cat" %>
<%@ Register Src="~/userControls/ui/catalogosSlider.ascx" TagName="sliderCatalogos" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
   
        <div class="row ">
            <h1 id="titulo_categoria" runat="server"></h1>
            <h2 id="descripcion_categoria" visible="false" runat="server"></h2>
            <nav id="nav_categorias_breadcrumb">
                <div class="nav-wrapper  blue-grey  lighten-1">
                    <div id="navegacion" runat="server" style="" class=" col l12 ">
                        <asp:HyperLink ID="link_todas_categorias" CssClass="breadcrumb" runat="server">Productos</asp:HyperLink>

                    </div>
                </div>
            </nav>
        </div>

    <div class="row">
        <uc_cat:categorias runat="server"></uc_cat:categorias>
    </div>

    <div class="row">
            <uc_pro:productos ID="ucProductos" runat="server"></uc_pro:productos>
        </div>
 
  <div class="row">
            <uc1:sliderCatalogos runat="server"></uc1:sliderCatalogos>
        </div>
</asp:Content>
