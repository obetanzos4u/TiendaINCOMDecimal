using System;
using System.Collections.Generic;

using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/* Este botón se muestra en el archivo [productosTiendaListado.ascx.cs] y Visualizar producto,  sirve para productos que no se realizaran operaciones [pedido/cotización]
  y solo necesitan visualizarce para que el cliente solicite informes de su precio ya que este no se muestra.
     */


public partial class uc_producto_btn_SoloVisualizar : System.Web.UI.UserControl
{
    public void Establecer_Numero_Parte(string Numero_Parte) {

        link_solicitarCotización.NavigateUrl = "/informacion/ubicacion-y-sucursales.aspx?info=Información y cotización del producto: " + Numero_Parte;
    }
    /// <summary>
    /// Si el producto ya se esta visualizando, activamos el botón de envío de email
    /// </summary>
    /// 
    public void solicitarInforme()
    {
       
 
            link_VisualizarProducto.Visible = false;
        link_solicitarCotización.Visible = true;
        /* Pendientes desarrollar la funcionalidad de email */
        //up_solicitarCotización.Visible = true; 


    }

    /// <summary>
    /// Si el producto se muestra en el listado con otros productos, mostramos y establecemos el link para su visualización
    /// </summary>
    /// 
    public void setLink(string linkProducto)
    {
         link_VisualizarProducto.NavigateUrl = linkProducto;
    }





    
}
