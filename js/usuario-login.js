async function onSignIn(googleUser) {
    var profile = googleUser.getBasicProfile();
    console.log('ID: ' + profile.getId()); // Do not send to your backend! Use an ID token instead.
    console.log('Name: ' + profile.getName());
    console.log('Image URL: ' + profile.getImageUrl());
    console.log('Email: ' + profile.getEmail()); // This is null if the 'email' scope is not present.

    var id_token = googleUser.getAuthResponse().id_token;
    console.log(id_token);

    var res = await LoginWithGoogle(id_token);

    console.log(res);

    let contentMsg = document.querySelector("#ajax-login-msg-result > p");
    if (res.result) {
        contentMsg.textContent = res.message + " - Redireccionando en 3 segundos...";
        contentMsg.className = 'green-text text-darken-2';

        setTimeout(function () { location.reload(); }, 1000);
    } else {
        contentMsg.textContent = res.message;
        contentMsg.className = 'red-text text-darken-1';
    }


}
function loadJsLoginWithGoogle() {
    var head = document.getElementsByTagName('head')[0];
    var script = document.createElement('script');
    script.src = 'https://apis.google.com/js/platform.js';
    head.appendChild(script);
}

// Regístro con Google
async function SignWithGoogle(token) {


    var url = '/api/CrearUsuario';
    var data = { "AccessToken": token };

    try {
        var init = {
            // el método de envío de la información será POST
            method: "POST",
            body: JSON.stringify(data),
            headers: {
                'Content-Type': 'application/json'
            }
        };

        var response = await fetch(url, init

        );
        if (response.ok) {

            var res = JSON.parse(await response.json());

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

function signOut() {
    var auth2 = gapi.auth2.getAuthInstance();
    auth2.signOut().then(function () {
        console.log('User signed out.');
    });
}
async function LoginWithGoogle(token) {


    var url = '/api/LoginWithGoogle';
    var data = { "AccessToken": token };

    try {
        var init = {
            // el método de envío de la información será POST
            method: "POST",
            body: JSON.stringify(data),
            headers: {
                'Content-Type': 'application/json'
            }
        };

        var response = await fetch(url, init

        );
        if (response.ok) {

            var res = JSON.parse(await response.json());

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

async function LoginAjaxValidateLogin(){

    try {
 
        // en el objeto init tenemos los parámetros de la petición AJAX
        var init = {
            // el método de envío de la información será POST
            method: "POST"
        };
 
        var response = await fetch('/service/usuarios-login.ashx', init);
        if (response.ok) {
 
            var respuesta = await response.json();
 
         return respuesta;
 
        } else {
            throw new Error(response.statusText);
        }
    } catch (err) {
        var r = new Object();
        r.result = false;
        r.exception = true;
        r.message = err.message;
        
        console.log("Error al realizar la petición AJAX: " + err.message);
        return r;
    }
    

}

async function LoginAjaxCreateLoginBTN(ContentBtn){

        let btnHTML = `
                    <a class="btn-small blue " onclick="LoginAjaxOpenModal();">Iniciar Sesión</a>
                    <a class="btn-small blue" href="/registro-de-usuario.aspx">Registrarse</a>
                  `;

        ContentBtn.innerHTML = btnHTML;

}

// Crea un modal si este no se ha creado y lo abre
async function LoginAjaxOpenModal() {


    var modalContentLoginAjax = document.querySelector("#modalContentLoginAjax");

    if (modalContentLoginAjax === null) LoginAjaxCreateModalContent();

    var content = `<div class="modal-content">
                       <div class="row"> 
                        <div class="col push-s1 s10 push-m2 m8 push-l3 l6 push-xl4 xl4"> 
                <p style="font-size:1.7rem;">Inicio de sesión</p>

                <div class="row center-align">
                    <p><strong>Inica sesion con tu cuenta de Google</strong></p>
                    <div class="g-signin2" style="display: inline-block;" data-longtitle="true" data-onsuccess="onSignIn"></div>
 
                        <p></p>
                    </div>
                </div>



                        <div class="row"> 
                            <div class="col s12 l12 xl12">
                                      <label for="ajax-login-user">Usuario</label>
                                <input id="ajax-login-user" type="text" />
                             </div>
                        </div>
                        <div class="row">
                            <div class="input-field col  s12 l12 xl12">
                                      
                                <input id="ajax-login-password" type="password" />  
                                <label for="ajax-login-password">Contraseña</label>
 
                            </div> 
                        </div> 
                        <div class="row">
                            <div class="col s12 l12 xl12 ">
                                <a  id="ajax-login-btn" class="btn blue" onclick="LoginAjaxSet();">Iniciar sesión</a>
                            </div>
                           
                            <div id="ajax-login-msg-result" class="col s12 ">
                                <p></p>
                            </div>
                            <div  class="col  ">
                                <p>
                                    <a href="/restablecer-password.aspx" >Olvidé mi contraseña</a>
                                    </p>
                            </div>
                              </div>
                        </div>
                    </div>
                   `;

    modalContentLoginAjax = document.querySelector("#modalContentLoginAjax");

 
    modalContentLoginAjax.innerHTML = content;

    loadJsLoginWithGoogle();


     var elem = document.querySelector('#modalContentLoginAjax');
    var instances = M.Modal.init(elem, null);

     var instance = M.Modal.getInstance(elem);
     instance.open();
    btnEnter("#ajax-login-password", "#ajax-login-btn")
}

// Intenta realizar la sesión
async function LoginAjaxSet() {

 
    let user = document.querySelector("#ajax-login-user").value;
    let password = document.querySelector("#ajax-login-password").value;

    var r  = new Object();
    r.result = false;
    r.exception = true;
    r.message = "Error";



    let formData = new FormData();
    formData.append('user', user);
    formData.append('password', password);

   
      await  fetch('/service/usuarios-login.aspx', {
          method: 'POST',
          body: formData,
           
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
              console.log(r);
            r = JSON.parse(texto);
        })
        .catch(function (err) {
            r.message = err;
        });

    LoginAjaxTextResult(r);
}
function LoginAjaxTextResult(r) {

    console.log(r);
    let contentMsg = document.querySelector("#ajax-login-msg-result > p");

    let result = r.result;
    let exception = r.exception;
    let message =r.message;


    contentMsg.className = '';
    if (!exception) {

        if (result) {
            contentMsg.textContent = message+ " - Redireccionando en 3 segundos...";
            contentMsg.className = 'green-text text-darken-2';

            setTimeout(function () { location.reload(); }, 1000);
        } else {
            contentMsg.textContent = message;
            contentMsg.className = 'red-text text-darken-1';
        }
        

    } else {

        contentMsg.textContent = message;
        contentMsg.className = 'red-text text-darken-1';

    }

}

function LoginAjaxCreateModalContent() {


    var modalContent = document.createElement("div");
    modalContent.classList.add("modal");
    modalContent.id = "modalContentLoginAjax";

    var body = document.getElementsByTagName("body")[0];
    body.appendChild(modalContent);

}