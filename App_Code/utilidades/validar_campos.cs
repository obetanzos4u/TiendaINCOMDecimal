using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

/// <summary>
/// Grupo de utilidades para validar campos de formulario
/// </summary>
public class validarCampos
{
    /// <summary>
    /// Valida y recibe: Nombre, Apellidos (Paterno, Materno)
    /// </summary>
    /// 
    static public bool nombres(TextBox txt_nombre, TextBox txt_apellido_paterno, TextBox txt_apellido_materno, Control t)
    {
        Regex re = new Regex(@"\b([a-zA-ZÀ-ÿ][-,a-zA-Z. ']+[ ]*)+");
        if (txt_nombre.Text.Length == 0)
        {
            materializeCSS.crear_toast(t, "El campo nombre es obligatorio.", false);
            return false;
        }
        else
        {
            if (!re.IsMatch(txt_nombre.Text))
            {
                materializeCSS.crear_toast(t, "Nombre no válido.", false);
                return false;
            }
        }
        if (txt_apellido_paterno.Text.Length == 0)
        {
            materializeCSS.crear_toast(t, "El campo apellido paterno es obligatorio.", false);
            return false;
        }
        else
        {
            if (!re.IsMatch(txt_apellido_paterno.Text))
            {
                materializeCSS.crear_toast(t, "Apellido paterno no válido.", false);
                return false;
            }
        }
        if (txt_apellido_materno.Text.Length == 0)
        {
            materializeCSS.crear_toast(t, "El campo apellido materno es obligatorio.", false);
            return false;
        }
        else
        {
            if (!re.IsMatch(txt_apellido_materno.Text))
            {
                materializeCSS.crear_toast(t, "Apellido materno no válido.", false);
                return false;
            }
        }
        return true;
    }
    static public bool nombres(TextBox txt_nombre, TextBox txt_apellido_paterno, Control t)
    {
        Regex re = new Regex(@"\b([a-zA-ZÀ-ÿ][-,a-zA-Z. ']+[ ]*)+");
        if (txt_nombre.Text.Length == 0)
        {
            materializeCSS.crear_toast(t, "El campo nombre es obligatorio.", false);
            return false;
        }
        else
        {
            if (!re.IsMatch(txt_nombre.Text))
            {
                materializeCSS.crear_toast(t, "Nombre no válido.", false);
                return false;
            }
        }
        if (txt_apellido_paterno.Text.Length == 0)
        {
            materializeCSS.crear_toast(t, "El campo apellidos es obligatorio.", false);
            return false;
        }
        else
        {
            if (!re.IsMatch(txt_apellido_paterno.Text))
            {
                materializeCSS.crear_toast(t, "Apellidos no válidos.", false);
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Valida longitudes permitidas para email así como su formato
    /// </summary>
    static public bool email(string email, Control t)
    {
        Regex re = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        email = textTools.lineSimple(email);
        if (!re.IsMatch(email))
        {
            materializeCSS.crear_toast(t, "El correo electrónico no es válido.", false);
            return false;
        }
        return true;
    }
    /// <summary>
    /// Valida la longitud de una contraseña
    /// </summary>
    static public bool passsword(TextBox txt_password, Control t)
    {
        Regex re = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$");
        if (re.IsMatch(txt_password.Text))
        {
            return true;
        }
        materializeCSS.crear_toast(t, "La contraseña no cumple los criterios de seguridad.", false);
        return false;
    }
    /// <summary>
    /// Valida la longitud de la contraseña y verificación, así como su coincidencia.
    /// </summary>
    static public bool passsword(TextBox txt_password, TextBox txt_password_confirma, Control t)
    {
        Regex re = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$");
        if (object.Equals(txt_password.Text, txt_password_confirma.Text))
        {
            if (re.IsMatch(txt_password.Text) && re.IsMatch(txt_password_confirma.Text))
            {
                return true;
            }
            materializeCSS.crear_toast(t, "La contraseña no cumple los criterios de seguridad.", false);
        }
        materializeCSS.crear_toast(t, "Las contraseñas son diferentes.", false);
        return false;
    }

    /// <summary>
    /// Valida y recibe: Telefono y Celular (Máx 50)
    /// </summary>
    /// 
    static public bool telefonos(TextBox telefono, Control t)
    {
        Regex re = new Regex(@"\+?\d{2}[-\s]?\(?\d\)?[-\s]?\d{2}?[-\s]?\d{2,4}[-\s]?\d{2,4}");
        if (!re.IsMatch(telefono.Text.Trim()) && telefono.Text.Length != 0)
        {
            materializeCSS.crear_toast(t, "El número de teléfono no es válido.", false);
            return false;
        }
        return true;
    }
    static public bool telefonos(TextBox telefono, TextBox celular, Control t)
    {
        Regex re = new Regex(@"\+?\d{2}[-\s]?\(?\d\)?[-\s]?\d{2}?[-\s]?\d{2,4}[-\s]?\d{2,4}");
        if (celular.Text.Length == 0)
        {
            materializeCSS.crear_toast(t, "El número de celular es obligatorio.", false);
            return false;
        }
        else
        {
            if (!re.IsMatch(celular.Text.Trim()))
            {
                materializeCSS.crear_toast(t, "El número de celular no es válido.", false);
                return false;
            }
        }
        if (telefono.Text.Length != 0)
        {
            if (!re.IsMatch(telefono.Text.Trim()))
            {
                materializeCSS.crear_toast(t, "El número de teléfono no es válido.", false);
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Valida la dirección de envío de acuerdo a los longitudes y formatos necesarios.
    /// </summary>
    [Obsolete("Este método solo funciona con Materialize CSS, usar la función [direccionDeEnvio] para obtener una respuesta genérica de la clase [json_respuestas] ")]
    static public bool direccionEnvio(direccionesEnvio direccion, Control t)
    {
        if (string.IsNullOrWhiteSpace(direccion.nombre_direccion) || direccion.nombre_direccion.Length > 20) { materializeCSS.crear_toast(t, "El campo nombre de dirección no cumple con los requerimientos.", false); return false; };
        if (direccion.calle.Length == 0 || direccion.calle.Length > 60) { materializeCSS.crear_toast(t, "El campo calle no cumple con los requerimientos.", false); return false; };
        if (direccion.numero.Length == 0 || direccion.numero.Length > 30) { materializeCSS.crear_toast(t, "El campo número no cumple con los requerimientos.", false); return false; };
        if (direccion.colonia.Length > 70) { materializeCSS.crear_toast(t, "El campo colonia no cumple con los requerimientos.", false); return false; };
        if (direccion.delegacion_municipio.Length > 60) { materializeCSS.crear_toast(t, "El campo delegación excede la cantidad de caracteres.", false); return false; };
        if (direccion.estado.Length > 35) { materializeCSS.crear_toast(t, "El campo estado excede la cantidad de caracteres.", false); return false; };
        if (direccion.codigo_postal.Length < 2 || direccion.codigo_postal.Length > 15) { materializeCSS.crear_toast(t, "El campo código postal es obligatorio y debe ser numérico", false); return false; };
        if (direccion.pais == "") { materializeCSS.crear_toast(t, "El campo pais es requerido.", false); return false; };

        if (direccion.referencias.Length > 100) { materializeCSS.crear_toast(t, "El campo pais excede con los requerimientos de longitud.", false); return false; };

        return true;
    }

    /// <summary>
    /// 20200804 - Valida la dirección de envío de acuerdo a los longitudes y formatos necesarios.
    /// </summary>
    /// 
    static public json_respuestas direccionDeEnvio(direccionesEnvio direccion)
    {
        json_respuestas respuesta = new json_respuestas(false, "Error");

        if (string.IsNullOrWhiteSpace(direccion.nombre_direccion) || direccion.nombre_direccion.Length > 20) { respuesta.message = "El campo \"nombre de dirección\" no cumple con los requerimientos."; return respuesta; };
        if (direccion.calle.Length == 0 || direccion.calle.Length > 60) { respuesta.message = "El campo \"calle\" no cumple con los requerimientos."; return respuesta; };
        if (direccion.numero.Length == 0 || direccion.numero.Length > 30) { respuesta.message = "El campo \"número\" no cumple con los requerimientos."; return respuesta; };
        if (direccion.colonia.Length > 70) { respuesta.message = "El campo \"colonia\" no cumple con los requerimientos."; return respuesta; };
        if (direccion.delegacion_municipio.Length > 60) { respuesta.message = "El campo \"delegación\" excede la cantidad de caracteres."; return respuesta; };
        if (direccion.estado.Length > 35) { respuesta.message = "El campo \"estado\" excede la cantidad de caracteres."; return respuesta; };
        if (direccion.codigo_postal.Length < 2 || direccion.codigo_postal.Length > 15) { respuesta.message = "El campo \"código postal\" es obligatorio y debe ser numérico"; return respuesta; };
        if (string.IsNullOrWhiteSpace(direccion.pais)) { respuesta.message = "El campo \"pais\" es requerido."; return respuesta; };

        if (direccion.referencias.Length > 100) { respuesta.message = "El campo \"pais\" excede con los requerimientos de longitud."; return respuesta; };

        respuesta.result = true;
        return respuesta;
    }
    /// <summary>
    /// Valida  que la dirección de envío tenga los campos completos necesarios y requeridos Item1[bool]: True si la dirección es válida. Item2[string]: Los campos faltantes
    /// </summary>
    /// 
    static public Tuple<bool, string> direccionEnvioCompleta(model_direccionesEnvio direccion)
    {

        string camposFaltantes = string.Empty;
        bool resultado = true;
        if (direccion == null) return Tuple.Create<bool, string>(false, "No hay ingresado una dirección de envío");


        if (direccion.calle == null || direccion.calle.Length < 2) { resultado = false; camposFaltantes += "calle, "; };
        if (direccion.numero == null || direccion.numero.Length < 1) { resultado = false; camposFaltantes += "número, "; };
        if (direccion.colonia == null || direccion.colonia.Length < 2) { resultado = false; camposFaltantes += "colonia, "; };
        if (direccion.delegacion_municipio == null || direccion.delegacion_municipio.Length < 2) { resultado = false; camposFaltantes += "delegación/municipio, "; };
        if (direccion.estado == null || direccion.estado.Length < 2) { resultado = false; camposFaltantes += "estado, "; };
        if (direccion.codigo_postal == null || direccion.codigo_postal.Length < 2 || direccion.codigo_postal.Length > 15) { resultado = false; camposFaltantes += "código postal, "; };
        if (direccion.pais == null || direccion.pais.Length < 2) { resultado = false; camposFaltantes += "país, "; };



        Tuple<bool, string> resultadoFinal = Tuple.Create<bool, string>(resultado, camposFaltantes);
        return resultadoFinal;
    }

    static public bool direccionFacturacion(direccionesFacturacion direccion, Control t)
    {
        if (direccion.nombre_direccion == "" || direccion.nombre_direccion.Length > 20) { materializeCSS.crear_toast(t, "El campo nombre de dirección no cumple con los requerimientos.", false); return false; };

        if (direccion.razon_social == "" || direccion.nombre_direccion.Length > 150) { materializeCSS.crear_toast(t, "El campo razón social no cumple con los requerimientos.", false); return false; };
        if (direccion.rfc == "" || direccion.rfc.Length > 13) { materializeCSS.crear_toast(t, "El campo RFC no cumple con los requerimientos.", false); return false; };

        if (direccion.calle.Length == 0 || direccion.calle.Length > 50) { materializeCSS.crear_toast(t, "El campo calle no cumple con los requerimientos.", false); return false; };
        if (direccion.numero.Length == 0 || direccion.numero.Length > 30) { materializeCSS.crear_toast(t, "El campo número no cumple con los requerimientos.", false); return false; };
        if (direccion.colonia.Length > 60) { materializeCSS.crear_toast(t, "El campo colonia no cumple con los requerimientos.", false); return false; };
        if (direccion.delegacion_municipio.Length > 60) { materializeCSS.crear_toast(t, "El campo delegación/municipio excede la cantidad de caracteres.", false); return false; };
        if (direccion.estado.Length > 35) { materializeCSS.crear_toast(t, "El campo estado excede la cantidad de caracteres.", false); return false; };
        if (direccion.codigo_postal.Length < 2 || direccion.codigo_postal.Length > 15) { materializeCSS.crear_toast(t, "El campo código postal es obligatorio y debe ser numérico", false); return false; };
        if (direccion.pais == "") { materializeCSS.crear_toast(t, "El campo pais es requerido.", false); return false; };



        return true;
    }

    static public bool cotizacion_nombreCotizacion(string nombreCotizacion, Control t)
    {
        if (nombreCotizacion == "" || nombreCotizacion.Length == 0) { materializeCSS.crear_toast(t, "El nombre esta vacío, debes ingresar un nombre.", false); return false; };
        if (nombreCotizacion.Length > 60) { materializeCSS.crear_toast(t, "El nombre excede el límite de 40 caracteres", false); return false; };
        return true;

    }
}