﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class restablecer_password_email_enviado : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {    
      materializeCSS.crear_toast(this, "Se ha enviado un email con las instrucciones", true);
        
    }
}