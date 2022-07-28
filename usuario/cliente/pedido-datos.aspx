<%@ Page Language="C#" AutoEventWireup="true" Async="true" MasterPageFile="~/bootstrap/basic.master" CodeFile="pedido-datos.aspx.cs" Inherits="usuario_cliente_pedido_datos" %>

<%@ Register Src="~/usuario/cliente/cliente-header.ascx" TagPrefix="header" TagName="menuGeneral" %>

 


<asp:Content runat="server" ContentPlaceHolderID="body">
    <header:menuGeneral ID="menuGeneral" runat="server" />
 



    <asp:HiddenField ID="hf_id_pedido" runat="server" />
    <asp:HiddenField ID="hf_id_pedido_direccion_envio" runat="server" />
    <div class="container-md mt-4">
        <div class="row ">
            <div class="col">
                <h1 class="h2">Resumen de pedido #<asp:Literal ID="lt_numero_pedido" runat="server"></asp:Literal></h1>
            </div>
        </div>
        <div class="row ">
                <div class="col  col-xs-12  col-sm-6   col-md-6  col-lg-6">
                


                <div class="col">
                    <p class="h2">Contactos guardados</p>
                    <div class="alert alert-info" role="alert">
                        <i class="fas fa-info-circle"></i>
                        Usa uno de tus datos guardados previamente.
                    </div>
                    <asp:ListView ID="lv_contactos" OnItemCanceling="OnItemCanceling" OnItemUpdating="OnItemUpdating"  
                        OnItemEditing="OnItemEditing" runat="server">

                        <LayoutTemplate>
                            <div class="row ">
                                <div id="itemPlaceholder" runat="server"></div>

                            </div>


                        </LayoutTemplate>

                        <ItemTemplate>
                            <asp:HiddenField ID="hf_id_contacto" Value='<%#Eval("id") %>' runat="server" />
                            <div class="col col-sm-12 col-md-12 col-lg-12">
                                <div id='contentCard_DireccEnvio' class="card " runat="server">
                                    <div class="card-body">
                                        <h5 class="card-title"><%# Eval("nombre") %> <%# Eval("apellido_paterno") %> <%# Eval("apellido_materno") %></h5>
                                        <p class="card-text">
                                            Cel: <%# Eval("celular")%>
                                            <br />
                                            Tel: <%# Eval("telefono")%>
                                        </p>
                                          <asp:LinkButton  class="btn btn-sm   btn-outline-danger" OnClientClick="return confirm('Confirma que deseas ELIMINAR?');"
                                           
                                            OnClick="btn_eliminarContacto_Click" ID="btn_eliminarContacto" runat="server">
                                              <i class="fas fa-trash-alt"></i>


                                          </asp:LinkButton>
         <a class="btn btn-outline-secondary  text-dark bg-light" 

                            href='/usuario/cliente/editar/contacto/<%#Eval("id") %>?ref=<%= seguridad.Encriptar(hf_id_pedido.Value)%>'>Editar</a>
                    

                                        <asp:LinkButton ID="btn_usarDatos" OnClick="btn_usarDatos_Click"
                                            class="btn   btn-primary" runat="server">Usar estos datos</asp:LinkButton>

                                              <div id="msg_sucess" runat="server" visible="false" class="alert alert-success mt-2" role="alert">
                    
                    </div>

                                    </div>
                                </div>
                            </div>




                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:HiddenField ID="hf_id_contacto" Value='<%#Eval("id") %>' runat="server" />
                            <div class="col col-lg-12">
                                <div id='contentCard_DireccEnvio' class="card " runat="server">
                                    <div class="card-body">
                                        <h5 class="card-title"><%# Eval("nombre") %> <%# Eval("apellido_paterno") %></h5>
                                        <div class="row g-3">


                                           <div class="col-md-12">
                                                    <label>Nombre</label>
                                                <asp:TextBox ID="txt_edit_nombre" Text='<%# Eval("nombre") %>' class="form-control" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                                            </div>
                                           <div class="col-md-6">
                                                    <label >Apellido paterno</label>
                                                    <asp:TextBox ID="txt_edit_apellido_paterno" Text='<%# Eval("apellido_paterno") %>' class="form-control" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-6">
                                                     <label >Apellido materno</label>
                                                    <asp:TextBox ID="txt_edit_apellido_materno" Text='<%# Eval("apellido_materno") %>' class="form-control" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-6">
                                                     <label >Celular</label>
                                                    <asp:TextBox ID="txt_edit_celular" Text='<%# Eval("celular") %>' class="form-control" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                                                </div>

                                              <div class="col-md-6">
                                                     <label >Teléfono</label>
                                                    <asp:TextBox ID="txt_edit_telefono" Text='<%# Eval("telefono") %>' class="form-control" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                                                </div>

                                               <div id='content_alert_actualizar_contacto_<%#Eval("id") %>' class="mt-0 " ></div>
                                                <div class="col-md-6">

                                                <asp:LinkButton ID="btn_Cancelar"
                                                    class="btn btn-sm  " CommandName="Cancel" runat="server">Cancelar</asp:LinkButton>
                                                    
                                                <asp:LinkButton ID="btn_Actualizar"
                                                    class="btn btn-sm btn-primary" CommandName="Update" runat="server">Guardar</asp:LinkButton>
                                            </div>
                                    </div>
                                </div>
                                </div>
                            </div>

                        </EditItemTemplate>
                        <EmptyDataTemplate>
                            No hay datos de contacto guardados
                        </EmptyDataTemplate>
                    </asp:ListView>


                </div>

            </div>
           
            <div class="col  col-xs-12  col-sm-6   col-md-6  col-lg-6">
 


                <div class="d-grid gap-2 ">

                    <div class="col ">
                        <div class="row">
                            <div class="col">
                                <h1 class="h2">Agregar un contacto</h1>

                                <div class="alert alert-info" role="alert">
                                    <i class="fas fa-info-circle"></i>
                                    Agrega uno nuevo para usar en operaciones futuras
                                </div>


                            </div>
                        </div>
                        <div class="form-row">
                            <div class="form-group col-md-6">
                                <label for="<%=txt_add_nombre.ClientID %>">Nombre(s)</label>

                                <asp:TextBox ID="txt_add_nombre" ClientIDMode="Static" class="form-control" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                                <small id="emailHelp" class="form-text text-muted">Nombres</small>
                            </div>
                            <div class="form-group col-md-6">
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="form-group col-md-6 col-lg-5 col-xl-3">
                                <label for="<%=txt_add_apellido_paterno.ClientID %>">Apellido Paterno</label>
                                <asp:TextBox ID="txt_add_apellido_paterno" ClientIDMode="Static" class="form-control" data-length="50" MaxLength="50" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="form-group col-md-6 col-lg-5 col-xl-3">
                                <label for="<%= txt_add_apellido_materno.ClientID %>">Apellido Materno</label>
                                <asp:TextBox ID="txt_add_apellido_materno" ClientIDMode="Static" class="form-control" data-length="35" MaxLength="35" runat="server"></asp:TextBox>

                            </div>
                            <div class="form-group col-md-6 col-lg-5 col-xl-3">
                                <label for="txt_add_telefono">Teléfono local</label>
                                <asp:TextBox ID="txt_add_telefono" ClientIDMode="Static" class="form-control" data-length="35" MaxLength="35" runat="server"></asp:TextBox>

                            </div>
                            <div class="form-group col-md-6 col-lg-5 col-xl-3">
                                <label for="txt_add_celular">Celular</label>
                                <asp:TextBox ID="txt_add_celular" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div id="content_alert_crear_contacto" class="mt-0 " ></div>
                        <div class="form-group col-md-6 col-lg-5 col-xl-3 mt-2 ">
                            <asp:Button ID="btn_crear_contacto" class="btn btn-lg btn-primary" OnClick="btn_crear_contacto_Click" Text="Guardar" runat="server" />
                        </div>

                    </div>
   


                 

                </div>
            </div>
        
        </div>
    </div>
    <script>


</script>

</asp:Content>


