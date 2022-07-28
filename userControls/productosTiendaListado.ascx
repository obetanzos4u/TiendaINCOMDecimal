<%@ Control Language="C#" AutoEventWireup="true" CodeFile="productosTiendaListado.ascx.cs" Inherits="userControls_productosTiendaListado" %>
<%@ Register Src="~/userControls/uc_btn_agregar_carritoListado.ascx" TagName="add" TagPrefix="uc_addCarrito" %>
<%@ Register Src="~/userControls/operaciones/uc_btn_agregar_operacion.ascx" TagName="btn_addOperacion" TagPrefix="uc1" %>
<%@ Register Src="~/userControls/operaciones/uc_modal_agregar_operacion.ascx" TagName="mdl_addOperacion" TagPrefix="uc1" %>
<%@ Register Src="~/userControls/moneda.ascx" TagName="moneda" TagPrefix="uc_mon" %>
<%@ Register Src="~/userControls/productosVisitados.ascx" TagName="visitados" TagPrefix="productos" %>
<%@ Register Src="~/userControls/uc_producto_btn_SoloVisualizar.ascx" TagName="link" TagPrefix="uc_visualizarProducto" %>

<style>
    @media (min-width: 700px) {
        #contentResultados {
            margin: auto 0px !important;
            display: grid;
            grid-gap: 5px;
            grid-template-columns: 170px auto;
            grid-template-areas: 'sidebar content'
        }

        .contentResultados-sidedar {
            grid-area: sidebar;
            position: fixed;
            width: min-content;
            left: 0px;
        }

        .contentResultados-content {
            grid-area: content;
        }
    }
</style>
<script>

    async function producto_agregar_carrito_Service(btn) {

        var idLoading = btnLoadingHide(btn);

        var numero_parte = document.querySelector("#producto_disponibilidad_numero_parte").textContent;
        var cantidad = document.querySelector("#txt_producto_cantidad_disponibilidad").value;


        ///AJAX request
        $.ajax(
            {
                ///server script to process data
                //url: "/service/producto_add_carrito.ashx", //web service
                url: "/service/producto-add-carrito.aspx?numero_parte=" + numero_parte + "&cantidad=" + cantidad, //web service
                type: 'POST',
                complete: function () {
                    //on complete event     
                },
                progress: function (evt) {
                    //progress event    
                },
                ///Ajax events
                beforeSend: function (e) {
                    //before event  
                },
                success: function (e) {

                    var jsonResultado = JSON.parse(e);

                    console.log(jsonResultado);

                    crear_toast(jsonResultado.message, jsonResultado.result)
                    $("#btnTotalProductosCarrito").click();

                    btnLoadingShow(btn, idLoading);

                },
                error: function (e) {
                    crear_toast("Ocurrio un error intenta más tarde", false);
                    btnLoadingShow(btn, idLoading);

                    //errorHandler
                },

                //  data: formData,
                ///Options to tell JQuery not to process data or worry about content-type
                cache: false,
                contentType: false,
                processData: false
            });



    } 
