<%@ Page Language="C#" AutoEventWireup="true"   Async="true" CodeFile="direcciones-de-facturacion_oldf.aspx.cs" MasterPageFile="~/gnCliente.master" Inherits="direcciones_facturacion" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
   
          


    <div class="container z-depth-3">
        <div class="row">
            <div class="col l12">
                <h1 class="center-align">Facturacion</h1>
            </div>
        </div>
        <div class="row">
            <div class="col s12 m12 l12">
                <h2>Mis direcciones de facturación</h2>
            </div>
            <div class="col s12 m12 l9">Administra tus direcciones de facturación para: Cotizaciones ó Pedidos</div>

            <div class="col s12 m12 l3 right-align">
                <a id="eliminar" href="<%= ResolveUrl("~/usuario/mi-cuenta/crear-direccion-de-facturacion.aspx") %>" class="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5 right-align tooltipped" data-tooltip="Agregar dirección denvío ">
                    <i class="material-icons right">local_shipping</i>Agregar dirección</a>
            </div>
        </div>
        <div class="row ">
            <div class="col s12 m12 l12">
                <asp:UpdatePanel ID="up_lv_Direcciones" class="modal-content" ClientIDMode="Static" UpdateMode="Conditional" runat="server" RenderMode="Block">
                    <ContentTemplate>
                        <asp:ListView ID="lv_direcciones" OnItemUpdating="direccion_ItemUpdating" OnItemDeleting="direccion_ItemDeleted" OnItemEditing="direccion_ItemEditing" OnItemCanceling="direccion_ItemCanceling" runat="server">
                            <LayoutTemplate>
                                <ul class="collapsible" data-collapsible="accordion">
                                    <div runat="server" id="itemPlaceholder"></div>
                                </ul>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li>
                                    <div class="collapsible-header ">
                                        <i class="material-icons">local_shipping</i>
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
                                            <li class="collection-item"><strong>Delegación:</strong> <%#Eval("delegacion") %></li>
                                            <li class="collection-item"><strong>Municipio/Estado:</strong> <asp:Label ID="lbl_municipio_estado" runat="server" Text='<%#Eval("municipio_estado") %>'></asp:Label></li>
                                            <li class="collection-item"><strong>Código Postal:</strong> <%#Eval("codigo_postal") %></li>
                                            <li class="collection-item"><strong>Pais:</strong> <%#Eval("pais") %></li>
                                            <li class="collection-item"><strong>Referencias:</strong> <%#Eval("referencias") %></li>
                                        </ul>

                                        <p>
                                            <asp:LinkButton ID="eliminar" class="waves-effect waves-light btn red  tooltipped" data-tooltip="Elimina esta dirección"
                                                OnClientClick="return confirm('Seguro que deseas eliminar esta dirección?')" CommandName="Delete"
                                                runat="server">
                                    <i class="material-icons">delete</i></asp:LinkButton>
                                            &nbsp;
                            <asp:LinkButton ID="editar" class="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5" CommandName="Edit" runat="server">
                                <i class="material-icons right">edit</i>Editar</asp:LinkButton>
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
                                            <asp:Label CssClass="hide" ID="lbl_municipio_estado" runat="server" Text='<%#Eval("municipio_estado") %>'></asp:Label>
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
                                                            <asp:TextBox ID="txt_delegacion" ClientIDMode="Static" Text='<%#Eval("delegacion") %>' CssClass="validate" data-length="35" MaxLength="35" runat="server"></asp:TextBox>
                                                            <label for="txt_delegacion">Delegación</label>
                                                        </div>
                                                        <div id="cont_txt_municipio_estado" class="input-field col s12 m12 l3" runat="server">
                                                            <asp:TextBox ID="txt_municipio_estado" ClientIDMode="Static" Text='<%#Eval("municipio_estado") %>' CssClass="validate" data-length="25" MaxLength="24" runat="server"></asp:TextBox>
                                                            <label for="txt_municipio_estado">Municipio/Estado</label>
                                                        </div>
                                                        <div id="cont_ddl_municipio_estado" class="input-field col s12 m12 l3" visible="false" runat="server">
                                                            <asp:DropDownList ID="ddl_municipio_estado" ClientIDMode="Static" runat="server"></asp:DropDownList>
                                                           
                                                            <label for="ddl_municipio_estado">Estado</label>

                                                        </div>

                                                        <div class="input-field col s12 m12 l4">
                                                            <asp:DropDownList ID="ddl_pais" AutoPostBack="true" OnSelectedIndexChanged="ddl_pais_SelectedIndexChanged" ClientIDMode="Static" runat="server"></asp:DropDownList>
                                                      
                                                            <label for="ddl_pais">Pais</label>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>  <asp:AsyncPostBackTrigger ControlID="ddl_pais" EventName="SelectedIndexChanged" />

                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col s12 m12 l12">
                                                <div class="input-field col s12 m12 l5">
                                                        <asp:TextBox ID="txt_codigo_postal" ClientIDMode="Static" Text='<%#Eval("codigo_postal") %>' CssClass="validate" runat="server"></asp:TextBox>
                                                        <label for="txt_codigo_postal" >Código Postal</label>

                                                    </div>
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
                                                        <asp:LinkButton ID="btn_cancelar" runat="server" CommandName="Cancel" class="waves-effect waves-light btn  blue-grey darken-1" Text="Cerrar edición" />
                                                        <asp:LinkButton ID="btn_actualizar" runat="server" CommandName="Update" class="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5 " Text="Guardar cambios ✓" />
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btn_cancelar" EventName="Click" />
                                                         <asp:AsyncPostBackTrigger ControlID="btn_actualizar" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>

                                            </div>
                                        </div>
                                </li>
                            </EditItemTemplate>
                        </asp:ListView>
                    </ContentTemplate>

                </asp:UpdatePanel>
            </div>
        </div>
    </div>



</asp:Content>
