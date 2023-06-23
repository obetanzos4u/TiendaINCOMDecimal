<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="soplado_fremco_2022" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="utf-8">
    <meta content="width=device-width, initial-scale=1.0" name="viewport">
    <title>INCOM</title>
    <meta content="Landing page del webinar de soplado de fibra óptica impartido en INCOM por FREMCO" name="description">
    <meta content="webinar soplado fibra optica fremco incom" name="keywords">
    <link href="assets/img/favicon.png" rel="icon">
    <link href="assets/img/apple-touch-icon.png" rel="apple-touch-icon">
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Montserrat:300,300i,400,400i,500,500i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i" rel="stylesheet">
    <link href="assets/vendor/aos/aos.css" rel="stylesheet">
    <link href="assets/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
    <link href="assets/vendor/bootstrap-icons/bootstrap-icons.css" rel="stylesheet">
    <link href="assets/vendor/boxicons/css/boxicons.min.css" rel="stylesheet">
    <link href="assets/vendor/glightbox/css/glightbox.min.css" rel="stylesheet">
    <link href="assets/vendor/remixicon/remixicon.css" rel="stylesheet">
    <link href="assets/vendor/swiper/swiper-bundle.min.css" rel="stylesheet">
    <link href="assets/css/style.css" rel="stylesheet">
