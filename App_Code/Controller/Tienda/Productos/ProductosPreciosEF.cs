using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// Descripción breve de ProductosTiendaEf
/// </summary>
public class ProductosPreciosEF {
    
  
    public ProductosPreciosEF() {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    } 


    /// <summary>
    ///  
    /// </summary>
    static public async Task<json_respuestas> ObtenerYActualizarPreciosFromSAP(string Url) {
         
        string LogREsultado = "";
        try {
         
            var _user = WebConfigurationManager.AppSettings["UserRestProductivoSAP"];
            var _pwn = WebConfigurationManager.AppSettings["PassRestProductivoSAP"];

            var client = new RestClient("https://my338095.sapbydesign.com");
            client.Authenticator = new HttpBasicAuthenticator(_user, _pwn);

            var request = new RestRequest(Url,Method.GET);

            var response = await client.ExecuteGetAsync(request);

            dynamic JsonREsult = Newtonsoft.Json.JsonConvert.DeserializeObject(response.Content);


            dynamic PreciosProductos = JsonREsult.d.results;

            foreach (dynamic v in PreciosProductos) {

                var Producto = new precios_rangos();

                string numero_parteSAP = v.CPRICESUATION503363A1609841F3;


                var resGetNumerosParte = await ProductosTiendaEF.ObtenerSoloNumerosParte(numero_parteSAP);

                if (resGetNumerosParte != null && numero_parteSAP == resGetNumerosParte.noParte_Sap) {
                    string PrecioStr = v.KCAMOUNTUATION5766473FFF195FF7;
                    decimal Precio = decimal.Parse(PrecioStr);

                    // El precio que arroja por cantidad no siempre es 1, entonces si es por ejemplo 2 piezas el precio, hacemos la división entre precio/cantidad
                    string CantidadStr = v.KCBASEQUUATION001CEA476E31CDC8;
                    decimal Cantidad = decimal.Parse(CantidadStr);

                    if (Cantidad > 1) Precio= Precio / Cantidad;

                    string Moneda =  v.CAMOUNTUATION7FFFD9EB1ED5ED81;
                
                    Producto.numero_parte = textTools.lineSimple(resGetNumerosParte.numero_parte).ToUpper();

                    Producto.moneda_rangos = Moneda;
                    Producto.precio1 = Precio;
                    Producto.max1 = 99999999;

                    var result = await ProductosPreciosEF.CargarPreciosRangos(Producto);

                    LogREsultado += result.message + "\n";

                }
                else {

                    LogREsultado += $"\n El producto {numero_parteSAP} no se encontró en" +
                        $" Productos Datos o no tiene la columna número de parte SAP o es # parte viejo o ocurrió un error al obtener números de parte, " +
                        $" \n";
                }

            }

            return new json_respuestas(true, LogREsultado, false);
        }
        catch (Exception ex) {

            LogREsultado += $"\n Ocurrio un al actualizar los precios desde SAP: \n"+ ex.ToString();
            return new json_respuestas(false, LogREsultado, true, ex);
        }
    }
    /// <summary>
    ///  
    /// </summary>
    static public async Task< List<precios_rangos>> ObtenerPreciosRangos() {

        try {

            using (var db = new tiendaEntities())
            {
                List<precios_rangos> PreciosRangos = await db.precios_rangos
                 .AsNoTracking()
                 .ToListAsync();
              
                return PreciosRangos;
            }
        }
        catch (Exception ex) {

            return null;
        }
    }
      /// <summary>
    ///  Obtiene todo el listado de los precios fantasma
    /// </summary>
    static public async Task< List<precios_fantasma>> ObtenerPreciosFantasma() {

        try {

            using (var db = new tiendaEntities())
            {
                List<precios_fantasma> PreciosFantasma = await db.precios_fantasma
                 .AsNoTracking()
                 .ToListAsync();
              
                return PreciosFantasma;
            }
        }
        catch (Exception ex) {

            return null;
        }
    }

    /// <summary>
    ///  Obtiene un registro de precios fantasma de acuerdo al número de parte
    /// </summary>
    static public async Task<precios_fantasma> ObtenerPreciosFantasma(string numero_parte)
    {

        try
        {

            using (var db = new tiendaEntities())
            {
              var PrecioFantasma = await db.precios_fantasma
                 .AsNoTracking()
                   .Where(p => p.numero_parte == numero_parte)

                 .FirstOrDefaultAsync();

                return PrecioFantasma;
            }
        }
        catch (Exception ex)
        {

            return null;
        }
    }

