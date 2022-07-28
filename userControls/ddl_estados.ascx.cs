using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class uc_ddl_estados : System.Web.UI.UserControl
{
      protected void Page_Init(object sender, EventArgs e)
      {
         if (!IsPostBack)
          {
              using(var db = new tiendaEntities())
              {
                  var estados = db.estados_mexico.AsNoTracking().ToList();

                  ddl_municipio_estado.DataSource = estados;
                  ddl_municipio_estado.DataTextField = "nombre";
                  ddl_municipio_estado.DataValueField = "nombre";
                  ddl_municipio_estado.DataBind();
                ddl_municipio_estado.Items.Insert(0,new ListItem("Selecciona", ""));
                ddl_municipio_estado.DataBind();
                ddl_municipio_estado.ClearSelection();

              }
          } 
} 
    public bool Enabled
    {

        set { this.ddl_municipio_estado.Enabled = value; }
    }




    public string SelectedText
    {
        get { return ddl_municipio_estado.SelectedValue; }
        set {
            try { this.ddl_municipio_estado.SelectedValue = value.ToString(); }
            catch(Exception ex)
            {
               
            }
           
        }
    }

  
}