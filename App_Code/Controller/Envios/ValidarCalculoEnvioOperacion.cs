using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ValidarEnvioOperacion
/// </summary>
public class ValidarCalculoEnvioOperacion
{
    string Numero_Operacion { get; set; }

    /// <summary>
    ///  Valores: [cotizacion][pedido]
    /// </summary>
    string TipoOperacion { get; set; }

    public bool OperacionValida { get {return _OperacionValida; } }
    private bool _OperacionValida { get; set; }
    public string Message { get { return _Message; } }
    private string _Message { get; set; }
 

    public ValidarCalculoEnvioOperacion(string _Numero_Operacion, string _TipoOperacion) { 


        Numero_Operacion = _Numero_Operacion;
        TipoOperacion = _TipoOperacion;

        switch (TipoOperacion)
        {
            case "cotizacion": validarCotizacion(); break;
            case "pedido": validarPedido(); break;
        
        }
     
    }

    // "Ninguno" "Estándar"  "En Tienda"  "Gratuito" 
    private void validarCotizacion() {

        bool ApiCalculoFletectivado = operacionesConfiguraciones.obtenerEstatusApiFlete();
        string MetodoDeEnvio = cotizaciones.obtenerMetodoDeEnvio(Numero_Operacion);
        bool Calculo_Costo_Envio = cotizaciones.obtenerEstatusCalculo_Costo_Envio(Numero_Operacion);


        #region Validaciones previas (condiciones de la operación)
        if (ApiCalculoFletectivado == false && Calculo_Costo_Envio == true && MetodoDeEnvio == "Estándar")
        {
            _Message = "El cálculo del flete mediante API para resta cotización esta deshabilitado, habitalo o ingreselo manualmente.";
            _OperacionValida = false;
            return;
        }

        if (ApiCalculoFletectivado == true && Calculo_Costo_Envio == true && MetodoDeEnvio != "Estándar")
        {
            _Message = "Cálculo de envio no aplica para entrega en tienda, gratuito o ninguno.";
            _OperacionValida = false;
            return;
        }

        if (ApiCalculoFletectivado == true && Calculo_Costo_Envio == false && MetodoDeEnvio != "Estándar")
        {
            _Message = "El envio no requiere ser calculado.";
            _OperacionValida = true;
            return;
        }

        if (ApiCalculoFletectivado == true && Calculo_Costo_Envio == false && MetodoDeEnvio == "Estándar")
        {
            _Message = "El costo de envío esta en manual, este podría cambiar sin previo aviso.";
            _OperacionValida = false;
            return;
        }

        /* 
      Si el calculo del flete por APi esta activado y la cotización tiene establecido que se calcule automáticamente
      y si el envio es estándar lo establecemos en ninguno y en 0, para que e
      */

        if (ApiCalculoFletectivado == false && Calculo_Costo_Envio == true && MetodoDeEnvio == "Estándar")
        {
            _Message = "El cálculo del flete mediante API esta deshabilitado, ingreselo manualmente.";
            _OperacionValida = false;

            string resultado = cotizaciones.actualizarEnvio(0, "Ninguno", Numero_Operacion);
            bool resultadoTotales = cotizacionesProductos.actualizarTotalStatic(Numero_Operacion);

            return;
        }

        #endregion




        if (ApiCalculoFletectivado == true && Calculo_Costo_Envio == true && MetodoDeEnvio == "Estándar")
            {


            cotizaciones_direccionEnvio DireccionEnvio = new cotizaciones_direccionEnvio();
            var ProductosEnvio = cotizacionesProductos.ObtenerProductosCalculoEnvio(Numero_Operacion);

            using (var db = new tiendaEntities()) {
                DireccionEnvio = db.cotizaciones_direccionEnvio
                  .Where(c => c.numero_operacion == Numero_Operacion)
                  .AsNoTracking()
                  .FirstOrDefault();
            }


            try 
            {
                CalcularEnvioOperacion Envio = new CalcularEnvioOperacion("operacion", Numero_Operacion, DireccionEnvio);

                foreach (ProductoEnvioCalculoModel p in ProductosEnvio)
                {

                    Envio.AgregarProducto(p.Numero_Parte, p.PesoKg, p.Largo, p.Ancho, p.Alto, p.Cantidad, p.RotacionVertical, p.RotacionHorizontal);
                }

                Envio.CalcularEnvio();

               

                if (Envio.IsValidCalculo)
                {
                    // Él envío se calcula en MXN desde el API 
                    decimal envio = (decimal)Envio.CostoEnvio;

                    var monedaCotizacion = cotizaciones.obtenerMonedaOperacion(Numero_Operacion);

                    // Validamos la moneda del pedido, si es en USD, lo convertimos al tipo de cambio
                    if (monedaCotizacion == "USD") envio = (decimal)(envio / tipoDeCambio.obtenerTipoDeCambio());


                  
                    string resultado = cotizaciones.actualizarEnvio(envio, "Estándar", Numero_Operacion);
                    bool resultadoTotales = cotizacionesProductos.actualizarTotalStatic(Numero_Operacion);


                    EnviosIncomReglas AplicarPromo = new EnviosIncomReglas(Numero_Operacion, "cotizacion", ProductosEnvio);

                }
                else
                {
                    string resultado = cotizaciones.actualizarEnvio(0, "Ninguno", Numero_Operacion);
                    bool resultadoTotales = cotizacionesProductos.actualizarTotalStatic(Numero_Operacion);
                }
            

                _Message = Envio.MessageCalculo;
                _OperacionValida = Envio.IsValidCalculo;

                
                return;
            }



            
            catch (Exception ex) {
                // Si ocurre un error al calcularse el envio, lo establecemos en ninguno.
                _Message = ex.Message;
                _OperacionValida = false;

                string resultado = cotizaciones.actualizarEnvio(0, "Ninguno", Numero_Operacion);
                bool resultadoTotales = cotizacionesProductos.actualizarTotalStatic(Numero_Operacion);

                return;
            }



    }


        


    

    }


