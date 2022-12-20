<%@ Page Language="C#" MasterPageFile="~/herramientas/_masterConfiguraciones.master"
    AutoEventWireup="true" Async="true" CodeFile="reporte-pedidos.aspx.cs" Inherits="herramientas_reporte_pedidos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <h2 class="center-align title-analisis_pedidos">Análisis de pedidos</h2>
    <div class="container">
        <div class="row">
            <div class="col s12 m3 l4 xl4">
                <label>Fecha desde:</label>
                <asp:TextBox ID="txt_fecha_desde" TextMode="Date" runat="server"></asp:TextBox>
            </div>
            <div class="col s12  m3 l4 xl4">
                <label>Fecha hasta:</label>
                <asp:TextBox ID="txt_fecha_hasta" TextMode="Date" runat="server"></asp:TextBox>
            </div>
            <div class="col s12  m3 l4 xl4">
                <br />
                <asp:LinkButton ID="btn_ConsultarPedidos" OnClick="btn_ConsultarPedidos_Click" CssClass=""
                    runat="server">
                    <div class="is-btn-blue">Consultar pedidos</div>
                </asp:LinkButton>
            </div>
        </div>
        <div class="row">
            <div class="col s12 m12 l12 xl12">
                <%--<label style="margin-right: 10px;">
                    <asp:CheckBox ID="chk_omitir_repetidos" runat="server" />
                    <span>Omitir repetidos</span>
                </label>--%>
                <label style="margin-right: 10px;">
                    <asp:CheckBox ID="chk_omitir_cancelados" runat="server" />
                    <span>Omitir cancelados</span>
                </label>
                <%--<label>
                    <asp:CheckBox ID="chk_omitir_pruebas" runat="server" />
                    <span>Omitir pedidos prueba</span>
                </label>--%>
                <label>
                    <asp:CheckBox ID="chk_omitir_cotizaciones" runat="server" />
                    <span>Omitir cotizaciones</span>
                </label>
            </div>
        </div>
        <div class="row" id="tb_pedidos" visible="false" runat="server">
            <div class="col s12 m12 l6 xl6">
                <table>
                    <tr>
                        <th>Registros</th>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_total_registros" runat="server"></asp:Label>
                        </td>
                        <%--td>Monto MXN Productos s/n Impuestos</td>
                        <td>
                            <asp:Label ID="lbl_MontoProductos_MXN_Sin_Impuestos" runat="server"></asp:Label>
                        </td>--%>
                    </tr>
                </table>
            </div>
            <div class="col s12 m12 l6 xl6">
                <asp:GridView ID="gv_desgloseMeses"
                    OnRowDataBound="gv_desgloseMeses_RowDataBound"
                    AutoGenerateColumns="False"
                    EmptyDataText="No hay datos disponibles."
                    AllowPaging="True"
                    runat="server">
                    <columns>
                        <asp:BoundField DataField="Year" HeaderText="Año"
                            SortExpression="Year" />
                        <asp:BoundField DataField="MesNombre" HeaderText="Mes"
                            InsertVisible="False" ReadOnly="True" SortExpression="MesNombre" />
                        <asp:BoundField DataField="TotalPedidosMes" HeaderText="Pedidos"
                            SortExpression="TotalPedidosMes" />
                        <asp:BoundField DataField="MontoTotalMesSNImpuestos" HeaderText="Monto"
                            SortExpression="MontoTotalMesSNImpuestos" />
                        <asp:BoundField DataField="TotalPedidosPagados" HeaderText="Pagados"
                            SortExpression="TotalPedidosPagados" />
                    </columns>
                </asp:GridView>
            </div>
        </div>
        <div class="row">
            <asp:ListView ID="lv_desglose_pedidos" OnItemDataBound="lv_desglose_pedidos_ItemDataBound" runat="server">
                <layouttemplate>
                    <h2 class="title-desglose_pedidos">Desglose</h2>
                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                </layouttemplate>
                <itemtemplate>
                    <div class="col s12 m6 xl4" style="margin-bottom: 10px;">
                        <div class="card card-reporte_pedidos">
                            <asp:HiddenField ID="hf_numero_operacion" Value='<%# Eval("PedidoDatos.numero_operacion") %>' runat="server" />
                            <asp:HiddenField ID="hf_id_operacion" Value='<%# Eval("PedidoDatos.id") %>' runat="server" />
                            <div class="card-content">
                                <p class="is-text-black left-align" style="height: 60px;">
                                    <%#String.Format("{0:f}",  Eval("PedidoDatos.fecha_creacion")) %>
                                    <asp:Label ID="txt_tipo_operacion" class="is-flex is-justify-center is-font-semibold is-top-1" runat="server"></asp:Label>
                                </p>
                                <div class="is-inline-block is-top-1 is-w-full">
                                    <section class="is-w-full is-text-center is-bt-1">
                                            <asp:HyperLink ID="link_pedido_resumen" class="is-text-center" runat="server">
                                                Ver resumen de pedido &nbsp<i class="tiny material-icons">open_in_new</i>
                                            </asp:HyperLink>
                                    </section>
                                    <p style="padding-left: 5px;">Número de operación: 
                                        <span class="is-font-bold is-mx-2"><%# Eval("PedidoDatos.numero_operacion") %></span>
                                    </p>
                                </div>
                                <!-- <p style="text-transform: capitalize; font-weight: bold"><%# Eval("PedidoDatos.nombre_pedido") %></p> -->

                                <table>
                                    <tbody>
                                        <tr class="is-flex is-top-1" style="border-top: 1px solid #e0e0e0;" >
                                            <td class="is-flex" style="width: 100px;">
                                            <p>Creado por:</p>
                                        </td>
                                            <td class="is-flex" style="width: 280px;"><span style="text-transform: lowercase;"><strong><%# Eval("PedidoDatos.creada_por") %></strong></span></td>
                                        </tr>
                                        <tr class="is-flex">
                                            <td class="is-flex" style="width: 100px;">
                                                <p>Total:</p>
                                            </td>
                                            <td><%#String.Format("{0:C}",   Eval("PedidoDatosNumericos.total")) %>      <%# Eval("PedidoDatosNumericos.monedaPedido") %></td>
                                        </tr>
                                        <tr class="is-flex" style="height: 100px;">
                                            <td class="is-flex" style="width: 100px;"><p class="text-estatus_pago" style="margin: auto 0px !important; width: 100px;">Pago:</p></td>
                                            <td class="is-flex" style="width: 280px;"><asp:Label ID="lbl_estatus_pago" style="margin: auto 0px !important; font-weight: 600;" runat="server"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <span class="hide"><%#Container.DataItemIndex %></span>
                                <asp:UpdatePanel ID="up_desglose_pedido" UpdateMode="Conditional" style="height: 200px;" runat="server">
                                    <contenttemplate>
                                        <div class="text-estatus_reporte_pedido is-flex is-top-1" style="padding-bottom: 1rem; border-bottom: 1px solid #252525;"> 
                                            <p class="is-flex" style="width: 100px; padding-left: 5px;">
                                                Estatus:
                                            </p>
                                            <strong>
                                                <asp:Label ID="lbl_OperacionCancelada"
                                                class="is-text-xl is-font-semibold is-flex is-items-center"
                                                style="width: 280px;" runat="server">
                                                <%# Eval("PedidoDatos.OperacionCancelada") %>
                                                </asp:Label>
                                            </strong>
                                        </div>
                                        <asp:Panel ID="ContentPedidoDesactivar" class="is-flex is-justify-center is-items-center is-flex-col" runat="server">
                                            <p class="is-w-full" style="float: left; font-size: 1rem; margin: 1rem 0rem 1rem auto;">En caso de cancelación describe el motivo:</p>
                                            <asp:TextBox ID="txt_motivo_cancelacion" Text="" placeholder="Describe aquí el motivo de cancelación" class="is-pl-4" runat="server"></asp:TextBox>
                                            <asp:LinkButton ID="btn_desactivar_pedido"
                                                OnClick="btn_desactivar_pedido_Click" CssClass="is-btn-gray is-flex is-justify-center is-top-1 is-bt-1" runat="server">
                                                    Cancelar
                                            </asp:LinkButton>
                                        </asp:Panel>
                                        <asp:Panel ID="ContentPedidoActivar" class="container-reactivar_pedido is-bt-2" runat="server">
                                            <asp:LinkButton ID="btn_activar_pedido" OnClick="btn_activar_pedido_Click"
                                                CssClass="is-flex is-justify-center is-btn-gray" runat="server">
                                                Reactivar pedido
                                            </asp:LinkButton>
                                        </asp:Panel>
                                    </contenttemplate>
                                    <triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btn_desactivar_pedido" EventName="Click" />
                                    </triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </itemtemplate>
            </asp:ListView>
        </div>
    </div>

</asp:Content>