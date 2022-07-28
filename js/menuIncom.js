// 202001016 - Carlos Miranda
var IcoAbrir = "arrow_drop_down";
var IcoCerrar = "clear";


function OcultarMenuPrincipalMarcas() {
    $('#menu_ico_marcas').html(IcoAbrir);
    $('#contenedorSubMenuMarcas').hide();
}

function MostrarMenuPrincipalMarcas() {
    $('#menu_ico_marcas').html(IcoCerrar);
    $('#contenedorSubMenuMarcas').show();
}

function OcultarMenuPrincipalCategorias() {
    $('#menu_ico_productos').html(IcoAbrir);
    $('li.menu-categorias').children().next().hide();
}

function MostrarMenuPrincipalCategorias() {
    $('#menu_ico_productos').html(IcoCerrar);
    $('li.menu-categorias').children().next().show();
}



       


    // Crea el menú Incom
    function menuIncom() {

        $('li.menu-categorias').on({
            click: function () {

                if ($('#menu_ico_productos').html() == "clear") {
                    OcultarMenuPrincipalCategorias();
                  
                } else {
                    MostrarMenuPrincipalCategorias();
                    OcultarMenuPrincipalMarcas();
                }

            }

        });


        // Inicio trigger btn desplegable
        $('a#menu-incom-marcas-btn').on({
            click: function () {

                if ($('#menu_ico_marcas').html() === "clear") {
                    OcultarMenuPrincipalMarcas();
                } else {
                    MostrarMenuPrincipalMarcas();
                    OcultarMenuPrincipalCategorias();
                }


            }
        });
        // Fin   trigger btn desplegable


        // INICIO Buscador en listado de marcas

        var input = document.querySelector('#txt_buscadorMenuMarcas');
        input.onkeyup = function () {
            var filter = input.value.toUpperCase();
            var lis = document.querySelectorAll(".menu-incom-marcas-list > li > a");
            for (var i = 0; i < lis.length; i++) {

                var name = lis[i].innerHTML;

                if (name.toUpperCase().includes(filter))
                    lis[i].style.display = 'list-item';
                else lis[i].style.display = 'none';
            }
        }
        // FIN Buscador en listado de marcas

















        // Los elementos del menú
        const elements = document.querySelectorAll(".menu-incom-marcas-list > li");


        // Recorremos todos los elementos del menú
        for (let i = 0; i < elements.length; i++) {
            elements[i].addEventListener("mouseenter", function() {

                // INICIO - Quita la clase agregada al hover para establecer todos en "blanco"
                for (var x = 0; x < elements.length; x++) {
                    var c = "menu-incom-marcas-list-element-hover";
                    var e = elements[x];

                    e.className = e.className.replace(new RegExp('(?:^|s)' + c + '(?!S)'), '');
                }
                // FIN - Quita la clase agregada al hover para establecer todos en "blanco"

                // Añadimos una clase de sombreado para que se quede como elemento "activo"
                elements[i].className = "menu-incom-marcas-list-element-hover";

                // Obtenemos el valor del atributo que este contiene el ID de la div a mostrar
                var atributo = elements[i].getAttribute("data-active");

                // obtenemos todos los contenidos y los vamos ocultando
                var contens = document.querySelectorAll(".menu-incom-marcas-categorias-list");
                for (var x = 0; x < contens.length; x++)
                    contens[x].style.display = 'none';

               

                // Una vez ocultados todos los contenidos, mostramos únciamente el que nos interesa
                var div = document.querySelector("#" + atributo);
                div.style.display = "block";

            });
        }

    }