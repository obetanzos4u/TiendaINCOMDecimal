<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/herramientas/_masterConfiguraciones.master" Async="true" 
    CodeFile="agregar-producto-pedido.aspx.cs"
  Inherits="herramientas_agregar_producto_pedido" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <h1 class="center-align">Agregar Producto a Pedido</h1>
    <div class="container">
        <div class="row">
            <h2 class="c">Agregar</h2>
              <div class="input-field col s6 m4 l4">
                <asp:TextBox ID="txt_numero_operacion" runat="server"></asp:TextBox>
                <label for="<%= txt_numero_operacion.UniqueID %>">Número de operación de pedido</label>
            </div>
            <div class="input-field col s6m4 l4">
                <asp:TextBox ID="txt_numero_parte" runat="server"></asp:TextBox>
                <label for="<%= txt_numero_parte.UniqueID %>">Número de parte</label>
            </div>
              <div class="input-field col s6 m3 l2">
                <asp:TextBox ID="txt_cantidad" runat="server"></asp:TextBox>
                <label for="<%= txt_cantidad.UniqueID %>">Cantidad</label>
            </div>
            <div class="input-field col s12 l12">
                <asp:LinkButton ID="btn_añadir"   OnClick="btn_añadir_Click"
                    CssClass="waves-effect waves-light btn btn-s blue" runat="server">Guardar</asp:LinkButton>

                
            </div>
        </div>
    </div>
</asp:Content>



