<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_asesores_modalidad_clientes_bar.ascx.cs" Inherits="uc_asesores_modalidad_clientes_bar" %>

<!-- Dropdown Structure -->
<div class="user-menu">
    <%--<button type="button" id="btn-asesor" class="is-text-white is-bg-blue is-px-2 is-rounded-2xl" style="outline-width: 0; border: 0; cursor: pointer;" data-target="advisorDropdown">Asesor</button>--%>
    <asp:Label ID="lbl_modalidad_asesores" class="is-bg-blue is-text-white is-px-2 is-rounded" runat="server">
        <asp:CheckBox ID="chk_modalidad_asesores" AutoPostBack="true" Text="Modo asesor" CssClass="is-text-white" OnCheckedChanged="chk_modalidad_asesores_CheckedChanged" runat="server" />
    </asp:Label>
<%--    <label class="is-text-white is-bg-blue is-px-4 is-rounded-2xl">
    </label>--%>
    <ul style="list-style: none;">
        <li>
            <asp:HyperLink ID="myBtnCambiarAsesorModal" ClientIDMode="Static" Style="cursor: pointer;" runat="server">Cambiar</asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink ID="link_" NavigateUrl="~/usuario/mi-cuenta/cotizaciones-busqueda.aspx" runat="server">Buscar operaciones asesores</asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink ID="btn_agregar_usuario" NavigateUrl="~/usuario/mi-cuenta/registro-de-usuario-asesor.aspx" runat="server">Registrar Cliente</asp:HyperLink>
        </li>
        <li>
            <asp:CheckBox ID="chk_salir_modalidad_asesores" AutoPostBack="true" Text="Salir" CssClass="is-text-white" OnCheckedChanged="chk_modalidad_asesores_CheckedChanged" runat="server" />
        </li>
    </ul>
</div>
<%--<ul id="advisorDropdown" class='dropdown-content'>
    <li>
        <asp:HyperLink ID="btn_agregar_usuario" NavigateUrl="~/usuario/mi-cuenta/registro-de-usuario-asesor.aspx" runat="server">Registrar Cliente</asp:HyperLink>
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
    <div class="modal-content-incom">
        <span class="close">&times;</span>
        <div class="row">
            <div class="col s12 m12 l12">
                <h2>Selecciona un cliente</h2>
                <span>Solo teclea el usuario, email o nombre.</span>
            </div>
            <div class="col s12 m12 l12">
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
            console.log(data.id);
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
            console.log("false");
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
