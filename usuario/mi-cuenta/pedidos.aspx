<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeFile="pedidos.aspx.cs" MasterPageFile="~/gnCliente.master" Inherits="usuario_pedidos" %>

<%@ Import Namespace="System.Globalization" %>
<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <div>
        <div id="border-mis_pedidos" class="is-bt-5 is-mx-6 is-border-soft is-rounded-xl is-p-8">
            <div class="row">
                <div class="is-bt-1">
                    <h2 class="is-text-center is-m-0 is-bt-1 is-font-bold is-text-black-soft">Mis pedidos</h2>
                </div>
                <div class="col l12">
                    <h3 style="font-size: 1.25rem;">Encuentra toda la información asociada a cualquiera de tus pedidos:</h3>
                </div>
            </div>
            <!-- INICIO : Filtros y orden -->
            <div id="filtro-mis_pedidos" class="is-flex is-justify-evenly is-items-center is-py-2">
                <div class="search_bar-mis_pedidos">
                    <label class="is-text-black">Busca por número de operación: </label>
                    <asp:TextBox ID="txt_search" placeholder="Busca por nombre de pedido o número de operación" AutoPostBack="true" OnTextChanged="orden" Style="border-radius: 8px; height: 2rem; padding-left: 1rem;" runat="server"></asp:TextBox>
                </div>
                <div id="ordenar_pedido_fecha">
                    <label class="is-text-black">Ordenar por:</label>
                    <asp:DropDownList ID="ddl_ordenBy" AutoPostBack="true" OnSelectedIndexChanged="orden" Style="height: 2rem !important;" runat="server">
                        <asp:ListItem Value="fecha_creacion" Text="Fecha"></asp:ListItem>
                        <%--<asp:ListItem Value="nombre_pedido" Text="Nombre Operación"></asp:ListItem>--%>
                        <asp:ListItem Value="total" Text="Total"></asp:ListItem>
                        <asp:ListItem Value="productosTotales" Text="Productos"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div id="ordenar_pedido_ascendente">
                    <label class="is-text-black">Ordenar por:</label>
                    <asp:DropDownList ID="ddl_ordenTipo" AutoPostBack="true" OnSelectedIndexChanged="orden" Style="height: 2rem !important;" runat="server">
                        <asp:ListItem Value="DESC" Text="Descendente"></asp:ListItem>
                        <asp:ListItem Value="ASC" Text="Ascendente"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div id="mostrar_activos">
                    <label class="is-text-black">Visualizar:</label>
                    <asp:DropDownList ID="ddl_mostrar" AutoPostBack="true" OnSelectedIndexChanged="orden" Style="height: 2rem !important;" runat="server">
                        <asp:ListItem Value="TODOS" Text="Todos"></asp:ListItem>
                        <asp:ListItem Value="ACTIVOS" Text="Activos"></asp:ListItem>
                        <asp:ListItem Value="CANCELADOS" Text="Cancelados"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <!-- FIN : Filtros y orden -->
            <div class="row center-align">
                <!-- <asp:DataPager ID="dp_1" class="dataPager_productos" OnPagePropertiesChanging="OnPagePropertiesChanging" runat="server" PagedControlID="lvPedidos"
                PageSize="10" QueryStringField="PageId">
                <fields>
                    <asp:NextPreviousPagerField RenderNonBreakingSpacesBetweenControls="false" ButtonCssClass="pagerButton"
                        FirstPageText="&lt;&lt; &nbsp;" ShowFirstPageButton="True" ShowNextPageButton="False" />
                    <asp:NumericPagerField CurrentPageLabelCssClass="dataPager_productosCurrentPage" RenderNonBreakingSpacesBetweenControls="true"
                        NextPreviousButtonCssClass="pagerButton" NumericButtonCssClass="pagerButton" />
                    <asp:NextPreviousPagerField RenderNonBreakingSpacesBetweenControls="true" ButtonCssClass="pagerButton"
                        LastPageText=" &nbsp; &gt;&gt;" ShowLastPageButton="True" ShowPreviousPageButton="False" />
                </fields>
            </asp:DataPager>
        </div> -->
                <div class="row">
                    <div class="col l12 card-mis_pedidos">
                        <asp:ListView ID="lvPedidos" OnItemDataBound="lvPedidos_ItemDataBound" runat="server">
                            <LayoutTemplate>
                                <div class="col no-padding-x2 s12 m12 l12">
                                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="col no-padding-x2 s12 m12 l6 xl4" style="margin-bottom: 20px;">
                                            <div class="card horizontal">
                                                <div class="card-stacked">
                                                    <div class="card-content grey-text text-darken-3" style="padding: 1rem; background-color: #f4f4f4;">
                                                        <!-- <asp:TextBox ID="txt_nombre_pedido" class=" browser-default nombreCotizaciontitulo tooltipped" data-position="top"
                                                    data-delay="50" data-tooltip="Clic para editar"
                                                    onchange="txtLoading(this);" AutoPostBack="true" OnTextChanged="cambiarNombrePedido"
                                                    Text='<%#Eval("nombre_pedido") %>' runat="server">
                                                </asp:TextBox> -->
                                                        <div class="">
                                                            <div style="text-align: end; margin-bottom: 1rem">
                                                                <strong></strong><%#Eval("fecha_creacion") %>
                                                            </div>
                                                            <p class="is-inline-block">Número de operación:</p>
                                                            <asp:Label ID="lbl_numero_operacion" Style="font-weight: 600;" CssClass="is-select-all" runat="server" Text='<%#Eval("numero_operacion") %>'></asp:Label>
                                                        </div>
                                                        <asp:HiddenField ID="hf_id_pedidoSQL" Value='<%#Eval("id") %>' runat="server" />
                                                    </div>
                                                    <div class="card-content cotizacionesProductos_min" style="padding: 1rem">

                                                        <asp:ListView ID="lvProductos" OnItemDataBound="lvProductos_ItemDataBound" runat="server">
                                                            <LayoutTemplate>
                                                                <table class="bordered operacionListadoProductos">
                                                                    <tbody>
                                                                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td style="width: 5%; padding-right: 5px;">
                                                                        <%# Eval("numero_parte") %> 
                                                           - 
                                                                <asp:Literal ID="lt_descripcion" runat="server"></asp:Literal>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <EmptyDataTemplate>
                                                                No hay ningún producto.
                                                            </EmptyDataTemplate>
                                                        </asp:ListView>
                                                        <div class="green-text text-darken-2 is-text-center" style="font-size: 1.4rem; font-weight: 700;">
                                                            <!-- <%# int.Parse(Eval("productosTotales").ToString()) > 5 ? "Mostrando <strong>4</strong> de  <strong>" +  Eval("productosTotales") + " </strong> productos" : " <strong> "+Eval("productosTotales") + " </strong> producto(s)."%>  -->
                                                            <%# Eval("productosTotales") + " producto(s)"%>
                                                        </div>
                                                        <table>
                                                            <tbody>
                                                                <tr class="importe_total-mis_pedidos">
                                                                    <td>
                                                                        <strong>Importe total:</strong>
                                                                    </td>
                                                                    <td>
                                                                        <span class="blue-text text-darken-2" style="font-weight: 600;">
                                                                            <%#(Convert.ToDouble(Eval("total"))).ToString("C2",new CultureInfo("es-MX"))+ " " + Eval("monedaPedido") %> 
                                                                        </span>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                    <div class="card-content cotizaciones-agente" style="padding: 0 1rem;">
                                                        <div class="status-pedido_card is-m-0"><%# string.IsNullOrEmpty(Eval("OperacionCancelada").ToString()) ? "<p class='status-activo is-font-semibold' style='margin-left: 1rem'>Activo</p>" : "<p class='status-cancelado is-font-semibold' style='margin-left: 1rem'>Cancelado</p>" %></div>
                                                        <strong>Gestionada por: </strong><span class="is-text-black"><%#Eval("creada_por") %> </span>
                                                    </div>
                                                    <div class="card-action">
                                                        <asp:HyperLink ID="btn_editarPedido" Style="width: 100%; margin-right: 0px !important;"
                                                            CssClass=""
                                                            runat="server">
                                                    <div class="is-btn-blue" style="width: 100%; margin-bottom: 1.5rem;"">Visualizar</div>
                                                        </asp:HyperLink>

                                                        <asp:HyperLink ID="btn_visualizar"
                                                            CssClass="" Style="text-transform: none;" runat="server">
                                                    <div class="is-text-white is-btn-gray" style="width: 100%">Obtener comprobante</div></asp:HyperLink>
                                                        <a class="hide waves-effect waves-light btn  blue-grey-text text-darken-2  
                                                    activator right-align">Totales</a>
                                                    </div>
                                                </div>
                                                <div class="card-reveal">
                                                    <span class="card-title grey-text text-darken-4">Totales <i class="material-icons right">close</i></span>
                                                    <p>
                                                        <table class="bordered">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: 5%; text-align: right; padding-right: 5px; padding: 5px 5px;">Subtotal</td>
                                                                    <td>
                                                                        <%# (Convert.ToDouble(Eval("subtotal"))).ToString("C2",new CultureInfo("es-MX")) + " " + Eval("monedaPedido") %>      
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 5%; text-align: right; padding-right: 5px; padding: 5px 5px;">Envío</td>
                                                                    <td><%# (Convert.ToDouble(Eval("envio"))).ToString("C2",new CultureInfo("es-MX")) + " " +  Eval("monedaPedido") %> </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 5%; text-align: right; padding-right: 5px; padding: 5px 5px;">Impuestos</td>
                                                                    <td><%# (Convert.ToDouble(Eval("impuestos"))).ToString("C2",new CultureInfo("es-MX")) + " " +  Eval("monedaPedido") %> </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 5%; text-align: right; padding-right: 5px; padding: 5px 5px;">Total</td>
                                                                    <td><%#(Convert.ToDouble(Eval("total"))).ToString("C2",new CultureInfo("es-MX"))+ " " + Eval("monedaPedido") %> </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </ItemTemplate>


                            <EmptyDataTemplate>
                                <h3>Aún no tienes pedidos realizados, <a href="/productos/">comienza a comprar ahora.</a></h3>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </div>
                </div>

                <div id="pagination-mis_pedidos" class="row center-align">
                    <asp:DataPager ID="DataPager1" class="dataPager_productos" OnPagePropertiesChanging="OnPagePropertiesChanging" runat="server" PagedControlID="lvPedidos"
                        PageSize="10" QueryStringField="PageId">
                        <Fields>
                            <asp:NextPreviousPagerField RenderNonBreakingSpacesBetweenControls="false" ButtonCssClass="pagerButton"
                                FirstPageText="&lt;&lt; &nbsp;" ShowFirstPageButton="True" ShowNextPageButton="False" />
                            <asp:NumericPagerField CurrentPageLabelCssClass="dataPager_productosCurrentPage" RenderNonBreakingSpacesBetweenControls="true"
                                NextPreviousButtonCssClass="pagerButton" NumericButtonCssClass="pagerButton" />
                            <asp:NextPreviousPagerField RenderNonBreakingSpacesBetweenControls="true" ButtonCssClass="pagerButton"
                                LastPageText=" &nbsp; &gt;&gt;" ShowLastPageButton="True" ShowPreviousPageButton="False" />
                        </Fields>
                    </asp:DataPager>
                </div>
            </div>
        </div>
</asp:Content>
