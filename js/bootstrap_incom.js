
document.addEventListener("DOMContentLoaded", function (event) {
    BootstrapCheckBoxRenderWebforms() 
});
// 20200626 CM - Recibe el selector donde se insertará el mensaje, el tipo de mensaje y el mensaje.
function BootstrapAlert(contentSelector, type, title, message) {
    let html = null;

    let btn_close = ` <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close">
        <span aria-hidden="true">&times;</span>
    </button>`



    let primary = `<div class="alert alert-primary alert-dismissible fade show" role="alert">
    <h4 class="alert-heading">${title}</h4>
    ${message}${btn_close}
</div>`;

    let secondary = `<div class="alert alert-secondary alert-dismissible fade show" role="alert">
     <h4 class="alert-heading">${title}</h4>${message}${btn_close}
</div >`;

    let success = `<div class="alert alert-success alert-dismissible fade show" role="alert">
    <h4 class="alert-heading">${title}</h4> ${message}${btn_close}
</div>`

    let danger = `<div class="alert alert-danger alert-dismissible fade show" role="alert">
    <h4 class="alert-heading">${title}</h4> ${message}${btn_close}
</div>`;

    let warning = `<div class="alert alert-warning alert-dismissible fade show" role="alert">
     <h4 class="alert-heading">${title}</h4>${message}${btn_close}
</div>`;

    let info = `<div class="alert alert-info alert-dismissible fade show" role="alert">
     <h4 class="alert-heading">${title}</h4>${message} ${btn_close}
</div >`;

    let light = `<div class="alert alert-light" role="alert">
    <h4 class="alert-heading">${title}</h4> ${message} ${btn_close}
</div>`;

    let dark = `<div class="alert alert-dark alert-dismissible fade show" role="alert">
    <h4 class="alert-heading">${title}</h4> ${message} ${btn_close}
</div > `



    switch (type) {
        case "primary": html = primary; break;
        case "secondary": html = secondary; break;
        case "success": html = success; break;
        case "danger": html = danger; break;
        case "warning": html = warning; break;
        case "info": html = info; break;
        case "light": html = light; break;
        case "dark": html = dark; break;
    }
    //console.log(contentSelector);
    //console.log(document.querySelector(contentSelector));
    document.querySelector(contentSelector).innerHTML += html;
}

// 20200626 CM - Crea una animación de espera en el mismo boton y lo deshabilita.  

function BootstrapClickLoading(btn) {

    
    btn.innerHTML = `
                     <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                      Procesando...
                          `;
    btn.classList.add("disabled");
    btn.setAttribute("disabled", "");
}


// 20200626 CM - Script que ayuda a crear la operación al teclear la tecla "enter"  
function BootstrapTxtAction(selector_txt, selector_btn) {
    var txt = document.querySelector(selector_txt);
    var btn = document.querySelector(selector_btn);

    txt.addEventListener("keyup", function (event) {
        event.preventDefault();
        if (event.keyCode === 13) {
            btn.click();
        }
    });

}

// 20210423 CM - Corrige la visualización para las etiquetas  <asp:CheckBox

function BootstrapCheckBoxRenderWebforms() {
    let NameClassCheck = "form-check-input";
    var span = document.querySelectorAll("span." + NameClassCheck);

    Object.keys(span).forEach(key => {
        span[key].removeAttribute("class")
        var chk = span[key].children[0];
        console.log(chk);
        chk.setAttribute("class", NameClassCheck);
       
    });
}