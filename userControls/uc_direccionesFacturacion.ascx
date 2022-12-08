<%@ Control Language="C#" AutoEventWireup="true"  CodeFile="uc_direccionesFacturacion.ascx.cs" Inherits="uc_direccionesFacturacion" %>
<%@ Register TagPrefix="uc" TagName="ddlPaises"  Src="~/userControls/ddl_paises.ascx" %>
<%@ Register TagPrefix="uc" TagName="ddlEstados"  Src="~/userControls/ddl_estados.ascx" %>

   

  <asp:UpdatePanel ID="up_lv_Direcciones" class="modal-content" ClientIDMode="Static" UpdateMode="Conditional" runat="server" RenderMode="Block">
                    <ContentTemplate>
                        <asp:ListView ID="lv_direcciones" OnItemUpdating="direccion_ItemUpdating" OnItemDeleting="direccion_ItemDeleted" OnItemEditing="direccion_ItemEditing" OnItemCanceling="direccion_ItemCanceling" runat="server">
                            <LayoutTemplate>
                                <ul class="collapsible" style="border: 0px; box-shadow: none;" data-collapsible="accordion">
                                    <div runat="server" id="itemPlaceholder"></div>
                                </ul>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li>
                                    <div class="collapsible-header is-border-gray is-rounded-lg is-bt-1">
                                       <i class="material-icons left">playlist_add_check</i>
                                        <span class="blue-text text-darken-2"><strong><%#Eval("nombre_direccion") %>  </strong>(<%#Eval("rfc") %>)-</span>&nbsp;<%#Eval("calle") %>, <%#Eval("numero") %>
                                    </div>
                                    <div class="collapsible-body">
                                        <ul class="collection">
                                            <asp:HiddenField ID="hd_id_contacto" Value='<%#Eval("id") %>' runat="server" />
                                            <li class="collection-item"><strong>Nombre:</strong> <%#Eval("nombre_direccion") %></li>
                                            <li class="collection-item"><strong>RFC:</strong> <%#Eval("rfc") %></li>
                                            <li class="collection-item"><strong>Razón social:</strong> <%#Eval("razon_social") %></li>
                                            <li class="collection-item"><strong>Calle y Número:</strong> <%#Eval("calle") %>, <%#Eval("numero") %> </li>
                                            <li class="collection-item"><strong>Colonia:</strong> <%#Eval("colonia") %></li>
                                            <li class="collection-item"><strong>Delegación/municipio:</strong> <%#Eval("delegacion_municipio") %></li>
                                            <li class="collection-item"><strong>Estado:</strong> <asp:Label ID="lblestado" runat="server" Text='<%#Eval("estado") %>'></asp:Label></li>
                                            <li class="collection-item"><strong>Código Postal:</strong> <%#Eval("codigo_postal") %></li>
                                            <li class="collection-item"><strong>Pais:</strong> <%#Eval("pais") %></li>
                                        </ul>
                                        <p>
                                            <asp:LinkButton ID="eliminar" class="is-text-white is-btn-gray" style="margin-right: 2rem;"
                                                OnClientClick="return confirm('Seguro que deseas eliminar esta dirección?')" CommandName="Delete"
                                                runat="server">
                                                Eliminar
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="editar" class="is-text-white is-btn-gray" style="text-transform: none;" CommandName="Edit" runat="server">
                                                Editar
                                            </asp:LinkButton>
                                        </p>
                                    </div>
                                </li>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <li>
                                    <div class="collapsible-header">
                                        <i class="material-icons">local_shipping</i>
                                        <span class="blue-text text-darken-2"><strong><%#Eval("nombre_direccion") %> - </strong></span>&nbsp;<%#Eval("calle") %>, <%#Eval("numero") %>
                                    </div>
                                    <div class="collapsible-body">
                                        <div class="row">
                                            <asp:HiddenField ID="hd_id_direccion" Value='<%#Eval("id") %>' runat="server" />
                                            <asp:Label CssClass="hide" ID="lbl_estado" runat="server" Text='<%#Eval("estado") %>'></asp:Label>
                                            <asp:Label CssClass="hide" ID="lbl_pais" runat="server" Text='<%#Eval("pais")%>'></asp:Label>
                                            <div class="row ">
                                                <div class="col s12 m12 l12 ">
                                                    <div class="input-field col s12 m12 l6">
                                                        <asp:TextBox ID="txt_nombre_direccion" Text='<%#Eval("nombre_direccion") %>' ClientIDMode="Static" CssClass="validate" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                                                        <label for="txt_nombre_direccion">Asigna un nombre corto como referencia a esta dirección </label>
                                                        <i>Ejemplo: Incom</i>
                                                    </div>
                                                </div>
                                                <div class="col s12 m12 l12">
                                                    <div class="input-field col s12 m12 l8">
                                                        <asp:TextBox ID="txt_razon_social" Text='<%#Eval("razon_social") %>' ClientIDMode="Static" CssClass="validate" data-length="150" MaxLength="150" runat="server"></asp:TextBox>
                                                        <label for="txt_razon_social">Razón social</label>
                                                        <i>Ejemplo: Insumos Comerciales de Occidente S.A. de C.V.</i>
                                                    </div>
                                                    <div class="input-field col s12 m12 l4">
                                                        <asp:TextBox ID="txt_rfc" Text='<%#Eval("rfc") %>'  ClientIDMode="Static" CssClass="validate" data-length="15" MaxLength="15" runat="server"></asp:TextBox>
                                                        <label for="txt_numero">RFC</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col s12 m12 l12">
                                                <div class="input-field col s12 m12 l4">
                                                    <asp:TextBox ID="txt_calle" ClientIDMode="Static" Text='<%#Eval("calle") %>' CssClass="validate" data-length="50" MaxLength="50" runat="server"></asp:TextBox>
                                                    <label for="txt_calle">Calle</label>
                                                </div>
                                                <div class="input-field col s12 m12 l3">
                                                    <asp:TextBox ID="txt_numero" ClientIDMode="Static" Text='<%#Eval("numero") %>' CssClass="validate" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                                                        <label for="txt_numero">Número</label>
                                                    </div>
                                                    <div class="input-field col s12 m12 l5">
                                                        <asp:TextBox ID="txt_colonia" ClientIDMode="Static" Text='<%#Eval("colonia") %>' CssClass="validate" data-length="35" MaxLength="35" runat="server"></asp:TextBox>
                                                        <label for="txt_colonia">Colonia</label>
                                                    </div>
                                            </div>
                                            <div class="col s12 m12 l12">
                                                <asp:UpdatePanel ID="up_pais_estado" class="modal-content" ClientIDMode="Static" UpdateMode="Conditional" runat="server" RenderMode="Block">
                                                    <ContentTemplate>
                                                        <div class="input-field col s12 m12 l5">
                                                            <asp:TextBox ID="txt_delegacion_municipio" ClientIDMode="Static" Text='<%#Eval("delegacion_municipio") %>' CssClass="validate" data-length="35" MaxLength="35" runat="server"></asp:TextBox>
                                                            <label for="txt_delegacion_municipio">Delegación/Municipio</label>
                                                        </div>
                                                        <div id="cont_txt_estado" class="input-field col s12 m12 l3" runat="server">
                                                            <asp:TextBox ID="txt_estado" ClientIDMode="Static" Text='<%#Eval("estado") %>' CssClass="validate" data-length="35" MaxLength="35" runat="server"></asp:TextBox>
                                                            <label for="txt_estado">Estado</label>
                                                        </div>
                                                        <div id="cont_ddl_estado" class="input-field col s12 m12 l3" visible="false" runat="server">
                                                            <asp:DropDownList ID="ddl_estado" ClientIDMode="Static" runat="server"></asp:DropDownList>

                                                            <label for="ddl_estado">Estado</label>
                                                        </div>
                                                        <div class="input-field col s12 m12 l4">
                                                            <asp:DropDownList ID="ddl_pais" AutoPostBack="true" OnSelectedIndexChanged="ddl_pais_SelectedIndexChanged" ClientIDMode="Static" runat="server"></asp:DropDownList>

                                                            <label for="ddl_pais">Pais</label>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddl_pais" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col s12 m12 l12">
                                                <div class="input-field col s12 m12 l5">
                                                    <asp:TextBox ID="txt_codigo_postal" ClientIDMode="Static" Text='<%#Eval("codigo_postal") %>' CssClass="validate" runat="server"></asp:TextBox>
                                                    <label for="txt_codigo_postal" >Código Postal</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col s12">
                                                <asp:UpdatePanel ID="UpdatePanel1" class="modal-content" ClientIDMode="Static" UpdateMode="Conditional" runat="server" RenderMode="Block">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="btn_cancelar" runat="server" CommandName="Cancel" class="is-text-white is-btn-gray" Text="Cerrar" style="margin-right: 2rem"/>
                                                        <asp:LinkButton ID="btn_actualizar" runat="server" CommandName="Update" class="is-text-white is-btn-gray" Text="Guardar" />
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
                                <div class="row center-align"> <h2>Aún no tienes direcciones de facturación, crea uno ahora.</h2>
                                    <a id="eliminar" href="<%= ResolveUrl("~/usuario/mi-cuenta/crear-direccion-de-facturacion.aspx") %>" class="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5 right-align tooltipped" style="text-transform: none;" data-tooltip="Agregar dirección de envío ">
                                    <i class="material-icons right">playlist_add_check</i>Agregar dirección</a>
                                </div>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </ContentTemplate>
                </asp:UpdatePanel>