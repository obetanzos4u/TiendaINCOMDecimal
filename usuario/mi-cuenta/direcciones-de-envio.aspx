<%@ Page Language="C#" AutoEventWireup="true"   Async="true" CodeFile="direcciones-de-envio.aspx.cs" MasterPageFile="~/gnCliente.master" Inherits="direcciones_envio" %>
<%@ Register TagPrefix="uc" TagName="dEnvio"  Src="~/userControls/uc_direccionesEnvio.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">

    <div class="is-bt-5 is-mx-6 is-border-soft is-rounded-xl is-p-8">
        <div class="row">
            <div class="col l12 is-bt-1">
                <h2 class="center-align is-m-0">Mis direcciones de envío</h2>
            </div>
        </div>
        <div class="row">
            <!-- <div class="col s12 m12 l12">
                <h2>Mis direcciones de envío</h2>
            </div> -->
            <!-- <div class="col s12 m12 l9" style="font-size: 1.25rem;">Administra tus direcciones de envío para realizar pedidos y cotizaciones:</div> -->
            <div class="col" style="float: right;">
                <a id="eliminar" href="<%= ResolveUrl("~/usuario/mi-cuenta/crear-direccion-de-envio.aspx") %>">
                    <div class="is-text-white is-btn-gray">
                        <img  class="icon_mi_cuenta-nueva_direccion" src="/img/webUI/newdesign/entrega.png"/>
                        <span>Nueva dirección</span>
                    </div>
                </a>
            </div>
        </div>
        <div class="row ">
            <div class="col s12 m12 l12">
                <uc:dEnvio ID="dEnvio" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
