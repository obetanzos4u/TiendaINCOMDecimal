<%@ Page Language="C#" AutoEventWireup="true" Async="true" MasterPageFile="~/general.master" CodeFile="completar-datos.aspx.cs" Inherits="_blank_" %>


<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <div class="container">
        <div class="row">
            <div  class="col s12 m12 l6 xl4 center-align">
               <h1 >Completa los siguientes datos</h1>
          
                <h2 class="red-text">A ingresa algún número telefónico para continuar con tu operación</h2>

                         </div>
         
                <asp:UpdatePanel ID="up_contacto" UpdateMode="Always" class="col s12 " runat="server">
                    <contenttemplate>
                        <p>
                            <span>Teléfono celular</span>
                            <asp:TextBox ID="txt_celular" AutoPostBack="true" style="width: 250px;     display: block;" OnTextChanged="txt_celular_TextChangedAsync"
                                ClientIDMode="Static" placeholder="Teléfono celular" runat="server">
                            </asp:TextBox>
                        </p>
                        <p>
                            <span>Teléfono fijo/oficina</span>
                            <asp:TextBox ID="txt_telefono_fijo" AutoPostBack="true" style="width: 250px;display: block;" OnTextChanged="txt_telefono_fijo_TextChangedAsync"
                                ClientIDMode="Static" placeholder="Teléfono fijo/oficina" runat="server">
                            </asp:TextBox>
                        </p>

                        <p>

                            <asp:Label ID="lbl_text_result_saved_tel" runat="server"></asp:Label>

                        </p>
                    </contenttemplate>
                </asp:UpdatePanel>
           
        </div>
        <div class="row">
            <a href="mi-carrito.aspx" class="btn btn-s blue waves-effect waves-light  ">Regresar a carrito</a>
        </div>
    </div>
          <asp:HiddenField ID="hf_UserEmail"  runat="server" />

</asp:Content>
