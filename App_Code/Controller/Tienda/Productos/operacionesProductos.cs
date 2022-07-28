using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Clase para manipular los productos a agregar a el carrito, cotización o pedido.
/// </summary>
public class operacionesProductos {

    public bool resultado_operacion { get; set; }
    public string mensaje_ResultadoOperacion { get; set; }

    private string tipoOperacion { get; set; }
    private string tipoAdd { get; set; }
    private string numero_operacion { get; set; }
    private string cantidad { get; set; }
    private string numero_parte { get; set; }
    private string moneda { get; set; }
    private decimal cantidadF { get; set; }
    private decimal precio = decimal.MinusOne;
   
    private preciosTienda procesarPrecios;
    private model_productosTienda productoTienda;


    public operacionesProductos(string _tipoOperacion, string _tipoAdd, string _numero_operacion, string _numero_parte, string _cantidad, string _moneda) {
        tipoOperacion = _tipoOperacion;
        tipoAdd = _tipoAdd;
        numero_operacion = _numero_operacion;
        numero_parte = _numero_parte;
        cantidad = _cantidad;
        moneda = _moneda;
        procesarPrecios = new preciosTienda();
        }

    public decimal obtenerPrecioUnitario() { return precio; }
    public decimal obtenerCantidad() { return cantidadF; }
    public model_productosTienda obtenerProducto() { return productoTienda; }

    public async Task agregarProductoAsync() {
        usuarios datosUsuario = usuarios.modoAsesor();
        if (validarCantidad() == false)
        {
            mensaje_ResultadoOperacion = "Debe ingresar valores númericos y mayores a 0";
            resultado_operacion = false;

            return;
        }

        var ValidarCantidad = await ProductosBloqueoCantidades.ValidarCantidadAsync(numero_parte, cantidadF);

        
        if (ValidarCantidad.result == false)
        {
            mensaje_ResultadoOperacion = ValidarCantidad.message;
            resultado_operacion = false;

            return;
        }

        productosTienda obtener = new productosTienda();
            DataTable dt_Producto = obtener.obtenerProducto(numero_parte);

            preciosTienda procesar = new preciosTienda();
            procesar.monedaTienda = moneda;

            //Procesamos precios, visibilidad etc.
            dt_Producto = procesar.procesarProductos(dt_Producto);


            // Si paso los filtros procedemos a realizar otras acciones
            if (dt_Producto != null && dt_Producto.Rows.Count >= 1) {

                // ***** Validación de existencia en operación y para realizar insert o update con su actualización de cantidad si fuera este último caso (update).
                bool productoEnOperacion = false;
                bool resultado = false;
                DataTable dtProductosEnOperacion = new DataTable();  

                switch (tipoOperacion) {

                    case "carrito":
                        carrito add = new carrito();
                        dtProductosEnOperacion = add.obtenerCarritoProducto(datosUsuario.email, numero_parte);
                        productoEnOperacion = dtProductosEnOperacion != null && dtProductosEnOperacion.Rows.Count >= 1;
                
                    productoTienda = obtener.dtProductoToList(dt_Producto);
                   

                    // si el producto ya esta en el carrito, realizamos un UPDATE
                    if (productoEnOperacion == true) {
                       
                        precio = precioTabulado(productoTienda);
                        resultado = actualizarProductoCarrito();

                        if (resultado == true) {
                            mensaje_ResultadoOperacion = "El producto se ha actualizado";
                            resultado_operacion =  true;
                            } else {
                            mensaje_ResultadoOperacion = "El producto no puede ser actualizado";
                            resultado_operacion = false;
                            }
                        }
                    // Si el producto no esta en la operacion es hacer un INSERT
                    else {
                        precio = precioTabulado(productoTienda);
                        resultado = agregarProductoCarrito();
                        if (resultado == true) {
                            mensaje_ResultadoOperacion = "El producto se ha agregado con éxito";
                            resultado_operacion = true;
                            } else {
                            mensaje_ResultadoOperacion = "Error: El producto no pudo ser agregado.";
                            resultado_operacion = false;
                            }
                        }

                    break;

                    case "cotizacion": 
                        dtProductosEnOperacion = cotizacionesProductos.obtenerProducto(numero_operacion, numero_parte);
                        productoEnOperacion = dtProductosEnOperacion != null && dtProductosEnOperacion.Rows.Count >= 1;
                    productoTienda = obtener.dtProductoToList(dt_Producto);

                    // si el producto ya esta en la operación, realizamos un UPDATE
                         if (productoEnOperacion == true) {
                     
                        if (tipoAdd == "sum") cantidadF = cantidadF + decimal.Parse(dtProductosEnOperacion.Rows[0]["cantidad"].ToString());
                        precio = precioTabulado(productoTienda);
                        resultado = actualizarProductoCotizacion();

                            if (resultado == true) {
                            mensaje_ResultadoOperacion = "El producto se ha actualizado";
                            resultado_operacion = true;
                                } else {
                            mensaje_ResultadoOperacion = "El producto no puede ser actualizado";
                            resultado_operacion = false;
                                }
                        }
                         // Si el producto no esta en la operacion es hacer un INSERT
                         else {
                            precio = precioTabulado(productoTienda);
                            resultado = agregarProductoCotizacion();
                            if (resultado == true) {
                            mensaje_ResultadoOperacion = "El producto se ha agregado con éxito";
                            resultado_operacion = true;
                                } else {
                            mensaje_ResultadoOperacion = "Error: El producto no pudo ser agregado.";
                            resultado_operacion = false;
                                }
                        }

                    break;

                    case "pedido": 
                        dtProductosEnOperacion = pedidosProductos.obtenerProducto(numero_operacion, numero_parte);
                        productoEnOperacion = dtProductosEnOperacion != null && dtProductosEnOperacion.Rows.Count >= 1; 
                    productoTienda = obtener.dtProductoToList(dt_Producto);

                        // si el producto ya esta en la operación, realizamos un UPDATE
                         if (productoEnOperacion == true) {
                        if (tipoAdd == "sum") cantidadF = cantidadF + decimal.Parse(dtProductosEnOperacion.Rows[0]["cantidad"].ToString());
                            precio = precioTabulado(productoTienda);
                            resultado = actualizarProductoPedido();

                        if (resultado == true) {
                            mensaje_ResultadoOperacion = "El producto se ha actualizado";
                            resultado_operacion  =  true;
                            } else {
                            mensaje_ResultadoOperacion = "Error: El producto no puede ser actualizado";
                            resultado_operacion = false;
                            }
                        } 
                         // Si el producto no esta en la operacion es hacer un INSERT
                         else {
                            precio = precioTabulado(productoTienda);
                            resultado = agregarProductoPedido();
                            if (resultado == true) {
                            mensaje_ResultadoOperacion = "El producto se ha agregado al pedido";
                            resultado_operacion =  true;
                                } else {
                            mensaje_ResultadoOperacion = "Error: El producto no pudo ser agregado al pedido";
                            resultado_operacion = false;
                                }
                        }
                    break;

                    }
                } 
            // Si el producto es nulo en cantidad o no fué encontrado en la DB
            else {
                resultado_operacion = false;
                mensaje_ResultadoOperacion = "Error: El producto no puede ser agregado";
            }


             
      
        }
    
