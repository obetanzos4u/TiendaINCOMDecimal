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
        <img class="icon-check_circle" src="/img/webUI/newdesign/check_comprado.gif" alt="Compra correcta marca de acuerdo" title="Compra finalizada de forma exitosa" />
    </div>"
    <div class="is-w-full">
        <button class="button-seguir_comprando is-flex is-m-auto">
            <asp:HyperLink ID="btn_seguir_comprando" style="color: white;
            text-decoration: none;" runat="server">
                <span>Seguir comprando</span>
                <div class="cart-seguir_comprando">
                    <svg viewBox="0 0 36 26">
                        <polyline points="1 2.5 6 2.5 10 18.5 25.5 18.5 28.5 7.5 7.5 7.5"></polyline>
                        <polyline points="15 13.5 17 15.5 22 10.5"></polyline>
                    </svg>
                </div>
            </asp:HyperLink>
        </button>
    </div>
<style>
    .button-seguir_comprando {
        --background: #06c;
        --text: #fff;
        --cart: #fff;
        --tick: var(--background);
        position: relative;
        border: none;
        background: none;
        padding: 8px 28px;
        border-radius: 8px;
        -webkit-appearance: none;
        -webkit-tap-highlight-color: transparent;
        -webkit-mask-image: -webkit-radial-gradient(white, black);
        overflow: hidden;
        cursor: pointer;
        text-align: center;
        min-width: 144px;
        color: var(--text);
        background: var(--background);
        transform: scale(var(--scale, 1));
        transition: transform 0.4s cubic-bezier(0.36, 1.01, 0.32, 1.27);
    }
        .button-seguir_comprando:hover {
            background: #1c74f8;
        }

        .button-seguir_comprando:active {
        --scale: 0.95;
        }

    .button-seguir_comprando span {
        font-size: 14px;
        font-weight: 600;
        display: block;
        position: relative;
        padding-left: 24px;
        margin-left: -28px;
        line-height: 26px;
        transform: translateY(var(--span-y, 0));
        transition: transform 0.7s ease;
    }

    .button-seguir_comprando .cart-seguir_comprando {
        position: absolute;
        left: 50%;
        top: 50%;
        margin: -13px 0 0 -18px;
        transform-origin: 12px 23px;
        transform: translateX(-120px) rotate(-18deg);
    }

    .button-seguir_comprando .cart-seguir_comprando:before, .button-seguir_comprando .cart-seguir_comprando:after {
        content: "";
        position: absolute;
    }

    .button-seguir_comprando .cart-seguir_comprando:before {
        width: 6px;
        height: 6px;
        border-radius: 50%;
        box-shadow: inset 0 0 0 2px var(--cart);
        bottom: 0;
        left: 9px;
        filter: drop-shadow(11px 0 0 var(--cart));
    }

    .button-seguir_comprando .cart-seguir_comprando:after {
        width: 16px;
        height: 9px;
        background: var(--cart);
        left: 9px;
        bottom: 7px;
        transform-origin: 50% 100%;
        transform: perspective(4px) rotateX(-6deg) scaleY(var(--fill, 0));
        transition: transform 1.2s ease var(--fill-d);
    }

    .button-seguir_comprando .cart-seguir_comprando svg {
        z-index: 1;
        width: 36px;
        height: 26px;
        display: block;
        position: relative;
        fill: none;
        stroke: var(--cart);
        stroke-width: 2px;
        stroke-linecap: round;
        stroke-linejoin: round;
    }

    .button-seguir_comprando .cart-seguir_comprando svg polyline:last-child {
        stroke: var(--tick);
        stroke-dasharray: 10px;
        stroke-dashoffset: var(--offset, 10px);
        transition: stroke-dashoffset 0.4s ease var(--offset-d);
    }

    .button-seguir_comprando.loading {
        --scale: 0.95;
        --span-y: -32px;
        --icon-r: 180deg;
        --fill: 1;
        --fill-d: 0.8s;
        --offset: 0;
        --offset-d: 1.73s;
    }

    .button-seguir_comprando.loading .cart-seguir_comprando {
    -webkit-animation: cart 3.4s linear forwards 0.2s;
            animation: cart 3.4s linear forwards 0.2s;
    }

    @-webkit-keyframes cart {
        12.5% {
            transform: translateX(-60px) rotate(-18deg);
        }
        25%, 45%, 55%, 75% {
            transform: none;
        }
        50% {
            transform: scale(0.9);
        }
        44%, 56% {
            transform-origin: 12px 23px;
        }
        45%, 55% {
            transform-origin: 50% 50%;
        }
        87.5% {
            transform: translateX(70px) rotate(-18deg);
        }
        100% {
            transform: translateX(140px) rotate(-18deg);
        }
    }

    @keyframes cart {
        12.5% {
            transform: translateX(-60px) rotate(-18deg);
        }
        25%, 45%, 55%, 75% {
            transform: none;
        }
        50% {
            transform: scale(0.9);
        }
        44%, 56% {
            transform-origin: 12px 23px;
        }
        45%, 55% {
            transform-origin: 50% 50%;
        }
        87.5% {
            transform: translateX(70px) rotate(-18deg);
        }
        100% {
            transform: translateX(140px) rotate(-18deg);
        }
    }

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
<script>
    document.querySelectorAll('.button-seguir_comprando').forEach(button=> window.addEventListener('load', e => {
        if(!button.classList.contains('loading'))
            {
                button.classList.add('loading');
                setTimeout(() => button.classList.remove('loading'), 3700);
            }
        e.preventDefault();
        }));
</script>
</asp:Content>
