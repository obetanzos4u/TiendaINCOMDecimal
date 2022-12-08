<%@ Page Language="C#" AutoEventWireup="true" Async="true" MasterPageFile="~/bootstrap/basic.master" CodeFile="pedido-facturacion.aspx.cs"
    Inherits="usuario_cliente_pedido_facturacion" %>

<%@ Register Src="~/usuario/cliente/cliente-header.ascx" TagPrefix="header" TagName="menuGeneral" %>

<%@ Register TagPrefix="uc" TagName="ddlPaises" Src="~/userControls/ddl_paises.ascx" %>
<%@ Register TagPrefix="uc" TagName="ddlEstados" Src="~/userControls/ddl_estados.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="body">

    <header:menuGeneral ID="menuGeneral" runat="server" />

    <asp:HiddenField ID="hf_id_pedido" runat="server" />
    <asp:HiddenField ID="hf_id_pedido_direccion_facturacion" runat="server" />
    <div class="container-md is-top-3">
        <div class="row">
            <div class="col">
                <div class="is-flex is-justify-between is-items-center">
                    <div class="is-flex is-justify-start is-items-center">
                        <h1 class="h5">Facturación del pedido:
                            <asp:Label ID="lt_numero_pedido" runat="server"></asp:Label></h1>
                        <button type="button" class="is-cursor-pointer" title="Copiar número de pedido" style="background-color: transparent; outline: none; border: none;" onclick="copiarNumeroParte('body_lt_numero_pedido', 'Pedido')">
                            <span class="is-text-gray is-inline-block">
                                <svg class="is-w-4 is-h-4" aria-labelledby="Clipcopy" title="Copiar elemento" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 5H6a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2v-1M8 5a2 2 0 002 2h2a2 2 0 002-2M8 5a2 2 0 012-2h2a2 2 0 012 2m0 0h2a2 2 0 012 2v3m2 4H10m0 0l3-3m-3 3l3 3"></path>
                                    <title id="Clipcopy">Copiar elemento</title>
                                </svg>
                            </span>
                        </button>
                    </div>
                    <asp:HyperLink ID="btn_volver_resumen" runat="server">Volver al resumen</asp:HyperLink>
                </div>
                <p class="text-facturaion-sub">Establece el tipo de facturación, elige una entrada guardada o agrega una nueva.</p>
            </div>
            <asp:Label ID="msg_alert" Visible="false" class="alert alert-warning" role="alert" runat="server"> 
            </asp:Label>
            <asp:Label ID="msg_succes" Visible="false" class="alert alert-success" role="alert" runat="server">
            </asp:Label>
        </div>
        <div class="direcciones_guardadas">
            <p class="is-text-center">Direcciones guardadas:</p>
        </div>
        <div class="row">
            <div class="col form-direcciones is-w-1_2">
                <div class="row row-cols-1 row-cols-sm-1  row-cols-md-2  row-cols-lg-2  row-cols-xl-2">
                    <div class="col mb-4">
                        <div class="card" runat="server">
                            <div id="card_envio_recoge_en_tienda" class="card-body">
                                <h5 class="card-title">Sin factura</h5>
                                <p class="card-text">
                                    Si no requieres factura, selecciona esta opción.
                                </p>
                                <br />
                                <div class="gap-2 mt-4">
                                    <asp:LinkButton ID="btn_Sin_Factura" OnClick="btn_Sin_Factura_Click"
                                        class="is-decoration-none" runat="server">
                                        <div class="is-btn-blue is-m-auto">No requiero factura</div>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:ListView ID="lv_direcciones" OnItemDataBound="lv_direcciones_ItemDataBound" runat="server">
                        <LayoutTemplate>
                            <div id="itemPlaceholder" runat="server"></div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <asp:HiddenField ID="hf_id_direccion" Value='<%#Eval("id") %>' runat="server" />
                            <div class="col mb-4">
                                <div id='contentCard_DireccFact' class="card " runat="server">
                                    <div class="card-body">
                                        <h5 class="card-title"><%# Eval("nombre_direccion") %></h5>
                                        <p class="card-text">
                                            <%# Eval("rfc") %>
                                            <br />
                                            <%# Eval("razon_social") %><br />
                                            <%# Eval("calle") %> <%# Eval("numero") %>, <%# Eval("colonia") %>,  <%# Eval("delegacion_municipio") %>,  <%# Eval("estado") %>   <%# Eval("codigo_postal") %>
                                        </p>
                                        <p class="card-text">Régimen fiscal: <%# Eval("regimen_fiscal") %></p>
                                        <section class="is-m-auto is-text-center options-factura">
                                            <asp:LinkButton class="is-btn-gray-light" OnClientClick="return confirm('Confirma que deseas ELIMINAR?');"
                                                OnClick="btn_eliminarDireccion_Click" ID="btn_eliminarDireccion" runat="server">
                                                <i class="fas fa-trash-alt"></i>
                                            </asp:LinkButton>
                                            <a class="is-btn-gray-light"
                                                href='/usuario/cliente/editar/facturacion/<%#Eval("id") %>?ref=<%= seguridad.Encriptar(hf_id_pedido.Value)%>&numero_operacion=<%= lt_numero_pedido.Text%>'>Editar
                                            </a>
                                            <div class="gap-2 is-inline-block">
                                                <asp:LinkButton ID="btn_usarDirección" OnClick="btn_usarDirección_Click"
                                                    class="is-decoration-none" runat="server">
                                                <div class="is-btn-blue">Seleccionar</div>
                                                </asp:LinkButton>
                                            </div>
                                        </section>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <p class="h4">No hay direcciones guardadas</p>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </div>
            </div>
            <asp:UpdatePanel ID="up_datos_facturacion" RenderMode="Block" UpdateMode="Conditional" runat="server" class="is-w-2_5">
                <ContentTemplate>
                    <asp:Panel runat="server">
                        <div class="col form-datos_facturacion">
                            <div class="form-container-datos_facturacion">
                                <div class="row">
                                    <div class="col">
                                        <h1 class="h6"><strong>Agregar datos de facturación:</strong></h1>
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="form-group is-top-1">
                                        <label for="<%= txt_nombre_direccion.ClientID %>">Asigna un nombre a esta dirección:</label>
                                        <asp:TextBox ID="txt_nombre_direccion" ClientIDMode="Static" class="form-control" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                                        <small id="emailHelp" class="form-text text-muted">Ejemplo: Casa, trabajo, bodega</small>
                                    </div>
                                    <div class="form-group is-top-1">
                                        <label for="<%= txt_razon_social.ClientID %>">Razón social:</label>
                                        <asp:TextBox ID="txt_razon_social" ClientIDMode="Static" class="form-control" data-length="150" MaxLength="150" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group is-top-1">
                                        <label for="<%= txt_rfc.ClientID %>">RFC:</label>
                                        <asp:TextBox ID="txt_rfc" ClientIDMode="Static" class="form-control" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group is-top-1">
                                        <label for="<%= ddl_regimen_fiscal %>">Régimen fiscal:</label>
                                        <asp:DropDownList ID="ddl_regimen_fiscal" AutoPostBack="false" class="is-w-full" runat="server">
                                            <asp:ListItem Selected="True" Value="601">601 - General de Ley Personas Morales</asp:ListItem>
                                            <asp:ListItem Value="603">603 - Personas Morales con Fines no Lucrativos</asp:ListItem>
                                            <asp:ListItem Value="605">605 - Sueldos y Salarios e Ingresos Asimilados a Salarios</asp:ListItem>
                                            <asp:ListItem Value="606">606 - Arrendamiento</asp:ListItem>
                                            <asp:ListItem Value="607">607 - Régimen de Enajenación o Adquisición de Bienes</asp:ListItem>
                                            <asp:ListItem Value="608">608 - Demás ingresos</asp:ListItem>
                                            <asp:ListItem Value="610">610 - Residentes en el Extranjero sin Establecimiento Permanente en México</asp:ListItem>
                                            <asp:ListItem Value="611">611 - Ingresos por Dividendos (socios y accionistas)</asp:ListItem>
                                            <asp:ListItem Value="612">612 - Personas Físicas con Actividades Empresariales y Profesionales</asp:ListItem>
                                            <asp:ListItem Value="614">614 - Ingresos por intereses</asp:ListItem>
                                            <asp:ListItem Value="615">615 - Régimen de los ingresos por obtención de premios</asp:ListItem>
                                            <asp:ListItem Value="616">616 - Sin obligaciones fiscales</asp:ListItem>
                                            <asp:ListItem Value="620">620 - Sociedades Cooperativas de Producción que optan por diferir sus ingresos</asp:ListItem>
                                            <asp:ListItem Value="621">621 - Incorporación Fiscal</asp:ListItem>
                                            <asp:ListItem Value="622">622 - Actividades Agrícolas, Ganaderas, Silvícolas y Pesqueras</asp:ListItem>
                                            <asp:ListItem Value="623">623 - Opcional para Grupos de Sociedades</asp:ListItem>
                                            <asp:ListItem Value="624">624 - Coordinados</asp:ListItem>
                                            <asp:ListItem Value="625">625 - Régimen de las Actividades Empresariales con ingresos a través de Plataformas Tecnológicas</asp:ListItem>
                                            <asp:ListItem Value="626">626 - Régimen Simplificado de Confianza</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="form-group is-top-1">
                                        <label for="txt_codigo_postal">Código postal:</label>
                                        <asp:TextBox ID="txt_codigo_postal" AutoPostBack="true" OnTextChanged="txt_codigo_postal_TextChanged" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group is-top-1">
                                        <label for="<%= txt_calle.ClientID %>">Calle:</label>
                                        <asp:TextBox ID="txt_calle" ClientIDMode="Static" class="form-control is-w-full" data-length="50" MaxLength="50" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group is-top-1">
                                        <label for="<%= txt_numero.ClientID %>">Número:</label>
                                        <asp:TextBox ID="txt_numero" ClientIDMode="Static" class="form-control is-w-full" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="form-group is-top-1">
                                        <label for="<%= txt_colonia.ClientID %>">Colonia:</label>
                                        <asp:DropDownList ID="ddl_colonia" Visible="false" class="form-select is-w-full" runat="server"></asp:DropDownList>

                                        <asp:TextBox ID="txt_colonia" ClientIDMode="Static" class="form-control is-w-full" data-length="35" MaxLength="35" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group is-top-1">
                                        <label for="txt_delegacion_municipio">Delegación/Municipio:</label>
                                        <asp:TextBox ID="txt_delegacion_municipio" ClientIDMode="Static" class="form-control is-w-full" data-length="35" MaxLength="35" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group is-top-1">
                                        <label for="txt_ciudad">Ciudad:</label>
                                        <asp:TextBox ID="txt_ciudad" ClientIDMode="Static" CssClass="validate form-control is-block" data-length="60" MaxLength="60" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="form-group is-top-1">
                                        <label for="ddl_pais">País:</label>
                                        <uc:ddlPaises ID="ddl_pais" class="form-control is-w-full" runat="server" />
                                    </div>
                                    <div id="cont_ddl_estado" class="form-group is-top-1" runat="server">
                                        <label for="ddl_municipio_estado">Estado:</label>
                                        <uc:ddlEstados ID="ddl_estado" class="is-w-full" runat="server" />
                                    </div>
                                    <!-- <div id="cont_txt_estado" class="form-group col-md-4" runat="server">
                    <label for="txt_estado">Estado:</label>
                    <asp:TextBox ID="txt_estado" class="form-control" ClientIDMode="Static" data-length="35" MaxLength="35" runat="server"></asp:TextBox>
                </div> -->
                                </div>
                                <div id="content_alert"></div>
                                <asp:Button ID="btn_crear_direccion" class="is-btn-blue is-block is-m-auto is-top-2" OnClick="btn_crear_direccion_Click" Text="Guardar" runat="server" />
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txt_codigo_postal" EventName="TextChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>

    <style>
        .direcciones_guardadas {
            width: 50%;
            font-weight: 600;
        }

        .form-container-datos_facturacion {
            background-color: #f5f5f5;
            /*width: 50%;*/
            padding: 30px;
            border-radius: 8px;
            margin: auto auto 4rem 2rem;
        }

        #txt_calle {
            width: 100%;
        }

        @media only screen and (max-width: 450px) {
            .container-md {
                margin-top: 2rem;
            }

                .container-md h1.h4 {
                    font-size: 14px;
                }

            .col > p.text-facturaion-sub {
                font-size: 10px;
            }

            .direcciones_guardadas {
                width: 100%;
            }

            .form-direcciones {
                flex: auto;
            }

            .card-title {
                font-size: 10px;
                font-weight: 600;
            }

            .form-datos_facturacion {
                display: inline;
                margin: auto;
            }

                .form-datos_facturacion > div {
                    width: 70%;
                    margin: auto;
                }

            .direcciones_guardadas > p.is-text-center {
                font-size: 12px;
            }

            #card_envio_recoge_en_tienda > .card-text {
                font-size: 9px;
            }

            #body_btn_Sin_Factura > .is-btn-blue {
                font-size: 8px;
                height: 22px;
                line-height: 22px;
            }

            .card-text {
                font-size: 9px;
            }

            .options-factura > .is-btn-gray-light {
                font-size: 8px;
                height: 22px;
                line-height: 22px;
                padding: 0 8px;
            }

            .options-factura > div > a > .is-btn-blue {
                height: 22px;
                line-height: 22px;
                padding: 0 8px;
                font-size: 8px;
            }

            .form-container-datos_facturacion {
                width: 100%;
                padding: 30px;
                border-radius: 8px;
                margin: 0;
            }

            .form-datos_facturacion {
                font-size: 9px;
                margin-bottom: 3rem;
            }

                .form-datos_facturacion h1.h6 {
                    font-size: 9px;
                }

                .form-datos_facturacion input.form-control {
                    font-size: 9px;
                    width: 100%;
                    line-height: 1.5;
                    padding: 0.125rem;
                }

            #body_ddl_pais_ddl_pais, #body_ddl_estado_ddl_municipio_estado {
                font-size: 9px;
                line-height: 1.5;
                padding: 0.225rem;
            }

            .form-group {
                margin-top: 0.25rem;
            }

            #body_btn_crear_direccion {
                height: 22px;
                line-height: 22px;
                padding: 0 8px;
                font-size: 8px;
                margin-top: 1rem;
            }
        }

        @media only screen and (min-width: 450px) and (max-width: 700px) {

            .container-md h1.h4 {
                font-size: 16px;
            }

            .col > p.text-facturaion-sub {
                font-size: 12px;
            }

            .direcciones_guardadas {
                width: 100%;
                font-size: 10px;
            }

            .form-direcciones {
                flex: auto;
            }

            .card-title {
                font-size: 12px;
                font-weight: 600;
            }

            .form-datos_facturacion {
                display: inline;
                margin: auto;
            }

                .form-datos_facturacion > div {
                    width: 70%;
                    margin: auto;
                }

            .direcciones_guardadas > p.is-text-center {
                font-size: 14px;
            }

            #card_envio_recoge_en_tienda > .card-text {
                font-size: 11px;
            }

            #body_btn_Sin_Factura > .is-btn-blue {
                font-size: 12px;
                height: 26px;
                line-height: 26px;
            }

            .card-text {
                font-size: 11px;
            }

            .options-factura > .is-btn-gray-light {
                font-size: 12px;
                height: 26px;
                line-height: 26px;
                padding: 0 10px;
            }

            .options-factura > div > a > .is-btn-blue {
                height: 26px;
                line-height: 26px;
                padding: 0 10px;
                font-size: 12px;
            }

            .form-container-datos_facturacion {
                width: 100%;
                padding: 30px;
                border-radius: 8px;
                margin: 0;
            }

            .form-datos_facturacion {
                font-size: 11px;
                margin-bottom: 3rem;
            }

                .form-datos_facturacion h1.h6 {
                    font-size: 11px;
                }

                .form-datos_facturacion input.form-control {
                    font-size: 11px;
                    width: 100%;
                    line-height: 1.5;
                    padding: 0.125rem;
                }

            #body_ddl_pais_ddl_pais, #body_ddl_estado_ddl_municipio_estado {
                font-size: 11px;
                line-height: 1.5;
                padding: 0.225rem;
            }

            .form-group {
                margin-top: 0.25rem;
            }

            #body_btn_crear_direccion {
                height: 26px;
                line-height: 26px;
                padding: 0 10px;
                font-size: 12px;
                margin-top: 1rem;
            }
        }

        @media only screen and (min-width: 450px) and (max-width: 700px) {

            .container-md h1.h4 {
                font-size: 16px;
            }

            .col > p.text-facturaion-sub {
                font-size: 12px;
            }

            .direcciones_guardadas {
                width: 100%;
                font-size: 10px;
            }

            .form-direcciones {
                flex: auto;
            }

            .card-title {
                font-size: 12px;
                font-weight: 600;
            }

            .form-datos_facturacion {
                display: inline;
                margin: auto;
            }

                .form-datos_facturacion > div {
                    width: 70%;
                    margin: auto;
                }

            .direcciones_guardadas > p.is-text-center {
                font-size: 14px;
            }

            #card_envio_recoge_en_tienda > .card-text {
                font-size: 11px;
            }

            #body_btn_Sin_Factura > .is-btn-blue {
                font-size: 12px;
                height: 26px;
                line-height: 26px;
            }

            .card-text {
                font-size: 11px;
            }

            .options-factura > .is-btn-gray-light {
                font-size: 12px;
                height: 26px;
                line-height: 26px;
                padding: 0 10px;
            }

            .options-factura > div > a > .is-btn-blue {
                height: 26px;
                line-height: 26px;
                padding: 0 10px;
                font-size: 12px;
            }

            .form-container-datos_facturacion {
                width: 100%;
                padding: 30px;
                border-radius: 8px;
                margin: 0;
            }

            .form-datos_facturacion {
                font-size: 11px;
                margin-bottom: 3rem;
            }

                .form-datos_facturacion h1.h6 {
                    font-size: 11px;
                }

                .form-datos_facturacion input.form-control {
                    font-size: 11px;
                    width: 100%;
                    line-height: 1.5;
                    padding: 0.125rem;
                }

            #body_ddl_pais_ddl_pais, #body_ddl_estado_ddl_municipio_estado {
                font-size: 11px;
                line-height: 1.5;
                padding: 0.225rem;
            }

            .form-group {
                margin-top: 0.25rem;
            }

            #body_btn_crear_direccion {
                height: 26px;
                line-height: 26px;
                padding: 0 10px;
                font-size: 12px;
                margin-top: 1rem;
            }
        }
    </style>
</asp:Content>


