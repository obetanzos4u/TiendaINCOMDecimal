<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_footerTienda.ascx.cs" Inherits="uc_footerTienda" %>

<footer>
    <div class="is-bg-footer">
        <div class="is-grid is-col-footer is-gap-4 is-text-white is-px-8">
            <div class="is-flex is-flex-col is-justify-center is-items-start borderTest">
                <p class="is-text-xl is-font-bold">Contáctanos</p>
                <p class="is-text-lg is-font-semibold">Llámanos al <a href="tel:5552436900">(55) 5243 - 6900</a></p>
                <div class="is-text-xs">
                    <p class="is-m-0">Plutarco Elías Calles 276, colonia Tlazintla, C.P. 08710, Iztacalco, Ciudad de México.</p>
                    <p class="is-m-0">Horario de atención: Lunes a Jueves de 8:00 a 19:00 hrs • Viernes de 8:00 a 17:00 hrs</p>
                </div>
                <div class="is-flex borderTest">
                    <a href="https://www.facebook.com/incommexico/" target="_blank">
                        <img title="Facebook INCOM" alt="Facebook INCOM" loading="lazy" src="/img/webUI/rs/Facebook.svg" />
                    </a>
                    <a href="https://www.facebook.com/incommexico/" target="_blank">
                        <img title="Instagram INCOM" alt="Instagram INCOM" loading="lazy" src="/img/webUI/rs/Instagram.svg" />
                    </a>
                </div>
                <asp:HyperLink ID="link_contacto" ToolTip="Contácto" CssClass="btn blue waves-effect waves-light" href="/informacion/ubicacion-y-sucursales.aspx#contacto" runat="server">Contáctanos</asp:HyperLink>
            </div>
            <div class="is-flex is-flex-col is-justify-center is-items-center">
                <p>Suscríbete a nuestro boletín:</p>
                <input type="email" id="newsletter_email" name="newsletter_email" />
                <button type="submit">Registro</button>
            </div>
            <div>
                <h3 class="is-text-sm">Información</h3>
                <ul>
                    <li>
                        <a title="Aviso de Privacidad" href="/informacion/aviso-de-privacidad.aspx" target="_blank">Aviso de Privacidad</a>
                    </li>
                    <li>
                        <a title="Devoluciones y Garantía" href="/informacion/devoluciones-y-garantias.aspx" target="_blank">Devoluciones y Garantía</a>
                    </li>
                    <li>
                        <a title="Términos y Condiciones de compra" href="/informacion/terminos-y-condiciones-de-compra.aspx" target="_blank">Términos y condiciones de compra</a>
                    </li>
                    <li>
                        <a title="Ubicación y Sucursales" href="/informacion/ubicacion-y-sucursales.aspx" target="_blank">Ubicación</a>
                    </li>
                    <li>
                        <a title="Cuentas bancarias" href="/informacion/cuentas-bancarias.aspx" target="_blank">Cuentas Bancarias</a>
                    </li>
                    <li>
                        <a title="Quejas,sugerencias,denuncias y felicitaciones" href="/informacion/denuncias-incom.aspx" target="_blank">Quejas y sugerencias</a>
                    </li>
                    <li>
                        <a title="Código de ética" href="/documents/CODIGO_DE_ETICA.pdf" target="_blank">Código de ética</a>
                    </li>
                    <li>
                        <a title="Calendario 2022" href="/documents/CALENDARIO_INCOM_2022.pdf" target="_blank">Calendario 2022</a>
                    </li>
                </ul>

                <div>
                    <h3>Síguenos en nuestras redes sociales</h3>
                    <div>
                        <a href="https://www.facebook.com/incommexico/" target="_blank">
                            <img class="responsive-img imagesWebpPng" width="32" title="Facebook Incom" alt="Facebook Incom" loading="lazy" src="/img/webUI/rs/facebook.webp" /></a>
                        <a href="https://www.instagram.com/incom_mx/" target="_blank">
                            <img class="responsive-img imagesWebpPng" width="32" title="Instagram Incom" alt="Instagram Incom" loading="lazy" src="/img/webUI/rs/instagram.webp" /></a>
                        <a href="https://twitter.com/incom_mx" target="_blank">
                            <img class="responsive-img imagesWebpPng" width="32" title="Twitter Incom" alt="Twitter Incom" loading="lazy" src="/img/webUI/rs/twitter.webp" />
                        </a>
                        <a href=" https://www.youtube.com/user/incommx" target="_blank">
                            <img class="responsive-img imagesWebpPng" title="Youtube Incom" alt="Youtube Incom" loading="lazy" width="32" src="/img/webUI/rs/youtube.webp" />
                        </a>
                    </div>
                </div>
            </div>
        </div>

        <div>
            <div>
                <div>
                    <div>
                        Incom ® | Hecho en México por  IT4U Development   <%= DateTime.Now.Year %> | Todos los derechos reservados.
                        <a href="#!">More Links aquí</a>
                    </div>
                </div>
            </div>
        </div>
</footer>
