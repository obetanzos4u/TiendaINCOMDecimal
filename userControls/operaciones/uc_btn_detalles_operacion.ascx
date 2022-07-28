<%@ Control Language="C#" AutoEventWireup="true"   CodeFile="uc_btn_detalles_operacion.ascx.cs" Inherits="uc_btn_detalles_operacion" %>

<asp:HiddenField ID="hf_id_operacion" runat="server" />
<asp:HiddenField ID="hf_numero_operacion" runat="server" />
<asp:HiddenField ID="hf_tipo_operacion" runat="server" />
<asp:HiddenField ID="hf_monedaCotizacion" runat="server" />
<div>

      <asp:UpdatePanel ID="upEstatusOperacion" UpdateMode="Conditional" class="row margin-t-4x line-bottom" runat="server"><ContentTemplate>
 
    <div id="cont_operacion_vencida" style="padding: 17px 5px;
    border: #f7b323 1px solid;
    background: #ffffe7;
    margin: 10px 0px;
    text-align: center;" visible="false" class="m12 l12" runat="server">
        Esta cotización ya ha vencido, puedes <a class=" tooltipped" data-position="bottom" data-delay="50"
            data-tooltip="Tu cotización conservará todos sus productos y datos, solamente se actualizaran los precios.">renovarla</a> y mantener tus productos con  precios actualizados.
   <br />
              <strong>
                  <asp:LinkButton ID="btn_renovar" OnClick="btn_renovar_Click" OnClientClick="btnLoading(this);" Text="Renovar cotización ahora" runat="server"> </asp:LinkButton>

              </strong>
    </div>
    <div id="content_resultado_renovar_cotizacion" class="incom-mensaje-error" visible="false" runat="server">
        <div id="resultado_renovar_cotizacion"   runat="server"></div>
        <asp:LinkButton id="cerrarResultadoRenovarCotizacion" OnClick="cerrarResultadoRenovarCotizacion_Click" Text="Cerrar"
            CssClass="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5 " runat="server"></asp:LinkButton>
    </div>
       <div id="cont_cotizacionPedido" style="padding: 17px 5px;
    border: #f7b323 1px solid;
    background: #ffffe7;
    margin: 10px 0px;
    text-align: center;" visible="false" class="m12 l12" runat="server">
        Esta cotización ya ha convertido en pedido.
   
              <strong>
                  <asp:HyperLink ID="link_pedidoFromCotizacion" runat="server">Ver pedido</asp:HyperLink>

              </strong>
    </div>

    <asp:Panel ID="cont_operacion_status" CssClass="col s12 m6 l6 xl6" runat="server">
        <div class="row ">
            <div class="m12 l12">
                <asp:Literal ID="lt_tipo_peracion" runat="server"></asp:Literal>Creada el
                <strong>
                    <asp:Label ID="lbl_fecha_operacion" runat="server" Text=""></asp:Label></strong>, vigencia de
                <asp:Label ID="lbl_vigencia" runat="server" Text=""></asp:Label>
                día(s)
            </div>


        </div>


    </asp:Panel>
    <div id="Div1" class="col s12 m6 l6 xl6 right-align " visible="true" runat="server">
        <div class="m12 l12 ">
            <!-- Dropdown Trigger -->
            <a class='dropdown-trigger btnOpcionesCotizacion btn waves-effect waves-light  blue-grey darken-1 ' href='#' data-target='OpcionesCotizacion'>
                <i class="left material-icons">build</i> Opciones</a>

            <!-- Dropdown Structure -->
            <ul id="OpcionesCotizacion" class='dropdown-content'>
                <li>
                    <asp:LinkButton ID="LinkButton1" OnClick="btn_renovar_Click" Text="Renovar cotización ahora" runat="server"> </asp:LinkButton>
                </li>
                <li>
                    <a href="#modal_quickAddProducts" class="modal-trigger" id="btn_quickAddProducts">Agregar productos rápido</a>

                </li>
                <li>
                    <a href="#modal_tipo_cotizacion" onclick="$('.modal_tipo_cotizacion').modal('open');"   class="modal-trigger" id="btn_tipoCotizacion" >Tipo de cotización</a>
                </li>
                <li visible="false" runat="server"><a href="#!">two</a></li>
                <li visible="false" runat="server" class="divider"></li>
                <li visible="false" runat="server"><a href="#!">three</a></li>
                <li visible="false" runat="server"><a href="#!"><i class="material-icons">view_module</i>four</a></li>

            </ul>



        </div>
    </div>

    <div class="col l12">
        <span class=" blue-grey lighten-5 nota">Nota: Las cotizaciones en moneda nacional <strong>(MXN)</strong> tienen una vigencia de  1 día.</span>
        <span class=" blue-grey lighten-5 nota">Nota: Las cotizaciones en moneda extranjera  <strong>(USD)</strong> tienen una vigencia de  30 días.</span>
    </div>
 </ContentTemplate></asp:UpdatePanel>
<!-- Modal Structure -->
<div id="modal_quickAddProducts" class="modal">
    <div class="modal-content">
        <h4>Agrega productos a tu cotización</h4>
        <p>Si conoces el número de parte de tus productos, podras agregarlos de manera rápida.</p>
        <p>
            Simplemente escribe el numero de parte <strong>seguido de un espacio</strong> y su cantidad. 
            Para multiples productos <strong>ingresa un salto de linea (enter)</strong> por producto
        </p>
        <asp:UpdatePanel ID="up_QuickAdd" UpdateMode="Conditional" class="row" runat="server">
            <ContentTemplate>
                <div class="col s12 m12 l12">
                    <asp:TextBox ID="txt_productosQuickAdd" CssClass="materialize-textarea" Height="40" TextMode="MultiLine"
                        placeholder="NúmeroParte Cantidad &#10;NúmeroParte Cantidad&#10;NúmeroParte Cantidad" runat="server"></asp:TextBox>
                </div>
                <div class="inline col s12 m12 l12">
                         <label  > O bien selecciona una plantilla de tu listado</label>  
                    <asp:DropDownList ID="ddl_plantillasPersonalizadas" AutoPostBack="true" OnSelectedIndexChanged="ddl_plantillasPersonalizadas_SelectedIndexChanged" runat="server"></asp:DropDownList>
               <asp:LinkButton ID="btn_eliminarPlantilla" OnClick="btn_eliminarPlantilla_Click" CssClass="red-text" runat="server">
                   Eliminar plantilla seleccionada</asp:LinkButton>
                    </div>
                <div class="margin-t-4x col s12 m12 l6">
                 <br />
                    <asp:LinkButton ID="agregarProductosQuick" OnClientClick="btnLoading(this);" OnClick="agregarProductosQuick_Click"
                        CssClass="btn waves-effect waves-light blue-grey darken-1" runat="server">
                            Agregar productos</asp:LinkButton>
                    <br />
                    <asp:Label ID="lbl_result_quickAddProducts" runat="server"></asp:Label>
            </ContentTemplate>
            <Triggers>
                   <asp:AsyncPostBackTrigger ControlID="btn_eliminarPlantilla" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="agregarProductosQuick" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_plantillasPersonalizadas" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>

    </div>
    </div>
    <div class="modal-footer">
        <a href="#!" class="modal-action modal-close waves-effect waves-green btn-flat">Cerrar ventana</a>
    </div>
</div>

 