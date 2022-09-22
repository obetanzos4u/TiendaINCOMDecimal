<%@ Page Language="C#" AutoEventWireup="true"   Async="true" CodeFile="contactos_old.aspx.cs" MasterPageFile="~/gnCliente.master" Inherits="usuario_mi_cuenta" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
     
    
    <div class="is-bt-5 is-mx-6 is-border-soft is-rounded-xl is-p-8 ">

</div>
        <div class="row">
             <div class="col s12 m12 l12 is-bt-3">
            <h2 class="center-align is-m-0">Mis contactos</h2>
                 </div>
                  <div class="col s12 m12 l9">Administra la información de contactos para tus operaciones (Cotizaciones, Pedidos)</div>
                  
             <div class="col s12 m12 l3 right-align">
                  <a ID="eliminar" OnClick="$('#modal_crearContacto').modal('open');" class="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5 right-align tooltipped" data-tooltip="Crear contacto">
                      <i class="material-icons right">person_add</i>Agregar</a>
             </div>
        </div>
        <div class="row ">
            <div class="col s12 m12 l12">
                       <asp:UpdatePanel ID="up_lv_Contactos" class="modal-content" ClientIDMode="Static" UpdateMode="Conditional" runat="server" RenderMode="Block">
            <ContentTemplate>
                <asp:ListView ID="lv_contactos" OnItemUpdating="contacto_ItemUpdating"  OnItemDeleting="contacto_ItemDeleted"  OnItemEditing="contacto_ItemEditing" OnItemCanceling="contacto_ItemCanceling" runat="server">
                    <LayoutTemplate>
                        <ul class="collapsible" data-collapsible="accordion">
                            <div runat="server" id="itemPlaceholder"></div>
                        </ul>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <li>
                            <div class="collapsible-header ">
                                <i class="material-icons">person</i>
                               <span class="blue-text text-darken-2"><strong><%#Eval("nombre") %> <%#Eval("apellido_paterno") %>,</strong></span>&nbsp;<%#Eval("email") %>
                            </div>
                            <div class="collapsible-body">
                              
                                <ul class="collection">
                                       <asp:HiddenField ID="hd_id_contacto" Value='<%#Eval("id") %>'  runat="server" />
                                    <li class="collection-item"><strong>Email:</strong> <%#Eval("email") %></li>
                                    <li class="collection-item"><strong>Nombre:</strong> <%#Eval("nombre") %> </li>
                                    <li class="collection-item"><strong>Apellido Paterno:</strong> <%#Eval("apellido_paterno") %></li>
                                    <li class="collection-item"><strong>Apellido Materno:</strong> <%#Eval("apellido_materno") %></li>
                                
                                    <li class="collection-item"><strong>Teléfono:</strong> <%#Eval("telefono") %></li>
                                    <li class="collection-item"><strong>Celular:</strong> <%#Eval("celular") %></li>
                                </ul>
                               
                                <p>
                                    <asp:LinkButton ID="eliminar" class="waves-effect waves-light btn red  tooltipped" data-tooltip="Elimina este contacto" 
                                         OnClientClick="return confirm('Seguro que deseas eliminar este contacto?')" CommandName="Delete" 
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
                                        <label for="txt_apellido_paterno">Apellido Paterno</label>
                                    </div>
                                    <div class="input-field col s12 m6 l6">
                                        <asp:TextBox ID="txt_apellido_materno" Text='<%#Eval("apellido_materno") %>' runat="server"></asp:TextBox>
                                        <label for="txt_apellido_materno">Apellido Materno</label>
                                    </div>

                                    <div class="input-field col s12 m6 l6">
                                        <asp:TextBox ID="txt_telefono" Text='<%#Eval("telefono") %>' runat="server"></asp:TextBox>
                                        <label for="txt_telefono">Teléfono</label>
                                    </div>
                                    <div class="input-field col s12 m6 l6">
                                        <asp:TextBox ID="txt_celular" Text='<%#Eval("celular") %>' runat="server"></asp:TextBox>
                                        <label for="txt_celular">Celular</label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col s12">
                                        <asp:LinkButton ID="btn_cancelar" runat="server" CommandName="Cancel" class="waves-effect waves-light btn  blue-grey darken-1" Text="Cerrar edición" />
                                        <asp:LinkButton ID="btn_actualizar" runat="server" CommandName="Update" class="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5 " Text="Guardar cambios ✓" />

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






   <div id="modal_crearContacto" class="modal bottom-sheet">

        <asp:UpdatePanel ID="up_comentariosPrecios" class="modal-content" ClientIDMode="Static" UpdateMode="Conditional" runat="server" RenderMode="Block">
            <ContentTemplate>


                <div class="modal-content">
                    <h4>Crear contacto</h4>
                 
                  <div class="row">
           
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
                <label for="txt_apellido_paterno">Apellido Paterno</label>
            </div>
            <div class="input-field col s12 m6 l6">
                <asp:TextBox ID="txt_apellido_materno" Text='<%#Eval("apellido_materno") %>' runat="server"></asp:TextBox>
                <label for="txt_apellido_materno">Apellido Materno</label>
            </div>

                      <div class="input-field col s12 m6 l6">
                          <asp:TextBox ID="txt_telefono" Text='<%#Eval("telefono") %>' runat="server"></asp:TextBox>
                          <label for="txt_telefono">Teléfono</label>
                      </div>
                      <div class="input-field col s12 m6 l6">
                          <asp:TextBox ID="txt_celular" Text='<%#Eval("celular") %>' runat="server"></asp:TextBox>
                          <label for="txt_celular">Celular</label>
                      </div>
                      <div class="input-field col s12 m12 l12">
                          <asp:LinkButton ID="btn_crearContacto" OnClick="btn_crearContacto_Click" CssClass="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5 right-align "
                              runat="server">Crear Contacto</asp:LinkButton>
                      </div>

                  </div>



                </div>
                <div class="modal-footer">
                </div>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_crearContacto" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>

   </div>





<script>

 $(document).ready(function(){
    // the "href" attribute of the modal trigger must specify the modal ID that wants to be triggered
    $('.modal').modal();
  });
        
</script>




</asp:Content>
