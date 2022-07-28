using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Newtonsoft.Json;
using SelectPdf;
using System.Threading.Tasks;
/// <summary>
/// Ayuda a manipular y crear pdfs
/// </summary>
public class pdfTools {
    protected string mapPath { get; set; }
    protected  string header { get; set; }
    protected  string body { get; set; }
    protected  string footer { get; set; }

    public pdfTools(string _body, string _mapPath) { body= _body ; ; mapPath = _mapPath;  }
    public pdfTools(string _body,string _header, string _mapPath)  { header=_header; body = _body; mapPath = _mapPath;  }
    public pdfTools(string _header, string _body, string _footer, string _mapPath) {
        header = _header ;
        body = _body ;
        footer = _footer;
        mapPath = _mapPath; }
    /// <summary>
    /// Elimina espacios dobles y al inicio y final de una cadena de texto
    /// </summary>
    public byte[]  crearPdfBytes(bool ScaleImages) {



        string input = body;

        // string outputFile = Server.MapPath(Path.Combine("~/", @"temp/myimage.pdf"));

        string pdf_page_size = "A4";
        PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize),
            pdf_page_size, true);

        string pdf_orientation = "Portrait";
        PdfPageOrientation pdfOrientation =
            (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation),
            pdf_orientation, true);

        int webPageWidth = 1330;


        // instantiate a html to pdf converter object
        HtmlToPdf converter = new HtmlToPdf();
        converter.Options.ScaleImages = ScaleImages;


        // set converter options
        converter.Options.PdfPageSize = pageSize;
        converter.Options.PdfPageOrientation = pdfOrientation;
        converter.Options.WebPageWidth = webPageWidth;

        // Margenes
        converter.Options.MarginLeft = 10;
        converter.Options.MarginRight = 10;
        converter.Options.MarginTop = 20;
        converter.Options.MarginBottom = 20;


        if (header != null) {
            // header settings
            converter.Options.DisplayHeader = true;
            converter.Header.DisplayOnFirstPage = true;
            converter.Header.DisplayOnOddPages = true;
            converter.Header.DisplayOnEvenPages = true;
            converter.Header.Height = 70;
            // add some html content to the header
            PdfHtmlSection headerHtml = new PdfHtmlSection(header, mapPath);
            headerHtml.AutoFitHeight = HtmlToPdfPageFitMode.NoAdjustment;
            converter.Header.Add(headerHtml);

            }




        // footer settings
        converter.Options.DisplayFooter = true;
        converter.Footer.DisplayOnFirstPage = true;
        converter.Footer.DisplayOnOddPages = true;
        converter.Footer.DisplayOnEvenPages = true;
        converter.Footer.Height = 20;

        // page numbers can be added using a PdfTextSection object
        PdfTextSection text = new PdfTextSection(0, 10, "Página: {page_number} de {total_pages}  ", new System.Drawing.Font("Arial", 8));
        text.HorizontalAlign = PdfTextHorizontalAlign.Right;
        converter.Footer.Add(text);



        // var plainTextBytes = System.Text.Encoding.UTF8.GetBytes("your-username:your-password");

        //converter.Options.Authentication.Username = "your-username";
        //converter.Options.Authentication.Password = "your-password";
        //  converter.Options.HttpHeaders.Add("Authorization", System.Convert.ToBase64String(plainTextBytes));

        // create a new pdf document converting an url
        PdfDocument doc = converter.ConvertHtmlString(body, mapPath);

        // save pdf document
        //   doc.Save(Response, false,  "Cotizacion");
        //    doc.Save(outputFile);
        byte[] pdfBytes = doc.Save();


        // close pdf document
        doc.Close();

        return pdfBytes;
        }
    
    }