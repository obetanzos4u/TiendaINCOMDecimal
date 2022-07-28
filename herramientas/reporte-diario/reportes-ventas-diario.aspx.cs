using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class herramientas_reportes_ventas_diario : System.Web.UI.Page {
    NumberFormatInfo myNumberFormatInfo = new CultureInfo("es-MX", true).NumberFormat;
    protected void Page_Load(object sender, EventArgs e) {

        if (!IsPostBack) {
            cargarDias();
        }
    }
    
    void cargarDias() {

        int diaActual = DateTime.Now.Day;
        int mesActual = DateTime.Now.Month;
        int añoActual = DateTime.Now.Year;

        for (int i = 1; i <= 12; i++) {
            ListItem mes = new ListItem();
            mes.Text = new DateTime(2018, i, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("es"));
            mes.Value = i.ToString();
            //  ddl_mes.Items.Add(mes);
        }

        int diasEnMes = DateTime.DaysInMonth(añoActual, mesActual);


        for (int i = 1; i <= diasEnMes; i++) {
            ListItem dia = new ListItem();
            dia.Text = i.ToString();
         //   ddl_dia.Items.Add(dia);
        }
    }



    protected void btn_mostrarFechaActual_Click(object sender, EventArgs e) {

        content_datos_reporte_porcentajes.Visible = false;
        content_datos_reporte.Visible = true ;
        // Fecha Periodos
        string[] fechaActualArray = txt_fechaActual.Text.Split('-');
        int añoActual = int.Parse(fechaActualArray[0]);
        int mesActual = int.Parse(fechaActualArray[1]);
        int diaActual = int.Parse(fechaActualArray[2]);

        string nombreMesActual = new DateTime(añoActual, mesActual, diaActual).ToString("MMMM", CultureInfo.CreateSpecificCulture("es"));
        DateTime fechaDesde = new DateTime(añoActual, mesActual, 1);
        DateTime fechaActual = new DateTime(añoActual, mesActual, diaActual);

       
        DateTime dt_acomuladoAnualInicio = new DateTime(añoActual, 1, 1);

        DataTable dtFacturadoAsesores = reportesVentasDiario.recuperarMonto_facturadoAsesores(fechaDesde, fechaActual);
        DataTable dtUltimosProductosRegistrados = reportesVentasDiario.recuperarUltimosProductosRegistrados(10, fechaDesde, fechaActual);
        DataTable dtPedidosAsesores= reportesVentasDiario.recuperarMonto_pedidosAsesores(fechaDesde, fechaActual);

        decimal montoMetaMes = reportesVentasDiario.montoMetaMes(añoActual, mesActual);

        // Fecha Periodos
        string[] diasLaborales = reportesVentasDiario.obtenerDiasLAborales(añoActual, mesActual).Split(',');
        decimal objetivoDiario = montoMetaMes / int.Parse(diasLaborales[diasLaborales.Length - 1]);
        int numeroDiaLaboral = int.Parse(diasLaborales[diaActual]);
        string numeroDiaLaboralRestantes = (int.Parse(diasLaborales[diasLaborales.Length - 1]) - numeroDiaLaboral).ToString();

        decimal facturadoMes = reportesVentasDiario.montoFacturado(fechaDesde, fechaActual);
        decimal acomuladoAnual = reportesVentasDiario.montoFacturado(dt_acomuladoAnualInicio, fechaActual);

        string redText = "color: #ef4e4e !important;";
        string greenText = "color: #74d21f !important;";
        decimal expectativaActualMesFacturado = numeroDiaLaboral * objetivoDiario;
        decimal montoFacturadoDiferenciaActualVSMetaMes = montoMetaMes - facturadoMes;
        decimal montoTotalPedidosIngresadosMes = reportesVentasDiario.montoPedidosIngresados(fechaDesde, fechaActual);
        decimal montoDiferenciaActualVSExpectativaActual = expectativaActualMesFacturado - facturadoMes;

        string txtAbajo = "abajo";
        string txtArriba = "arriba";


        decimal facturadoEnElDia = reportesVentasDiario.montoFacturado(fechaActual, fechaActual);

        decimal porcentajeExpectativaDía = Math.Round((facturadoEnElDia / objetivoDiario) * 100);

        decimal porcentajeAlMes = Math.Round(((facturadoMes / montoMetaMes) * 100), 2);
        decimal porcentajeExpectativaMes = Math.Round((expectativaActualMesFacturado / montoMetaMes) * 100);
        decimal diferenciaPorcentajeRealVsExpectativa = porcentajeExpectativaMes - porcentajeAlMes;
        decimal  montoTotalFacturadoMesTelemarketing = reportesVentasDiario.montoFacturado(fechaDesde, fechaActual,"Telemarketing");


        decimal porcentajeExpectativaDíaRedondeado = decimal.Parse(Math.Round(porcentajeExpectativaDía, 2).ToString());
        //Porcentaje expectativa del día: 
        lbl_porcentajeExpectativaDía.Text = "Cumplimos el "+
          porcentajeExpectativaDíaRedondeado.ToString() +
            "% de nuestra meta diaria.";

        if(porcentajeExpectativaDíaRedondeado < 100) {
            lbl_porcentajeExpectativaDía.Text += " Hay que recuperar el " + (100 - porcentajeExpectativaDíaRedondeado) + "% " +
                " el dia de mañana";
            lbl_porcentajeExpectativaDía.Attributes.Add("style", redText);
        }

        lbl_objetivo_diario.Text = Math.Round(objetivoDiario, 2).ToString("C2", myNumberFormatInfo) + " MXN";
        lbl_facturadoElDia.Text = facturadoEnElDia.ToString("C2", myNumberFormatInfo) + " MXN";
        lbl_numeroDiaLaboralTranscurridos.Text = numeroDiaLaboral.ToString();
        lbl_numeroDiaLaboralRestantes.Text = numeroDiaLaboralRestantes;

        lbl_expectativaActualMes.Text = expectativaActualMesFacturado.ToString("C2", myNumberFormatInfo) + " MXN";
        lbl_facturadoMes.Text = facturadoMes.ToString("C2", myNumberFormatInfo) + " MXN";
        lbl_facturadoMetaMes.Text = montoMetaMes.ToString("C2", myNumberFormatInfo) + " MXN";


        lbl_montoFacturadoDiferenciaActualVSMetaMes.Text = montoFacturadoDiferenciaActualVSMetaMes.ToString("C2", myNumberFormatInfo) + " MXN";

        lt_diaReporteSeleccionado.Text = diaActual.ToString();

        lbl_montoTotalPedidosIngresadosMes.Text =  montoTotalPedidosIngresadosMes.ToString("C2", myNumberFormatInfo);
        lbl_porcentajeMensual.Text = decimal.Parse(Math.Round(porcentajeAlMes, 2).ToString()) + " %";
        lbl_porcentajeExpectativaMensual.Text= "Deberiamos ir  al " + porcentajeExpectativaMes + "% y vamos  al " +  porcentajeAlMes + "%";

        lbl_acomuladoAnual.Text = acomuladoAnual.ToString("C2", myNumberFormatInfo) + " MXN";
        
        h2_periodoVendedorFacturado.InnerHtml = "Facturado de asesores del 1 al " + diaActual + " de " + nombreMesActual;
        h2_periodoVendedorPedidos .InnerHtml = "Montos de pedidos ingresados de asesores del 1 al " + diaActual + " de " + nombreMesActual;
        h1_titulo_facturado.InnerHtml = "Datos del 1 al " + diaActual + " de " + nombreMesActual;

        lbl_montoTotalFacturadoMesTelemarketing.Text = montoTotalFacturadoMesTelemarketing.ToString("C2", myNumberFormatInfo);

        if (facturadoMes < expectativaActualMesFacturado) {
            lbl_porcentajeExpectativaMensual.Text += " ("+ diferenciaPorcentajeRealVsExpectativa  +"% "+ txtAbajo + ")";
            lbl_porcentajeExpectativaMensual.Attributes.Add("style", redText);

        } else {
            lbl_porcentajeExpectativaMensual.Text += " (" + diferenciaPorcentajeRealVsExpectativa.ToString().Substring(1, diferenciaPorcentajeRealVsExpectativa.ToString().Length-1) + "% " + txtArriba+ ")";
            lbl_porcentajeExpectativaMensual.Attributes.Add("style", greenText);
        }


        string trCss = "style='border-bottom: 1px solid #d0d0d0;'";
        string tdCSSRight = " style='padding: 3px 3px; text-align: right; ' ";
        string tdCSSLeft = " style='padding: 3px 3px; text-align: left; ' ";


        // INICIO - Código que llena la tabla de vendedores
        string td = string.Empty;
        foreach(DataRow r in dtFacturadoAsesores.Rows) {
            string[] nombresVendedor = r["NombreVendedor"].ToString().Split(' ') ;
            string nombreVendedor = nombresVendedor[0] + " " + nombresVendedor[1];
            string monto_facturado = decimal.Parse(r["MontoFacturado"].ToString()).ToString("C2", myNumberFormatInfo) + "";
            td += "<tr "+ trCss+" ><td "+ tdCSSLeft + " > " + nombreVendedor + "</td><td" + tdCSSRight + " >" + monto_facturado + "</td> <tr>";
        }
        td += "<tr " + trCss + " ><td " + tdCSSLeft + " >Total Telemarketing</td><td" + tdCSSRight + " >" + lbl_montoTotalFacturadoMesTelemarketing.Text + "</td> <tr>";



        string tablaVendedoresFacturado = "<table><thead><tr><th>Asesor</th><th>Facturado</th></tr></thead>" + td + "<tbody></table>";
        vendedoresFacturado.InnerHtml = tablaVendedoresFacturado;
        // FIN - Código que llena la tabla de vendedores




        // INICIO - Código que llena la tabla de PEDIDOS
        td = string.Empty;
        foreach (DataRow r in dtPedidosAsesores.Rows) {
            string[] nombresVendedor = r["NombreVendedor"].ToString().Split(' ');
            string nombreVendedor = nombresVendedor[0] + " " + nombresVendedor[1];
            string monto_pedidos = decimal.Parse(r["montoPedidos"].ToString()).ToString("C2", myNumberFormatInfo) + "";
            td += "<tr " + trCss + " ><td" +  tdCSSLeft + " >" + nombreVendedor + "</td><td" + tdCSSRight + " >" + monto_pedidos  + "</td><td><tr>";
        }
        td += "<tr " + trCss + " ><td" + tdCSSLeft + " >Total Todos</td><td" + tdCSSRight + " >" + lbl_montoTotalPedidosIngresadosMes.Text + "</td><td><tr>";
        string tablaVendedoresPedidos = "<table><thead><tr><th>Asesor</th><th>Pedidos</th></tr></thead>" + td + "<tbody></table>";
        vendedoresPedidos.InnerHtml = tablaVendedoresPedidos;
        // FIN - Código que llena la tabla de vendedores


        // INICIO - Código que llena la tabla de PRODUCTOS
          td = string.Empty;
        foreach (DataRow r in dtUltimosProductosRegistrados.Rows) {
           
            string numero_parte = r["numero_parte"].ToString();
            string monto_facturado = decimal.Parse(r["montoFacturado"].ToString()).ToString("C2", myNumberFormatInfo) + " MXN";
            string fechaFacturado = r["fechaFacturado"].ToString();
            string nombreVendedor = r["NombreVendedor"].ToString();
            td += "<tr " + trCss + " ><td" + tdCSSLeft + " >" + numero_parte + "</td><td>" + monto_facturado + "</td><td" + tdCSSRight + " >" + nombreVendedor + "</td><td" + tdCSSLeft + " > " + fechaFacturado + "</td></tr>";
        }
        string tablaUltimosProductosFacturados = "<table><thead><tr " + trCss + " ><th>No. Parte</th><th>Monto</th><th>Asesor</th><th>Fecha</th></tr></thead>"
            + td + "<tbody></table>";
        ultimosProductosRegistrados.InnerHtml = tablaUltimosProductosFacturados;
        // FIN - Código que llena la tabla de vendedores

        // INICIO de barra
        string styleBarraAzul = "height:38px;background:#007cc9;width:" + porcentajeAlMes + "%;";
        barraAzulFacturadoMes.Attributes.Add("style", styleBarraAzul);
        Panel textoBarraFacturadoMes = new Panel();
        textoBarraFacturadoMes.Attributes.Add("style", "text-shadow: 1px 1px 2px rgba(0, 0, 0, 0.87); color: white; margin:0 50%; line-height: 38px");
        HtmlGenericControl textBarra = new HtmlGenericControl("div");
        textBarra.Attributes.Add("style", "position: absolute;");
        textBarra.InnerHtml = lbl_porcentajeExpectativaMensual.Text; //porcentajeAlMes + "%";
         textoBarraFacturadoMes.Controls.Add(textBarra);
        barraAzulFacturadoMes.Controls.Add(textoBarraFacturadoMes);
        // FIN - barra

        UpdatePanel2.Update();
       // ocultarInformación();
    }

    protected void ocultarInformación() {
        content_datos_reporte.Visible = false;
    }
    protected void btn_enviarReporte_Click(object sender, EventArgs e) {

         
        var outputBuffer = new StringBuilder();
        using (var writer = new HtmlTextWriter(new StringWriter(outputBuffer))) {
            cont_barraFacturado.RenderControl(writer);
        }

        var outputBufferTableFacturado = new StringBuilder();

        using (var writer = new HtmlTextWriter(new StringWriter(outputBufferTableFacturado))) {
            vendedoresFacturado.RenderControl(writer);
        }


        var outputBufferTablePedidos = new StringBuilder();

        using (var writer = new HtmlTextWriter(new StringWriter(outputBufferTablePedidos))) {
            vendedoresPedidos.RenderControl(writer);
        }


        string barraFacturado = outputBuffer.ToString();
        string mensaje = "";
        string filePathHTML = "/email_templates/reportes/pedidosYfacturacion.html";
        Dictionary<string, string> datosDiccRemplazo = new Dictionary<string, string>();

        datosDiccRemplazo.Add("{tituloPrincipal}", h1_titulo_facturado.InnerHtml);

        datosDiccRemplazo.Add("{porcentajeTexto}", lbl_porcentajeExpectativaMensual.Text);


        datosDiccRemplazo.Add("{tituloTablaFacturado}", h2_periodoVendedorFacturado.InnerHtml);
        datosDiccRemplazo.Add("{tituloTablaPedidos}", h2_periodoVendedorPedidos.InnerHtml);

       
        datosDiccRemplazo.Add("{TableFacturado}", outputBufferTableFacturado.ToString());
        datosDiccRemplazo.Add("{TablePedidos}", outputBufferTablePedidos.ToString());


        datosDiccRemplazo.Add("{ExpectativaActualMes}", lbl_expectativaActualMes.Text);
        datosDiccRemplazo.Add("{porcentajeExpectativaMensual}", lbl_porcentajeExpectativaMensual.Text);

        datosDiccRemplazo.Add("{cont_barraFacturado}", barraFacturado);
        datosDiccRemplazo.Add("{dominio}", HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority));
        datosDiccRemplazo.Add("{objetivoDiario}", lbl_objetivo_diario.Text);
        datosDiccRemplazo.Add("{facturadoEnElDia}", lbl_facturadoElDia.Text);
        datosDiccRemplazo.Add("{numeroDiaLaboral}", lbl_numeroDiaLaboralTranscurridos.Text);
        datosDiccRemplazo.Add("{montoFacturadoDiferenciaActualVSMes}", lbl_montoFacturadoDiferenciaActualVSMetaMes.Text);
        datosDiccRemplazo.Add("{numeroDiaLaboralRestantes}", lbl_numeroDiaLaboralRestantes.Text);
        datosDiccRemplazo.Add("{facturadoMes}", lbl_facturadoMes.Text);
        datosDiccRemplazo.Add("{montoMetaMes}", lbl_facturadoMetaMes.Text);
        datosDiccRemplazo.Add("{porcentajeAlMes}", lbl_porcentajeMensual.Text);
        datosDiccRemplazo.Add("{acomuladoAnual}", lbl_acomuladoAnual.Text);

        datosDiccRemplazo.Add("{diaReporteSeleccionado}",  lt_diaReporteSeleccionado.Text);

        datosDiccRemplazo.Add("{montoTotalPedidosIngresadosMes}", lbl_montoTotalPedidosIngresadosMes.Text);
        datosDiccRemplazo.Add("{montoTotalFacturadoMesTelemarketing}", lbl_montoTotalFacturadoMesTelemarketing.Text);
        
        mensaje = archivosManejador.reemplazarEnArchivo(filePathHTML, datosDiccRemplazo);

        emailDevelopment enviar = new emailDevelopment("", "jvazquez@incom.mx", mensaje, "development@incom.mx");

     
         enviar.enviarReporteFacturacionPedidos();
      
        //lbl_mensaje.Text =  enviar.resultado;
        materializeCSS.crear_toast(this, enviar.resultadoMensaje, enviar.resultado);
    }

    protected void btn_solo_porcentajes_Click(object sender, EventArgs e) {
        btn_mostrarFechaActual_Click(sender, e);

        h1_titulo_facturado2.InnerHtml = h1_titulo_facturado.InnerHtml;
        lbl_numeroDiaLaboralTranscurridos2.Text = lbl_numeroDiaLaboralTranscurridos.Text;
        lbl_numeroDiaLaboralRestantes2.Text = lbl_numeroDiaLaboralRestantes.Text;
        lbl_porcentajeExpectativaDía2.Text = lbl_porcentajeExpectativaDía.Text;
        lbl_porcentajeMensual2.Text = lbl_porcentajeMensual.Text;
        lbl_porcentajeExpectativaMensual2.Text = lbl_porcentajeExpectativaMensual.Text;
        content_datos_reporte_porcentajes.Visible = true;
        content_datos_reporte.Visible = false;
    }



    protected void Timer1_Tick(object sender, EventArgs e) {
        DateTime fechaTermino = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 25, 0);
        int diferencia = DateTime.Compare(utilidad_fechas.obtenerCentral(), fechaTermino);
        lbl_fechas_timer.Text = utilidad_fechas.obtenerCentral().ToString("MM/dd/yyyy H:mm") + " • " + fechaTermino.ToString("MM/dd/yyyy H:mm") + " • "+diferencia.ToString();
        


        if (diferencia > 0) {
            content_datos_reporte.Visible = false;
            content_msg_apagado.Visible = true;
            UpdatePanel2.Update();
        }

    }
}