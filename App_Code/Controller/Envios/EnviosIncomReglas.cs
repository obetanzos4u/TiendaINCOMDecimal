using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de EnviosIncomReglas
/// </summary>
public class EnviosIncomReglas
{

    /// <summary>
    /// Establece el monto mínimo del pedido para poder ofrecer envío gratis
    /// </summary>
    private readonly decimal MontoEnvioGratuitoMXN = 3000m; //2000
    readonly string tipoOperacion;
    /// <summary>
    ///Establecer el valor de esta variable en false para  cancelar el envío gratis
    /// </summary>
    private bool AplicaEnvioGratis;
    private readonly string numero_operacion;
    readonly decimal? MontoTotalSinImpuestosMXN;
    readonly List<ProductoEnvioCalculoModel> Productos;
    private string ProductosNoAplicanEnvioGratis;
    public json_respuestas Resultado { get { return _Resultado; } }
    public bool PromocionActiva { get { return true; } }

    private json_respuestas _Resultado { get; set; }
    public EnviosIncomReglas(string _numero_operacion, string _tipoOperacion, List<ProductoEnvioCalculoModel> _Productos)
    {
        numero_operacion = _numero_operacion;
        tipoOperacion = _tipoOperacion;
        Productos = _Productos;
        AplicaEnvioGratis = true;
        switch (tipoOperacion.ToLower())
        {
            case "carrito":
                break;
            case "pedido":
                string monedaPedido = PedidosEF.ObtenerMonedaOperacion(numero_operacion);
                if (monedaPedido == "USD")
                {
                    MontoTotalSinImpuestosMXN = (PedidosEF.ObtenerMontoTotalProductos(numero_operacion)) * operacionesConfiguraciones.obtenerTipoDeCambio();
                }
                else
                {
                    MontoTotalSinImpuestosMXN = PedidosEF.ObtenerMontoTotalProductos(numero_operacion);
                }
                break;
            case "cotizacion":
                string monedaCotizacion = CotizacionesEF.ObtenerMonedaOperacion(numero_operacion);
                if (monedaCotizacion == "USD")
                {
                    MontoTotalSinImpuestosMXN = (CotizacionesEF.ObtenerMontoTotalProductos(numero_operacion)) * operacionesConfiguraciones.obtenerTipoDeCambio();
                }
                else
                {
                    MontoTotalSinImpuestosMXN = CotizacionesEF.ObtenerMontoTotalProductos(numero_operacion);
                }
                break;
        }
        Calcular();
    }
    public EnviosIncomReglas(string _numero_operacion, string _tipoOperacion, List<ProductoEnvioCalculoModel> _Productos, decimal _MontoTotalSinImpuestosMXN)
    {
        numero_operacion = _numero_operacion;
        tipoOperacion = _tipoOperacion;
        Productos = _Productos;
        MontoTotalSinImpuestosMXN = _MontoTotalSinImpuestosMXN;
        AplicaEnvioGratis = true;
        Calcular();
    }
    private void Calcular()
    {
        #region Reglas de validación pre cálculo
        if (PromocionActiva == false)
        {
            _Resultado = new json_respuestas(false, "No hay promociones activas", false, null);
            return;
        }
        if (MontoTotalSinImpuestosMXN == null)
        {
            _Resultado = new json_respuestas(false, "No se ha podido calcular el monto total del ", false, null);
            return;
        }
        if (tipoOperacion != "carrito")
        {
            if (string.IsNullOrWhiteSpace(numero_operacion))
            {
                _Resultado = new json_respuestas(false, "No se ha proporcionado un número de operación", true, null);
                return;
            }
        }

        if (Productos == null || Productos.Count <= 0)
        {
            _Resultado = new json_respuestas(false, "No se ha proporcionado un listado de productos", true, null);
            return;
        }
        if (string.IsNullOrWhiteSpace(tipoOperacion))
        {
            _Resultado = new json_respuestas(false, "No se ha proporcionado un tipo de operación", true, null);
            return;
        }

        foreach (var p in Productos)
        {
            if (p.DisponibleParaEnvioGratuito == 0)
            {
                AplicaEnvioGratis = false;
                ProductosNoAplicanEnvioGratis += $"{p.Numero_Parte}, ";
                break;
            }

        }

        if (AplicaEnvioGratis == false)
        {
            _Resultado = new json_respuestas(false, $"Tienes productos que no aplican para tu envío gratuito ({ProductosNoAplicanEnvioGratis}).", false, null);
            return;
        }
        if (MontoTotalSinImpuestosMXN < 3000)
        {
            _Resultado = new json_respuestas(false, "El monto de tu pedido es menor a la promoción de envíos gratuitos, agrega más productos para el envío gratis.", false, null);
            return;
        }
        #endregion
        switch (tipoOperacion.ToLower())
        {
            case "carrito": ActualizarCarrito(); break;
            case "cotizacion": ActualizarCotizacion(); break;
            case "pedido": ActualizarPedido(); break;
        }
    }


    /// <summary>
    /// el carrito no actualiza nada, por lo tanto devuelve el nuevo precio en la variable Result.
    /// </summary>
    private void ActualizarCarrito()
    {
        _Resultado = new json_respuestas(true, "Envío gratis disponible (carrito)", false, 0);
    }
    private void ActualizarCotizacion()
    {
        if (AplicaEnvioGratis == true && MontoTotalSinImpuestosMXN >= MontoEnvioGratuitoMXN)
        {
            cotizaciones.actualizarEnvio(0, "Gratuito", numero_operacion, "Tu operación aplica para envío gratis.");
            bool resultadoTotales = cotizacionesProductos.actualizarTotalStatic(numero_operacion);

            if (resultadoTotales == true)
            {
                _Resultado = new json_respuestas(true, "Descuento aplicado correctamente", false, null);
            }

        }

    }
    private void ActualizarPedido()
    {


        if (AplicaEnvioGratis == true && MontoTotalSinImpuestosMXN >= MontoEnvioGratuitoMXN)
        {
            pedidosDatos.actualizarEnvio(0, "Gratuito", numero_operacion, "Tu operación aplica para envío gratis.");
            bool resultadoTotales = pedidosProductos.actualizarTotalStatic(numero_operacion);

            if (resultadoTotales == true)
            {
                _Resultado = new json_respuestas(true, "Descuento aplicado correctamente", false, null);
            }

        }

    }
}