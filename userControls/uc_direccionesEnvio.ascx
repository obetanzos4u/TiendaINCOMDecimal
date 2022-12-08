<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_direccionesEnvio.ascx.cs" Inherits="uc_direccionesEnvio" %>
<%@ Register TagPrefix="uc" TagName="ddlPaises"  Src="~/userControls/ddl_paises.ascx" %>
<%@ Register TagPrefix="uc" TagName="ddlEstados"  Src="~/userControls/ddl_estados.ascx" %>

   

<asp:UpdatePanel ID="up_lv_Direcciones"  ClientIDMode="AutoID" UpdateMode="Conditional" runat="server" >
              
    <ContentTemplate>
        <asp:ListView ID="lv_direcciones" ClientIDMode="AutoID" OnItemUpdating="direccion_ItemUpdating" OnItemDeleting="direccion_ItemDeleted" OnItemEditing="direccion_ItemEditing" OnItemCanceling="direccion_ItemCanceling" runat="server">
            <LayoutTemplate>
                <div class="col s12 m12 l9" style="font-size: 1.25rem;">Administra tus direcciones de envío para realizar pedidos y cotizaciones:</div>
                <br class="is-bt-2"/>
                <ul class="collapsible" data-collapsible="accordion" style="border: 0px; box-shadow: none;">
                    <div runat="server" id="itemPlaceholder"></div>
                </ul>
            </LayoutTemplate>
            <ItemTemplate>
                <li>
                    <div class="collapsible-header is-rounded-lg is-bt-1" style="border: 1px solid #B7B7B7">
                        <img src="/img/webUI/newdesign/entrega_rapida_black.png" class="is-space-x-9" style="width: 28px;"/>
                        <span class="blue-text text-darken-2"><strong><%#Eval("nombre_direccion") %> - </strong></span>&nbsp;<%#Eval("calle") %>, <%#Eval("numero") %>
                    </div>
                    <div class="collapsible-body">
                        <ul class="collection">
                            <asp:HiddenField ID="hd_id_direccion" Value='<%#Eval("id") %>' runat="server" />
                            <li class="collection-item"><strong>Nombre:</strong> <%#Eval("nombre_direccion") %></li>
                            <li class="collection-item"><strong>Calle y Número:</strong> <%#Eval("calle") %>, <%#Eval("numero") %> </li>
                            <li class="collection-item"><strong>Colonia:</strong> <%#Eval("colonia") %></li>
                            <li class="collection-item"><strong>Delegación/Municipio:</strong> <%#Eval("delegacion_municipio") %></li>
                            <li class="collection-item"><strong>Estado:</strong> <asp:Label ID="lbl_estado" runat="server" Text='<%#Eval("estado") %>'></asp:Label></li>
                            <li class="collection-item"><strong>Código Postal:</strong> <%#Eval("codigo_postal") %></li>
                            <li class="collection-item"><strong>Pais:</strong> <%#Eval("pais") %></li>
                            <li class="collection-item"><strong>Referencias:</strong> <%#Eval("referencias") %></li>
                        </ul>
                        <p>
                            <asp:LinkButton ID="eliminar" class="is-text-white is-btn-gray is-space-x-9"
                                OnClientClick="return confirm('Seguro que deseas eliminar esta dirección?')" CommandName="Delete"
                                runat="server">Eliminar</asp:LinkButton>
                            <asp:LinkButton ID="editar" class="is-text-white is-btn-gray" CommandName="Edit" runat="server">Editar</asp:LinkButton>
                        </p>
                    </div>
                </li>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:HiddenField Value="" runat="server" ID="hf_IndexItem" />
                    <li>
                        <div class="collapsible-header">
                            <img src="/img/webUI/newdesign/entrega_rapida_black.png" class="is-space-x-9" style="width: 28px; height: auto;">
                            <span class="blue-text text-darken-2"><strong><%#Eval("nombre_direccion") %> - </strong></span>&nbsp;<%#Eval("calle") %>, <%#Eval("numero") %>
                        </div>
                        <div class="collapsible-body">
                            <div class="row">
                                <asp:HiddenField ID="hd_id_direccion" Value='<%#Eval("id") %>' runat="server" />
                                <asp:Label  CssClass="hide" ID="lbl_estado" runat="server" Text='<%#Eval("estado") %>'></asp:Label>
                                <asp:Label CssClass="hide" ID="lbl_pais" runat="server" Text='<%#Eval("pais")%>'></asp:Label>
                                    <div class="col s12 m12 l12">
                                        <div class="input-field col s12 m12 l6">
                                            <asp:TextBox ID="txt_nombre_direccion" ClientIDMode="Static" CssClass="validate" Text='<%#Eval("nombre_direccion") %>' data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                                            <label for="txt_nombre_direccion">Asigna un nombre a esta dirección </label>
                                            <i>(Ejemplo: Casa, Bodega, Almacén principal)</i>
                                        </div>
                                    </div>
                                    <div class="col s12 m12 l12">
                                        <div class="input-field col s12 m12 l5">
                                            <asp:TextBox ID="txt_codigo_postal" onchange="txtLoading(this);" ClientIDMode="Static"  AutoPostBack="true" OnTextChanged="txt_codigo_postal_TextChanged"
                                            Text='<%#Eval("codigo_postal") %>' CssClass="validate" runat="server"></asp:TextBox>
                                            <label for="txt_codigo_postal" >Código Postal</label>
                                        </div>
                                    </div>
                                    <div class="col s12 m12 l12">
                                        <div class="input-field col s12 m12 l4">
                                            <asp:TextBox ID="txt_calle" ClientIDMode="Static" Text='<%#Eval("calle") %>' CssClass="validate" data-length="50" MaxLength="50" runat="server"></asp:TextBox>
                                            <label for="txt_calle">Calle</label>
                                        </div>
                                        <div class="input-field col s12 m12 l3">
                                            <asp:TextBox ID="txt_numero" ClientIDMode="Static" Text='<%#Eval("numero") %>' CssClass="validate" data-length="30" MaxLength="30" runat="server"></asp:TextBox>
                                            <label for="txt_numero">Número</label>
                                        </div>
                                        <div class="input-field col s12 m12 l5">
                                                <asp:DropDownList ID="ddl_colonia" runat="server"></asp:DropDownList>
                                            <asp:TextBox ID="txt_colonia" ClientIDMode="Static" Visible="false" Text='<%#Eval("colonia") %>' CssClass="validate" data-length="70" MaxLength="35" runat="server"></asp:TextBox>
                                            <label for="txt_colonia">Colonia</label>
                                        </div>
                                    </div>
                                <div class="col s12 m12 l12">
                                    <asp:UpdatePanel ID="up_pais_estado" class="modal-content" ClientIDMode="Static" UpdateMode="Conditional" runat="server" RenderMode="Block">
                                        <ContentTemplate>
                                            <div class="input-field col s12 m12 l5">
                                                <asp:TextBox ID="txt_delegacion_municipio" ClientIDMode="Static" Text='<%#Eval("delegacion_municipio") %>'
                                                    CssClass="validate" style="color: black;" data-length="50" MaxLength="50" runat="server"></asp:TextBox>
                                                <label for="txt_delegacion_municipio">Delegación/Municipio</label>
                                            </div>
                                            <div class="input-field col s12 m12 l5">
                                                <asp:TextBox ID="txt_ciudad" ClientIDMode="Static" CssClass="validate" style="color: black;" data-length="60" MaxLength="60" runat="server"></asp:TextBox>
                                                <label for="txt_ciudad">Ciudad</label>
                                            </div>
                                            <div id="cont_txt_estado" class="input-field col s12 m12 l3" runat="server">
                                                <asp:TextBox ID="txt_estado" ClientIDMode="Static" Text='<%#Eval("estado") %>' CssClass="validate"
                                                    data-length="35" MaxLength="35" runat="server"></asp:TextBox>
                                                <label for="txt_estado">Estado</label>
                                            </div>
                                            <div id="cont_ddl_estado" class="input-field col s12 m12 l3" visible="false" runat="server">
                                                <asp:DropDownList ID="ddl_estado" ClientIDMode="Static" class="selectize-select  browser-default" runat="server"></asp:DropDownList>
                                                <label for="ddl_estado">Estado</label>
                                            </div>
                                            <div class="input-field col s12 m12 l4">
                                                <asp:DropDownList ID="ddl_pais" AutoPostBack="true" OnSelectedIndexChanged="ddl_pais_SelectedIndexChanged" ClientIDMode="Static" runat="server"></asp:DropDownList>
                                                <label for="ddl_pais">País</label>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddl_pais" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                    <div class="col s12 m12 l12">
                                        <div class="input-field col s12 m12 l12">
                                            <asp:TextBox ID="txt_referencias" TextMode="MultiLine" Text='<%#Eval("referencias") %>' ClientIDMode="Static" CssClass="materialize-textarea" data-length="100" MaxLength="100" runat="server"></asp:TextBox>
                                            <label for="txt_referencias" >Referencias</label>
                                        </div>
                                    </div>
                            </div>
                            <div class="row">
                                <div class="col s12">
                                    <asp:UpdatePanel ID="UpdatePanel1" class="modal-content" ClientIDMode="Static" UpdateMode="Conditional" runat="server" RenderMode="Block">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="btn_cancelar" runat="server" CommandName="Cancel" class="is-text-white is-btn-gray is-space-x-9" Text="Volver a direcciones de envío" />
                                            <asp:LinkButton ID="btn_actualizar" runat="server" CommandName="Update" class="is-text-white is-btn-gray is-space-x-9" Text="Guardar" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btn_cancelar" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btn_actualizar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </li>
            </EditItemTemplate>
            <EmptyDataTemplate>
                <div class="row center-align is-bt-3">
                    <h3>Aún no tienes direcciones de envío. Puedes añadir tu primer dirección ahora.</h3>
                    <!-- <a id="eliminar" href="<%= ResolveUrl("~/usuario/mi-cuenta/crear-direccion-de-envio.aspx") %>" style="text-transform: none;">
                        <div class="btn-1 is-text-white is-flex is-items-center is-m-auto is-w-fit is-p-4">
                            <img src="/img/webUI/newdesign/entrega.png" class="is-space-x-9" style="width: 28px;"/>
                            <span>Agregar dirección </span>
                        </div>
                    </a> -->
                </div>
            </EmptyDataTemplate>
        </asp:ListView>
    </ContentTemplate>
</asp:UpdatePanel>
         