</head>
<body>

    <!-- ======= Header ======= -->
    <header id="header" class="fixed-top d-flex align-items-center header-bg-color">
        <div class="container d-flex align-items-center justify-content-between">

            <div class="logo">
                <a href='<%= ResolveUrl("https://www.incom.mx") %>'>
                    <img src="assets/img/incom.png" alt="INCOM La ferretera de las telecomunicaciones" class="img-fluid"/>
                </a>
                <!-- Uncomment below if you prefer to use an image logo -->
                <!-- <a href="index.html"><img src="assets/img/logo.png" alt="" class="img-fluid"></a>-->
            </div>

            <nav id="navbar" class="navbar">
                <ul>
                    <li><a class="nav-link scrollto active" href="#hero">Inicio</a></li>
                    <li><a class="nav-link scrollto" href="#team">Ventajas</a></li>
                    <li><a class="nav-link scrollto" href=" #features">Características</a></li>
                    <li><a class="nav-link scrollto" href="#documents">Documentos</a></li>
                    <li><a class="nav-link scrollto" href="#gallery">Galeria</a></li>
                    <li><a class="nav-link scrollto" href="#video">Video</a></li>
                    <li><a class="nav-link scrollto" href="#contact">Contacto</a></li>
                    <%--<li><a class="nav-link scrollto" href="#pricing">Pricing</a></li>--%>
                    <%--                    <li class="dropdown"><a href="#"><span>Drop Down</span> <i class="bi bi-chevron-down"></i></a>
                        <ul>
                            <li><a href="#">Drop Down 1</a></li>
                            <li class="dropdown"><a href="#"><span>Deep Drop Down</span> <i class="bi bi-chevron-right"></i></a>
                                <ul>
                                    <li><a href="#">Deep Drop Down 1</a></li>
                                    <li><a href="#">Deep Drop Down 2</a></li>
                                    <li><a href="#">Deep Drop Down 3</a></li>
                                    <li><a href="#">Deep Drop Down 4</a></li>
                                    <li><a href="#">Deep Drop Down 5</a></li>
                                </ul>
                            </li>
                            <li><a href="#">Drop Down 2</a></li>
                            <li><a href="#">Drop Down 3</a></li>
                            <li><a href="#">Drop Down 4</a></li>
                        </ul>
                    </li>--%>
                </ul>
                <i class="bi bi-list mobile-nav-toggle"></i>
            </nav>
            <!-- .navbar -->
        </div>
    </header>
    <!-- End Header -->

    <!-- ======= Hero Section ======= -->
    <section id="hero">
        <div class="container">
            <div class="row justify-content-between">
                <div class="col-lg-7 pt-5 pt-lg-0 order-2 order-lg-1 d-flex align-items-center">
                    <div data-aos="zoom-out">
                        <h1 class="renta_eq-title">Renta de equipo</h1>
                        <h1>Microzanjadora</h1>
                        <h2>Marca:  MARAIS</h2>
                        <h2>Número de parte: SC3C</h2>
                        <div class="col-lg-5 order-1 order-lg-2 hero-img-movil" data-aos="zoom-out" data-aos-delay="300">
                            <img src="assets/img/MARAIS_SC3C-MICROZANJADORA-removebg.png" class="img-fluid" alt="Webinar FREMCO soplado de fibra óptica">
                        </div>
                        <div class="text-center text-lg-start">
                            <a href="#contact" class="btn-get-started scrollto">Cotizar ahora</a>
                        </div>
                    </div>
                </div>
                <div class="col-lg-5 order-1 order-lg-2 hero-img" data-aos="zoom-out" data-aos-delay="300">
                    <img src="assets/img/MARAIS_SC3C-MICROZANJADORA-removebg.png" class="img-fluid" alt="Webinar FREMCO soplado de fibra óptica">
                </div>
            </div>
        </div>

        <svg class="hero-waves" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 24 150 28 " preserveAspectRatio="none">
            <defs>
                <path id="wave-path" d="M-160 44c30 0 58-18 88-18s 58 18 88 18 58-18 88-18 58 18 88 18 v44h-352z">
            </defs>
            <g class="wave1">
                <use xlink:href="#wave-path" x="50" y="3" fill="rgba(255,255,255, .1)">
            </g>
            <g class="wave2">
                <use xlink:href="#wave-path" x="50" y="0" fill="rgba(255,255,255, .2)">
            </g>
            <g class="wave3">
                <use xlink:href="#wave-path" x="50" y="9" fill="#fff">
            </g>
        </svg>

    </section>
    <!-- End Hero -->

    <main id="main">

        <!-- ======= Intro Section ======= -->
        <section class="features">
            <div class="container">
                <div class="section-title" data-aos="fade-up">
                    <h2></h2>
                    <p class="subtitle-lp">Microzanjadora SC3C</p>
                </div>
                <div class="row" data-aos="fade-left">
                    <div class="col col-lg-6 cont-ventajas">
                        <div class="icon-box d-flex justify-content-center align-items-center bg-icon-box" data-aos="zoom-in" data-aos-delay="50">
                            <div class="intro-box-rent">
                                <h2>Sabias que el microzanjado...</h2>
                                <br>
                                <h5>
                                    Es un proceso que consiste en abrir una zanja estrecha (2,50 a 12 cm) de profundidad débil (10 a 80 cm) en la calzada o sobre la acera.
                                    El microzanjado es una tendencia de instalación subterránea que ofrece una mayor optimización de la red, rapidez y ahorro de recursos.
                                </h5>
                            </div>
                        </div>
                        <div class="icon-box d-flex justify-content-center align-items-center bg-icon-box" data-aos="zoom-in" data-aos-delay="50">
                            <div class="intro-box-rent">
                                <h5>
                                    ¡Descubre nuestra oferta de alquiler completo! En INCOM, nos enorgullece ofrecerte un servicio integral que va más allá de la simple renta de equipos de alta tecnología. También proporcionamos asesoramiento técnico especializado y nos encargamos del manejo del equipo.</h5>
                            </div>
                        </div>
                    </div>
                    <div class="col col-lg-6 cont-ventajas">
                        <div class="icon-box d-flex justify-content-center align-items-center bg-icon-box" data-aos="zoom-in" data-aos-delay="50">
                            <img src="./assets/img/IMG_7025.JPG" class="img-venta-renta" alt="">
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- End intro Section -->

        <!-- ======= Advantage Section ======= -->
        <section id="team" class="team bg-alt">
            <div class="container container-box">
                <div class="section-title" data-aos="fade-up">
                    <h2></h2>
                    <p class="subtitle-lp">Ventajas</p>
                </div>
                <div class="row" data-aos="fade-left">
                    <div class="d-flex flex-column flex-lg-row justify-content-evenly align-items-center">
                        <div class="d-flex flex-column justify-content-center align-items-center rounded border p-4" style="width: 320px">
                            <div class="px-2" data-aos="zoom-in" data-aos-delay="100">
                                <div class="text-center content-start">
                                    <img src="./assets/img/start-point.png" class="start-point" alt="">
                                    <h5>Microzanjado para redes FTTx</h5>
                                </div>
                            </div>
                        </div>

                        <div class="d-flex flex-column justify-content-center align-items-center rounded border p-4" style="width: 320px">
                            <div class="px-2" data-aos="zoom-in" data-aos-delay="100">
                                <div class="d-flex justify-content-center align-items-center">
                                    <img src="./assets/img/start-point.png" class="start-point" alt="">
                                </div>
                                <div class="text-center content-start">
                                    <h6>Mínima molestia a vecinos, automóviles y peatones</h6>
                                </div>
                            </div>
                        </div>

                        <div class="d-flex flex-column justify-content-center align-items-center rounded border p-4" style="width: 320px">
                            <div class="px-2" data-aos="zoom-in" data-aos-delay="100">
                                <div class="d-flex justify-content-center align-items-center">
                                    <img src="./assets/img/start-point.png" class="start-point" alt="">
                                </div>
                                <div class="text-center content-start">
                                    <h5>Mayor seguridad dentro de la obra</h5>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row my-4" data-aos="fade-left">
                    <div class="d-flex flex-column flex-lg-row justify-content-evenly align-items-center">
                        <div class="d-flex flex-column justify-content-center align-items-center rounded border p-4" style="width: 320px">
                            <div class="px-2" data-aos="zoom-in" data-aos-delay="100">
                                <div class="d-flex justify-content-center align-items-center">
                                    <img src="./assets/img/start-point.png" class="start-point" alt="">
                                </div>
                                <div class="text-center content-start">
                                    <h5>Preservación de la estructura de la acera</h5>
                                </div>
                            </div>
                        </div>

                        <div class="d-flex flex-column justify-content-center align-items-center rounded border p-4" style="width: 320px">
                            <div class="px-2" data-aos="zoom-in" data-aos-delay="100">
                                <div class="d-flex justify-content-center align-items-center">
                                    <img src="./assets/img/start-point.png" class="start-point" alt="">
                                </div>
                                <div class="text-center content-start">
                                    <h5>Reducción de los costos de construcción</h5>
                                </div>
                            </div>
                        </div>
                        <div class="d-flex flex-column justify-content-center align-items-center rounded border p-4" style="width: 320px">
                            <div class="px-2" data-aos="zoom-in" data-aos-delay="100">
                                <div class="d-flex justify-content-center align-items-center">
                                    <img src="./assets/img/start-point.png" class="start-point" alt="">
                                </div>
                                <div class="text-center content-start">
                                    <h5>Rapidez de ejecución</h5>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </section>
        <!-- End Advantage Section -->

        <!-- ======= Features Section ======= -->
        <section id="features" class="features">
            <div class="container">
                <div class="section-title" data-aos="fade-up">
                    <h2></h2>
                    <p class="subtitle-lp">Características</p>
                </div>
                <div class="row" data-aos="fade-left">
                    <div class="col col-lg-6 cont-ventajas">
                        <div class="icon-box d-flex justify-content-center align-items-center bg-icon-box" data-aos="zoom-in" data-aos-delay="50">
                            <ul style="list-style-type: disclosure-closed">
                                <li>Los trabajos de microzanja se realizan con máquinas de corte en seco, por lo que son más rápidas y limpias.</li>
                                <li>La microzanja se utiliza principalmente para desplegar redes de fibra óptica, pero también se aplica en diversas áreas, como instalaciones de agua y circuitos cerrados de TV.</li>
                                <li>La máquina ofrece un rendimiento aproximado de zanjado continua de 80 a 100 m lineales por hora (El rendimiento puede variar dependiendo la supercie). Profundidad mínima de 10 cm y una máxima de 80 cm.</li>
                            </ul>
                        </div>
                    </div>
                    <div class="col col-lg-6 cont-ventajas">
                        <div class="icon-box d-flex justify-content-center align-items-center bg-icon-box" data-aos="zoom-in" data-aos-delay="50">
                            <ul style="list-style-type: disclosure-closed">
                                <li>Las ventajas de la microzanja frente a las zanjas convencionales son principalmente la reducción de costes y de tiempos de instalación, la menor alteración del tráfico y el menor impacto ambiental.</li>
                                <li>La microzanja es una solución económica en zonas industriales alejadas sin canalizaciones. Costo: 1/3 de las canalizaciones convencionales.</li>
                                <li>Desplazamiento horizontal de disco con corte en ángulo y zanjado en curva. Ofrece manejo a control remoto y funcionamiento continuo en cambios de nivel o dirección.</li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- End Features Section -->

        <!-- ======= Features Section ======= -->
            <section id="documents" class="team bg-alt">
                <div class="container">
                    <div class="section-title" data-aos="fade-up">
                        <h2></h2>
                        <p class="subtitle-lp">Documentos</p>
                    </div>
                    <div class="row content">
                        <div class="d-flex flex-column flex-lg-row justify-content-between cont-docs">
                            <div class="col-md-6 order-2 order-md-1 container-bg" data-aos="fade-up">
                                <h3>Haz realidad tus proyectos de manera eficiente y económica.</h3>
                                <h4>¡Despliega tu red sin límites!</h4>
                                <a href="https://www.incom.mx/documents/pdf/MARAIS_SC3C-MICROZANJADORA.pdf" target="_blank">
                                    <div class="btn-degradate my-4">Especificaciones técnicas 
                                        <img class="pdf-bgout" src="./assets/img/pdf-bgout.png" alt="">
                                    </div>
                                </a>
                                <a href="https://www.incom.mx/documents/pdf/INCOM_CATALOGO_SOLUCION_FIBRA_SOPLADA.pdf" target="_blank">
                                    <div class="btn-degradate">Catálogo de solución completa
                                        <img class="pdf-bgout" src="./assets/img/pdf-bgout.png" alt="">
                                    </div>
                                </a>
                            </div>
                            <div class="col-md-5 order-1 order-md-2 container-bg-right is-h-fit" data-aos="fade-left">
                                <h4 class="is-bt-2 title-tractor">Tractor y discos disponibles</h4>
                                <div>
                                    <img src="assets/img/detalle-zanjadora.png" class="img-fluid" alt="Soplado de fibra óptica">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        <!-- End Features Section -->

        <!-- ======= Video Section ======= -->
        <section id="gallery" class="about about-1">
            <div class="container">
                <div class="section-title" data-aos="fade-up">
                    <h2></h2>
                    <p class="subtitle-lp">Galería</p>
                </div>
                <div class="wrapper-gallery">
                    <div class="gallery">
                        <ul>
                            <li><img src="./assets/img/pt1-renta-equipo.png"></li>
                            <li><img src="./assets/img/pt3-renta-equipo.png"></li>
                            <li><img src="./assets/img/pt5-renta-equipo.png"></li>
                            <li><img src="./assets/img/pt6-renta-equipo.png"></li>
                            <li><img src="./assets/img/pt7-renta-equipo.png"></li>
                            <li><img src="./assets/img/pt9-renta-equipo.png"></li>
                        </ul>
                    </div>
                </div>
        </section>
        <!-- End Video Section -->

        <!-- ======= Video Section ======= -->
        <section id="video" class="about bg-alt">
            <div class="container">
                <div class="section-title" data-aos="fade-up">
                    <h2></h2>
                    <p class="subtitle-lp">Video</p>
                </div>
                <div class="container-fluid">
                    <div class="row">
                        <div class="col video-box d-flex justify-content-center align-items-stretch" data-aos="fade-right">
                            <iframe width="560" height="315" src="https://www.youtube.com/embed/Te92CjP6EhI" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- End Video Section -->

        <!-- ======= Contact Section ======= -->
        <section id="contact" class="contact">
            <div class="container">
                <div class="section-title" data-aos="fade-up">
                    <h2></h2>
                    <p class="subtitle-lp">Cotizar ahora</p>
                </div>
                <div class="row">
                    <div class="col-lg-4" data-aos="fade-right" data-aos-delay="100">
                        <div class="info">
                            <div class="email">
                                <i class="bi bi-envelope"></i>
                                <div>
                                    <h6 class="contact_form-evento">Correo electrónico:</h6>
                                    <a href="mailto:telemarketing@incom.mx" style="padding: 0;"><h4 class="contact_form-link">telemarketing@incom.mx</h4></a>                                    
                                </div>
                            </div>
                            <div class="address">
                                <i class="bi bi-phone"></i>
                                <h6 class="contact_form-evento">Teléfono:</h6>
                                <a href="tel:5552436900" style="padding: 0;"><h4 class="contact_form-link">(55) 5243-6900</h4></a>
                            </div>
                            <div class="phone">
                                <i class="bi bi-whatsapp"></i>
                                <h6 class="contact_form-evento">WhatsApp</h6>
                                <a href="https://api.whatsapp.com/send?phone=525530327332" style="padding: 0;" target="_blank"><h4 class="contact_form-link">+52 (52) 3032-7332</h4></a>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-8 mt-5 mt-lg-0" data-aos="fade-left" data-aos-delay="200">
                        <form runat="server" class="php-email-form">
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="d-flex flex-column justify-content-center align-items-center">
                                        <div class="col-md-10 cont-form" style="padding-bottom: 1rem;">
                                            <div class="form-group">
                                                <asp:Label ID="lbl_nombre" class="label-renta" AssociatedControlID="txt_nombre" Text="Nombre: * " runat="server"></asp:Label>
                                                <asp:TextBox class="form-control w-75" ID="txt_nombre" required="true" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-10 cont-form" style="padding-bottom: 1rem;">
                                            <div class="form-group">
                                                <asp:Label ID="lbl_email" class="label-renta" AssociatedControlID="txt_email" Text="Correo electrónico: * " runat="server"></asp:Label>
                                                <asp:TextBox class="form-control w-75" ID="txt_email" required="true" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-10 cont-form" style="padding-bottom: 1rem;">
                                            <div class="form-group">
                                                <asp:Label ID="lbl_telefono" class="label-renta" AssociatedControlID="txt_telefono" Text="Teléfono: " runat="server"></asp:Label>
                                                <asp:TextBox class="form-control w-75" ID="txt_telefono" required="false" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-10 cont-form" style="padding-bottom: 2rem;">
                                            <div class="form-group">
                                                <asp:Label ID="lbl_empresa" class="label-renta" AssociatedControlID="txt_empresa" Text="Empresa: " runat="server"></asp:Label>
                                                <asp:TextBox class="form-control w-75" ID="txt_empresa" required="false" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <asp:Label ID="lbl_mensaje" runat="server"></asp:Label>
                                        <asp:Button Text="Solicitar cotización" ID="btn_enviar" OnClientClick="btn_click(this)" class="btn btn-success rounded" runat="server" OnClick="btn_enviar_Click" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </form>
                    </div>
                </div>
            </div>
        </section>
        <!-- End Contact Section -->

    </main>
    <!-- End #main -->

    <!-- ======= Footer ======= -->
    <footer id="footer" style="background-color: #0C3766;">
        <div class="footer-top">
            <div class="container">
                <div class="row">
                    <div class="col-lg-4 col-md-6">
                        <img src="assets/img/incom.png" alt="INCOM La ferretera de las telecomunicaciones" />
                    </div>
                    <div class="col-lg-2 col-md-6 footer-links">
                        <h4>Redes</h4>
                        <ul>
                            <li><i class="bx bxl-facebook"></i><a href="https://www.facebook.com/incommexico/" target="_blank">Facebook</a></li>
                            <li><i class="bx bxl-twitter"></i><a href="https://twitter.com/incom_mx" target="_blank">Twitter</a></li>
                            <li><i class="bx bxl-instagram"></i><a href="https://www.instagram.com/incom_mx/" target="_blank">Instagram</a></li>
                            <li><i class="bx bxl-youtube"></i><a href="https://www.youtube.com/user/incommx" target="_blank">YouTube</a></li>
                        </ul>
                    </div>

                    <div class="col-lg-2 col-md-6 footer-links">
                        <h4>INCOM</h4>
                        <ul>
                            <li><i class="bx bx-chevron-right"></i><a href="https://www.incom.mx/productos?Length=15" target="_blank">Tienda</a></li>
                            <li><i class="bx bx-chevron-right"></i><a href="http://blog.incom.mx/" target="_blank">Blog</a></li>
                            <li><i class="bx bx-chevron-right"></i><a href="https://www.incom.mx/glosario/A" target="_blank">Glosario</a></li>
                            <li><i class="bx bx-chevron-right"></i><a href="https://www.incom.mx/ense%C3%B1anza/infograf%C3%ADas.aspx" target="_blank">Infografías</a></li>
                        </ul>
                    </div>

                    <div class="col-lg-4 col-md-6 footer-links">
                        <h4>Catálogos</h4>
                        <ul>
                            <li><i class="bx bx-chevron-right"></i><a href="https://www.incom.mx/documents/pdf/INCOM_CATALOGO_SOLUCION_FIBRA_SOPLADA.pdf" target="_blank">Solución fibra soplada</a></li>
                            <li><i class="bx bx-chevron-right"></i><a href="https://www.incom.mx/documents/pdf/CATALOGO_MAQUINAS_PARA_SOPLADO_FREMCO.pdf" target="_blank">Máquinas de soplado</a></li>
                            <li><i class="bx bx-chevron-right"></i><a href="https://www.incom.mx/documents/pdf/CATALOGO_INCOM_SUBTERRANEO.pdf" target="_blank">Instalación subterranea</a></li>
                            <li><i class="bx bx-chevron-right"></i><a href="https://www.incom.mx/documents/pdf/ICOPTIKS_CATALOGO_SOLUCIONES_PARA_FIBRA_OPTICA.pdf" target="_blank">Redes de fibra óptica</a></li>
                        </ul>
                    </div>

                </div>
            </div>
        </div>

        <div class="container">
            <div class="copyright">
                &copy; Copyright <strong><span>IT4U</span></strong>. Todos los derechos reservados
            </div>
        </div>
    </footer>
    <!-- End Footer -->

    <a href="#" class="back-to-top d-flex align-items-center justify-content-center"><i class="bi bi-arrow-up-short"></i></a>
    <div id="preloader"></div>

    <!-- Vendor JS Files -->
    <script src="assets/vendor/purecounter/purecounter_vanilla.js"></script>
    <script src="assets/vendor/aos/aos.js"></script>
    <script src="assets/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="assets/vendor/glightbox/js/glightbox.min.js"></script>
    <script src="assets/vendor/swiper/swiper-bundle.min.js"></script>
    <script src="assets/vendor/php-email-form/validate.js"></script>

    <!-- Template Main JS File -->
    <script src="assets/js/main.js"></script>

