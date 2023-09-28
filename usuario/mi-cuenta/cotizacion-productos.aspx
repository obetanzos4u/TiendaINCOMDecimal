<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" Async="true"
enableEventValidation="false" CodeFile="cotizacion-productos.aspx.cs"
MasterPageFile="~/usuario/masterPages/clienteCotizacion.master"
Inherits="usuario_cotizacionDatos" %> <%@ Import
Namespace="System.Globalization" %> <%@ Register
Src="~/userControls/operaciones/uc_btn_editar_operacion.ascx"
TagName="editarProductos" TagPrefix="uc1" %> <%@ Register
Src="~/userControls/operaciones/uc_btn_detalles_operacion.ascx"
TagName="detalles_operacion" TagPrefix="uc1" %> <%@ Register
Src="~/userControls/uc_precio_detalles.ascx" TagName="preciosDetalles"
TagPrefix="uc1" %> <%@ Register
Src="~/userControls/operaciones/uc_addProductoPersonalizado.ascx"
TagName="addProdPer" TagPrefix="uc1" %> <%@ Register
Src="~/userControls/operaciones/uc_editProductoPersonalizado.ascx"
TagName="editProdPer" TagPrefix="uc1" %> <%@ Register
Src="~/userControls/operaciones/uc_editModalProductoPersonalizado.ascx"
TagName="editProdPerModal" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
  <asp:HiddenField ID="hf_id_operacion" runat="server" />
  <asp:HiddenField ID="hf_operacionActiva" runat="server" />

  <div
    id="content_pedido_creado"
    runat="server"
    visible="false"
    class="container center-align">
    <h1>Pedido creado con éxito</h1>
    <h2>en 3 segundos serás redireccionado</h2>
  </div>
  <div id="content_productos" runat="server">
    <asp:UpdatePanel
      ID="up_operacion"
      UpdateMode="Conditional"
      class="container-crear_cotizacion"
      runat="server">
      <ContentTemplate>
        <uc1:detalles_operacion
          ID="detallesOperacion"
          tipo_operacion="cotizacion"
          runat="server" />
        <div id="content_lv_datos" runat="server" class="row is-flex">
          <div class="col s12 m6 l6 xl6" style="padding-right: 4rem">
            <h1
              class=""
              style="
                font-size: 1.25rem;
                font-weight: 600;
                margin-bottom: 2rem;
                line-height: 1.5rem;
                margin: 2.5rem 0 1.68rem 0 !important;
              ">
              Productos de cotización "
              <asp:Literal ID="lt_nombre_cotizacion" runat="server">
              </asp:Literal
              >"
            </h1>
            <div class="is-flex" style="align-items: center; padding-left: 2rem;">
              <label
                style="
                  font-size: 1rem;
                  font-weight: 400;
                  color: #000;
                  margin-right: 1rem;
                "
                >Número de operacion:
              </label>
              <h2
                style="
                  font-size: 1rem !important;
                  font-weight: 400;
                  margin: 0 !important;
                ">
                <asp:Literal ID="lt_numero_operacion" runat="server">
                </asp:Literal>
              </h2>
            </div>
            <div class="is-flex" style="align-items: center; padding-left: 2rem;">
              <label
                style="
                  font-size: 1rem;
                  font-weight: 400;
                  color: #000;
                  margin-right: 1rem;
                "
                >Cliente</label
              >
              <h2
                style="
                  font-size: 1rem !important;
                  font-weight: 400;
                  margin: 0 !important;
                ">
                <asp:Literal
                  ID="lt_cliente_nombre"
                  runat="server"></asp:Literal>
              </h2>
            </div>
            <div style="height: 2rem"></div>
            <asp:HyperLink
              ID="hl_editarDatos"
              CssClass="btn-edit_cotizacion"
              runat="server"
              >Editar contacto, envío y facturación
            </asp:HyperLink>
            &nbsp;
            <asp:HyperLink
              ID="hl_editarProductos"
              CssClass="hide waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-4 "
              runat="server"
              ><i class="material-icons left">edit</i> Productos
            </asp:HyperLink>
            <asp:LinkButton
              ID="btn_converPedido"
              OnClick="btn_converPedido_Click"
              OnClientClick="btnLoading(this);"
              runat="server"
              >Convertir cotización a pedido
            </asp:LinkButton>
            <hr class="hr-ct" style="margin-top: 3rem;
            margin-bottom: 1rem;">
            <div style="float: left; width: 100%">
              <h1
                style="font-size: 1.25rem; margin: 1rem 0 1.68rem 0; font-weight: 600; line-height: 1.5">
                Total de la cotización:
              </h1>
              <div
                style="padding: 1.8rem; font-size: 1.2rem"
                class="blue-grey lighten-5">
                <div id="content_envioCliente" visible="true" runat="server">
                  Método de envío:&nbsp;&nbsp;
                  <strong>
                    <asp:Label ID="lbl_metodo_envio" runat="server"></asp:Label>
                    <asp:Label ID="lbl_envio" runat="server"></asp:Label>
                  </strong>
                </div>
                <div id="content_descuento" visible="false" runat="server">
                  Subtotal:&nbsp;&nbsp;
                  <strong>
                    <asp:Label ID="lbl_subtotalSinDescuento" runat="server">
                    </asp:Label> </strong
                  ><br /> Descuento aplicado:&nbsp;&nbsp;
                  <strong>
                    <asp:Label ID="lbl_descuento_porcentaje" runat="server">
                    </asp:Label
                    >%</strong
                  ><br />
                </div>
                Subtotal antes de IVA:&nbsp;&nbsp;
                <strong>
                  <asp:Label
                    ID="lbl_subTotal"
                    runat="server"></asp:Label></strong
                ><br />
                Impuestos:&nbsp;&nbsp;
                <strong>
                  <asp:Label ID="lbl_impuestos" runat="server"> </asp:Label>
                </strong>
                <br />
                <span class="">Total:&nbsp;&nbsp;</span
                ><strong>
                  <asp:Label ID="lbl_total" runat="server"></asp:Label
                ></strong>
                <!-- <asp:Label
                  ID="lbl_moneda"
                  CssClass=""
                  runat="server">
                </asp:Label> -->
                <br />
                <asp:LinkButton
                  ID="btn_ConvertirMoneda"
                  CssClass="btn btn-s blue tooltipped"
                  data-tooltip="Convertir la moneda de la cotización también cambiará la vigencia de esta, posiblemente sea necesario renovar los precios si
														la cotización venciera debido vigencia."
                  OnClientClick="btnLoading(this);"
                  OnClick="btn_ConvertirMoneda_Click"
                  runat="server">
                </asp:LinkButton>
              </div>
            </div>
          </div>
          <div class="" style="width: 50%" style="padding-left: 5rem">
            <div
              class=""
              style="padding: 1rem 0px; display: flex; flex-direction: column">
              <p
                style="
                  font-size: 1.25rem;
                  font-weight: 600;
                  float: left;
                  text-align: left;
                ">
                Agregar productos
              </p>
              <div class="is-flex" style="margin-left: 2rem;">
                <uc1:editarProductos
                  ID="btn_editarProductos"
                  id_operacion="<%# hf_id_operacion.Value %>"
                  style="float: left;"
                  tipo_operacion="cotizacion"
                  runat="server" />
                <uc1:addProdPer
                  ID="addProdPer"
                  style="float: left"
                  Visible="false"
                  runat="server" />
              </div>
            </div>
            <hr class="hr-ct" style="margin-top: 2rem;">
            <div style="margin-top: 2rem">
              <p style="font-size: 1.25rem; font-weight: 600; line-height: 1.5">
                Selecciona el método de envío
              </p>
            </div>
            <div
              id="content_envioUsuario"
              runat="server"
              visible="false"
              class="input-field inline">
              <!-- <div
                class="input-field inline input-count_envio"
                style="display: flex"> -->
                <!-- <div class="input-field inline"> -->
                  <asp:DropDownList
                    ID="ddl_metodo_envio"
                    OnSelectedIndexChanged="ddl_metodo_envio_SelectedIndexChanged"
                    AutoPostBack="true"
                    style="width: auto;
                    height: 42px;
                    padding-top: 6px;
                    margin-left: 1.5rem;"
                    runat="server">
                    <asp:ListItem Value="Ninguno" Text="Ninguno">
                    </asp:ListItem>
                    <asp:ListItem Value="Estándar" Text="Estándar">
                    </asp:ListItem>
                    <asp:ListItem Value="En Tienda" Text="En Tienda">
                    </asp:ListItem>
                    <asp:ListItem Value="Gratuito" Text="Gratuito">
                    </asp:ListItem>
                  </asp:DropDownList>
                <!-- </div> -->
                <!-- //Envio slot
              </div> -->
            </div>
            <asp:TextBox
                  ID="txt_envio"
                  style="
                    padding-left: 1rem;
                    width: 30%;
                    max-width: 250px;
                    height: 36px;
                    line-height: 36px;
                    margin-left: 1.85rem;
                  "
                  runat="server"></asp:TextBox>
                <asp:LinkButton
                  ID="btn_guardarMetodoEnvio"
                  class="waves-effect waves-teal btn-small"
                  style="height: 38px; margin-top: -8px;"
                  OnClick="btn_guardarMetodoEnvio_Click"
                  runat="server"
                  >Guardar envío
                </asp:LinkButton>
            <div
              style="
                margin-top: 1rem;
                margin-bottom: 2rem;
                padding-left: 2rem;
              ">
              <label>
                <asp:CheckBox
                  ID="chk_CalculoAutomáticoEnvio"
                  OnCheckedChanged="chk_CalculoAutomáticoEnvio_CheckedChanged"
                  AutoPostBack="true"
                  runat="server" />
                <span>Calculo automático en esta cotización.</span>
              </label>
            </div>
            <hr class="hr-ct">
            <div class="col s12 m6 l6 xl6" style="padding: 0">
              <p style="font-size: 1.25rem; font-weight: 600; line-height: 1.5">
                Aplica un descuento al pedido
              </p>
              <div id="content_descuento_asesor" runat="server">
                Descuento de
                <asp:TextBox
                  ID="txt_descuento"
                  placeholer="Establece %"
                  Style="width: 70px;"
                  OnTextChanged="txt_descuento_TextChanged"
                  AutoPostBack="true"
                  runat="server"></asp:TextBox>
                %
              </div>
            </div>
          </div>
        </div>
        <div
          id="content_lv_productos"
          runat="server"
          class="row"
          style="
            margin-bottom: 6rem;
            display: flex;
            flex-direction: column;
            align-items: center;
            margin-top: 4rem;
          ">
          <div class="btn_actions_cotizacion">
            <hr style="margin-bottom: 3rem;color: #f4f4f4;">
            <asp:LinkButton
              ID="btn_guardarPlantilla"
              data-tooltip="Guarda este listado de productos para cotizaciones"
              OnClick="btn_guardarPlantilla_Click"
              CssClass="tooltipped"
              style="text-transform: none"
              runat="server">
              Guardar plantilla
            </asp:LinkButton>
            <asp:HyperLink
              ID="link_enviar"
              CssClass="btn-enviar_email"
              runat="server">
              Confirmar y Enviar cotización
              <i class="material-icons left">email</i>
            </asp:HyperLink>
            <hr style="margin-top: 3rem;color: #f4f4f4;">
          </div>
          <asp:ListView
            ID="lv_productosCotizacion"
            OnItemDataBound="lv_productos_OnItemDataBound"
            runat="server">
            <LayoutTemplate>
              <div class="row line-bottom" style="width: 80%; margin-top: 1rem">
                <h2
                  style="
                    font-size: 1.25rem !important;
                    font-weight: 600;
                    text-align: center;
                    line-height: 1.5;
                    margin-bottom: 3rem;
                  ">
                  Edita tus productos o las cantidades
                </h2>
                <div class="col s12 m3 l2" style="margin-bottom: 1rem">
                  <strong>Producto</strong>
                </div>
                <div class="col s12 m3 l5"><strong>Descripción </strong></div>
                <div class="col s12 m3 l2"><strong>Cantidad</strong></div>
                <div class="col s12 m3 l3 right-align">
                  <strong style="float: left">Precio Total</strong>
                </div>
              </div>
              <ul class="cotizacionProductos">
                <div runat="server" id="itemPlaceholder"></div>
              </ul>
            </LayoutTemplate>
            <ItemTemplate>
              <li class="row">
                <asp:HiddenField
                  ID="hf_numero_parte"
                  Value='<%#Eval("numero_parte") %>'
                  runat="server" />
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
                      CssClass="responsive-img  imgDrag"
                      style="border: 1px solid #ccc"
                      runat="server" />
                  </asp:HyperLink>
                </div>
                <div class="col s12 m4 l5">
                  <h2
                    class="margin-b-2x"
                    style="margin-top: 5px; font-size: 1.25rem !important">
                    <asp:Label
                      ID="lbl_activo"
                      Style="color: red;"
                      Visible="false"
                      runat="server"></asp:Label>
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
                    <asp:Label
                      ID="lbl_moneda_producto"
                      runat="server"></asp:Label>
                    <uc1:preciosDetalles
                      ID="detalles_precios"
                      runat="server"></uc1:preciosDetalles>
                    <span
                      class="blue-grey lighten-5 nota"
                      style="padding: 2px 10px"
                      >Impuestos: <strong>No incluidos</strong> </span
                    ><br />
                  </div>
                  <div style="min-height: 60px"><%#Eval("descripcion") %></div>

                  <asp:UpdatePanel
                    ID="up_opcionesProducto"
                    UpdateMode="Conditional"
                    class="margin-t-1x"
                    runat="server">
                    <ContentTemplate>
                      <asp:Panel
                        ID="content_btn_productoAlternativo"
                        class="margin-b-4x tooltipped"
                        runat="server"
                        Visible="false"
                        data-position="bottom"
                        data-delay="50"
                        data-tooltip="Establece si es un producto como sugerencia o alternativo a otro producto.">
                      </asp:Panel>
                      <div>
                        <label>
                          <asp:CheckBox
                            ID="chk_alternativo"
                            AutoPostBack="true"
                            OnCheckedChanged="chk_alternativo_CheckedChanged"
                            runat="server" />
                          <span style="margin-bottom: 1rem"
                            >Establacer como producto alternativo</span
                          >
                        </label>
                      </div>
                      <div class="editar-producto_cotizacion">
                        <uc1:editProdPer
                          ID="editProdPer"
                          Visible="false"
                          runat="server" />
                      </div>
                      <asp:LinkButton
                        CssClass="btn-eliminar_producto_ct"
                        OnClientClick=" return confirm('Confirma que deseas ELIMINAR este producto');
																					btnLoading(this);"
                        OnClick="btn_eliminarProducto_Click"
                        ID="btn_eliminarProducto"
                        style="color: red;
                        background-color: white;
                        border-radius: 6px;
                        text-transform: none;
                        font-weight: bold;
                        margin-top: -4px;
                        box-shadow: inset;
                        box-shadow: none;"
                        runat="server"
                        >Eliminar</asp:LinkButton
                      > </ContentTemplate
                    ><Triggers
                      ><asp:AsyncPostBackTrigger
                        ControlID="chk_alternativo"
                        EventName="CheckedChanged"
                    /></Triggers>
                  </asp:UpdatePanel>
                </div>
                <div class="col s12 m3 l2 right-align input-count_cotizacion">
                  <asp:UpdatePanel UpdateMode="Always" runat="server">
                    <ContentTemplate>
                      <asp:TextBox
                        ID="txt_cantidadCotizacion"
                        OnTextChanged="txt_cantidadCotizacion_TextChanged"
                        AutoPostBack="true"
                        type="number"
                        onchange="txtLoading(this);"
                        Text='<%#Eval("cantidad") %>'
                        runat="server"></asp:TextBox>
                      <asp:TextBox
                        ID="txt_cantidadCotizacionPersonalizado"
                        OnTextChanged="txt_cantidadCotizacionPersonalizado_TextChanged"
                        AutoPostBack="true"
                        type="number"
                        onchange="txtLoading(this);"
                        Text='<%#Eval("cantidad") %>'
                        runat="server"></asp:TextBox>
                    </ContentTemplate>
                    <Triggers>
                      <asp:AsyncPostBackTrigger
                        ControlID="txt_cantidadCotizacion"
                        EventName="TextChanged" />
                      <asp:AsyncPostBackTrigger
                        ControlID="txt_cantidadCotizacionPersonalizado"
                        EventName="TextChanged" />
                    </Triggers>
                  </asp:UpdatePanel>
                </div>
                <div class="col s12 m3 l3 right-align">
                  <asp:Label
                    ID="lbl_precio_total"
                    style="float: left"
                    runat="server"></asp:Label>
                </div>
              </li>
            </ItemTemplate>
          </asp:ListView>
        </div>
        <uc1:editProdPerModal ID="editProdPersonalizadoModal" runat="server">
        </uc1:editProdPerModal>
      </ContentTemplate>
    </asp:UpdatePanel>
  </div>
  <asp:UpdatePanel
    ID="upAsync"
    style="display: none"
    UpdateMode="Conditional"
    runat="server">
    <ContentTemplate>
      <asp:LinkButton
        ID="btn_async"
        OnClick="btn_async_Click"
        runat="server"></asp:LinkButton>
    </ContentTemplate>
    <Triggers>
      <asp:AsyncPostBackTrigger ControlID="btn_async" EventName="Click" />
    </Triggers>
  </asp:UpdatePanel>

  <!-- Modal Structure -->
  <div
    id="modal_tipo_cotizacion"
    class="modal_tipo_cotizacion modal"
    runat="server">
    <div class="modal-content">
      <h2>Selecciona el tipo de cotización popup</h2>
      <p>
        Es importante clasificar tu cotización si es que aplica a uno de los
        siguientes casos:
      </p>
      <div class="row">
        <div class="col s12 m12 l12 popup-cotizacion">
          <asp:DropDownList
            ID="ddl_tipo_cotizacion"
            OnSelectedIndexChanged="ddl_tipo_cotizacion_SelectedIndexChanged"
            AutoPostBack="true"
            runat="server">
            <asp:ListItem Value="" Text="Ninguno"></asp:ListItem>
            <asp:ListItem Value="Licitación" Text="Licitación"></asp:ListItem>
            <asp:ListItem Value="Proyecto" Text="Proyecto"></asp:ListItem>
            <asp:ListItem Value="Comparativo" Text="Comparativo"></asp:ListItem>
            <asp:ListItem Value="Regular" Text="Regular"></asp:ListItem>
          </asp:DropDownList>
        </div>
      </div>
      <div class="row">
        <div class="col s12 m12 l12"><br /><br /><br /><br /></div>
      </div>
    </div>
    <div class="modal-footer" style="height: 75px;">
      <a
        href="#!"
        class="modal-action modal-close waves-effect waves-green btn-flat"
        style="border: 1px solid red;
        border-radius: 6px;
        color: red;
        text-transform: none;
        padding: 0 26px;
        font-weight: bold;
        margin-top: 1rem;"
        >Cerrar</a
      >
    </div>
  </div>
  <style>
    .container-crear_cotizacion {
      width: 90%;
      margin: auto;
    }

    .btn-enviar_email,
    #top_contenido_btn_guardarPlantilla {
      color: #fff;
      background-color: #06c;
      border-radius: 4px;
      text-transform: none;
      padding: 8px 26px;
    }

    #top_contenido_content_envioUsuario {
      width: 15%;
      padding-top: 6px;
      margin-left: 2rem;
    }

    #top_contenido_btn_guardarPlantilla {
      padding-top: 0;
      margin-right: 20%;
    }

    #top_contenido_btn_ConvertirMoneda {
      margin-top: 2rem;
    }

    #top_contenido_content_lv_productos {
      padding-right: 1rem;
    }

    #top_contenido_txt_descuento {
      padding-left: 1rem;
    }

    #top_contenido_btn_guardarMetodoEnvio {
      margin-left: 0.5rem;
    }

    #top_contenido_btn_converPedido {
      margin-left: 2rem;
    }

    #top_contenido_content_descuento_asesor {
      padding-left: 2rem;
    }

    #top_contenido_content_envioUsuario > div:nth-child(1) > input:nth-child(1) {
      height: 100% !important;
    }

    #top_contenido_content_envioUsuario > div:nth-child(1) {
      height: 38px;
    }

    .editar-producto_cotizacion {
      display: inline-block;
    }

    input.select-dropdown {
      height: 100% !important;
      padding-left: 1rem !important;
	  }

    .editar-producto_cotizacion a {
      border-radius: 6px;
      font-weight: bold;
      color: #06c;
      height: 36px;
      display: block;
      margin-right: 4rem;
      background-color: white;
      border: 1px solid white;
      box-shadow: none;
      text-transform: none;
      padding: 0;
    }

    .editar-producto_cotizacion a:hover {
      text-decoration-line: underline;
      text-decoration-color: #06c;
      text-underline-offset: 6px;
      border-radius: 6px;
      font-weight: bold;
      color: #06c;
      height: 36px;
      display: block;
      margin-right: 4rem;
      background-color: white;
      border: 1px solid white;
      box-shadow: none;
      text-transform: none;
      padding: 0;
    }

    .btn-eliminar_producto_ct {
      color: red;
      background-color: white;
      border-radius: 6px;
      text-transform: none;
      font-weight: bold;
      margin-top: -4px;
      box-shadow: inset;
      box-shadow: none;
    }

    .btn-eliminar_producto_ct:hover, 
    #top_contenido_lv_productosCotizacion_btn_eliminarProducto_0:hover,
    #top_contenido_lv_productosCotizacion_btn_eliminarProducto_1:hover,
    #top_contenido_lv_productosCotizacion_btn_eliminarProducto_2:hover,
    #top_contenido_lv_productosCotizacion_btn_eliminarProducto_3:hover,
    #top_contenido_lv_productosCotizacion_btn_eliminarProducto_4:hover,
    #top_contenido_lv_productosCotizacion_btn_eliminarProducto_5:hover {
      background-color: #fff;
      box-shadow: none;
      text-decoration-line: underline;
      text-decoration-color: red;
      text-underline-offset: 6px;
    }

    .cotizacionProductos {
      width: 80%;
    }

    .input-count_cotizacion div input {
      padding-left: 1rem !important;
      width: 50% !important;
      float: left;
    }

    .hr-ct {
      color: #f4f4f4;
    }

    .btn-edit_cotizacion {
      margin-left: 2rem !important;
    }

    .btn-edit_cotizacion,
    .btn_convertir_pedido,
    #top_contenido_btn_converPedido,
    #top_contenido_btn_ConvertirMoneda,
    #top_contenido_btn_guardarMetodoEnvio,
    #top_contenido_btn_editarProductos_btn_editar_productos_operacion {
      border: none;
      border-radius: 6px;
      display: inline-block;
      height: 36px;
      line-height: 36px;
      padding: 0 16px;
      text-transform: none;
      vertical-align: middle;
      -webkit-tap-highlight-color: transparent;
      text-decoration: none;
      color: #fff;
      background-color: #878787;
      text-align: center;
      font-weight: bold;
      letter-spacing: 0.5px;
      -webkit-transition: background-color 0.2s ease-out;
      transition: background-color 0.2s ease-out;
      cursor: pointer;
      font-size: 12px;
      outline: 0;
      box-shadow: 0 2px 2px 0 rgba(0, 0, 0, 0.14),
        0 3px 1px -2px rgba(0, 0, 0, 0.12), 0 1px 5px 0 rgba(0, 0, 0, 0.2);
    }

    div.input-field:nth-child(2) > div:nth-child(1) {
      height: 5rem;
    }

    .btn_actions_cotizacion {
      width: 100%;
      text-align: center;
    }

    .input-count_envio
      > div:nth-child(1)
      > div:nth-child(1)
      > input:nth-child(1) {
      height: 36px !important;
    }

    .input-count_envio {
      margin-left: 1.25rem !important;
    }

    .select-wrapper .caret {
      left: 70%;
      margin-top: 10px;
    }

    .input-field {
      margin-top: 0 !important;
    }

    .popup-cotizacion > div:nth-child(1) {
      width: 50%;
    }

    .popup-cotizacion > div.select-wrapper .caret {
      right: 20%;
      margin-top: 10px;
    }

    #top_contenido_modal_tipo_cotizacion > div:nth-child(2) {
      padding-right: 2rem;
      padding-bottom: 2rem;
    }

  </style>
  <script>
    document.addEventListener("DOMContentLoaded", function () {
      $(".modal_tipo_cotizacion").modal();
    });

    $(document).ready(function () {
      $("ul.cotizacionProductos").sortable({
        delay: 200,
        handle: ".imgDrag",

        onDrop: function ($placeholder, container, $closestItemOrContainer) {
          $placeholder
            .removeClass(container.group.options.draggedClass)
            .removeAttr("style");
          $("body").removeClass(container.group.options.bodyClass);

          var contenedor = $placeholder;
          var numero_parte = $placeholder[0].firstElementChild.value;
          var elemento = $placeholder[0];

          var productos = document.querySelectorAll(
            "ul.cotizacionProductos > li"
          );

          var i;
          for (i = 0; i < productos.length; i++) {
            productoLista = productos[i].firstElementChild.value;
            console.log([i] + " - " + productoLista);

            if (numero_parte == productoLista) {
              console.log(productoLista + " posición es: " + [i]);
              __doPostBack(
                "<%= btn_async.ClientID %>",
                numero_parte + "|" + [i]
              );
            }
          }
        },
      });
    });
  </script>
</asp:Content>
