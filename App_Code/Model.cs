﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

public partial class base_cotizaciones_estatus
{
    public int idEstatus { get; set; }
    public string nombreEstatus { get; set; }
}

public partial class carrito_productos
{
    public int id { get; set; }
    public string usuario { get; set; }
    public int activo { get; set; }
    public int tipo { get; set; }
    public System.DateTime fecha_creacion { get; set; }
    public string numero_parte { get; set; }
    public string descripcion { get; set; }
    public string marca { get; set; }
    public string moneda { get; set; }
    public decimal tipo_cambio { get; set; }
    public System.DateTime fecha_tipo_cambio { get; set; }
    public string unidad { get; set; }
    public decimal precio_unitario { get; set; }
    public decimal cantidad { get; set; }
    public decimal precio_total { get; set; }
    public Nullable<double> stock1 { get; set; }
    public Nullable<System.DateTime> stock1_fecha { get; set; }
    public Nullable<double> stock2 { get; set; }
    public Nullable<System.DateTime> stock2_fecha { get; set; }
}

public partial class categoria
{
    public int id { get; set; }
    public string nombre { get; set; }
    public string descripcion { get; set; }
    public string imagen { get; set; }
    public string rol_categoria { get; set; }
    public string productos_Destacados { get; set; }
    public string identificador { get; set; }
    public string asociacion { get; set; }
    public int nivel { get; set; }
    public Nullable<int> orden { get; set; }
}

public partial class contacto
{
    public int id { get; set; }
    public int id_cliente { get; set; }
    public string nombre { get; set; }
    public string apellido_paterno { get; set; }
    public string apellido_materno { get; set; }
    public string email { get; set; }
    public string telefono { get; set; }
    public string celular { get; set; }

    public virtual usuario usuario { get; set; }
}

public partial class cotizaciones_datos
{
    public int id { get; set; }
    public string nombre_cotizacion { get; set; }
    public string numero_operacion { get; set; }
    public System.DateTime fecha_creacion { get; set; }
    public string creada_por { get; set; }
    public Nullable<int> mod_asesor { get; set; }
    public string id_cliente { get; set; }
    public string usuario_cliente { get; set; }
    public string cliente_nombre { get; set; }
    public string cliente_apellido_paterno { get; set; }
    public string cliente_apellido_materno { get; set; }
    public string email { get; set; }
    public string telefono { get; set; }
    public string celular { get; set; }
    public Nullable<int> activo { get; set; }
    public string comentarios { get; set; }
    public Nullable<int> vigencia { get; set; }
    public Nullable<int> vecesRenovada { get; set; }
    public Nullable<int> conversionPedido { get; set; }
    public string numero_operacion_pedido { get; set; }
    public string tipo_cotizacion { get; set; }
    public Nullable<int> idEstatus { get; set; }
    public Nullable<bool> Calculo_Costo_Envio { get; set; }
}

public partial class cotizaciones_datosNumericos
{
    public int id { get; set; }
    public string numero_operacion { get; set; }
    public string monedaCotizacion { get; set; }
    public decimal tipo_cambio { get; set; }
    public System.DateTime fecha_tipo_cambio { get; set; }
    public decimal subtotal { get; set; }
    public decimal envio { get; set; }
    public string metodoEnvio { get; set; }
    public string monedaEnvio { get; set; }
    public decimal impuestos { get; set; }
    public string nombreImpuestos { get; set; }
    public decimal total { get; set; }
    public Nullable<decimal> descuento { get; set; }
    public Nullable<decimal> descuento_porcentaje { get; set; }
    public string EnvioNota { get; set; }
}

public partial class cotizaciones_direccionEnvio
{
    public int id { get; set; }
    public string numero_operacion { get; set; }
    public string calle { get; set; }
    public string numero { get; set; }
    public string colonia { get; set; }
    public string delegacion_municipio { get; set; }
    public string estado { get; set; }
    public string codigo_postal { get; set; }
    public string pais { get; set; }
    public string referencias { get; set; }
    public string ciudad { get; set; }
}

