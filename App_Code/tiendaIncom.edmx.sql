
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/21/2022 14:27:34
-- Generated from EDMX file: C:\Users\AlexisRuizSantiago\source\repos\TiendaINCOMDecimalOmar\App_Code\tiendaIncom.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [tienda];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[fk_contactos_usuarios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[contactos] DROP CONSTRAINT [fk_contactos_usuarios];
GO
IF OBJECT_ID(N'[dbo].[fk_direccionesenvio_usuarios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[direcciones_envio] DROP CONSTRAINT [fk_direccionesenvio_usuarios];
GO
IF OBJECT_ID(N'[dbo].[fk_direccionesFact_usuarios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[direcciones_facturacion] DROP CONSTRAINT [fk_direccionesFact_usuarios];
GO
IF OBJECT_ID(N'[dbo].[FK_idUsuario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[productos_comentarios] DROP CONSTRAINT [FK_idUsuario];
GO
IF OBJECT_ID(N'[dbo].[fk_permisos_usuarios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[permisos_app] DROP CONSTRAINT [fk_permisos_usuarios];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[base_cotizaciones_estatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[base_cotizaciones_estatus];
GO
IF OBJECT_ID(N'[dbo].[carrito_productos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[carrito_productos];
GO
IF OBJECT_ID(N'[dbo].[categorias]', 'U') IS NOT NULL
    DROP TABLE [dbo].[categorias];
GO
IF OBJECT_ID(N'[dbo].[contactos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[contactos];
GO
IF OBJECT_ID(N'[dbo].[cotizaciones_datos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[cotizaciones_datos];
GO
IF OBJECT_ID(N'[dbo].[cotizaciones_datosNumericos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[cotizaciones_datosNumericos];
GO
IF OBJECT_ID(N'[dbo].[cotizaciones_direccionEnvio]', 'U') IS NOT NULL
    DROP TABLE [dbo].[cotizaciones_direccionEnvio];
GO
IF OBJECT_ID(N'[dbo].[cotizaciones_productos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[cotizaciones_productos];
GO
IF OBJECT_ID(N'[dbo].[direcciones_envio]', 'U') IS NOT NULL
    DROP TABLE [dbo].[direcciones_envio];
GO
IF OBJECT_ID(N'[dbo].[direcciones_facturacion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[direcciones_facturacion];
GO
IF OBJECT_ID(N'[dbo].[estados_mexico]', 'U') IS NOT NULL
    DROP TABLE [dbo].[estados_mexico];
GO
IF OBJECT_ID(N'[dbo].[glosario]', 'U') IS NOT NULL
    DROP TABLE [dbo].[glosario];
GO
IF OBJECT_ID(N'[dbo].[infografías]', 'U') IS NOT NULL
    DROP TABLE [dbo].[infografías];
GO
IF OBJECT_ID(N'[dbo].[paises]', 'U') IS NOT NULL
    DROP TABLE [dbo].[paises];
GO
IF OBJECT_ID(N'[dbo].[pedidos_datos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[pedidos_datos];
GO
IF OBJECT_ID(N'[dbo].[pedidos_datosNumericos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[pedidos_datosNumericos];
GO
IF OBJECT_ID(N'[dbo].[pedidos_direccionEnvio]', 'U') IS NOT NULL
    DROP TABLE [dbo].[pedidos_direccionEnvio];
GO
IF OBJECT_ID(N'[dbo].[pedidos_direccionFacturacion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[pedidos_direccionFacturacion];
GO
IF OBJECT_ID(N'[dbo].[pedidos_modificaciones]', 'U') IS NOT NULL
    DROP TABLE [dbo].[pedidos_modificaciones];
GO
IF OBJECT_ID(N'[dbo].[pedidos_pagos_liga_santander]', 'U') IS NOT NULL
    DROP TABLE [dbo].[pedidos_pagos_liga_santander];
GO
IF OBJECT_ID(N'[dbo].[pedidos_pagos_paypal]', 'U') IS NOT NULL
    DROP TABLE [dbo].[pedidos_pagos_paypal];
GO
IF OBJECT_ID(N'[dbo].[pedidos_pagos_respuesta_santander]', 'U') IS NOT NULL
    DROP TABLE [dbo].[pedidos_pagos_respuesta_santander];
GO
IF OBJECT_ID(N'[dbo].[pedidos_pagos_transferencia]', 'U') IS NOT NULL
    DROP TABLE [dbo].[pedidos_pagos_transferencia];
GO
IF OBJECT_ID(N'[dbo].[pedidos_productos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[pedidos_productos];
GO
IF OBJECT_ID(N'[dbo].[pedidos_productos_modificaciones]', 'U') IS NOT NULL
    DROP TABLE [dbo].[pedidos_productos_modificaciones];
GO
IF OBJECT_ID(N'[dbo].[PedidosClaveUsoCFDI]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PedidosClaveUsoCFDI];
GO
IF OBJECT_ID(N'[dbo].[permisos_app]', 'U') IS NOT NULL
    DROP TABLE [dbo].[permisos_app];
GO
IF OBJECT_ID(N'[dbo].[precios_fantasma]', 'U') IS NOT NULL
    DROP TABLE [dbo].[precios_fantasma];
GO
IF OBJECT_ID(N'[dbo].[precios_ListaFija]', 'U') IS NOT NULL
    DROP TABLE [dbo].[precios_ListaFija];
GO
IF OBJECT_ID(N'[dbo].[precios_multiplicador]', 'U') IS NOT NULL
    DROP TABLE [dbo].[precios_multiplicador];
GO
IF OBJECT_ID(N'[dbo].[precios_rangos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[precios_rangos];
GO
IF OBJECT_ID(N'[dbo].[productos_comentarios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[productos_comentarios];
GO
IF OBJECT_ID(N'[dbo].[productos_Datos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[productos_Datos];
GO
IF OBJECT_ID(N'[dbo].[productos_Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[productos_Roles];
GO
IF OBJECT_ID(N'[dbo].[productos_solo_visualizacion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[productos_solo_visualizacion];
GO
IF OBJECT_ID(N'[dbo].[productos_unidades]', 'U') IS NOT NULL
    DROP TABLE [dbo].[productos_unidades];
GO
IF OBJECT_ID(N'[dbo].[ProductosBloqueoStock]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductosBloqueoStock];
GO
IF OBJECT_ID(N'[dbo].[usuarios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[usuarios];
GO
IF OBJECT_ID(N'[dbo].[usuarios_ligas_confirmaciones]', 'U') IS NOT NULL
    DROP TABLE [dbo].[usuarios_ligas_confirmaciones];
GO
IF OBJECT_ID(N'[dbo].[usuariosInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[usuariosInfo];
GO
IF OBJECT_ID(N'[tiendaModelStoreContainer].[productos_stock]', 'U') IS NOT NULL
    DROP TABLE [tiendaModelStoreContainer].[productos_stock];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'base_cotizaciones_estatus'
CREATE TABLE [dbo].[base_cotizaciones_estatus] (
    [idEstatus] int IDENTITY(1,1) NOT NULL,
    [nombreEstatus] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'productos_comentarios'
CREATE TABLE [dbo].[productos_comentarios] (
    [idComentario] int IDENTITY(1,1) NOT NULL,
    [numero_parte] nvarchar(100)  NOT NULL,
    [fechaComentario] datetime  NOT NULL,
    [idUsuario] int  NULL,
    [comentario] nvarchar(850)  NOT NULL,
    [calificación] tinyint  NULL,
    [visible] bit  NOT NULL
);
GO

-- Creating table 'productos_stock'
CREATE TABLE [dbo].[productos_stock] (
    [numero_parte] nvarchar(100)  NOT NULL,
    [cantidad] float  NOT NULL,
    [unidad] nvarchar(25)  NOT NULL,
    [ubicación] nvarchar(50)  NULL,
    [fecha_actualización] datetime  NOT NULL,
    [ubicación_pre] nvarchar(50)  NULL
);
GO

-- Creating table 'pedidos_pagos_paypal'
CREATE TABLE [dbo].[pedidos_pagos_paypal] (
    [id] int IDENTITY(1,1) NOT NULL,
    [idTransacciónPayPal] nvarchar(25)  NOT NULL,
    [numero_operacion] nvarchar(20)  NOT NULL,
    [intento] nvarchar(25)  NULL,
    [estado] nvarchar(25)  NULL,
    [moneda] nvarchar(5)  NULL,
    [monto] nvarchar(25)  NULL,
    [fecha_primerIntento] datetime  NULL,
    [fecha_actualización] datetime  NULL,
    [aprobadoAsesor] bit  NULL,
    [paymentID] nvarchar(25)  NULL
);
GO

-- Creating table 'glosario'
CREATE TABLE [dbo].[glosario] (
    [id] int IDENTITY(1,1) NOT NULL,
    [término] nvarchar(100)  NOT NULL,
    [términoInglés] nvarchar(100)  NULL,
    [descripción] nvarchar(800)  NOT NULL,
    [simbolo] nvarchar(25)  NULL,
    [link] nvarchar(250)  NULL
);
GO

-- Creating table 'infografías'
CREATE TABLE [dbo].[infografías] (
    [id] int IDENTITY(1,1) NOT NULL,
    [titulo] nvarchar(100)  NOT NULL,
    [descripción] nvarchar(400)  NOT NULL,
    [fecha] datetime  NOT NULL,
    [nombreImagenMiniatura] nvarchar(250)  NOT NULL,
    [nombreArchivo] nvarchar(250)  NOT NULL
);
GO

-- Creating table 'carrito_productos'
CREATE TABLE [dbo].[carrito_productos] (
    [id] int IDENTITY(1,1) NOT NULL,
    [usuario] nvarchar(60)  NOT NULL,
    [activo] int  NOT NULL,
    [tipo] int  NOT NULL,
    [fecha_creacion] datetime  NOT NULL,
    [numero_parte] nvarchar(50)  NOT NULL,
    [descripcion] nvarchar(250)  NOT NULL,
    [marca] nvarchar(50)  NOT NULL,
    [moneda] nvarchar(5)  NOT NULL,
    [tipo_cambio] decimal(19,4)  NOT NULL,
    [fecha_tipo_cambio] datetime  NOT NULL,
    [unidad] nvarchar(25)  NOT NULL,
    [precio_unitario] decimal(19,4)  NOT NULL,
    [cantidad] decimal(19,4)  NOT NULL,
    [precio_total] decimal(19,4)  NOT NULL,
    [stock1] float  NULL,
    [stock1_fecha] datetime  NULL,
    [stock2] float  NULL,
    [stock2_fecha] datetime  NULL
);
GO

-- Creating table 'direcciones_envio'
CREATE TABLE [dbo].[direcciones_envio] (
    [id] int IDENTITY(1,1) NOT NULL,
    [nombre_direccion] nvarchar(20)  NOT NULL,
    [id_cliente] int  NOT NULL,
    [calle] nvarchar(50)  NOT NULL,
    [numero] nvarchar(30)  NOT NULL,
    [colonia] nvarchar(70)  NULL,
    [delegacion_municipio] nvarchar(60)  NULL,
    [estado] nvarchar(35)  NULL,
    [codigo_postal] nvarchar(15)  NOT NULL,
    [pais] nvarchar(35)  NOT NULL,
    [referencias] nvarchar(100)  NULL,
    [direccion_predeterminada] bit  NULL,
    [numero_interior] nvarchar(30)  NULL,
    [ciudad] nvarchar(60)  NULL
);
GO

-- Creating table 'direcciones_facturacion'
CREATE TABLE [dbo].[direcciones_facturacion] (
    [id] int IDENTITY(1,1) NOT NULL,
    [id_cliente] int  NOT NULL,
    [nombre_direccion] nvarchar(20)  NOT NULL,
    [razon_social] nvarchar(150)  NOT NULL,
    [rfc] nvarchar(15)  NOT NULL,
    [calle] nvarchar(50)  NOT NULL,
    [numero] nvarchar(20)  NOT NULL,
    [colonia] nvarchar(35)  NULL,
    [delegacion_municipio] nvarchar(35)  NULL,
    [estado] nvarchar(35)  NULL,
    [codigo_postal] nvarchar(15)  NOT NULL,
    [pais] nvarchar(35)  NOT NULL,
    [ciudad] nvarchar(60)  NULL
);
GO

-- Creating table 'estados_mexico'
CREATE TABLE [dbo].[estados_mexico] (
    [id] int IDENTITY(1,1) NOT NULL,
    [nombre] nvarchar(25)  NULL,
    [codigo] nvarchar(5)  NULL
);
GO

-- Creating table 'paises'
CREATE TABLE [dbo].[paises] (
    [id] int IDENTITY(1,1) NOT NULL,
    [nombre] nvarchar(35)  NULL,
    [codigo] nvarchar(5)  NULL
);
GO

-- Creating table 'pedidos_direccionFacturacion'
CREATE TABLE [dbo].[pedidos_direccionFacturacion] (
    [id] int IDENTITY(1,1) NOT NULL,
    [numero_operacion] nvarchar(20)  NOT NULL,
    [razon_social] nvarchar(150)  NOT NULL,
    [rfc] nvarchar(15)  NOT NULL,
    [calle] nvarchar(35)  NOT NULL,
    [numero] nvarchar(30)  NOT NULL,
    [colonia] nvarchar(35)  NOT NULL,
    [delegacion_municipio] nvarchar(35)  NOT NULL,
    [estado] nvarchar(35)  NOT NULL,
    [codigo_postal] nvarchar(15)  NOT NULL,
    [pais] nvarchar(35)  NOT NULL,
    [ciudad] nvarchar(60)  NULL,
    [idDireccionFacturacion] int  NULL,
    [UsoCFDI] nvarchar(5)  NULL
);
GO

-- Creating table 'pedidos_modificaciones'
CREATE TABLE [dbo].[pedidos_modificaciones] (
    [id] int IDENTITY(1,1) NOT NULL,
    [numero_operacion] nvarchar(20)  NOT NULL,
    [descripcion] nvarchar(150)  NOT NULL,
    [fecha_modificacion] datetime  NOT NULL,
    [modificada_por] nvarchar(60)  NOT NULL
);
GO

-- Creating table 'pedidos_productos'
CREATE TABLE [dbo].[pedidos_productos] (
    [id] int IDENTITY(1,1) NOT NULL,
    [numero_operacion] nvarchar(20)  NOT NULL,
    [usuario] nvarchar(60)  NOT NULL,
    [orden] int  NULL,
    [tipo] int  NOT NULL,
    [fecha_creacion] datetime  NOT NULL,
    [numero_parte] nvarchar(50)  NOT NULL,
    [descripcion] nvarchar(250)  NOT NULL,
    [marca] nvarchar(50)  NOT NULL,
    [unidad] nvarchar(25)  NOT NULL,
    [precio_unitario] decimal(19,4)  NOT NULL,
    [cantidad] decimal(19,4)  NOT NULL,
    [precio_total] decimal(19,4)  NOT NULL,
    [stock1] float  NULL,
    [stock1_fecha] datetime  NULL,
    [stock2] float  NULL,
    [stock2_fecha] datetime  NULL
);
GO

-- Creating table 'pedidos_productos_modificaciones'
CREATE TABLE [dbo].[pedidos_productos_modificaciones] (
    [id] int IDENTITY(1,1) NOT NULL,
    [numero_operacion] nvarchar(20)  NOT NULL,
    [numero_parte] nvarchar(50)  NOT NULL,
    [descripcion] nvarchar(150)  NULL,
    [fecha_modificacion] datetime  NOT NULL,
    [modificada_por] nvarchar(60)  NOT NULL
);
GO

-- Creating table 'precios_ListaFija'
CREATE TABLE [dbo].[precios_ListaFija] (
    [id] int IDENTITY(1,1) NOT NULL,
    [id_cliente] nvarchar(15)  NOT NULL,
    [numero_parte] nvarchar(50)  NOT NULL,
    [precio] decimal(19,4)  NOT NULL,
    [moneda_fija] nvarchar(5)  NOT NULL
);
GO

-- Creating table 'precios_multiplicador'
CREATE TABLE [dbo].[precios_multiplicador] (
    [id] int IDENTITY(1,1) NOT NULL,
    [nivel] int  NOT NULL,
    [nombre_multiplicador] nvarchar(50)  NOT NULL,
    [multiplicador_valor] decimal(19,4)  NOT NULL
);
GO

-- Creating table 'precios_rangos'
CREATE TABLE [dbo].[precios_rangos] (
    [id] int IDENTITY(1,1) NOT NULL,
    [numero_parte] nvarchar(50)  NOT NULL,
    [moneda_rangos] nvarchar(5)  NOT NULL,
    [precio1] decimal(19,4)  NULL,
    [max1] decimal(19,4)  NULL,
    [precio2] decimal(19,4)  NULL,
    [max2] decimal(19,4)  NULL,
    [precio3] decimal(19,4)  NULL,
    [max3] decimal(19,4)  NULL,
    [precio4] decimal(19,4)  NULL,
    [max4] decimal(19,4)  NULL,
    [precio5] decimal(19,4)  NULL,
    [max5] decimal(19,4)  NULL
);
GO

-- Creating table 'productos_Roles'
CREATE TABLE [dbo].[productos_Roles] (
    [id] int IDENTITY(1,1) NOT NULL,
    [numero_parte] nvarchar(50)  NOT NULL,
    [rol_preciosMultiplicador] nvarchar(250)  NOT NULL,
    [rol_visibilidad] nvarchar(250)  NOT NULL
);
GO

-- Creating table 'productos_unidades'
CREATE TABLE [dbo].[productos_unidades] (
    [id] int IDENTITY(1,1) NOT NULL,
    [unidad_nombre] nvarchar(25)  NOT NULL
);
GO

-- Creating table 'permisos_app'
CREATE TABLE [dbo].[permisos_app] (
    [id] int IDENTITY(1,1) NOT NULL,
    [idUsuario] int  NOT NULL,
    [seccion_pagina] nvarchar(100)  NOT NULL,
    [permiso] bit  NOT NULL
);
GO

-- Creating table 'cotizaciones_datos'
CREATE TABLE [dbo].[cotizaciones_datos] (
    [id] int IDENTITY(1,1) NOT NULL,
    [nombre_cotizacion] nvarchar(60)  NOT NULL,
    [numero_operacion] nvarchar(20)  NOT NULL,
    [fecha_creacion] datetime  NOT NULL,
    [creada_por] nvarchar(60)  NOT NULL,
    [mod_asesor] int  NULL,
    [id_cliente] nvarchar(15)  NOT NULL,
    [usuario_cliente] nvarchar(60)  NULL,
    [cliente_nombre] nvarchar(20)  NULL,
    [cliente_apellido_paterno] nvarchar(20)  NULL,
    [cliente_apellido_materno] nvarchar(20)  NULL,
    [email] nvarchar(60)  NULL,
    [telefono] nvarchar(50)  NULL,
    [celular] nvarchar(50)  NULL,
    [activo] int  NULL,
    [comentarios] nvarchar(1500)  NULL,
    [vigencia] int  NULL,
    [vecesRenovada] int  NULL,
    [conversionPedido] int  NULL,
    [numero_operacion_pedido] nvarchar(20)  NULL,
    [tipo_cotizacion] nvarchar(20)  NULL,
    [idEstatus] int  NULL,
    [Calculo_Costo_Envio] bit  NULL
);
GO

-- Creating table 'cotizaciones_productos'
CREATE TABLE [dbo].[cotizaciones_productos] (
    [id] int IDENTITY(1,1) NOT NULL,
    [numero_operacion] nvarchar(20)  NOT NULL,
    [usuario] nvarchar(60)  NOT NULL,
    [orden] int  NULL,
    [activo] int  NOT NULL,
    [tipo] int  NOT NULL,
    [alternativo] int  NULL,
    [fecha_creacion] datetime  NOT NULL,
    [numero_parte] nvarchar(50)  NOT NULL,
    [descripcion] nvarchar(500)  NOT NULL,
    [marca] nvarchar(50)  NOT NULL,
    [unidad] nvarchar(25)  NOT NULL,
    [precio_unitario] decimal(19,4)  NOT NULL,
    [cantidad] decimal(19,4)  NOT NULL,
    [precio_total] decimal(19,4)  NOT NULL,
    [stock1] float  NULL,
    [stock1_fecha] datetime  NULL,
    [stock2] float  NULL,
    [stock2_fecha] datetime  NULL
);
GO

-- Creating table 'productos_Datos'
CREATE TABLE [dbo].[productos_Datos] (
    [id] int IDENTITY(1,1) NOT NULL,
    [numero_parte] nvarchar(50)  NOT NULL,
    [titulo] nvarchar(130)  NOT NULL,
    [descripcion_corta] nvarchar(250)  NOT NULL,
    [titulo_corto_ingles] nvarchar(250)  NULL,
    [especificaciones] nvarchar(4000)  NULL,
    [marca] nvarchar(50)  NOT NULL,
    [categoria_identificador] nvarchar(100)  NULL,
    [imagenes] nvarchar(500)  NULL,
    [metatags] nvarchar(250)  NULL,
    [peso] decimal(10,5)  NULL,
    [alto] decimal(10,5)  NULL,
    [ancho] decimal(10,5)  NULL,
    [profundidad] decimal(10,5)  NULL,
    [pdf] nvarchar(350)  NULL,
    [video] nvarchar(250)  NULL,
    [unidad_venta] nvarchar(25)  NULL,
    [cantidad] decimal(16,2)  NULL,
    [unidad] nvarchar(25)  NULL,
    [producto_alternativo] nvarchar(150)  NULL,
    [productos_relacionados] nvarchar(250)  NULL,
    [atributos] nvarchar(3000)  NULL,
    [noParte_proveedor] nvarchar(50)  NULL,
    [noParte_interno] nvarchar(100)  NULL,
    [upc] nvarchar(50)  NULL,
    [noParte_Competidor] nvarchar(250)  NULL,
    [orden] int  NULL,
    [etiquetas] nvarchar(150)  NULL,
    [disponibleVenta] int  NOT NULL,
    [disponibleEnvio] int  NULL,
    [RotacionHorizontal] bit  NULL,
    [RotacionVertical] bit  NULL,
    [noParte_Sap] nvarchar(100)  NULL,
    [avisos] nvarchar(4000)  NULL,
    [destacado] int  NULL
);
GO

-- Creating table 'pedidos_pagos_liga_santander'
CREATE TABLE [dbo].[pedidos_pagos_liga_santander] (
    [idLigasSantander] int IDENTITY(1,1) NOT NULL,
    [numero_operacion] nvarchar(25)  NOT NULL,
    [liga] nvarchar(4000)  NOT NULL,
    [fecha_creacion] datetime  NOT NULL,
    [fecha_vigencia] datetime  NOT NULL,
    [monto] decimal(19,4)  NULL,
    [moneda] nvarchar(3)  NULL
);
GO

-- Creating table 'pedidos_pagos_respuesta_santander'
CREATE TABLE [dbo].[pedidos_pagos_respuesta_santander] (
    [idRespuestasSantander] int IDENTITY(1,1) NOT NULL,
    [numero_operacion] nvarchar(25)  NOT NULL,
    [response] nvarchar(4000)  NOT NULL,
    [estatus] nvarchar(10)  NOT NULL,
    [fecha_primerIntento] datetime  NOT NULL,
    [fecha_actualización] datetime  NULL
);
GO

-- Creating table 'cotizaciones_datosNumericos'
CREATE TABLE [dbo].[cotizaciones_datosNumericos] (
    [id] int IDENTITY(1,1) NOT NULL,
    [numero_operacion] nvarchar(20)  NOT NULL,
    [monedaCotizacion] nvarchar(5)  NULL,
    [tipo_cambio] decimal(19,4)  NOT NULL,
    [fecha_tipo_cambio] datetime  NOT NULL,
    [subtotal] decimal(19,4)  NOT NULL,
    [envio] decimal(19,4)  NOT NULL,
    [metodoEnvio] nvarchar(15)  NULL,
    [monedaEnvio] nvarchar(5)  NULL,
    [impuestos] decimal(19,4)  NOT NULL,
    [nombreImpuestos] nvarchar(5)  NULL,
    [total] decimal(19,4)  NOT NULL,
    [descuento] decimal(19,4)  NULL,
    [descuento_porcentaje] decimal(10,4)  NULL,
    [EnvioNota] nvarchar(200)  NULL
);
GO

-- Creating table 'cotizaciones_direccionEnvio'
CREATE TABLE [dbo].[cotizaciones_direccionEnvio] (
    [id] int IDENTITY(1,1) NOT NULL,
    [numero_operacion] nvarchar(20)  NOT NULL,
    [calle] nvarchar(50)  NOT NULL,
    [numero] nvarchar(30)  NULL,
    [colonia] nvarchar(70)  NOT NULL,
    [delegacion_municipio] nvarchar(60)  NOT NULL,
    [estado] nvarchar(35)  NOT NULL,
    [codigo_postal] nvarchar(15)  NOT NULL,
    [pais] nvarchar(35)  NOT NULL,
    [referencias] nvarchar(100)  NOT NULL,
    [ciudad] nvarchar(60)  NULL
);
GO

-- Creating table 'pedidos_direccionEnvio'
CREATE TABLE [dbo].[pedidos_direccionEnvio] (
    [id] int IDENTITY(1,1) NOT NULL,
    [numero_operacion] nvarchar(20)  NOT NULL,
    [calle] nvarchar(60)  NOT NULL,
    [numero] nvarchar(30)  NULL,
    [colonia] nvarchar(70)  NOT NULL,
    [delegacion_municipio] nvarchar(60)  NOT NULL,
    [estado] nvarchar(35)  NOT NULL,
    [codigo_postal] nvarchar(15)  NULL,
    [pais] nvarchar(35)  NOT NULL,
    [referencias] nvarchar(100)  NULL,
    [ciudad] nvarchar(60)  NULL,
    [idDireccionEnvio] int  NULL,
    [numero_interior] nvarchar(60)  NULL
);
GO

-- Creating table 'pedidos_datos'
CREATE TABLE [dbo].[pedidos_datos] (
    [id] int IDENTITY(1,1) NOT NULL,
    [nombre_pedido] nvarchar(60)  NOT NULL,
    [numero_operacion] nvarchar(20)  NOT NULL,
    [fecha_creacion] datetime  NOT NULL,
    [creada_por] nvarchar(60)  NOT NULL,
    [mod_asesor] int  NULL,
    [id_cliente] nvarchar(15)  NULL,
    [usuario_cliente] nvarchar(60)  NULL,
    [cliente_nombre] nvarchar(20)  NULL,
    [cliente_apellido_paterno] nvarchar(20)  NULL,
    [cliente_apellido_materno] nvarchar(20)  NULL,
    [email] nvarchar(60)  NULL,
    [telefono] nvarchar(50)  NULL,
    [celular] nvarchar(50)  NULL,
    [activo] int  NULL,
    [comentarios] nvarchar(600)  NULL,
    [preCotizacion] int  NULL,
    [numero_operacion_cotizacion] nvarchar(20)  NULL,
    [Calculo_Costo_Envio] bit  NULL,
    [factura] bit  NULL,
    [OperacionCancelada] bit  NULL,
    [motivoCancelacion] nvarchar(300)  NULL,
    [detalleCancelacionAsesor] nvarchar(800)  NULL,
    [idUsuarioSeguimiento] int  NULL
);
GO

-- Creating table 'pedidos_datosNumericos'
CREATE TABLE [dbo].[pedidos_datosNumericos] (
    [id] int IDENTITY(1,1) NOT NULL,
    [numero_operacion] nvarchar(20)  NOT NULL,
    [monedaPedido] nvarchar(5)  NULL,
    [tipo_cambio] decimal(19,4)  NOT NULL,
    [fecha_tipo_cambio] datetime  NOT NULL,
    [subtotal] decimal(19,4)  NOT NULL,
    [envio] decimal(19,4)  NOT NULL,
    [metodoEnvio] nvarchar(15)  NULL,
    [monedaEnvio] nvarchar(5)  NULL,
    [impuestos] decimal(19,4)  NOT NULL,
    [nombreImpuestos] nvarchar(5)  NULL,
    [total] decimal(19,4)  NOT NULL,
    [descuento] decimal(19,4)  NULL,
    [descuento_porcentaje] decimal(10,4)  NULL,
    [EnvioNota] nvarchar(200)  NULL
);
GO

-- Creating table 'contactos'
CREATE TABLE [dbo].[contactos] (
    [id] int IDENTITY(1,1) NOT NULL,
    [id_cliente] int  NOT NULL,
    [nombre] nvarchar(20)  NOT NULL,
    [apellido_paterno] nvarchar(20)  NULL,
    [apellido_materno] nvarchar(20)  NULL,
    [email] nvarchar(60)  NULL,
    [telefono] nvarchar(50)  NULL,
    [celular] nvarchar(50)  NULL
);
GO

-- Creating table 'usuarios_ligas_confirmaciones'
CREATE TABLE [dbo].[usuarios_ligas_confirmaciones] (
    [idLiga] int IDENTITY(1,1) NOT NULL,
    [usuario] nvarchar(100)  NOT NULL,
    [clave] nvarchar(4000)  NOT NULL,
    [fecha_creacion] datetime  NOT NULL,
    [fecha_vigencia] datetime  NOT NULL
);
GO

-- Creating table 'categorias'
CREATE TABLE [dbo].[categorias] (
    [id] int IDENTITY(1,1) NOT NULL,
    [nombre] nvarchar(50)  NOT NULL,
    [descripcion] nvarchar(250)  NULL,
    [imagen] nvarchar(100)  NULL,
    [rol_categoria] nvarchar(250)  NULL,
    [productos_Destacados] nvarchar(250)  NULL,
    [identificador] nvarchar(30)  NOT NULL,
    [asociacion] nvarchar(30)  NULL,
    [nivel] int  NOT NULL,
    [orden] int  NULL
);
GO

-- Creating table 'usuarios'
CREATE TABLE [dbo].[usuarios] (
    [id] int IDENTITY(1,1) NOT NULL,
    [fecha_registro] datetime  NOT NULL,
    [nombre] nvarchar(100)  NOT NULL,
    [apellido_paterno] nvarchar(20)  NULL,
    [apellido_materno] nvarchar(20)  NULL,
    [email] nvarchar(60)  NOT NULL,
    [password] nvarchar(100)  NOT NULL,
    [tipo_de_usuario] nvarchar(10)  NOT NULL,
    [rango] int  NOT NULL,
    [departamento] nvarchar(20)  NULL,
    [grupoPrivacidad] nvarchar(20)  NULL,
    [perfil_cliente] nvarchar(20)  NULL,
    [id_cliente] nvarchar(15)  NULL,
    [rol_precios_multiplicador] nvarchar(25)  NULL,
    [rol_productos] nvarchar(20)  NULL,
    [rol_categorias] nvarchar(20)  NULL,
    [asesor_base] nvarchar(60)  NULL,
    [grupo_asesor] nvarchar(20)  NULL,
    [asesor_adicional] nvarchar(250)  NULL,
    [grupo_asesores_adicional] nvarchar(250)  NULL,
    [grupo_usuario] nvarchar(20)  NULL,
    [ultimo_inicio_sesion] datetime  NULL,
    [fecha_nacimiento] datetime  NULL,
    [registrado_por] nvarchar(60)  NULL,
    [telefono] nvarchar(50)  NULL,
    [celular] nvarchar(50)  NULL,
    [cuenta_confirmada] bit  NULL,
    [cuenta_activa] bit  NULL
);
GO

-- Creating table 'pedidos_pagos_transferencia'
CREATE TABLE [dbo].[pedidos_pagos_transferencia] (
    [idRegistroTransferencia] int IDENTITY(1,1) NOT NULL,
    [numero_operacion] nvarchar(25)  NOT NULL,
    [referencia] nvarchar(400)  NULL,
    [fecha_captura] datetime  NULL,
    [confirmacionAsesor] bit  NULL,
    [idUsuario] int  NULL,
    [fecha_confirmacion] datetime  NULL
);
GO

-- Creating table 'usuariosInfoes'
CREATE TABLE [dbo].[usuariosInfoes] (
    [id] int IDENTITY(1,1) NOT NULL,
    [idUsuario] int  NOT NULL,
    [registroMetodo] int  NOT NULL,
    [registradoIdAsesor] int  NULL,
    [fecha_registro] datetime  NULL
);
GO

-- Creating table 'PedidosClaveUsoCFDIs'
CREATE TABLE [dbo].[PedidosClaveUsoCFDIs] (
    [id_CFDI] int IDENTITY(1,1) NOT NULL,
    [ClaveUsoCFDI] nvarchar(120)  NOT NULL,
    [Descripción] nvarchar(100)  NOT NULL,
    [PersonaFisica] bit  NULL,
    [PersonaMoral] bit  NULL
);
GO

-- Creating table 'ProductosBloqueoStocks'
CREATE TABLE [dbo].[ProductosBloqueoStocks] (
    [IdBloqueoStock] int IDENTITY(1,1) NOT NULL,
    [numero_parte] nvarchar(100)  NOT NULL,
    [Disponible] decimal(16,2)  NOT NULL
);
GO

-- Creating table 'StockRegistroFaltaDeStocks'
CREATE TABLE [dbo].[StockRegistroFaltaDeStocks] (
    [Id_SRFS] int IDENTITY(1,1) NOT NULL,
    [NumeroParte] nvarchar(100)  NOT NULL,
    [IdUsuario] int  NOT NULL,
    [Fecha] datetime  NOT NULL
);
GO

-- Creating table 'productos_solo_visualizacion'
CREATE TABLE [dbo].[productos_solo_visualizacion] (
    [id] int IDENTITY(1,1) NOT NULL,
    [numero_parte] nvarchar(50)  NOT NULL,
    [solo_para_Visualizar] bit  NOT NULL
);
GO

-- Creating table 'precios_fantasma'
CREATE TABLE [dbo].[precios_fantasma] (
    [id] int IDENTITY(1,1) NOT NULL,
    [numero_parte] nvarchar(50)  NOT NULL,
    [preciosFantasma] decimal(19,4)  NOT NULL,
    [porcentajeFantasma] nvarchar(3)  NULL
);
GO

-- Creating table 'ProductosBloqueoStock1'
CREATE TABLE [dbo].[ProductosBloqueoStock1] (
    [IdBloqueoStock] int IDENTITY(1,1) NOT NULL,
    [numero_parte] nvarchar(100)  NOT NULL,
    [Disponible] decimal(16,2)  NOT NULL
);
GO

-- Creating table 'usuariosInfo1'
CREATE TABLE [dbo].[usuariosInfo1] (
    [id] int IDENTITY(1,1) NOT NULL,
    [idUsuario] int  NOT NULL,
    [registroMetodo] int  NOT NULL,
    [registradoIdAsesor] int  NULL,
    [fecha_registro] datetime  NULL
);
GO

-- Creating table 'PedidosClaveUsoCFDI1'
CREATE TABLE [dbo].[PedidosClaveUsoCFDI1] (
    [id_CFDI] int IDENTITY(1,1) NOT NULL,
    [ClaveUsoCFDI] nvarchar(120)  NOT NULL,
    [Descripción] nvarchar(100)  NOT NULL,
    [PersonaFisica] bit  NULL,
    [PersonaMoral] bit  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [idEstatus] in table 'base_cotizaciones_estatus'
ALTER TABLE [dbo].[base_cotizaciones_estatus]
ADD CONSTRAINT [PK_base_cotizaciones_estatus]
    PRIMARY KEY CLUSTERED ([idEstatus] ASC);
GO

-- Creating primary key on [idComentario] in table 'productos_comentarios'
ALTER TABLE [dbo].[productos_comentarios]
ADD CONSTRAINT [PK_productos_comentarios]
    PRIMARY KEY CLUSTERED ([idComentario] ASC);
GO

-- Creating primary key on [numero_parte], [cantidad], [unidad], [fecha_actualización] in table 'productos_stock'
ALTER TABLE [dbo].[productos_stock]
ADD CONSTRAINT [PK_productos_stock]
    PRIMARY KEY CLUSTERED ([numero_parte], [cantidad], [unidad], [fecha_actualización] ASC);
GO

-- Creating primary key on [id] in table 'pedidos_pagos_paypal'
ALTER TABLE [dbo].[pedidos_pagos_paypal]
ADD CONSTRAINT [PK_pedidos_pagos_paypal]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'glosario'
ALTER TABLE [dbo].[glosario]
ADD CONSTRAINT [PK_glosario]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'infografías'
ALTER TABLE [dbo].[infografías]
ADD CONSTRAINT [PK_infografías]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'carrito_productos'
ALTER TABLE [dbo].[carrito_productos]
ADD CONSTRAINT [PK_carrito_productos]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'direcciones_envio'
ALTER TABLE [dbo].[direcciones_envio]
ADD CONSTRAINT [PK_direcciones_envio]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'direcciones_facturacion'
ALTER TABLE [dbo].[direcciones_facturacion]
ADD CONSTRAINT [PK_direcciones_facturacion]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'estados_mexico'
ALTER TABLE [dbo].[estados_mexico]
ADD CONSTRAINT [PK_estados_mexico]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'paises'
ALTER TABLE [dbo].[paises]
ADD CONSTRAINT [PK_paises]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'pedidos_direccionFacturacion'
ALTER TABLE [dbo].[pedidos_direccionFacturacion]
ADD CONSTRAINT [PK_pedidos_direccionFacturacion]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'pedidos_modificaciones'
ALTER TABLE [dbo].[pedidos_modificaciones]
ADD CONSTRAINT [PK_pedidos_modificaciones]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'pedidos_productos'
ALTER TABLE [dbo].[pedidos_productos]
ADD CONSTRAINT [PK_pedidos_productos]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'pedidos_productos_modificaciones'
ALTER TABLE [dbo].[pedidos_productos_modificaciones]
ADD CONSTRAINT [PK_pedidos_productos_modificaciones]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'precios_ListaFija'
ALTER TABLE [dbo].[precios_ListaFija]
ADD CONSTRAINT [PK_precios_ListaFija]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'precios_multiplicador'
ALTER TABLE [dbo].[precios_multiplicador]
ADD CONSTRAINT [PK_precios_multiplicador]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'precios_rangos'
ALTER TABLE [dbo].[precios_rangos]
ADD CONSTRAINT [PK_precios_rangos]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'productos_Roles'
ALTER TABLE [dbo].[productos_Roles]
ADD CONSTRAINT [PK_productos_Roles]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'productos_unidades'
ALTER TABLE [dbo].[productos_unidades]
ADD CONSTRAINT [PK_productos_unidades]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'permisos_app'
ALTER TABLE [dbo].[permisos_app]
ADD CONSTRAINT [PK_permisos_app]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'cotizaciones_datos'
ALTER TABLE [dbo].[cotizaciones_datos]
ADD CONSTRAINT [PK_cotizaciones_datos]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'cotizaciones_productos'
ALTER TABLE [dbo].[cotizaciones_productos]
ADD CONSTRAINT [PK_cotizaciones_productos]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'productos_Datos'
ALTER TABLE [dbo].[productos_Datos]
ADD CONSTRAINT [PK_productos_Datos]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [idLigasSantander] in table 'pedidos_pagos_liga_santander'
ALTER TABLE [dbo].[pedidos_pagos_liga_santander]
ADD CONSTRAINT [PK_pedidos_pagos_liga_santander]
    PRIMARY KEY CLUSTERED ([idLigasSantander] ASC);
GO

-- Creating primary key on [idRespuestasSantander] in table 'pedidos_pagos_respuesta_santander'
ALTER TABLE [dbo].[pedidos_pagos_respuesta_santander]
ADD CONSTRAINT [PK_pedidos_pagos_respuesta_santander]
    PRIMARY KEY CLUSTERED ([idRespuestasSantander] ASC);
GO

-- Creating primary key on [id] in table 'cotizaciones_datosNumericos'
ALTER TABLE [dbo].[cotizaciones_datosNumericos]
ADD CONSTRAINT [PK_cotizaciones_datosNumericos]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'cotizaciones_direccionEnvio'
ALTER TABLE [dbo].[cotizaciones_direccionEnvio]
ADD CONSTRAINT [PK_cotizaciones_direccionEnvio]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'pedidos_direccionEnvio'
ALTER TABLE [dbo].[pedidos_direccionEnvio]
ADD CONSTRAINT [PK_pedidos_direccionEnvio]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'pedidos_datos'
ALTER TABLE [dbo].[pedidos_datos]
ADD CONSTRAINT [PK_pedidos_datos]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'pedidos_datosNumericos'
ALTER TABLE [dbo].[pedidos_datosNumericos]
ADD CONSTRAINT [PK_pedidos_datosNumericos]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'contactos'
ALTER TABLE [dbo].[contactos]
ADD CONSTRAINT [PK_contactos]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [idLiga] in table 'usuarios_ligas_confirmaciones'
ALTER TABLE [dbo].[usuarios_ligas_confirmaciones]
ADD CONSTRAINT [PK_usuarios_ligas_confirmaciones]
    PRIMARY KEY CLUSTERED ([idLiga] ASC);
GO

-- Creating primary key on [id] in table 'categorias'
ALTER TABLE [dbo].[categorias]
ADD CONSTRAINT [PK_categorias]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'usuarios'
ALTER TABLE [dbo].[usuarios]
ADD CONSTRAINT [PK_usuarios]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [idRegistroTransferencia] in table 'pedidos_pagos_transferencia'
ALTER TABLE [dbo].[pedidos_pagos_transferencia]
ADD CONSTRAINT [PK_pedidos_pagos_transferencia]
    PRIMARY KEY CLUSTERED ([idRegistroTransferencia] ASC);
GO

-- Creating primary key on [id] in table 'usuariosInfoes'
ALTER TABLE [dbo].[usuariosInfoes]
ADD CONSTRAINT [PK_usuariosInfoes]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id_CFDI] in table 'PedidosClaveUsoCFDIs'
ALTER TABLE [dbo].[PedidosClaveUsoCFDIs]
ADD CONSTRAINT [PK_PedidosClaveUsoCFDIs]
    PRIMARY KEY CLUSTERED ([id_CFDI] ASC);
GO

-- Creating primary key on [IdBloqueoStock] in table 'ProductosBloqueoStocks'
ALTER TABLE [dbo].[ProductosBloqueoStocks]
ADD CONSTRAINT [PK_ProductosBloqueoStocks]
    PRIMARY KEY CLUSTERED ([IdBloqueoStock] ASC);
GO

-- Creating primary key on [Id_SRFS] in table 'StockRegistroFaltaDeStocks'
ALTER TABLE [dbo].[StockRegistroFaltaDeStocks]
ADD CONSTRAINT [PK_StockRegistroFaltaDeStocks]
    PRIMARY KEY CLUSTERED ([Id_SRFS] ASC);
GO

-- Creating primary key on [id] in table 'productos_solo_visualizacion'
ALTER TABLE [dbo].[productos_solo_visualizacion]
ADD CONSTRAINT [PK_productos_solo_visualizacion]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'precios_fantasma'
ALTER TABLE [dbo].[precios_fantasma]
ADD CONSTRAINT [PK_precios_fantasma]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [IdBloqueoStock] in table 'ProductosBloqueoStock1'
ALTER TABLE [dbo].[ProductosBloqueoStock1]
ADD CONSTRAINT [PK_ProductosBloqueoStock1]
    PRIMARY KEY CLUSTERED ([IdBloqueoStock] ASC);
GO

-- Creating primary key on [id] in table 'usuariosInfo1'
ALTER TABLE [dbo].[usuariosInfo1]
ADD CONSTRAINT [PK_usuariosInfo1]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id_CFDI] in table 'PedidosClaveUsoCFDI1'
ALTER TABLE [dbo].[PedidosClaveUsoCFDI1]
ADD CONSTRAINT [PK_PedidosClaveUsoCFDI1]
    PRIMARY KEY CLUSTERED ([id_CFDI] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [id_cliente] in table 'contactos'
ALTER TABLE [dbo].[contactos]
ADD CONSTRAINT [fk_contactos_usuarios]
    FOREIGN KEY ([id_cliente])
    REFERENCES [dbo].[usuarios]
        ([id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_contactos_usuarios'
CREATE INDEX [IX_fk_contactos_usuarios]
ON [dbo].[contactos]
    ([id_cliente]);
GO

-- Creating foreign key on [id_cliente] in table 'direcciones_envio'
ALTER TABLE [dbo].[direcciones_envio]
ADD CONSTRAINT [fk_direccionesenvio_usuarios]
    FOREIGN KEY ([id_cliente])
    REFERENCES [dbo].[usuarios]
        ([id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_direccionesenvio_usuarios'
CREATE INDEX [IX_fk_direccionesenvio_usuarios]
ON [dbo].[direcciones_envio]
    ([id_cliente]);
GO

-- Creating foreign key on [id_cliente] in table 'direcciones_facturacion'
ALTER TABLE [dbo].[direcciones_facturacion]
ADD CONSTRAINT [fk_direccionesFact_usuarios]
    FOREIGN KEY ([id_cliente])
    REFERENCES [dbo].[usuarios]
        ([id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_direccionesFact_usuarios'
CREATE INDEX [IX_fk_direccionesFact_usuarios]
ON [dbo].[direcciones_facturacion]
    ([id_cliente]);
GO

-- Creating foreign key on [idUsuario] in table 'permisos_app'
ALTER TABLE [dbo].[permisos_app]
ADD CONSTRAINT [fk_permisos_usuarios]
    FOREIGN KEY ([idUsuario])
    REFERENCES [dbo].[usuarios]
        ([id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_permisos_usuarios'
CREATE INDEX [IX_fk_permisos_usuarios]
ON [dbo].[permisos_app]
    ([idUsuario]);
GO

-- Creating foreign key on [idUsuario] in table 'productos_comentarios'
ALTER TABLE [dbo].[productos_comentarios]
ADD CONSTRAINT [FK_idUsuario]
    FOREIGN KEY ([idUsuario])
    REFERENCES [dbo].[usuarios]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_idUsuario'
CREATE INDEX [IX_FK_idUsuario]
ON [dbo].[productos_comentarios]
    ([idUsuario]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------