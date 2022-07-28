public class ProductoEnvioCalculoModel
{
    public string Numero_Operacion { get; set; }
    public string Numero_Parte { get; set; }

    public int Tipo { get; set; }
    public decimal? Cantidad { get; set; }
    public decimal? PesoKg { get; set; }
    public decimal? Largo { get; set; }
    public decimal? Ancho { get; set; }
    public decimal? Alto { get; set; }

    public bool? RotacionVertical { get; set; }
    public bool? RotacionHorizontal { get; set; }

    public int? DisponibleParaEnvioGratuito { get; set; }

}
