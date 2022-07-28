using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;

public class MKT_EventosAsistentes {
public string Evento { get; set; }
    public string Titulo { get; set; }
    public string Nombre { get; set; }
    public string Apellidos { get; set; }
    public string Compañia { get; set; }
    public string Email { get; set; }
    public string Celular { get; set; }
    public string TelOficina { get; set; }
    public string Comentarios { get; set; }
    public string Intereses { get; set; }
    public DateTime FechaRegistro { get; set; }
}
public partial class mkt_circuito_soplado_de_fibra_index : System.Web.UI.Page {


     private string connection = System.Configuration.ConfigurationManager.ConnectionStrings["mkt_eventos"].ToString();
    protected void Page_Load(object sender, EventArgs e) {


    }
    // Devuelve true si existe el email, false si no existe o ocurre un error
    protected bool ValidarExistencia(string email) {

        try {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(connection);
            cmd.Connection = con;

            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SELECT COUNT(*) FROM EventosAsistentes WHERE Email = @Email AND Evento = @Evento ");


                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;


               

                    cmd.Parameters.Add("@Evento", SqlDbType.NVarChar, 150);
                cmd.Parameters["@Evento"].Value = "Circuito de prueba para soplado";

                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
                cmd.Parameters["@Email"].Value = email;

                con.Open();

                int id = int.Parse(cmd.ExecuteScalar().ToString());

                if (id >= 1) return true;
                else  return false;
               


            }
        }
        catch (Exception ex) {

           
            return false;
        }

    }
    protected void Registro( MKT_EventosAsistentes asistente) {
        try {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(connection);
            cmd.Connection = con;

            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("INSERT INTO EventosAsistentes  ");
                query.Append(" (Evento, Titulo, Nombre, Apellidos, Compañia, Email, Celular, TelOficina, Comentarios, Intereses, FechaRegistro) ");
                query.Append(" VALUES (@Evento, @Titulo, @Nombre, @Apellidos, @Compañia, @Email, @Celular, @TelOficina, @Comentarios, @Intereses, @FechaRegistro);  ");
                query.Append("SELECT SCOPE_IDENTITY(); "); 

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;


                cmd.Parameters.Add("@Evento", SqlDbType.NVarChar, 150);
                cmd.Parameters["@Evento"].Value = asistente.Evento;


                cmd.Parameters.Add("@Titulo", SqlDbType.NVarChar, 20);
                cmd.Parameters["@Titulo"].Value = asistente.Titulo;


                cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 35);
                cmd.Parameters["@Nombre"].Value = asistente.Nombre;


                cmd.Parameters.Add("@Apellidos", SqlDbType.NVarChar, 35);
                cmd.Parameters["@Apellidos"].Value = asistente.Apellidos;


                cmd.Parameters.Add("@Compañia", SqlDbType.NVarChar, 150);
                cmd.Parameters["@Compañia"].Value = asistente.Compañia;


                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
                cmd.Parameters["@Email"].Value = asistente.Email;

                cmd.Parameters.Add("@Celular", SqlDbType.NVarChar, 50);
                cmd.Parameters["@Celular"].Value = asistente.Celular;

                cmd.Parameters.Add("@TelOficina", SqlDbType.NVarChar, 50);
                cmd.Parameters["@TelOficina"].Value = asistente.TelOficina;

                cmd.Parameters.Add("@Comentarios", SqlDbType.NVarChar, 500);
                cmd.Parameters["@Comentarios"].Value = asistente.Comentarios;

                cmd.Parameters.Add("@Intereses", SqlDbType.NVarChar, 2000);
                cmd.Parameters["@Intereses"].Value = asistente.Intereses;

                cmd.Parameters.Add("@FechaRegistro", SqlDbType.DateTime);
                cmd.Parameters["@FechaRegistro"].Value = asistente.FechaRegistro;
                con.Open();

                string id = cmd.ExecuteScalar().ToString();
                content_msg_resultado.Visible = true;

                btn_registrar.Enabled = false;
                EnvialEmail(id, asistente.Nombre, asistente.Apellidos, asistente.Email);
                msg_resultado.Text = "Registro exitoso, en unos momentos recibirás un email con tu credencial de acceso.";


            }
        }
        catch (Exception ex) {

            content_msg_resultado.Visible = true;
            msg_resultado.Text = "----";
        }
    }

    protected void EnvialEmail(string id, string Nombre, string Apellidos, string Email) {

        string asunto = Nombre+ ", confirmación para el evento de circuito de prueba para soplado de fibra óptica de Incom";
        string filePathHTML = "/mkt/landing/circuito-soplado-de-fibra/email.html";



        string dominio = Request.Url.GetLeftPart(UriPartial.Authority);

        Dictionary<string, string> datosDiccRemplazo = new Dictionary<string, string>();
        datosDiccRemplazo.Add("{dominio}", dominio);
        
        datosDiccRemplazo.Add("{Nombre}", Nombre);
        datosDiccRemplazo.Add("{Apellidos}", Apellidos);
        datosDiccRemplazo.Add("{LinkCredencial}", dominio+ $"/mkt/landing/circuito-soplado-de-fibra/credencial.aspx?id={id}&email={Email}");
        datosDiccRemplazo.Add("{LinkPrograma}", dominio + $"/mkt/landing/circuito-soplado-de-fibra/PROGRAMA_INCOM.pdf");
        

        string mensaje = archivosManejador.reemplazarEnArchivo(filePathHTML, datosDiccRemplazo);




        emailTienda email = new emailTienda(asunto, Email+ ",tpavia@incom.mx,egarza@incom.mx,publicidad@incom.mx", mensaje, "retail@incom.mx");
        email.general();
    }
    protected void btn_registrar_Click(object sender, EventArgs e) {

        TextInfo myTI = new CultureInfo("es-MX", false).TextInfo;
        string nombre = myTI.ToTitleCase(txt_nombre.Text);
        string apellidos = myTI.ToTitleCase(txt_apellidos.Text);
        string compañia = txt_compañia.Text;
        string email = txt_email.Text;
        string telefono_movil = txt_telefono_movil.Text;
        string telefono_oficina = txt_telefono_oficina.Text;
        string comentarios = txt_comentarios.Text;
        
        bool form_validado = false;

        if (string.IsNullOrWhiteSpace(nombre)) {
            content_msg_resultado.Visible = true;
            msg_resultado.Text = "Debes ingresar un nombre";
            return;
        }


        if (string.IsNullOrWhiteSpace(compañia)) {
            content_msg_resultado.Visible = true;
            msg_resultado.Text = "Debes ingresar una compañia";
            return;
        }

        if (string.IsNullOrWhiteSpace(email)) {
            content_msg_resultado.Visible = true;
            msg_resultado.Text = "Debes ingresar un email";
            return;
        }

        if (string.IsNullOrWhiteSpace(telefono_movil)) {
            content_msg_resultado.Visible = true;
            msg_resultado.Text = "Debes ingresar un teléfono móvil";
            return;
        }


        if (ValidarExistencia(email)) {

            content_msg_resultado.Visible = true;
            msg_resultado.Text = "Ya existe un registro con tu email";
            btn_registrar.Enabled = false;
            return;
        }

        MKT_EventosAsistentes asistente = new MKT_EventosAsistentes();

        asistente.Nombre = textTools.lineSimple( nombre);
        asistente.Apellidos = textTools.lineSimple(apellidos);
        asistente.Compañia = textTools.lineSimple(textTools.lineSimple(compañia));
        asistente.Titulo = "";
        asistente.Intereses = "";
        asistente.Email = textTools.lineSimple(email);
        asistente.TelOficina = telefono_oficina;
        asistente.Celular = telefono_movil;
        asistente.Comentarios = comentarios + " ~ Invitado por: " + ddl_invitado_por.SelectedValue;
        asistente.Evento = "Circuito de prueba para soplado";
        asistente.FechaRegistro =  utilidad_fechas.obtenerCentral();
        Registro(asistente);
    }

    protected void GeneratePDF(object sender, System.EventArgs e) {
      
    }
}