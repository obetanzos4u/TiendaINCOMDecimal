<%@ Control Language="C#" AutoEventWireup="true"    CodeFile="productosVisitadosFullWidth.ascx.cs" Inherits="uc_productosVisitadosFullWidth" %>

                        <div class=" "> 
                            <h2 id="titulo" visible="false"  class="margin-t-8x" runat="server">Productos más visitados</h2>   </div>
  <asp:ListView ID="lv_productosMasVistos" OnItemDataBound="lv_productosMasVistos_OnItemDataBound"  runat="server">
                    <LayoutTemplate>
                        <div><ul class="productosVisitados">
                            <div runat="server" id="itemPlaceholder"></div></ul>
                     </div>
                    </LayoutTemplate>
      <ItemTemplate>
          <li>
          <div class="col s12 m12 l12 xl12">
              <div class="card">
                  <div class="card-image">
                      <asp:HyperLink ID="link_producto" runat="server" CssClass="hoverLinkTituloProducto">
                          <asp:Image ID="img_producto" class="responsive-img" runat="server" />
                          <h2 class="tituloProductoTienda " style="margin: 5px;">
                              <%#Eval("numero_parte") %> - <%#Eval("titulo") %> 
                          </h2>
                      </asp:HyperLink>
                  </div>

              </div>
          </div></li>
      </ItemTemplate>

      <EmptyDataTemplate>
      </EmptyDataTemplate>

  </asp:ListView>
<script>
$(document).ready(function(){
    $('.productosVisitados').bxSlider({
        auto: true,
        autoStart: true,
        speed: 1000,
        minSlides: 2,
        maxSlides: 15,
        slideWidth: 244,
        slideMargin: 10,
        touchEnabled: false,
        responsive: true,

    });
});

</script>