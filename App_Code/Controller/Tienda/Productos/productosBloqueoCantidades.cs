using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Funciones para la administración de bloqueo de productos, los productos que se inserten en esta tabla limitara la cantidad de compra, cotización o carrito.
/// </summary>
public class ProductosBloqueoCantidades
{
    public struct ProductoBloqueoCantidadStruct
    {
        public string numero_parte;
        public decimal cantidadMaximaDisponible;
    }
    public ProductosBloqueoCantidades()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    /// <summary>
    /// 20220523 - Devuelve [true] si la cantidad solicitada es menor a la cantidad permitida, de lo contrario devuelve [false]
    /// </summary>
    public static async Task<json_respuestas> ValidarCantidadAsync (string numero_parte, decimal CantidadSolicitada){

        ProductosBloqueoStock ObtenerBloqueoCantidadProd;

        using (tiendaEntities db = new tiendaEntities())
        {
              ObtenerBloqueoCantidadProd = await db.ProductosBloqueoStocks.
                AsNoTracking()
                .Where(a => a.numero_parte == numero_parte).FirstOrDefaultAsync();
          
        }

        if(ObtenerBloqueoCantidadProd != null)
        
            return ObtenerBloqueoCantidadProd.Disponible >= CantidadSolicitada ?  
                new json_respuestas(true,"Cantidad válida") : new json_respuestas(false, "Cantidad máxima de venta: "+ ObtenerBloqueoCantidadProd.Disponible);


        // Si no hay registro permitimos el proceso
        return new json_respuestas(true, "Cantidad válida");
    }
    /// <summary>
    /// 20220523 - Devuelve la cantidad máxima del producto si existe un registro, si no, devuelve [null]
    /// </summary>
    public static async Task<decimal?> ObtenerCantidadMaxima(string numero_parte)
    {

        ProductosBloqueoStock ObtenerBloqueoCantidadProd;

        using (tiendaEntities db = new tiendaEntities())
        {
            ObtenerBloqueoCantidadProd = await  db.ProductosBloqueoStocks.
              AsNoTracking()
              .Where(a => a.numero_parte == numero_parte).FirstOrDefaultAsync();



        }

        if (ObtenerBloqueoCantidadProd != null)

            return ObtenerBloqueoCantidadProd.Disponible;
        else
        {
            return null;
        }



    }
    public static async Task<json_respuestas> EliminarTodosLosRegistrosAsync()
    {
        int Result = 0;
        try
        {
            using (tiendaEntities db = new tiendaEntities())
            {

                Result = await db.Database.ExecuteSqlCommandAsync("TRUNCATE TABLE [ProductosBloqueoStock]");
            }

            return new json_respuestas(true, $@"Se han borrado un total de: {Result} registros." , false);
        }
        catch(Exception ex)
        {

            return new json_respuestas(false, "Ocurrio un error al borrar los registros: "+ ex.Message, true,ex);
        }


            
    }
        public static async Task<json_respuestas> DescontarProductosAsync(string numero_parte, decimal CantidadARestar)
    {
        try { 
        ProductosBloqueoStock ObtenerBloqueoCantidadProd;

        using (tiendaEntities db = new tiendaEntities())
        {
            ObtenerBloqueoCantidadProd = await db.ProductosBloqueoStocks
              .Where(a => a.numero_parte == numero_parte).FirstOrDefaultAsync();

            if (ObtenerBloqueoCantidadProd.numero_parte != null)
            {
                ObtenerBloqueoCantidadProd.Disponible = ObtenerBloqueoCantidadProd.Disponible - CantidadARestar;
                await db.SaveChangesAsync();

                return new json_respuestas(true, "Actualizado con éxito", false);
            }
            else
            {
                return new json_respuestas(false, "No se encontró un registro a actualizar", false);

            }

        }
        } catch(Exception ex)
        {

            return new json_respuestas(false, "Hubo un error al actualizar: " + ex.Message, true,ex);
        }



    }


    /// <summary>
    /// 20220526 - Actualiza o inserta los registros recibidos.
    /// </summary>
    public static async Task<json_respuestas> CargarListadoProductosBloqueoMax(List<ProductoBloqueoCantidadStruct> ListadoProducto)
    {

        using (var db = new tiendaEntities())
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
              

                try {
                    foreach (var p in ListadoProducto)
                    {
                        string numero_parte = p.numero_parte;
                        


                        Decimal DisponibleMaximo = p.cantidadMaximaDisponible;


                        var ObtenerRegistro = await db.ProductosBloqueoStocks
                            .Where(pSearch => pSearch.numero_parte == numero_parte)
                            .FirstOrDefaultAsync();

                        if(ObtenerRegistro != null) {
                            ObtenerRegistro.Disponible = p.cantidadMaximaDisponible;
                        }
                        // Si no existe, añadimos el registro.
                        else {
                            var Registro = new ProductosBloqueoStock() {
                                numero_parte = numero_parte,
                                Disponible = DisponibleMaximo
                            };
                            db.ProductosBloqueoStocks.Add(Registro);
                        };

                        db.SaveChanges();
                    }



                    
                    transaction.Commit();
                    return new json_respuestas(true, "Registros cargados con éxito");

                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                  
                    return new json_respuestas(false, $"Ocurrio un error al guardar un registro, Ex: " + ex.ToString(),true, ex);

                }
                finally
                {




                }
            }
        }
    }
}