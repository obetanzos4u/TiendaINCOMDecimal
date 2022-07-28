<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_addProductoPersonalizado.ascx.cs" Inherits="uc_addProductoPersonalizado" %>

 
<asp:HiddenField ID="hf_numero_operacion" runat="server" />
<asp:HiddenField ID="hf_idSQL" runat="server" />
<asp:HiddenField ID="hf_tipo_operacion" runat="server" />

<a class="waves-effect waves-light btn modal-trigger" href="#modal_agregar_producto_personalizado">Agregar Producto Personalizado</a>

<!-- Modal Structure -->
<div id="modal_agregar_producto_personalizado" class="modal no-autoinit">
    <div class="modal-content">
        <h3 class="left-align">Ingresa los datos de tu producto</h3>
        <div class="row">
            <div class="col s12 m6 l6 xl6  left-align"> 
                <label class="active" for="<%= txt_numero_parte.UniqueID %>">Número de parte </label>
                <asp:TextBox ID="txt_numero_parte" placeholder="Ingresa número de parte" MaxLength="50" lenght="50" runat="server"></asp:TextBox>
          
            </div>

            <div class="col s12 m6 l6 xl4  left-align "> 
                <label for="<%= ddl_marca.UniqueID %>">Marca</label>
                <asp:DropDownList ID="ddl_marca"  class="selectize-select browser-default "  runat="server"></asp:DropDownList>
              
            </div>
        </div>
        <div class="row">
            <div class="col s12 m12 l12 xl12 left-align ">
                  <label for="<%= txt_descripcion.UniqueID %>">Descripción</label>
                <asp:TextBox ID="txt_descripcion" TextMode="MultiLine" CssClass="materialize-textarea" MaxLength="250" lenght="250" runat="server"></asp:TextBox>
              
            </div>
        </div>
        <div class="row">
            <div class="col s12 m6 l4 xl4  left-align">
                    <label for="<%= txt_cantidad.UniqueID %>">Cantidad</label>
                <asp:TextBox ID="txt_cantidad" runat="server"></asp:TextBox>
            
            </div>

            <div class="col s12 m6 l4 xl4 left-align">
                                <label for="<%= ddl_unidad.UniqueID %>">Unidad</label>
                <asp:DropDownList ID="ddl_unidad"  class="selectize-select browser-default " runat="server"></asp:DropDownList>

            </div>
            <div class="col s12 m6 l4 xl4  left-align">
                  <label for="<%= txt_precio.UniqueID %>">Precio</label>
                <span class=" blue-grey lighten-5 nota"><strong>SIN</strong> Impuestos </span>
                <asp:TextBox ID="txt_precio" placeholder="Precio" runat="server"></asp:TextBox>
              

            </div>
            <div class="col s12 m6 l4 xl4  left-align"> 
                <label for="<%= ddl_tipo.UniqueID %>">Tipo</label>
                <asp:DropDownList ID="ddl_tipo" runat="server">
                    <asp:ListItem Value="2" Selected="True" Text="Personalizado"></asp:ListItem>
                    <asp:ListItem Value="3" Text="Servicio"></asp:ListItem>
                </asp:DropDownList>
              
            </div>
        </div>
        <div class="row">
            <div class="col s12 m12 l12 xl12 input-field">
                <asp:UpdatePanel ID="up_btn_agregar_producto" runat="server">
                    <ContentTemplate>
                        <asp:LinkButton ID="btn_agregar_producto" OnClientClick="btnLoading(this);$('#modal_agregar_producto_personalizado').modal('close');" CssClass="waves-effect waves-light btn blue"
                            OnClick="btn_agregar_producto_Click" runat="server" Text="Agregar producto"></asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal-footer">
        <a href="#!" class="modal-action modal-close waves-effect waves-green btn-flat">Cerrar</a>
    </div>
</div>
 