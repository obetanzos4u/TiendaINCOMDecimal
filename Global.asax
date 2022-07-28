<%@ Application Language="C#" CodeBehind="Global.asax.cs" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="System.Web.Http" %>
<%@ Import Namespace="System.Security.Principal" %> 
<script runat="server">
    protected void Application_AuthenticateRequest(Object sender, 
                                               EventArgs e) {

  // Get the authentication cookie
  string cookieName  = FormsAuthentication.FormsCookieName;
  HttpCookie authCookie  = Context.Request.Cookies[cookieName];
  
  // If the cookie can't be found, don't issue the ticket
  if(authCookie == null) return;

  // Get the authentication ticket and rebuild the principal 
  // & identity
  FormsAuthenticationTicket authTicket  = 
    FormsAuthentication.Decrypt(authCookie.Value);
  string[] roles = authTicket.UserData.Split(new Char [] {','});


  GenericIdentity userIdentity = 
    new GenericIdentity(authTicket.Name);
  GenericPrincipal userPrincipal = 
    new GenericPrincipal(userIdentity, roles);
  Context.User = userPrincipal;
}

    void Application_Start(object sender, EventArgs e)
    {
        // Código que se ejecuta al iniciarse la aplicación
        DefinirRutas();
        



                   GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/plain"));
           GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream"));
         GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/plain"));


        // 20200619 CM -  WebAPI
            RouteTable.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            defaults: new { id = System.Web.Http.RouteParameter.Optional }
                );
         // 20200619 CM - WebAPI remueve las respuestas XML por JSON
        GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();



                //Evito las referencias circulares al trabajar con Entity FrameWork         
GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            
//Elimino que el sistema devuelva en XML, sólo trabajaremos con JSON
GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);



        }

    void Application_End(object sender, EventArgs e)
    {
        //  Código que se ejecuta al cerrarse la aplicación

        }

    void Application_Error(object sender, EventArgs e)
    {
        // Código que se ejecuta cuando se produce un error sin procesar
        Exception ex = Server.GetLastError();
      //   devNotificaciones.ErrorSQL("Obtener Productos visitados", ex,"");

        }

    void Session_Start(object sender, EventArgs e) {
        // Código que se ejecuta al iniciarse una nueva sesión
        //  usuarios session = new usuarios();
        //    sessionestablecer_DatosUsuario(HttpContext.Current.User.Identity.Name);
        }

    void Session_End(object sender, EventArgs e)
    {
        // Código que se ejecuta cuando finaliza una sesión. 
        // Nota: el evento Session_End se produce solamente con el modo sessionstate
        // se establece como InProc en el archivo Web.config. Si el modo de sesión se establece como StateServer
        // o SQLServer, el evento no se produce.

        }

    void DefinirRutas()
    {
        // Categorias
        RouteTable.Routes.MapPageRoute("categoriasTodas", "productos/", "~/categoriasTodas.aspx");
        RouteTable.Routes.MapPageRoute("categorias", "productos/{nombre}-{identificador}", "~/categoria.aspx");
        RouteTable.Routes.MapPageRoute("categoriasL2", "productos/{l1}/{nombre}-{identificador}", "~/categoria.aspx");
        RouteTable.Routes.MapPageRoute("categoriasL3", "productos/{l1}/{l2}/{nombre}-{identificador}", "~/categoria.aspx");


        // Productos
        RouteTable.Routes.MapPageRoute("productos", "producto/{productoNombre}-MARCA-{marca}-{numero_parte}", "~/visualizarProducto.aspx");

        RouteTable.Routes.MapPageRoute("busqueda", "productos/buscar", "~/busqueda.aspx");
        RouteTable.Routes.MapPageRoute("productos-old", "{producto-nombre}-MARCA-{marca}-{numero_parte}", "~/visualizarProducto.aspx");

        // Cliente 
        RouteTable.Routes.MapPageRoute("usuario-cotizaciones", "usuario/mi-cuenta/cotizaciones/", "~/usuario/mi-cuenta/cotizaciones.aspx");
        RouteTable.Routes.MapPageRoute("usuario-cotizacion-datos", "usuario/mi-cuenta/cotizaciones/datos/{id_operacion}", "~/usuario/mi-cuenta/cotizacion-datos.aspx");
        RouteTable.Routes.MapPageRoute("usuario-cotizacion-productos", "usuario/mi-cuenta/cotizaciones/productos/{id_operacion}", "~/usuario/mi-cuenta/cotizacion-productos.aspx");
        RouteTable.Routes.MapPageRoute("usuario-cotizacion-visualizar", "usuario/mi-cuenta/cotizaciones/visualizar/{id_operacion}", "~/usuario/mi-cuenta/cotizacion-visualizar.aspx");

        RouteTable.Routes.MapPageRoute("usuario-pedidos", "usuario/mi-cuenta/pedidos/", "~/usuario/mi-cuenta/pedidos.aspx");
        RouteTable.Routes.MapPageRoute("usuario-pedido-datos", "usuario/mi-cuenta/pedidos/datos/{id_operacion}", "~/usuario/mi-cuenta/pedido-datos.aspx");
        RouteTable.Routes.MapPageRoute("usuario-pedido-productos", "usuario/mi-cuenta/pedidos/productos/{id_operacion}", "~/usuario/mi-cuenta/pedido-productos.aspx");
        RouteTable.Routes.MapPageRoute("usuario-pedido-visualizar", "usuario/mi-cuenta/pedidos/visualizar/{id_operacion}", "~/usuario/mi-cuenta/pedido-visualizar.aspx");
        
        RouteTable.Routes.MapPageRoute("usuario-pedido-pago-paypal", "usuario/mi-cuenta/pedidos/pagos/{id_operacion}", "~/usuario/mi-cuenta/pedido-pago.aspx");
        RouteTable.Routes.MapPageRoute("usuario-pedido-pago-santander", "usuario/mi-cuenta/pedidos/pagos/3DSecure/{id_operacion}", "~/usuario/mi-cuenta/pedido-pago-santander.aspx");

        // Enseñanza

        RouteTable.Routes.MapPageRoute("glosario", "glosario/{letra}", "~/enseñanza/glosario.aspx");
        RouteTable.Routes.MapPageRoute("infografias", "enseñanza/infografías", "~/enseñanza/infografías.aspx");
        RouteTable.Routes.MapPageRoute("infografia", "infografía/{titulo}/{id}", "~/enseñanza/infografía.aspx");

        //Pedidos Cliente Update 2021
         RouteTable.Routes.MapPageRoute("cliente-pedido-datos", "usuario/cliente/mi-cuenta/pedidos/datos/{id_operacion}", "~/usuario/cliente/pedido-datos.aspx");
        RouteTable.Routes.MapPageRoute("cliente-pedido-envio", "usuario/cliente/mi-cuenta/pedidos/envio/{id_operacion}", "~/usuario/cliente/pedido-envio.aspx");
         RouteTable.Routes.MapPageRoute("cliente-pedido-facturacion", "usuario/cliente/mi-cuenta/pedidos/facturacion/{id_operacion}", "~/usuario/cliente/pedido-facturacion.aspx");

       RouteTable.Routes.MapPageRoute("cliente-pedido-pago", "usuario/cliente/mi-cuenta/pedidos/pago/{id_operacion}", "~/usuario/cliente/pedido-pago.aspx");
        RouteTable.Routes.MapPageRoute("cliente-pedido-pago-santander", "usuario/cliente/mi-cuenta/pedidos/pago/santander/{id_operacion}", "~/usuario/cliente/pago-santander.aspx");
     RouteTable.Routes.MapPageRoute("cliente-pedido-pago-paypal", "usuario/cliente/mi-cuenta/pedidos/pago/paypal/{id_operacion}", "~/usuario/cliente/pago-paypal.aspx");


     
        RouteTable.Routes.MapPageRoute("cliente-pedido-resumen", "usuario/cliente/mi-cuenta/pedidos/resumen/{id_operacion}", "~/usuario/cliente/resumen.aspx");


       RouteTable.Routes.MapPageRoute("cliente-editar-direccion-envio", "usuario/cliente/editar/envio/{id_direccion}", "~/usuario/cliente/editar-direccion-envio.aspx");
       RouteTable.Routes.MapPageRoute("cliente-editar-contacto", "usuario/cliente/editar/contacto/{id_contacto}", "~/usuario/cliente/editar-contacto.aspx");

       RouteTable.Routes.MapPageRoute("cliente-editar-direccion-facturacion", "usuario/cliente/editar/facturacion/{id_direccion}", "~/usuario/cliente/editar-direccion-facturacion.aspx");


        }
</script>
