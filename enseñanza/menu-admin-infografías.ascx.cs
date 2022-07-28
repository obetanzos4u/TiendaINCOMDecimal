using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class enseñanza_menu_admin_infografías : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void activeTab(string tabName) {

        switch (tabName)
        {

            case "crear": li_crear.Attributes.Add("class", "is-active"); break;
            case "editar": li_editar.Attributes.Add("class", "is-active"); break;

        }


    }



}