<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="soplado_fremco_2022" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="utf-8">
    <meta content="width=device-width, initial-scale=1.0" name="viewport">
    <title>INCOM eventos</title>
    <meta content="INCOM te invita a nuestro próximo evento" name="description">
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
    <link href="assets/css/style.css?ver=1.10" rel="stylesheet">
</head>
<body>
    <!-- ======= Header ======= -->
    <header id="header" class="fixed-top d-flex align-items-center header-transparent">
        <div class="container d-flex align-items-center justify-content-between">
            <div class="logo">
                <a href='<%= ResolveUrl("~/mkt/eventos/index.aspx") %>'>
                    <img src="assets/img/incom.png" alt="INCOM La ferretera de las telecomunicaciones" class="img-fluid" />
                </a>
                <!-- Uncomment below if you prefer to use an image logo -->
                <!-- <a href="index.html"><img src="assets/img/logo.png" alt="" class="img-fluid"></a>-->
            </div>

            <nav id="navbar" class="navbar">
                <ul>
                    <li><a class="nav-link scrollto active" href="#hero">Inicio</a></li>
                    <li><a class="nav-link scrollto" href="#team">Detalles</a></li>
                    <li><a class="nav-link scrollto" href="#contact">Registro</a></li>
                    <li><a class="nav-link scrollto" href="#features">Temario</a></li>
                    <li><a class="nav-link scrollto" href="#exhibitor">Expositor</a></li>
                    <li><a class="nav-link scrollto" href="#video">Video</a></li>
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
                        <h1>INCOM te invita a nuestro próximo evento</h1>
                        <h2>CUPRUM llega a INCOM</h2>
                        <h4>Presencial | En línea</h4>
                        <h2>Jueves 09 de febrero | 10:00 horas México</h2>
                        <p>Plutarco Elías Calles 276, Col. Tlazintla, Iztacalco, CDMX</p>
                        <div class="text-center text-lg-start">
                            <a href="#contact" class="btn-get-started scrollto bg-danger">¡Inscríbete aquí!</a>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 order-1 order-lg-2 hero-img" data-aos="zoom-out" data-aos-delay="300">
                    <img src="assets/img/INCOM_WEBINAR_CUPRUM_LP1.png" class="img-fluid animated" alt="Webinar FREMCO soplado de fibra óptica">
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
                    <p>Detalles</p>
                </div>
                <div class="row" data-aos="fade-left">
                    <div class="d-flex flex-column flex-lg-row justify-content-evenly align-items-center">
                        <div class="d-flex flex-column justify-content-center align-items-center rounded border p-4 h-100" style="width: 320px">
                            <div class="px-2" data-aos="zoom-in" data-aos-delay="100">
                                <div class="d-flex justify-content-center align-items-center my-4">
                                    <span style="background-color: rgba(4, 52, 101, 1); border-radius: 100%; padding: 1rem">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="72" height="72" viewBox="0 0 24 24" style="fill: rgba(255, 255, 255, 1);">
                                            <path d="M7 11h2v2H7zm0 4h2v2H7zm4-4h2v2h-2zm0 4h2v2h-2zm4-4h2v2h-2zm0 4h2v2h-2z"></path>
                                            <path d="M5 22h14c1.103 0 2-.897 2-2V6c0-1.103-.897-2-2-2h-2V2h-2v2H9V2H7v2H5c-1.103 0-2 .897-2 2v14c0 1.103.897 2 2 2zM19 8l.001 12H5V8h14z"></path>
                                        </svg>
                                    </span>
                                </div>
                                <div class="text-center">
                                    <h4>Fecha</h4>
                                    <div class="">
                                        <p class="my-0">Jueves 09 de febrero | 10:00 horas</p>
                                        <p class="my-0">Zona horaria: CDMX (GMT-6)</p>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="d-flex flex-column justify-content-center align-items-center rounded border p-4 mt-4 mt-lg-0 h-100" style="width: 320px">
                            <div class="px-2" data-aos="zoom-in" data-aos-delay="100">
                                <div class="d-flex justify-content-center align-items-center my-4">
                                    <span style="background-color: rgba(4, 52, 101, 1); border-radius: 100%; padding: 1rem">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="72" height="72" viewBox="0 0 24 24" style="fill: rgba(255, 255, 255, 1);">
                                            <path d="M18 7c0-1.103-.897-2-2-2H4c-1.103 0-2 .897-2 2v10c0 1.103.897 2 2 2h12c1.103 0 2-.897 2-2v-3.333L22 17V7l-4 3.333V7zm-1.998 10H4V7h12l.001 4.999L16 12l.001.001.001 4.999z"></path></svg>
                                    </span>
                                </div>
                                <div class="text-center">
                                    <h4>Asistencia</h4>
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
                                    <span style="background-color: rgba(4, 52, 101, 1); border-radius: 100%; padding: 1rem">
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
                        </div>
                        <div class="d-flex flex-column justify-content-center align-items-center rounded border p-4 mt-4 mt-lg-0 h-100" style="width: 320px">
                            <div class="px-2" data-aos="zoom-in" data-aos-delay="100">
                                <div class="d-flex justify-content-center align-items-center my-4">
                                    <span style="background-color: rgba(4, 52, 101, 1); border-radius: 100%; padding: 1rem; color: #fff">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="72" height="72" fill="currentColor" class="bi bi-building-fill-gear" viewBox="0 0 16 16">
                                            <path d="M2 1a1 1 0 0 1 1-1h10a1 1 0 0 1 1 1v7.256A4.493 4.493 0 0 0 12.5 8a4.493 4.493 0 0 0-3.59 1.787A.498.498 0 0 0 9 9.5v-1a.5.5 0 0 0-.5-.5h-1a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .39-.187A4.476 4.476 0 0 0 8.027 12H6.5a.5.5 0 0 0-.5.5V16H3a1 1 0 0 1-1-1V1Zm2 1.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1a.5.5 0 0 0-.5.5Zm3 0v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1a.5.5 0 0 0-.5.5Zm3.5-.5a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1ZM4 5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1a.5.5 0 0 0-.5.5ZM7.5 5a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1Zm2.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1a.5.5 0 0 0-.5.5ZM4.5 8a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1Z" />
                                            <path d="M11.886 9.46c.18-.613 1.048-.613 1.229 0l.043.148a.64.64 0 0 0 .921.382l.136-.074c.561-.306 1.175.308.87.869l-.075.136a.64.64 0 0 0 .382.92l.149.045c.612.18.612 1.048 0 1.229l-.15.043a.64.64 0 0 0-.38.921l.074.136c.305.561-.309 1.175-.87.87l-.136-.075a.64.64 0 0 0-.92.382l-.045.149c-.18.612-1.048.612-1.229 0l-.043-.15a.64.64 0 0 0-.921-.38l-.136.074c-.561.305-1.175-.309-.87-.87l.075-.136a.64.64 0 0 0-.382-.92l-.148-.045c-.613-.18-.613-1.048 0-1.229l.148-.043a.64.64 0 0 0 .382-.921l-.074-.136c-.306-.561.308-1.175.869-.87l.136.075a.64.64 0 0 0 .92-.382l.045-.148ZM14 12.5a1.5 1.5 0 1 0-3 0 1.5 1.5 0 0 0 3 0Z" />
                                        </svg>
                                    </span>
                                </div>
                                <div class="text-center">
                                    <h4>Sector</h4>
                                    <div class="">
                                        <p class="my-0">
                                            Principalmente para uso industrial, telecomunicaciones y eléctrico 
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- End Team Section -->

        <!-- ======= Contact Section ======= -->
        <section id="contact" class="contact">
            <div class="container">
                <div class="section-title" data-aos="fade-up">
                    <h2>Contáctanos</h2>
                    <p>Regístrate</p>
                </div>
                <div class="row">
                    <div class="col-lg-4" data-aos="fade-right" data-aos-delay="100">
                        <div class="info">
                            <div class="email">
                                <i class="bi bi-envelope"></i>
                                <h4>Correo electrónico:</h4>
                                <a href="mailto:comunicacion@incom.mx">comunicacion@incom.mx</a>
                            </div>
                            <div class="address">
                                <i class="bi bi-phone"></i>
                                <h4>Teléfono:</h4>
                                <a href="tel:5552436900">(55) 5243-6900</a>
                            </div>
                            <div class="phone">
                                <i class="bi bi-whatsapp"></i>
                                <h4>WhatsApp</h4>
                                <a href="https://api.whatsapp.com/send?phone=525530327332" target="_blank">+52 (52) 3032-7332</a>
                            </div>
                            <div class="phone">
                                <i class="bi bi-geo-alt"></i>
                                <h4>Ubicación</h4>
                                <a href="https://www.incom.mx/informacion/ubicacion-y-sucursales.aspx#Ubicaciones" target="_blank">INCOM Ciudad de México</a>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-8 mt-5 mt-lg-0" data-aos="fade-left" data-aos-delay="200">
                        <form runat="server" class="php-email-form">
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="d-flex flex-column justify-content-center align-items-center">
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <asp:Label ID="lbl_nombre" AssociatedControlID="txt_nombre" Text="Nombre: * " runat="server"></asp:Label>
                                                <asp:TextBox class="form-control w-75" ID="txt_nombre" required="true" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <asp:Label ID="lbl_email" AssociatedControlID="txt_email" Text="Correo electrónico: * " runat="server"></asp:Label>
                                                <asp:TextBox class="form-control w-75" ID="txt_email" required="true" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <asp:Label ID="lbl_telefono" AssociatedControlID="txt_telefono" Text="Teléfono: " runat="server"></asp:Label>
                                                <asp:TextBox class="form-control w-75" ID="txt_telefono" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <asp:Label ID="lbl_empresa" AssociatedControlID="txt_empresa" Text="Empresa: " runat="server"></asp:Label>
                                                <asp:TextBox class="form-control w-75" ID="txt_empresa" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <asp:Label ID="lbl_asistencia" AssociatedControlID="ddl_asistencia" Text="Tipo de asistencia: * " runat="server"></asp:Label>
                                                <asp:DropDownList ID="ddl_asistencia" AutoPostBack="false" CssClass="w-75" required="true" runat="server">
                                                    <asp:ListItem Selected="True" Value="">--</asp:ListItem>
                                                    <asp:ListItem Value="Presencial">Presencial</asp:ListItem>
                                                    <asp:ListItem Value="Online">En línea</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <asp:Label ID="lbl_mensaje" runat="server"></asp:Label>
                                        <asp:Button Text="Registrarme" ID="btn_enviar" OnClientClick="btn_click(this)" class="btn btn-success rounded" runat="server" OnClick="btn_enviar_Click" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </form>
                    </div>

                </div>

            </div>
        </section>
        <!-- End Contact Section -->

        <!-- ======= Features Section ======= -->
        <section id="features" class="features">
            <div class="container">
                <div class="section-title" data-aos="fade-up">
                    <h2>Conoce el</h2>
                    <p>Temario de la presentación</p>
                </div>
                <div class="row" data-aos="fade-left">
                    <div class="col col-lg-12">
                        <div class="icon-box d-flex justify-content-center align-items-center" data-aos="zoom-in" data-aos-delay="50">
                            <ul style="list-style-type: square">
                                <li>Tipo de escaleras</li>
                                <li>Planta Cuprum</li>
                                <li>Normas</li>
                                <li>Uso de las escaleras</li>
                                <li>Cómo elegir una escalera</li>
                            </ul>
                        </div>
                    </div>
                    <%--<div class="col col-lg-6">
                        <div class="icon-box d-flex justify-content-center align-items-center" data-aos="zoom-in" data-aos-delay="50">
                            <ul style="list-style-type: square">
                                <li>Llevando a cabo el soplado</li>
                                <li>Casos de éxito</li>
                                <li>Pruebas en planta</li>
                                <li>Pruebas en campo</li>
                                <li>Tiempo y costos</li>
                                <li>Circuito de soplado</li>
                            </ul>
                        </div>
                    </div>--%>
                </div>
            </div>
            <div class="container">
                <div class="row content">
                    <div class="d-flex flex-column flex-lg-row justify-content-between align-items-center">
                        <div class="col-md-4 order-1 order-md-2 my-4" data-aos="fade-left">
                            <img src="assets/img/INCOM_WEBINAR_CUPRUM_LP2.jpg" class="img-fluid" alt="Soplado de fibra óptica">
                        </div>
                        <div class="col-md-7 pt-5 order-2 order-md-1" data-aos="fade-up">
                            <h3>ESCALERAS CUPRUM</h3>
                            <strong class="text-justify">Grupo Cuprum es una empresa mexicana fundada en 1948 en Monterrey, México, dedicada a la transformación y comercialización de aluminio, que ha ofrecido productos de la más alta calidad por más de 70 años.
                            </strong>
                            <p class="text-justify">
                                Escaleras Cuprum es el principal fabricante de escaleras en México y uno de los más grandes en el mundo, siendo exportador de escaleras a nivel global.
                           
                            </p>
                            <p class="text-justify">
                                Cuenta con una capacidad de producción de millones de escaleras al año, utilizando diversos materiales como el aluminio, fibra de vidrio y acero para la fabricación de escaleras en diferentes configuraciones y capacidades de carga. 
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- End Features Section -->

        <!-- ======= Exhibitor Section ======= -->
        <section id="exhibitor" class="testimonials">
            <div class="container">
                <div class="testimonials-slider swiper">
                    <div class="swiper-wrapper">
                        <div class="swiper-slide">
                            <div class="testimonial-item">
                                <img src="assets/img/INCOM_WEBINAR_CUPRUM_LP3.png" class="" alt="">
                                <h3>Miguel Ángel Hernández Bautista</h3>
                                <h4>Asesor comercial CUPRUM</h4>
                                <%--<p>
                                    <i class="bx bxs-quote-alt-left quote-icon-left"></i>
                                    La técnica de soplado es la técnica más eficiente y segura para instalar fibra óptica y con las máquinas de soplado de FREMCO podemos aprovechar grandes ventajas. 
                                    <i class="bx bxs-quote-alt-right quote-icon-right"></i>
                                </p>--%>
                            </div>
                        </div>
                        <!-- End testimonial item -->
                    </div>
                    <%--<div class="swiper-pagination"></div>--%>
                </div>

            </div>
        </section>
        <!-- End Exhibitor Section -->

        <!-- ======= Video Section ======= -->
        <section id="video" class="about">
            <div class="container-fluid">
                <div class="row">
                    <div class="col video-box d-flex justify-content-center align-items-stretch" data-aos="fade-right">
                        <iframe width="560" height="315" src="https://www.youtube.com/embed/GIPZbE8f1BY" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>
                    </div>
                </div>
            </div>
        </section>
        <!-- End Video Section -->

    </main>
    <!-- End #main -->

    <!-- ======= Footer ======= -->
    <footer id="footer">
        <div class="footer-top">
            <div class="container">
                <div class="row">
                    <div class="col-lg-4 col-md-6">
                        <img src="assets/img/incom.png" alt="INCOM La ferretera de las telecomunicaciones" />
                    </div>
                    <div class="col-lg-4 col-md-6 footer-links">
                        <h4>Redes</h4>
                        <ul>
                            <li><i class="bx bxl-facebook"></i><a href="https://www.facebook.com/incommexico/" target="_blank">Facebook</a></li>
                            <li><i class="bx bxl-twitter"></i><a href="https://twitter.com/incom_mx" target="_blank">Twitter</a></li>
                            <li><i class="bx bxl-instagram"></i><a href="https://www.instagram.com/incom_mx/" target="_blank">Instagram</a></li>
                            <li><i class="bx bxl-youtube"></i><a href="https://www.youtube.com/user/incommx" target="_blank">YouTube</a></li>
                        </ul>
                    </div>

                    <div class="col-lg-4 col-md-6 footer-links">
                        <h4>INCOM</h4>
                        <ul>
                            <li><i class="bx bx-chevron-right"></i><a href="https://www.incom.mx/productos?Length=15" target="_blank">Tienda</a></li>
                            <li><i class="bx bx-chevron-right"></i><a href="http://blog.incom.mx/" target="_blank">Blog</a></li>
                            <li><i class="bx bx-chevron-right"></i><a href="https://www.incom.mx/glosario/A" target="_blank">Glosario</a></li>
                            <li><i class="bx bx-chevron-right"></i><a href="https://www.incom.mx/ense%C3%B1anza/infograf%C3%ADas.aspx" target="_blank">Infografías</a></li>
                        </ul>
                    </div>

                    <%--                    <div class="col-lg-4 col-md-6 footer-links">
                        <h4>Catálogos</h4>
                        <ul>
                            <li><i class="bx bx-chevron-right"></i><a href="https://www.incom.mx/documents/pdf/INCOM_CATALOGO_SOLUCION_FIBRA_SOPLADA.pdf" target="_blank">Solución fibra soplada</a></li>
                            <li><i class="bx bx-chevron-right"></i><a href="https://www.incom.mx/documents/pdf/CATALOGO_MAQUINAS_PARA_SOPLADO_FREMCO.pdf" target="_blank">Máquinas de soplado</a></li>
                            <li><i class="bx bx-chevron-right"></i><a href="https://www.incom.mx/documents/pdf/CATALOGO_INCOM_SUBTERRANEO.pdf" target="_blank">Instalación subterranea</a></li>
                            <li><i class="bx bx-chevron-right"></i><a href="https://www.incom.mx/documents/pdf/ICOPTIKS_CATALOGO_SOLUCIONES_PARA_FIBRA_OPTICA.pdf" target="_blank">Redes de fibra óptica</a></li>
                        </ul>
                    </div>--%>
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

</html>
