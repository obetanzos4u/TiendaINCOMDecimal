<%@ Page Language="C#" AutoEventWireup="true" Async="true" MasterPageFile="~/bootstrap/basic.master" CodeFile="pedido-envio.aspx.cs" Inherits="usuario_cliente_basic" %>

<%@ Register Src="~/usuario/cliente/cliente-header.ascx" TagPrefix="header" TagName="menuGeneral" %>
<%@ Register TagPrefix="uc" TagName="ddlPaises" Src="~/userControls/ddl_paises.ascx" %>
<%@ Register TagPrefix="uc" TagName="ddlEstados" Src="~/userControls/ddl_estados.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="body">
    <header:menuGeneral ID="menuGeneral" runat="server"/>
    <asp:HiddenField ID="hf_id_pedido" runat="server"/>
    <asp:HiddenField ID="hf_pedido_tipo_envio" runat="server"/>
    <asp:HiddenField ID="hf_id_pedido_direccion_envio" runat="server"/>
    <div class="container-md is-top-3">
        <div class="is-flex is-flex-col is-justify-center is-items-start">
            <div class="is-flex is-justify-start is-items-center">
                <h4>Método de envío:<asp:Label ID="lt_numero_pedido" class="is-px-2 is-select-all" runat="server"></asp:Label></h4>
                <button type="button" class="is-cursor-pointer" style="background-color: transparent; outline: none; border: none;" onclick="copiarNumeroParte('body_lt_numero_pedido', 'Número de pedido')">
                    <span class="is-text-gray">
                        <svg class="is-w-4 is-h-4" aria-labelledby="Clipcopy" title="Copiar elemento" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 5H6a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2v-1M8 5a2 2 0 002 2h2a2 2 0 002-2M8 5a2 2 0 012-2h2a2 2 0 012 2m0 0h2a2 2 0 012 2v3m2 4H10m0 0l3-3m-3 3l3 3"></path>
                            <title id="Clipcopy">Copiar elemento</title>
                        </svg>
                    </span>
                </button>
            </div>
            <p>Establece el método de envío.</p>
            <%--<asp:Label ID="msg_alert" Visible="false" class="alert alert-warning" role="alert" runat="server">       
            </asp:Label>
            <asp:Label ID="msg_succes" Visible="false" class="alert alert-success" role="alert" runat="server">       
            </asp:Label>--%>
        </div>
        <asp:UpdatePanel ID="up_envio" runat="server">
            <ContentTemplate>
                <div class="is-flex is-justify-between is-items-start">
                    <asp:Panel ID="ContentReferenciaDomicilio" runat="server">
                        <div class="is-container">
                            <p class="text-center">Elige un método de envío:</p>
                            <div class="is-flex is-justify-around is-items-center">
                                <div id='contentCard_DireccEnvio' class="card is-rounded-xl is-bg-gray-light" runat="server">
                                    <div id="card_envio_recoge_en_tienda" class="card-body is-rounded-lg is-border-gray-soft">
                                        <div style="height: 30px;"></div>
                                        <h6 class="card-title is-text-center">Recoger en tienda</h6>
                                        <div class="d-grid gap-2 mt-4">
                                            <%-- OnClientClick="BootstrapClickLoading(this);" --%>
                                            <asp:LinkButton ID="btn_recogeEnTienda" OnClick="btn_recogeEnTienda_Click"
                                                class="btn-3" runat="server">Seleccionar</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-domicilio card is-rounded-xl is-space-x-6" runat="server">
                                    <div class="card-body is-rounded-lg is-bg-gray-light is-border-gray-soft">
                                        <div style="height: 30px"></div>
                                        <h6 class="card-title is-text-center">Entrega a domicilio</h6>
                                        <div>
                                            <asp:LinkButton ID="btn_entregaDomicilio" OnClick="btn_entregaDomicilio_Click" class="btn-3 d-grid gap-2 mt-4 hover-direccion is-text-white is-decoration-none" runat="server">Seleccionar</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="ContentReferenciaDomiciliosGuardados" Visible="false" runat="server">
                        <p class="is-text-center is-m-auto">Direcciones guardadas:</p>
                        <div>
                            <asp:ListView ID="lv_direcciones" OnItemDataBound="lv_direcciones_ItemDataBound" runat="server">
                                <LayoutTemplate>
                                    <div id="itemPlaceholder" runat="server"></div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hf_id_direccion" Value='<%#Eval("id") %>' runat="server"/>
                                    <div class="col-8 mb-4 is-m-auto is-top-1">
                                        <div id='contentCard_DireccEnvio' class="card is-bg-gray-light is-border-none is-rounded-lg is-border-gray-soft is-rounded-lg" runat="server">
                                            <div class="card-body">
                                                <p class="is-text-lg is-font-semibold"><%# Eval("nombre_direccion") %></p>
                                                <p class="is-select-all">
                                                    <%# Eval("calle") %> <%# Eval("numero") %>, <%# Eval("colonia") %>,  <%# Eval("delegacion_municipio") %>, <%# Eval("estado") %>
                                                    <%# Eval("codigo_postal") %>. <%# Eval("referencias") %>
                                                </p>
                                                <div class="is-flex is-justify-around is-items-center">
                                                    <asp:LinkButton class="btn is-bg-gray-400 is-text-white" OnClientClick="return confirm('¿Eliminar?');" OnClick="btn_eliminarDireccion_Click" ID="btn_eliminarDireccion" runat="server">
                                                        <i class="fas fa-trash-alt"></i>
                                                    </asp:LinkButton>
                                                    <a class="btn is-bg-gray-400 is-text-white" href='/usuario/cliente/editar/envio/<%#Eval("id") %>?ref=<%= seguridad.Encriptar(hf_id_pedido.Value)%>&numero_operacion=<%= lt_numero_pedido.Text%>'>Editar</a>
                                                    <asp:LinkButton ID="btn_usarDirección" OnClientClick="BootstrapClickLoading(this);" OnClick="btn_usarDirección_Click" class="btn-3" runat="server">Seleccionar</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <p>No hay direcciones guardadas</p>
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </div>
                        <div class="is-top-2 is-text-center">
                            <asp:LinkButton ID="btn_entregaDomicilioNuevo" OnClick="btn_entregaDomicilioNuevo_Click" class="btn-3" runat="server">Agregar dirección</asp:LinkButton>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="ContentReferenciaDomicilioNuevo" Visible="false" runat="server">
                        <div class="col">
                            <div class="background-form is-bg-gray-light is-items-center is-border-gray-soft" style="width: 96%; padding: 30px; border-radius: 8px;">
                                <div class="row">
                                    <div class="col">
                                        <p id="title-form_direccion"><strong>Agregar nueva dirección: </strong></p>
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="form-group is-top-1">
                                        <label for="<%= txt_nombre_direccion.ClientID %>">Asigna un nombre a esta dirección:</label>
                                        <asp:TextBox ID="txt_nombre_direccion" ClientIDMode="Static" class="form-control" Style="padding: .175rem .75rem; border-radius: 6px;" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                                        <small id="emailHelp" class="form-text text-muted">Ejemplo: Casa, trabajo, bodega</small>
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="form-group is-top-1">
                                        <label for="txt_codigo_postal">Código Postal:</label>
                                        <asp:TextBox ID="txt_codigo_postal" AutoPostBack="true" OnTextChanged="txt_codigo_postal_TextChanged" ClientIDMode="Static" class="form-control" Style="padding: .175rem .75rem; border-radius: 6px;" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group is-top-1">
                                        <label for="<%= txt_calle.ClientID %>">Calle:</label>
                                        <asp:TextBox ID="txt_calle" ClientIDMode="Static" class="form-control" Style="padding: .175rem .75rem; border-radius: 6px;" data-length="50" MaxLength="50" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group is-top-1">
                                        <label for="<%= txt_numero.ClientID %>">Número:</label>
                                        <asp:TextBox ID="txt_numero" ClientIDMode="Static" class="form-control" Style="padding: .175rem .75rem; border-radius: 6px;" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="form-group is-top-1">
                                        <label for="<%= txt_colonia.ClientID %>">Colonia:</label>
                                        <asp:DropDownList ID="ddl_colonia" Visible="false" class="form-select" runat="server"></asp:DropDownList>
                                        <asp:TextBox ID="txt_colonia" ClientIDMode="Static" class="form-control" Style="padding: .175rem .75rem; border-radius: 6px;" data-length="35" MaxLength="35" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group is-top-1">
                                        <label for="txt_delegacion_municipio">Delegación o municipio:</label>
                                        <asp:TextBox ID="txt_delegacion_municipio" ClientIDMode="Static" class="form-control" Style="padding: .175rem .75rem; border-radius: 6px;" data-length="35" MaxLength="35" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group is-top-1">
                                        <label for="txt_ciudad">Ciudad:</label>
                                        <asp:TextBox ID="txt_ciudad" ClientIDMode="Static" CssClass="validate" Style="display: initial; width: 100%; border-radius: 6px; border: 1px solid #37373733;" data-length="60" MaxLength="60" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div id="cont_txt_estado" class="form-group col-md-6 is-top-1" runat="server">
                                        <label for="txt_estado">Estado:</label>
                                        <asp:TextBox ID="txt_estado" class="form-control" Style="padding: .175rem .75rem; border-radius: 6px;" ClientIDMode="Static" data-length="35" MaxLength="35" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group is-top-1">
                                    <label for="ddl_pais" style="width:15%;">País:</label>
                                    <uc:ddlPaises ID="ddl_pais" class="form-control" style="padding: .175rem .75rem; border-radius: 6px;" runat="server" />
                                </div>
                                <!-- <div id="cont_ddl_estado" class="form-group col-md-4" runat="server">
                    <label for="ddl_municipio_estado">Estado:</label>
                    <uc:ddlEstados ID="ddl_estado" runat="server" />
                </div> -->
                                <div class="form-group is-top-1">
                                    <label for="txt_referencias">Referencias:</label>
                                    <asp:TextBox ID="txt_referencias" ClientIDMode="Static" CssClass="form-control" Style="padding: .175rem .75rem; border-radius: 6px;" runat="server"></asp:TextBox>
                                </div>
                                <div id="content_alert"></div>
                                <div style="margin: auto; width: fit-content;">
                                    <asp:Button ID="btn_crear_direccion" class="btn-3 mt-4 is-justify-center" OnClick="btn_crear_direccion_Click" Text="Guardar" runat="server" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_entregaDomicilio" EventName="Click"/>
                <%--<asp:AsyncPostBackTrigger ControlID="btn_entregaDomicilioNuevo" EventName="Click" />--%>
            </Triggers>
        </asp:UpdatePanel>
        <div style="float: left;">
            <hr class="is-top-4">
            <p>Dirección de tienda INCOM.</p>
            <div class="is-flex is-py-4">
                <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3763.340310184435!2d-99.11385668466113!3d19.397696846821297!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x85d1fe8542cb5455%3A0xe8681194a59f3b5a!2sIncom!5e0!3m2!1ses-419!2smx!4v1666278428806!5m2!1ses-419!2smx" width="500" height="320" style="border:0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>
            </div>
            <p>Av. Presidente Plutarco Elías Calles 276, Col. Tlazintla, Iztacalco, C.P. 08710, Ciudad de México.</p>
            <div class="is-flex is-justify-start is-py-4">
                <a href="https://g.page/Incom_CDMX?share" class="is-decoration-none" target="_blank">Mapa</a>
                <a href="/documents/pdf/croquis-plutarco.pdf" class="is-decoration-none is-space-x-6" target="_blank">Croquis</a>
            </div>
        </div>
    </div>

    <script>
        const eliminarConfirmacion = () => {
            event.preventDefault();
            Notiflix.Confirm.show(
                'Eliminar dirección',
                '¿Deseas eliminar la dirección?',
                'Confirmar',
                'Cancelar',
                () => {
                    let btn = $("#body_lv_direcciones_btn_eliminarDireccion_0");
                    console.log(btn);
                    btn.trigger("onclick");
                },
                () => {
                    console.log("Ñopi");
                    return false;
                }
            )
        }
    </script>

    <style>
        .btn-3 {
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
            background-color: #0066cc;
            text-align: center;
            font-weight: bold;
            /* letter-spacing: .5px; */
            -webkit-transition: background-color .2s ease-out;
            transition: background-color .2s ease-out;
            cursor: pointer;
            font-size: 12px;
            outline: 0;
            box-shadow: 0 2px 2px 0 rgba(0,0,0,0.14),0 3px 1px -2px rgba(0,0,0,0.12),0 1px 5px 0 rgba(0,0,0,0.2);
        }

        #card_envio_recoge_en_tienda, .card-domicilio {
            height: 160px;
        }
            .btn-3:hover {
                background: #1c74f8;
                color: #FFFFFF;
            }

            .hover-direccion:hover {
            color: #FFFFFF !important;
            }

            .space-envios {
                --bs-gutter-x: -2.5rem !important;
            }
    </style>
</asp:Content>