    private decimal precioTabulado (model_productosTienda producto ) {
        decimal precio = decimal.MinusOne;
        // Si el producto no tiene la propiedad precio (no encontró precio fijo) [columna precio = "precio fijo"]
        if (producto.precio <= 0) {
            List<preciosTabulador> preciosTab = new List<preciosTabulador>();

            preciosTab.Add(new preciosTabulador { min = producto.min1, max = producto.max1, precio = producto.precio1 });
            preciosTab.Add(new preciosTabulador { min = producto.min2, max = producto.max2, precio = producto.precio2 });
            preciosTab.Add(new preciosTabulador { min = producto.min3, max = producto.max3, precio = producto.precio3 });
            preciosTab.Add(new preciosTabulador { min = producto.min4, max = producto.max4, precio = producto.precio4 });
            preciosTab.Add(new preciosTabulador { min = producto.min5, max = producto.max5, precio = producto.precio5 });

           

            decimal tipoCambio = operacionesConfiguraciones.obtenerTipoDeCambio();

            // Obtenemos el precio adecuado de acuerdo a la cantidad
            precio = preciosTienda.precioRango(preciosTab, cantidadF);
            //  Procesamos el precio correcto a la moneda, en este caso la de la operación y no de la tienda
            // Recordar que en el momento que se hizo la instancia se establecio la moneda de la operación en la variable "monedaTienda"
            precio = procesarPrecios.precio_a_MonedaOperacion(producto.moneda_rangos, moneda, precio);

            } else if (producto.precio > 0) {
            precio = procesarPrecios.precio_a_MonedaOperacion(producto.moneda_fija, moneda, producto.precio);
            }


        return precio;
        }


     bool validarCantidad() {
        if (cantidad != "" && cantidad != null) {
            try {
                cantidadF = textTools.soloNumerosD(cantidad);

             
                }
            catch (Exception ex) {
               
               
                return false;
                }

            } else {
            
            

            return false;
            }

        if (cantidadF >= 1)
            return true;
        else {
            return false;
            }

       ;
          
        }

