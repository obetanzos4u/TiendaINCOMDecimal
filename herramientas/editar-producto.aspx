<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/herramientas/_masterConfiguraciones.master" Async="true" 
    CodeFile="editar-producto.aspx.cs"
  Inherits="herramientas_editar_producto" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <h1 class="center-align">Configuración de productos</h1>
    <h3 class="center-align">Powered by Marketing development</h3>
    <div class="container">
        <div class="row">
             <asp:UpdatePanel UpdateMode="Conditional" RenderMode="Block" class=" col s12" runat="server"><ContentTemplate>
                <i class="material-icons prefix">search</i>
                <asp:TextBox ID="txt_search_product"  OnTextChanged="txt_search_product_TextChanged" AutoPostBack="true"
                      placeholder="Busca por número de parte o descripción" runat="server"></asp:TextBox>
                    <label>Busca por número de parte o descripción</label>
             </ContentTemplate>

                    </asp:UpdatePanel>
        </div>
        <div class="row">
            <div class="col s12">
                <ul class="tabs">
                    <li class="tab col s3 active"><a href="#up_productos_Datos">Datos de productos</a></li>
                    <li class="tab col s3"><a class="" href="#test2">Roles de producto</a></li>
                    <li class="tab col s3 "><a href="#up_productos_precios_rangos">Precios rangos</a></li>
                    <li class="tab col s3 disabled"><a href="#test4">Precios Lista Fija</a></li>
                </ul>
            </div>
            <asp:UpdatePanel  id="up_productos_Datos" UpdateMode="Conditional" ClientIDMode="Static" class="col s12" runat="server" RenderMode="Block">
                <Triggers>
                  
                </Triggers>
                <ContentTemplate>
                     
                        <h2>Datos de productos</h2> 
                        <div class="row">
                            <div class=" col s12 m6 l6">
                                <asp:TextBox ID="txt_numero_parte" Enabled="false" runat="server"></asp:TextBox>
                                <label>Número de parte</label>
                            </div>
                              <div class=" col s12 m6 l6">
                                  <asp:LinkButton ID="btn_eliminarProducto" OnClick="btn_eliminarProducto_Click" CssClass="btn red"  runat="server">Eliminar producto</asp:LinkButton>
                              </div>
                            <div class=" col s12 m12 l12">
                                <asp:TextBox ID="txt_titulo" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <label>Titulo</label>
                    </div>
                    <div class=" col s12 m12 l12">
                        <asp:TextBox ID="txt_descripcion_corta" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <label>Descripción corta</label>
                    </div>
                    <div class=" col s12 m6 l6">
                        <asp:TextBox ID="txt_titulo_corto_ingles" placeholder="Titulo corto inglés" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <label>Titulo corto inglés</label>
                    </div>
                    <div class=" col s12 m12 l12">
                        <asp:TextBox ID="txt_especificaciones" placeholder="Especificaciones"   ValidateRequestMode="Disabled"  CausesValidation="false" 
                            CssClass="materialize-textarea" TextMode="MultiLine" runat="server" 
                            OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <label>Especificaciones</label>
                    </div>
                    <div class=" col s12 m6 l6">
                        <asp:TextBox ID="txt_marca" placeholder="Marca" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <label>Marca</label>
                    </div>
                    <div class=" col s12 m6 l6">
                        <asp:TextBox ID="txt_categoria_identificador" placeholder="Categoria Identificador" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <label for="<%= txt_categoria_identificador.ClientID %>">Categoria Identificador</label>
                    </div>
                    <div class=" col s12 m12 l12">
                        <asp:TextBox ID="txt_imagenes" placeholder="Imágenes" CssClass="materialize-textarea" TextMode="MultiLine" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <label>Imágenes</label>
                    </div>
                    <div class=" col s12 m12 l12">
                        <asp:TextBox ID="txt_metatags" placeholder="Metatags" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <label>Metatags</label>
                    </div>
                    <div class=" col s12 m12 l12">
                        <asp:TextBox ID="txt_pdf" placeholder="PDF" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <label>PDF</label>
                    </div>
                    <div class=" col s12 m4 l3 xl2">
                        <asp:TextBox ID="txt_peso" placeholder="Peso" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <label>Peso</label>
                    </div>
                    <div class=" col s12 m4 l3  xl2">
                        <asp:TextBox ID="txt_alto" placeholder="Alto" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <label>Alto</label>
                    </div>
                    <div class=" col s12 m4 l3  xl2">
                        <asp:TextBox ID="txt_ancho" placeholder="Ancho" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <label>Ancho</label>
                    </div>
                    <div class=" col s12 m4 l3  xl2">
                        <asp:TextBox ID="txt_profundidad" placeholder="Profundidad" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <label>Profundidad</label>
                    </div>
                    <div class=" col s12 m12 l12">
                        <asp:TextBox ID="txt_video" placeholder="Video" CssClass="materialize-textarea" TextMode="MultiLine" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <label>Video</label>
                    </div>
                    <div class=" col s12 m4 l4">
                        <asp:TextBox ID="txt_unidad_venta" placeholder="Unidad de venta" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <label>Unidad de venta</label>
                    </div>
                    <div class=" col s12 m4 l4">
                        <asp:TextBox ID="txt_cantidad" placeholder="Cantidad" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <label>Cantidad</label>
                    </div>
                    <div class=" col s12 m4 l4">
                        <asp:TextBox ID="txt_unidad" placeholder="Unidad" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <label>Unidad</label>
                    </div>
                    <div class=" col s12 m6 l6">
                        <asp:TextBox ID="txt_producto_alternativo" placeholder="Producto Alternativo" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <label>Producto Alternativo</label>
                    </div>
                    <div class=" col s12 m6 l6">
                        <asp:TextBox ID="txt_productos_relacionados" placeholder="Productos Relacionados" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <label>Productos Relacionados</label>
                    </div>
                    <div class=" col s12 m12 l12">
                        <asp:TextBox ID="txt_atributos" placeholder="Atributos" ValidateRequestMode="Disabled" CssClass="materialize-textarea" TextMode="MultiLine" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <label>Atributos</label>
                    </div>

                    <div class=" col s12 m6 l4">
                        <asp:TextBox ID="txt_noParte_proveedor" placeholder="No. parte proveedor" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <label>No. Parte proveedor </label>
                    </div>

                    <div class=" col s12 m6 l4">
                        <asp:TextBox ID="txt_noParte_interno" placeholder="No. parte interno" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <label>No. Parte interno </label>
                    </div>
                        <div class=" col s12 m6 l4">
                        <asp:TextBox ID="txt_noParte_Competidor" placeholder="No. parte competidor" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <label>No. parte competidor</label>
                    </div>
                    <div class=" col s12 m6 l4">
                        <asp:TextBox ID="txt_upc" placeholder="No. UPC" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <label>No. UPC</label>
                    </div>


                     <div class=" col s12 m3 l2">
                        <asp:TextBox ID="txt_orden" placeholder="Orden de producto" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <label>Orden de producto</label>
                    </div>
                      <div class=" col s12 m3 l2">
                        <asp:TextBox ID="txt_disponibleVenta" placeholder="Disponible Venta" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <label>Disponible venta</label>
                    </div>
                     <div class=" col s12 m12 l12">
                        <asp:TextBox ID="txt_etiquetas" placeholder="Etiquetas" runat="server" OnTextChanged="actualizar_campo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <label>Etiquetas</label>
                    </div>
                     <div class=" col s12 m12 l12">
                        <asp:TextBox ID="txt_Avisos" CssClass="materialize-textarea" placeholder="Avisos" TextMode="MultiLine" runat="server" OnTextChanged="actualizar_campo_TextChanged"
                            AutoPostBack="true"></asp:TextBox>
                        <label>Avisos</label>
                    </div>
                </div>
            </ContentTemplate>
            </asp:UpdatePanel>
 


                <asp:UpdatePanel ID="up_productos_precios_rangos" UpdateMode="Conditional" ClientIDMode="Static" class="col s12" runat="server" RenderMode="Block">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>

                        <h2>Rangos de precios de producto</h2>
                        <div class="row">
                            <div class=" col s12 m6 l6">
                                <asp:TextBox ID="txt_numero_parte2" Text='<%= txt_numero_parte.Text %>' Enabled="false" runat="server"></asp:TextBox>
                                <label>Número de parte</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col s12 m6 l6">
                                <asp:TextBox ID="txt_moneda_rangos" runat="server" OnTextChanged="actualizar_precio_rangos_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <label>txt_moneda_rangos</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class=" col s12 m4 l4">
                                <asp:TextBox ID="txt_precio1" runat="server" OnTextChanged="actualizar_precio_rangos_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <label>Precio 1</label>
                            </div>
                             <div class=" col s12 m4 l4">
                                <asp:TextBox ID="txt_max1" runat="server" OnTextChanged="actualizar_precio_rangos_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <label>Máximo 1</label>
                            </div>
                        </div>
                         <div class="row">
                            <div class=" col s12 m4 l4">
                                <asp:TextBox ID="txt_precio2" runat="server" OnTextChanged="actualizar_precio_rangos_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <label>Precio 2</label>
                            </div>
                             <div class=" col s12 m4 l4">
                                <asp:TextBox ID="txt_max2" runat="server" OnTextChanged="actualizar_precio_rangos_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <label>Máximo 2</label>
                            </div>
                        </div>
                         <div class="row">
                            <div class=" col s12 m4 l4">
                                <asp:TextBox ID="txt_precio3" runat="server" OnTextChanged="actualizar_precio_rangos_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <label>Precio 3</label>
                            </div>
                             <div class=" col s12 m4 l4">
                                <asp:TextBox ID="txt_max3" runat="server" OnTextChanged="actualizar_precio_rangos_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <label>Máximo 3</label>
                            </div>
                        </div>
                         <div class="row">
                            <div class=" col s12 m4 l4">
                                <asp:TextBox ID="txt_precio4" runat="server" OnTextChanged="actualizar_precio_rangos_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <label>Precio 4</label>
                            </div>
                             <div class=" col s12 m4 l4">
                                <asp:TextBox ID="txt_max4" runat="server" OnTextChanged="actualizar_precio_rangos_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <label>Máximo 4</label>
                            </div>
                        </div>
                         <div class="row">
                            <div class=" col s12 m4 l4">
                                <asp:TextBox ID="txt_precio5" runat="server" OnTextChanged="actualizar_precio_rangos_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <label>Precio 5</label>
                            </div>
                             <div class=" col s12 m4 l4">
                                <asp:TextBox ID="txt_max5" runat="server" OnTextChanged="actualizar_precio_rangos_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <label>Máximo 5</label>
                            </div>
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            
            <div id="test3" class="col s12">Test 3</div>
            <div id="test4" class="col s12">Test 4</div>
    </div>
</asp:Content>



