<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_contactos.ascx.cs" Inherits="uc_contactos" %>


    
<div id="HeaderInfoContactos" runat="server" class="row">
    <div class="col s12 m12 l12 is-bt-3">
        <h2 class="is-text-center is-m-0 is-bt-1 is-font-bold is-text-black-soft">Mis contactos</h2>
    </div>
    <div class="is-w-full">
        <div style="float: right">
            <a ID="eliminar" OnClick="$('#modal_crearContacto').modal('open');" class="is-text-white right-align" style="text-transform: none;">
                <div class="is-flex is-btn-blue btn-agregar_contactos" style="align-items: center">
                    <i class="material-icons icon-agregar_contactos">person_add</i>
                    <span class="span-agregar_contactos">Agregar contacto</span>
                </div>
            </a>
        </div>
    </div>
    <div class="text-mis_contactos is-w-full is-flex" style="font-size: 1.25rem; padding-top: 2rem">Administra la información de contactos para realizar tus operaciones:
    </div>
</div>
<div class="row ">
    <div class="col s12 m12 l12">
        <asp:UpdatePanel ID="up_lv_Contactos" class="modal-content" ClientIDMode="Static" UpdateMode="Conditional" runat="server" RenderMode="Block">
            <ContentTemplate>
                <asp:ListView ID="lv_contactos" OnItemUpdating="contacto_ItemUpdating"  OnItemDeleting="contacto_ItemDeleted"  OnItemEditing="contacto_ItemEditing" OnItemCanceling="contacto_ItemCanceling" runat="server">
                    <LayoutTemplate>
                        <ul class="collapsible" data-collapsible="accordion" style="border: 0px; box-shadow: none;">
                            <div runat="server" id="itemPlaceholder"></div>
                        </ul>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <li>
                            <div class="collapsible-header is-border-gray is-rounded-lg is-bt-1" style="border: 1px solid #878787; margin-bottom: 1rem; border-radius: 8px;">
                                <i class="material-icons">person</i>
                            <span class="blue-text text-darken-2"><strong><%#Eval("nombre") %> <%#Eval("apellido_paterno") %>,</strong></span>&nbsp;<%#Eval("email") %>
                            </div>
                            <div class="collapsible-body">
                            
                                <ul class="collection">
                                    <asp:HiddenField ID="hd_id_contacto" Value='<%#Eval("id") %>'  runat="server" />
                                    <li class="collection-item"><strong>Email:</strong> <%#Eval("email") %></li>
                                    <li class="collection-item"><strong>Nombre:</strong> <%#Eval("nombre") %> </li>
                                    <li class="collection-item"><strong>Apellidos:</strong> <%#Eval("apellido_paterno") %></li>
                                    <!-- <li class="collection-item"><strong>Apellido Materno:</strong> <%#Eval("apellido_materno") %></li> -->
                                    <li class="collection-item"><strong>Teléfono:</strong> <%#Eval("telefono") %></li>
                                    <li class="collection-item"><strong>Teléfono alternativo:</strong> <%#Eval("celular") %></li>
                                </ul>
                            
                                <p>
                                    <asp:LinkButton ID="eliminar" class="btn-eliminar_direccion btn-mis_contactos is-inline-block" style="margin-right: 2rem;"
                                        OnClientClick="return confirm('Seguro que deseas eliminar este contacto?')" CommandName="Delete" runat="server">
                                        Eliminar
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="editar" class="is-text-white is-btn-gray editar-mi_cuenta-contacto" style="border: 8px"  CommandName="Edit" runat="server">
                                        Editar
                                    </asp:LinkButton>
                                </p>
                            </div>
                        </li>
                    </ItemTemplate>

                    <EmptyDataTemplate>
                        <div class="row center-align"> <h3>Aún no tienes contactos. Puedes añadir tu primer contacto ahora.</h3>
                            <!-- <a ID="eliminar" OnClick="$('#modal_crearContacto').modal('open');" class="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5 right-align tooltipped" style="text-transform: none;" data-tooltip="Crear contacto">
                                <i class="material-icons right">person_add</i>Agregar</a> -->
                        </div>
                    </EmptyDataTemplate>

                    <EditItemTemplate>
                        <li>
                            <div class="collapsible-header collapsible-header_contacto">
                                <i class="material-icons">person</i>
                                <span class="blue-text text-darken-2"><strong><%#Eval("nombre") %> <%#Eval("apellido_paterno") %>,</strong></span>&nbsp;<%#Eval("email") %>
                            </div>
                            <div class="collapsible-body">
                                <div class="row">
                                    <asp:HiddenField ID="hd_id_contacto" Value='<%#Eval("id") %>'  runat="server" />
                                    <div class="input-field col s12 m6 l6">
                                        <asp:TextBox ID="txt_email" Text='<%#Eval("email") %>' runat="server"></asp:TextBox>
                                        <label for="txt_email">Email</label>
                                    </div>
                                    <div class="input-field col s12 m6 l6">
                                        <asp:TextBox ID="txt_nombre" Text='<%#Eval("nombre") %>' runat="server"></asp:TextBox>
                                        <label for="txt_nombre">Nombre(s)</label>
                                    </div>
                                    <div class="input-field col s12 m6 l6">
                                        <asp:TextBox ID="txt_apellido_paterno" Text='<%#Eval("apellido_paterno") %>' runat="server"></asp:TextBox>
                                        <label for="txt_apellido_paterno">Apellidos</label>
                                    </div>
                                    <!-- <div class="input-field col s12 m6 l6">
                                        <asp:TextBox ID="txt_apellido_materno" Text='<%#Eval("apellido_materno") %>' runat="server"></asp:TextBox>
                                        <label for="txt_apellido_materno">Apellido Materno</label>
                                    </div> -->
                                    <div class="input-field col s12 m6 l6">
                                        <asp:TextBox ID="txt_telefono" Text='<%#Eval("telefono") %>' runat="server"></asp:TextBox>
                                        <label for="txt_telefono">Teléfono</label>
                                    </div>
                                    <div class="input-field col s12 m6 l6">
                                        <asp:TextBox ID="txt_celular" Text='<%#Eval("celular") %>' runat="server"></asp:TextBox>
                                        <label for="txt_celular">Teléfono alternativo</label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col s12">
                                        <asp:UpdatePanel ID="UpdatePanel1" class="modal-content" ClientIDMode="Static" UpdateMode="Conditional" runat="server" RenderMode="Block">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="btn_cancelar" runat="server" CommandName="Cancel" class="btn-cerrar_edicion edicion-mis_contactos is-inline-block" style="margin-right: 2rem;" Text="Cerrar" />
                                                <asp:LinkButton ID="btn_actualizar" runat="server" CommandName="Update" class="btn-guardar_edicion is-btn-blue is-text-white  is-inline-block" Text="Guardar" />
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
                </asp:ListView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>

