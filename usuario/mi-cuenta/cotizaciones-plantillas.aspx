<%@ Page Language="C#" AutoEventWireup="true"   Async="true" CodeFile="cotizaciones-plantillas.aspx.cs"
    MasterPageFile="~/gnCliente.master" Inherits="usuario_cotizaciones_plantillas" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <div class="container z-depth-3">
        <div id="HeaderInfoContactos" runat="server" class="row">
            <div class="col s12 m12 l12">
                <h2>Mis Plantillas </h2>
            </div>
            <div class="col s12 m12 l9">Guarda tus listados de carga rápida de productos.
                   <p>Si conoces el número de parte de tus productos, podras agregarlos de manera rápida. 
      </p>
            </div>
               <div class="col s12 m12 l3 right-align">
                <a id="eliminar" onclick="$('#modal_crearPlantilla').modal('open');" class="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5 right-align tooltipped" data-tooltip="Crear contacto">
                    <i class="material-icons right">add_box
</i>Agregar</a>
            </div>
        </div>

        <div class="row ">
            <div class="col s12 m12 l12">
                <asp:UpdatePanel ID="up_lv_plantillas" class="modal-content" ClientIDMode="Static" UpdateMode="Conditional" runat="server" RenderMode="Block">
                    <ContentTemplate>
                        <asp:ListView ID="lv_plantillas" OnItemUpdating="plantilla_ItemUpdating" OnItemDeleting="plantilla_ItemDeleted"
                            OnItemEditing="plantilla_ItemEditing" OnItemCanceling="plantilla_ItemCanceling" runat="server">
                            <LayoutTemplate>
                                <ul class="collapsible" data-collapsible="accordion">
                                    <div runat="server" id="itemPlaceholder"></div>
                                </ul>
                            </LayoutTemplate>
                            <ItemTemplate>

                                <li>
                                    <div class="collapsible-header ">
                                        <i class="material-icons">assignment</i>
                                        <span class="blue-text text-darken-2"><%#Eval("nombre")  %></span>&nbsp; <span>Creada el: <%#Eval("fechaCreacion") %></span>
                                    </div>
                                    <div class="collapsible-body">
                                        
                                            <asp:HiddenField ID="hf_id_plantilla" Value='<%#Eval("id") %>' runat="server" />
                                            Nombre: <strong><%#Eval("nombre") %></strong><br />
                                            Fecha de creación: <strong><%#Eval("fechaCreacion") %></strong><br />
                                        
                                            <asp:TextBox ID="txt_productosQuickAdd" Enabled="false" CssClass="materialize-textarea" Text='<%#Eval("valor") %>'  TextMode="MultiLine"
                                                placeholder="NúmeroParte Cantidad &#10;NúmeroParte Cantidad&#10;NúmeroParte Cantidad" runat="server"></asp:TextBox>
                                       <br />
                                   
                                            Última modificación: <strong><%#Eval("fechaModificacion")  %></strong><br />
                                            <asp:LinkButton ID="eliminar" class="waves-effect waves-light btn red  tooltipped" data-tooltip="Elimina esta plantilla"
                                                OnClientClick="return confirm('Seguro que deseas eliminar esta plantilla?')" CommandName="Delete"
                                                runat="server">
                                    <i class="material-icons">delete</i></asp:LinkButton>
                                            &nbsp;
                            <asp:LinkButton ID="editar" class="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5"
                                CommandName="Edit" runat="server">
                                <i class="material-icons right">add_box</i>Editar Plantilla</asp:LinkButton>

                                         
                                    </div>
                                </li>
                            </ItemTemplate>

                            <EmptyDataTemplate>
                                <div class="row center-align">
                                    <h2>Aún no tienes plantillas, crea una ahora.</h2>
                                    <a id="eliminar" onclick="$('#modal_crearPlantilla').modal('open');" class="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5 right-align tooltipped" data-tooltip="Crear contacto">
                                        <i class="material-icons right">add_box</i>Agregar</a>
                                </div>
                            </EmptyDataTemplate>
                            <EditItemTemplate>
                                <li>
                                    <div class="collapsible-header ">
                                        <i class="material-icons">add_box</i>
                                        <span class="blue-text text-darken-2"> <%#Eval("nombre")  %></span> &nbsp; <span>Creada el: <%#Eval("fechaCreacion") %></span>
                                    </div>
                                    <div class="collapsible-body">
                                           <asp:HiddenField ID="hf_id_plantilla" Value='<%#Eval("id") %>' runat="server" />

                                        <div class="row">
                                            <div class="col s12 m12 l12">
                                                <asp:TextBox ID="txt_nombre" Text='<%#Eval("nombre") %>' runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col s12 m12 l12">
                                                <asp:TextBox ID="txt_productosQuickAdd" Text='<%#Eval("valor") %>' CssClass="materialize-textarea"  TextMode="MultiLine"
                                                    placeholder="NúmeroParte Cantidad &#10;NúmeroParte Cantidad&#10;NúmeroParte Cantidad" runat="server"></asp:TextBox>
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

        <div id="modal_crearPlantilla" class="modal bottom-sheet">

            <asp:UpdatePanel ID="up_crearPlantilla" class="modal-content" ClientIDMode="Static" UpdateMode="Conditional" runat="server" RenderMode="Block">
                <ContentTemplate>


                    <div class="modal-content">
                        <h3>Crear Plantilla</h3>
                           <p>Simplemente escribe el numero de parte <strong>seguido de un espacio</strong> y su cantidad.   
            Para multiples productos <strong>ingresa un salto de linea (enter)</strong> por producto</p>
                        <div class="row">
                         

                            <div class="input-field col s12 m12 l12">
                                <asp:TextBox ID="txt_productosQuickAdd" CssClass="materialize-textarea" Height="40" TextMode="MultiLine"
                                    placeholder="NúmeroParte Cantidad &#10;NúmeroParte Cantidad&#10;NúmeroParte Cantidad" runat="server"></asp:TextBox>
                            </div>
                            <div class="input-field col s12 m6 l6">
                                <asp:TextBox ID="txt_nombre" Text='<%#Eval("nombre") %>' runat="server"></asp:TextBox>
                                <label for="txt_nombre">Nombre de esta plantilla.</label>
                            </div>
                            <div class="input-field col s12 m12 l12">
                                <asp:LinkButton ID="btn_crearPlantilla" OnClick="btn_crearPlantilla_Click" CssClass="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5 right-align "
                                    runat="server">Crear Plantilla</asp:LinkButton>
                            </div>

                        </div>



                    </div>
                    <div class="modal-footer">
                    </div>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_crearPlantilla" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>

        </div>

    </div>



    <script>

        $(document).ready(function () {
            // the "href" attribute of the modal trigger must specify the modal ID that wants to be triggered
            $('#modal_crearPlantilla').modal();
            $('.collapsible').collapsible();
              $('textarea').trigger('autoresize');

        });

    </script>
</asp:Content>
