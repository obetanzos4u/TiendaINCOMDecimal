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
                            <img src="assets/img/MARAIS_SC3C-MICROZANJADORA-removebg.png" class="img-fluid animated" alt="Webinar FREMCO soplado de fibra óptica">
                        </div>
                        <div class="text-center text-lg-start">
                            <a href="#contact" class="btn-get-started scrollto">Cotizar ahora</a>
                        </div>
                    </div>
                </div>
                <div class="col-lg-5 order-1 order-lg-2 hero-img" data-aos="zoom-out" data-aos-delay="300">
                    <img src="assets/img/MARAIS_SC3C-MICROZANJADORA-removebg.png" class="img-fluid animated" alt="Webinar FREMCO soplado de fibra óptica">
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
        <!-- ======= Team Section ======= -->
        <section id="team" class="team">
            <div class="container">
                <div class="section-title" data-aos="fade-up">
                    <h2></h2>
                    <p class="subtitle-lp">Ventajas</p>
                </div>
                <div class="row" data-aos="fade-left">
                    <div class="d-flex flex-column flex-lg-row justify-content-evenly align-items-center">
                        <div class="d-flex flex-column justify-content-center align-items-center rounded border p-4" style="width: 320px">
                            <div class="px-2" data-aos="zoom-in" data-aos-delay="100">
                                <div class="text-center content-start">
                                    <img src="../Renta-equipo/assets/img/start-point.png" class="start-point" alt="">
                                    <h5>Microzanjado para redes FTTx</h5>
                                    <!-- <div class="">
                                        <p class="my-0">Jueves 20 de octubre | 10:00 AM</p>
                                        <p class="my-0">Zona horaria: CDMX (GMT-5)</p>
                                    </div> -->
                                </div>
                            </div>
                        </div>

                        <div class="d-flex flex-column justify-content-center align-items-center rounded border p-4" style="width: 320px">
                            <div class="px-2" data-aos="zoom-in" data-aos-delay="100">
                                <div class="d-flex justify-content-center align-items-center">
                                    <img src="../Renta-equipo/assets/img/start-point.png" class="start-point" alt="">
                                </div>
                                <div class="text-center content-start">
                                    <h6>Mínima molestia a vecinos, automoviles y peatones</h6>
                                    <!-- <div class="">
                                        <p class="my-0">Jueves 20 de octubre | 10:00 AM</p>
                                        <p class="my-0">Zona horaria: CDMX (GMT-5)</p>
                                    </div> -->
                                </div>
                            </div>
                        </div>

                        <div class="d-flex flex-column justify-content-center align-items-center rounded border p-4" style="width: 320px">
                            <div class="px-2" data-aos="zoom-in" data-aos-delay="100">
                                <div class="d-flex justify-content-center align-items-center">
                                    <img src="../Renta-equipo/assets/img/start-point.png" class="start-point" alt="">
                                </div>
                                <div class="text-center content-start">
                                    <h5>Mayor seguridad dentro de la obra</h5>
                                    <!-- <div class="">
                                        <p class="my-0">Jueves 20 de octubre | 10:00 AM</p>
                                        <p class="my-0">Zona horaria: CDMX (GMT-5)</p>
                                    </div> -->
                                </div>
                            </div>
                        </div>

                        <!-- <div class="d-flex flex-column justify-content-center align-items-center rounded border p-4 mt-4 mt-lg-0 h-100" style="width: 320px">
                            <div class="px-2" data-aos="zoom-in" data-aos-delay="100">
                                <div class="d-flex justify-content-center align-items-center my-4">
                                    <span style="background-color: rgba(1, 3, 111, 0.8); border-radius: 100%; padding: 1rem">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="72" height="72" viewBox="0 0 24 24" style="fill: rgba(255, 255, 255, 1);">
                                            <path d="M18 7c0-1.103-.897-2-2-2H4c-1.103 0-2 .897-2 2v10c0 1.103.897 2 2 2h12c1.103 0 2-.897 2-2v-3.333L22 17V7l-4 3.333V7zm-1.998 10H4V7h12l.001 4.999L16 12l.001.001.001 4.999z"></path></svg>
                                    </span>
                                </div>
                                <div class="text-center">
                                    <h4>Acceso</h4>
                                    <div class="">
                                        <p class="my-0">
                                            Al registrarte se te compartirá la<br />
                                            invitación al evento en tu correo electrónico.
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="d-flex flex-column justify-content-center align-items-center rounded border p-4 mt-4 mt-lg-0 h-100" style="width: 320px">
                            <div class="px-2" data-aos="zoom-in" data-aos-delay="100">
                                <div class="d-flex justify-content-center align-items-center my-4">
                                    <span style="background-color: rgba(1, 3, 111, 0.8); border-radius: 100%; padding: 1rem">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="72" height="72" viewBox="0 0 24 24" style="fill: rgba(255, 255, 255, 1);">
                                            <path d="M12.586 2.586A2 2 0 0 0 11.172 2H4a2 2 0 0 0-2 2v7.172a2 2 0 0 0 .586 1.414l8 8a2 2 0 0 0 2.828 0l7.172-7.172a2 2 0 0 0 0-2.828l-8-8zM7 9a2 2 0 1 1 .001-4.001A2 2 0 0 1 7 9z"></path>
                                        </svg>
                                    </span>
                                </div>
                                <div class="text-center">
                                    <h4>Beneficios</h4>
                                    <div class="">
                                        <p class="my-0">
                                            Increíbles descuentos
                                            <br />
                                            para los asistentes.
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div> -->
                    </div>
                </div>

                <div class="row my-4" data-aos="fade-left">
                    <div class="d-flex flex-column flex-lg-row justify-content-evenly align-items-center">
                        <div class="d-flex flex-column justify-content-center align-items-center rounded border p-4" style="width: 320px">
                            <div class="px-2" data-aos="zoom-in" data-aos-delay="100">
                                <div class="d-flex justify-content-center align-items-center">
                                    <img src="../Renta-equipo/assets/img/start-point.png" class="start-point" alt="">
                                </div>
                                <div class="text-center content-start">
                                    <h5>Preservación de la estructura de la acera</h5>
                                    <!-- <div class="">
                                        <p class="my-0">Jueves 20 de octubre | 10:00 AM</p>
                                        <p class="my-0">Zona horaria: CDMX (GMT-5)</p>
                                    </div> -->
                                </div>
                            </div>
                        </div>

                        <div class="d-flex flex-column justify-content-center align-items-center rounded border p-4" style="width: 320px">
                            <div class="px-2" data-aos="zoom-in" data-aos-delay="100">
                                <div class="d-flex justify-content-center align-items-center">
                                    <img src="../Renta-equipo/assets/img/start-point.png" class="start-point" alt="">
                                </div>
                                <div class="text-center content-start">
                                    <h5>Reducción de los costos de construcción</h5>
                                    <!-- <div class="">
                                        <p class="my-0">Jueves 20 de octubre | 10:00 AM</p>
                                        <p class="my-0">Zona horaria: CDMX (GMT-5)</p>
                                    </div> -->
                                </div>
                            </div>
                        </div>

                        <div class="d-flex flex-column justify-content-center align-items-center rounded border p-4" style="width: 320px">
                            <div class="px-2" data-aos="zoom-in" data-aos-delay="100">
                                <div class="d-flex justify-content-center align-items-center">
                                    <img src="../Renta-equipo/assets/img/start-point.png" class="start-point" alt="">
                                </div>
                                <div class="text-center content-start">
                                    <h5>Rápidez de ejecución</h5>
                                    <!-- <div class="">
                                        <p class="my-0">Jueves 20 de octubre | 10:00 AM</p>
                                        <p class="my-0">Zona horaria: CDMX (GMT-5)</p>
                                    </div> -->
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- End Team Section -->

        <!-- ======= Features Section ======= -->
        <section id="features" class="features bg-alt">
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
                                <li>La máquina ofrece un rendimiento aproximado de zanjado continua de 80m a 100m lineales por hora. Produndida mínima de 25 cm y una máxima de 45cm.</li>
                            </ul>
                        </div>
                    </div>
                    <div class="col col-lg-6 cont-ventajas">
                        <div class="icon-box d-flex justify-content-center align-items-center bg-icon-box" data-aos="zoom-in" data-aos-delay="50">
                            <ul style="list-style-type: disclosure-closed">
                                <li>Las ventajas de la microzanja frente a las zanjas convencionales son principalmente la reducción de costes y de tiempos de instalación, la menor alteración del tráfico y el menor impacto ambiental.</li>
                                <li>La microzanja es una solución económica en zonas industriales alejadas sin canalizaciones. Costo: 1/3 de las canalizaciones convencionales.</li>
                                <li>Desplazamiento horizontal de disco con corte en ángulo y zanjado en curva.Ofrece manejo a control remoto y funcionamiento continuo en cambios de nivel o dirección.</li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- End Features Section -->

        <!-- ======= Features Section ======= -->
            <section id="documents" class="team">
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
                                <a href="">
                                    <div class="btn-degradate my-4">Especificaciones técnicas 
                                        <img class="pdf-bgout" src="../Renta-equipo/assets/img/pdf-bgout.png" alt="">
                                    </div>
                                </a>
                                <a href="">
                                    <div class="btn-degradate">Catálogo de solución completa
                                        <img class="pdf-bgout" src="../Renta-equipo/assets/img/pdf-bgout.png" alt="">
                                    </div>
                                </a>
                            </div>
                            <div class="col-md-5 order-1 order-md-2 container-bg-right is-h-fit" data-aos="fade-left">
                                <h3 class="is-bt-2 title-tractor">Tractor y discos disponibles</h3>
                                <h6>Modelo SC3C Carrier</h6>
                                <img class="tractor-img" src="assets/img/tractor.png" alt="">
                                <img src="assets/img/discos-zanjadora.png" class="img-fluid" alt="Soplado de fibra óptica">
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        <!-- End Features Section -->

        <!-- ======= Exhibitor Section ======= -->
        <!-- <section id="exhibitor" class="testimonials">
            <div class="container">
                <div class="testimonials-slider swiper">
                    <div class="swiper-wrapper">
                        <div class="swiper-slide">
                            <div class="testimonial-item">
                                <img src="assets/img/INCOM_WEBINAR_FREMCO_LP3.png" class="" alt="">
                                <h3>Ing. Eva Garza</h3>
                                <h4>Gerente de soporte técnico en INCOM</h4>
                                <p>
                                    <i class="bx bxs-quote-alt-left quote-icon-left"></i>
                                    La técnica de soplado es la técnica más eficiente y segura para instalar fibra óptica y con las máquinas de soplado de FREMCO podemos aprovechar grandes ventajas. 
                                    <i class="bx bxs-quote-alt-right quote-icon-right"></i>
                                </p>
                            </div>
                        </div>
                        <!-- End testimonial item -->
                    <!-- </div> -->
                    <%--<div class="swiper-pagination"></div>--%>
                <!-- </div>

            </div>
        </section> -->
        <!-- End Exhibitor Section -->

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
                                    <a href="mailto:comunicacion@incom.mx" style="padding: 0;"><h4 class="contact_form-link">comunicacion@incom.mx</h4></a>                                    
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
    background-color: rgb(31, 41, 55);
    color: white;
    padding: 3rem;
    border-radius: 24px;
    margin-right: 2rem;
    height: fit-content;
    align-self: center;
  }

  .container-bg > h3 { 
    margin-bottom: 4rem;
  }

  .container-bg-right {
    border-radius: 24px;
    margin-right: 2rem;
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
        margin-bottom: 2rem;
    }

    .hero-img-movil {
        display: none;
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
    }

    .start-point {
        width: 32px;
    }

    .section-title {
        padding-bottom: 1rem;
    }

    .container-bg-right {
        margin-right: 0px;
    }

    .title-tractor {
        font-size: 1.25rem;
        margin-top: 1rem;
        margin-bottom: 1rem;
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
    }
</style>
</html>
