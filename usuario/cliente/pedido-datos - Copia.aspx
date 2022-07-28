<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/bootstrap/basic.master" CodeFile="pedido-datos - Copia.aspx.cs" Inherits="usuario_cliente_pedido_datos" %>

<%@ Register Src="~/usuario/cliente/cliente-header.ascx" TagPrefix="header" TagName="menuGeneral" %>

<%@ Register TagPrefix="uc" TagName="ddlPaises" Src="~/userControls/ddl_paises.ascx" %>
<%@ Register TagPrefix="uc" TagName="ddlEstados" Src="~/userControls/ddl_estados.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="body">
    <header:menuGeneral ID="menuGeneral" runat="server" />


        <div class="container ">
        <br />
        <div class="row">
            <div class="btn-group">
                <asp:HyperLink ID="link_envio" runat="server"  class="btn btn-outline-primary " aria-current="page">
                    <span class="h4">Envío</span><br />
                    <span>Elige el método de envío</span>
                </asp:HyperLink>
                <asp:HyperLink ID="link_datos" runat="server"   class="btn btn-primary active " aria-current="page">
                    <span class="h4">Contacto</span><br />
                    <span>Establece los datos del pedido</span>
                </asp:HyperLink>
                <asp:HyperLink ID="link_pago" runat="server"   class="btn btn-outline-primary " aria-current="page">
                    <span class="h4">Pago</span><br />
                    <span>Selecciona el método de pago preferido</span>
                </asp:HyperLink>
            </div>

      
        </div>
    </div>





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



                <div class="shadow p-3 mb-5 bg-body rounded">
                    <p class="h4">Datos de contacto para este pedido.</p>
                    <strong>Nombre: </strong>
                    <asp:Label ID="lbl_nombre" runat="server"></asp:Label>
                    <asp:Label ID="lbl_apellido_paterno" runat="server"></asp:Label>
                    <asp:Label ID="lbl_apellido_materno" runat="server"></asp:Label>

                  
                    <br />
                    <strong>Celular: </strong>
                    <asp:Label ID="lbl_celular" runat="server"></asp:Label>
                      <br />
                    <strong>Télefono fijo: </strong>
                    <asp:Label ID="lbl_telefono" runat="server"></asp:Label>
                   
                <div class="d-grid gap-2 mt-4">
                      <p class="h5">Si tus datos son correctos, procede al pago</p>
                    <button class="btn btn-lg btn-success" type="button">Proceder al pago</button>
                </div>
            
                </div>


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
                    <p class="h4 d-none">Productos</p>


                    <asp:ListView ID="lv_productos" OnItemDataBound="lv_productos_ItemDataBound" Visible="false" runat="server">

                        <LayoutTemplate>
                            <table class="table">
                                <thead>
                                    <tr>

                                        <th scope="col">Producto</th>
                                        <th scope="col">Precio</th>
                                        <th scope="col">total</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <div id="itemPlaceholder" runat="server"></div>
                                </tbody>
                            </table>


                        </LayoutTemplate>

                        <ItemTemplate>

                            <td><%#Eval("numero_parte") %> -  <%#Eval("descripcion") %> </td>
                            <td>
                                <asp:Literal ID="lt_cantidad" Text='<%#Eval("cantidad") %>' runat="server"> </asp:Literal>
                                x
                            <asp:Literal ID="lt_precio_unitario" Text=' <%#Eval("precio_unitario") %>' runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="lt_precio_total" Text='<%#Eval("precio_total") %>' runat="server"></asp:Literal></td>

                        </ItemTemplate>
                        <EmptyDataTemplate>
                            No hay productos
                        </EmptyDataTemplate>
                    </asp:ListView>

                </div>
            </div>
            <div class="col  col-xs-12  col-sm-6   col-md-6  col-lg-6">
                <p class="h3 d-none">
                    Total Productos:  <strong>
                        <asp:Label ID="lbl_total_productos" runat="server"></asp:Label></strong>
                </p>




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
                                        <asp:LinkButton ID="btn_editarDirección_Click" CommandName="Edit" title="Editar"
                                            class="btn btn-sm btn-outline-secondary  text-dark bg-light"
                                            runat="server"><i class="fas fa-edit"></i></asp:LinkButton>

                                        <asp:LinkButton ID="btn_usarDatos" OnClick="btn_usarDatos_Click"
                                            class="btn   btn-primary" runat="server">Usar estos datos</asp:LinkButton>

                                        

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
           
        </div>
    </div>
    <script>


</script>

</asp:Content>


