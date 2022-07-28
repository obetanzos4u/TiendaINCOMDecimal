using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de privacidad
/// </summary>
public class privacidad
{
    static string rolGeneralName = "general";

    /// <summary>
    ///  Array de roles permitidos para  visibilidad del producto contra el rol de producto del usuario
    /// </summary>
    public static bool validarProducto(string[] rol_visibilidadProducto, string usuario_rol_producto)
    {
        foreach (string rol_Prod in rol_visibilidadProducto)
        {
            // Si el rol de la categoria tiene como general, por default es visible para todo el público
            if (rol_Prod == usuario_rol_producto || rol_Prod == rolGeneralName)
            {
                return true;

            } 
        }

        return false;
    }

    /// <summary>
    /// Recibe un DT con los productos y elimina los productos (filas) que no cumplan con los roles de privacidad. Cuando el usuario logeado es un asesor, se permite
    /// </summary>
    public static DataTable procesarProductos(DataTable dt)
    {
        if (dt.Rows.Count < 1 || dt == null) return dt;

        foreach( DataRow r in dt.Rows)
        {
            string rol = r[""].ToString();

            if (rol.Contains(","))
            {

            }
        }

        return null;
    }

    /// <summary>
    /// Array de los roles permisibles en la categoria contra los roles de categoria disponibles para el usuario;
    /// </summary>
    public static bool validarCategoria(string[] rol_visibilidadCategorias, string[] usuario_rol_categorias)
    {
        // Comenzamos con la validación de roles[] de categoria del usuario logeado contra la visibilidad de la configuración de la categoria
        // Para eso necesitamos recorrer cada uno de los roles de cat. que el usuario tenga asignado contra los roles permitidos de la categoria
        foreach (string rol_user in usuario_rol_categorias)
        {
            foreach (string rol_Cat in rol_visibilidadCategorias)
            {
                // Si el rol de la categoria tiene como general, por default es visible para todo el público
                if (rol_Cat.Replace(" ", "") == rolGeneralName || rol_user.Replace(" ", "") == rol_Cat.Replace(" ", ""))
                {
                    return true;
                }
            }

        }
        return false;
    }
}