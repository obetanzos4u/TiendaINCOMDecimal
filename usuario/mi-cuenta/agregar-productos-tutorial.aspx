<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeFile="agregar-productos-tutorial.aspx.cs" MasterPageFile="~/gnCliente.master" Inherits="usuario_mi_cuenta" %>

<%@ Register TagPrefix="uc" TagName="contactos" Src="~/userControls/uc_contactos.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">

    <div class="container z-depth-3">


        <div class="row center-align">
            <h1>1) Busca tu producto y presiona este botón</h1>
            <div class="col s12 m12 l12">
                <a class="waves-effect waves-light btn blue">
                    <i class="material-icons left">add</i> a operación 
                </a>
            </div>
            
            <div class="col s12 m12 l12"> <h1>2) Selecciona una cotización del listado y da clic en agregar!</h1>
               <img  src="../../img/webUI/ejemploEditarOperacion/agregar_a_operacion.jpg"/>
            </div>
            <div class="col s12 m12 l12"> <br /><br />
                <a class="btn-large green" href="/productos/">Navegar ahora</a>
                <br /><br />
                   </div>
        </div>
    </div>

</asp:Content>