public partial class cotizaciones_productos
{
    public int id { get; set; }
    public string numero_operacion { get; set; }
    public string usuario { get; set; }
    public Nullable<int> orden { get; set; }
    public int activo { get; set; }
    public int tipo { get; set; }
    public Nullable<int> alternativo { get; set; }
    public System.DateTime fecha_creacion { get; set; }
    public string numero_parte { get; set; }
    public string descripcion { get; set; }
    public string marca { get; set; }
    public string unidad { get; set; }
    public decimal precio_unitario { get; set; }
    public decimal cantidad { get; set; }
    public decimal precio_total { get; set; }
    public Nullable<double> stock1 { get; set; }
    public Nullable<System.DateTime> stock1_fecha { get; set; }
    public Nullable<double> stock2 { get; set; }
    public Nullable<System.DateTime> stock2_fecha { get; set; }
}

public partial class direcciones_envio
{
    public int id { get; set; }
    public string nombre_direccion { get; set; }
    public int id_cliente { get; set; }
    public string calle { get; set; }
    public string numero { get; set; }
    public string colonia { get; set; }
    public string delegacion_municipio { get; set; }
    public string estado { get; set; }
    public string codigo_postal { get; set; }
    public string pais { get; set; }
    public string referencias { get; set; }
    public Nullable<bool> direccion_predeterminada { get; set; }
    public string numero_interior { get; set; }
    public string ciudad { get; set; }

    public virtual usuario usuario { get; set; }
}

public partial class direcciones_facturacion
{
    public int id { get; set; }
    public int id_cliente { get; set; }
    public string nombre_direccion { get; set; }
    public string razon_social { get; set; }
    public string regimen_fiscal { get; set; }
    public string rfc { get; set; }
    public string calle { get; set; }
    public string numero { get; set; }
    public string colonia { get; set; }
    public string delegacion_municipio { get; set; }
    public string estado { get; set; }
    public string codigo_postal { get; set; }
    public string pais { get; set; }
    public string ciudad { get; set; }

    public virtual usuario usuario { get; set; }
}

public partial class estados_mexico
{
    public int id { get; set; }
    public string nombre { get; set; }
    public string codigo { get; set; }
}

public partial class glosario
{
    public int id { get; set; }
    public string término { get; set; }
    public string términoInglés { get; set; }
    public string descripción { get; set; }
    public string simbolo { get; set; }
    public string link { get; set; }
}

public partial class infografías
{
    public int id { get; set; }
    public string titulo { get; set; }
    public string descripción { get; set; }
    public System.DateTime fecha { get; set; }
    public string nombreImagenMiniatura { get; set; }
    public string nombreArchivo { get; set; }
}

public partial class pais
{
    public int id { get; set; }
    public string nombre { get; set; }
    public string codigo { get; set; }
}

public partial class pedidos_datos
{
    public int id { get; set; }
    public string nombre_pedido { get; set; }
    public string numero_operacion { get; set; }
    public System.DateTime fecha_creacion { get; set; }
    public string creada_por { get; set; }
    public Nullable<int> mod_asesor { get; set; }
    public string id_cliente { get; set; }
    public string usuario_cliente { get; set; }
    public string cliente_nombre { get; set; }
    public string cliente_apellido_paterno { get; set; }
    public string cliente_apellido_materno { get; set; }
    public string email { get; set; }
    public string telefono { get; set; }
    public string celular { get; set; }
    public Nullable<int> activo { get; set; }
    public string comentarios { get; set; }
    public Nullable<int> preCotizacion { get; set; }
    public string numero_operacion_cotizacion { get; set; }
    public Nullable<bool> Calculo_Costo_Envio { get; set; }
    public Nullable<bool> factura { get; set; }
    public Nullable<bool> OperacionCancelada { get; set; }
    public string motivoCancelacion { get; set; }
    public string detalleCancelacionAsesor { get; set; }
    public Nullable<int> idUsuarioSeguimiento { get; set; }
}