</script>
<div class="row" style="margin: auto 0px !important;">

    <div class="col s12 m12 l12 xl12 " style="margin: auto 0px !important; min-height: 330px;">
        <div runat="server" id="content_resultado_busqueda_text" visible="false" style="position: sticky; top: 100px; background: white; z-index: 99; overflow: auto; height: 53px;">
            <h2 style="margin: 0px; line-height: 50px;">Resultado de la búsqueda de: <span class="blue-text">
                <asp:HyperLink ID="linkTerminoBusqueda" runat="server"></asp:HyperLink>
                <asp:Literal ID="lt_termino_busqueda" runat="server"></asp:Literal></span></h2>
        </div>
        <!-- INICIO : Filtros y orden -->
        <div class="row" style="margin: auto 0px !important;" id="cont_ordenar" runat="server">


            <div class="col s12 m5 l4" visible="false" runat="server">
                <label>Busca por: Nombre de cotización ó Número de operación</label>
                <asp:TextBox ID="txt_search" placeholder="Busca por: Nombre de cotización ó Número de operación" AutoPostBack="true" OnTextChanged="cargarProductos" runat="server"></asp:TextBox>
            </div>


        </div>
        <!-- FIN : Filtros y orden -->
        <div id="contentResultados">
            <div class="contentResultados-sidedar">


                <div runat="server" id="Div1" class="input-field fixInput">
                    <label style="position: initial;">Filtrar por categoria</label>
                    <asp:DropDownList ID="ddl_filtroCategorias" class="browser-default  ddlDefault" AutoPostBack="true" OnSelectedIndexChanged="orden" runat="server">
                    </asp:DropDownList>



                </div>
                <div runat="server" id="cont_filtros" class="input-field fixInput">
                    <label style="position: initial;">Filtrar por marca</label>
                    <asp:DropDownList ID="ddl_filtroMarcas" class="browser-default  ddlDefault" AutoPostBack="true" OnSelectedIndexChanged="orden" runat="server">
                    </asp:DropDownList>
                    <asp:RadioButtonList ID="rd_filtroMarcas" Visible="false" OnSelectedIndexChanged="orden" AutoPostBack="true" RepeatDirection="Vertical" CssClass="ulFlow"
                        RepeatLayout="UnorderedList" runat="server">
                        <asp:ListItem Text="Todas las marcas" Value=""></asp:ListItem>
                    </asp:RadioButtonList>


                </div>

                <div class="input-field fixInput hide">
                    <label style="position: initial;">Ordenar por</label>
                    <asp:DropDownList ID="ddl_ordenBy" class="browser-default  ddlDefault" AutoPostBack="true" OnSelectedIndexChanged="orden" runat="server">
                        <asp:ListItem Value="orden" Text="Automático"></asp:ListItem>
                        <asp:ListItem Value="numero_parte" Text="Número de Parte"></asp:ListItem>
                        <asp:ListItem Value="marca" Text="Marca"></asp:ListItem>
                        <asp:ListItem Value="precio1" Text="Precio"></asp:ListItem>
                    </asp:DropDownList>

                </div>
                <div class="input-field  fixInput hide">
                    <label style="position: initial;">Ordenar por</label>
                    <asp:DropDownList ID="ddl_ordenTipo" class="browser-default  ddlDefault" AutoPostBack="true" OnSelectedIndexChanged="orden" runat="server">
                        <asp:ListItem Value="ASC" Text="Ascendente"></asp:ListItem>
                        <asp:ListItem Value="DESC" Text="Descendente"></asp:ListItem>
                    </asp:DropDownList>

                </div>

                <div class="input-field fixInput">
                    <label style="position: initial;">Moneda</label>
                    <uc_mon:moneda ID="uc_moneda" runat="server"></uc_mon:moneda>


                </div>


               

                <div class="fixInput  hide-on-small-only hide-on-med-only">
                    <productos:visitados ID="ProductosVisitados" runat="server"></productos:visitados>
                </div>

            </div>
            <div class="contentResultados-content">


                <asp:ListView ID="lv_productos" OnItemDataBound="lv_productos_OnItemDataBound" runat="server">
                    <LayoutTemplate>
                        <div class="row">
                            <asp:DataPager ID="dp_1" class="dataPager_productos" runat="server" Visible="true" PagedControlID="lv_productos"
                                PageSize="50" QueryStringField="PageId">
                                <Fields>
                                    <asp:NextPreviousPagerField RenderNonBreakingSpacesBetweenControls="false" PreviousPageText=" < " ButtonCssClass="pagerButton"
                                        FirstPageText=" Atrás  &nbsp;" ShowFirstPageButton="True" ShowNextPageButton="False" />
                                    <asp:NumericPagerField CurrentPageLabelCssClass="dataPager_productosCurrentPage" RenderNonBreakingSpacesBetweenControls="true"
                                        NextPreviousButtonCssClass="pagerButton" NumericButtonCssClass="pagerButton" />
                                    <asp:NextPreviousPagerField RenderNonBreakingSpacesBetweenControls="true" NextPageText=" > " ButtonCssClass="pagerButton"
                                        LastPageText=" &nbsp; Siguiente " ShowLastPageButton="True" ShowPreviousPageButton="False" />
                                </Fields>
                            </asp:DataPager>

                        </div>
                        <div class="row">
                            <div runat="server" id="itemPlaceholder"></div>
                        </div>
                        <div class="row">
                            <asp:DataPager ID="dp_2" class="dataPager_productos" Visible="true" runat="server" style="margin: 13px 0px;" PagedControlID="lv_productos"
                                PageSize="50" QueryStringField="PageId">
                                <Fields>
                                    <asp:NextPreviousPagerField RenderNonBreakingSpacesBetweenControls="false" ButtonCssClass="pagerButton"
                                        PreviousPageText="Anterior &nbsp;" FirstPageText=" « Primera;" ShowFirstPageButton="False" ShowNextPageButton="False" />
                                    <asp:NumericPagerField CurrentPageLabelCssClass="pagerButtonCurrentPage" RenderNonBreakingSpacesBetweenControls="false"
                                        NextPreviousButtonCssClass="pagerButton" NumericButtonCssClass="pagerButton" />
                                    <asp:NextPreviousPagerField RenderNonBreakingSpacesBetweenControls="false" ButtonCssClass="pagerButton"
                                        LastPageText=" Última »" NextPageText=" Siguiente »" ShowLastPageButton="False" ShowPreviousPageButton="False" />
                                </Fields>
                            </asp:DataPager>

                        </div>

                    </LayoutTemplate>
                    <ItemTemplate>
                        <div id="item_producto" runat="server" class="cajaProductosFCO ">
                            <div style="padding: 0px !important" class="card ">
                                <div class='card-image waves-effect waves-block waves-light content_imgProducto_<%# Eval("id") %>'>
                                    <asp:Panel ID="contentSlider" runat="server"></asp:Panel>

                                    <asp:HyperLink ID="link_productoIMG" Visible="false" runat="server">
                                       
                                    </asp:HyperLink>
                                    <uc1:btn_addOperacion ID="productoAddOperacion" numero_parte='<%# Eval("numero_parte") %>'
                                        descripcion_corta='<%# Eval("descripcion_corta") %>' runat="server"></uc1:btn_addOperacion>
                                        <asp:Label ID="lbl_descuento_porcentaje_fantasma"   Visible="false"
                                        class="red white-text" style="padding: 2px 5px; position: absolute; top: 24px;" runat="server" ></asp:Label>
                                </div>

                                <div class="card-content descripciónProductoListado" style="padding-top: 12px;">
                                    <h2 class="margin-b-1x margin-t-2x card-title tituloProductoTienda">
                                        <asp:HyperLink ID="link_producto" class=" hoverLinkTituloProducto" Target="_blank" runat="server">   <%# Eval("numero_parte") %> -  <%# Eval("titulo") %> </asp:HyperLink>
                                        <asp:Literal ID="lt_numero_parte" Text='<%# Eval("numero_parte") %>' Visible="false" runat="server"></asp:Literal>
                                    </h2>

                                    <p style="line-height: 20px;">
                                        <asp:Literal ID="lt_descripcion_corta" Visible="false" runat="server"></asp:Literal>
                                        Marca
                                        <asp:Label ID="lbl_marca" class="tooltipped" runat="server" Text=""></asp:Label>


                                        <br />
                                    <asp:Label ID="lbl_preciosFantasma" style="text-decoration:line-through;color: red; font-size: 0.9rem;" Visible="false"  runat="server"></asp:Label>

                                        <span class="producto_precio">$</span>
                                        <asp:Label ID="lbl_producto_precio" CssClass="producto_precio" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="lbl_producto_moneda" CssClass="producto_moneda" runat="server" Text=""></asp:Label>
                                      
                                    </p>
                                    <div style="font-size: 14px;"><%# Eval("unidad_venta") %> (<%# Eval("cantidad") %>  <%# Eval("unidad") %>)</div>
                                   
                                
                                   

                                    <div>
                                        <span style="inline-size: -webkit-fill-available; white-space: nowrap; overflow: hidden;"
                                            class=" white-text green darken-1 nota">Incluye <strong>IVA ✓</strong> </span>
                                    </div>

                                    <div>
                                        <uc_addCarrito:add ID="AddCarrito" numero_parte='<%# Eval("numero_parte") %>' runat="server"></uc_addCarrito:add>
                                        <uc_visualizarProducto:link ID="linkVisualizarProducto" Visible="false" runat="server"></uc_visualizarProducto:link>

                                        <asp:Label ID="lbl_disponibilidad_stock" runat="server"></asp:Label>

                                        <a id="btn_VerDisponibilidad" visible="false"
                                            
                                            runat="server" class="waves-effect waves-light btn btn-full-text blue modal-trigger" style="margin-top: 5px;"><i class="material-icons left">done</i>
                                            Ver Disponibilidad</a>
                                        <asp:Label runat="server" ID="lbl_puntajeBusqueda" Visible="false"></asp:Label>
                                    </div>




                                </div>

                            </div>
                        </div>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <div class="row center-align" style="height: 150px;">
                            <div class="col col s12">
                                <h3>Intenta con otro término de búsqueda</h3>
                            </div>
                        </div>
                       
                    </EmptyDataTemplate>
                </asp:ListView>

            </div>


        </div>

    </div>