    // "Ninguno" "Estándar"  "En Tienda"  "Gratuito" 
    private async void validarPedido()
    {

        bool ApiCalculoFletectivado = operacionesConfiguraciones.obtenerEstatusApiFlete();
        string MetodoDeEnvio = pedidosDatos.obtenerMetodoDeEnvio(Numero_Operacion);
        bool Calculo_Costo_Envio = pedidosDatos.obtenerEstatusCalculo_Costo_Envio(Numero_Operacion);


        #region Validaciones previas (condiciones de la operación)
        if (ApiCalculoFletectivado == false && Calculo_Costo_Envio == true && MetodoDeEnvio == "Estándar")
        {
            _Message = "El cálculo del flete mediante API para resta cotización esta deshabilitado, habitalo o ingreselo manualmente.";
            _OperacionValida = false;
            return;
        }

        if (ApiCalculoFletectivado == true && Calculo_Costo_Envio == true && MetodoDeEnvio != "Estándar")
        {
            _Message = "Cálculo de envio no aplica para entrega en tienda, gratuito o ninguno.";
            _OperacionValida = false;
            return;
        }

        if (ApiCalculoFletectivado == true && Calculo_Costo_Envio == false && MetodoDeEnvio != "Estándar")
        {
            _Message = "El envio no requiere ser calculado.";
            _OperacionValida = true;
            return;
        }

        if (ApiCalculoFletectivado == true && Calculo_Costo_Envio == false && MetodoDeEnvio == "Estándar")
        {
            _Message = "El costo de envío esta en manual, este podría cambiar sin previo aviso.";
            _OperacionValida = false;
            return;
        }

        /* 
      Si el calculo del flete por APi esta activado y la cotización tiene establecido que se calcule automáticamente
      y si el envio es estándar lo establecemos en ninguno y en 0, para que e
      */

        if (ApiCalculoFletectivado == false && Calculo_Costo_Envio == true && MetodoDeEnvio == "Estándar")
        {
            _Message = "El cálculo del flete mediante API esta deshabilitado, ingreselo manualmente.";
            _OperacionValida = false;

            string resultado = pedidosDatos.actualizarEnvio(0, "Ninguno", Numero_Operacion,"El costo de envío no ha podido ser calculado, en unos momentos un asesor lo resolverá.");
            bool resultadoTotales = pedidosProductos.actualizarTotalStatic(Numero_Operacion);

            return;
        }

        #endregion




        if (ApiCalculoFletectivado == true && Calculo_Costo_Envio == true && MetodoDeEnvio == "Estándar")
        {


            pedidos_direccionEnvio DireccionEnvio = new pedidos_direccionEnvio();
            var ProductosEnvio = pedidosProductos.ObtenerProductosCalculoEnvio(Numero_Operacion);

            using (var db = new tiendaEntities()) {
                DireccionEnvio = db.pedidos_direccionEnvio
                  .Where(c => c.numero_operacion == Numero_Operacion)
                  .AsNoTracking()
                  .FirstOrDefault();
            }


            try
            {
                CalcularEnvioOperacion Envio = new CalcularEnvioOperacion("operacion", Numero_Operacion, DireccionEnvio);

                foreach (ProductoEnvioCalculoModel p in ProductosEnvio)
                {

                    Envio.AgregarProducto(p.Numero_Parte, p.PesoKg, p.Largo, p.Ancho, p.Alto, p.Cantidad, p.RotacionVertical, p.RotacionHorizontal);
                }

                await Envio.CalcularEnvio();



                if (Envio.IsValidCalculo)
                {
                    // Él envío se calcula en MXN desde el API 
                    decimal envio = (decimal)Envio.CostoEnvio;

                    var pedidoDatosNumericos = PedidosEF.ObtenerNumeros(Numero_Operacion);

                    // Validamos la moneda del pedido, si es en USD, lo convertimos al tipo de cambio
                    if (pedidoDatosNumericos.monedaPedido == "USD") envio = (decimal)(envio / tipoDeCambio.obtenerTipoDeCambio());


                    string resultado = pedidosDatos.actualizarEnvio(envio, "Estándar", Numero_Operacion,"");
                    bool resultadoTotales = pedidosProductos.actualizarTotalStatic(Numero_Operacion);

                    EnviosIncomReglas AplicarPromo = new EnviosIncomReglas(Numero_Operacion, "pedido",  ProductosEnvio);
                   


                }
                else
                {
                    string resultado = pedidosDatos.actualizarEnvio(0, "Ninguno", Numero_Operacion, "Un asesor se pondrá en contacto contigo");
                    bool resultadoTotales = pedidosProductos.actualizarTotalStatic(Numero_Operacion);
                }


                _Message = Envio.MessageCalculo;
                _OperacionValida = Envio.IsValidCalculo;


                return;
            }




            catch (Exception ex)
            {
                // Si ocurre un error al calcularse el envio, lo establecemos en ninguno.
                _Message = ex.ToString();
                _OperacionValida = false;

                string resultado = pedidosDatos.actualizarEnvio(0, "Ninguno", Numero_Operacion,"Un asesor se pondrá en contacto contigo");
                bool resultadoTotales = pedidosProductos.actualizarTotalStatic(Numero_Operacion);

                return;
            }



        }







    }

   

}