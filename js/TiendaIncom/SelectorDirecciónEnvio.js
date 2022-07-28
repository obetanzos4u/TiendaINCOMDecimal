/* 
 202001013 CM - Establecido en el header para establecer la dirección de envío predeterminada
 */
document.addEventListener('DOMContentLoaded', function () {

    document.getElementById('OpenModalSelectorDireccionEnvio').addEventListener('click', function (e) { //say this is an anchor
        //do something
        e.preventDefault();
        OpenModalSelectorDireccionEnvio()

    });
});
function OpenModalSelectorDireccionEnvio() {


    var modalContentSelectorDireccionEnvio = document.querySelector("#modalContentSelectorDireccionEnvio");

    if (modalContentSelectorDireccionEnvio === null) SelectorDireccionEnvioCreateModalContent();


    var modalHtml = `
<!-- Modal Structure -->
 
    <div class="modal-content">
      <h3>Dirección de envío <a href="/usuario/mi-cuenta/crear-direccion-de-envio.aspx">Crear una</a></h4>
      <p>Establece tu dirección de envío predeterminada</p>
<div id="content_listado_direcciones_envio">
    </div>
    <div class="modal-footer">
      <a id="btn_GuardarDireccionEnvioPredeterminada" href="#!" class="modal-close waves-effect waves-green btn-flat">Cerrar</a>
    </div>
 
         `;



    modalContentSelectorDireccionEnvio = document.querySelector("#ModalDirecciónEnvioPredeterminada");


    modalContentSelectorDireccionEnvio.innerHTML = modalHtml;

 

    $('#ModalDirecciónEnvioPredeterminada').modal();

    $('#ModalDirecciónEnvioPredeterminada').modal('open');

    CargarDireccionesEnvio();

}

async function CargarDireccionesEnvio() {
    var r = new Object();
    r.result = false;
    r.exception = true;
    r.message = "Error";


    await fetch('/service/usuario-modo-asesor.aspx', {
        method: 'POST'

    })
        .then(function (response) {
            if (response.ok) {
                return response.text();

            } else {
                r.message = "Error en la llamada Ajax";
                throw "Error en la llamada Ajax";

            }

        })
        .then(function (texto) {
            // Si todo salió ok
            r = JSON.parse(texto);
        })
        .catch(function (err) {
            r.message = err;
        });


    if (r.exception !== true) {


        console.log(r);

       let idUsuario = r.response.id;

        var r = new Object();
        r.result = false;
        r.exception = true;
        r.message = "Error";


        await fetch('/api/DireccionesEnvio/' + idUsuario, {
            method: 'Get'

        })
            .then(function (response) {
                if (response.ok) {
                    return response.text();

                } else {
                    r.message = "Error en la llamada Ajax";
                    throw "Error en la llamada Ajax";

                }

            })
            .then(function (texto) {
                // Si todo salió ok
                r = JSON.parse(texto);
            })
            .catch(function (err) {
                r.message = err;
            });
        console.log(r);

        if (r.exception === false) {

            let row = ``;
   

            for (var i = 0; i < r.response.length; ++i) {
                var d = r.response[i];

                console.log(d.direccion_predeterminada);
                let tr = d.direccion_predeterminada === true ? "<tr style='background:#def3ff'>" : "<tr>" ;
                row += `${tr}
                                               
                                                    <td>
                                                        <a href="#" onclick="EstablerDireccionEnvioPredeterminada(${d.id})">
                                                         ${d.nombre_direccion} 
                                                        </a>
                                                    </td>
                                                    <td>
                                                        <a href="#" onclick="EstablerDireccionEnvioPredeterminada(${d.id})">
                                                          ${d.calle} ${d.numero} ${d.colonia} ${d.delegacion_municipio} ${d.estado}  ${d.codigo_postal}
                                                        </a>
                                                    </td>
                                               
                                        </tr>`

            }
            /*
            r.response.forEach(d => row += `<tr >
                                               
                                                    <td>
                                                        <a href="#" onclick="EstablerDireccionEnvioPredeterminada(${d.id})">
                                                         ${d.nombre_direccion} 
                                                        </a>
                                                    </td>
                                                    <td>
                                                        <a href="#" onclick="EstablerDireccionEnvioPredeterminada(${d.id})">
                                                          ${d.calle} ${d.numero} ${d.colonia} ${d.delegacion_municipio} ${d.estado}  ${d.codigo_postal}
                                                        </a>
                                                    </td>
                                               
                                        </tr>`);

            */
            let table = `   <table class="table highlight">
                            <thead>
                                <tr>

                                    <th scope="col">Nombre</th>
                                    <th scope="col">Dirección</th>
 
                                </tr>
                            </thead>
                            <tbody>
                                ${row}
                            </tbody>
                        </table>`;
 

            let content_listado_direcciones_envio = document.querySelector("#content_listado_direcciones_envio");

            content_listado_direcciones_envio.innerHTML = table;
            
        }

    }
}





    function SelectorDireccionEnvioCreateModalContent() {


        var modalContent = document.createElement("div");
        modalContent.classList.add("modal");
        modalContent.id = "ModalDirecciónEnvioPredeterminada";

        var body = document.getElementsByTagName("body")[0];
        body.appendChild(modalContent);

}


async function EstablerDireccionEnvioPredeterminada(idDireccion) {
    var r = new Object();
    r.result = false;
    r.exception = true;
    r.message = "Error";


    await fetch('/service/establecer-direccion-envio-preferida.aspx?idDireccion=' + idDireccion, {
        method: 'POST'

    })
        .then(function (response) {
            if (response.ok) {
                return response.text();

            } else {
                r.message = "Error en la llamada Ajax";
                throw "Error en la llamada Ajax";

            }

        })
        .then(function (texto) {
             
            r = JSON.parse(texto);
        })
        .catch(function (err) {
            r.message = err;
        });

 
  
    if (r.exception === false) {

        M.toast({ html: 'Actualizada con éxito' })
        window.location.href = window.location.pathname

 
    }
}
