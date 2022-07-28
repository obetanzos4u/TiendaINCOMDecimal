using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class userControls_uc_productos_comentarios : System.Web.UI.UserControl
{

    public string numero_parte {
        get { return this.hf_numero_parte.Value; }
        set { this.hf_numero_parte.Value = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                txt_calificacion.Enabled = false;
                txt_calificacion.Visible = false;
                txt_comentario.Enabled = false;
                btn_agregar_comentario.Enabled = false;
             //   myRating.Attributes.Add("class", "hide");
            }
            else
            {
                txt_calificacion.Visible = true;
            }
            
            if (Page.RouteData.Values["numero_parte"] != null)
            {
                hf_numero_parte.Value = Page.RouteData.Values["numero_parte"].ToString();
                cargarComentarios();
            }
            else
            {
                Content_ProductosComentarios.Visible = false;
            }

        }
    }

    protected async void btn_agregar_comentario_Click(object sender, EventArgs e)
    {
        productos_comentarios comentario = new productos_comentarios();

        try
        {
            if (string.IsNullOrWhiteSpace(txt_calificacion.Text)) comentario.calificación = null;
            else comentario.calificación = byte.Parse(txt_calificacion.Text);

        }
        catch (Exception ex)
        {
            materializeCSS.crear_toast(up_comentarios, "El formato no es correcto", false);

        }
        try
        {
            comentario.comentario = txt_comentario.Text;
            comentario.idUsuario = usuarios.userLogin().id;
            comentario.fechaComentario = utilidad_fechas.obtenerCentral();
            comentario.numero_parte = hf_numero_parte.Value;
            comentario.visible = true;

            if (validarComentario(comentario))
            {
                productosComentarios guardar = new productosComentarios();
                await Task.Run(async () => await guardar.guardarComentario(comentario) );  

                materializeCSS.crear_toast(up_comentarios, guardar.msg_resultado, guardar.resultado);
                if(guardar.resultado == true)
                {
                    txt_calificacion.Text = "";
                    txt_comentario.Text = "";
                }
                cargarComentarios();
            }
        }

        catch (Exception ex)
        {
            Exception t = ex;
        }
    }
    

    protected void cargarComentarios()
    {

        lv_comentarios.DataSource = productosComentarios.obtenerComentarios(hf_numero_parte.Value);
        lv_comentarios.DataBind();
    }

    protected bool validarComentario(productos_comentarios c)
    {
        if(c.calificación < 1 || c.calificación > 5) { materializeCSS.crear_toast(up_comentarios, "El rango de calificación no es válido", false); return false; }
        if (c.comentario.Length < 10 ) { materializeCSS.crear_toast(up_comentarios, "El comentario debe ser de al menos 10 caracteres", false); return false; }
        if (c.comentario.Length < 10 || c.comentario.Length > 850) { materializeCSS.crear_toast(up_comentarios, "El comentario excede el límite de 850 caracteres", false); return false; }


        return true;
    }

    protected void lv_comentarios_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HtmlGenericControl Link_calificación = (HtmlGenericControl)e.Item.FindControl("calificación");
 

        dynamic  comentario =  e.Item.DataItem;
        try
        {

            int? cal = comentario.GetType().GetProperty("calificación").GetValue(comentario, null);

            if(cal != null)
            {
                for (int i = 0; i < cal; i++)
                {
                    Link_calificación.InnerHtml += "<i class='material-icons'>grade</i>";
                }

            }

         
        }
        catch (Exception ex)
        {
            Exception t = ex;
        }


    }
}