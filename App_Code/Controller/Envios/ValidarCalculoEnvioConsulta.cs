using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Realiza consulta del carrito o la guarda, él # de operación lo considera como el email del usuario pues los carritos son únicos por cliente.
/// </summary>
public class ValidarCalculoEnvioConsulta
{

    public string host { get; set; }
    public bool OperacionValida { get {return _OperacionValida; } }
    private bool _OperacionValida { get; set; }
    public string Message { get { return _Message; } }
    private string _Message { get; set; }

    public decimal CostoEnvio { get { return _CostoEnvio; } }
    private decimal _CostoEnvio { get; set; }

    public decimal MontoDeclarado { get; set; }
   
    private direcciones_envio DireccionEnvio { get; set; }

    private string TipoConsulta { get; set; }
    private string EmailCarrito { get; set; }
 private List<object> Productos { get; set; }
    private DataTable _DTproductos { get; set; }
      
    public ValidarCalculoEnvioConsulta(direcciones_envio _DireccionEnvio, DataTable DTproductos, string _EmailCarrito, decimal _MontoDeclarado)
    {
        DireccionEnvio = _DireccionEnvio;
        EmailCarrito = _EmailCarrito;
     
        TipoConsulta = "";

        MontoDeclarado = _MontoDeclarado; 
        _DTproductos = DTproductos;
         
     
    }
   
    
    private List<object> ProcesarProductosDT(DataTable DTproductos) {
        var ListadoProductos = new List<object>();
        foreach (DataRow r in DTproductos.Rows) {
            string Numero_Parte = r["numero_parte"].ToString();
            string strPesoKg = r["peso"].ToString();
            decimal PesoKg;

            string strLargo = r["profundidad"].ToString();
            string strAncho = r["ancho"].ToString();
            string strAlto = r["alto"].ToString();
             
            if (string.IsNullOrWhiteSpace(strAlto) || string.IsNullOrWhiteSpace(strAncho) || string.IsNullOrWhiteSpace(strLargo)) {
                _Message = "El producto: " + Numero_Parte + " no tiene las dimensiones completas";
            
                throw new Exception(_Message);

            }
            if (string.IsNullOrWhiteSpace(strPesoKg)) {
                _Message = "El producto: " + Numero_Parte + " no tiene el peso establecido";

                throw new Exception(_Message);
            }
            PesoKg = decimal.Parse(strPesoKg);
            decimal? Largo = decimal.Parse(r["profundidad"].ToString()) ;
            decimal? Ancho = decimal.Parse(r["ancho"].ToString()) ;
            decimal? Alto = decimal.Parse(r["alto"].ToString());
            decimal? Cantidad = decimal.Parse(r["cantidad"].ToString());

            bool? RotacionHorizontal;
            bool? RotacionVertical;

            try { 
                  RotacionHorizontal = bool.Parse(r["RotacionHorizontal"].ToString());
                  RotacionVertical = bool.Parse(r["RotacionVertical"].ToString());
            } catch (Exception) {
                _Message = "El producto: " + Numero_Parte + " no tiene las condiciones de rotación";
                throw new Exception(_Message);
            }

            var producto = new {

                Numero_Parte = Numero_Parte,
                PesoKg = PesoKg,
                Largo = Largo,
                Ancho = Ancho,
                Alto = Alto,
                Cantidad = Cantidad,

                RotacionVertical = RotacionVertical,
                RotacionHorizontal = RotacionHorizontal
            };
            ListadoProductos.Add(producto);
        }

        return ListadoProductos;

    }


    public  async Task validarConsulta() {
       Productos = ProcesarProductosDT(_DTproductos);

        bool ApiCalculoFletectivado = operacionesConfiguraciones.obtenerEstatusApiFlete();



        #region Validaciones previas (condiciones de la operación)

       //   1 - Validacción si esta activado el sistema de cálculo con API
        if (ApiCalculoFletectivado == false )
        {
            _Message = "El cálculo del flete mediante API esta deshabilitado";
            _OperacionValida = false;
            return;
        }

        // Si la dirección de envío no se ha recibido/establecido

        if (DireccionEnvio == null) {
            _Message = "La dirección de envío no es válida o no se ha establecido";
            _OperacionValida = false;
            return;
        }

        #endregion
 
            try  {
                CalcularEnvioOperacion Envio = new CalcularEnvioOperacion(TipoConsulta, EmailCarrito, DireccionEnvio);
            Envio.host = host;
            Envio.MontoDeclarado = MontoDeclarado;

                foreach (dynamic p in Productos) {

                    Envio.AgregarProducto(p.Numero_Parte, p.PesoKg, p.Largo, p.Ancho, p.Alto, p.Cantidad, p.RotacionVertical, p.RotacionHorizontal);
                }

               var result = await Envio.CalcularEnvio();
 
                if (Envio.IsValidCalculo) {
                      _CostoEnvio = (decimal)Envio.CostoEnvio;
                   
                }
                else {
                  
                }
            

                _Message = Envio.MessageCalculo;
                _OperacionValida = Envio.IsValidCalculo;

                
                return;
            }

                     
            catch (Exception ex) {
            // Si ocurre un error al calcularse el envio, lo establecemos en ninguno.
            _Message = "Ocurrio un error al calcular el envio";
                _OperacionValida = false;

            devNotificaciones.error(Message, ex);
                return;
            }



   
    }
     

}