<%@ Page Language="C#" AutoEventWireup="true" Async="true"
MaintainScrollPositionOnPostback="true" CodeFile="pedido-productos.aspx.cs"
MasterPageFile="~/usuario/masterPages/clienteCotizacion.master"
Inherits="usuario_pedidoDatos" %> <%@ Import Namespace="System.Globalization" %>
<%@ Register TagPrefix="uc" TagName="contactos"
Src="~/userControls/uc_contactos.ascx" %> <%@ Register TagPrefix="uc"
TagName="dEnvio" Src="~/userControls/uc_direccionesEnvio.ascx" %> <%@ Register
TagPrefix="uc" TagName="dFacturacion"
Src="~/userControls/uc_direccionesFacturacion.ascx" %> <%@ Register
TagPrefix="uc" TagName="ddlPaises" Src="~/userControls/ddl_paises.ascx" %> <%@
Register TagPrefix="uc" TagName="ddlEstados"
Src="~/userControls/ddl_estados.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
  <asp:HiddenField ID="hf_id_operacion" runat="server" />
  <div id="content_productos" runat="server" class="container">
    <asp:UpdatePanel
      ID="up_operacion"
      UpdateMode="Conditional"
      class="container"
      runat="server">
      <ContentTemplate>
        <div class="row">
          <div class="col s12 m6 l6 xl6">
            <h1 class="margin-b-2x">
              Productos de "<asp:Literal
                ID="lt_nombre_pedido"
                runat="server"></asp:Literal
              >"
            </h1>

            <label>Número de operacion</label>
            <h2 class="margin-t-2x margin-b-2x">
              <asp:Literal
                ID="lt_numero_operacion"
                runat="server"></asp:Literal>
            </h2>

            <label>Cliente</label>
            <h2 class="margin-t-2x">
              <asp:Literal ID="lt_cliente_nombre" runat="server"></asp:Literal>
            </h2>
            <asp:HyperLink
              ID="hl_editarDatos"
              CssClass="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5 "
              runat="server"
              ><i class="material-icons left">edit</i>Regresar a Contacto, envío
              y facturación</asp:HyperLink
            >
            &nbsp;<asp:HyperLink
              ID="hl_editarProductos"
              CssClass="hide waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-4 "
              runat="server"
              ><i class="material-icons left">edit</i> Productos</asp:HyperLink
            >
          </div>
          <div style="float: right" class="col s12 m6 l5 xl5 right-align">
            <h1>Total del pedido:</h1>
            <div
              style="padding: 1rem 10px; font-size: 1.2rem"
              class="blue-grey lighten-5">
              <div id="content_envioCliente" visible="true" runat="server">
                Método de envío :
                <strong>
                  <asp:Label ID="lbl_metodo_envio" runat="server"></asp:Label>
                  <asp:Label ID="lbl_envio" runat="server"></asp:Label
                ></strong>
              </div>

              <div id="content_descuento" visible="false" runat="server">
                Sub Total
                <strong>
                  <asp:Label
                    ID="lbl_subtotalSinDescuento"
                    runat="server"></asp:Label></strong
                ><br /> Descuento aplicado:
                <strong>
                  <asp:Label
                    ID="lbl_descuento_porcentaje"
                    runat="server"></asp:Label
                  >%</strong
                ><br />
              </div>

              Sub Total antes de IVA:
              <strong>
                <asp:Label ID="lbl_subTotal" runat="server"></asp:Label></strong
              ><br /> Impuestos:
              <strong>
                <asp:Label
                  ID="lbl_impuestos"
                  runat="server"></asp:Label></strong
              ><br /> <span class="orange-text text-darken-3">Total: </span
              ><strong>
                <asp:Label ID="lbl_total" runat="server"></asp:Label
              ></strong>

              <asp:Label
                ID="lbl_moneda"
                CssClass="orange-text"
                runat="server"></asp:Label>
            </div>
            <div class="col m12 l12 right-align" style="padding: 1rem 10px">
              <asp:HyperLink
                ID="link_pago"
                CssClass="waves-effect waves-light btn-large blue"
                runat="server">
                Pago<i class="material-icons left">payment</i>
              </asp:HyperLink>
            </div>
            <div class="col m12 l12 right-align" style="padding: 1rem 10px">
              <div
                id="content_envioUsuario"
                runat="server"
                visible="false"
                class="input-field inline">
                Método de envío.
                <br />
                <div class="input-field inline">
                  <asp:DropDownList
                    ID="ddl_metodo_envio"
                    OnSelectedIndexChanged="ddl_metodo_envio_SelectedIndexChanged"
                    AutoPostBack="true"
                    runat="server">
                    <asp:ListItem Value="Ninguno" Text="Ninguno"></asp:ListItem>
                    <asp:ListItem
                      Value="Estándar"
                      Text="Estándar"></asp:ListItem>
                    <asp:ListItem
                      Value="En Tienda"
                      Text="En Tienda"></asp:ListItem>
                    <asp:ListItem
                      Value="Gratuito"
                      Text="Gratuito"></asp:ListItem>
                  </asp:DropDownList>
                </div>
                <div class="input-field inline">
                  <asp:TextBox ID="txt_envio" runat="server"></asp:TextBox>
                </div>
                <br />
                <asp:LinkButton
                  ID="btn_guardarMetodoEnvio"
                  OnClick="btn_guardarMetodoEnvio_Click"
                  runat="server"
                  >Guardar Envio</asp:LinkButton
                >
                <div>
                  <label>
                    <asp:CheckBox
                      ID="chk_CalculoAutomáticoEnvio"
                      OnCheckedChanged="chk_CalculoAutomáticoEnvio_CheckedChanged"
                      AutoPostBack="true"
                      runat="server" />
                    <span>Calculo automático en esta cotización.</span>
                  </label>
                </div>
              </div>
            </div>
            <div id="content_descuento_asesor" visible="false" runat="server">
              Descuento en %
              <asp:TextBox
                ID="txt_descuento"
                placeholer="Establece %"
                style="width: 70px"
                OnTextChanged="txt_descuento_TextChanged"
                AutoPostBack="true"
                runat="server"></asp:TextBox>
            </div>
            <div class="col m12 l12 right-align" style="padding: 1rem 10px">
              <asp:HyperLink
                ID="link_enviar"
                CssClass="waves-effect waves-light btn-large blue"
                runat="server">
                Confirmar y Enviar <i class="material-icons left">email</i>
              </asp:HyperLink>
            </div>
          </div>
        </div>

        <div class="row">
          <asp:ListView
            ID="lv_productosCotizacion"
            OnItemDataBound="lv_productos_OnItemDataBound"
            runat="server">
            <LayoutTemplate>
              <div class="row line-bottom">
                <div class="col s12 m3 l2"><strong>Producto</strong></div>
                <div class="col s12 m3 l5"><strong>Descripción </strong></div>
                <div class="col s12 m3 l3 right-align">
                  <strong>Precio Total</strong>
                </div>
                <div class="col s12 m3 l2"><strong>Cantidad</strong></div>
              </div>

              <div runat="server" id="itemPlaceholder"></div>
            </LayoutTemplate>
            <ItemTemplate>
              <div class="row">
                <div class="col s12 m3 l2">
                  <asp:HiddenField
                    ID="hf_idProducto"
                    Value='<%#Eval("id") %>'
                    runat="server" />
                  <asp:HiddenField
                    ID="hf_tipoProducto"
                    Value='<%#Eval("tipo") %>'
                    runat="server" />
                  <asp:HyperLink
                    ID="link_imgProducto"
                    Target="_blank"
                    runat="server">
                    <asp:Image
                      ID="imgProducto"
                      CssClass="responsive-img"
                      runat="server" />
                  </asp:HyperLink>
                </div>

                <div class="col s12 m4 l5">
                  <h2 class="margin-b-2x" style="margin-top: 5px">
                    <asp:Literal
                      ID="lt_numeroParte"
                      Text='<%#Eval("numero_parte") %>'
                      runat="server"></asp:Literal>
                    - <%#Eval("marca") %>
                    <asp:HyperLink
                      Target="_blank"
                      ID="link_producto"
                      runat="server">
                      <i class="material-icons">launch</i>
                    </asp:HyperLink>
                  </h2>
                  <div>
                    Precio Unitario:<strong>
                      <asp:Label
                        ID="lbl_precio_unitario"
                        runat="server"></asp:Label
                    ></strong>
                    <span class="blue-grey lighten-5 nota"
                      >Impuestos: <strong>No incluidos</strong> </span
                    ><br />
                  </div>
                  <div><%#Eval("descripcion") %></div>
                  <br />
                  <strong>
                    <asp:UpdatePanel UpdateMode="Always" runat="server">
                      <ContentTemplate>
                        <asp:LinkButton
                          CssClass="btn btn-s deep-orange text-darken-1"
                          OnClientClick="btnLoading(this);"
                          OnClick="btn_eliminarProducto_Click"
                          ID="btn_eliminarProducto"
                          runat="server"
                          >Eliminar</asp:LinkButton
                        >
                      </ContentTemplate>
                    </asp:UpdatePanel>
                  </strong>
                </div>
                <div class="col s12 m3 l3 right-align">
                  <asp:Label ID="lbl_precio_total" runat="server"></asp:Label>
                  <span class="blue-grey lighten-5 nota"
                    >Impuestos <strong>No incluidos</strong> </span
                  ><br />
                </div>
                <div class="col s12 m3 l2 right-align">
                  <asp:HiddenField
                    ID="hf_cantidadPedido"
                    Value='<%#Eval("cantidad") %>'
                    runat="server" />
                  <asp:TextBox
                    ID="txt_cantidadPedido"
                    Enabled="false"
                    CssClass="txtCantidad"
                    AutoPostBack="true"
                    type="number"
                    onchange="txtLoading(this);"
                    Text='<%#Eval("cantidad") %>'
                    OnTextChanged="txt_cantidadPedido_TextChanged"
                    runat="server"></asp:TextBox>
                </div>
              </div>
            </ItemTemplate>
          </asp:ListView>
        </div> </ContentTemplate
    ></asp:UpdatePanel>
  </div>
</asp:Content>