    /// <summary>
    ///   
    /// </summary>
    static public async Task<productos_solo_visualizacion> ObtenerProductoCotizalo(string numero_parte)
    {

        try
        {

            using (var db = new tiendaEntities())
            {
                var Producto = await db.productos_solo_visualizacion
                   .AsNoTracking()
                     .Where(p => p.numero_parte == numero_parte)

                   .FirstOrDefaultAsync();

                return Producto;
            }
        }
        catch (Exception ex)
        {

            return null;
        }
    }
    /// <summary>
    ///  
    /// </summary>
    static public async Task<json_respuestas> EliminarProductoCotizalo(string numero_parte)
    {

        try
        {

            using (var db = new tiendaEntities())
            {
                var ProductoCotizalo = await db.productos_solo_visualizacion
                    .Where(p => p.numero_parte == numero_parte)
                    .FirstOrDefaultAsync();

                if (ProductoCotizalo != null)
                {

                    db.productos_solo_visualizacion.Remove(ProductoCotizalo);
                    await db.SaveChangesAsync();
                    return new json_respuestas(true, $"Se eliminó el registro en Cotízalo para # parte: [{numero_parte}]");
                }
                else
                {
                    return new json_respuestas(true, $"No se encontró el registro en Cotízalo para # parte: [{numero_parte}]");

                }
            }



        }
        catch (Exception ex)
        {
            return new json_respuestas(true, $"Ocurrio un error al eliminar en Cotízalo # parte: [{numero_parte}] " + ex.Message);

        }
    }

    /// <summary>
    ///  
    /// </summary>
    static public async Task<json_respuestas> EliminarTodosLosProductosCotizalo() {

        try {

            using (var db = new tiendaEntities()) {
               var Result = await db.Database.ExecuteSqlCommandAsync("TRUNCATE TABLE [productos_solo_visualizacion]");

                    await db.SaveChangesAsync();
                    return new json_respuestas(true, $"e ha eliminado los productos de cotizalo: {Result}");
              
            }

        }
        catch (Exception ex) {
            return new json_respuestas(true, $"Ocurrio un error al eliminar productos de Cotízalo;" + ex.Message);

        }
    }
    /// <summary>
    ///  
    /// </summary>
    static public  async Task< precios_rangos>  ObtenerPreciosRangos(string numero_parte)
    {

        try
        {

            using (var db = new tiendaEntities())
            {
                var Precios =  await db.precios_rangos
                     .AsNoTracking()
                 .Where(p => p.numero_parte == numero_parte)
                
                  .FirstOrDefaultAsync(); 
                return Precios;
            }


            
        }
        catch (Exception ex)
        {

            return null;
        }
    }
    /// <summary>
    ///  
    /// </summary>
    static public async Task<json_respuestas> EliminarPreciosRangos(string numero_parte)
    {

        try
        {

            using (var db = new tiendaEntities())
            {
                var Precio = await db.precios_rangos
                    .Where(p => p.numero_parte == numero_parte)
                    .FirstOrDefaultAsync();

                if (Precio != null)
                {

                    db.precios_rangos.Remove(Precio);
                    await db.SaveChangesAsync();
                    return new json_respuestas(true, $"Se eliminó el registro en Precios Rangos para # parte: [{numero_parte}]");
                }
                else
                {
                    return new json_respuestas(true, $"No se encontró el registro en Precios Rangos  para # parte: [{numero_parte}]");

                }
            }



        }
        catch (Exception ex)
        {
            return new json_respuestas(true, $"Ocurrio un error al eliminar  en Precios Rangos # parte: [{numero_parte}] " + ex.Message);
      
        }
    }
    /// <summary>
    ///  
    /// </summary>
    static public async Task<json_respuestas> EliminarPreciosListaFija(string numero_parte)
    {

        try
        {

            using (var db = new tiendaEntities())
            {
                var Precio = await db.precios_ListaFija
                    .Where(p => p.numero_parte == numero_parte)
                    .FirstOrDefaultAsync();

                if (Precio != null)
                {

                    db.precios_ListaFija.Remove(Precio);
                    await db.SaveChangesAsync();
                    return new json_respuestas(true, $"Se eliminó el registro de la Lista Fija para # parte: [{numero_parte}]");
                }
                else
                {
                    return new json_respuestas(true, $"No se encontró el registro en Lista Fija para # parte: [{numero_parte}]");

                }
            }



        }
        catch (Exception ex)
        {
            return new json_respuestas(true, $"Ocurrio un error al eliminar eb  Lista Fija # parte: [{numero_parte}] " + ex.Message);

        }
    }
    /// <summary>
    /// 
    /// </summary>
    static public async Task<json_respuestas>  CargarPreciosRangos(precios_rangos rangos)
    {

        try
        {

            using (var db = new tiendaEntities())
            {
                var Precios = await db.precios_rangos
                   .Where(p => p.numero_parte == rangos.numero_parte)
                   .FirstOrDefaultAsync();
               

                if(Precios != null)
                {
                    Precios.moneda_rangos = rangos.moneda_rangos;
                    Precios.max1 = rangos.max1;
                    Precios.precio1 = rangos.precio1;

                    Precios.max2 = rangos.max2;
                    Precios.precio2 = rangos.precio2;

                    Precios.max3 = rangos.max3;
                    Precios.precio3 = rangos.precio3;

                    Precios.max4 = rangos.max4;
                    Precios.precio4 = rangos.precio4;


                    Precios.max5 = rangos.max5;
                    Precios.precio5 = rangos.precio5;

                    await db.SaveChangesAsync();

                    return new json_respuestas(true, $"Actualizado con éxito, # parte: [{rangos.numero_parte}]");
                }
                else
                {

                     db.precios_rangos.Add(rangos);
                    await db.SaveChangesAsync();
                    return new json_respuestas(true, $"Creado con éxito, # parte: [{rangos.numero_parte}]");

                }
                /*else
                {
                    return new json_respuestas(false, $"No se encontró el rango con el # parte: [{numero_parte}]");
                }
                */
            }



        }
        catch (Exception ex)
        {
            return new json_respuestas(false, $"Ocurrio un error con el # parte: [{rangos.numero_parte}]", true, ex.Message);

           
        }
    }

