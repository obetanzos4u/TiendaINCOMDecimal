using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;

public partial class aviso_de_privacidad : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            Page.Title = "Aviso de privacidad Incom";
            Page.MetaDescription = "Aviso de privacidad";
        }



        DataTable tabla = new DataTable();
        tabla.Columns.AddRange(new DataColumn[3]
            {
            new DataColumn("Destinatario de los datos personales", typeof(string)),
            new DataColumn("Finalidad", typeof(string)),
            new DataColumn("Requiere del consentimiento", typeof(string)) });

        tabla.Rows.Add("SHCP ", "Cumplimiento de información solicitada.", "No");
        tabla.Rows.Add("Autoridades federales y locales", "Cumplimiento de información solicitada", "No");
        tabla.Rows.Add("IMSS e INFONAVIT", "Revisión de cargas sociales.", "No");
        tabla.Rows.Add("INFONACOT", "Compulsas de préstamos", "No");
        tabla.Rows.Add("Empresas del grupo", "Servicio y atención del negocio", "Si");
        tabla.Rows.Add("Instituciones bancarias", "Referencias bancarias", "Si");
        tabla.Rows.Add("Despachos de auditoría", "Referencias de negocio", "Si");


        grid_datos.DataSource = tabla;
        grid_datos.DataBind();





        DataTable tabla_2 = new DataTable();
        tabla_2.Columns.AddRange(new DataColumn[3]
        {
            new DataColumn("Nombre del listado", typeof(string)),
            new DataColumn("Finalidad para las que aplica",typeof(string)),
            new DataColumn("Medio para obtener mayor información",typeof(string))

        });

        tabla_2.Rows.Add("Empleados", "Dar referencias laborales; Para una posible recontratación", "avisodeprivacidad@incom.mx");
        tabla_2.Rows.Add("Clientes", "Envío de la gaceta, promociones y/o información relevante de la empresa de forma electrónica o impresa;Evaluar la calidad del servicio que le brinda", "avisodeprivacidad@incom.mx");
        tabla_2.Rows.Add("Proveedores", "Envío de la gaceta, promociones y/o información relevante de la empresa de forma electrónica o impresa", "avisodeprivacidad@incom.mx");

        grid_limitar_uso.DataSource = tabla_2;
        grid_limitar_uso.DataBind();

    }
    }
