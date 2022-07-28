using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Si la afiliación lo requiere y éste elemento está presente, contiene la información necesaria para solicitar una autenticación al servicio de 3D Secure.
/// </summary>
public class SantanderData3ds
{
    private string v1;
    private long v2;
    private string v3;
    private string v4;
    private string v5;
    private string v6;

    /// <summary>
    ///  Especifica el correo electrónico del tarjetahabiente de hasta cien caracteres.
    /// </summary>
    public string ml { get; set; }
    /// <summary>
    ///  Especifica el número telefónico del tarjetahabiente de hasta veinte posiciones.
    /// </summary>
    public Int64 cl { get; set; }
    /// <summary>
    /// Especifica el domicilio del tarjetahabiente de hasta sesenta caracteres.
    /// </summary>
    public string dir { get; set; }
    /// <summary>
    ///  Especifica la ciudad del domicilio del tarjetahabiente de hasta treinta caracteres.
    /// </summary>
    public string cd { get; set; }
    /// <summary>
    ///  Especifica la clave ISO 3166-2 del estado del domicilio del tarjetahabiente de dos caracteres.
    /// </summary>
    public string est { get; set; }
    /// <summary>
    ///  Especifica el código postal del domicilio del tarjetahabiente de hasta diez caracteres.
    /// </summary>
    public string cp { get; set; }
    /// <summary>
    /// Especifica la clave numérica ISO 3166-1 del país del domicilio del tarjetahabiente de 3 caracteres.
    /// </summary>
    public string idc { get; set; }

    public SantanderData3ds()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public SantanderData3ds(string ml, Int64 cl, string dir, string cd, string est, string cp, string idc)
    {
        if (string.IsNullOrWhiteSpace(ml)) throw new Exception("No se ha recibido el parámetro email para Data 3DS");
        if (textTools.validarEmail(ml) == false) throw new Exception("El email no tiene formato válido para  Data 3DS");
        this.ml = ml;
        this.cl = cl;
        this.dir = dir;
        this.cd = cd;
        this.est = est;
        this.cp = cp;
        this.idc = idc;
    }

    public SantanderData3ds(string ml, Int64 cl, string dir, string cd, string est, string cp)
    {
        if (string.IsNullOrWhiteSpace(ml)) throw new Exception("No se ha recibido el parámetro email para Data 3DS");
        if (textTools.validarEmail(ml) == false) throw new Exception("El email no tiene formato válido para  Data 3DS");
        this.ml = ml;
        this.cl = cl;
        this.dir = dir;
        this.cd = cd;
        this.est = est;
        this.cp = cp;

    }

 
}