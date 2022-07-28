<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/herramientas/_masterConfiguraciones.master" Async="true" 
    CodeFile="actualiar-stock-from-sap.aspx.cs"
  Inherits="herramientas_agregar_producto_pedido" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <h1 class="center-align">Actualizar Stock from SAP</h1>
    <div class="container">
        <div class="row">
 
            <div class="input-field col s12 l12">
                <p>Obtiene sl stock de todos los productos mediante el web service de oData, el proceso es largo ya que realizá la consulta de todos los productos disponibles.</p>
                <asp:LinkButton ID="btn_actualizar_stock"   OnClick="btn_actualizar_stock_Click"
                    CssClass="waves-effect waves-light btn btn-s blue" runat="server">Actualizar</asp:LinkButton>

                
            </div>

              <div class="input-field col s12 l12">
        </div>
    </div>
</asp:Content>



