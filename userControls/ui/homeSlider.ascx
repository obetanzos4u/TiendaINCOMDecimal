<%@ Control Language="C#" AutoEventWireup="true" CodeFile="homeSlider.ascx.cs" Inherits="tienda.homeSlider" %>


 
<%--            <div style="text-align: right; overflow: hidden;">
                <span class="btn-ocultar-mostrarSliderHome">Mostrar/Ocultar avisos</span>

            </div>  --%>
            <div id="slider_home_principal">  
                <div id="bxsliderHome" class="bxslider homeSlider" runat="server">
                </div>
            </div>
<script>


    //function validarslider() {
    //var btnToggle = document.querySelector(".btn-ocultar-mostrarSliderHome");
    //btnToggle.addEventListener("click", function () {

    //    toggleSlider();
    //}, false); 

    //var sliderHomeVisible = localStorage.getItem('sliderHomeVisible');


    //if (sliderHomeVisible == "hide") {
    //    $('#slider_home_principal').hide();

    //} else {
    //    $('#slider_home_principal').show();
    //}

    //}


    function toggleSlider() {
        var btnToggle = document.querySelector(".btn-ocultar-mostrarSliderHome");

        btnToggle.innerText = 'Mostrar/Ocultar avisos';

 


        if (localStorage.getItem("sliderHomeVisible") === null) {
            localStorage.setItem("sliderHomeVisible", "hide");
        } else {

           var sliderHomeVisible = localStorage.getItem("sliderHomeVisible") 
          

            if (sliderHomeVisible == "hide") {
                $('#slider_home_principal').show(); 
  
               
                localStorage.setItem("sliderHomeVisible", "visible");
             
            } else {
                $('#slider_home_principal').hide(); 
                localStorage.setItem("sliderHomeVisible", "hide");

            }
        }

    }


    function loadSlider() {
        $('.homeSlider.bxslider').bxSlider({
            mode: 'fade',
            captions: false,
            auto: true,
            autoStart: true,
            adaptiveHeight: true,
            responsive: true,
            touchEnabled: false,
        });

    }

    $(function () {
        $(document).ready(function () {


            loadSlider();
            validarslider();
            animacion = function () {

                document.querySelector('.btn-ocultar-mostrarSliderHome').classList.toggle('opacity');
            }

            setInterval(animacion, 500);


        });
    });
</script>

<style>
   .btn-ocultar-mostrarSliderHome {  
padding: 5px; 
color: #2e6acb; 
margin: 14px 0px; 
background: whitesmoke;
cursor: pointer;
  transition: opacity 1s;
}

.btn-ocultar-mostrarSliderHome.opacity {
  opacity:0.5;
}
</style>
