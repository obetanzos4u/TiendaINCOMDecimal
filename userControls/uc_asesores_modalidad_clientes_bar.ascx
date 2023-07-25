<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_asesores_modalidad_clientes_bar.ascx.cs" Inherits="uc_asesores_modalidad_clientes_bar" %>

<!-- Dropdown Structure -->
<div class="user-menu">
    <%--<button type="button" id="btn-asesor" class="is-text-white is-bg-blue is-px-2 is-rounded-2xl" style="outline-width: 0; border: 0; cursor: pointer;" data-target="advisorDropdown">Asesor</button>--%>
    <asp:Label ID="lbl_modalidad_asesores" class="is-bg-blue-darky is-text-white is-rounded-lg" style="padding: 0.25rem 1.5rem 0.5rem 1rem; margin-right: 2rem;" runat="server">
        <asp:CheckBox ID="chk_modalidad_asesores" class="checkbox_modalidad_asesores" AutoPostBack="true" Text="Modo asesor" OnCheckedChanged="chk_modalidad_asesores_CheckedChanged" runat="server" />
    </asp:Label>
    <%--    <label class="is-text-white is-bg-blue is-px-4 is-rounded-2xl">
    </label>--%>
    <ul id="modoAsesorUl" class="is-absolute is-text-black" style="list-style: none; padding: 0.5rem 1rem; line-height: 1.5;" runat="server">
        <li class="is-text-black">
            <asp:HyperLink ID="myBtnCambiarAsesorModal" ClientIDMode="Static" class="is-text-black" Style="cursor: pointer;" runat="server">Cambiar</asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink ID="link_" NavigateUrl="~/usuario/mi-cuenta/cotizaciones-busqueda.aspx" class="is-text-black" runat="server">Buscar operaciones</asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink ID="btn_agregar_usuario" NavigateUrl="~/usuario/mi-cuenta/registro-de-usuario-asesor.aspx" class="is-text-black" runat="server">Registrar cliente</asp:HyperLink>
        </li>
        <li>
            <asp:CheckBox ID="chk_salir_modalidad_asesores" AutoPostBack="true" Text="Salir" CssClass="is-text-black" OnCheckedChanged="chk_modalidad_asesores_CheckedChanged" runat="server" />
        </li>
    </ul>
</div>
<%--<ul id="advisorDropdown" class='dropdown-content'>
    <li>
        <asp:HyperLink ID="btn_agregar_usuario" NavigateUrl="~/usuario/mi-cuenta/registro-de-usuario-asesor.aspx" runat="server">Registrar cliente</asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="link_" NavigateUrl="~/usuario/mi-cuenta/cotizaciones-busqueda.aspx" runat="server">Buscar operaciones asesores</asp:HyperLink>
    </li>
</ul>--%>

<%--<div class="switch is-flex is-justify-center is-items-center is-px-4 borderTest">
    <p>Modo asesor: </p>
    <label>
        <asp:CheckBox ID="chk_modalidad_asesores" AutoPostBack="true" OnCheckedChanged="chk_modalidad_asesores_CheckedChanged" runat="server" />
        <span class="lever"></span>
    </label>
</div>--%>
<asp:Label ID="lbl_cliente" runat="server" Text=""></asp:Label>
&nbsp;&nbsp;


<div id="myModal" class="modal-incom">
    <!-- Modal content -->
    <div class="modal-content-incom is-rounded-lg" style="height: 400px">
        <span class="close">&times;</span>
        <div class="is-flex is-flex-col is-justify-center is-items-start">
            <h3>Selecciona el cliente</h3>
            <div class="is-w-full is-flex is-justify-between is-items-center">
                <span class="is-w-1_2">Puedes buscar por nombre o correo:</span>
                <select id="ddl_clientes_asesores" style="width: 100%" class="browser-default xxx templatingSelect2" name="clientes" runat="server"></select>
            </div>
        </div>
    </div>
