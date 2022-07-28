<%@ Page Language="C#" AutoEventWireup="true" Async="true" MasterPageFile="~/bootstrap/basic.master" CodeFile="pedido-facturacion.aspx.cs" 
    Inherits="usuario_cliente_pedido_facturacion" %>
<%@ Register Src="~/usuario/cliente/cliente-header.ascx" TagPrefix="header" TagName="menuGeneral" %>

<%@ Register TagPrefix="uc" TagName="ddlPaises"  Src="~/userControls/ddl_paises.ascx" %>
<%@ Register TagPrefix="uc" TagName="ddlEstados"  Src="~/userControls/ddl_estados.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="body">


    <header:menuGeneral ID="menuGeneral" runat="server" />


     


    <asp:HiddenField ID="hf_id_pedido"   runat="server" />
         <asp:HiddenField ID="hf_id_pedido_direccion_facturacion"   runat="server" />
    <div class="container-md ">
        <div class="row">
            <div class="col">
                <h1 class="h1">Facturación</h1>
                <h1 class="h2">para el pedido
                    <asp:Literal ID="lt_numero_pedido" runat="server"></asp:Literal></h1>
                <p>Establece el tipo de facturación, usa una de tus direcciones guardadas o agrega una nueva.</p>
            </div>
            <asp:Label ID="msg_alert" Visible="false" class="alert alert-warning" role="alert" runat="server">
                 
            </asp:Label>
                <asp:Label ID="msg_succes" Visible="false" class="alert alert-success" role="alert" runat="server">
                 
            </asp:Label>
        </div>



        <p><strong>Selecciona una dirección de facturación</strong></p>

  <div class="row">
  <div class="col">  <div class="row row-cols-1 row-cols-sm-1  row-cols-md-2  row-cols-lg-2  row-cols-xl-2">
           <div class="col mb-4">
            <div   class="card " runat="server"  >
 
                <div id="card_envio_recoge_en_tienda"  class="card-body">
                    <h5 class="card-title">Sin factura</h5>
                    <p class="card-text">
                     Si no requieres factura, selecciona esta opción.
                    </p>  <br />
     <div class="d-grid gap-2 mt-4">
                    <asp:LinkButton ID="btn_Sin_Factura"  OnClick="btn_Sin_Factura_Click"
                        class="btn btn-lg btn-primary" runat="server">NO requiero factura </asp:LinkButton>
              </div>
                </div>
            </div>
            </div>
    <asp:ListView ID="lv_direcciones" OnItemDataBound="lv_direcciones_ItemDataBound" runat="server">

        <LayoutTemplate>
        
                  
                        <div id="itemPlaceholder" runat="server"></div>
                   
            

          
        </LayoutTemplate>

        <ItemTemplate>
            <asp:HiddenField ID="hf_id_direccion" Value='<%#Eval("id") %>' runat="server" />
            <div class="col mb-4">
                <div id='contentCard_DireccFact' class="card " runat="server">
            
                    <div class="card-body">
                        <h5 class="card-title"><%# Eval("nombre_direccion") %></h5>
                        <p class="card-text">
                               <%# Eval("rfc") %> <br />   <%# Eval("razon_social") %><br />
                            <%# Eval("calle") %> <%# Eval("numero") %>, <%# Eval("colonia") %>,  <%# Eval("delegacion_municipio") %>,  <%# Eval("estado") %>   <%# Eval("codigo_postal") %>
                        </p>
                            <asp:LinkButton class="btn    btn-danger" OnClientClick="return confirm('Confirma que deseas ELIMINAR?');"
                            OnClick="btn_eliminarDireccion_Click" ID="btn_eliminarDireccion" runat="server">
                                              <i class="fas fa-trash-alt"></i>
                        </asp:LinkButton>
 <a class="btn btn-outline-secondary  text-dark bg-light" 

                            href='/usuario/cliente/editar/facturacion/<%#Eval("id") %>?ref=<%= seguridad.Encriptar(hf_id_pedido.Value)%>&numero_operacion=<%= lt_numero_pedido.Text%>'>
     Editar</a>
                    
                        <div class="d-grid gap-2 mt-2">
                            <asp:LinkButton ID="btn_usarDirección" OnClick="btn_usarDirección_Click"
                                class="btn btn-lg btn-primary" runat="server">Usar dirección</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>




        </ItemTemplate>
        <EmptyDataTemplate>
         <p class="h4">   No hay direcciones guardadas</p>
        </EmptyDataTemplate>
    </asp:ListView>
 
    </div>
      </div><div class="col">       <div class="row">
            <div class="col">
                <h1 class="h2">Agregar una dirección</h1>

            </div>
      </div>
          <div class="form-row">
              <div class="form-group col-md-6">
                  <label for="<%= txt_nombre_direccion.ClientID %>">Asigna un nombre a esta dirección </label>

                  <asp:TextBox ID="txt_nombre_direccion" ClientIDMode="Static" class="form-control" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                  <small id="emailHelp" class="form-text text-muted">Ejemplo: Casa, Trabajo, Bodega</small>
              </div>
              <div class="form-group col-md-6">
                  <label for="<%= txt_razon_social.ClientID %>">Razón social</label>
                  <asp:TextBox ID="txt_razon_social" ClientIDMode="Static" class="form-control" data-length="150" MaxLength="150" runat="server"></asp:TextBox>

              </div>
              <div class="form-group col-md-6">
                  <label for="<%= txt_rfc.ClientID %>">Ingresa un RFC </label>
                  <asp:TextBox ID="txt_rfc" ClientIDMode="Static" class="form-control" data-length="20" MaxLength="20" runat="server"></asp:TextBox>

              </div>
          </div>
          <div class="form-row">
              <div class="form-group col-md-3">
                  <label for="txt_codigo_postal">Código Postal</label>
                  <asp:TextBox ID="txt_codigo_postal" AutoPostBack="true" OnTextChanged="txt_codigo_postal_TextChanged" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox>
              </div>
              <div class="form-group col-md-6 col-lg-5 col-xl-3">
                  <label for="<%= txt_calle.ClientID %>">Calle</label>
                  <asp:TextBox ID="txt_calle" ClientIDMode="Static" class="form-control" data-length="50" MaxLength="50" runat="server"></asp:TextBox>
              </div>
              <div class="form-group col-md-6  col-lg-3 col-xl-2">
                  <label for="<%= txt_numero.ClientID %>">Número</label>
                  <asp:TextBox ID="txt_numero" ClientIDMode="Static" class="form-control" data-length="20" MaxLength="20" runat="server"></asp:TextBox>

              </div>
          </div>
          <div class="form-row">
              <div class="form-group col-md-6">
                  <label for="<%= txt_colonia.ClientID %>">Colonia</label>
                  <asp:DropDownList ID="ddl_colonia" Visible="false" class="form-select" runat="server"></asp:DropDownList>

                  <asp:TextBox ID="txt_colonia" ClientIDMode="Static" class="form-control" data-length="35" MaxLength="35" runat="server"></asp:TextBox>

              </div>
              <div class="form-group col-md-4">
                  <label for="txt_delegacion_municipio">Delegación/Municipio</label>
                  <asp:TextBox ID="txt_delegacion_municipio" ClientIDMode="Static" class="form-control" data-length="35" MaxLength="35" runat="server"></asp:TextBox>

              </div>
              <div class="form-group col-md-4">
                  <label for="txt_ciudad">Ciudad</label>
                  <asp:TextBox ID="txt_ciudad" ClientIDMode="Static" CssClass="validate" data-length="60" MaxLength="60" runat="server"></asp:TextBox>

              </div>
          </div>
          <div class="form-row">
              <div class="form-group col-md-4">
                  <label for="ddl_pais">Pais</label>
                  <uc:ddlPaises ID="ddl_pais" runat="server" />

              </div>
            <div id="cont_ddl_estado" class="form-group col-md-4" runat="server">
                <label for="ddl_municipio_estado">Estado</label>
                <uc:ddlEstados ID="ddl_estado" runat="server" />

            </div>
            <div id="cont_txt_estado" class="form-group col-md-4" runat="server">
                  <label for="txt_estado">Estado</label>
                <asp:TextBox ID="txt_estado" class="form-control" ClientIDMode="Static" data-length="35" MaxLength="35" runat="server"></asp:TextBox>
              
            </div>

        </div>
 
        <div id="content_alert"></div>
        <asp:Button ID="btn_crear_direccion" class="btn btn-primary mt-4" OnClick="btn_crear_direccion_Click" Text="Guardar" runat="server" />


    </div></div>
   
        <script>
       
             
        </script>
 
</asp:Content>