</body>

<style>
    .my-button-style {
        border-width: 1px;
        width: 200px;
        height: 80px;
        border-radius: 100px !important;
        background: #fff;
        position: absolute;
        border: 5px solid #6fb07f;
        font-size: 22px;
        color: #6fb07f;
        text-align: center;
        vertical-align: middle;
    }

    .cont-docs {
        background-color: rgb(31, 41, 55);
    }

    .my-button-style:hover, .my-button-style:focus {
        outline: none;
        border-color: #6fb07f;
        box-shadow: 0 0 5px 0 #6fb07f;
    }

    .my-button-style:focus {
        -webkit-animation: extend 1s ease-in-out;
        -ms-animation: extend 1s ease-in-out;
        animation: extend 1s ease-in-out;
        -webkit-animation-fill-mode: forwards;
        animation-fill-mode: forwards;
    }

    .my-button-style:focus > span {
        -webkit-animation: disappear 1s ease-in-out;
        -ms-animation: disappear 1s ease-in-out;
        animation: disappear 1s ease-in-out;
        -webkit-animation-fill-mode: forwards;
        animation-fill-mode: forwards;
    }

    .my-button-style:focus > img {
        -webkit-animation: appear 1s ease-in-out;
        -ms-animation: appear 1s ease-in-out;
        animation: appear 1s ease-in-out;
        -webkit-animation-fill-mode: forwards;
        animation-fill-mode: forwards;
    }

    .renta_eq-title {
        position: relative;
        display: inline-block;
        font-size: 24px;
        font-weight: bold;
        margin-bottom: 10px;
    }
    .renta_eq-title::before {
        content: "";
        position: absolute;
        bottom: -90px;
        left: 0;
        width: 50%;
        height: 6px;
        background-color: red;
    }

    .container-bg {
        color: white;
        padding: 3rem;
        margin-right: 2rem;
        height: fit-content;
        align-self: center;
    }

    .container-bg > h3 { 
        margin-bottom: 4rem;
    }

    .container-bg-right {
        border-radius: 24px;
        height: fit-content;
    }

    .btn-degradate {
        width: 300px;
        height: 60px;
        border-radius: 5px;
        display: flex;
        justify-content: center;
        align-items: center;
        background-image: linear-gradient(to right, rgb(241, 113, 4), rgb(254, 81, 113));
        font-weight: 600;
        color: #fff;
        width: 80%;
        display: flex;
        justify-content: space-between;
        padding: 0% 10%;
    }

    .pdf-bgout {
        width: 30px;
        margin-left: 20px;
    }

    .contact_form-evento {
        padding: 0 0 0 60px;
    }

    .subtitle-lp {
        font-size: 1.5rem !important;
    }

    .bg-alt {
       background-color: rgb(241, 246, 249);
    }

    .bg-icon-box {
        background-color: #fff !important;
    }

    .icon-box li {
        margin-top: 2rem;
    }

    .tractor-img {
        width: 60%;
        padding: 0rem 2rem;
    }

    .is-bt-2 {
        background-image: linear-gradient(to right, rgb(241, 113, 4), rgb(254, 81, 113));
        color: white;
        text-align: center;
        margin-bottom: 0;
        line-height: 4;
    }

    .hero-img-movil {
        display: none;
    }

    .subtitle-intro-tr {
        background-color: #ffffff00;
    }

    .cont-ventajas li {
        text-align: justify;
    }

    .subtitle-intro {
        margin: 0;
        font-size: 24px;
        font-weight: 700;
        text-transform: uppercase;
        font-family: "Poppins", sans-serif;
        color: #010483;
    }

    .container-box {
        margin-bottom: 4rem;
    }

    .subtitle-intro {
        margin-bottom: 2rem;
    }

    .intro-box-team {
        margin-bottom: 4rem;
    }

    .wrapper-gallery {
        width: 80%;
        margin: 0 auto;
    }

    div.gallery {
        margin-top: 0;
    }

    div.gallery ul {
        list-style-type: none;
        margin-left: 35px;
    }

    /* animation */
    div.gallery ul li, div.gallery li img {
        -webkit-transition: all 0.1s ease-in-out;
        -moz-transition: all 0.1s ease-in-out;
        -o-transition: all 0.1s ease-in-out;
        transition: all 0.1s ease-in-out;
    }

    div.gallery ul li {
        position: relative;
        float: left;
        width: 260px;
        height: 260px;
        margin: 5px;
        padding: 5px;
        z-index: 0;
    }

    /* Image is position nicely under li */
    div.gallery ul li img {
        position: absolute;
        left: 0;
        top: 0;
        border: 1px solid #dddddd;
        padding: 5px;
        width: 260px;
        height: 260px;
        background: #f0f0f0;
    }

    .section-title.aos-init.aos-animate, .subtitle-intro {
        text-align: center;
    }

    .intro-box-team {
        padding: 1rem;
    }

    .about-1 {
        padding: 40px 0 7rem 0 !important;
    }

    .intro-box-team > h5:nth-child(2) {
        font-size: 1rem;
    }

    p.attribution {
        font-family: 'Consolas';
        color: #000;
        clear: both;
        text-align: center;
        line-height: 25px;
        padding-top: 30px;
    }

    p.attribution a {
        color: #4c8d7c;
    }

    .img-venta-renta {
        width: 100%;
        height: auto;
    }

    section.features:nth-child(3) > div:nth-child(1) > div:nth-child(2) > div:nth-child(1) {
        display: flex;
        justify-content: space-between;
        flex-direction: column;
    }

    .intro-box-rent {
        text-align: justify;
    }

    .cont-img-tractor {
        width: 100%;
        background-color: #fff;
    }

    .cont-img-tractor > h6 {
        text-align: center;
        padding-top: 1rem;
    }

    /* Responsive hack */
    @media only screen and (min-width: 499px) and (max-width: 1212px) {
        .wrapper {
            width: 500px;
        }
    }

    @media only screen and (max-width: 498px) {
        .wrapper {
            width: 300px;
        }

        div.gallery ul {
            list-style-type: none;
            margin: 0;
        }
    }

    @media (max-width: 575px) {
    .hero-img-movil {
        display: block;
        margin: auto;
    }

    .hero-img {
        display: none;
    }

    .renta_eq-title::before {
        bottom: -50px;
    }

    div.aos-animate:nth-child(1) > h1:nth-child(2) {
        margin-bottom: 30px !important;
    }

    .hero-waves {
        margin-top: 30px;
    }

    .hero-img-movil {
        max-width: 80%;
    }

    div.aos-animate:nth-child(1) {
        text-align: center;
    }

    #hero h2 {
        margin-bottom: 10px;
    }

    #hero .btn-get-started {
        margin-top: 0;
    }

    .content-start > h5, .content-start > h6 {
        font-size: 1rem;
    }

    .flex-column.justify-content-center.align-items-center{
        padding: 0.75rem !important;
        margin-bottom: 6px;
    }

    .icon-box li {
        font-size: 0.75rem;
        margin-top: 1rem;
    }

    div.col:nth-child(2) > div:nth-child(1) {
        margin-top: -2rem;
    }

    .container-bg > h3 {
        font-size: 1rem;
    }

    .cont-docs {
        padding: 0rem 2rem;
    }

    .btn-degradate {
        width: 80%;
        width: 100%;
        font-size: 0.75rem;
    }

    .container-bg > h3 {
        margin-bottom: 2rem;
        margin-right: 0;
    }

    .container-bg {
        margin-top: 2rem;
        margin-right: 0;
        padding: 2rem;
        margin-bottom: 4rem;
    }

    .start-point {
        width: 32px;
    }

    .section-title {
        padding-bottom: 1rem;
    }

    .title-tractor {
        font-size: 1.25rem;
        margin-top: 2rem;
    }

    .tractor-img {
        width: 100%;
    }

    .contact_form-link {
        font-size: 16px !important;
    }

    .info {
        padding: 1rem;
        margin: auto;
    }

    .form-group {
        display: flex;
        flex-direction: column;
        width: 90%;
        margin: auto;
    }

    #txt_nombre {
        padding: 0rem;
        width: 100%;
    }

    .form-control {
        width: 100% !important;
    }

    .cont-form {
        width: 100%;
    }

    .footer-top > div > div {
        padding-left: 1.5rem;
    }

    .label-renta {
        margin-bottom: 0.5rem;
    }

    section.features:nth-child(1) > div:nth-child(1) > div:nth-child(2) > div:nth-child(1) > div:nth-child(1) > div:nth-child(1) > h2:nth-child(1) {
        font-size: 1rem;
    }

    section.features:nth-child(1) > div:nth-child(1) > div:nth-child(2) > div:nth-child(1) > div:nth-child(1) > div:nth-child(1) > h5:nth-child(3) {
        font-size: 0.75rem;
    }

    div.icon-box:nth-child(2) > div:nth-child(1) > h5:nth-child(1) {
        font-size: 0.75rem;
    }

    div.icon-box:nth-child(2) {
        padding-top: 0;
        margin-bottom: 1rem;
    }
    
    }

    @media (min-width: 1200px) {
        div.gallery ul li:nth-child(1) img:hover,
        div.gallery ul li:nth-child(2) img:hover,
        div.gallery ul li:nth-child(3) img:hover {
            margin-top: -170px !important;
        }

        div.gallery ul li:nth-child(2) img:hover,
        div.gallery ul li:nth-child(3) img:hover,
        div.gallery ul li:nth-child(6) img:hover {
            left: -38% !important;
        }
    }

    @media (max-width: 800px) {
        div.gallery ul li {
            width: 100%;
            height: auto;
        }

        div.gallery ul li img {
            position: inherit;
            width: 80%;
            height: auto;
        }

        .wrapper-gallery {
            width: 100%;
        }

        .cont-docs {
            padding: 0 2rem 0 2rem;
        }
    }

    @media (min-width: 800px) {
        /* Make sure z-index is higher on hover */
        /* Ensure that hover image overlapped the others */
            div.gallery ul li:hover {
                z-index: 5;
        }

        div.gallery ul li img:hover {
            width: 532px;
            height: 530px;
            margin-top: -440px;
            margin-left: -170px;
            top: 65%;
            left: 65%;
        }

        .cont-docs {
            background-color: rgb(31, 41, 55);
            padding-right: 0;
            padding-left: 0 !important;
        }
    }

    @media (min-width: 1000px) and (max-width: 1200px) { 
        div.gallery ul li:nth-child(2) img:hover,
        div.gallery ul li:nth-child(4) img:hover,
        div.gallery ul li:nth-child(6) img:hover {
            left: -38% !important;
        }

        div.gallery ul li:nth-child(1) img:hover,
        div.gallery ul li:nth-child(2) img:hover {
            margin-top: -170px !important;
        }
    }

    @media (min-width: 360px) and (max-width: 800px) { 
        .cont-ventajas {
            flex: 1 1 100%;
        }
    }

    @media (min-width: 320px) and (max-width: 580px) { 
        .bi.bi-envelope, .bi.bi-phone, .bi.bi-whatsapp {
            margin-left: 1rem;
        }

        div.gallery ul {
            margin-left: 0;
        }
    }
</style>
</html>
