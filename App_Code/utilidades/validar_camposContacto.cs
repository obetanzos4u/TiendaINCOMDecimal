using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Grupo de utilidades para validar campos de formulario de contacto
/// </summary>
public class validar_camposContacto {
    public string mensaje { get; set; }
    public bool resultado { get; set; }
    /// <summary>
    /// Valida y recibe: Nombre, email, telefono, comentario
    /// </summary>
    public bool contactoSimple(string nombre, string email, string telefono, string comentario) {

        resultado = false;
        if (nombre.Length < 2 || nombre.Length > 45) { mensaje = "El campo nombre no cumple con los requerimientos de longitud."; return false; };
        if (email.Length > 60 || email.Length < 5  ) { mensaje = "El email supera la cantidad de carácteres permitidos"; return false; };
        if (!textTools.validarEmail(email)) { mensaje = "El email no tiene el formato correcto"; return false; };
        if (telefono.Length > 60) { mensaje = "El teléfono supera la longitud"; return false; };

        mensaje = "Datos válidos";
        resultado = true;
        return true;
        }
    

 
    }