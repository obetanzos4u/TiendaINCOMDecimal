using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class menuPrincipal : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        CrearMenuMovilCategorias();
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            LoginStatus2.Visible = false;
            usuarios usuarioLogin = (usuarios)HttpContext.Current.Session["datosUsuario"];
            usuarios userActivo = usuarios.modoAsesor();
            HyperLink link_miCuenta = (HyperLink)LoginView1.FindControl("miCuenta");
            HtmlGenericControl miCuenta = (HtmlGenericControl)LoginView1.FindControl("miCuentaNombre");
            //Image userImage = (Image)LoginView1.FindControl("profile_photo");
            Image userImage = new Image();
            userImage.Attributes.Add("class", "profile_photo");
            userImage.ImageUrl = $"https://ui-avatars.com/api/?name={userActivo.nombre}+{userActivo.apellido_paterno}&background=000&color=fff&rounded=true&format=svg";
            miCuenta.InnerText = "Hola " + userActivo.nombre.ToLowerInvariant();
            link_miCuenta.ToolTip = "Hola " + userActivo.nombre.ToUpper();
            link_miCuenta.Controls.Add(userImage);
            if (usuarioLogin.tipo_de_usuario == "usuario") barraAsesores.Visible = true;
            int cantidadProductos = carrito.obtenerCantidadProductos(userActivo.email);
            if (cantidadProductos > 0)
            {
                lbl_cantidadProductosCarrito.Visible = true;
                lbl_cantidadProductosCarrito.InnerText = cantidadProductos.ToString();
            }
            //lbl_cantidadProductosCarrito.InnerText = carrito.cantidadProductos;
        }
        //}
    }
    protected void CrearMenuMovilCategorias()
    {
        var categorias = CategoriasEF.obtenerNivel_1();
        usuarios datosUsuario = usuarios.modoAsesor();
        string[] usuario_rol_categorias = datosUsuario.rol_categorias;

        if (categorias == null) return;
        foreach (var cat in categorias)
        {
            var roles = cat.rol_categoria.Replace(" ", "").Split(',');
            if (privacidad.validarCategoria(roles, usuario_rol_categorias))
            {
                var url = GetRouteUrl("categorias", new System.Web.Routing.RouteValueDictionary {
                    { "identificador", cat.identificador }, { "nombre", menusCategorias.limpiarURL(cat.nombre) } }); ;
                menu_movil_categorias.InnerHtml += $"<li><a href='{url}'>{cat.nombre}</a></li>";
            }
        }
    }

    protected void LoginStatus1_LoggedOut(Object sender, System.EventArgs e)
    {
        string script = @"  var auth2 = gapi.auth2.getAuthInstance();
                                        auth2.signOut().then(function () {
                                          console.log('User signed out.');
                                        });";
        ScriptManager.RegisterStartupScript(this, typeof(Control), "LoginOutGoogle", script, true);
        Session.Clear();
        Session.Abandon();
        Session.RemoveAll();
        FormsAuthentication.SignOut();
        FormsAuthentication.RedirectToLoginPage();
    }
}