    /// <summary>
    /// 
    /// </summary>
    static public async Task<json_respuestas> CargarPreciosFantasma(string numero_parte, decimal precioFantasma)
    {

        try
        {

            using (var db = new tiendaEntities())
            {
                var Precio = await db.precios_fantasma
                   .Where(p => p.numero_parte == numero_parte)
                   .FirstOrDefaultAsync();


                if (Precio != null)
                {
                    Precio.preciosFantasma = precioFantasma;
                   

                    await db.SaveChangesAsync();

                    return new json_respuestas(true, $"Se actualizó el registro para # parte: [{numero_parte}]");
                }
                else
                {
                    var PrecioFantasma = new precios_fantasma()
                    {
                        numero_parte= numero_parte,
                        preciosFantasma = precioFantasma
                    };
                    db.precios_fantasma.Add(PrecioFantasma);
                    await db.SaveChangesAsync();
                    return new json_respuestas(true, $"Se creó el registro para # parte: [{numero_parte}]");
                }
            }



        }
        catch (Exception ex)
        {
            return new json_respuestas(false, $"Ocurrio un error con el # parte: [{numero_parte}] "+ ex.Message, true, ex.Message);


        }
    }

    /// <summary>
    /// 
    /// </summary>
    static public async Task<json_respuestas> EstablecerProductoEnCotizalo(string numero_parte)
    {

        try
        {

            using (var db = new tiendaEntities())
            {
                var Producto = await db.productos_solo_visualizacion
                   .Where(p => p.numero_parte == numero_parte)
                   .FirstOrDefaultAsync();


                if (Producto != null)
                {
                    Producto.solo_para_Visualizar = true;


                    await db.SaveChangesAsync();

                    return new json_respuestas(true, $"Se estableció en cotízalo para # parte: [{numero_parte}]");
                }
                else
                {
                    var ProductoAdd = new productos_solo_visualizacion()
                    {
                        numero_parte = numero_parte,
                        solo_para_Visualizar = true
                    };
                    db.productos_solo_visualizacion.Add(ProductoAdd);
                    await db.SaveChangesAsync();
                    return new json_respuestas(true, $"Se creó el registro en cotízalo para # parte: [{numero_parte}]");
                }
            }



        }
        catch (Exception ex)
        {
            return new json_respuestas(false, $"Ocurrio un error con el # parte: [{numero_parte}] " + ex.Message, true, ex.Message);


        }
    }
    static public async Task<json_respuestas> EliminarPreciosFantasma(string numero_parte)
    {

        try
        {

            using (var db = new tiendaEntities())
            {
                var Precio = await db.precios_fantasma
                   .Where(p => p.numero_parte == numero_parte)
                   .FirstOrDefaultAsync();


                if (Precio != null)
                {

                    db.precios_fantasma.Remove(Precio);
                    await db.SaveChangesAsync();
                    return new json_respuestas(true, $"Se eliminó el registro en Precios Fantasma para # parte: [{numero_parte}]");
                }
                else
                {
                    return new json_respuestas(true, $"No se encontró el registro  en Precios Fantasma para # parte: [{numero_parte}]");
                   
                }
            }



        }
        catch (Exception ex)
        {
            return new json_respuestas(false, $"Ocurrio un error al eliminar en Precios Fantasma con el # parte: [{numero_parte}]", true, ex.Message);


        }
    }
}