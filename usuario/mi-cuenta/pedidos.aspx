<%@ Page Language="C#" AutoEventWireup="true"   Async="true" CodeFile="pedidos.aspx.cs" MasterPageFile="~/gnCliente.master" Inherits="usuario_pedidos" %>

<%@ Import Namespace="System.Globalization" %>
<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">

    <div class="container z-depth-3">
        <div class="row">
            <div class="col l12">
                <h1 class="center-align">Mis pedidos</h1>
            </div>
        </div>
   <!-- INICIO : Filtros y orden -->
        <div class="row">
            <div class="col s12 m5 l4">
                <label>Busca por: Nombre ó Número de operación</label>
                <asp:TextBox ID="txt_search" placeholder="Busca por: Nombre de pedido ó Número de operación" AutoPostBack="true" OnTextChanged="orden" runat="server"></asp:TextBox>
            </div>
            <div class="col s6 m4 l3">
                  <label>Ordenar por</label>
                <asp:DropDownList ID="ddl_ordenBy" AutoPostBack="true" OnSelectedIndexChanged="orden" runat="server">
                      <asp:ListItem Value="fecha_creacion" Text="Fecha"></asp:ListItem>
                    <asp:ListItem Value="nombre_pedido" Text="Nombre Operación"></asp:ListItem>  
                    <asp:ListItem Value="total" Text="Total"></asp:ListItem>
                    <asp:ListItem Value="productosTotales" Text="Cantidad de Productos"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col s6 m3 l2">

                <label>Ordenar por</label>
                <asp:DropDownList ID="ddl_ordenTipo" AutoPostBack="true" OnSelectedIndexChanged="orden" runat="server">
                        <asp:ListItem Value="DESC" Text="Descendente"></asp:ListItem>
                    <asp:ListItem Value="ASC" Text="Ascendente"></asp:ListItem>
                

                </asp:DropDownList>
            </div>
        </div>
        <!-- FIN : Filtros y orden -->
                  <div class="row center-align">
        <asp:DataPager  ID="dp_1" class="dataPager_productos" OnPagePropertiesChanging="OnPagePropertiesChanging" runat="server" PagedControlID="lvPedidos"
            PageSize="10"  QueryStringField="PageId">
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
        <div class="row">

            <div class="col l12">
                <asp:listview id="lvPedidos" onitemdatabound="lvPedidos_ItemDataBound" runat="server">
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
                                            <div class="card-content grey lighten-4 grey-text text-darken-3">

                                                <asp:TextBox ID="txt_nombre_pedido" class=" browser-default nombreCotizaciontitulo tooltipped" data-position="top"
                                                    data-delay="50" data-tooltip="Clic para editar"
                                                    onchange="txtLoading(this);" AutoPostBack="true" OnTextChanged="cambiarNombrePedido"
                                                    Text='<%#Eval("nombre_pedido") %>' runat="server"></asp:TextBox>

                                                <div class="col no-padding-x s12 m12 l6 xl6 ">
                                                    <strong>Número de operación: </strong>
                                                    <asp:Label ID="lbl_numero_operacion" runat="server" Text='<%#Eval("numero_operacion") %>'></asp:Label>
                                                </div>
                                                <div class="col no-padding-x s12 m12 l6 xl6 ">
                                                    <strong>Fecha de creación: </strong><%#Eval("fecha_creacion") %>
                                                </div>

                                                <asp:HiddenField ID="hf_id_pedidoSQL" Value='<%#Eval("id") %>' runat="server" />
                                            </div>
                                            <div class="card-content cotizacionesProductos_min">
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
                                                <span class="green-text text-darken-2">
                                                <%# int.Parse(Eval("productosTotales").ToString()) > 5 ? "Mostrando <strong>4</strong> de  <strong>" +  Eval("productosTotales") + " </strong> productos" : " <strong> "+Eval("productosTotales") + " </strong> producto(s)."%> 
                                                </span>
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 5%; text-align: right;">
                                                                <strong>Total:</strong>
                                                            </td>
                                                            <td>
                                                                <span class="blue-text text-darken-2">
                                                                    <%#(Convert.ToDouble(Eval("total"))).ToString("C2",new CultureInfo("es-MX"))+ " " + Eval("monedaPedido") %> 
                                                                </span></td>
                                                        </tr>
                                                    </tbody>
                                                </table>


                                            </div>
                                            <div class="card-content ">
                                                <strong>Creada por: </strong><span class="blue-text text-darken-2"><%#Eval("creada_por") %> </span>

                                            </div>
                                            <div class="card-action">
     
                                                
                                                <asp:HyperLink ID="btn_editarPedido"  style="width: 100%;"
                                                    CssClass="btn blue waves-effect waves-light margin-b-3x"
                                                    runat="server"><i class="material-icons right">edit</i>Visualizar  </asp:HyperLink>


                                                <asp:HyperLink ID="btn_visualizar"
                                                    CssClass="waves-effect waves-light  blue-text text-darken-2  " runat="server">
                                                 Enviar comprobante</asp:HyperLink>
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
                        <h2>Aún no tienes pedidos realizados, <a href="/productos/">comienza a comprar ahora.</a></h2>
                    </EmptyDataTemplate>
                </asp:listview>
            </div>
        </div>

                          <div class="row center-align">
        <asp:DataPager  ID="DataPager1" class="dataPager_productos" OnPagePropertiesChanging="OnPagePropertiesChanging" runat="server" PagedControlID="lvPedidos"
            PageSize="10"  QueryStringField="PageId">
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

</asp:Content>
