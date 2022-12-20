<%@ Page Language="C#" MasterPageFile="~/herramientas/_masterConfiguraciones.master"
    AutoEventWireup="true" Async="true" CodeFile="reporte-pedidos.aspx.cs" Inherits="herramientas_reporte_pedidos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <h2 class="center-align">Análisis pedidos</h2>
    <div class="container">
        <div class="row">
            <div class="col s12 m3 l4 xl4">
                <label>Fecha desde</label>
                <asp:TextBox ID="txt_fecha_desde" TextMode="Date" runat="server"></asp:TextBox>
            </div>
            <div class="col s12  m3 l4 xl4">
                <label>Fecha hasta</label>
                <asp:TextBox ID="txt_fecha_hasta" TextMode="Date" runat="server"></asp:TextBox>
            </div>
            <div class="col s12  m3 l4 xl4">
                <br />
                <asp:LinkButton ID="btn_ConsultarPedidos" OnClick="btn_ConsultarPedidos_Click" CssClass="btn blue"
                    runat="server">
                    Consultar Pedidos</asp:LinkButton>
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
                    <h2>Desglose</h2>
                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                </layouttemplate>
                <itemtemplate>
                    <div class="col s12 m6 xl4" style="margin-bottom: 10px;">
                        <div class="card ">
                            <asp:HiddenField ID="hf_numero_operacion" Value='<%# Eval("PedidoDatos.numero_operacion") %>' runat="server" />
                            <asp:HiddenField ID="hf_id_operacion" Value='<%# Eval("PedidoDatos.id") %>' runat="server" />

                            <div class="card-content">
                                <asp:Label ID="txt_tipo_operacion" runat="server"></asp:Label>
                                <p class="blue-text left-align">
                                    <%#String.Format("{0:f}",  Eval("PedidoDatos.fecha_creacion")) %>
                                </p>
                                <span class="card-title"><%# Eval("PedidoDatos.numero_operacion") %>
                                    <asp:HyperLink ID="link_pedido_resumen" runat="server">
                                        Abrir <i class="tiny material-icons">open_in_new</i>
                                    </asp:HyperLink>
                                </span>

                                <p style="text-transform: capitalize; font-weight: bold"><%# Eval("PedidoDatos.nombre_pedido") %></p>

                                <table>
                                    <tbody>
                                        <tr>
                                            <td>Creada Por:</td>
                                            <td><span style="text-transform: lowercase;"><strong><%# Eval("PedidoDatos.creada_por") %></strong></span></td>

                                        </tr>
                                        <tr>
                                            <td>Total:</td>
                                            <td><%#String.Format("{0:C}",   Eval("PedidoDatosNumericos.total")) %>      <%# Eval("PedidoDatosNumericos.monedaPedido") %></td>

                                        </tr>
                                        <tr>
                                            <td>Estatus de pago:</td>
                                            <td>
                                                <asp:Label ID="lbl_estatus_pago" runat="server"></asp:Label>
                                            </td>

                                        </tr>
                                    </tbody>
                                </table>

                                <br />

                                <br />


                                <span class="hide"><%#Container.DataItemIndex %></span>
                                <asp:UpdatePanel ID="up_desglose_pedido" UpdateMode="Conditional" style="height: 160px;" runat="server">
                                    <contenttemplate>
                                        Estatus operacion:
                                 <strong>
                                     <asp:Label ID="lbl_OperacionCancelada" runat="server"><%# Eval("PedidoDatos.OperacionCancelada") %> </asp:Label>
                                 </strong>
                                        <hr>
                                        <asp:Panel ID="ContentPedidoDesactivar" runat="server">
                                            <h3>Cancelar Pedido</h3>

                                            <asp:TextBox ID="txt_motivo_cancelacion" Text="" placeholder="Ingresa un texto de cancelación" runat="server"></asp:TextBox>
                                            <asp:LinkButton ID="btn_desactivar_pedido"
                                                OnClick="btn_desactivar_pedido_Click" CssClass="btn-small blue-grey-text  blue-grey lighten-4" runat="server">
                                                Cancelar</asp:LinkButton>

                                        </asp:Panel>
                                        <asp:Panel ID="ContentPedidoActivar" runat="server">
                                            <h3>Reactivar Pedido</h3>
                                            <asp:LinkButton ID="btn_activar_pedido" OnClick="btn_activar_pedido_Click"
                                                CssClass="btn-small blue-grey-text  blue-grey lighten-4" runat="server">
                                                Reactivar Pedido</asp:LinkButton>

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
