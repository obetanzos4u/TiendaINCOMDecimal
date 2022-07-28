<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/herramientas/_masterConfiguraciones.master" Async="true" 
    CodeFile="cargar-bloqueo-productos-disponible.aspx.cs"
  Inherits="herramientas_cargar_bloqueo_productos_disponibles" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <h1 class="center-align">Agregar cantidad máxima de compra de producto a operaciones</h1>
    <div class="container">
        <div class="row">
              <div class="col s12 m12 l12"> <p>Solamente los productos en los que se indiquen, aplicará dicho bloqueo, si no se encuentran en este listado no 
                  se les aplicara un bloqueo de cantidades disponibles a agregar a carritos, cotizaciones y pedidos.</p>

            
                  <p><strong>Indicaciones: </strong>Debe ser un documento de excel, la hoja a cargar debe estar nombrada como: 
                      <strong>[ProductosCantidadMaxima]</strong> y las columnas deben ser en este orden    <strong> [numero_parte]</strong>, <strong>[DisponibleMaximo]</strong></p>
                    <asp:FileUpload ID="FileUpload1" class="form-control-file margin-b-4x" ClientIDMode="Static" runat="server" />
 
              <br />
                <asp:LinkButton ID="btn_cargar_listado_productos"
                    OnClick="btn_cargar_listado_productos_Click"   CssClass="waves-effect waves-light btn btn-s blue" runat="server">Eliminar y cargar registros</asp:LinkButton>

                
                <asp:LinkButton ID="btn_EliminarTodosLosRegistros"  CssClass="waves-effect waves-light btn btn-s red" OnClick="btn_EliminarTodosLosRegistros_Click" runat="server">
                    Eliminar todos los registros existentes
                </asp:LinkButton>
                
            </div>  
             <div class="col s12 l12">
                 <h2>Limitar desde SAP.</h2>
                 <p>Los productos ingresados en la siguiente caja de texto, se obtendra su stock disponible desde SAP y luego los ingresará como cantidad
                     máxima de compra. El listado ingresado aquí, debe existir tanto en Incom.mx y SAP
                    <br /> Ingrese el número de parte por fila.
                 </p>
                 <asp:TextBox ID="txt_ListadoProductosActualizarFromSAP" class="materialize-textarea"
                     TextMode="MultiLine" runat="server"></asp:TextBox>
                   <asp:LinkButton ID="btn_CargarYLimitarFromSAP"  CssClass="waves-effect waves-light btn btn-s blue"
                        OnClick="btn_CargarYLimitarFromSAP_Click" runat="server">
                   Cargar y limitar desdes SAP
                </asp:LinkButton>
             </div>
            <div class="col s12 l12">
                <h2>Log resultado operación</h2>
              
                <asp:TextBox ID="txt_log_result" TextMode="MultiLine" runat="server">

                </asp:TextBox>
                     </div>
        
    </div>

    </div>
</asp:Content>