<div id="modal_crearContacto" class="modal bottom-sheet">
    <asp:UpdatePanel ID="up_crearContacto" class="modal-content" ClientIDMode="Static" UpdateMode="Conditional" runat="server" RenderMode="Block">
        <ContentTemplate>
            <div class="modal-content">
                <h3>Agregar contacto</h3>
                <div class="row">
                    <div class="input-field col s12 m6 l6">
                        <asp:TextBox ID="txt_email" Text='<%#Eval("email") %>' runat="server"></asp:TextBox>
                        <label for="txt_email">Correo electrónico</label>
                    </div>
                    <div class="input-field col s12 m6 l6">
                        <asp:TextBox ID="txt_nombre" Text='<%#Eval("nombre") %>' runat="server"></asp:TextBox>
                        <label for="txt_nombre">Nombre(s)</label>
                    </div>
                    <div class="input-field col s12 m6 l6">
                        <asp:TextBox ID="txt_apellido_paterno" Text='<%#Eval("apellido_paterno") %>' runat="server"></asp:TextBox>
                        <label for="txt_apellido_paterno">Apellidos</label>
                    </div>
                    <!-- <div class="input-field col s12 m6 l6">
                        <asp:TextBox ID="txt_apellido_materno" Text='<%#Eval("apellido_materno") %>' runat="server"></asp:TextBox>
                        <label for="txt_apellido_materno">Apellido Materno</label>
                    </div> -->
                    <div class="input-field col s12 m6 l6">
                        <asp:TextBox ID="txt_telefono" Text='<%#Eval("telefono") %>' runat="server"></asp:TextBox>
                        <label for="txt_telefono">Teléfono</label>
                    </div>
                    <div class="input-field col s12 m6 l6">
                        <asp:TextBox ID="txt_celular" Text='<%#Eval("celular") %>' runat="server"></asp:TextBox>
                        <label for="txt_celular">Teléfono alternativo</label>
                    </div>
                    <div class="input-field col s12 m12 l12">
                        <asp:LinkButton ID="btn_crearContacto" OnClick="btn_crearContacto_Click"
                            runat="server">
                            <div class="is-text-white is-btn-blue">Guardar</div>
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="modal-footer"></div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_crearContacto" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</div>
<script>

 $(document).ready(function(){
    // the "href" attribute of the modal trigger must specify the modal ID that wants to be triggered
     $('#modal_crearContacto').modal();
  });
        
</script>

