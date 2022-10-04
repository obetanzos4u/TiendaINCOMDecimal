<%@ Page Language="C#" AutoEventWireup="true"   Async="true" CodeFile="direcciones-de-facturacion.aspx.cs" MasterPageFile="~/gnCliente.master" Inherits="direcciones_facturacion" %>
<%@ Register TagPrefix="uc" TagName="dFacturacion"  Src="~/userControls/uc_direccionesFacturacion.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">

    <div class="is-bt-5 is-mx-6 is-border-soft is-rounded-xl is-p-8">
        <div class="row">
            <div class="col l12">
                <h2 class="center-align is-m-0">Facturación</h2>
            </div>
        </div>
        <div class="row">
            <div class="col s12 m12 l12">
                <h2>Mis direcciones de facturación</h2>
            </div>
            <div class="col s12 m12 l9">Administra tus direcciones de facturación para: Cotizaciones o Pedidos</div>

            <div class="col s12 m12 l3 right-align">
                <a id="eliminar" href="<%= ResolveUrl("~/usuario/mi-cuenta/crear-direccion-de-facturacion.aspx") %>" class="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5 right-align tooltipped" style="text-transform: none;" data-tooltip="Agregar dirección de envío ">
                    <i class="material-icons right">playlist_add_check</i>Agregar dirección</a>
            </div>
        </div>
        <div class="row ">
            <div class="col s12 m12 l12">
                <uc:dFacturacion ID="dFacturacion" runat="server" />
            </div>
        </div>
    </div>

</asp:Content>
