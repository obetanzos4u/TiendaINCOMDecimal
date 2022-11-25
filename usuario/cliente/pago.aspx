<%@ Page Language="C#" AutoEventWireup="true" Async="true" MaintainScrollPositionOnPostback="true" MasterPageFile="~/bootstrap/basic.master" CodeFile="pago.aspx.cs" Inherits="usuario_cliente_pago" %>

<%@ Register Src="~/usuario/cliente/cliente-header.ascx" TagPrefix="hdr" TagName="menu" %>
<%@ Register TagPrefix="uc" TagName="progreso" Src="~/userControls/uc_progresoCompra.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="body">
    <asp:HiddenField ID="hf_id_operacion" runat="server" />
    <asp:HiddenField ID="hf_moneda" runat="server" />
    <asp:HiddenField ID="hf_numero_operacion" runat="server" />
    <hdr:menu runat="server" />
    <uc:progreso runat="server" />
    <div class="container-pago container-md">
        <div class="is-flex is-flex-col is-justify-center is-items-center">
            <div class="margin-metodos_pago is-flex is-justify-between is-items-center">
                <div class="is-flex is-justify-center is-items-center">
                    <h1 class="h5 text-metodo_pago"><strong>Método de pago del pedido:
                    <asp:Label ID="lbl_numero_pedido" class="is-select-all" runat="server"></asp:Label></strong></h1>
                    <button type="button" class="is-cursor-pointer" style="background-color: transparent; outline: none; border: none;" onclick="copiarNumeroParte('body_lt_numero_pedido', 'Pedido')">
                        <span class="is-text-gray">
                            <svg class="is-w-4 is-h-4" aria-labelledby="Clipcopy" title="Copiar elemento" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 5H6a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2v-1M8 5a2 2 0 002 2h2a2 2 0 002-2M8 5a2 2 0 012-2h2a2 2 0 012 2m0 0h2a2 2 0 012 2v3m2 4H10m0 0l3-3m-3 3l3 3"></path>
                                <title id="Clipcopy">Copiar elemento</title>
                            </svg>
                        </span>
                    </button>
                </div>
                <div class="is-flex container-formas_pago">
                    <p class="text-aceptamos_formas_pago"><strong>Aceptamos:</strong></p>
                    <img title="Formas de pago" class="icono-formas_pago" src="/img/webUI/newdesign/formaspago.jpg" alt="Métodos de pago bancario" />
                </div>
            </div>
            <div class="is-container container-metodo_pago">
                <div class="container_movil-metodo_pago is-flex is-justify-between">
                    <div>
                        <p><strong>Elige el método de pago:  </strong></p>
                        <asp:UpdatePanel ID="up_pasarelaPago" UpdateMode="Conditional" RenderMode="Block" runat="server">
                            <ContentTemplate>
                                <div class="is-flex is-items-start">
                                    <asp:Button ID="btn_tarjeta" class="is-btn-gray is-space-x-9" Text="Tarjeta de crédito/débito" OnClick="btn_tarjeta_Click" UseSubmitBehavior="false" runat="server" />
                                    <asp:Button ID="btn_paypal" class="is-btn-gray is-space-x-9" Text="PayPal" OnClick="btn_paypal_Click" UseSubmitBehavior="false" runat="server" />
                                    <asp:Button ID="btn_transferencia" class="is-btn-gray" OnClick="btn_transferencia_Click" Text="Transferencia o depósito" UseSubmitBehavior="false" runat="server" />
                                </div>
                                <asp:Panel ID="pnl_tarjeta" Visible="false" class="is-py-4" runat="server">
                                    <div id="btn_renovarPedidoSantanderContenedor" visible="false" class="is-flex is-justify-center is-items-center" runat="server">
                                        <asp:LinkButton ID="btn_renovarPedidoSantander" CssClass="" Style="text-decoration: none" OnClientClick="btnLoading(this);" OnClick="btn_renovarPedidoSantander_Click" runat="server">Renovar pedido</asp:LinkButton>
                                    </div>
                                    <iframe id="frm_pagoTarjeta" visible="true" style="width: 100%; height: 740px; border: 0;" runat="server"></iframe>
                                </asp:Panel>
                                <asp:Panel ID="pnl_paypal" Visible="false" runat="server">
                                    <asp:UpdatePanel ID="up_paypal" UpdateMode="Conditional" RenderMode="Block" runat="server">
                                        <ContentTemplate>

                                            <!--<div id="paypal_button_container" class="paypal_button_container" runat="server" style="border: 2px solid red"></div>
                                            <asp:Panel ID="pnl_noDisponiblePago" Visible="false" runat="server">
                                                <strong>El pago no está disponible por los siguientes motivos: </strong>
                                                <p id="motivosNoDisponiblePago" visible="false" runat="server"></p>
                                            </asp:Panel>
                                            <asp:LinkButton ID="btn_renovarPedidoPayPal" Visible="false" OnClick="btn_renovarPedidoPayPal_Click" runat="server">Renovar pedido</asp:LinkButton>
                                            <div id="content_msg_bootstrap"></div>
                                            <table id="dt_desglose_paypal" visible="false" class="table" runat="server">
                                                <tr>
                                                    <td>Tipo de intento</td>
                                                    <td>
                                                        <asp:Label ID="lbl_paypal_intento" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Estado del pago</td>
                                                    <td>
                                                        <asp:Label ID="lbl_paypal_estado" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Monto pagado en PayPal</td>
                                                    <td>
                                                        <asp:Label ID="lbl_paypal_monto" runat="server"></asp:Label>
                                                        <asp:Label ID="lbl_paypal_moneda" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Fecha del primer intento</td>
                                                    <td>
                                                        <asp:Label ID="lbl_paypal_fecha_primerIntento" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Última actualización</td>
                                                    <td>
                                                        <asp:Label ID="lbl_paypal_fecha_actualizacion" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:LinkButton ID="linkActualizarUP" runat="server" OnClick="linkActualizarUP_Click">Actualizar</asp:LinkButton>-->
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                                <asp:Panel ID="pnl_transferencia" Visible="false" runat="server">
                                    <div class="border-datos_pago_transferencia">
                                        <div class="title-datos_pago_transferencia">
                                            <h5><strong>Datos de la cuenta</strong></h5>
                                        </div>
                                        <div class="datos_pago_transferencia">
                                            <span>
                                                <p><strong>Razón social:&nbsp;&nbsp;&nbsp;</strong>Insumos Comerciales de Occidente S.A. de C.V.</p>
                                                <p><strong>RFC:&nbsp;&nbsp;&nbsp;</strong> ICO990224H93</p>
                                                <p><strong>Dirección:&nbsp;&nbsp;&nbsp;</strong>Plutarco Elías Calles 276, Colonia Tlazintla, C.P. 08710, Iztacalco, Ciudad de México, México.</p>
                                                <p><strong>Banco:&nbsp;&nbsp;&nbsp;</strong>Banamex</p>
                                            </span>
                                            <div class="container-datos_pago_transferencia">
                                                <span class="column-datos_pago">
                                                    <p><strong>Moneda</strong></p>
                                                    <p>Moneda Nacional</p>
                                                    <p>Dólares (USD)</p>
                                                    <p>Dólares (USD) desde el extranjero</p>
                                                </span>
                                                <span class="column-datos_pago">
                                                    <p><strong>Sucursal</strong></p>
                                                    <p>0269</p>
                                                    <p>414</p>
                                                    <p>414</p>
                                                </span>
                                                <span class="column-datos_pago">
                                                    <p><strong>Cuenta</strong></p>
                                                    <p>7782861</p>
                                                    <p>9412714</p>
                                                    <p>9412714</p>
                                                </span>
                                                <span class="column-datos_pago">
                                                    <p><strong>Clabe</strong></p>
                                                    <p>002180026977828615</p>
                                                    <p>002180041494127146</p>
                                                    <p>002180041494127146</p>
                                                </span>
                                                <span class="column-datos_pago">
                                                    <p><strong>Plaza</strong></p>
                                                    <p>&nbsp;&nbsp;&nbsp;</p>
                                                    <p>&nbsp;&nbsp;&nbsp;</p>
                                                    <p>001</p>
                                                </span>
                                                <span class="column-datos_pago">
                                                    <p><strong>Swift</strong></p>
                                                    <p>&nbsp;&nbsp;&nbsp;</p>
                                                    <p>&nbsp;&nbsp;&nbsp;</p>
                                                    <p>BNMXMXMM</p>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="container_movil-datos_pago_transferencia">
                                            <hr/>
                                            <span class="movil-datos_pago">
                                                <p style="width: 30%;"><strong>Moneda</strong><br/>Moneda Nacional</p>
                                                <p style="width: 20%;"><strong>Sucursal</strong><br/>0269</p>
                                                <p style="width: 20%;"><strong>Cuenta</strong><br/>7782861</p>
                                                <p style="width: 30%;"><strong>Clabe</strong><br/>002180026977828615</p>
                                            </span>
                                            <hr/>
                                            <span class="movil-datos_pago">
                                                <p style="width: 30%;"><strong>Moneda</strong><br/>Dólares (USD)</p>
                                                <p style="width: 20%;"><strong>Sucursal</strong><br/>414</p>
                                                <p style="width: 20%;"><strong>Cuenta</strong><br/>9412714</p>
                                                <p style="width: 30%;"><strong>Clabe</strong><br/>002180041494127146</p>                                                                             
                                            </span>
                                            <hr/>
                                            <span class="movil-datos_pago">
                                                <p style="width: 30%;"><strong>Moneda</strong><br/>Dólares (USD) desde el extranjero</p>                                               
                                                <p style="width: 20%;"><strong>Sucursal</strong><br/>414</p>
                                                <p style="width: 20%;"><strong>Cuenta</strong><br/>9412714</p>
                                            </span>
                                            <span class="movil-datos_pago">
                                                <p style="width: 30%;"><strong>Clabe</strong><br/>002180041494127146</p>
                                                <p style="width: 20%;"><strong>Plaza</strong><br/>001</p>
                                                <p style="width: 20%;"><strong>Swift</strong><br/>BNMXMXMM</p>
                                            </span>
                                        </div>
                                    </div>
                                    </div>
                                    <div class="border-anuncio_realice_pago">
                                        <span class="row-datos_pago">                                     
                                            <p><strong>Importante:</strong></p><br/>
                                        </span>
                                        <span class="row-datos_pago">                                     
                                            <p>Con el fin de agilizar la identificación de su pago, le agradeceremos indicar en el campo de referencia alfanumérica del depósito el número de pedido y/o nombre del pedido.</p>
                                        </span>
                                        <span class="row-datos_pago">
                                            <p><strong>Número de pedido:&nbsp;&nbsp;&nbsp;</strong> 
                                                <asp:Label ID="lbl_numero_pedido_bottom" class="is-select-all" runat="server"></asp:Label>
                                            </p>                                      
                                        </span>
                                        <span class="container-btn_realice_pago">
                                            <asp:HyperLink ID="btn_finalizar_compra" Visible="false" class="is-btn-green-pago is-m-auto" runat="server" Text="Ya realicé el pago"></asp:HyperLink>
                                        </span>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_tarjeta" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btn_paypal" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btn_transferencia" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="desgloce_ticket is-flex is-flex-col is-justify-start is-items-center">
                        <asp:HyperLink ID="btn_regresar_resumen" runat="server">Regresar al resumen</asp:HyperLink>
                        <div style="border: 1px solid #b7b7b7; border-radius: 8px; height: fit-content;">
                            <table style="width: 100%;">
                                <thead style="border-bottom: 1px solid #b7b7b7;">
                                    <tr>
                                        <td colspan="2" style="padding: 0.75rem 0.75rem 0.75rem 1.5rem;"><strong>Desglose</strong></td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td style="padding: 0.75rem 0rem 0.25rem 1.5rem;">Productos:</td>
                                        <td style="padding: 0.75rem 1.5rem 0.5rem 0; text-align: end;">
                                            <asp:Label ID="lbl_productos" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 0.25rem 1.5rem;">Descuentos:</td>
                                        <td style="padding: 0.25rem 1.5rem; text-align: end;">
                                            <asp:Label ID="lbl_descuento" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 0.25rem 1.5rem;">Envío (estándar):</td>
                                        <td style="padding: 0.25rem 1.5rem; text-align: end;">
                                            <asp:Label ID="lbl_envio" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 0.25rem 1.5rem;">Subtotal:</td>
                                        <td style="padding: 0.25rem 1.5rem; text-align: end;">
                                            <asp:Label ID="lbl_subtotal" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 0.25rem 0rem 0.75rem 1.5rem;">Impuestos:</td>
                                        <td style="padding: 0.25rem 1.5rem 0.75rem 0rem; text-align: end;">
                                            <asp:Label ID="lbl_impuestos" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                                <tfoot style="border-top: 1px solid #b7b7b7;">
                                    <tr>
                                        <td style="padding: 0.75rem 0rem 0.75rem 1.5rem;">Total:</td>
                                        <td style="padding: 0.75rem 1.5rem 0.75rem 0rem; text-align: end;">
                                            <asp:Label ID="lbl_total" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                        <asp:HyperLink ID="btn_regresar" Visible="false" runat="server" Text="Regresar al resumen"></asp:HyperLink>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <style>
        #body_btn_tarjeta.aspNetDisabled, #body_btn_paypal.aspNetDisabled, #body_btn_transferencia.aspNetDisabled {
            border: none;
            border-radius: 6px;
            display: inline-block;
            height: 36px;
            line-height: 36px;
            padding: 0 16px;
            text-transform: none;
            vertical-align: middle;
            -webkit-tap-highlight-color: transparent;
            text-decoration: none;
            color: #9f9f9f;
            background-color: #f4f4f6;
            text-align: center;
            font-weight: bold;
            letter-spacing: .5px;
            -webkit-transition: background-color .2s ease-out;
            transition: background-color .2s ease-out;
            cursor: default;
            font-size: 12px;
            outline: 0;
            box-shadow: 0 2px 2px 0 rgba(0,0,0,0.14),0 3px 1px -2px rgba(0,0,0,0.12),0 1px 5px 0 rgba(0,0,0,0.2);
            margin-right: 2rem;
        }

        .desgloce_ticket {
            margin-top: 2.25rem
        }

        @media only screen and (min-width: 700px) {

        .container_movil-datos_pago_transferencia {
            display: none;
        }

        .container-pago {
            margin-top: 3rem;
        }

        .border-datos_pago_transferencia {
            border: 1px solid #b7b7b7;
            border-radius: 8px;
            margin: 2rem 1rem 1rem 0rem;
        }

        .datos_pago_transferencia {
            padding: 1rem;
            font-size: 0.75rem;
        }

        .title-datos_pago_transferencia {
            background-color: #d9d9d9;
            height: 2.25rem;
            font-size: 1rem;
            display: flex;
            align-items: center;
        }

        .title-datos_pago_transferencia > h5 {
            padding-left: 1rem;
            margin-bottom: 0px;
        }

        .container-datos_pago_transferencia {
            width: 100%;
            display: flex;
            justify-content: space-between;
        }

        .column-datos_pago {
            display: flex;
            flex-direction: column;
            float: left;
            font-size: 0.75rem;
        }

        .border-anuncio_realice_pago {
            margin: 1rem auto 3rem 1rem;
            padding-right: 1rem;
        }

        .row-datos_pago {
            display: flex;
            flex-direction: row;
            width: 100%;
            font-size: 0.75rem;
        }

        .container-btn_realice_pago {
            display: flex;
            width: 100%;
        }

        .is-btn-green-pago {
            border: none;
            border-radius: 6px;
            display: inline-block;
            height: 36px;
            line-height: 36px;
            padding: 0 16px;
            text-transform: none;
            vertical-align: middle;
            -webkit-tap-highlight-color: transparent;
            text-decoration: none;
            color: #fff;
            background-color: #119100;
            text-align: center;
            font-weight: bold;
            letter-spacing: .5px;
            -webkit-transition: background-color .2s ease-out;
            transition: background-color .2s ease-out;
            cursor: pointer;
            font-size: 12px;
            outline: 0;
            box-shadow: 0 2px 2px 0 rgba(0,0,0,0.14),0 3px 1px -2px rgba(0,0,0,0.12),0 1px 5px 0 rgba(0,0,0,0.2);
        }      
    }

    @media only screen and (max-width: 700px) {
    
    .container-metodo_pago {
        display: flex;
        flex-direction: column-reverse;
    }

    .container_movil-datos_pago_transferencia {
        display: block;
    }

    .container_movil-metodo_pago {
        order: 1;
    }

    .container-pago {
        margin-top: 1rem;
    }

    .border-datos_pago_transferencia {
            border: 1px solid #b7b7b7;
            border-radius: 8px;
            margin: 2rem 1rem 1rem 0rem;
        }

        .datos_pago_transferencia {
            padding: 1rem 1rem 0rem 1rem;
            font-size: 0.75rem;
        }

        .title-datos_pago_transferencia {
            background-color: #d9d9d9;
            height: 3rem;
            font-size: 1rem;
            display: flex;
            align-items: center;
        }

        .title-datos_pago_transferencia > h5 {
            padding-left: 1rem;
        }

        .container-datos_pago_transferencia {
            display: none;
        }

        .column-datos_pago {
            display: flex;
            flex-direction: column;
            float: left;
            font-size: 0.75rem;
        }

        .border-anuncio_realice_pago {
            margin: 1rem auto 3rem 1rem;
        }

        .movil-datos_pago {
            display: inline-flex;
            font-size: 0.75rem;
            padding: 0rem 1rem;
            width: 100%;
            justify-content: space-between;
        }

        .row-datos_pago {
            display: flex;
            flex-direction: row;
            width: 100%;
            font-size: 12px;
            padding-right: 2rem;
        }

        .container-btn_realice_pago {
            display: flex;
            width: 100%;
            margin-top: 1rem;
        }

        .is-btn-green-pago {
            border: none;
            border-radius: 6px;
            display: inline-block;
            height: 36px;
            line-height: 36px;
            padding: 0 16px;
            text-transform: none;
            vertical-align: middle;
            -webkit-tap-highlight-color: transparent;
            text-decoration: none;
            color: #fff;
            background-color: #119100;
            text-align: center;
            font-weight: bold;
            letter-spacing: .5px;
            -webkit-transition: background-color .2s ease-out;
            transition: background-color .2s ease-out;
            cursor: pointer;
            font-size: 12px;
            outline: 0;
            box-shadow: 0 2px 2px 0 rgba(0,0,0,0.14),0 3px 1px -2px rgba(0,0,0,0.12),0 1px 5px 0 rgba(0,0,0,0.2);
        }

        .container-metodo_pago > div:nth-child(1) {
            display: flex;
            flex-direction: column;
            justify-content: center;
        }
    }

        #body_btn_regresar_resumen {
            margin-bottom: 3rem;
            float: right;
            width: 100%;
            text-align: end;
            text-decoration: none;
        }

        @media only screen and (min-width: 1600px) {
            .container-pay-process {
                width: 40%;
                margin: 1rem auto;
            }
        }

        @media only screen and (max-width: 1000px) {
        .margin-metodos_pago {
            width: auto;
            margin: auto;
        }
        }

        @media only screen and (min-width: 1000px) {
            .container-pay-process {
                width: 60%;
                margin: 1rem auto;
            }

            #body_btn_regresar_resumen {
                margin-top: 2rem;
                margin-bottom: 1rem;
            }

            .margin-metodos_pago {
                width: 100%;
            }
        }

        @media only screen and (min-width: 1000px) and (max-width: 1200px) {
        
        .desgloce_ticket {
            margin: 0;
        }
        }

        @media only screen and (min-width: 1200px) {
            .icono-formas_pago {
                width: 450px;
                height: 50px;
            }

            .container-metodo_pago {
                margin-top: 2rem;
            }

            .text-aceptamos_formas_pago {
                margin-right: 1rem;
                margin: 1rem;
            }

            .container-metodo_pago > div:nth-child(1) {
                display: flex;
                flex-direction: row;
            }
        }

        @media only screen and (max-width: 1200px) {

            .container-pago {
                margin-left: 2rem;
                margin-right: 2rem;
            }

            .text-metodo_pago {
                font-size: 1rem;
            }

            .text-aceptamos_formas_pago > strong:nth-child(1) {
                font-size: 1rem;
            }

            .icono-formas_pago {
                width: 350px;
                height: 40px;
            }

            .container-metodo_pago {
                margin-top: 0rem;
            }

            .text-aceptamos_formas_pago {
                margin: 0.25rem;
            }

            #body_up_pasarelaPago .is-btn-gray {
                margin-right: 1rem;
            }

            .desgloce_ticket {
                margin: auto;
            }

            .container-pago > div > div {
                flex-direction: column;
                align-items: first baseline;
            }

            .container-metodo_pago > div:nth-child(1) {
                display: flex;
                flex-direction: column;
            }

            .container_movil-metodo_pago {
                order: 1;
            }

            .container-metodo_pago > div:nth-child(1) > div:nth-child(1) {
                margin: 2rem auto 0rem auto;
            }

            .container-pago > div > div > div:nth-child(1) {
                margin-bottom: 2rem;
            }
        }

        @media only screen and (min-width:500px) and (max-width: 700px) {

            .container-metodo_pago > div:nth-child(1) > div:nth-child(2) {
                width: 380px;
            }

            .text-resumen {
                font-size: 0.8rem;
            }

            .container-metodo_pago table {
                font-size: 12px;
            }

            #body_btn_regresar_resumen {
                margin-bottom: 1rem !important;
                margin-top: 2rem !important;
                font-size: 12px;
            }

            .text-aceptamos_formas_pago > strong:nth-child(1) {
                font-size: 0.75rem;
            }

            .text-aceptamos_formas_pago {
                margin: 0.5rem 0.5rem 0.5rem 0rem;
            }

            .is-w-full > div:nth-child(1) {
                margin-bottom: 1rem;
            }

            .container-pago {
                margin-left: 1rem;
                margin-right: 1rem;
            }

            .container-metodo_pago p:nth-child(1) > strong:nth-child(1) {
                font-size: 0.75rem;
            }

            #body_up_pasarelaPago .is-btn-gray {
                height: 36px;
                line-height: 36px;
                font-size: 10px;
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

            .icono-formas_pago {
                width: 270px;
                height: 36px;
            }

            #body_btn_regresar_resumen {
                margin-top: 0rem;
                margin-bottom: 1rem;
                font-size: 8px;
            }

            .text-metodo_pago {
                font-size: 0.75rem;
            }

            .container-metodo_pago table {
                font-size: 10px;
            }

            .container-formas_pago {
                flex-direction: column;
            }

            .text-aceptamos_formas_pago > strong:nth-child(1) {
                font-size: 10px;
            }

            .container-metodo_pago p:nth-child(1) > strong:nth-child(1) {
                font-size: 7px;
            }

            .movil-datos_pago {
                font-size: 7px;
            }

            span.movil-datos_pago:nth-child(2) > p:nth-child(1) {
                font-size: 7px;
            }

            .text-aceptamos_formas_pago {
                margin: 0.5rem 0.5rem 0.5rem 0rem;
            }

            .container-pago > div > div > div:nth-child(1) {
                margin-bottom: 0px;
            }

            .border-datos_pago_transferencia {
                margin: 0rem 1rem 1rem 0rem;
            }

            .datos_pago_transferencia {
                font-size: 7px;
            }

            .container-metodo_pago > div:nth-child(1) > div:nth-child(1) {
                margin: 0rem auto;
            }

            .container_movil-datos_pago_transferencia > hr {
                margin: 0px;
            }

            .title-datos_pago_transferencia > h5:nth-child(1) {
                font-size: 12px;
                font-weight: bold;
                margin-bottom: 0px;
            }

            .title-datos_pago_transferencia {
                height: 3rem;
                height: 2rem;
            }

            span.row-datos_pago:nth-child(2) > p:nth-child(1) {
                font-size: 7px;
            }

            span.movil-datos_pago:nth-child(6) > p:nth-child(1) {
                margin-bottom: 0rem;
            }

            .is-btn-green-pago {
                font-size: 9px;
                height: 26px;
                line-height: 26px;
            }

            .container-pago {
                margin-left: 0.5rem;
            }

            #body_up_pasarelaPago .is-btn-gray {
                font-size: 7px;
                height: 26px;
                line-height: 26px;
                padding: 0px 6px !important;
                margin-right: 0.5rem !important;
                margin-bottom: 2rem;
            }

            #body_btn_tarjeta.aspNetDisabled,
            #body_btn_paypal.aspNetDisabled,
            #body_btn_transferencia.aspNetDisabled {
                font-size: 7px;
                height: 26px;
                line-height: 26px;
                padding: 0px 6px !important;
                margin-right: 0.5rem !important;
            }

            .svg_resumen, .svg_pago,
            .svg_finalizar, .svg_resumen_puntos,
            .svg_pago_puntos {
                width: 1.5rem;
                height: 1.5rem;
            }
        }
    </style>
</asp:Content>