</div>
<uc1:mdl_addOperacion ID="mdl_addOperacion" runat="server"></uc1:mdl_addOperacion>


 
<!-- Modal que carga el slide de fotografias de productos -->
<div id="modal_slideShow_productos" class="modal modal-fixed-footer" style="width: 700px">
    <div class="modal-content">
      <p class="center-align">
            <a id="link_productoSlideShow" class="btn blue">
                Ver detalles del producto   <i class="material-icons right">search</i>


            </a> 
</p>
        <div id="sliderProductosModal" class="slick "></div>


    </div>
    <div class="modal-footer">
        <a href="#!" class="modal-close waves-effect waves-green btn-flat">Cerrar</a>
    </div>
</div>





<!-- Modal Structure -->
<div id="modal_producto_disponibilidad" class="modal">
    <div class="modal-content">
        <h4><strong>Disponibilidad para el producto <span id="producto_disponibilidad_numero_parte" class="blue-text"></span></strong></h4>
        <div id="content_producto_disponibilidad" class="row">
            <div class="col s12 m12">
                <p>Ingresa una cantidad requerida y consulta la disponibilidad de esta</p>
            </div>
            <div class="col s12">

                <input id="txt_producto_cantidad_disponibilidad" value="1" style="text-align: center; width: 100px;" type="number" placeholder="Ingresa una cantidad" />

                <a class="btn green" onclick="consultarDisponibilidad(this);" style="vertical-align: top;" href="#">Ver disponibilidad</a>
                <a class="btn blue" onclick="producto_agregar_carrito_Service(this);" style="vertical-align: top;" href="#">
                    <i class="material-icons left">add_shopping_cart</i> Agregar a carrito
                </a>
            </div>

            <div id="productoDisponibilidad"></div>
        </div>


    </div>
    <div class="modal-footer">
        <a href="#!" class="modal-close waves-effect waves-green btn-flat">Cerrar</a>
    </div>
