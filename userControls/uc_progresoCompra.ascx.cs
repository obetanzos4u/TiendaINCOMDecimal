using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class uc_progresoCompra : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string currentStep = obtenerPasoActual();
        switch (currentStep)
        {
            case "RESUMEN":
                summaryStep();
                break;
            case "PAYPAL":
            case "PAGO":
                paymentStep();
                break;
            case "FINALIZADO":
                finalStep();
                break;
            default:
                defaultStep();
                break;
        }
    }
    protected string obtenerPasoActual()
    {
        string[] absolutePath = HttpContext.Current.Request.Url.AbsolutePath.ToUpper().Split('/');
        // Se recorren dos posiciones del arreglo para obtener el paso
        return absolutePath[absolutePath.Length - 2];
    }
    protected void summaryStep()
    {
        spn_resumen.Attributes["style"] = "color: #1d4ed8;";
        spn_resumen_puntos.Attributes["style"] = "color: #71717a;";
        spn_pago.Attributes["style"] = "color: #71717a;";
        spn_pago_puntos.Attributes["style"] = "color: #71717a;";
        spn_finalizar.Attributes["style"] = "color: #71717a;";
    }
    protected void paymentStep()
    {
        spn_resumen.Attributes["style"] = "color: #10b981;";
        spn_resumen_puntos.Attributes["style"] = "color: #10b981;";
        spn_pago.Attributes["style"] = "color: #1d4ed8;";
        spn_pago_puntos.Attributes["style"] = "color: #71717a;";
        spn_finalizar.Attributes["style"] = "color: #71717a;";
    }
    protected void finalStep()
    {
        spn_resumen.Attributes["style"] = "color: #10b981;";
        spn_resumen_puntos.Attributes["style"] = "color: #10b981;";
        spn_pago.Attributes["style"] = "color: #10b981;";
        spn_pago_puntos.Attributes["style"] = "color: #10b981;";
        spn_finalizar.Attributes["style"] = "color: #10b981;";
    }
    protected void defaultStep()
    {
        spn_resumen.Attributes["style"] = "color: #71717a;";
        spn_resumen_puntos.Attributes["style"] = "color: #71717a;";
        spn_pago.Attributes["style"] = "color: #71717a;";
        spn_pago_puntos.Attributes["style"] = "color: #71717a;";
        spn_finalizar.Attributes["style"] = "color: #71717a;";
    }
}