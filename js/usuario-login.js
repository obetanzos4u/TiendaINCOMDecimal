async function onSignIn(googleUser) {
    let id_token = googleUser.getAuthResponse().id_token;
    let res = await LoginWithGoogle(id_token);
    const contentMsg = document.querySelector("#ajax-login-msg-result > p");

    if (res.result) {
        let countdown = 3;
        contentMsg.className = 'is-text-emerald is-text-semibold is-text-center is-select-none';
        setInterval(() => {
            contentMsg.textContent = "Inicio de sesión con Google exitoso, serás redirigido en " + countdown + " segundos.";
            if (countdown === 0 || countdown < 0) {
                location.reload();
            }
            countdown--;
        }, 1000);
    } else {
        contentMsg.textContent = res.message;
        contentMsg.className = 'is-text-red is-text-semibold is-text-center is-select-none';
    }
}
const loadJsLoginWithGoogle = () => {
    let head = document.getElementsByTagName('head')[0];
    let script = document.createElement('script');
    script.src = 'https://apis.google.com/js/platform.js';
    head.appendChild(script);
}

// Regístro con Google
async function SignWithGoogle(token) {
    let url = '/api/CrearUsuario';
    let data = { "AccessToken": token };

    try {
        let init = {
            // el método de envío de la información será POST
            method: "POST",
            body: JSON.stringify(data),
            headers: {
                'Content-Type': 'application/json'
            }
        };

        let response = await fetch(url, init
        );
        if (response.ok) {
            let res = JSON.parse(await response.json());
            return res;
        } else {
            throw new Error(response.statusText);
        }
    }
    catch (err) {
        var r = new Object();
        r.result = false;
        r.exception = true;
        r.message = err.message;
        console.log("Error al realizar la petición AJAX: " + err.message);
        return r;
    }
}

const signOut = () => {
    let auth2 = gapi.auth2.getAuthInstance();
    auth2.signOut();
}

async function LoginWithGoogle(token) {
    let url = '/api/LoginWithGoogle';
    let data = { "AccessToken": token };

    try {
        let init = {
            // el método de envío de la información será POST
            method: "POST",
            body: JSON.stringify(data),
            headers: {
                'Content-Type': 'application/json'
            }
        };

        let response = await fetch(url, init
        );
        if (response.ok) {
            let res = JSON.parse(await response.json());
            return res;
        } else {
            throw new Error(response.statusText);
        }
    }
    catch (err) {
        let r = new Object();
        r.result = false;
        r.exception = true;
        r.message = err.message;
        console.log("Error al realizar la petición AJAX: " + err.message);
        return r;
    }
}

async function LoginAjaxValidateLogin() {
    try {
        // en el objeto init tenemos los parámetros de la petición AJAX
        let init = {
            // el método de envío de la información será POST
            method: "POST"
        };

        let response = await fetch('/service/usuarios-login.ashx', init);

        if (response.ok) {

            let respuesta = await response.json();

            return respuesta;

        } else {
            throw new Error(response.statusText);
        }
    } catch (err) {
        var r = new Object();
        r.result = false;
        r.exception = true;
        r.message = err.message;

        console.log("Error al realizar la petición: " + err.message);
        return r;
    }
}

async function LoginAjaxCreateLoginBTN(ContentBtn) {
    let btnHTML = `
                    <a class="btn-small blue " onclick="LoginAjaxOpenModal();">Iniciar sesión</a>
                    <a class="btn-small blue" href="/registro-de-usuario.aspx">Registrarse</a>
                  `;
    ContentBtn.innerHTML = btnHTML;
}

