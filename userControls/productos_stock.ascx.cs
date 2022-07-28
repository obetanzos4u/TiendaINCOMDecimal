using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class uc_productos_stock : System.Web.UI.UserControl
{
    public string numero_parte { get; set; }
    
 

    protected void Page_Load(object sender, EventArgs e)
    {
       
          
         
    }


    public void obtenerStock()
    {

      List<productos_stock> stock =  productosStock.obtenerStock(numero_parte);
    DataTable dtStock = productosStock.obtenerStockDT(numero_parte);
        lv_producto_stock.DataSource = dtStock ;
        lv_producto_stock.DataBind();

        if(dtStock.Rows .Count > 0 && dtStock != null)
        {

            titulo.Visible = true;
        }
        

    }

    protected void lv_producto_stock_OnItemDataBound(object sender, ListViewItemEventArgs e)
    {
        System.Data.DataRowView rowView = e.Item.DataItem as System.Data.DataRowView;

      Label lbl_fecha_actualización = (Label)e.Item.FindControl("lbl_fecha_actualización");
        System.Globalization.CultureInfo cultureinfo =
        new System.Globalization.CultureInfo("es-mx");
        // productos_stock entrada = e.Item.DataItem as dynamic;
        lbl_fecha_actualización.Text = String.Format("{0:g}", rowView["fecha_actualización"].ToString(), cultureinfo); 



    }


}