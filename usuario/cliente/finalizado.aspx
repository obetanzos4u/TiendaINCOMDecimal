<%@ Page Language="C#" AutoEventWireup="true" Async="true" MaintainScrollPositionOnPostback="true" MasterPageFile="~/bootstrap/basic.master" CodeFile="finalizado.aspx.cs" Inherits="usuario_cliente_finalizado" %>

<%@ Register Src="~/usuario/cliente/cliente-header.ascx" TagPrefix="hdr" TagName="menu" %>
<%@ Register TagPrefix="uc" TagName="progreso" Src="~/userControls/uc_progresoCompra.ascx" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <hdr:menu runat="server" />
    <uc:progreso runat="server" />
    <div class="container-compra_finalizada">
        <p class="text-compra_finalizada is-m-auto is-flex is-text-center">
            ¡Gracias por comprar con nosotros!
            <br class="textspace-compra_finalizada">
            El pedido se ha efectuado correctamente y se te mandará un correo de confirmación
        </p>
        <img class="icon-check_circle" src="/img/webUI/newdesign/circle_check.png" alt="Compra correcta marca de acuerdo" title="Compra finalizada de forma exitosa"/>
        <div class="is-btn-blue is-m-auto btn-seguir_comprando">Seguir comprando</div>
    </div>


    <style>

    .container-pay-process {
        margin: 1rem auto;
    }

    .container-compra_finalizada {
        width: fit-content;
        margin: 4rem auto auto auto;
        padding: 0% 6% 0% 6%;
    }

    .textspace-compra_finalizada {
        margin: 0.5rem;
    }

    .icon-check_circle {
        display: flex;
    }

    .text-resumen {
        text-align: center;
    }

    @media only screen and (min-width: 1600px) {
        .container-pay-process {
            width: 40%;
        }
    }

    @media only screen and (min-width: 1000px) {
        .container-pay-process {
            width: 60%;
        }
    }

    @media only screen and (min-width: 700px) {

        .text-compra_finalizada {
            font-size: 1.25rem;
            line-height: 1.5rem;
        }

        .icon-check_circle {
            margin: 4rem auto;
            width: 180px;
        }
    }

    @media only screen and (max-width: 700px) {

        .text-compra_finalizada {
            font-size: 0.75rem;
            line-height: 1.5rem;
        }

        .icon-check_circle {
            margin: 3rem auto;
            width: 140px;
        }

        .btn-seguir_comprando {
            height: 26px;
            line-height: 26px;
            font-size: 10px;
        }
    }

    @media only screen and (min-width:500px) and (max-width: 700px) {

        .text-resumen {
        font-size: 0.8rem;
        }

        .svg_resumen, .svg_pago,
        .svg_finalizar, .svg_resumen_puntos,
        .svg_pago_puntos {
            width: 2rem;
            height: 2rem;
        }
    }

    @media only screen and (max-width: 500px) {

        .text-resumen {
            font-size: 0.5rem;
        }

        .svg_resumen, .svg_pago,
        .svg_finalizar, .svg_resumen_puntos,
        .svg_pago_puntos {
            width: 1.5rem;
            height: 1.5rem;
        }

        .icon-check_circle {
            margin: 3rem auto;
            width: 100px;
        }
    }
    </style>
</asp:Content>