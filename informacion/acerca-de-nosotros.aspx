<%@ Page Language="C#" AutoEventWireup="true" Async="true"
CodeFile="acerca-de-nosotros.aspx.cs"
MasterPageFile="~/general.master" Inherits="acerca_de_nosotros" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
  <div class="main-acerca-nosotros">
    <div class="row">
        <div class="container-nosotros">
          <div class="sipnosis-nosotros">
            <img src="../img/webUI/newdesign/Incom_mediano.png" alt="">
            <p class="title-nosotros">¿Quiénes somos?</p>
            <p class="content-sipnosis">Incom es una empresa orgullosamente mexicana con experiencia en el sector
                desde 1999, cubriendo las necesidades de las empresas en los ramos de
                telecomunicaciones, construcción, integración de TI, televisión por cable, radio,
                internet, telefonía, voz/datos, seguridad industrial y ferretería.</p>
          </div>
          <div class="portada-nosotros">
            <img src="../img/informacion/PORTADA.jpg" alt="">
          </div>
        </div>
        <div class="cualidades-nosotros">
            <img src="../img/informacion/MARCAS50.png" alt="">
            <img src="../img/informacion/PLATAS4.png" alt="">
            <img src="../img/informacion/MAQUINARIA.png" alt="">
            <img src="../img/informacion/CIRCUITO.png" alt="">
            <img src="../img/informacion/ASESORIA.png" alt="">
        </div>
        <div class="visitanos-nosotros">
            <p class="title-nosotros">Visítanos</p>
            <p class="content-visitanos">Estamos comprometidos en brindarle cada vez más y mejores servicios, mediante un esfuerzo diario basado en programas de
                mejora continua, desarrollo tecnológico y constante capacitación. <br>
                Nuestras oficinas se encuentran ubicadas en la ciudad de México, cuentan con almacén, unidades de reparto, sala de conferencias,
                auditorio, sala de exhibición y punto de venta. <br>
                Brindamos soporte técnico y asesoría gratuita personal, vía telefónica y medios electrónicos a fin de satisfacer cualquier duda.
                Somos: La Ferretera de las Telecomunicaciones® <br>
                Contamos líneas de financiamiento por medio de un tercero, para apoyar a nuestros clientes.</p>
        </div>
        <div class="img-visitanos">
            <img src="../img/informacion/CAMION.jpg" alt="">
            <img src="../img/informacion/ALMACEN.jpg" alt="">
            <img src="../img/informacion/FACHADA1.jpg" alt="">
        </div>
        <p class="title-nosotros">Nuestras marcas</p>
        <div class="nuestras_marcas-nosotros">
          <a href="">
            <img src="../img/informacion/ICOPTIKS-INFO.jpg" alt="">
          </a>
          <a href="./icoplastik.aspx">
            <img src="../img/informacion/ICOPLASTIK-INFO.jpg" alt="">
          </a>
          <a href="">
            <img src="../img/informacion/METALICO-INFO.jpg" alt="">
          </a>
          <a href="">
            <img src="../img/informacion/TULIKO-INFO.jpg" alt="">
          </a>
          <a href="">
            <img src="../img/informacion/POLYMERICO-INFO.jpg" alt="">
          </a>
        </div>
    </div>
  </div>

  <style>
    .main-acerca-nosotros {
      width: 80%;
      max-width: 1600px;
      margin: auto;
    }

    .container-nosotros {
      display: inline-flex;
      padding-top: 2rem;
      margin-bottom: 3rem;
    }

    .sipnosis-nosotros {
      width: 100%;
      display: flex;
      flex-direction: column;
      justify-content: center;
    }

    .portada-nosotros {
      width: 75%;
      margin: auto;
      display: flex;
      justify-content: center;
    }

    .title-nosotros {
      font-size: 1.5rem;
      font-weight: 600;
      padding-left: 1.5rem;
    }

    .content-sipnosis, .content-visitanos {
      font-size: 1.25rem;
      padding-left: 1.5rem;
      padding-right: 2rem;
    }

    .cualidades-nosotros {
      display: flex;
      justify-content: center;
      margin-top: 2rem;
    }

    .cualidades-nosotros img {
      width: 16%;
      height: auto;
      margin-top: 2rem;
    }

    .visitanos-nosotros {
      display: flex;
      flex-direction: column;
      justify-content: center;
      margin-top: 2rem;
      margin-bottom: 1rem;
    }

    .img-visitanos {
      display: flex;
      justify-content: space-between;
      margin-bottom: 4rem;
      padding-right: 1rem;
      padding-left: 1rem;
    }

    .sipnosis-nosotros img {
      width: 100%;
      max-width: 450px;
      margin-bottom: 3rem;
    }

    .nuestras_marcas-nosotros {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-left: 1rem;
      margin-bottom: 6rem;
    }

    .nuestras_marcas-nosotros a {
      /* flex: 0 0 18%;
      margin: 0; */
      display: flex;
      margin: auto;
    }

    .nuestras_marcas-nosotros img {
      width: 85%;
      height: auto;
      margin: auto;
    }

    .portada-nosotros > img:nth-child(1) {
      width: 100%;
      max-width: 450px;
    }
    
    @media only screen and (max-width: 500px) {
      .nuestras_marcas-nosotros a {
        object-fit: contain;
        height: 100px;
      }
    }

    @media only screen and (min-width: 500px) and (max-width: 650px) {
      .nuestras_marcas-nosotros a {
        object-fit: contain;
        height: 130px;
      }
    }

    @media only screen and (max-width: 650px) {
    .container-nosotros {
      flex-direction: column;
      margin-bottom: 1rem;
    }

    /* .sipnosis-nosotros {
      width: 100%;
    } */

    .sipnosis-nosotros img {
      width: 90%;
      margin: auto;
      margin-bottom: 3rem;
      margin-left: 0px;
    }

    .content-sipnosis, .content-visitanos {
      font-size: 0.85rem;
    }

    .title-nosotros {
      font-size: 1rem;
      margin: 0;
    }

    .portada-nosotros {
      width: 90%;
    }

    .cualidades-nosotros {
      flex-direction: column;
      width: 80%;
      margin: auto;
    }

    .cualidades-nosotros img {
      width: 40%;
      margin: auto;
    }

    .img-visitanos {
      width: 60%;
      flex-direction: column;
      margin: auto;
      margin-bottom: 2rem;
    }

    .nuestras_marcas-nosotros {
      margin: 0 1rem;
      flex-direction: column;
    }

    .nuestras_marcas-nosotros img {
      width: 50%;
      margin: auto;
    }

    .sipnosis-nosotros > img:nth-child(1) {
      display: none;
    }

    .nuestras_marcas-nosotros > img:nth-child(2) {
      margin-top: -2rem;
    }

    .nuestras_marcas-nosotros > img:nth-child(3) {
      margin-top: -2rem;
    }

    .nuestras_marcas-nosotros > img:nth-child(4) {
      margin-top: -1rem;
    }

    p.title-nosotros:nth-child(5) {
      z-index: 1;
      position: absolute;
    }
    }

    @media only screen and (min-width: 650px) { 
      .img-visitanos > img {
        width: 30%;
        height: auto;
      }
    }

    @media only screen and (min-width: 650px) and (max-width: 1000px) {
      .container-nosotros {
        flex-direction: column;
      }

      .sipnosis-nosotros img {
        width: 100%;
      }

      .cualidades-nosotros img {
        width: 19%;
      }

      .cualidades-nosotros {
        justify-content: flex-start;
      }

      .portada-nosotros {
        width: 80%;
      }

      .portada-nosotros > img {
        width: 50%;
      }

      .img-visitanos {
        justify-content: space-evenly;
        flex-direction: row;
      }

      /* .nuestras_marcas-nosotros img {
        width: 20%;
      } */

      .nuestras_marcas-nosotros {
        margin: 1rem;
      }

      .img-visitanos > img {
        width: 30%;
        height: auto;
      }

      .img-visitanos > img:nth-child(2) {
          width: 30%;
        height: auto;
      }

      .img-visitanos > img:nth-child(3) {
        width: 30%;
        height: auto;
      }
    }

    @media only screen and (max-width: 1600px) {
      .sipnosis-nosotros img {
        width: 100%;
        margin-bottom: 3rem;
      }
    }
</style>

</asp:Content>