public partial class pedidos_datosNumericos
{
    public int id { get; set; }
    public string numero_operacion { get; set; }
    public string monedaPedido { get; set; }
    public decimal tipo_cambio { get; set; }
    public System.DateTime fecha_tipo_cambio { get; set; }
    public decimal subtotal { get; set; }
    public decimal envio { get; set; }
    public string metodoEnvio { get; set; }
    public string monedaEnvio { get; set; }
    public decimal impuestos { get; set; }
    public string nombreImpuestos { get; set; }
    public decimal total { get; set; }
    public Nullable<decimal> descuento { get; set; }
    public Nullable<decimal> descuento_porcentaje { get; set; }
    public string EnvioNota { get; set; }
}

public partial class pedidos_direccionEnvio
{
    public int id { get; set; }
    public string numero_operacion { get; set; }
    public string calle { get; set; }
    public string numero { get; set; }
    public string colonia { get; set; }
    public string delegacion_municipio { get; set; }
    public string estado { get; set; }
    public string codigo_postal { get; set; }
    public string pais { get; set; }
    public string referencias { get; set; }
    public string ciudad { get; set; }
    public Nullable<int> idDireccionEnvio { get; set; }
    public string numero_interior { get; set; }
}

public partial class pedidos_direccionFacturacion
{
    public int id { get; set; }
    public string numero_operacion { get; set; }
    public string razon_social { get; set; }
    public string rfc { get; set; }
    public string calle { get; set; }
    public string numero { get; set; }
    public string colonia { get; set; }
    public string delegacion_municipio { get; set; }
    public string estado { get; set; }
    public string codigo_postal { get; set; }
    public string pais { get; set; }
    public string ciudad { get; set; }
    public Nullable<int> idDireccionFacturacion { get; set; }
    public string UsoCFDI { get; set; }
}

public partial class pedidos_modificaciones
{
    public int id { get; set; }
    public string numero_operacion { get; set; }
    public string descripcion { get; set; }
    public System.DateTime fecha_modificacion { get; set; }
    public string modificada_por { get; set; }
}

public partial class pedidos_pagos_liga_santander
{
    public int idLigasSantander { get; set; }
    public string numero_operacion { get; set; }
    public string liga { get; set; }
    public System.DateTime fecha_creacion { get; set; }
    public System.DateTime fecha_vigencia { get; set; }
    public Nullable<decimal> monto { get; set; }
    public string moneda { get; set; }
}

public partial class pedidos_pagos_paypal
{
    public int id { get; set; }
    public string idTransacciónPayPal { get; set; }
    public string numero_operacion { get; set; }
    public string intento { get; set; }
    public string estado { get; set; }
    public string moneda { get; set; }
    public string monto { get; set; }
    public Nullable<System.DateTime> fecha_primerIntento { get; set; }
    public Nullable<System.DateTime> fecha_actualización { get; set; }
    public Nullable<bool> aprobadoAsesor { get; set; }
    public string paymentID { get; set; }
}

public partial class pedidos_pagos_respuesta_santander
{
    public int idRespuestasSantander { get; set; }
    public string numero_operacion { get; set; }
    public string response { get; set; }
    public string estatus { get; set; }
    public System.DateTime fecha_primerIntento { get; set; }
    public Nullable<System.DateTime> fecha_actualización { get; set; }
}

public partial class pedidos_pagos_transferencia
{
    public int idRegistroTransferencia { get; set; }
    public string numero_operacion { get; set; }
    public string referencia { get; set; }
    public Nullable<System.DateTime> fecha_captura { get; set; }
    public Nullable<bool> confirmacionAsesor { get; set; }
    public Nullable<int> idUsuario { get; set; }
    public Nullable<System.DateTime> fecha_confirmacion { get; set; }
}

