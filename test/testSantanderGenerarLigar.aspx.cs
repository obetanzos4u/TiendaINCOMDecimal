using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.Remoting.Messaging;

public partial class testSantanderGenerarLigar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      

    }

    protected  void btn_GenerarLigaPago_Click(object sender, EventArgs e)
    {
        Int64 Teléfono = 5559514677;

        SantanderData3ds Data3DS = new SantanderData3ds("resback@gmail.com", Teléfono, "317, 935", 
                                     "Ciudad de México", "CX", "07420");

        SantanderLigaCobro liga = new SantanderLigaCobro("309253934",1, "MXN", Data3DS,DateTime.Now.AddDays(1), "resback@gmail.com");


        liga.GenerarLigaAsync();
 
        string url = liga.URL;

        Log.Text = url;
    }
}