// Crea un modal si este no se ha creado y lo abre
async function LoginAjaxOpenModal() {
    let modalContentLoginAjax = document.querySelector("#modalContentLoginAjax");

    if (modalContentLoginAjax === null) LoginAjaxCreateModalContent();

    let content = `
        <div class="modal-content">
            <div class="is-flex is-flex-col is-justify-center is-items-center">
                <h1 class="txt-inicio_sesion" style="font-size: 2rem; margin-top:2rem; text-align: center">Iniciar sesión</h1>
                <div class="is-text-center is-my-4">
                <p class="is-select-none is-font-regular">
                    Inicia sesión con tu cuenta de Google
                </p>
                <div
                    class="g-signin2 is-inline-block"
                    data-longtitle="true"
                    data-onsuccess="onSignIn"
                ></div>
                </div>
            </div>
            <div class="contain-separator">
                <span class="separator">O</span>
            </div>
            <div
                class="is-flex is-flex-col is-justify-center is-items-center is-mx-4 is-px-4">
                <p class="is-select-none is-font-regular">
                Continúa con tu correo electrónico
                </p>
                <div class="is-w-full is-text-center">
                <label for="ajax-login-user" class="is-block">Correo electrónico:</label>
                <input id="ajax-login-user" class="input-login_modal" type="text" />
                </div>
                <div class="is-w-full is-text-center">
                <label for="ajax-login-password" class="is-block">Contraseña:</label>
                <input id="ajax-login-password" class="input-login_modal" type="password" />
                </div>
                <div class="btn-1">
                <a
                    id="ajax-login-btn"
                    class="btn-inicio_sesion_modal is-select-none"
                    onclick="LoginAjaxSet();"
                    >Iniciar sesión</a
                >
                </div>
                <span class="is-my-4">
                    <a href="/restablecer-password.aspx">¿Olvidaste tu contraseña?</a>                
                </span>
                <span class="is-my-4">
                ¿Aún no tienes cuenta? <a href="/registro-de-usuario.aspx">Crea una cuenta</a>               
                </span>
            </div>
            <div id="ajax-login-msg-result" class="is-w-full">
                <p class="is-m-0"></p>
            </div>
        </div>
        `;

    modalContentLoginAjax = document.querySelector("#modalContentLoginAjax");
    modalContentLoginAjax.innerHTML = content;

    loadJsLoginWithGoogle();

    let elem = document.querySelector('#modalContentLoginAjax');
    let instances = M.Modal.init(elem, null);

    let instance = M.Modal.getInstance(elem);
    instance.open();
    btnEnter("#ajax-login-password", "#ajax-login-btn")
}

// Intenta realizar la sesión
async function LoginAjaxSet() {
    let user = document.querySelector("#ajax-login-user").value;
    let password = document.querySelector("#ajax-login-password").value;
    let formData = new FormData();

    let r = new Object();
    r.result = false;
    r.exception = true;
    r.message = "Error";

    formData.append('user', user);
    formData.append('password', password);

    await fetch('/service/usuarios-login.aspx', {
        method: 'POST',
        body: formData,
    })
        .then(function (response) {
            if (response.ok) {
                return response.text();
            } else {
                r.message = "Error en la llamada";
                throw "Error en la llamada";
            }
        })
        .then(function (texto) {
            r = JSON.parse(texto);
        })
        .catch(function (err) {
            r.message = err;
        });
    LoginAjaxTextResult(r);
}

const LoginAjaxTextResult = (r) => {
    let contentMsg = document.querySelector("#ajax-login-msg-result > p");
    let result = r.result;
    let exception = r.exception;
    let message = r.message;

    contentMsg.className = '';
    if (!exception) {
        if (result) {
            let countdown = 3;
            setInterval(() => {
                contentMsg.className = 'is-text-emerald is-text-semibold is-text-center is-select-none';
                contentMsg.textContent = "Inicio de sesión exitoso, serás redirigido en " + countdown + " segundos."
                if (countdown === 0 || countdown < 0) {
                    location.reload();
                }
                countdown--;
            }, 1000);
        } else {
            contentMsg.textContent = message;
            contentMsg.className = 'is-text-red is-text-semibold is-text-center is-select-none';
        }
    } else {
        contentMsg.textContent = message;
        contentMsg.className = 'is-text-red is-text-semibold is-text-center is-select-none';
    }
}

const LoginAjaxCreateModalContent = () => {
    let modalContent = document.createElement("div");
    modalContent.classList.add("modal");
    modalContent.id = "modalContentLoginAjax";
    let body = document.getElementsByTagName("body")[0];
    body.appendChild(modalContent);
}