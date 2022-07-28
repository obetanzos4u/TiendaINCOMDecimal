
function fileUpload(selector, contentMsg) {

var file = document.querySelector(selector);

file.onchange = function () {
if (file.files.length > 0) {
    document.querySelector(contentMsg).innerHTML = file.files[0].name;

}
};
}

document.addEventListener('DOMContentLoaded', () => {

    (document.querySelectorAll('.notification .delete') || []).forEach(($delete) => {
        $notification = $delete.parentNode;
        $delete.addEventListener('click', () => {
            $notification.parentNode.removeChild($notification);
        });
    });



    // Elima los mensajes del tipo Message creados mediante la función  BulmaMessage();

    (document.querySelectorAll('.message > div > a') || []).forEach(($delete) => {
        $notification = $delete.parentNode;

        console.log($notification.parentNode);

        $delete.addEventListener('click', () => {
            console.log($notification.parentNode);
            $notification.parentNode.remove();
        });
    });

});


// 20200626 - Recibe el selector donde se insertará el mensaje, el tipo de mensaje y el mensaje.
function BulmaMessage(contentSelector, type, message) {
    console.log(type);
    console.log(message);
    let html = null;
    let Success =`
                <article class="message is-success">
                    <div class="message-header">
                        <p>Se ha realizado con éxito</p>
                        <a class="delete" aria-label="delete"></a>
                    </div>
                    <div class="message-body">
                          ${message}
                     </div>
                </article>
    `;

    let Warning = `
                <article class="message is-warning">
                  <div class="message-header">
                    <p>Advertencia</p>
                    <a class="delete" aria-label="delete"></a>
                  </div>
                  <div class="message-body">
                    ${message}
                  </div>
                </article>
            `;

    let Danger = `
                    <article class="message is-danger">
                      <div class="message-header">
                        <p>Error</p>
                        <a class="delete" aria-label="delete"></a>
                      </div>
                      <div class="message-body">
                           ${message}
                      </div>
                    </article>
                    `;

    switch (type) {
        case "success" : html = Success; break;
        case "danger" : html = Danger; break;
        case "warning" : html = htmlbreak; break;
    }


    document.querySelector(contentSelector).innerHTML += html;

}