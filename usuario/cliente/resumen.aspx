<%@ Page Language="C#" AutoEventWireup="true" Async="true" MaintainScrollPositionOnPostback="true" MasterPageFile="~/bootstrap/basic.master" CodeFile="resumen.aspx.cs" Inherits="usuario_cliente_resumen" %>

<%@ Register Src="~/usuario/cliente/cliente-header.ascx" TagPrefix="header" TagName="menuGeneral" %>
<%@ Register TagPrefix="uc" TagName="ddlAsignarUsuarioAsesor" Src="~/userControls/operaciones/PedidosUsuarioSeguimientoUC.ascx" %>
<%@ Register TagPrefix="uc" TagName="EdicionDetallesDeEnvioPedido" Src="~/userControls/operaciones/EditarCostoDeEnvioPedido.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <!-- Event snippet for Inclusión en el carrito conversion page -->
    <script> gtag('event', 'conversion', { 'send_to': 'AW-10903694501/B-zjCMjt3MEDEKXZpM8o' }); </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="body">
    <header:menuGeneral ID="menuGeneral" runat="server" />
    <asp:HiddenField ID="hf_id_pedido" runat="server" />

    <asp:HiddenField ID="hf_pedido_tipo_envio" runat="server" />
    <asp:HiddenField ID="hf_id_pedido_direccion_envio" runat="server" />
    <div class="container">
        <div class="row">
            <div class="col">
                <h1 class="h2">Resumen de pedido "<asp:Literal ID="lt_nombre_operacion" runat="server"></asp:Literal>"</h1>
            </div>
        </div>

        <div class="row">
            <div class="col  col-12 col-xs-12 col-sm-12 col-md-5 col-xl-6">
                <h1 class="h3">#<asp:Literal ID="lt_numero_pedido" runat="server"></asp:Literal></h1>
                <p>
                    Creado el <strong>
                        <asp:Label ID="lbl_fecha_creacion" runat="server"></asp:Label></strong>
                    Usuario: <strong>
                        <asp:Literal ID="lt_usuario_cliente" runat="server"></asp:Literal></strong>
                </p>

                <div id="content_pedido_cancelado" visible="false" runat="server">
                    <div class="alert alert-danger" role="alert">
                        <h4 class="alert-heading">Pedido cancelado</h4>
                        <p>Se encontró una solicitud de cancelación de pedido o bien, tu pedido ya se encuentra cancelado</p>
                        <strong>Mensaje:</strong>
                        <asp:Label ID="lbl_motivoCancelacion" runat="server"></asp:Label>
                    </div>
                </div>

                <div id="Content_AsesorSeguimiento" runat="server" visible="false" class="card mb-4">
                    <div class="card-body  border-top border-3">
                        <h4 class="card-title">Seguimiento asesor </h4>
                        <h6 id="H1" runat="server" class="card-subtitle mb-2 text-muted"></h6>
                        <p id="P1" runat="server" class="card-text"></p>
                        <p id="P2" runat="server">

                            <uc:ddlAsignarUsuarioAsesor ID="AsignarUsuarioAsesor" runat="server" />
                        </p>


                    </div>
                </div>

                <div class="card">
                    <div class="card-body  border-top border-3">
                        <h4 class="card-title">Facturación </h4>
                        <h6 id="facturacion_title" runat="server" class="card-subtitle mb-2 text-muted"></h6>
                        <p id="facturacion_desc" runat="server" class="card-text"></p>
                        <p id="ContentFacturacionUsoCFDI" runat="server">
                            <strong>
                                <label for="ddl_UsoCFDI" class="form-label">Uso de CFDI</label><span class="text-danger">*</span></strong>
                            <asp:DropDownList ID="ddl_UsoCFDI" class="form-select"
                                OnSelectedIndexChanged="ddl_UsoCFDI_SelectedIndexChanged" AutoPostBack="true" runat="server">
                            </asp:DropDownList>
                        </p>
                        <asp:LinkButton runat="server" ID="btn_sin_factura" OnClick="btn_sin_factura_Click"
                            class="card-link btn btn-secondary">
                             Sin factura </asp:LinkButton>
                        <asp:HyperLink runat="server" ID="link_cambiar_direcc_facturacion"
                            class="card-link btn btn-secondary">
                                Cambiar
                        </asp:HyperLink>
                    </div>
                </div>
                <div class="card mt-4  ">
                    <div class="card-body  border-top border-3">
                        <h4 class="card-title">Envio</h4>
                        <uc:EdicionDetallesDeEnvioPedido ID="uc_EdicionDetallesDeEnvioPedido" runat="server" />
                        <h6 id="metodo_envio_title" class="card-subtitle mb-2 text-muted" runat="server"></h6>
                        <p id="metodo_envio_desc" class="card-text" runat="server"></p>
                        <div id="msg_alert_envio" visible="false" class="alert alert-warning" role="alert" runat="server"></div>
                        <asp:LinkButton ID="btn_Borrar_msg_alert_envio" CssClass="mb-2" Visible="false" OnClick="btn_Borrar_msg_alert_envio_Click" runat="server">
                            Borrar mensaje de error envío
                        </asp:LinkButton>
                        <div class="Conteng_msg_envioNota">
                        </div>

                        <div class="dropdown">
                            <asp:HyperLink runat="server" ID="btn_cambiar_metodo_envio"
                                class="card-link btn btn-secondary">
                                Cambiar método de envío
                            </asp:HyperLink>
                        </div>

                    </div>
                </div>
                <div class="card  mt-4  ">
                    <div class="card-body  border-top border-3">
                        <h4 class="card-title">Contacto </h4>
                        <h6 id="contacto_title" class="card-subtitle mb-2 text-muted" runat="server"></h6>
                        <p id="contacto_desc" class="card-text" runat="server"></p>

                        <asp:HyperLink runat="server" ID="link_cambiar_contacto"
                            class="card-link btn btn-secondary">
                               Cambiar/Establecer
                        </asp:HyperLink>
                    </div>
                </div>
                <div class="d-grid gap-2 mt-3">
                    <h3>Aclaraciones</h3>
                    <ul>
                        <li>Los <strong>costos de envió</strong> podrían ser re calculados y estos pueden ser mayores o menores a lo mostrado.</li>
                        <li>Algunos productos por seguridad, requieren un <strong>seguro de envío</strong> el cuál tu asesor te informará del costo.</li>
                        <li>Una vez registrado el pago, solo podrás actualizar el método de envío con ayuda de un asesor. Los datos de facturación o contacto podrás actualizarlos normalmente.</li>
                    </ul>
                    <div class="content_msg_confirmacion_pedido" runat="server"></div>



                </div>
                <asp:Panel ID="Pago_Pendiente" Visible="false" CssClass="mt-4 col col-12 col-xs-12 col-sm-12 col-md-12 col-xl-12" runat="server">

                    <p class="h5">Elige el método de pago </p>

                    <asp:HyperLink ID="link_pago_santander" class="btn btn-lg btn-primary " runat="server">
                       <img src="/img/webUI/visa_mastercard.png" style="width: 120px;" class="img-thumbnail" />
                        <i class="fas fa-lock"></i> Tarjeta crédito/débito</asp:HyperLink>
                    <p class="mt-2">Otras formas de pago</p>
                    <asp:HyperLink ID="link_pago_paypal" class="btn btn-outline-primary btn-sm" runat="server">
                        <i class="fas fa-lock"></i> PayPal</asp:HyperLink>

                    <a data-bs-toggle="modal" data-bs-target="#modal_deposito_trans" class="btn btn-outline-primary btn-sm">Transferencia o depósito</a>
                    <div class="alert alert-warning mt-4" role="alert">
                        <strong>Aviso</strong>  No sé ha confirmado un pago aún.
                    </div>
                </asp:Panel>
                <asp:Panel ID="Pago_Confirmado" Visible="false" CssClass="mt-4 col col-12 col-xs-12 col-sm-12 col-md-12 col-xl-12" runat="server">
                    <p class="h5">
                        Pago ya realizado vía
                        <asp:Label ID="Tipo_pago" runat="server"></asp:Label>
                    </p>
                </asp:Panel>
            </div>
            <div class="col col-12  col-xs-12 col-sm-12 col-md-7 col-xl-6">





                <p class="h5 ">Productos</p>

                <asp:ListView ID="lv_productos" OnItemDataBound="lv_productos_ItemDataBound" Visible="true" runat="server">

                    <LayoutTemplate>
                        <ul class="list-group mb-3">
                            <div id="itemPlaceholder" runat="server"></div>

                        </ul>

                    </LayoutTemplate>

                    <ItemTemplate>
                        <li class="list-group-item d-flex justify-content-between lh-sm">
                            <asp:Image ID="img_producto" class="img-fluid" Style="width: 100px;" runat="server" /><div>


                                <h6 class="my-0"><%#Eval("productos.numero_parte") %>
                                </h6>

                                <small class="text-muted"><%#Eval("productos.descripcion") %>
                                    <br />
                                    <asp:Literal ID="lt_cantidad" Text='<%#Eval("productos.cantidad") %>' runat="server"> </asp:Literal>
                                    pza
                                        x
                            <asp:Literal ID="lt_precio_unitario" Text=' <%#Eval("productos.precio_unitario") %>' runat="server"></asp:Literal>
                                </small>

                            </div>
                            <span class="text-muted">
                                <strong>
                                    <asp:Literal ID="lt_precio_total" Text='<%#Eval("productos.precio_total") %>' runat="server">  </asp:Literal>
                                </strong>

                            </span>

                        </li>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        No hay productos
                    </EmptyDataTemplate>
                </asp:ListView>

                <table class="table table-sm">
                    <thead>
                        <tr>
                            <th scope="col">Concepto</th>
                            <th class="text-end" scope="col">Total</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>Productos</td>
                            <td class="text-end"><strong>
                                <asp:Label ID="lbl_total_productos" runat="server"></asp:Label></strong></td>
                        </tr>
                        <tr>
                            <td>Envio (estimado)</td>
                            <td class="text-end"><strong>
                                <asp:Label ID="lbl_envio" runat="server"></asp:Label></strong></td>
                        </tr>

                        <tr>
                            <td>Subtotal</td>
                            <td class="text-end"><strong>
                                <asp:Label ID="lbl_subtotal" runat="server"></asp:Label></strong></td>
                        </tr>
                        <tr>
                            <td>Impuestos</td>
                            <td class="text-end"><strong>
                                <asp:Label ID="lbl_impuestos" runat="server"></asp:Label></strong></td>
                        </tr>
                        <tr class="table-active">
                            <td>Total</td>
                            <td class="text-end"><strong>
                                <asp:Label ID="lbl_total" runat="server"></asp:Label></strong></td>
                        </tr>
                    </tbody>
                </table>

            </div>
        </div>
        <div class="row">
            <div class="col col-12  col-xs-12 col-sm-12 col-md-7 col-xl-6">
                <div id="content_msg_cancelar_pedido"></div>
                <a id="link_modal_cancelar_pedido" runat="server" data-bs-toggle="modal" data-bs-target="#modal_cancelar_pedido"
                    class="btn btn-outline-danger btn-sm">Cancelar pedido</a>
            </div>
        </div>
    </div>

    <!-- Modal cancelar pedido -->
    <div class="modal  " id="modal_cancelar_pedido" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog  modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Cancelar pedido</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <p>
                        Una vez que solicites la cancelación de pedido no podras realizar cambios en este.<br />
                        Las canciones deben cumplir con 
          nuestros <a target="_blank" href="/informacion/terminos-y-condiciones-de-compra.aspx">términos y condiciones</a> y de 
          <a target="_blank" href="/informacion/devoluciones-y-garantias.aspx">devoluciones y garantías.</a>
                    </p>
                    <div class="mb-3">
                        <label for="txt_motivo_cancelacion" class="form-label">Motivo de cancelación</label>
                        <asp:TextBox ID="txt_motivo_cancelacion" ClientIDMode="Static" class="form-control" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
                    </div>
                    <asp:LinkButton OnClick="btn_cancelar_pedido_Click" OnClientClick="BootstrapClickLoading(this);"
                        ID="btn_cancelar_pedido" class="btn btn-danger" runat="server"> Cancelar pedido </asp:LinkButton>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar ventana</button>

                </div>
            </div>
        </div>
    </div>
    <!-- FIN Modal cancelar pedido -->



    <!-- Modal cuentas bancarias -->
    <div class="modal  " id="modal_deposito_trans" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog  modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Transferencia o deposito</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">


                    <img src="/img/informacion/logo-banamex.svg" style="width: 200px;" class="img-thumbnail" />
                    <h5 class="card-title mt-2">Datos de la cuenta</h5>
                    <p>
                        Número de operación
            <strong>Razón social:</strong> Insumos Comerciales de Occidente S.A. de C.V.
                            <br>
                        <strong>RFC: </strong>ICO990224H93<br>
                        <strong>Dirección:</strong> Plutarco Elías Calles 276, Colonia Tlazintla, Ciudad de México, México. C.P. 08710, Delegación: Iztacalco<br>
                        <strong>Banco:</strong> Banamex
                    </p>
                    <div class="table-responsive ">
                        <table class="table table-striped">
                            <tbody>
                            </tbody>
                            <thead>
                                <tr>
                                    <th>Moneda </th>
                                    <th>Sucursal</th>
                                    <th>Cuenta</th>
                                    <th>Clabe</th>
                                    <th>Plaza</th>
                                    <th>Swift</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>Moneda Nacional</td>
                                    <td>0269</td>
                                    <td>7782861</td>
                                    <td>002180026977828615</td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Dólares (USD)</td>
                                    <td>414</td>
                                    <td>9412714</td>
                                    <td>002180041494127146</td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Dólares (USD) desde el extranjero</td>
                                    <td>414</td>
                                    <td>9412714</td>
                                    <td>002180041494127146</td>
                                    <td>001</td>
                                    <td>BNMXMXMM</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <p>
                        <span class="h4">Importante</span><br />

                        Con el fin de agilizar la identificación de su pago, le agradeceremos indicar en el campo de referencia alfanumérica del depósito
                           el número de pedido y/o nombre del pedido.
             <br />
                        <br />
                        <strong>Número de pedido: </strong>
                        <span class="text-success"><%= lt_numero_pedido.Text %></span>
                    </p>


                    <asp:UpdatePanel ID="up_ConfirmarDeposito" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="ContentGenerarReferenciaTransferencia" runat="server">
                                Si ya realizaste la transferencia da click en el siguiente botón:
                      <br />
                                <asp:LinkButton ID="btn_pago_transferencia" OnClick="btn_pago_transferencia_Click"
                                    class="btn btn-success"
                                    runat="server">Ya he realizado la transferencia</asp:LinkButton>
                            </asp:Panel>
                            <asp:Panel ID="ContentReferenciaTransferencia" Visible="false" runat="server">
                                <p class="h4">Gracias por tu pago</p>
                                <p>En unos momentos se confirmara tu transferencia y un asesor se comunicará contigo.</p>
                                <asp:Label ID="lbl_Referenca_Transferencia" runat="server" Text=""></asp:Label>

                                <asp:Panel ID="Content_Pago_Datos_Transferencia_Asesor" Visible="false" runat="server">
                                    <div class="mb-3">

                                        <label for="txt_TranfenciaReferenciaAsesor" class="form-label">Asesor: Ingresa algún texto referente a esta transferencia, solo lo verás tú.</label>
                                        <asp:TextBox ID="txt_TranfenciaReferenciaAsesor" ClientIDMode="Static" TextMode="Multiline" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="mb-3">

                                        <div class="form-check">
                                            <asp:CheckBox ID="chk_TranfenciaConfirmadaAsesor" CssClass="form-check-input" ClientIDMode="Static" runat="server" />
                                            <label class="form-check-label" for="chk_TranfenciaConfirmada">
                                                Pago confirmado
                                            </label>
                                        </div>
                                    </div>
                                    <asp:LinkButton ID="btn_guardarTransferenciaDatosAsesor" OnClick="btn_guardarTransferenciaDatosAsesor_Click"
                                        CssClass="btn btn-primary" runat="server">Guardar</asp:LinkButton>
                                </asp:Panel>
                            </asp:Panel>
                            <div id="content_msg_transfrencia"></div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_pago_transferencia" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btn_guardarTransferenciaDatosAsesor" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>

                </div>
            </div>
        </div>
    </div>
    <!-- FIN Modal cuentas bancarias -->


</asp:Content>


