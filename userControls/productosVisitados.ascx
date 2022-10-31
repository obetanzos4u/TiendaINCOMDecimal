<%@ Control Language="C#" AutoEventWireup="true"    CodeFile="productosVisitados.ascx.cs" Inherits="uc_productosVisitados" %>

                        <div class=" "> 
                            <h2 id="titulo" visible="false" class="margin-t-8x" runat="server" style="font-size:1.2rem !important; text-align: center;">Productos más visitados</h2>   </div>
  <asp:ListView ID="lv_productosMasVistos" OnItemDataBound="lv_productosMasVistos_OnItemDataBound"  runat="server">
                    <LayoutTemplate>
                         <div class="sliderVisitados">
                            <div runat="server" id="itemPlaceholder"></div>
                    </div>
                    </LayoutTemplate>
      <ItemTemplate>

        
              <div class="card">
                  <div class="card-image">
                      <asp:HyperLink ID="link_producto" runat="server" CssClass="hoverLinkTituloProducto">
                          <asp:Image ID="img_producto" class="responsive-img" runat="server" />
                          <h2 class="tituloProductoTienda " style="margin: 0px 5px;">
                              <%#Eval("numero_parte") %> - <%#Eval("titulo") %> 
                          </h2>
                      </asp:HyperLink>
                  </div>

              </div>
          
      </ItemTemplate>

      <EmptyDataTemplate>
      </EmptyDataTemplate>

  </asp:ListView>
