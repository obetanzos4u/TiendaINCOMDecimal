<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_direccionesEnvio.ascx.cs" Inherits="uc_direccionesEnvio" %>
<%@ Register TagPrefix="uc" TagName="ddlPaises"  Src="~/userControls/ddl_paises.ascx" %>
<%@ Register TagPrefix="uc" TagName="ddlEstados"  Src="~/userControls/ddl_estados.ascx" %>

   

<asp:UpdatePanel ID="up_lv_Direcciones"  ClientIDMode="AutoID" UpdateMode="Conditional" runat="server" >
              
    <ContentTemplate>
        <asp:ListView ID="lv_direcciones" ClientIDMode="AutoID" OnItemUpdating="direccion_ItemUpdating" OnItemDeleting="direccion_ItemDeleted" OnItemEditing="direccion_ItemEditing" OnItemCanceling="direccion_ItemCanceling" runat="server">
            <LayoutTemplate>
                <div class="">Administra tus direcciones de envío para realizar pedidos y cotizaciones:</div>
                <br class="is-bt-2"/>
                <ul class="collapsible" data-collapsible="accordion" style="border: 0px; box-shadow: none;">
                    <div runat="server" id="itemPlaceholder"></div>
                </ul>
            </LayoutTemplate>
            <ItemTemplate>
                <li>
                    <div class="collapsible-header cards-mis_direcciones_envio is-rounded-lg is-bt-1 is-border-gray">
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
                        <div class="is-flex is-top-2" style="justify-content: space-evenly;">
                            <asp:LinkButton ID="eliminar" class="btn-eliminar_direccion is-space-x-9"
                                OnClientClick="return confirm('Seguro que deseas eliminar esta dirección?')" CommandName="Delete"
                                runat="server">Eliminar</asp:LinkButton>
                            <asp:LinkButton ID="editar" class="is-text-white is-btn-gray btn_editar-direcciones_envio" CommandName="Edit" runat="server">Editar</asp:LinkButton>
                        </div>
                    </div>
                </li>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:HiddenField Value="" runat="server" ID="hf_IndexItem" />
                    <li>
                        <div class="collapsible-header card_edit-mis_direcciones_envio">
                            <img src="/img/webUI/newdesign/entrega_rapida_black.png" class="is-space-x-9" style="width: 28px; height: auto;">
                            <span class="blue-text text-darken-2"><strong><%#Eval("nombre_direccion") %> - </strong></span>&nbsp;<%#Eval("calle") %>, <%#Eval("numero") %>
                        </div>
                        <div class="collapsible-body body_form-direcciones_envio">
                            <div class="row">
                                <asp:HiddenField ID="hd_id_direccion" Value='<%#Eval("id") %>' runat="server" />
                                <asp:Label  CssClass="hide" ID="lbl_estado" runat="server" Text='<%#Eval("estado") %>'></asp:Label>
                                <asp:Label CssClass="hide" ID="lbl_pais" runat="server" Text='<%#Eval("pais")%>'></asp:Label>
                                    <div class="">
                                        <div class="input-field">
                                            <asp:TextBox ID="txt_nombre_direccion" ClientIDMode="Static" CssClass="validate" style="padding: 0rem 0rem 0rem 1rem;" Text='<%#Eval("nombre_direccion") %>' data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                                            <label for="txt_nombre_direccion">Asigna un nombre a esta dirección:</label>
                                            <p style="font-size: 0.75rem; margin: 0 auto 40px auto;"><i>(Ejemplo: Casa, Bodega, Almacén principal u otro)</i></p>
                                        </div>
                                    </div>
                                    <div class="">
                                        <div class="input-field">
                                            <asp:TextBox ID="txt_calle" ClientIDMode="Static" Text='<%#Eval("calle") %>' CssClass="validate" style="padding: 0rem 0rem 0rem 1rem;" data-length="50" MaxLength="50" runat="server"></asp:TextBox>
                                            <label for="txt_calle">Calle:</label>
                                        </div>
                                        <div class="input-field is-flex" style="justify-content: space-between;">
                                            <div style="width: 40%;">
                                                <label for="txt_numero">Número:</label>
                                                <asp:TextBox ID="txt_numero" ClientIDMode="Static" Text='<%#Eval("numero") %>' CssClass="validate" style="padding: 0rem 0rem 0rem 1rem;" data-length="30" MaxLength="30" runat="server"></asp:TextBox>
                                            </div>
                                            <div style="width: 40%;">
                                                <label for="txt_codigo_postal" >Código Postal:</label>
                                                <asp:TextBox ID="txt_codigo_postal" onchange="txtLoading(this);" ClientIDMode="Static"  AutoPostBack="true" OnTextChanged="txt_codigo_postal_TextChanged"
                                                Text='<%#Eval("codigo_postal") %>' CssClass="validate" style="padding: 0rem 0rem 0rem 1rem;" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="input-field input-direcciones_envio" style="margin: 3rem auto;" >
                                            <asp:DropDownList ID="ddl_colonia" runat="server"></asp:DropDownList>
                                            <asp:TextBox ID="txt_colonia" ClientIDMode="Static" Visible="false" Text='<%#Eval("colonia") %>' CssClass="validate" style="padding: 0rem 0rem 0rem 1rem;" data-length="70" MaxLength="35" runat="server"></asp:TextBox>
                                            <label for="txt_colonia" style="top: -36px !important;">Colonia:</label>
                                        </div>
                                    </div>
                                    <div class="">
                                    <asp:UpdatePanel ID="up_pais_estado" class="modal-content" ClientIDMode="Static" UpdateMode="Conditional" runat="server" RenderMode="Block">
                                        <ContentTemplate>
                                            <div class="input-field">
                                                <asp:TextBox ID="txt_delegacion_municipio" ClientIDMode="Static" Text='<%#Eval("delegacion_municipio") %>'
                                                    CssClass="validate" style="color: black; padding: 0rem 0rem 0rem 1rem;" data-length="50" MaxLength="50" runat="server"></asp:TextBox>
                                                <label for="txt_delegacion_municipio">Delegación/Municipio:</label>
                                            </div>
                                            <div class="input-field" style="margin-top: 3rem">
                                                <asp:TextBox ID="txt_ciudad" ClientIDMode="Static" CssClass="validate" style="color: black; padding: 0rem 0rem 0rem 1rem;" data-length="60" MaxLength="60" runat="server"></asp:TextBox>
                                                <label for="txt_ciudad">Ciudad:</label>
                                            </div>
                                            <div id="cont_txt_estado" class="input-field is-top-3" runat="server">
                                                <asp:TextBox ID="txt_estado" ClientIDMode="Static" Text='<%#Eval("estado") %>' CssClass="validate" style="padding: 0rem 0rem 0rem 1rem;"
                                                    data-length="35" MaxLength="35" runat="server"></asp:TextBox>
                                                <label for="txt_estado">Estado:</label>
                                            </div>
                                            <div class="is-flex is-top-2 is-justify-between">
                                                <div id="cont_ddl_estado" class="input-field input-direcciones_envio" style="width: 40%; margin-right: 2rem;" visible="false" runat="server">
                                                    <asp:DropDownList ID="ddl_estado" ClientIDMode="Static" class="selectize-select  browser-default" style="padding: 0rem 0rem 0rem 1rem;" runat="server"></asp:DropDownList>
                                                    <label for="ddl_estado" style="top: -36px !important;">Estado:</label>
                                                </div>
                                                <div class="input-field input-direcciones_envio" style="width: 40%;">
                                                    <asp:DropDownList ID="ddl_pais" AutoPostBack="true" OnSelectedIndexChanged="ddl_pais_SelectedIndexChanged" ClientIDMode="Static" runat="server"></asp:DropDownList>
                                                    <label for="ddl_pais" style="top: -36px !important;">País:</label>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddl_pais" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                    <div class="is-top-2;" style="width: 102%">
                                        <div class="input-field">
                                            <asp:TextBox ID="txt_referencias" TextMode="MultiLine" Text='<%#Eval("referencias") %>' ClientIDMode="Static" CssClass="materialize-textarea" style="padding: 0.5rem 0rem 0rem 1rem;" data-length="100" MaxLength="100" runat="server"></asp:TextBox>
                                            <label for="txt_referencias" >Referencias:</label>
                                        </div>
                                    </div>
                            </div>
                            <div class="row">
                                <div class="">
                                    <asp:UpdatePanel ID="UpdatePanel1" class="modal-content is-flex is-justify-evenly" ClientIDMode="Static" UpdateMode="Conditional" runat="server" RenderMode="Block">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="btn_cancelar" runat="server" CommandName="Cancel" class="is-text-white is-btn-gray btn-volver_direcciones_envio is-space-x-9" Text="Volver a direcciones de envío" />
                                            <asp:LinkButton ID="btn_actualizar" runat="server" CommandName="Update" class="btn-guardar_direcciones_envio is-text-white is-inline-block is-btn-blue is-space-x-9" Text="Guardar" />
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
         