public partial class pedidos_productos
{
    public int id { get; set; }
    public string numero_operacion { get; set; }
    public string usuario { get; set; }
    public Nullable<int> orden { get; set; }
    public int tipo { get; set; }
    public System.DateTime fecha_creacion { get; set; }
    public string numero_parte { get; set; }
    public string descripcion { get; set; }
    public string marca { get; set; }
    public string unidad { get; set; }
    public decimal precio_unitario { get; set; }
    public decimal cantidad { get; set; }
    public decimal precio_total { get; set; }
    public Nullable<double> stock1 { get; set; }
    public Nullable<System.DateTime> stock1_fecha { get; set; }
    public Nullable<double> stock2 { get; set; }
    public Nullable<System.DateTime> stock2_fecha { get; set; }
}

public partial class pedidos_productos_modificaciones
{
    public int id { get; set; }
    public string numero_operacion { get; set; }
    public string numero_parte { get; set; }
    public string descripcion { get; set; }
    public System.DateTime fecha_modificacion { get; set; }
    public string modificada_por { get; set; }
}

public partial class PedidosClaveUsoCFDI
{
    public int id_CFDI { get; set; }
    public string ClaveUsoCFDI { get; set; }
    public string Descripción { get; set; }
    public Nullable<bool> PersonaFisica { get; set; }
    public Nullable<bool> PersonaMoral { get; set; }
}

public partial class permisos_app
{
    public int id { get; set; }
    public int idUsuario { get; set; }
    public string seccion_pagina { get; set; }
    public bool permiso { get; set; }

    public virtual usuario usuario { get; set; }
}

public partial class precios_fantasma
{
    public int id { get; set; }
    public string numero_parte { get; set; }
    public decimal preciosFantasma { get; set; }
    public string porcentajeFantasma { get; set; }
}

public partial class precios_ListaFija
{
    public int id { get; set; }
    public string id_cliente { get; set; }
    public string numero_parte { get; set; }
    public decimal precio { get; set; }
    public string moneda_fija { get; set; }
}

public partial class precios_multiplicador
{
    public int id { get; set; }
    public int nivel { get; set; }
    public string nombre_multiplicador { get; set; }
    public decimal multiplicador_valor { get; set; }
}

public partial class precios_rangos
{
    public int id { get; set; }
    public string numero_parte { get; set; }
    public string moneda_rangos { get; set; }
    public Nullable<decimal> precio1 { get; set; }
    public Nullable<decimal> max1 { get; set; }
    public Nullable<decimal> precio2 { get; set; }
    public Nullable<decimal> max2 { get; set; }
    public Nullable<decimal> precio3 { get; set; }
    public Nullable<decimal> max3 { get; set; }
    public Nullable<decimal> precio4 { get; set; }
    public Nullable<decimal> max4 { get; set; }
    public Nullable<decimal> precio5 { get; set; }
    public Nullable<decimal> max5 { get; set; }
}

public partial class productos_comentarios
{
    public int idComentario { get; set; }
    public string numero_parte { get; set; }
    public System.DateTime fechaComentario { get; set; }
    public Nullable<int> idUsuario { get; set; }
    public string comentario { get; set; }
    public Nullable<byte> calificación { get; set; }
    public bool visible { get; set; }

    public virtual usuario usuario { get; set; }
}

public partial class productos_Datos
{
    public int id { get; set; }
    public string numero_parte { get; set; }
    public string titulo { get; set; }
    public string descripcion_corta { get; set; }
    public string titulo_corto_ingles { get; set; }
    public string especificaciones { get; set; }
    public string marca { get; set; }
    public string categoria_identificador { get; set; }
    public string imagenes { get; set; }
    public string metatags { get; set; }
    public Nullable<decimal> peso { get; set; }
    public Nullable<decimal> alto { get; set; }
    public Nullable<decimal> ancho { get; set; }
    public Nullable<decimal> profundidad { get; set; }
    public string pdf { get; set; }
    public string video { get; set; }
    public string unidad_venta { get; set; }
    public Nullable<decimal> cantidad { get; set; }
    public string unidad { get; set; }
    public string producto_alternativo { get; set; }
    public string productos_relacionados { get; set; }
    public string atributos { get; set; }
    public string noParte_proveedor { get; set; }
    public string noParte_interno { get; set; }
    public string upc { get; set; }
    public string noParte_Competidor { get; set; }
    public Nullable<int> orden { get; set; }
    public string etiquetas { get; set; }
    public int disponibleVenta { get; set; }
    public Nullable<int> disponibleEnvio { get; set; }
    public Nullable<bool> RotacionHorizontal { get; set; }
    public Nullable<bool> RotacionVertical { get; set; }
    public string noParte_Sap { get; set; }
    public string avisos { get; set; }
    public Nullable<int> destacado { get; set; }
}