    private bool actualizarProductoCarrito() {


        model_cotizacionesProductos productoCarrito = new model_cotizacionesProductos();
        carrito add = new carrito();
        usuarios datosUsuario = usuarios.modoAsesor();
        decimal tipoCambio = operacionesConfiguraciones.obtenerTipoDeCambio();

        DateTime fechaActual = utilidad_fechas.obtenerCentral();
        productoCarrito.usuario = datosUsuario.email;
        productoCarrito.activo = 1;
        productoCarrito.tipo = 1;
        productoCarrito.fecha_creacion = utilidad_fechas.obtenerCentral();
        productoCarrito.numero_parte = numero_parte;
        productoCarrito.descripcion = productoTienda.descripcion_corta;
        productoCarrito.marca = productoTienda.marca;
        productoCarrito.moneda = moneda;
        productoCarrito.tipo_cambio = tipoCambio;
        productoCarrito.fecha_tipo_cambio = fechaActual;
        productoCarrito.unidad = productoTienda.unidad_venta;
        productoCarrito.precio_unitario = precio;
        productoCarrito.cantidad = cantidadF;
        productoCarrito.precio_total = decimal.Parse((Math.Round(precio, 2) * cantidadF).ToString());
        productoCarrito.stock1 = 0;
        productoCarrito.stock1_fecha = utilidad_fechas.obtenerCentral();



        add.actualizarCantidadCarritoProducto(datosUsuario.email, productoCarrito);

        return true;
        }
    private bool agregarProductoCarrito() {


        model_cotizacionesProductos productoCarrito = new model_cotizacionesProductos();
        carrito add = new carrito();
        usuarios datosUsuario = usuarios.modoAsesor();
        decimal tipoCambio = operacionesConfiguraciones.obtenerTipoDeCambio();

        DateTime fechaActual = utilidad_fechas.obtenerCentral();
        productoCarrito.usuario = datosUsuario.email;
        productoCarrito.activo = 1;
        productoCarrito.tipo = 1;
        productoCarrito.orden = 99;
        productoCarrito.fecha_creacion = utilidad_fechas.obtenerCentral();
        productoCarrito.numero_parte = numero_parte;
        productoCarrito.descripcion = productoTienda.descripcion_corta;
        productoCarrito.marca = productoTienda.marca;
        productoCarrito.moneda = moneda;
        productoCarrito.tipo_cambio = tipoCambio;
        productoCarrito.fecha_tipo_cambio = fechaActual;
        productoCarrito.unidad = productoTienda.unidad_venta;
        productoCarrito.precio_unitario = precio;
        productoCarrito.cantidad = cantidadF;
        productoCarrito.precio_total = decimal.Parse((Math.Round(precio, 2) * cantidadF).ToString());
        productoCarrito.stock1 = 0;
        productoCarrito.stock1_fecha = utilidad_fechas.obtenerCentral();

        add.agregarProductoCarrito(datosUsuario.email, productoCarrito);

        return true;
        }


