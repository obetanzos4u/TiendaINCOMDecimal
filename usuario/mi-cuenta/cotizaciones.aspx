<%@ Page Language="C#" AutoEventWireup="true"   Async="true" CodeFile="cotizaciones.aspx.cs" MasterPageFile="~/gnCliente.master" Inherits="usuario_cotizaciones" %>
<%@ Register Src="~/userControls/operaciones/crear_cotizacion_en_blanco_btn.ascx" TagName="cotizacion_en_blanco" TagPrefix="uc1" %>
<%@ Register TagPrefix="uc" TagName="cotizacionEstatus" Src="~/userControls/operaciones/uc_estatus_cotizacion.ascx" %>

<%@ Import Namespace="System.Globalization" %>
<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">

    <div class="is-bt-5 is-mx-6 is-border-soft is-rounded-xl is-p-8">
        <div class="row">
            <div class="">
                <h2 class="is-text-center is-m-0 is-bt-1 is-font-bold is-text-black-soft">Mis cotizaciones </h2>
            </div>
            <div class="">
                <h3>Encuentra cualquiera de tus cotizaciones realizadas:</h3>
            </div>
        </div>
        <!-- Dropdown Trigger -->
        <div class="row is-w-full">
            <div class="input-field right-align" style="float: right;">
                <a id="btn_opciones_cotizaciones" class='dropdown-trigger' href='#' style='text-transform: none;'  data-target='opciones_cotizaciones'>
                    <div class="is-btn-blue is-flex is-items-center">
                        <img alt="Opciones de cotización" style="width: 1.5rem; height: auto; margin-right: 1rem;" src="/img/webUI/newdesign/opciones_cotizacion.png" />
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
<div id="modal_crear_cotizacion_en_blanco" class="modal no-autoinit">
    <div class="modal-content">
        <h3>Crear cotización en blanco</h3>
        <p>Crea tu cotización y agrega productos después:</p>
        <div class="row">
            <div class="input-field">
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
           
            <div class="  col s12 m4 l3">
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
                            CssClass="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5" ClientIDMode="Static" OnClick="btn_crear_cotizacion_en_blanco_Click"
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
        <div class="row">
            <div class="col s12 m5">
                <label>Busca por: Nombre o número de operación</label>
                <asp:TextBox ID="txt_search" placeholder="Busca por: Nombre de cotización o número de operación" AutoPostBack="true" OnTextChanged="orden" runat="server"></asp:TextBox>
            </div>

            <div class="col s6 m4 l2">
                <label>Filtro por año </label>
                <asp:DropDownList ID="ddl_periodo" AutoPostBack="true" OnSelectedIndexChanged="orden" runat="server">
                    <asp:ListItem Value="6" Text="Últimos 6 meses"></asp:ListItem>
                    <asp:ListItem Value="2022" Text="2022"></asp:ListItem>
                    <asp:ListItem Value="2021" Text="2021"></asp:ListItem>
                    <asp:ListItem Value="2020" Text="2020"></asp:ListItem>
                    <asp:ListItem Value="2019" Text="2019"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="col s6 m4 l2">
                <label>Ordenar por</label>
                <asp:DropDownList ID="ddl_ordenBy" AutoPostBack="true" OnSelectedIndexChanged="orden" runat="server">
                    <asp:ListItem Value="fecha_creacion" Text="Fecha"></asp:ListItem>
                    <asp:ListItem Value="nombre_cotizacion" Text="Nombre Operación"></asp:ListItem>
                    <asp:ListItem Value="total" Text="Total"></asp:ListItem>
                    <asp:ListItem Value="productosTotales" Text="Cantidad de Productos"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="col s6 m3 l2">
                <label>Ordenar por</label>
                <asp:DropDownList ID="ddl_ordenTipo" AutoPostBack="true" OnSelectedIndexChanged="orden" runat="server">
                    <asp:ListItem Value="DESC" Text="Descendente"></asp:ListItem>
                    <asp:ListItem Value="ASC" Text="Ascendente"></asp:ListItem>
                </asp:DropDownList>
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
                                            Text='<%#Eval("nombre_cotizacion") %>' runat="server"></asp:TextBox>
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
                                                <table class="bordered operacionListadoProductos">
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
                                        <%# int.Parse(Eval("productosTotales").ToString()) > 5 ? "Mostrando <strong>5</strong> de  <strong>" +  Eval("productosTotales") + " </strong> productos" : " <strong> "+Eval("productosTotales") + " </strong> producto(s)."%> 
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
                                                        <span class=" blue-grey lighten-5 nota">Impuestos <strong>Incluidos</strong> </span><br />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="card-content ">
                                        <strong>Creada por: </strong><span class="blue-text text-darken-2"><%#Eval("creada_por") %> </span>
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
                                                <li id="ContentEliminarPermanentemente" runat="server">
                                                <asp:LinkButton ID="btn_EliminarPermanentemente" runat="server" Visible="false" OnClick="btn_EliminarPermanentemente_Click">
                                                    <i class="material-icons left">delete</i> Eliminar Permanentemente</asp:LinkButton>
                                            </li>
                                            <li class="divider" tabindex="-1"></li>
                                        </ul>
                                                <!-- Dropdown opciones operación -->
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
                        <h3>Aún no tienes cotizaciones realizadas. <a href="/productos/">Elige tus productos para crear tu primer cotización.</a></h3>
                    </EmptyDataTemplate>
                </asp:listview>
            </div>
        </div>
    </div>

    <script>

    $('.ddl_cotizacionOpciones').dropdown({
        constrainWidth: false, // Does not change width of dropdown to that of the activator
        gutter: 0, // Spacing from edge
        belowOrigin: false, // Displays dropdown below the button
        alignment: 'left', // Displays dropdown with edge aligned to the left of button
        stopPropagation: false // Stops event propagation
        }
    );
    </script>
</asp:Content>
