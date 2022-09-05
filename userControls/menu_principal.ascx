<%@ Control Language="C#" AutoEventWireup="true" CodeFile="menu_principal.ascx.cs" Inherits="menuPrincipal" %>

<nav class="is-max-w-full is-bg-white menuContainer">
    <ul id="contenedorMenu" class="is-m-0" runat="server">
    </ul>
</nav>
<script>
    const menuProductos = document.querySelector("#menuProductos");
    const menuProductosContenido = document.querySelector("#menuProductosContenido");
    menuProductos.addEventListener("mouseover", () => {
        menuProductosContenido.style.display = "block";
        //menuProductosContenido.style.height = "24rem"; // revisar si entra en clase
        //menuProductosContenido.style.overflow = "auto"; // revisar si entra en clase
    });
    //menuProductos.addEventListener("mouseout", () => {
    //    menuProductosContenido.style.transition = "all 18s";
    //    menuProductosContenido.style.display = "none";
    //});

    menuProductosContenido.childNodes.forEach((item) => {
        item.addEventListener("mouseover", () => {
            item.lastChild.style.display = "block";
        });
        item.addEventListener("mouseout", () => {
            item.lastChild.style.display = "none";
        })
    });
</script>
