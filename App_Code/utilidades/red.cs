using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de red
/// </summary>
public class red {
    public red() {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public static string GetDireccionIp(System.Web.HttpRequest request) {

        // Recuperamos la IP de la máquina del cliente

        // Primero comprobamos si se accede desde un proxy

        string ipAddress1 = request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        // Acceso desde una máquina particular

        string ipAddress2 = request.ServerVariables["REMOTE_ADDR"];



        string ipAddress = string.IsNullOrEmpty(ipAddress1) ? ipAddress2 : ipAddress1;



        // Devolvemos la ip

        return ipAddress;

    }
}