<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_producto_btn_SoloVisualizar.ascx.cs" Inherits="uc_producto_btn_SoloVisualizar" %>





<!-- <div class="btn_carrito_left">
    <a href="#" class="btn-small blue-grey darken-1 disabled"><i class="material-icons">remove</i></a>
</div>
<div class="txt_center">
    <input type="text" style="text-align: center; height: 1.5rem;   margin: 0 0 4px 0; width: -webkit-fill-available;" disabled class="inline" value="1" />

</div>
<div class="btn_carrito_right">
    <a href="#" class="btn-small blue-grey darken-1 disabled"><i class="material-icons">add</i></a>

</div> -->

<!-- <asp:HyperLink ID="link_VisualizarProduct" CssClass=" waves-effect waves-light btn blue  btn-full-text" runat="server">
            Ver y cotizar <i class="material-icons left">chevron_right</i></asp:HyperLink> -->
<asp:HyperLink ID="link_VisualizarProducto" CssClass="is-btn-blue" runat="server">
            Ver y cotizar</asp:HyperLink>
<asp:HyperLink ID="link_solicitarCotización" 
    ToolTip="Solicitar cotización" 
    style="margin-top: 5px;"  
    CssClass="waves-effect waves-light btn blue btn-full-text hide" 
   
    runat="server">
    Solicitar cotización
</asp:HyperLink>

<asp:UpdatePanel ID="up_solicitarCotización" Visible="false" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:LinkButton ID="btn_solicitarCotizacion"  style="margin-top: 5px;"  CssClass=" waves-effect waves-light btn blue btn-full-text  "
            Text="Solicitar cotización" runat="server"></asp:LinkButton>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btn_solicitarCotizacion" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
<a id="agregar_productoCarrito_logoOut" runat="server" visible="false" class=" waves-effect waves-light btn blue ">
    <i class="material-icons left">add_shopping_cart</i> Agregar a carrito
</a>


