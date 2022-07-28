<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" Async="true"  
     enableEventValidation="false"
    CodeFile="cotizacion-productos.aspx.cs"
    MasterPageFile="~/usuario/masterPages/clienteCotizacion.master" Inherits="usuario_cotizacionDatos" %>

<%@ Import Namespace="System.Globalization" %>
<%@ Register Src="~/userControls/operaciones/uc_btn_editar_operacion.ascx" TagName="editarProductos" TagPrefix="uc1" %>
<%@ Register Src="~/userControls/operaciones/uc_btn_detalles_operacion.ascx" TagName="detalles_operacion" TagPrefix="uc1" %>
<%@ Register Src="~/userControls/uc_precio_detalles.ascx" TagName="preciosDetalles" TagPrefix="uc1" %>
<%@ Register Src="~/userControls/operaciones/uc_addProductoPersonalizado.ascx" TagName="addProdPer" TagPrefix="uc1" %>
<%@ Register Src="~/userControls/operaciones/uc_editProductoPersonalizado.ascx" TagName="editProdPer" TagPrefix="uc1" %>
<%@ Register Src="~/userControls/operaciones/uc_editModalProductoPersonalizado.ascx" TagName="editProdPerModal" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <asp:HiddenField ID="hf_id_operacion" runat="server" />
    <asp:HiddenField ID="hf_operacionActiva" runat="server" />

     <div id="content_pedido_creado" runat="server" visible="false" class="container center-align">
         <h1>Pedido creado con éxito</h1>
         <h2>en 3 segundos serás redireccionado</h2>
     </div>
    <div id="content_productos" runat="server" class="container">
            <asp:UpdatePanel ID="up_operacion" UpdateMode="Conditional" class="container" runat="server">
        <ContentTemplate>
         <uc1:detalles_operacion ID="detallesOperacion"   tipo_operacion="cotizacion" runat="server" />
        <div id="content_lv_datos"  runat="server" class="row">
            <div class="col s12 m6 l6 xl6">
                <h1 class="margin-b-2x">
                    Productos de 
                    "<asp:Literal ID="lt_nombre_cotizacion" runat="server"></asp:Literal>" 
                       <asp:LinkButton ID="btn_guardarPlantilla" data-tooltip="Guarda este listado de productos para cotizaciones" 
                       OnClick="btn_guardarPlantilla_Click"      CssClass="tooltipped" runat="server"><i class="small material-icons">save</i></asp:LinkButton>
                </h1>
                <label>Número de operacion</label>
                <h2 class="margin-t-2x margin-b-2x">
                    <asp:Literal ID="lt_numero_operacion" runat="server"></asp:Literal>

                </h2>
                <label>Cliente</label>
                <h2 class="margin-t-2x">
                    <asp:Literal ID="lt_cliente_nombre" runat="server"></asp:Literal>

                </h2>
                <asp:HyperLink ID="hl_editarDatos" CssClass="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5 "
                    runat="server"><i class="material-icons left">edit</i>Regresar a  Contacto, envío y facturación</asp:HyperLink>
                &nbsp;<asp:HyperLink ID="hl_editarProductos" CssClass="hide waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-4 "
                    runat="server"><i class="material-icons left">edit</i> Productos</asp:HyperLink>
                               
                    <asp:LinkButton ID="btn_converPedido" CssClass="waves-effect waves-light btn green blue-grey-text text-lighten-5"
                        OnClick="btn_converPedido_Click" OnClientClick="btnLoading(this);" runat="server">Convertir cotización a pedido <i class="material-icons left">shopping_basket</i></asp:LinkButton>
                </div>
            <div style="float: right;" class="col s12 m6 l5 xl5 right-align">
                <h1>Total de la cotización: </h1>
                <div style="padding: 1rem 10px; font-size: 1.2rem" class=" blue-grey lighten-5">
                    <div id="content_envioCliente" visible="true" runat="server">
                     Método de envío : 
                          <strong> <asp:Label ID="lbl_metodo_envio" runat="server"></asp:Label>
                        <asp:Label ID="lbl_envio" runat="server"></asp:Label></strong> 
                    </div>
                     <div id="content_descuento" visible="false" runat="server">
                         Sub Total  <strong> <asp:Label ID="lbl_subtotalSinDescuento" runat="server"></asp:Label></strong><br />
                        Descuento aplicado:  <strong>
                            <asp:Label ID="lbl_descuento_porcentaje" runat="server"></asp:Label>%</strong><br />
                         </div> 
                    Sub Total antes de IVA: <strong>
                        <asp:Label ID="lbl_subTotal" runat="server"></asp:Label></strong><br />
                

                    Impuestos:  <strong>
                        <asp:Label ID="lbl_impuestos" runat="server"></asp:Label></strong><br />

                    <span class="orange-text  text-darken-3">Total: </span><strong>
                        <asp:Label ID="lbl_total" runat="server"></asp:Label></strong>

                    <asp:Label ID="lbl_moneda" CssClass="orange-text" runat="server"></asp:Label>
                   <br />
                    <asp:LinkButton ID="btn_ConvertirMoneda" CssClass="btn btn-s blue tooltipped"
                        data-tooltip="Convertir la moneda de la cotización también cambiará la vigencia de esta, posiblemente sea necesario renovar los precios si
                       la cotización venciera debido vigencia."
                        OnClientClick="btnLoading(this);" OnClick="btn_ConvertirMoneda_Click" runat="server"></asp:LinkButton>
                </div>
                <div class="col m12 l12   right-align " style="padding: 1rem 10px;">

                    <div id="content_envioUsuario" runat="server" visible="false" class="input-field inline">
                        Método de envío.
                        <br />
                        <div class="input-field inline">
                            <asp:DropDownList ID="ddl_metodo_envio" OnSelectedIndexChanged="ddl_metodo_envio_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                <asp:ListItem Value="Ninguno" Text="Ninguno"></asp:ListItem>
                                <asp:ListItem Value="Estándar" Text="Estándar"></asp:ListItem>
                                <asp:ListItem Value="En Tienda" Text="En Tienda"></asp:ListItem>
                                <asp:ListItem Value="Gratuito" Text="Gratuito"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="input-field inline">
                            <asp:TextBox ID="txt_envio" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="btn_guardarMetodoEnvio" class="waves-effect waves-teal  btn-small" OnClick="btn_guardarMetodoEnvio_Click" runat="server">Guardar Envio</asp:LinkButton>
                        </div>
                        <div>
                            <label>
                                <asp:CheckBox ID="chk_CalculoAutomáticoEnvio" OnCheckedChanged="chk_CalculoAutomáticoEnvio_CheckedChanged" AutoPostBack="true" runat="server" />
                                <span>Calculo automático en esta cotización.</span>
                            </label>
                        </div>
                    </div>
                </div>
                <div id="content_descuento_asesor" runat="server">
                    Descuento en %
                    <asp:TextBox ID="txt_descuento" placeholer="Establece %"
                        Style="width: 70px;" OnTextChanged="txt_descuento_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                </div>
                <div class="col m12 l12   right-align " style="padding: 1rem 10px;">
                    <uc1:editarProductos ID="btn_editarProductos" id_operacion='<%# hf_id_operacion.Value %>' tipo_operacion="cotizacion" runat="server" />
                    <uc1:addProdPer ID="addProdPer" Visible="false" runat="server" />

                </div>
                <div class="col m12 l12   right-align " style="padding: 0rem 10px 1rem 10px;">
                    <asp:HyperLink ID="link_enviar" CssClass="waves-effect waves-light btn-large blue" runat="server">
                          Confirmar y Enviar cotización <i class="material-icons left">email</i>
                    </asp:HyperLink>
                </div>
            </div>
        </div>

        <div id="content_lv_productos"  runat="server" class="row">
            <h2>Edita tus productos, cantidades</h2>

            <asp:ListView ID="lv_productosCotizacion" OnItemDataBound="lv_productos_OnItemDataBound" runat="server">
                <LayoutTemplate>
                    <div class="row line-bottom ">


                        <div class="col s12 m3 l2 "><strong>Producto</strong></div>
                        <div class="col s12 m3 l5"><strong>Descripción </strong></div>
                        <div class="col s12 m3 l3 right-align"><strong>Precio Total</strong></div>
                        <div class="col s12 m3 l2"><strong>Cantidad</strong></div>

                    </div>
                    <ul class="cotizacionProductos">
                    <div runat="server" id="itemPlaceholder"></div>
                    </ul>

                </LayoutTemplate>
                <ItemTemplate>

                    <li class="row">
                          <asp:HiddenField ID="hf_numero_parte" Value='<%#Eval("numero_parte") %>' runat="server" />
                        <div class="col s12 m3 l2">
                            <asp:HiddenField ID="hf_idProducto" Value='<%#Eval("id") %>' runat="server" />
                            <asp:HiddenField ID="hf_tipoProducto" Value='<%#Eval("tipo") %>' runat="server" />
                            <asp:HyperLink ID="link_imgProducto" Target="_blank" runat="server">
                            <asp:Image ID="imgProducto" CssClass="responsive-img  imgDrag" runat="server" />

                            </asp:HyperLink>
                        </div>

                        <div class="col s12 m4 l5">
                            <h2 class="margin-b-2x " style="margin-top: 5px;">
                                <asp:Label ID="lbl_activo" Style="color: red;" Visible="false" runat="server"></asp:Label>
                                <asp:Literal ID="lt_numeroParte" Text='<%#Eval("numero_parte") %>' runat="server"></asp:Literal>
                                - <%#Eval("marca") %>
                                 <asp:HyperLink Target="_blank" ID="link_producto" runat="server">
                                       <i class="material-icons">launch</i>
   
                                    </asp:HyperLink>
                            </h2>
                            <uc1:editProdPer ID="editProdPer" Visible="false"     runat="server" />
                            <div>
                                Precio Unitario:<strong>
                                  <asp:Label ID="lbl_precio_unitario" runat="server"></asp:Label></strong>
                                <asp:Label ID="lbl_moneda_producto" runat="server"  ></asp:Label>
                                  <uc1:preciosDetalles ID="detalles_precios"  runat="server"></uc1:preciosDetalles>
                                  <span class=" blue-grey lighten-5 nota">Impuestos <strong>No incluidos</strong> </span><br />
                            </div>
                            <div><%#Eval("descripcion") %></div>
                   
                                <asp:UpdatePanel ID="up_opcionesProducto" UpdateMode="Conditional" class="margin-t-1x" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="content_btn_productoAlternativo"
                                            class="margin-b-4x tooltipped" runat="server" Visible="false" data-position="bottom" data-delay="50"
                                            data-tooltip="Establece si es un producto como sugerencia o alternativo a otro producto.">
                                        </asp:Panel>
                                        <div>
                                            <label>
                                                <asp:CheckBox ID="chk_alternativo" AutoPostBack="true" OnCheckedChanged="chk_alternativo_CheckedChanged"  runat="server" />
                                                <span>Alternativo</span>
                                            </label>
                                        </div>
                                        <asp:LinkButton CssClass="btn btn-s deep-orange text-darken-1" OnClientClick=" return confirm('Confirma que deseas ELIMINAR este producto');
                                            btnLoading(this);"
                                            OnClick="btn_eliminarProducto_Click" ID="btn_eliminarProducto" runat="server">Eliminar</asp:LinkButton>
                                       
                                    </ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="chk_alternativo" EventName="CheckedChanged" /></Triggers>
                                </asp:UpdatePanel>
                        </div>
                        <div class="col s12 m3 l3 right-align">
                            <asp:Label ID="lbl_precio_total" runat="server"></asp:Label>
                        </div>
                        <div class="col s12 m3 l2 right-align">
                               <asp:UpdatePanel UpdateMode="Always" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txt_cantidadCotizacion" OnTextChanged="txt_cantidadCotizacion_TextChanged" AutoPostBack="true" type="number" onchange="txtLoading(this);"

                                Text='<%#Eval("cantidad") %>'  runat="server"></asp:TextBox>
                              <asp:TextBox ID="txt_cantidadCotizacionPersonalizado" OnTextChanged="txt_cantidadCotizacionPersonalizado_TextChanged" AutoPostBack="true" type="number" onchange="txtLoading(this);"
                                  Text='<%#Eval("cantidad") %>' runat="server"></asp:TextBox>
                                    </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txt_cantidadCotizacion" EventName="TextChanged" />
                                     <asp:AsyncPostBackTrigger ControlID="txt_cantidadCotizacionPersonalizado" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>


                    </li>
                </ItemTemplate>
            </asp:ListView>

        </div>

            <uc1:editProdPerModal ID="editProdPersonalizadoModal" runat="server"></uc1:editProdPerModal>

        </ContentTemplate>
            </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel ID="upAsync" style="display: none;" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <asp:LinkButton ID="btn_async" OnClick="btn_async_Click" runat="server"></asp:LinkButton>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_async" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>

     <!-- Modal Structure -->
    <div id="modal_tipo_cotizacion" class="modal_tipo_cotizacion modal" runat="server" >
        <div class="modal-content">
            <h2>Selecciona el tipo de cotización</h2>
            <p>Es importante clasificar tu cotización si es que aplica a uno de los siguientes casos:</p>
            <div class="row">
                <div class=" col s12 m12 l12">
                    <asp:DropDownList ID="ddl_tipo_cotizacion" OnSelectedIndexChanged="ddl_tipo_cotizacion_SelectedIndexChanged" AutoPostBack="true" runat="server">
                        <asp:ListItem Value="" Text="Ninguno"></asp:ListItem>
                        <asp:ListItem Value="Licitación" Text="Licitación"></asp:ListItem>
                        <asp:ListItem Value="Proyecto" Text="Proyecto"></asp:ListItem>
                        <asp:ListItem Value="Comparativo" Text="Comparativo"></asp:ListItem>
                        <asp:ListItem Value="Regular" Text="Regular"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
              <div class="row">
                <div class=" col s12 m12 l12">
                    <br /><br /><br /><br />
                </div></div>
        </div>
        <div class="modal-footer">
            <a href="#!" class="modal-action modal-close waves-effect waves-green btn-flat">Cerrar</a>
        </div>
    </div>
    <script>

       

        document.addEventListener('DOMContentLoaded', function () {
               $('.modal_tipo_cotizacion').modal();
   
  });
       


 
        $(document).ready(function () {

            $("ul.cotizacionProductos").sortable({
                delay: 200,
                handle : ".imgDrag" ,
 
                onDrop: function ($placeholder, container, $closestItemOrContainer) {
                    $placeholder.removeClass(container.group.options.draggedClass).removeAttr("style")
                    $("body").removeClass(container.group.options.bodyClass)

                        var contenedor = $placeholder;
                        var numero_parte = $placeholder[0].firstElementChild.value;
                        var elemento = $placeholder[0];

                        var productos = document.querySelectorAll("ul.cotizacionProductos > li");

                        var i;
                        for (i = 0; i < productos.length; i++) { 
                            productoLista = productos[i].firstElementChild.value;
                                console.log([i]+ " - " + productoLista);
                           
                            if (numero_parte == productoLista) {
                                   console.log(productoLista + " posición es: " + [i]);
                                 __doPostBack('<%= btn_async.ClientID %>',numero_parte+"|"+[i]);
                            }
                           

                        }

                    }   
                     
                });

        });
    </script>
</asp:Content>
