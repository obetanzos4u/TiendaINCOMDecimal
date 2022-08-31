<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_footerTienda.ascx.cs" Inherits="uc_footerTienda" %>

<footer class="is-text-white">
        <div class="is-px-xl is-py-2-mx is-py-2 is-bg-footer is-text-white">
            <div class="is-grid-xl is-col-footer is-gap-4 is-px-8-sm">
                <div class="is-flex-xl is-flex-col-xl is-justify-start is-items-start">
                <p class="is-text-xl is-font-semibold">Contáctanos</p>
                <p class="is-text-lg is-font-medium">Llámanos al <a href="tel:5552436900">(55) 5243 - 6900</a></p>
                <div class="is-font-light is-text-sm">
                    <p class="is-m-0">Plutarco Elías Calles 276, colonia Tlazintla, C.P. 08710, Iztacalco, Ciudad de México.</p>
                    <p class="is-m-0">Horario de atención: Lunes a Jueves de 8:00 a 19:00 hrs • Viernes de 8:00 a 17:00 hrs.</p>
                </div>
                <div class="is-flex is-justify-start is-items-center is-w-1_2 is-py-4">
                    <a href="https://www.facebook.com/incommexico/" target="_blank" class="is-w-6">
                        <img title="Facebook Incom" alt="Facebook Incom" loading="lazy" src="/img/webUI/rs/Facebook.svg" class="is-w-6"/></a>
                    <a href="https://www.instagram.com/incom_mx/" target="_blank" class="is-w-6 is-space-x-6">
                        <img title="Instagram Incom" alt="Instagram Incom" loading="lazy" src="/img/webUI/rs/Instagram.svg" class="is-w-6"/></a>
                    <a href="https://twitter.com/incom_mx" target="_blank" class="is-w-6 is-space-x-6">
                        <img title="Twitter Incom" alt="Twitter Incom" loading="lazy" src="/img/webUI/rs/Twitter.svg" class="is-w-6"/>
                    </a>
                    <a href="https://www.youtube.com/user/incommx" target="_blank" class="is-w-6 is-space-x-6">
                        <img title="Youtube Incom" alt="Youtube Incom" loading="lazy" src="/img/webUI/rs/Youtube.svg" class="is-w-6"/>
                    </a>
                    <a href="https://www.linkedin.com/company/incom-mx" target="_blank" class="is-w-6 is-space-x-6" >
                        <img title="LinkedIn Incom" alt="LinkedIn Incom" loading="lazy" src="/img/webUI/rs/LinkedIn.svg" class="is-w-6"/>
                    </a>
                     <a href="https://www.tiktok.com/@incom_mx" target="_blank" class="is-w-6 is-space-x-6">
                        <img title="Tiktok Incom" alt="Tiktok Incom" loading="lazy" src="/img/webUI/rs/Tiktok.svg" class="is-w-6"/>
                    </a>
                </div>
            </div>
            <div class="is-flex is-flex-col is-justify-start is-items-start">
                <p class="is-mx-1 is-text-xl is-font-bold">Regístrate a nuestro boletín:</p>
                <div class="is-mx-1 is-flex is-justify-center is-items-center is-w-full-xl is-px-4">
                    <input type="email" id="newsletter_email" name="newsletter_email" placeholder="Correo electrónico" style="background-color: rgb(255, 255, 255); color: rgb(0, 0, 0); padding-left: 0.5rem; height: 2rem; margin: 0; border-radius: 0.4rem 0rem 0rem 0.4rem" />
                    <button type="submit" class="is-text-white is-rounded" style="background-color: #004EEA; padding: 0.5rem 1.5rem; height: 100%; border: 0; border-radius: 0rem 0.4rem 0.4rem 0rem;">Registro</button>
                </div>
            </div>
            <div class="is-flex-xl is-justify-center-xl is-font-normal is-text-base">
                <ul>
                    <li>
                        <a title="Términos y Condiciones de compra" href="/informacion/terminos-y-condiciones-de-compra.aspx" target="_blank" class="is-text-white">Términos y condiciones de compra</a>
                    </li>
                    <li>
                        <a title="Devoluciones y Garantía" href="/informacion/devoluciones-y-garantias.aspx" target="_blank" class="is-text-white">Garantías y devoluciones</a>
                    </li>
                    <li>
                        <a title="Quejas,sugerencias,denuncias y felicitaciones" href="/informacion/denuncias-incom.aspx" target="_blank" class="is-text-white">Quejas y sugerencias</a>
                    </li>
                    <li>
                        <a title="Aviso de Privacidad" href="/informacion/aviso-de-privacidad.aspx" target="_blank" class="is-text-white">Aviso de privacidad</a>
                    </li>
                    <li>
                        <a title="Cuentas bancarias" href="/informacion/cuentas-bancarias.aspx" target="_blank" class="is-text-white">Cuentas bancarias</a>
                    </li>
                    <li>
                        <a title="Código de ética" href="/documents/CODIGO_DE_ETICA.pdf" target="_blank" class="is-text-white">Código de ética</a>
                    </li>
                    <%--<li>
                        <a title="Ubicación y Sucursales" href="/informacion/ubicacion-y-sucursales.aspx" target="_blank">Ubicación</a>
                    </li>--%>
                    <li>
                        <a title="Calendario 2022" href="/documents/CALENDARIO_INCOM_2022.pdf" target="_blank" class="is-text-white">Calendario 2022</a>
                    </li>
                </ul>
            </div>
        </div>
        <div class="is-flex is-justify-center is-items-center">
            <p class="is-text-sm">Incom &reg; | Hecho en México por  IT4U Development   <%= DateTime.Now.Year %> | Todos los derechos reservados.</p>
        </div>
</footer>
