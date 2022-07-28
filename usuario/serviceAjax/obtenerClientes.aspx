<%@ Page Language="C#" %>
<%@ Import Namespace="Newtonsoft.Json" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e) {
        obtenerClientes();
        }
    protected void obtenerClientes() {

        bool login = HttpContext.Current.User.Identity.IsAuthenticated;

        usuarios usuarioLogin = (usuarios)HttpContext.Current.Session["datosUsuario"];
        

      
       if (login && usuarioLogin.tipo_de_usuario == "usuario") {
            if (Request.QueryString["data"] != null) {

             Response.Write("{ \"results\": "+textTools.DataTableToJSON(clientes.obtenerClientesMin(usuarioLogin,(Request.QueryString["data"].ToString())))+"}");
                } else { }


            } 
        }

</script>

