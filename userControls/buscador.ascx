<%@ Control Language="C#" AutoEventWireup="true" CodeFile="buscador.ascx.cs"  Inherits="menuPrincipal" %>

<div class=" buscador_container"  >
            <asp:TextBox ID="txt_buscadorProducto"  placeholder="Buscar" name="buscador" ClientIDMode="Static" autocomplete="some-random-string"   runat="server"></asp:TextBox>
 
 
 <asp:LinkButton ID="btn_buscarProductos" CssClass="btn_buscador" ClientIDMode="Static" ToolTip="Buscar"   OnClick="btn_buscarProductos_Click"  runat="server">
     <img alt="Icono de lupa o signo de búsqueda" class="icon_busqueda" src="../img/webUI/newdesign/search-icon.svg" />
 </asp:LinkButton>

</div>
 

<script>
    /* Script que ayuda a buscar al teclear la tecla "enter"  */
    var input = document.getElementById("txt_buscadorProducto");
    input.addEventListener("keyup", function (event) {
        event.preventDefault();
        if (event.keyCode === 13) {
            document.getElementById("btn_buscarProductos").click();
        }
    });

 
</script>