    private bool actualizarProductoCotizacion() {

        model_cotizacionesProductos productoCotizacion = new model_cotizacionesProductos();

        DateTime fechaActual = utilidad_fechas.obtenerCentral();
        productoCotizacion.usuario = HttpContext.Current.User.Identity.Name;
        productoCotizacion.activo = 1;
        //productoPedido.tipo = int.Parse(tipoProducto.Value);
        // productoPedido.fecha_creacion = utilidad_fechas.obtenerCentral();
        productoCotizacion.numero_parte = numero_parte;
        productoCotizacion.descripcion = productoTienda.descripcion_corta;
        productoCotizacion.marca = productoTienda.marca;
        // productoPedido.moneda = lbl_moneda.Text;
        // productoPedido.tipo_cambio = tipoCambio;
        // productoPedido.fecha_tipo_cambio = fechaActual;
        productoCotizacion.unidad = productoTienda.unidad_venta;
        productoCotizacion.precio_unitario = precio;
        productoCotizacion.cantidad = cantidadF;
        productoCotizacion.precio_total = decimal.Parse((Math.Round(precio, 2) * cantidadF).ToString());
        // productoPedido.stock1 = 0;
        // productoPedido.stock1_fecha = utilidad_fechas.obtenerCentral();

        cotizacionesProductos add = new cotizacionesProductos();


        if (add.actualizarCantidadProducto(numero_operacion, productoCotizacion) != false) {

            if (add.actualizarTotal(numero_operacion) != false) {

                return true;
                } else return false;

            } else
            return false;
        }
    private bool agregarProductoCotizacion() {

        model_cotizacionesProductos productoCotizacion= new model_cotizacionesProductos();


        productoCotizacion.usuario = HttpContext.Current.User.Identity.Name;
        productoCotizacion.activo = 1;
        productoCotizacion.tipo = 1;
        productoCotizacion.fecha_creacion = utilidad_fechas.obtenerCentral();
        productoCotizacion.numero_parte = numero_parte;
        productoCotizacion.descripcion = productoTienda.descripcion_corta;
        productoCotizacion.marca = productoTienda.marca;
        productoCotizacion.orden = cotizacionesProductos.obtenerCantidadProductos(numero_operacion)+1;
        // productoPedido.moneda = lbl_moneda.Text;
        // productoPedido.tipo_cambio = tipoCambio;
        // productoPedido.fecha_tipo_cambio = fechaActual;
        productoCotizacion.unidad = productoTienda.unidad_venta;
        productoCotizacion.precio_unitario = precio;
        productoCotizacion.cantidad = cantidadF;
        productoCotizacion.precio_total = decimal.Parse((Math.Round(precio, 2) * cantidadF).ToString());
        // productoPedido.stock1 = 0;
        // productoPedido.stock1_fecha = utilidad_fechas.obtenerCentral();


        bool resultado = cotizacionesProductos.agregarProducto(numero_operacion, productoCotizacion);

        if (resultado == true) {
            cotizacionesProductos actualizar = new cotizacionesProductos();
            if (actualizar.actualizarTotal(numero_operacion) == true)
                return true;
            else
                return false;
            } else
            return false;




        }
    private bool actualizarProductoPedido() {

        model_pedidos_productos productoPedido = new model_pedidos_productos();

        DateTime fechaActual = utilidad_fechas.obtenerCentral();
        productoPedido.usuario = HttpContext.Current.User.Identity.Name;
        // productoPedido.activo = 1;
        //productoPedido.tipo = int.Parse(tipoProducto.Value);
        productoPedido.fecha_creacion = utilidad_fechas.obtenerCentral();
        productoPedido.numero_parte = numero_parte;
        productoPedido.descripcion = productoTienda.descripcion_corta;
        productoPedido.marca = productoTienda.marca;
        // productoPedido.moneda = lbl_moneda.Text;
        // productoPedido.tipo_cambio = tipoCambio;
        // productoPedido.fecha_tipo_cambio = fechaActual;
        productoPedido.unidad = productoTienda.unidad_venta;
        productoPedido.precio_unitario = precio;
        productoPedido.cantidad = cantidadF;
        productoPedido.precio_total = decimal.Parse((Math.Round(precio, 2) * cantidadF).ToString());
        // productoPedido.stock1 = 0;
        // productoPedido.stock1_fecha = utilidad_fechas.obtenerCentral();

        pedidosProductos add = new pedidosProductos();


        if (add.actualizarCantidadProducto(numero_operacion, productoPedido) != false) {

            if (add.actualizarTotal(numero_operacion) != false) {

                return true;
                } else return false;

            } else
            return false;
        }
    private bool agregarProductoPedido() {

        model_pedidos_productos productoPedido = new model_pedidos_productos();

        DateTime fechaActual = utilidad_fechas.obtenerCentral();
        productoPedido.usuario = HttpContext.Current.User.Identity.Name;
        // productoPedido.activo = 1;
        productoPedido.tipo = 1;
        productoPedido.fecha_creacion = utilidad_fechas.obtenerCentral();
        productoPedido.numero_parte = numero_parte;
        productoPedido.descripcion = productoTienda.descripcion_corta;
        productoPedido.marca = productoTienda.marca;
        // productoPedido.moneda = lbl_moneda.Text;
        // productoPedido.tipo_cambio = tipoCambio;
        // productoPedido.fecha_tipo_cambio = fechaActual;
        productoPedido.unidad = productoTienda.unidad_venta;
        productoPedido.precio_unitario = precio;
        productoPedido.cantidad = cantidadF;
        productoPedido.precio_total = decimal.Parse((Math.Round(precio, 2) * cantidadF).ToString());
        // productoPedido.stock1 = 0;
        // productoPedido.stock1_fecha = utilidad_fechas.obtenerCentral();


        bool resultado = pedidosProductos.agregarProducto(numero_operacion, productoPedido);

        if (resultado == true) {
            pedidosProductos actualizar = new pedidosProductos();
            if (actualizar.actualizarTotal(numero_operacion) == true)
                return true;
            else
                return false;
            } else
            return false;
            
        }
    }