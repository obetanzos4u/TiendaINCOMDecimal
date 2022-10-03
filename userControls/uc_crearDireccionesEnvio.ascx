<%@ Control Language="C#" AutoEventWireup="true"  CodeFile="uc_crearDireccionesEnvio.ascx.cs" Inherits="uc_crearDireccionesEnvio" %>
<%@ Register TagPrefix="uc" TagName="ddlPaises"  Src="~/userControls/ddl_paises.ascx" %>
<%@ Register TagPrefix="uc" TagName="ddlEstados"  Src="~/userControls/ddl_estados.ascx" %>



    <div class="container z-depth-3">
        <div class="row">
            <div class="col l12">
                <h2 class="center-align" style="font-size: 1.56rem !important;">Envíos</h2> 
            </div>
        </div>
        <div class="row">
            <div class="col s12 m12 l12">
                <h2>Crear una dirección de envío</h2>
            </div>
          
            
                  
        </div>
        <div class="row">
             <div class="col s12 m12 l12">
                <div class="input-field col s12 m12 l6">
                      <asp:TextBox ID="txt_nombre_direccion" ClientIDMode="Static" CssClass="validate"  data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                    <label for="txt_nombre_direccion">Asigna un nombre a esta dirección </label>
                   <i>(Ejemplo: Casa, Bodega, Almacén principal)</i>
                </div></div>
            <div class="col s12 m12 l12">
                <div class="input-field col s12 m12 l4">
                    <asp:TextBox ID="txt_calle" ClientIDMode="Static" CssClass="validate"  data-length="50" MaxLength="50" runat="server"></asp:TextBox>
                    <label for="txt_calle">Calle</label>
                </div>
                <div class="input-field col s12 m12 l3">
                    <asp:TextBox ID="txt_numero" ClientIDMode="Static" CssClass="validate" data-length="30" MaxLength="30"  runat="server"></asp:TextBox>
                    <label for="txt_numero">Número</label>
                </div>
       
                <div class="input-field col s12 m12 l2 xl1">
                    <asp:TextBox ID="txt_codigo_postal" AutoPostBack="true" onchange="txtLoading(this);"
                        OnTextChanged="txt_codigo_postal_TextChanged" ClientIDMode="Static" CssClass="validate" runat="server"></asp:TextBox>
                    <label for="txt_codigo_postal">Código Postal</label>

                </div>
                <div class="input-field col s12 m12 l5">
                    <asp:DropDownList ID="ddl_colonia" Visible="false" runat="server"></asp:DropDownList>
                    <asp:TextBox ID="txt_colonia" ClientIDMode="Static"  CssClass="validate" data-length="70" MaxLength="70"  runat="server"></asp:TextBox>
                    <label for="txt_colonia">Colonia</label>
                </div>
            </div>
            <div class="col s12 m12 l12">
                <div class="input-field col s12 m12 l5">
                    <asp:TextBox ID="txt_delegacion_municipio" ClientIDMode="Static" CssClass="validate" data-length="50" MaxLength="50" runat="server"></asp:TextBox>
                    <label for="txt_delegacion_municipio">Delegación/Municipio</label>
                </div>

                                <div class="input-field col s12 m12 l5">
                    <asp:TextBox ID="txt_ciudad" ClientIDMode="Static" CssClass="validate" data-length="60" MaxLength="60" runat="server"></asp:TextBox>
                    <label for="txt_ciudad">Ciudad</label>
                </div>
                <div class="input-field col s12 m12 l4">
                    <uc:ddlPaises ID="ddl_pais" runat="server" />
                    <label for="txt_pais">Pais</label>
                </div>
                <div id="cont_txt_estado" class="input-field col s12 m12 l3" runat="server">
                    <asp:TextBox ID="txt_estado" ClientIDMode="Static" CssClass="validate" data-length="35" MaxLength="35" runat="server"></asp:TextBox>
                    <label for="txt_estado">Estado</label>
                </div>


                <div id="cont_ddl_estado" class="input-field col s12 m12 l3" visible="false" runat="server">
                    <uc:ddlEstados ID="ddl_estado" runat="server" />
                    <label for="txt_pais">Estado</label>

                </div>

            </div>


                <div class="col s12 m12 l12">
                <div class="input-field col s12 m12 l5">
                    <asp:TextBox ID="txt_referencias" ClientIDMode="Static" CssClass="validate" runat="server"></asp:TextBox>
                    <label for="txt_referencias">Referencias </label>

                </div></div>
            
        </div>
        <div class="row">
            <div class="col s12 m12 l12">
                <div class="input-field col s12 m12 l12">
                    <a href="<%= ResolveUrl("~/usuario/mi-cuenta/direcciones-de-envio.aspx") %>" 
                        class="waves-effect waves-light btn blue-grey darken-1" style="text-transform: none;">Regresar</a>
                    <asp:LinkButton ID="btn_crear_direccion" class="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5" 
                  OnClick="btn_crear_direccion_Click"
                  style="text-transform: none;"
                        runat="server">
                                    <i class="material-icons right">add</i> Crear dirección de envío</asp:LinkButton>
                    
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