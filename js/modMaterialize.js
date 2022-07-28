function cargarSliderModalListadoProductos(idContenedor, linkProducto) {
 
    
    var arr_imgProductos = document.querySelectorAll("." + idContenedor + "   img");
    console.log("." + idContenedor + "   img");
 

    var sliderProductosModal = document.getElementById("sliderProductosModal");
    var link_productoSlideShow = document.getElementById("link_productoSlideShow");

   

   
        $(".slick-track").empty();
    $('.slick').slick('slickRemove', false);
    $('.slick').slick('slickRemove', false);
    $('.slick').slick('slickRemove', false);
    $('.slick').slick('slickRemove', false);
    $('.slick').slick('slickRemove', false);
    $('.slick').slick('slickRemove', false);
    $('.slick').slick('slickRemove', false);
    $('.slick').slick('slickRemove', false);
    $('.slick').slick('slickRemove', false);
    $('.slick').slick('slickRemove', false);
    $('.slick').slick('slickRemove', false);




    link_productoSlideShow.setAttribute('href', linkProducto);

    sliderProductosModal.innerHTML = "";
    console.log(arr_imgProductos.length);
    var arr_imgProductosTXT = new Array(20);

    arr_imgProductos.forEach(function (img) {

        arr_imgProductosTXT.push(img.src);


    });

    var unique = arr_imgProductosTXT.filter((v, i, a) => a.indexOf(v) === i); 

    unique.forEach(function (img) {

        
       // Reemplaza la ruta de imagen chica a la imagen grande quitando el texto "min/ch/"
        img = img.replace("min/min-", "");
        console.log(img);
        sliderProductosModal.innerHTML += "<div class='content_SlickImg' style='visibility:hidden;'><a href='" + linkProducto + "'><img  class='IncomWebpToJpg'  src='" + img + "'/></a></div>";



 
    });


   

  

    var elem = document.querySelector("#modal_slideShow_productos");

    var instance = M.Modal.getInstance(elem);
    instance.open();

    $('.slick').slick('unslick');
   
    setTimeout(function () {

        $('.slick').slick({
            infinite: false,
            speed: 300,

            respondTo: 'slider',
            centerMode: true,
            adaptiveHeight: true
        });

        var divs = document.querySelectorAll(".content_SlickImg");

        [].forEach.call(divs, function (div) {
            
            div.style.visibility = "visible";
        });
         


         

    }, 500);



}

/* Script que ayuda a crear la operación al teclear la tecla "enter"  */
function btnEnter(selector_txt, selector_btn) {
    var txt = document.querySelector(selector_txt);
    var btn = document.querySelector(selector_btn);

     txt.addEventListener("keyup", function (event) {
        event.preventDefault();
        if (event.keyCode === 13) {
            btn.click();
        }
    });

}
// Función que inserta la barra de carga en el mismo nivel DOM del objeto que se llama ocultando este
function txtLoading(txt) {
    txt.hidden = true;
    var padre = txt.parentNode;
    var progress = document.createElement("div");
    progress.classList.add("progress");
    progress.innerHTML = `<div class="indeterminate"></div>`;
    padre.insertBefore(progress, txt);
}

// Función que inserta la barra de carga en el mismo nivel del objeto que llama ocultando este
function btnLoading(btn) {
    btn.style.display = "none";
    var padre = btn.parentNode;
    var progress = document.createElement("div");
    progress.classList.add("progress");
    progress.innerHTML = `<div class="indeterminate"></div>`;
    padre.insertBefore(progress, btn);
}

function btnLoadingHide(btn) {
    var idLoading = "loading"+ Math.floor((Math.random() * 100) + 1);
    btn.style.display = "none";
    var padre = btn.parentNode;
    var progress = document.createElement("div");
    progress.id = idLoading;
    progress.classList.add("progress");
    progress.innerHTML = `<div class="indeterminate"></div>`;
    padre.insertBefore(progress, btn);

    return idLoading;
}

function btnLoadingShow(btn, idLoading) {
    var elem = document.querySelector('#' + idLoading);
    console.log(elem);
    elem.parentNode.removeChild(elem);
    btn.style.display = "";
}
/*Script que ayuda activar un botón al presiontar enter en una caja de texto 
 * @param {htmlNode} input Especifica la caja de texto que escuchara el evento keyup (enter).
 * @param {htmlNode} btn Especifica el elemento en el que se emulará el clic.
 * */
function establecerEnter(input,btn) {
     
    input.addEventListener("keyup", function (event) {
        event.preventDefault();
        if (event.keyCode === 13) {
            btn.click();
        }
    });

}

/* Nos ayuda a sumar o restar el contenido de un input basandose en los botones de suma o resta.
 * @param {x}  Es la acción a realizar, valores permitidos ["suma"]["resta"]
 * @param {id} Es el id del input text al que se desea sumar/restar el valor numérico
 * */
function calculoTxtCarrito(x, id) {

    var txt = document.querySelector("#" + id);
    var txtValor = parseInt(txt.value);
    var resultado = 1;
    switch (x) {
        case "suma":
            resultado = txtValor + 1;

            break;
        case "resta":

            resultado = txtValor - 1;
            break;
    }

    if (resultado > 0) {
        txt.value = resultado;
    } else {
        txt.value = 1;
    }

}

function crear_toast(mensaje, _tipo) {


    let tipo = (_tipo === true);
    let estilo = "";
    let icon = "";
    if (tipo === true) { estilo = "green darken-2"; icon = "done"; }
    if (tipo === false) { estilo = "red"; icon = "clear"; }

    M.toast({html: '<i class=\"material-icons\">' + icon + '</i>' + mensaje , classes : estilo });

}