<%@ Control Language="C#" AutoEventWireup="true"  CodeFile="uc_crearDireccionesEnvio.ascx.cs" Inherits="uc_crearDireccionesEnvio" %>
<%@ Register TagPrefix="uc" TagName="ddlPaises"  Src="~/userControls/ddl_paises.ascx" %>
<%@ Register TagPrefix="uc" TagName="ddlEstados"  Src="~/userControls/ddl_estados.ascx" %>



    <div class="border-form_direcciones_envio">
        <!-- <div class="row">
            <div class="col l12">
                <h2 class="center-align" style="font-size: 1.56rem !important;">Envíos</h2> 
            </div>
        </div> -->
        <div class="row">
            <div class="col s12 m12 l12">
                <h2>Crear una dirección de envío</h2>
            </div>     
        </div>
        <div class="row container-form_direccion_envio">
            <div class="col s12 m12 l12" style="margin-bottom: 1rem;">
                <div class="input-field" style="margin-bottom: 0px !important">
                    <asp:TextBox ID="txt_nombre_direccion" ClientIDMode="Static" CssClass="validate" data-length="20" MaxLength="20" style="height: 2rem; line-height: 2.5rem;" runat="server"></asp:TextBox>
                    <label for="txt_nombre_direccion" class="label-form_direccion_envio">Asigna un nombre a esta dirección:</label>
                   <i>(Ejemplo: Casa, Bodega, Almacén principal)</i>
                </div>
            </div>
            <div class="col s12 m12 l12">
                <div class="input-field">
                    <asp:TextBox ID="txt_calle" ClientIDMode="Static" CssClass="validate"  data-length="50" MaxLength="50" style="height: 2rem; line-height: 2.5rem;" runat="server"></asp:TextBox>
                    <label for="txt_calle" class="label-form_direccion_envio">Calle</label>
                </div>
                <div class="is-flex" style="margin-bottom: 1rem;">
                    <div style="width: 45%; margin-right: 10%">
                        <div class="input-field">
                            <asp:TextBox ID="txt_numero" ClientIDMode="Static" CssClass="validate" data-length="30" MaxLength="30" style="height: 2rem; line-height: 2.5rem" runat="server"></asp:TextBox>
                            <label for="txt_numero" class="label-form_direccion_envio">Número</label>
                        </div>
                    </div>
                    <div style="width: 45%;">
                        <div class="input-field">
                            <asp:TextBox ID="txt_codigo_postal" AutoPostBack="true" onchange="txtLoading(this);"
                                OnTextChanged="txt_codigo_postal_TextChanged" ClientIDMode="Static" CssClass="validate" style="height: 2rem; line-height: 2.5rem" runat="server"></asp:TextBox>
                            <label for="txt_codigo_postal" class="label-form_direccion_envio">Código Postal</label>
                        </div>
                    </div>
                </diV>
                <div class="input-field" style="margin-bottom: 0.5rem;">
                    <asp:DropDownList ID="ddl_colonia" Visible="false" runat="server"></asp:DropDownList>
                    <asp:TextBox ID="txt_colonia" ClientIDMode="Static"  CssClass="validate" data-length="70" MaxLength="70" style="height: 2rem; line-height: 2.5rem; margin-bottom: 1rem;" runat="server"></asp:TextBox>
                    <label for="txt_colonia" class="label-form_direccion_envio">Colonia</label>
                </div>
                <div class="input-field" style="margin-bottom: 1.5rem;">
                    <asp:TextBox ID="txt_delegacion_municipio" ClientIDMode="Static" CssClass="validate" data-length="50" MaxLength="50" style="height: 2rem; line-height: 2.5rem" runat="server"></asp:TextBox>
                    <label for="txt_delegacion_municipio" class="label-form_direccion_envio">Delegación o Municipio</label>
                </div>
                <div class="is-flex" style="margin-bottom: 1rem;">
                    <div class="input-field direcciones_envio-input_pais">
                        <label for="txt_pais" style="margin-top: -2rem; color: black; padding-left: 0px;" class="label-form_direccion_envio">País:</label>
                        <uc:ddlPaises ID="ddl_pais" runat="server" />
                    </div>
                   <div id="cont_ddl_estado" class="input_envio-estado input-field direcciones_envio-input_estado" visible="false" runat="server">
                        <label for="txt_pais" style="margin-top: -2rem; color: black">Estado:</label>
                        <uc:ddlEstados ID="ddl_estado" runat="server" />
                    </div> 
                </div>
                <div class="input-field">
                    <asp:TextBox ID="txt_ciudad" ClientIDMode="Static" CssClass="validate" data-length="60" MaxLength="60" style="height: 2rem; line-height: 2.5rem" runat="server"></asp:TextBox>
                    <label for="txt_ciudad" class="label-form_direccion_envio">Ciudad</label>
                </div>
                <div id="cont_txt_estado" class="input-field col s12 m12 l3" runat="server">
                    <asp:TextBox ID="txt_estado" ClientIDMode="Static" CssClass="validate" data-length="35" MaxLength="35" runat="server"></asp:TextBox>
                    <label for="txt_estado" class="label-form_direccion_envio">Estado test</label>
                </div>
            </div>
            <div class="col s12 m12 l12">
                <div class="input-field">
                    <asp:TextBox ID="txt_referencias" ClientIDMode="Static" CssClass="validate" style="height: 2rem; line-height: 2.5rem" runat="server"></asp:TextBox>
                    <label for="txt_referencias" class="label-form_direccion_envio">Referencias </label>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col s12 m12 l12">
                <div class="input-field col s12 m12 l12">
                    <a href="<%= ResolveUrl("~/usuario/mi-cuenta/direcciones-de-envio.aspx") %>" 
                        class="is-text-white is-btn-gray" style="text-transform: none; margin-right: 2rem">Cerrar</a>
                    <asp:LinkButton ID="btn_crear_direccion"
                  OnClick="btn_crear_direccion_Click"
                  style="text-transform: none;"
                        runat="server">
                        <div class="is-text-white is-btn-gray">
                            Crear dirección de envío
                        </div>
                    </asp:LinkButton>
                </div>
            </div>
        </div>
         <div class="row"></div>
    </div>
    <script>
        $(document).ready(function () {
            $('select').material_select();
        });
    </script>