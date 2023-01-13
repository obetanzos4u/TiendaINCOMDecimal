<%@ Page Language="C#" AutoEventWireup="true"   Async="true" CodeFile="cotizaciones-busqueda.aspx.cs" MasterPageFile="~/gnCliente.master" Inherits="usuario_cotizaciones" %>
<%@ Register Src="~/userControls/operaciones/crear_cotizacion_en_blanco_btn.ascx" TagName="cotizacion_en_blanco" TagPrefix="uc1" %>
<%@ Register TagPrefix="uc" TagName="cotizacionEstatus" Src="~/userControls/operaciones/uc_estatus_cotizacion.ascx" %>

<%@ Import Namespace="System.Globalization" %>
<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">

    <div class="is-bt-5 is-mx-6 is-border-soft is-rounded-xl is-p-8">
        <div class="row">
            <div class="col l12">
                <h1 class="title-cotizaciones_asesor is-text-center is-m-0">Cotizaciones por asesor </h1>
            </div>
        </div>
        <!-- Dropdown Trigger -->
        <div class="row is-flex">
            <div class="col m6 l3 xl2 left-align">
                <label>Usuario </label>
                <asp:DropDownList ID="ddl_usuarios"    class="selectize-select  browser-default"
                    OnSelectedIndexChanged="ddl_usuarios_SelectedIndexChanged" runat="server">
                </asp:DropDownList>
            </div>
            <div class="is-flex is-items-center right-align">
                <a id="btn_opciones_cotizaciones" style="text-transform: none;" href='#' data-target='opciones_cotizaciones'>
                    <div class='is-btn-blue is-flex is-items-center'>
                        <img alt="Opciones de cotización" style="width: 1.5rem; height: auto; margin-right: 1rem;" src="/img/webUI/newdesign/opciones_cotizacion.png">
                        Opciones
                    </div>
                </a>
            </div>
        </div>
        <!-- Dropdown Structure -->
        <ul id='opciones_cotizaciones' class='dropdown-content'>
            <li>
                <a class="modal-trigger" onclick=" $('#modal_crear_cotizacion_en_blanco').dropdown('open');" href="#modal_crear_cotizacion_en_blanco">
                    Crear cotización en blanco
                </a>
            </li>
            <li id="link_administrarPlantillas" runat="server">
                <a href="/usuario/mi-cuenta/cotizaciones-plantillas.aspx">Administrar plantillas</a>
            </li>

            <!--  <li><a href="#!">two</a></li>
    <li class="divider"></li>
    <li><a href="#!">three</a></li>
    <li><a href="#!"><i class="material-icons">view_module</i>four</a></li>
    <li><a href="#!"><i class="material-icons">cloud</i>five</a></li>-->
        </ul>
   
        <!-- Modal Trigger -->


<!-- INICIO : Modal Crear cotización en blanco -->
<div id="modal_crear_cotizacion_en_blanco" class="modal">
    <div class="modal-content">
        <h3>Crear cotización en blanco</h3>
        <p>Crea tu cotización y agrega productos después</p>
        <div class="row">
            <div class="input-field col s12 m8 l9">
             <label for="<%= txtNombreCotizacionEnBlanco.ClientID  %>">Nombre de la cotización</label>
                <asp:TextBox ID="txtNombreCotizacionEnBlanco" ClientIDMode="Static" placeholder="Ingresa un nombre de cotización" runat="server"></asp:TextBox>
                 <script>
                /* Script que ayuda a crear la operación al teclear la tecla "enter"  */
                var input = document.getElementById("txtNombreCotizacionEnBlanco");
                input.addEventListener("keyup", function (event) {
                    event.preventDefault();
                    if (event.keyCode === 13) {
                        document.getElementById("btn_crear_cotizacion_en_blanco").click();
                    }
                });

            </script>
            </div>
            <div class="col s12 m4 l3">
                  <label for="<%= ddl_moneda.ClientID  %>">Moneda</label>
                <asp:DropDownList ID="ddl_moneda" runat="server">
                    <asp:ListItem Selected="True" Value="MXN" Text="MXN"></asp:ListItem>
                    <asp:ListItem Value="USD" Text="USD"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col s12 m12 l12">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:LinkButton ID="btn_crear_cotizacion_en_blanco" OnClientClick="btnLoading(this);"
                            CssClass="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5 " ClientIDMode="Static" OnClick="btn_crear_cotizacion_en_blanco_Click"
                            runat="server">Crear cotización en blanco</asp:LinkButton>
                    </ContentTemplate><Triggers><asp:PostBackTrigger ControlID="btn_crear_cotizacion_en_blanco" /></Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal-footer">
        <a href="#!" class="modal-action modal-close waves-effect waves-green btn-flat">Cerrar</a>
    </div>
