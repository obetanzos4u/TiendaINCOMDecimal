<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_pedido_visualizar.ascx.cs" Inherits="userControls_pedido_visualizar" %>

<asp:HiddenField ID="hf_id" runat="server" />
<div id="content_operacion" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="font-size: 14px; background: #1a163114; color: #525252;">
        <tbody style="font-family: Helvetica,Arial,sans-serif; text-align: left;">
            <tr>
                <td>
                    <table id="content_operacion_center" width="100%" border="0" align="center" cellpadding="5" cellspacing="0"
                        style="font-family: Helvetica,Arial,sans-serif; background: white;" runat="server">
                        <tbody>
                            <tr>
                                <td>&nbsp; </td>
                            </tr>
                            <tr>
                                <td colspan="3" bgcolor="#FFFFFF">
                                    <table width="97%" border="0" align="center" cellpadding="0" cellspacing="2">
                                        <tbody>
                                            <tr>
                                                <td colspan="3" style="text-align: center;">
                                                    <br>
                                                    <img id="logo_header" alt="Incom.mx" src="<%= Request.Url.GetLeftPart(UriPartial.Authority)%>/img/webUI/company_logo_wide.jpg"
                                                        runat="server"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="background-color: #4CAF50 !important;">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td id="header_infoBasica" runat="server" height="33">Estimado(a):
                      <strong>
                          <asp:Label ID="lbl_cliente_nombre" runat="server"></asp:Label></strong>
                                                    <br>
                                                    Gracias por comprar en INCOM, a continuación presentamos la información detallada:<br>
                                                    <hr />
                                                    <asp:Label ID="lbl_nombre_pedido" lang="nombre_cotizacion" runat="server"></asp:Label>
                                                    |  Número de operación #: <strong>
                                                        <asp:Label ID="lbl_numero_operacion" lang="numero_operacion" runat="server"></asp:Label>
                                                    </strong>
                                                    <br>
                                                    Fecha de creación del pedido: <strong>
                                                        <asp:Label ID="lbl_fecha_creacion" lang="fecha_creacion" runat="server"></asp:Label>
                                                    </strong>


                                                    <br>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <table width="97%" border="0" cellspacing="2" cellpadding="0" style="margin: 0px 1%;">
                                                        <tr>
                                                            <td colspan="3" style="font-weight: bold; width: 50%; padding: 10px; border-bottom: solid 5px rgb(0, 103, 199);">Datos de contacto</td>

                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td>
                                                                            <div style="padding: 5px;">
                                                                                Nombre:
                                                <strong>
                                                    <asp:Label ID="lbl_nombre" lang="cliente_nombre" runat="server"></asp:Label>
                                                    <asp:Label ID="lbl_apellido_paterno" lang="apellido_paterno" runat="server"></asp:Label>
                                                    <asp:Label ID="lbl_apellido_materno" lang="apellido_materno" runat="server"></asp:Label>
                                                </strong>Email: 
                                                 <strong>
                                                     <asp:Label ID="lbl_email" lang="email" runat="server"></asp:Label></strong>
                                                                            </div>
                                                                            <div style="padding: 5px;">
                                                                                Teléfono:
                                                  <strong>
                                                      <asp:Label ID="lbl_telefono" lang="telefono" runat="server"> </asp:Label></strong>&nbsp;
                                                 Celular:
                                                 <strong>
                                                     <asp:Label ID="lbl_celular" lang="celular" runat="server"></asp:Label>
                                                 </strong>
                                                                            </div>
                                                                    </tr>
                                                                </table>
                                                            </td>

                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td colspan="3" height="194" valign="top">
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                                        <tbody>
                                                            <tr>
                                                                <td style="background-color: rgb(0, 103, 199); color: white; font-weight: bold; padding: 10px;">Dirección de facturación</td>
                                                                <td colspan="2" style="background-color: rgb(0, 103, 199); color: white; font-weight: bold; padding: 5px;">Dirección de envío</td>
                                                            </tr>
                                                            <tr>
                                                                <td lang="direccion_facturacion" style="width: 49%; border: 1px solid rgba(0,84,163,1.00);" valign="top">
                                                                    <asp:Label ID="lbl_facturacion_razon_social" lang="facturacion_razon_social" runat="server"></asp:Label>&nbsp;
                                            <asp:Label ID="lbl_facturacion_rfc" lang="facturacion_rfc" runat="server"></asp:Label>&nbsp;
                                            <asp:Label ID="lbl_facturacion_calle" lang="facturacion_calle" runat="server"></asp:Label>&nbsp;
                                            <asp:Label ID="llbl_facturacion_numero" lang="facturacion_numero" runat="server"></asp:Label>&nbsp;
                                            <asp:Label ID="lbl_facturacion_colonia" lang="facturacion_colonia" runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_facturacion_delegacion_municipio" lang="facturacion_delegacion_municipio" runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_facturacion_estado" lang="facturacion_estado" runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_facturacion_codigo_postal" lang="facturacion_codigo_postal" runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_facturacion_pais" lang="facturacion_pais" runat="server"></asp:Label>&nbsp;
                                                                </td>
                                                                <td lang="direccion_envio" style="width: 49%; border: 1px solid rgba(0,84,163,1.00);" colspan="2" valign="top">
                                                                    <asp:Label ID="lbl_envio_calle" lang="envio_calle" runat="server"></asp:Label>&nbsp;
                                            <asp:Label ID="lbl_envio_numero" lang="envio_numero" runat="server"></asp:Label>&nbsp;
                                            <asp:Label ID="lbl_envio_colonia" lang="envio_colonia" runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_envio_delegacion_municipio" lang="envio_delegacion_municipio" runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_envio_estado" lang="envio_estado" runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_envio_codigo_postal" lang="envio_codigo_postal" runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_envio_pais" lang="envio_pais" runat="server"></asp:Label>&nbsp;  
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td colspan="2">&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3" style="background-color: rgb(0, 103, 199); color: white; font-weight: bold; padding: 5px; text-align: left;">Condiciones de envío:</td>
                                                            </tr>
                                                                  <tr>
                                                                <td colspan="3">
                                                                    Método de envío: <asp:Label ID="lbl_metodoEnvio" runat="server"  ></asp:Label>
                                                                   &nbsp;  </td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td colspan="2">&nbsp;</td>
                                                            </tr>

                                                            <tr>
                                                             <td colspan="3" style="background-color: rgb(0, 103, 199); color: white; font-weight: bold; padding: 5px; text-align: left;">
                                                                  Comentarios:
                                                               
                                                                </td> <tr>
                                                                   <td colspan="3" >  <asp:Label ID="lbl_comentarios" runat="server"></asp:Label>
                                                                    &nbsp;<br />
  
                  </tr>

                                                               
                                                                </td>

                                                </td>

                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td colspan="2">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="background-color: rgb(0, 103, 199); color: white; font-weight: bold; padding: 5px;">Detalles de productos:</td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <div lang="compra">
                                                        <asp:ListView ID="lv_productos" OnItemDataBound="lv_productos_ItemDataBound" runat="server">
                                                            <LayoutTemplate>
                                                                <table id="productos" cellpadding="5" cellspacing="2" border="0" style="border-width: 0px; border-style: None; width: 100%">
                                                                    <thead style="display: table-header-group;">
                                                                        <tr style="background-color: rgb(0, 103, 199); color: white" align="center">


                                                                            <th>Información</th>
                                                                            <th style="text-align: center;">Cantidad</th>
                                                                            <th>Precio</th>

                                                                            <th>Total</th>

                                                                        </tr>
                                                                    </thead>

                                                                    <tbody>
                                                                        <div runat="server" id="itemPlaceholder"></div>
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr style="font-size: 12px; page-break-inside: avoid; text-align: left; <%# Container.DisplayIndex % 2 == 0 ? "": "background-color:#eaeaea73;" %>">
                                                                    <td style="text-align: left;">

                                                                        <table width="100%" border="0" cellspacing="5" cellpadding="5">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td rowspan="3" style="width: 5%;">
                                                                                        <asp:HyperLink ID="link_producto" Target="_blank" runat="server">
                                                                                            <asp:Image ID="img_producto" Style="width: 130px;" runat="server" />
                                                                                        </asp:HyperLink>

                                                                                    </td>
                                                                                    <td>
                                                                                        <div style="font-size: 16px; float: left; padding: 5px; color: rgba(25, 25, 25, 0.85);">
                                                                                            <strong>
                                                                                                <asp:Label ID="lbl_lvProductos_numero_parte" runat="server"></asp:Label>
                                                                                                - 
                                                                      <asp:Label ID="lbl_lvProductos_titulo" runat="server"></asp:Label>
                                                                                            </strong>
                                                                                        </div>
                                                                                        <asp:HyperLink ID="link_lvProductos_linkPDF" runat="server">
                                                                                            <asp:Image ID="img_lvProductos_linkPDF" runat="server" />

                                                                                        </asp:HyperLink>
                                                                                    </td>
                                                                                </tr>

                                                                                <tr>
                                                                                    <td valign="top" style="color: rgba(25, 25, 25, 0.8);">
                                                                                        <asp:Label ID="lbl_lvProductos_descripcion" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>


                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <asp:Label ID="lbl_lvProductos_cantidad" runat="server"></asp:Label>
                                                                        &nbsp;
                                               <asp:Label ID="lbl_lvProductos_unidad" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: right;">
                                                                        <asp:Label ID="lbl_lvProductos_precio_unitario" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: right;">
                                                                        <asp:Label ID="lbl_lvProductos_precio_total" runat="server"></asp:Label>
                                                                    </td>

                                                                </tr>

                                                            </ItemTemplate>

                                                            <EmptyDataTemplate>
                                                                <div class="grey lighten-4" style="padding: 20px;">
                                                                    <h5 class="center-align">Aún no hay productos para esta operación</h5>
                                                                </div>

                                                            </EmptyDataTemplate>
                                                        </asp:ListView>
                                                    </div>
                                                </td>
                                            </tr>
                                           
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td style="text-align: right"><strong>Costo Envío: </strong></td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lbl_envio" lang="envio" runat="server"></asp:Label></td>
                                            </tr>
                                          
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td   style="text-align: right"><strong>SubTotal: </strong></td>
                                                <td   style="text-align: right">
                                                    <asp:Label ID="lbl_subtotal" lang="subtotal" runat="server"></asp:Label></td>
                                            </tr>
                              <tr>
                                                <td>&nbsp;</td>
                                                <td style="text-align: right"><strong>Impuesto: </strong></td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lbl_impuestos" lang="impuestos" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td style="text-align: right"><strong>Total: </strong></td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lbl_total" lang="total" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td style="text-align: right"><strong>Tipo de cambio: </strong></td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lbl_tipo_cambio" lang="tipo_cambio" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td height="29">&nbsp;</td>
                            </tr>
                            <tr>
                                <td height="29">&nbsp;</td>
                            </tr>
                            <tr>
                                <td height="29">Formas de pago: depósito o transferencia a cuentas bancarias a nombre de Insumos Comerciales de Occidente S.A de C.V.<br>
                                    - Favor de incluir la&nbsp;<strong>Referencia / Operación #</strong>&nbsp;indicada arriba en depósito.</td>
                            </tr>
                            <tr>
                                <td height="29" align="center"><span style="color: white;" lang="tipo_operacion">pedido</span></td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                             <tr>
                                <td style="background-color: rgb(0, 103, 199); color: white; font-weight: bold; padding: 5px;">Cuentas bancarias:</td>
                            </tr>
                            <tr>
                                <tr>
                                    <td>Razón social:<strong> Insumos Comerciales de Occidente S.A. de C.V. </strong> <br />
                                        RFC: <strong>ICO990224H93</strong>
                                        <br />
                                        Dirección: <strong>Plutarco Elías Calles 276, Col. Tlazintla  Ciudad de México, México. C.P. 08710, Delegación: Iztacalco</strong>  <br />
                                        Banco:<strong> Banamex</strong>

                                    </td>
                                </tr>
                                <td>Banamex MN :<strong> Suc. 0269, Cta. 7782861, clabe 002180026977828615</strong></td>
                            </tr>
                            <tr>
                                <td>Banamex USD:<strong> Suc. 0414, Cta. 9412714, clabe 002180041494127146</strong></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>

                            <tr>
                                <!--- Inicio de atención --->

                                <td style="background-color: #607d8b !important; color: rgb(255, 255, 255); text-align: center; font-size: 18px;" height="38">
                                    <strong>Información</strong>

                                </td>
                            </tr>

                            <tr>
                                <td style="text-align: center">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td style="width: 50%;">
                                                    <div runat="server" id="content_img_asesor" style="float: left; margin: 5px 18px;">
                                                        <asp:Image ID="img_asesor" runat="server" />
                                                    </div>
                                                    <div style="padding: 3px 0px;">
                                                        <span style="font-size: 22px; color: #989898;">Operación creada por: </span>
                                                        <br />
                                                        <strong>
                                                            <asp:Label ID="lbl_operacion_creada_por_nombre" Style="color: #37474f;" runat="server"></asp:Label></strong>
                                                        (<asp:Label ID="lbl_operacion_creada_por_email" Style="color: #37474f;" runat="server"></asp:Label>)
                                                    </div>
                                                    <div style="padding: 3px 0px;">
                                                        <span style="font-size: 22px; color: #989898;">Documento emitido por el usuario:</span><br />
                                                        <strong>
                                                            <asp:Label ID="lbl_emitida_por" Style="color: #37474f;" runat="server"></asp:Label></strong>
                                                    </div>
                                                                                                        <div style="padding: 3px 0px;">
                                                        <span style="font-size: 20px; color: #989898;">Fecha de emisión de este documento</span><br />
                                                        <strong> <%= utilidad_fechas.obtenerCentral().ToString("D") %></strong>
                                                    </div>

                                                </td>
                                                <td   style="width: 50%;">
                                                    <div style="margin-top: 15px; padding: 7px 0px; color: #797979; clear: both;">
                                                        <strong><span style="color: #37474f;">INSUMOS COMERCIALES DE OCCIDENTE S.A. DE C.V.</span></strong>
                                                        <br />
                                                        Plutarco Elías Calles 276, Colonia Tlazintla. 
                                                        C.P. 08710, Iztacalco, Ciudad de México
                                                        <br />
                                                        <div style="margin: 10px 0px;font-weight:100;font-size:18px;">
                                                            <a style="color: #00af00;" 
                                                                href="https://www.google.com/maps/place/Incom/@19.397827,-99.111646,16z/data=!4m2!3m1!1s0x0:0xe8681194a59f3b5a?hl=es-ES">
                                                                Ubicación en Google Maps
                                                            </a>
                                                        </div>

                                                        <strong><span style="color: #37474f;">Para el D.F. y área metropolitana:</span> </strong>
                                                        (55) 5243-6900 
                                                               <br />
                                                        <strong><span style="color: #37474f;">Del interior sin costo:</span></strong>
                                                        01 800-INCOM(46266)-00
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left; font-size: 14px;">

                                                    <strong>Aclaraciones:</strong><br>
                                                    <ul>
                                                        <li>Los precios aquí mostrados deberán ser cubiertos en su totalidad a: <strong>Insumos Comerciales de Occidente S.A de C.V.</strong>
                                                        <li>No incluye comisión u otros gastos que pudieran generar terceros por el método/forma de pago realizado por el cliente.</li>
                                                        <li>Cualquier comisión o gasto generado por terceros deberán ser cubiertos por el cliente.</li>
                                                        <li>Sobre anticipos no se harán devoluciones.</li>
                                                        <li>Todas las cancelaciones causan un 20% de penalización.</li>
                                                        <li>Cambios y devoluciones únicamente dentro de los 15 días naturales una vez entregado el material.</li>
                                                        <li>Cancelaciones o re-facturación por modificación de datos fiscales únicamente dentro del mes de la operación.</li>
                                                        <li>Clientes con saldos vencidos no se le suministrará el material.</li>
                                                        <li>Cualquier incumplimiento en las condiciones de pago, dejará sin efecto las fechas comprometidas en entregas de pedidos pendientes.</li>
                                                        <li>Precios e inventario sujeto a cambios sin previo aviso.</li>
                                                        <li>Tiempo de entrega inicia a partir de pago en firme y/o confirmación de pedido.</li>
                                                        <li>Pago en pesos de factura en dólares será al tipo de cambio del&nbsp;<a href="http://www.dof.gob.mx/indicadores.php" target="_blank">Diario Oficial</a>&nbsp;del día en que efectúa su pago.</li>
                                                        <li>Imágenes mostradas son como referencia, solicitar a servicio técnico especificaciones.</li>
                                                    </ul>
                                                    <p>Aceptamos tarjeta VISA, MASTER CARD y AMERICAN EXPRESS</p>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <p style="font-size: 24px;"><a style="text-decoration: none;" href="https://www.incom.mx">www.incom.mx</a></p>
                                    <p>Síguenos en nuestras redes sociales</p>
                                    <p>
                                        &nbsp;&nbsp;<a href="https://www.facebook.com/incommexico" target="new">
                                            <img src="<%=Request.Url.GetLeftPart(UriPartial.Authority) %>/img/webUI/email/Facebook.png" alt="Facebook" width="43" height="46" title="Facebook"></a>
                                        &nbsp;&nbsp;<a href="https://twitter.com/incom_mx" target="new">
                                            <img src="<%=Request.Url.GetLeftPart(UriPartial.Authority)  %>/img/webUI/email/Twitter.png" alt="Twiiter" width="43" height="46" title="Twiiter"></a>
                                        &nbsp;&nbsp; <a href="https://www.youtube.com/user/incommx?sub_confirmation=1" target="new">
                                            <img src="<%=Request.Url.GetLeftPart(UriPartial.Authority)  %>/img/webUI/email/YouTube.png" alt="Youtube" width="43" height="46" title="Youtube"></a>
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center">​​<em>Recuerda que en INCOM estamos para servirte.​​</em></td>
                            </tr>
                            <tr>
                                <td style="text-align: center"><span style="color: white;" lang="tipo_operacion">pedido</span></td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
            </tr>
        </tbody>
    </table>
    <p>&nbsp;</p>
    </td>
    </tr>
  </tbody>
</table>
</div>
