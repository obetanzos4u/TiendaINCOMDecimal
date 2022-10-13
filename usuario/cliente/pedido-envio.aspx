<%@ Page Language="C#" AutoEventWireup="true" Async="true" MasterPageFile="~/bootstrap/basic.master" CodeFile="pedido-envio.aspx.cs" Inherits="usuario_cliente_basic" %>
<%@ Register Src="~/usuario/cliente/cliente-header.ascx" TagPrefix="header" TagName="menuGeneral" %>
<%@ Register TagPrefix="uc" TagName="ddlPaises" Src="~/userControls/ddl_paises.ascx" %>
<%@ Register TagPrefix="uc" TagName="ddlEstados" Src="~/userControls/ddl_estados.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="body">


    <header:menuGeneral ID="menuGeneral" runat="server" />

    <asp:HiddenField ID="hf_id_pedido" runat="server" />
    <asp:HiddenField ID="hf_pedido_tipo_envio" runat="server" />
    <asp:HiddenField ID="hf_id_pedido_direccion_envio" runat="server" />
    <div class="container-md ">
        <div class="row">
            <div class="col">
                <h1 class="h3 is-top-2">Método de envío para el pedido: <asp:Literal ID="lt_numero_pedido" runat="server"></asp:Literal>
                </h1>
                <p>Establece el tipo de envio.</p>
            </div>
            <asp:Label ID="msg_alert" Visible="false" class="alert alert-warning" role="alert" runat="server">       
            </asp:Label>
            <asp:Label ID="msg_succes" Visible="false" class="alert alert-success" role="alert" runat="server">       
            </asp:Label>
        </div>
        <div class="row">
            <div class="col">
                <div class="row row-cols-1 row-cols-sm-1  row-cols-md-2  row-cols-lg-2  row-cols-xl-2">
                    <p class="is-top-2" style="width: 100%"><strong>Elige un método de envío:</strong></p>
                    <div class="col mb-4">
                        <div id='contentCard_DireccEnvio' class="card " runat="server">
                            <div id="card_envio_recoge_en_tienda" class="card-body">
                                <h5 class="card-title">Recoger en tienda</h5>
                                <!-- <p class="card-text">
                                Recoge en nuestra sucursal CDMX.
                                </p>  <br /> -->
                                <div class="d-grid gap-2 mt-4">
                                    <asp:LinkButton ID="btn_recogeEnTienda" OnClick="btn_recogeEnTienda_Click" OnClientClick="BootstrapClickLoading(this);"
                                        class="btn btn-lg btn-primary" runat="server">Recoger en tienda</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:ListView ID="lv_direcciones" OnItemDataBound="lv_direcciones_ItemDataBound" runat="server">
                        <LayoutTemplate>
                            <div id="itemPlaceholder" runat="server">
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <asp:HiddenField ID="hf_id_direccion" Value='<%#Eval("id") %>' runat="server" />
                            <div class="col mb-4">
                                <div id='contentCard_DireccEnvio' class="card " runat="server">
                                    <img src="/img/webUI/envio-a-domicilio.gif" class="card-img-top" alt="...">
                                    <div class="card-body">
                                        <h5 class="card-title"><%# Eval("nombre_direccion") %></h5>
                                        <p class="card-text">
                                            <%# Eval("calle") %> <%# Eval("numero") %>, <%# Eval("colonia") %>,  <%# Eval("delegacion_municipio") %>,  <%# Eval("estado") %>
                                            <%# Eval("codigo_postal") %> <%# Eval("referencias") %>
                                        </p>
                                        <asp:LinkButton class="btn    btn-danger" OnClientClick="return confirm('Confirma que deseas ELIMINAR?');"
                                            OnClick="btn_eliminarDireccion_Click" ID="btn_eliminarDireccion" runat="server">
                                    <i class="fas fa-trash-alt"></i>
                                        </asp:LinkButton>
                                        <a class="btn btn-outline-secondary  text-dark bg-light"
                                            href='/usuario/cliente/editar/envio/<%#Eval("id") %>?ref=<%= seguridad.Encriptar(hf_id_pedido.Value)%>&numero_operacion=<%= lt_numero_pedido.Text%>'>Editar</a>
                                        <div class="d-grid gap-2 mt-2">
                                            <asp:LinkButton ID="btn_usarDirección" OnClientClick="BootstrapClickLoading(this);"
                                                OnClick="btn_usarDirección_Click"
                                                class="btn btn-lg btn-primary" runat="server">Usar dirección</asp:LinkButton>
                                        </div>
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
            <div class="col">
                <div class="background-form is-bg-gray-light is-items-center" style="width: 80%; padding: 30px; border-radius: 8px; margin-left: 2rem;">
                    <div class="row">
                        <div class="col">
                            <p id="title-form_direccion"><strong>Agregar una nueva dirección: </strong></p>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group">
                            <label for="<%= txt_nombre_direccion.ClientID %>">Asigna un nombre a esta dirección:</label>

                            <asp:TextBox ID="txt_nombre_direccion" ClientIDMode="Static" class="form-control" Style="padding: .175rem .75rem; border-radius: 6px; border-radius: 6px; border-radius: 6px; border-radius: 6px; border-radius: 6px;" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                            <small id="emailHelp" class="form-text text-muted">Ejemplo: Casa, trabajo, bodega</small>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group">
                            <label for="txt_codigo_postal">Código Postal:</label>
                            <asp:TextBox ID="txt_codigo_postal" AutoPostBack="true" OnTextChanged="txt_codigo_postal_TextChanged" ClientIDMode="Static" class="form-control" Style="padding: .175rem .75rem; border-radius: 6px; border-radius: 6px; border-radius: 6px; border-radius: 6px; border-radius: 6px;" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="<%= txt_calle.ClientID %>">Calle:</label>
                            <asp:TextBox ID="txt_calle" ClientIDMode="Static" class="form-control" Style="padding: .175rem .75rem; border-radius: 6px; border-radius: 6px; border-radius: 6px; border-radius: 6px; border-radius: 6px;" data-length="50" MaxLength="50" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="<%= txt_numero.ClientID %>">Número:</label>
                            <asp:TextBox ID="txt_numero" ClientIDMode="Static" class="form-control" Style="padding: .175rem .75rem; border-radius: 6px; border-radius: 6px; border-radius: 6px; border-radius: 6px; border-radius: 6px;" data-length="20" MaxLength="20" runat="server"></asp:TextBox>

                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group">
                            <label for="<%= txt_colonia.ClientID %>">Colonia:</label>
                            <asp:DropDownList ID="ddl_colonia" Visible="false" class="form-select" runat="server"></asp:DropDownList>

                            <asp:TextBox ID="txt_colonia" ClientIDMode="Static" class="form-control" Style="padding: .175rem .75rem; border-radius: 6px; border-radius: 6px; border-radius: 6px; border-radius: 6px; border-radius: 6px;" data-length="35" MaxLength="35" runat="server"></asp:TextBox>

                        </div>
                        <div class="form-group" style="margin-top: 1rem;">
                            <label for="txt_delegacion_municipio">Delegación o municipio:</label>
                            <asp:TextBox ID="txt_delegacion_municipio" ClientIDMode="Static" class="form-control" Style="padding: .175rem .75rem; border-radius: 6px; border-radius: 6px; border-radius: 6px; border-radius: 6px; border-radius: 6px;" data-length="35" MaxLength="35" runat="server"></asp:TextBox>

                        </div>
                        <div class="form-group">
                            <label for="txt_ciudad">Ciudad:</label>
                            <asp:TextBox ID="txt_ciudad" ClientIDMode="Static" CssClass="validate" Style="display: inherit; width: 100%; border-radius: 6px; border: 1px solid #37373733;" data-length="60" MaxLength="60" runat="server"></asp:TextBox>

                        </div>
                    </div>
                    <div class="form-row">
                        <div id="cont_txt_estado" class="form-group col-md-6" runat="server">
                            <label for="txt_estado">Estado:</label>
                            <asp:TextBox ID="txt_estado" class="form-control" Style="padding: .175rem .75rem; border-radius: 6px; border-radius: 6px; border-radius: 6px; border-radius: 6px; border-radius: 6px;" ClientIDMode="Static" data-length="35" MaxLength="35" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="ddl_pais">Pais:</label>
                        <uc:ddlPaises ID="ddl_pais" class="form-control" style="padding: .175rem .75rem; border-radius: 6px; border-radius: 6px; border-radius: 6px; border-radius: 6px; border-radius: 6px;" runat="server" />
                    </div>
                    <!-- <div id="cont_ddl_estado" class="form-group col-md-4" runat="server">
                    <label for="ddl_municipio_estado">Estado:</label>
                    <uc:ddlEstados ID="ddl_estado" runat="server" />
                </div> -->
                    <div class="form-group">
                        <label for="txt_referencias">Referencias:</label>
                        <asp:TextBox ID="txt_referencias" ClientIDMode="Static" CssClass="form-control" Style="padding: .175rem .75rem; border-radius: 6px; border-radius: 6px; border-radius: 6px; border-radius: 6px; border-radius: 6px;" runat="server"></asp:TextBox>
                    </div>
                    <div id="content_alert"></div>
                    <div style="margin: auto; width: fit-content;">
                        <asp:Button ID="btn_crear_direccion" class="btn-3 mt-4 is-justify-center" OnClick="btn_crear_direccion_Click" Text="Guardar" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    
    <script>
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

        .btn-3:hover {
           background: #1c74f8; 
        }
       

    </style>
</asp:Content>


