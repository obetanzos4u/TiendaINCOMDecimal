<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_asesores_modalidad_clientes_bar.ascx.cs" Inherits="uc_asesores_modalidad_clientes_bar" %>


<!-- Dropdown Structure -->
<div style="padding: 8px 10px;     float: left;"><a class=' btn blue btn-asesores' href='#' style="float: left; margin-right: 15px;" data-target='dropdown1'>Asesores</a>
</div>
<ul id='dropdown1' class='dropdown-content'>
    <li>
        <asp:HyperLink ID="btn_agregar_usuario"
            NavigateUrl="~/usuario/mi-cuenta/registro-de-usuario-asesor.aspx" runat="server">
    Registrar Cliente<i  style="line-height: 36px !important;" class="left material-icons">person_add</i>
        </asp:HyperLink></li>
    <li>
        <asp:HyperLink ID="link_"
            NavigateUrl="~/usuario/mi-cuenta/cotizaciones-busqueda.aspx" runat="server">
    Buscar operaciones asesores<i  style="line-height: 36px !important;" class="left material-icons">search</i>
        </asp:HyperLink></li>
   <!-- <li class="divider"></li>  
        <li><a href="#!">three</a></li>  
       <li><a href="#!"><i class="material-icons">view_module</i>Deshabilitado</a></li> -->

</ul>
<script>
    document.addEventListener('DOMContentLoaded', function () {

        var options = {
            constrainWidth : false,
            coverTrigger: true,
        
        };
    var elems = document.querySelectorAll('.btn-asesores');
    var instances = M.Dropdown.init(elems, options);
  });
</script>

<div class="switch" style="display: inline; float: left; padding: 8px 10px;  margin-right: 25px;">
    <label>
        Off
                <asp:CheckBox ID="chk_modalidad_asesores" AutoPostBack="true" OnCheckedChanged="chk_modalidad_asesores_CheckedChanged" runat="server" />
        <span class="lever"></span>
        On
    </label>
</div>
<asp:Label ID="lbl_cliente" runat="server" Text=""></asp:Label>
&nbsp;&nbsp;
<asp:HyperLink ID="myBtnCambiarAsesorModal" ClientIDMode="Static" Style="color: #84c7c1; cursor: pointer;" runat="server">CAMBIAR</asp:HyperLink>


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