</div>





<!-- INICIO - Funcionalidades JS disponibilidad -->
<script>


    document.addEventListener("DOMContentLoaded", function (event) {

        /*
        var disponibilidadProducto = localStorage.getItem('disponibilidadProducto');
        console.log(disponibilidadProducto)
    if (disponibilidadProducto  != null ) {
        console.log("is not null LS disponibilidadProducto")
        localStorage.removeItem('disponibilidadProducto');
        console.log(disponibilidadProducto);
        openModalProductoDisponibilidad(disponibilidadProducto);




    } else {
        console.log("is null LS disponibilidadProducto")

      
        }

        */

    });



    function openModalProductoDisponibilidad(numero_parte) {

        var login =  <%= HttpContext.Current.User.Identity.IsAuthenticated.ToString().ToLower() %>;


        console.log(login);

        if (login) {
            document.querySelector("#productoDisponibilidad").innerHTML = "";
            document.querySelector("#producto_disponibilidad_numero_parte").textContent = numero_parte;

            setTimeout(function () {
                $('#modal_producto_disponibilidad').modal('open');
            }, 1000);
        } else {

            localStorage.setItem('disponibilidadProducto', numero_parte);
            var content = document.querySelector("#content_producto_disponibilidad");

            content.classList.add("hide");
            LoginAjaxOpenModal();
        }





    }
    function consultarDisponibilidad(btn) {

        var numero_parte = document.querySelector("#producto_disponibilidad_numero_parte").textContent;
        var txt_cantidad = document.querySelector("#txt_producto_cantidad_disponibilidad");


        loadDisponibilidad(btn, txt_cantidad, numero_parte);

        var instance = M.Modal.getInstance(document.getElementById('modal_producto_disponibilidad'))
        instance.open();

    }

</script>

<!-- FIN - Funcionalidades JS disponibilidad -->









<style>
    #modal_slideShow_productos {
        /*    width: 50vh !important;*/
    }

    .ddlDefault {
        height: 2rem;
        padding: 3px;
        line-height: 1;
        font-size: 93%;
    }

    .slick-slide {
        text-align: center;
    }

        .slick-slide > a {
            display: inline-grid;
        }

    .slick img {
            width: 100%;
        color: #eee;
        font-family: monospace;
    }
</style>




<script>
 


    $(document).ready(function () {
        $('.slick').slick({
            speed: 300,
            slidesToShow: 1,
            centerMode: true,
            adaptiveHeight: true
        });
    });



    $(function () {
        $(document).ready(function () {
            $('.sliderProductos').bxSlider({
                auto: true,
                autoStart: true,
                speed: 1000,
                minSlides: 1,
                maxSlides: 3,
                pager: false,
                slideMargin: 0,
                touchEnabled: false,
                responsive: true,

            });
        });
    }); 
</script>


<style>
    .slider .slides {
        background-color: transparent !important;
        margin: 0 !important;
        height: 400px !important;
    }

        .slider .slides li img {
            height: 100% !important;
            width: 100% !important;
            background-position: center !important;
            background-size: 100% auto !important;
            background-repeat: no-repeat !important;
        }



    .bx-wrapper {
        margin-bottom: 0px !important;
    }

        .bx-wrapper .bx-prev {
            left: 0px;
            opacity: 0.5;
        }

        .bx-wrapper .bx-next {
            right: 0px;
            opacity: 0.5;
        }
</style>
