<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_direccionesFacturacion.ascx.cs" Inherits="uc_direccionesFacturacion" %>
<%@ Register TagPrefix="uc" TagName="ddlPaises" Src="~/userControls/ddl_paises.ascx" %>
<%@ Register TagPrefix="uc" TagName="ddlEstados" Src="~/userControls/ddl_estados.ascx" %>



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
                        <i class="material-icons left">insert_drive_file</i>
                        <span class="blue-text text-darken-2"><strong><%#Eval("nombre_direccion") %>  </strong>(<%#Eval("rfc") %>)-</span>&nbsp;<%#Eval("calle") %>, <%#Eval("numero") %>
                    </div>
                    <div class="collapsible-body">

                        <ul class="collection">
                            <asp:HiddenField ID="hd_id_contacto" Value='<%#Eval("id") %>' runat="server" />
                            <li class="collection-item">Alías: <span class="is-select-all is-font-semibold"><%#Eval("nombre_direccion") %></span></li>
                            <li class="collection-item">Razón social: <span class="is-select-all is-font-semibold"><%#Eval("razon_social") %></span></li>
                            <li class="collection-item">RFC: <span class="is-select-all is-font-bold"><%#Eval("rfc") %></span></li>
                            <li class="collection-item">Régimen fiscal: <span class="is-select-all is-font-bold"><%#Eval("regimen_fiscal") %></span></li>
                            <li class="collection-item">Código Postal: <span class="is-select-all is-font-bold"><%#Eval("codigo_postal") %></span></li>
                            <li class="collection-item">Calle y Número: <span class="is-select-all is-font-bold"><%#Eval("calle") %>, <%#Eval("numero") %></span></li>
                            <li class="collection-item">Colonia: <span class="is-select-all is-font-bold"><%#Eval("colonia") %></span></li>
                            <li class="collection-item">Delegación/municipio: <span class="is-select-all is-font-bold"><%#Eval("delegacion_municipio") %></span></li>
                            <li class="collection-item">Estado: 
                                <asp:Label ID="lblestado" runat="server" class="is-select-all is-font-semibold" Text='<%#Eval("estado") %>'></asp:Label></span></li>
                            <li class="collection-item">Pais: <span class="is-select-all is-font-bold"><%#Eval("pais") %></span></li>
                        </ul>

                        <div class="is-flex is-justify-around is-items-center">
                            <asp:LinkButton ID="eliminar" class="is-btn-gray"
                                OnClientClick="return confirm('Seguro que deseas eliminar esta dirección?')" CommandName="Delete"
                                runat="server">Eliminar</asp:LinkButton>
                            <asp:LinkButton ID="editar" class="is-btn-blue" Style="text-transform: none;" CommandName="Edit" runat="server">Editar</asp:LinkButton>
                        </div>
                    </div>
                </li>
            </ItemTemplate>
            <EditItemTemplate>
                <li>
                    <div class="collapsible-header">
                        <i class="material-icons">edit</i>
                        <span class="blue-text text-darken-2"><strong><%#Eval("nombre_direccion") %> - </strong></span>&nbsp;<%#Eval("calle") %>, <%#Eval("numero") %>
                    </div>
                    <div class="collapsible-body">
                        <div class="row">
                            <asp:HiddenField ID="hd_id_direccion" Value='<%#Eval("id") %>' runat="server" />
                            <asp:Label CssClass="hide" ID="lbl_estado" runat="server" Text='<%#Eval("estado") %>'></asp:Label>
                            <asp:Label CssClass="hide" ID="lbl_pais" runat="server" Text='<%#Eval("pais")%>'></asp:Label>
                            <asp:Label CssClass="hide" ID="lbl_regimen_fiscal" runat="server" Text='<%#Eval("regimen_fiscal") %>'></asp:Label>

                            <div class="row ">
                                <div class="col s12 m12 l12 ">
                                    <div class="input-field col s12 m12 l6">
                                        <asp:TextBox ID="txt_nombre_direccion" Text='<%#Eval("nombre_direccion") %>' ClientIDMode="Static" CssClass="validate" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                                        <label for="txt_nombre_direccion">Asigna un alías a esta dirección </label>
                                        <i class="is-text-sm">Ejemplo: Almacén, Casa, Oficina</i>
                                    </div>
                                    <div class="col s12 m12 l6">
                                        <asp:UpdatePanel ID="ddl_up_regimen_fiscal" class="modal-content" ClientIDMode="Static" UpdateMode="Conditional" runat="server" RenderMode="Block">
                                            <ContentTemplate>
                                                <label for="ddl_regimen_fiscal">Régimen fiscal:</label>
                                                <asp:DropDownList ID="ddl_regimen_fiscal" AutoPostBack="true" ClientIDMode="Static" runat="server">
                                                    <asp:ListItem Selected="True" Value="">Seleccionar</asp:ListItem>
                                                    <asp:ListItem Value="601">601 - General de Ley Personas Morales</asp:ListItem>
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
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddl_regimen_fiscal" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                                <div class="col s12 m12 l12">
                                    <div class="input-field col s12 m12 l8">
                                        <asp:TextBox ID="txt_razon_social" Text='<%#Eval("razon_social") %>' ClientIDMode="Static" CssClass="validate" data-length="150" MaxLength="150" runat="server"></asp:TextBox>
                                        <label for="txt_razon_social">Razón social</label>
                                        <i class="is-text-sm">Ejemplo: Insumos Comerciales de Occidente</i>
                                    </div>
                                    <div class="input-field col s12 m12 l4">
                                        <asp:TextBox ID="txt_rfc" Text='<%#Eval("rfc") %>' ClientIDMode="Static" CssClass="validate" data-length="15" MaxLength="15" runat="server"></asp:TextBox>
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
                                    <label for="txt_codigo_postal">Código Postal</label>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col s12">
                                <asp:UpdatePanel ID="UpdatePanel1" class="modal-content is-flex is-justify-around is-items-center" ClientIDMode="Static" UpdateMode="Conditional" runat="server" RenderMode="Block">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="btn_cancelar" runat="server" CommandName="Cancel" class="is-btn-gray" Text="Cancelar" />
                                        <asp:LinkButton ID="btn_actualizar" runat="server" CommandName="Update" class="is-btn-blue" Text="Guardar" />
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
            <EmptyDataTemplate>
                <div class="is-flex is-justify-around is-items-center">
                    <h2>Aún no tienes direcciones de facturación, crea uno ahora.</h2>
                </div>
            </EmptyDataTemplate>
        </asp:ListView>
    </ContentTemplate>
</asp:UpdatePanel>