</div>
<script>
    document.addEventListener('DOMContentLoaded', function () {
        var options = {
            constrainWidth: false,
            coverTrigger: true,
        };
        var elems = document.querySelectorAll('#btn-asesor');
        var instances = M.Dropdown.init(elems, options);
    });
</script>
<script type="text/javascript">
    document.addEventListener('DOMContentLoaded', function () {

        $('.xxx').on('select2:select', function (e) {
            var data = e.params.data;
            //console.log(data.id);
            __doPostBack('ddl_asesores', data.id.toString());
        });

        $('.xxx').select2({
            ajax: {
                delay: 250, // wait 250 milliseconds before triggering the request
                dataType: 'json',
                url: '<%=HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) %>/usuario/serviceAjax/obtenerClientes.aspx',
                data: function (params) {
                    var query = {
                        data: params.term,
                    }

                    // Query parameters will be ?search=[term]&type=public
                    return query;
                }, processResults: function (data, params) {
                    return {
                        results: data.results,
                    };
                },
                placeholder: 'Search for a repository',
                escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
                minimumInputLength: 2,
                templateResult: formatRepo,
                templateSelection: formatRepoSelection
            }
        });


        function formatRepo(repo) {
            if (repo.id) {
                console.log("true");
                return repo.text;
            }
            //console.log("false");
            var markup = "<div class='select2-result-repository clearfix'>" +
                "<div class='select2-result-repository__avatar'><img src='" + repo.owner.text + "' /></div>" +
                "<div class='select2-result-repository__meta'>" +
                "<div class='select2-result-repository__title'>" + repo.text + "</div>";
            return markup;
        }

        function formatRepoSelection(repo) {
            return repo.id || repo.text;
        }
    });
</script>

<style>
    /* The Modal (background) */
    .modal-incom {
        display: none; /* Hidden by default */
        position: fixed; /* Stay in place */
        z-index: 2; /* Sit on top */
        padding-top: 100px; /* Location of the box */
        left: 0;
        top: 0;
        width: 100%; /* Full width */
        height: 100%; /* Full height */
        overflow: auto; /* Enable scroll if needed */
        background-color: rgb(0,0,0); /* Fallback color */
        background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
    }

    /* Modal Content */
    .modal-content-incom {
        background-color: #fefefe;
        margin: auto;
        padding: 10px 20px;
        border: 1px solid #888;
        width: 80%;
        overflow: auto;
        color: black !important;
    }

    .checkbox_modalidad_asesores > input {
        position: relative !important;
        margin-top: 1rem;
    }

    #top_menuPricipal_barraAsesores_lbl_modalidad_asesores > label:nth-child(2) {
        color: #FFFFFF;
    }

    .user-menu span.is-text-black:nth-child(1) > label:nth-child(2) {
        color: #000000;
        font-size: 1rem;
    }

    .checkbox_modalidad_asesores {
        margin-top: 1rem;
    }

    .checkbox_modalidad_asesores > label:nth-child(2) { 
        color: #FFF;
    }

    #body_1 {
        fill: #FFFFFF;
    }

    /* The Close Button */
    .close {
        color: #aaaaaa;
        float: right;
        font-size: 28px;
        font-weight: bold;
    }

        .close:hover,
        .close:focus {
            color: #000;
            text-decoration: none;
            cursor: pointer;
        }
</style>

<script>
    document.addEventListener('DOMContentLoaded', function () {

        // Get the modal
        var modal = document.getElementById('myModal');

        // Get the button that opens the modal
        var btn = document.getElementById("myBtnCambiarAsesorModal");

        // Get the <span> element that closes the modal
        var span = document.getElementsByClassName("close")[0];

        // When the user clicks the button, open the modal 
        btn.onclick = function () {
            modal.style.display = "block";
        }

        // When the user clicks on <span> (x), close the modal
        span.onclick = function () {
            modal.style.display = "none";
        }

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == modal) {
                modal.style.display = "none";
            }
        }
    });
</script>