</div>

 <!-- FIN : Modal Crear cotización en blanco -->
        
        <!-- INICIO : Filtros y orden -->
        <div class="container-cotizaciones_busqueda is-flex">
            <div class="buscador_cotizacion-cotizaciones_busqueda" >
                <label>Busca por nombre de cotización o número de operación</label>
                <asp:TextBox ID="txt_search" placeholder="Nombre de cotización o número de operación"
                    AutoPostBack="true" OnTextChanged="orden" runat="server"></asp:TextBox>
            </div>
            <div class="filtro_periodo-busqueda_cotizaciones" >
                <label>Filtro periodo</label>
                <asp:DropDownList ID="ddl_periodo" AutoPostBack="true" OnSelectedIndexChanged="orden" runat="server">
                    <asp:ListItem Value="6" Text="Últimos 6 meses"></asp:ListItem>
                    <asp:ListItem Value="2021" Text="2021"></asp:ListItem>
                    <asp:ListItem Value="2020" Text="2020"></asp:ListItem>
                    <asp:ListItem Value="2019" Text="2019"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="ordenar_por_fecha-busqueda_cotizaciones">
                <label>Ordenar por</label>
                <asp:DropDownList ID="ddl_ordenBy" AutoPostBack="true" OnSelectedIndexChanged="orden" runat="server">
                    <asp:ListItem Value="fecha_creacion" Text="Fecha"></asp:ListItem>
                    <asp:ListItem Value="nombre_cotizacion" Text="Nombre Operación"></asp:ListItem>
                    <asp:ListItem Value="total" Text="Total"></asp:ListItem>
                    <asp:ListItem Value="productosTotales" Text="Cantidad de Productos"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="ordenar_por_ascendente-busqueda_cotizaciones">
                <label>Ordenar por</label>
                <asp:DropDownList ID="ddl_ordenTipo" AutoPostBack="true" OnSelectedIndexChanged="orden" runat="server">
                        <asp:ListItem Value="DESC" Text="Descendente"></asp:ListItem>
                    <asp:ListItem Value="ASC" Text="Ascendente"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="btn_buscar-cotizaciones_busqueda is-flex is-items-center">
                <asp:Button OnClick="orden" Text="Buscar" CssClass="is-btn-blue is-flex is-items-center" runat="server" />
            </div>
        </div>
        <!-- FIN : Filtros y orden -->

        <div class="row">
            <asp:DataPager  ID="dp_1" class="dataPager_productos" OnPagePropertiesChanging="OnPagePropertiesChanging" runat="server" PagedControlID="lvCotizaciones"
                PageSize="10"  QueryStringField="PageId">
                <Fields>
                    <asp:NextPreviousPagerField RenderNonBreakingSpacesBetweenControls="false" ButtonCssClass="pagerButton"
                        FirstPageText="&lt;&lt; &nbsp;" ShowFirstPageButton="True" ShowNextPageButton="False" />
                    <asp:NumericPagerField CurrentPageLabelCssClass="dataPager_productosCurrentPage" RenderNonBreakingSpacesBetweenControls="true"
                        NextPreviousButtonCssClass="pagerButton" NumericButtonCssClass="pagerButton" />
                    <asp:NextPreviousPagerField RenderNonBreakingSpacesBetweenControls="true" ButtonCssClass="pagerButton"
                        LastPageText=" &nbsp; &gt;&gt;" ShowLastPageButton="True" ShowPreviousPageButton="False" />
                </Fields>
            </asp:DataPager>
        </div>
        <div class="row">
            <div class="col l12">
                <asp:ListView ID="lvCotizaciones" OnItemDataBound="lvCotizaciones_ItemDataBound" runat="server">
                    <LayoutTemplate>
                        <div class="col no-padding-x2 s12 m12 l12">
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                         </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <div class="col no-padding-x2 s12 m12 l6 xl4" style="margin-bottom: 20px;">
                            <div class="card horizontal">
                                <div class="card-stacked">
                                    <div class="card-content grey lighten-4 grey-text text-darken-3">
                                        <asp:TextBox ID="txt_nombre_cotizacion" class=" browser-default nombreCotizaciontitulo tooltipped" data-position="top"
                                            data-delay="50" data-tooltip="Clic para editar"
                                            onchange="txtLoading(this);" AutoPostBack="true" OnTextChanged="cambiarNombreCotizacion"
                                            Text='<%#Eval("nombre_cotizacion") %>' runat="server">
                                        </asp:TextBox>
                                        <div class="col no-padding-x s12 m12 l6 xl6 ">
                                            <strong>Número de operación: </strong>
                                            <asp:Label ID="lbl_numero_operacion" runat="server" Text='<%#Eval("numero_operacion") %>'></asp:Label>
                                        </div>
                                        <div class="col no-padding-x s12 m12 l6 xl6 ">
                                            <strong>Fecha de creación: </strong><%#Eval("fecha_creacion") %>
                                        </div>
                                        <asp:HiddenField ID="hf_id_cotizacionSQL" Value='<%#Eval("id") %>' runat="server" />
                                        <uc:cotizacionEstatus ID="CotizacionEstatus" runat="server" />
                                    </div>
                                    <div class="card-content cotizacionesProductos_min">
                                        <asp:ListView ID="lvProductos" OnItemDataBound="lvProductos_ItemDataBound" runat="server">
                                            <LayoutTemplate>
                                                <table class="bordered">
                                                    <tbody>
                                                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>

                                            <ItemTemplate>
                                                <tr>
                                                    <td style="width: 5%; padding-right: 5px;">
                                                        <%# Eval("numero_parte") %>
                                                    -

                                                        <asp:Literal ID="lt_descripcion" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                No hay ningún producto.
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                        <span class="green-text text-darken-2">
                                            <%# int.Parse(Eval("productosTotales").ToString()) > 5 ? "Mostrando <strong>4</strong> de  <strong>" +  Eval("productosTotales") + " </strong> productos" : " <strong> "+Eval("productosTotales") + " </strong> producto(s)."%>
                                        </span>
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td style="width: 5%; text-align: right; ">
                                                        <strong>Total:</strong>
                                                    </td>
                                                    <td>
                                                        <span class="blue-text text-darken-2">
                                                        <%#(Convert.ToDouble(Eval("total"))).ToString("C2",new CultureInfo("es-MX"))+ " " + Eval("monedaCotizacion") %>
                                                        </span>
                                                        <span class=" blue-grey lighten-5 nota">Impuestos <strong>Incluidos</strong>
                                                        </span><br />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    <!--    <strong>Creada por: </strong><span class="blue-text text-darken-2"><%#Eval("creada_por") %> </span> -->
                                        <strong>Cliente/Usuario: </strong> <br />
                                        <asp:Label ID="lbl_usuario_cliente" CssClass="blue-text text-darken-2" Text='<%#Eval("usuario_cliente") %>' runat="server"></asp:Label>
                                        <strong>Vigencia: </strong><span class="blue-text text-darken-2"><%#Eval("vigencia") %> días.</span>
                                    </div>
                                    <div class="card-action">
                                        <asp:HyperLink ID="btn_visualizar" CssClass="waves-effect waves-light btn blue blue-grey-text text-lighten-5  right-align" runat="server">Enviar</asp:HyperLink>
                                        <a class="waves-effect waves-light btn  blue-grey-text text-darken-2 blue-grey lighten-5   activator right-align">Totales</a>

                                        <a class="btn-floating waves-effect waves-light  darken-2 grey right ddl_cotizacionOpciones"
                                                href='#' data-target='ddl_opciones_operacion<%# Container.DataItemIndex + 1 %>'>
                                            <i class="material-icons">edit</i>
                                        </a>
                                        <!-- Dropdown opciones operación -->
                                        <ul id='ddl_opciones_operacion<%# Container.DataItemIndex + 1%>'   class='dropdown-content'>
                                            <li>
                                                <asp:HyperLink CssClass="blue-text" ID="btn_editarCotizacion"
                                                    runat="server">
                                                <i class="material-icons left">edit</i>Envío y facturación
                                                </asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink CssClass="blue-text" ID="btn_editarProductos" runat="server">
                                                    <i class="material-icons left">edit</i>Editar Productos

                                                </asp:HyperLink>
                                            </li>
                                            <li class="divider" tabindex="-1"></li>
                                        </ul>
                                        <!-- Dropdown opciones operación -->
                                    </div>
                                    <div class="card-action">
                                        <asp:LinkButton ID="btn_activarModoAsesor"
                                            CssClass="waves-effect waves-light btn green  center-align  tooltipped" data-position="top"
                                            data-delay="50" data-tooltip="Activa el modo asesor para este cliente"
                                            OnClick="btn_activarModoAsesor_Click"
                                            runat="server">Activar modo asesor para este cliente</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="card-reveal">
                                    <span class="card-title grey-text text-darken-4">Totales <i class="material-icons right">close</i></span>
                                    <p>
                                        <table class="bordered">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 5%; text-align: right; padding-right: 5px; padding: 5px 5px;">Subtotal</td>
                                                    <td>
                                                        <%# (Convert.ToDouble(Eval("subtotal"))).ToString("C2",new CultureInfo("es-MX")) + " " + Eval("monedaCotizacion") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 5%; text-align: right; padding-right: 5px; padding: 5px 5px;">Envío</td>
                                                    <td><%# (Convert.ToDouble(Eval("envio"))).ToString("C2",new CultureInfo("es-MX")) + " " +  Eval("monedaCotizacion") %> </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 5%; text-align: right; padding-right: 5px; padding: 5px 5px;">Impuestos</td>
                                                    <td><%# (Convert.ToDouble(Eval("impuestos"))).ToString("C2",new CultureInfo("es-MX")) + " " +  Eval("monedaCotizacion") %> </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 5%; text-align: right; padding-right: 5px; padding: 5px 5px;">Total</td>
                                                    <td><%#(Convert.ToDouble(Eval("total"))).ToString("C2",new CultureInfo("es-MX"))+ " " + Eval("monedaCotizacion") %> </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <h3>Aún no tienes cotizaciones realizadas. <a href="/productos/">Puedes crear tu primer cotización ahora.</a></h3>
                    </EmptyDataTemplate>
                </asp:listview>
            </div>
        </div>
    </div>
    <script>
        var options = {  constrainWidth: false};
         document.addEventListener('DOMContentLoaded', function() {
    var elems = document.querySelectorAll('.ddl_cotizacionOpciones');
    var instances = M.Dropdown.init(elems, options);
        });

 
    </script>
</asp:Content>
