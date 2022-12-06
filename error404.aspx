<%@ Page Language="C#" AutoEventWireup="true" Async="true" MasterPageFile="~/general.master" CodeFile="error404.aspx.cs" Inherits="error_404" %>

<%@ Register Src="~/userControls/categoriasTodas.ascx" TagName="categoriasTodas" TagPrefix="uc_cat" %>
<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <div class="is-container" style="margin-bottom: 5vh">
        <div class="is-w-full is-flex is-flex-col is-justify-center is-items-center">
            <img src="https://www.incom.mx/img/webUI/newdesign/404.png" alt="Error 404" class="is-w-1_3" />
            <h2 class="is-text-xl is-font-semibold is-m-0">Página no encontrada</h2>
            <p class="is-m-2 is-py-2">La dirección ha cambiado o no se encuentra disponible temporalmente [<span class="is-italic">404</span>]</p>
        </div>
        <div class="center-carrito_vacio">
            <h3>¡Navega entre más de 2,500 productos!</h3>
            <div class="center-btn-carrito_vacio" style="margin: 2rem auto">
                <a class="is-btn-blue btn-carrito_vacio is-m-auto" href="/productos">Descubrir productos</a>
            </div>
        </div>
    </div>
</asp:Content>