public partial class productos_Roles
{
    public int id { get; set; }
    public string numero_parte { get; set; }
    public string rol_preciosMultiplicador { get; set; }
    public string rol_visibilidad { get; set; }
}

public partial class productos_solo_visualizacion
{
    public int id { get; set; }
    public string numero_parte { get; set; }
    public bool solo_para_Visualizar { get; set; }
}

public partial class productos_stock
{
    public string numero_parte { get; set; }
    public double cantidad { get; set; }
    public string unidad { get; set; }
    public string ubicación { get; set; }
    public System.DateTime fecha_actualización { get; set; }
    public string ubicación_pre { get; set; }
}

public partial class productos_unidades
{
    public int id { get; set; }
    public string unidad_nombre { get; set; }
}

public partial class ProductosBloqueoStock
{
    public int IdBloqueoStock { get; set; }
    public string numero_parte { get; set; }
    public decimal Disponible { get; set; }
}

public partial class usuario
{
    public usuario()
    {
        this.contactos = new HashSet<contacto>();
        this.direcciones_envio = new HashSet<direcciones_envio>();
        this.direcciones_facturacion = new HashSet<direcciones_facturacion>();
        this.permisos_app = new HashSet<permisos_app>();
        this.productos_comentarios = new HashSet<productos_comentarios>();
    }

    public int id { get; set; }
    public System.DateTime fecha_registro { get; set; }
    public string nombre { get; set; }
    public string apellido_paterno { get; set; }
    public string apellido_materno { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public string tipo_de_usuario { get; set; }
    public int rango { get; set; }
    public string departamento { get; set; }
    public string grupoPrivacidad { get; set; }
    public string perfil_cliente { get; set; }
    public string id_cliente { get; set; }
    public string rol_precios_multiplicador { get; set; }
    public string rol_productos { get; set; }
    public string rol_categorias { get; set; }
    public string asesor_base { get; set; }
    public string grupo_asesor { get; set; }
    public string asesor_adicional { get; set; }
    public string grupo_asesores_adicional { get; set; }
    public string grupo_usuario { get; set; }
    public Nullable<System.DateTime> ultimo_inicio_sesion { get; set; }
    public Nullable<System.DateTime> fecha_nacimiento { get; set; }
    public string registrado_por { get; set; }
    public string telefono { get; set; }
    public string celular { get; set; }
    public Nullable<bool> cuenta_confirmada { get; set; }
    public Nullable<bool> cuenta_activa { get; set; }

    public virtual ICollection<contacto> contactos { get; set; }
    public virtual ICollection<direcciones_envio> direcciones_envio { get; set; }
    public virtual ICollection<direcciones_facturacion> direcciones_facturacion { get; set; }
    public virtual ICollection<permisos_app> permisos_app { get; set; }
    public virtual ICollection<productos_comentarios> productos_comentarios { get; set; }
}

public partial class usuarios_ligas_confirmaciones
{
    public int idLiga { get; set; }
    public string usuario { get; set; }
    public string clave { get; set; }
    public System.DateTime fecha_creacion { get; set; }
    public System.DateTime fecha_vigencia { get; set; }
}

public partial class usuariosInfo
{
    public int id { get; set; }
    public int idUsuario { get; set; }
    public int registroMetodo { get; set; }
    public Nullable<int> registradoIdAsesor { get; set; }
    public Nullable<System.DateTime> fecha_registro { get; set; }
}
