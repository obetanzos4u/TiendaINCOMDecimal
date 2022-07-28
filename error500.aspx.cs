using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class error_500 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            /*
            foreach (System.Collections.DictionaryEntry entry in HttpContext.Current.Cache) {
                HttpContext.Current.Cache.Remove((string)entry.Key);
            }
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            */
            FormsAuthentication.SignOut();
            
        }
    }
}