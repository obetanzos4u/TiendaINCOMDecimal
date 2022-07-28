using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
public class ProductoNumerosParteDTO
{
    public string numero_parte { get; set; }
    public string noParte_Sap { get; set; }
}
/// <summary>
/// Descripción breve de ProductosTiendaEf
/// </summary>
public class ProductosTiendaEF {
    public ProductosTiendaEF() {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    static public List<productos_Datos> ObtenerTodosSoloNumeroParte() {

        try {

            using (var db = new tiendaEntities()) {
                var Productos = db.productos_Datos
                   .Where(p => p.disponibleVenta ==1 )
                   .Select(x => new productos_Datos {
                       numero_parte = x.numero_parte,
                       noParte_Sap = x.noParte_Sap,
                       
                   })
                    .AsNoTracking()
                    .ToList();


                return Productos;
            }
        }
        catch (Exception ex) {

            return null;
        }
    }
    /// <summary>
    /// Busca el producto por número de parte web o SAP y devuelve ambos campos
    /// </summary>
    static public async Task<ProductoNumerosParteDTO>  ObtenerSoloNumerosParte(string numero_parte)
    {

        try
        {

            using (var db = new tiendaEntities())
            {
                var Productos = await  db.productos_Datos
                     .AsNoTracking()
                   .Where(p => p.numero_parte == numero_parte || p.noParte_Sap == numero_parte)
                   .Select(x => new ProductoNumerosParteDTO
                   {
                       numero_parte = x.numero_parte,
                       noParte_Sap = x.noParte_Sap,

                   })
                   
                    .FirstOrDefaultAsync();


                return Productos;
            }
        }
        catch (Exception ex)
        {

            return null;
        }
    }
    static public async Task<string> ObtenerNumeroParteSAP(string numero_parte)
    {

        try {

            using (var db = new tiendaEntities())
            {
                var Productos = await  db.productos_Datos
                    .AsNoTracking()
                   .Where(p => p.numero_parte == numero_parte)
                   .Select(x => x.noParte_Sap)
                   .FirstOrDefaultAsync();


                return Productos;
            }
        }
        catch (Exception ex)
        {

            return null;
        }
    }
}