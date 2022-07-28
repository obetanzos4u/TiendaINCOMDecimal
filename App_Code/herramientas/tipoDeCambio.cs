using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

/// <summary>
/// Obtiene el tipo de cambio
/// </summary>
public class tipoDeCambio {
    public tipoDeCambio() {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    /// <summary>
    /// Obtiene el tipo de cambio desde el DOF
    /// </summary>
    static public decimal? obtenerTipoDeCambio() {
        try {
            int _dia, _mes, _año;
            string dia, mes, año;

            _dia = DateTime.Now.Day;
            _mes = DateTime.Now.Month;
            _año = DateTime.Now.Year;

            if (_dia < 10) dia = "0" + _dia.ToString();
            else dia = _dia.ToString();

            if (_mes < 10) mes = "0" + _mes.ToString();
            else mes = _mes.ToString();

           
            string cliente = null;

            año = _año.ToString();

            string url = string.Format(@"http://dof.gob.mx/indicadores_detalle.php?cod_tipo_indicador=158&dfecha={0}%2F{1}%2F{02}&hfecha={0}%2F{1}%2F{2}", dia, mes, año );
          


            using (var client = new WebClient()) {
                client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko)");
 
 

                cliente = client.DownloadString(url);
            }
           

            HtmlDocument doc = new HtmlDocument();
            string html = cliente;
            doc.LoadHtml(html);
            int i = 0;
            decimal itc = 0;
            HtmlNodeCollection t = doc.DocumentNode.SelectNodes("//td[@class='txt']");

            foreach (HtmlNode td in doc.DocumentNode.SelectNodes("//td[@class='txt']"))
            {
               
                string text = td.InnerText;
                // do whatever with text
                if (i == 3) itc = decimal.Parse(text);
                i += 1;
            }

 
          
         
            return itc;

        }
        catch (Exception ex) {

            devNotificaciones.error("Obtener tipo de cambio", ex);
            return null;
        }
    }
}