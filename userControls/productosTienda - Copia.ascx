<%@ Control Language="C#" AutoEventWireup="true"  CodeFile="productosTienda - Copia.ascx.cs" Inherits="userControls_productosTienda" %>
<%@ Register Src="~/userControls/uc_btn_agregar_carritoListado.ascx" TagName="add" TagPrefix="uc_addCarrito" %>
<%@ Register Src="~/userControls/operaciones/uc_btn_agregar_operacion.ascx" TagName="btn_addOperacion" TagPrefix="uc1" %>
<%@ Register Src="~/userControls/operaciones/uc_modal_agregar_operacion.ascx" TagName="mdl_addOperacion" TagPrefix="uc1" %>
<%@ Register src="~/userControls/moneda.ascx" TagName="moneda" TagPrefix="uc_mon" %>

<div class="row">
    <div class="col s12 m3 l3 xl2 ">
         <div class="row">
                <h2>  Moneda:</h2>
            <uc_mon:moneda ID="uc_moneda" runat="server">
            </uc_mon:moneda>
         </div>
        <div runat="server" id="cont_filtros" class="row">
              <h2>Filtrar por</h2>
            <asp:RadioButtonList ID="rd_filtroMarcas" OnSelectedIndexChanged="orden" AutoPostBack="true" RepeatDirection="Vertical" CssClass="ulFlow" RepeatLayout="UnorderedList" runat="server">
                <asp:ListItem Text="Todas las marcas" Value=""></asp:ListItem>
            </asp:RadioButtonList>
        </div>
    </div>

    <div class="col s12 m9 l9 xl10 ">
         <div runat="server" id="content_resultado_busqueda_text" visible="false" class="row">
             <h2>Resultado de la búsqueda de: <span class="blue-text"><asp:Literal ID="lt_termino_busqueda" runat="server"></asp:Literal></span></h2>
         </div>
        <!-- INICIO : Filtros y orden -->
        <div class="row"     id="cont_ordenar" runat="server">
            <div class="col s12 m5 l4" visible="false" runat="server">
                <label>Busca por: Nombre de cotización ó Número de operación</label>
                <asp:TextBox ID="txt_search" placeholder="Busca por: Nombre de cotización ó Número de operación" AutoPostBack="true" OnTextChanged="cargarProductos" runat="server"></asp:TextBox>
            </div>
            <div class="col s6 m4 l3">
                  <label>Ordenar por</label>
                <asp:DropDownList ID="ddl_ordenBy" AutoPostBack="true" OnSelectedIndexChanged="orden"  runat="server">
                      <asp:ListItem Value="numero_parte" Text="Número de Parte"></asp:ListItem>
                    <asp:ListItem Value="marca" Text="Marca"></asp:ListItem>  
                    <asp:ListItem Value="precio1" Text="Precio"></asp:ListItem>
                   </asp:DropDownList>
            </div>
            <div class="col s6 m3 l2">
                <label>Ordenar por</label>
                <asp:DropDownList ID="ddl_ordenTipo" AutoPostBack="true" OnSelectedIndexChanged="orden"  runat="server">
                   <asp:ListItem Value="DESC" Text="Descendente"></asp:ListItem>  
                    <asp:ListItem Value="ASC" Text="Ascendente"></asp:ListItem>

                </asp:DropDownList>
            </div>
        </div>
        <!-- FIN : Filtros y orden -->

<asp:ListView ID="lv_productos" OnItemDataBound="lv_productos_OnItemDataBound" runat="server">
    <LayoutTemplate>
          <div class="row"  style="margin: auto 0px !important;">
                <asp:DataPager  ID="dp_1" class="dataPager_productos"   runat="server" PagedControlID="lv_productos"
                    PageSize="21"  QueryStringField="PageId">
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
            <div class="row" style="margin: auto 0px !important;">
                <div runat="server" id="itemPlaceholder"></div>
            </div>
            <div class="row" style="margin: auto 0px !important;">
                <asp:DataPager ID="dp_2" class="dataPager_productos" runat="server" PagedControlID="lv_productos"
                    PageSize="21" QueryStringField="PageId">
                    <Fields>
                        <asp:NextPreviousPagerField RenderNonBreakingSpacesBetweenControls="false" ButtonCssClass="pagerButton"
                            FirstPageText="&lt;&lt; &nbsp;" ShowFirstPageButton="True" ShowNextPageButton="False" />
                        <asp:NumericPagerField CurrentPageLabelCssClass="pagerButtonCurrentPage" RenderNonBreakingSpacesBetweenControls="true"
                            NextPreviousButtonCssClass="pagerButton" NumericButtonCssClass="pagerButton" />
                        <asp:NextPreviousPagerField RenderNonBreakingSpacesBetweenControls="false" ButtonCssClass="pagerButton"
                            LastPageText=" &nbsp; &gt;&gt;" ShowLastPageButton="True" ShowPreviousPageButton="False" />
                    </Fields>
                </asp:DataPager>
            </div>
    </LayoutTemplate>

    <ItemTemplate>
        <div id="item_producto" runat="server" class="col s12 m12 l6 xl4 animated fadeIn">
            <div class="card hoverable">
        <div class="card-image">
               <asp:HyperLink ID="link_productoIMG" runat="server">
                    <asp:Image ID="img_producto" runat="server" />
               </asp:HyperLink>
            <span class="card-title titulo_producto" >  <asp:Literal ID="lt_numero_parte"  Text='<%# Eval("numero_parte") %>' runat="server"></asp:Literal></h2></span>      
        </div>
                 <div class="card-content">
                     <p class="productos_descripciones">
                         <asp:Literal ID="lt_descripcion_corta" runat="server"></asp:Literal>
                         <asp:HyperLink ID="link_producto" runat="server">Ver más
                         </asp:HyperLink>
                     </p>
                 </div>
                 <div class="card-action">
                       Marca <%# Eval("marca") %>
                 </div>
                 <div class="card-action">
                      <uc_addCarrito:add ID="AddCarrito" numero_parte='<%# Eval("numero_parte") %>' runat="server"></uc_addCarrito:add>
                 </div>
                 <div class="card-action ">
                      <uc1:btn_addOperacion ID="productoAddOperacion"  numero_parte='<%# Eval("numero_parte") %>' descripcion_corta='<%# Eval("descripcion_corta") %>' runat="server"></uc1:btn_addOperacion>                   
                 </div>
                 <div class="card-action">
                     <div class="producto_precio_contentedor">
                         <span class="producto_precio">$</span><asp:Label ID="lbl_producto_precio" CssClass="producto_precio" runat="server" Text=""></asp:Label>
                         <asp:Label ID="lbl_producto_moneda" CssClass="producto_moneda" runat="server" Text=""></asp:Label>
                         <span class=" blue-grey lighten-5 nota">Impuestos <strong>No incluidos</strong> </span>
                     </div>
                 </div>
             </div>
        </div>
    </ItemTemplate>

    <EmptyDataTemplate>
        <div class="row center-align">
            <div class="col col s12">
                <h3>Intenta con otro término de búsqueda</h3>
            </div>
        </div>
    </EmptyDataTemplate>

        </asp:ListView>
    </div>
</div>
<uc1:mdl_addOperacion ID="mdl_addOperacion" runat="server"></uc1:mdl_addOperacion>