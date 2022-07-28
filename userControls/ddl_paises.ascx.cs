using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class uc_ddl_paises : System.Web.UI.UserControl
{
    public string SelectedText {
        get { return ddl_pais.SelectedItem.Text;  }
        set { this.ddl_pais.SelectedItem.Text = value.ToString(); }
    }
    public bool Enabled
    {
       
        set { this.ddl_pais.Enabled = value; }
    }
 


    public delegate void ChangedIndex(object sender, EventArgs e);
    public event ChangedIndex SelectedIndexChanged;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        ddl_pais.SelectedIndexChanged +=
           new EventHandler(ddl_pais_SelectedIndexChanged);
    }

    protected override void OnLoad(EventArgs e) {
        base.OnLoad(e);
    }

    public void ddl_pais_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (SelectedIndexChanged != null)
        {
            SelectedIndexChanged(sender, e);
        }
    }

}
