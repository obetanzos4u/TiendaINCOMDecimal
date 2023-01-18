using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class herramientas_reporte_pedidos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Title = "Gestor de pedidos";
    }
    protected async void btn_ConsultarPedidos_Click(object sender, EventArgs e)
    {
        var FechaHoy = utilidad_fechas.obtenerCentral();

        if (string.IsNullOrWhiteSpace(txt_fecha_desde.Text)) txt_fecha_desde.Text = FechaHoy.AddDays(-15).ToString("dd/MM/yyyy");
        if (string.IsNullOrWhiteSpace(txt_fecha_hasta.Text)) txt_fecha_hasta.Text = FechaHoy.AddDays(1).ToString("dd/MM/yyyy");

        DateTime desde = DateTime.Parse(txt_fecha_desde.Text + " 00:00:00");
        DateTime hasta = DateTime.Parse(txt_fecha_hasta.Text + " 23:59:59");

        //bool omitir_repetidos = chk_omitir_repetidos.Checked;
        bool omitir_cancelados = chk_omitir_cancelados.Checked;
        //bool omitir_pruebas = chk_omitir_pruebas.Checked;
        bool omitir_cotizaciones = chk_omitir_cotizaciones.Checked;

        var pedidosDatos = await PedidosEF.ObtenerDatos(desde, hasta, omitir_cancelados, omitir_cotizaciones);

        List<DTO_Pedido> Pedidos = new List<DTO_Pedido>();

        foreach (var p in pedidosDatos)
        {
            var Pedido = new DTO_Pedido();

            Pedido.PedidoDatos = p;
            Pedido.PedidoDatosNumericos = PedidosEF.ObtenerNumeros(p.numero_operacion);
            Pedido.PedidoProductos = PedidosEF.ObtenerProductos(p.numero_operacion);
            Pedido.Pagado = await PedidosEF.ObtenerPagoPedido(p.numero_operacion);
            Pedidos.Add(Pedido);
        }
        var Reporte = new DTO_PedidosReportes(Pedidos);

        gv_desgloseMeses.DataSource = Reporte.DesgloseMeses;
        gv_desgloseMeses.DataBind();
        lbl_total_registros.Text = Reporte.Pedidos.Count.ToString() + " operaciones encontradas";

        tb_pedidos.Visible = true;
        MostrarPedidos(Reporte.Pedidos);
    }
    protected void MostrarInfo()
    {


    }
    protected void MostrarPedidos(List<DTO_Pedido> Pedidos)
    {
        lv_desglose_pedidos.DataSource = Pedidos.OrderBy(p => p.PedidoDatos.fecha_creacion).ToList();
        lv_desglose_pedidos.DataBind();
    }
    protected void gv_desgloseMeses_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView grid = (GridView)sender;
        BoundField col = (BoundField)grid.Columns[3];
        int numDecimals = 2; // from database
        col.DataFormatString = "{0:N" + numDecimals + "}";
    }
    protected void lv_desglose_pedidos_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            DTO_Pedido Pedido = e.Item.DataItem as DTO_Pedido;
            Label lbl_OperacionCancelada = (Label)e.Item.FindControl("lbl_OperacionCancelada");
            HiddenField hf_numero_operacion = (HiddenField)e.Item.FindControl("hf_numero_operacion");
            HiddenField hf_id_operacion = (HiddenField)e.Item.FindControl("hf_id_operacion");
            UpdatePanel up_desglose_pedido = e.Item.FindControl("up_desglose_pedido") as UpdatePanel;
            Panel ContentPedidoDesactivar = up_desglose_pedido.FindControl("ContentPedidoDesactivar") as Panel;
            Panel ContentPedidoActivar = up_desglose_pedido.FindControl("ContentPedidoActivar") as Panel;
            Label lbl_estatus_pago = (Label)e.Item.FindControl("lbl_estatus_pago");
            Label txt_tipo_operacion = (Label)e.Item.FindControl("txt_tipo_operacion");

            var Pago = Pedido.Pagado as json_respuestas;
            lbl_estatus_pago.Text = Pago.message;

            if (hf_numero_operacion.Value.StartsWith("pc"))
            {
                txt_tipo_operacion.Text = "Cotización";
            }

            if (Pago.result == false)
            {
                lbl_estatus_pago.ForeColor = System.Drawing.Color.Red;
            }

            if (Pedido.PedidoDatos.OperacionCancelada == null || Pedido.PedidoDatos.OperacionCancelada == false)
            {
                lbl_OperacionCancelada.Text = "&#9679; Activo";
                lbl_OperacionCancelada.ForeColor = System.Drawing.Color.Green;
                ContentPedidoDesactivar.Visible = true;
                ContentPedidoActivar.Visible = false;
            }
            else
            {
                lbl_OperacionCancelada.Text = "&#65794 Cancelado";
                lbl_OperacionCancelada.ForeColor = System.Drawing.Color.Red;
                ContentPedidoDesactivar.Visible = false;
                ContentPedidoActivar.Visible = true;
            }

            HyperLink link_pedido_resumen = (HyperLink)e.Item.FindControl("link_pedido_resumen");
            link_pedido_resumen.Target = "_blank";
            string UrlPedidoResumen = GetRouteUrl("cliente-pedido-resumen", new System.Web.Routing.RouteValueDictionary
            {
                { "id_operacion", seguridad.Encriptar( hf_id_operacion.Value) }
            });

            link_pedido_resumen.NavigateUrl = UrlPedidoResumen;
        }
    }
    protected void btn_desactivar_pedido_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        HiddenField hf_numero_operacion = btn.NamingContainer.FindControl("hf_numero_operacion") as HiddenField;
        TextBox txt_motivo_cancelacion = btn.NamingContainer.FindControl("txt_motivo_cancelacion") as TextBox;
        UpdatePanel up_desglose_pedido = btn.NamingContainer.FindControl("up_desglose_pedido") as UpdatePanel;
        Panel ContentPedidoDesactivar = btn.NamingContainer.FindControl("ContentPedidoDesactivar") as Panel;
        Panel ContentPedidoActivar = btn.NamingContainer.FindControl("ContentPedidoActivar") as Panel;
        Label lbl_OperacionCancelada = (Label)btn.NamingContainer.FindControl("lbl_OperacionCancelada");
        string motivo = txt_motivo_cancelacion.Text;
        var resultadoCancelacion = PedidosEF.CancelarPedido(hf_numero_operacion.Value, motivo);

        NotiflixJS.Message(this, NotiflixJS.MessageType.info, resultadoCancelacion.message);
        //materializeCSS.crear_toast(this, resultadoCancelacion.message, resultadoCancelacion.result);

        if (resultadoCancelacion.result)
        {
            lbl_OperacionCancelada.Text = "&#65794 Cancelada";
            lbl_OperacionCancelada.ForeColor = System.Drawing.Color.Red;
            ContentPedidoDesactivar.Visible = false;
            ContentPedidoActivar.Visible = true;
        }
        up_desglose_pedido.Update();
    }
    protected void btn_activar_pedido_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        HiddenField hf_numero_operacion = btn.NamingContainer.FindControl("hf_numero_operacion") as HiddenField;
        TextBox txt_motivo_cancelacion = btn.NamingContainer.FindControl("txt_motivo_cancelacion") as TextBox;
        UpdatePanel up_desglose_pedido = btn.NamingContainer.FindControl("up_desglose_pedido") as UpdatePanel;
        Panel ContentPedidoDesactivar = btn.NamingContainer.FindControl("ContentPedidoDesactivar") as Panel;
        Panel ContentPedidoActivar = btn.NamingContainer.FindControl("ContentPedidoActivar") as Panel;
        Label lbl_OperacionCancelada = (Label)btn.NamingContainer.FindControl("lbl_OperacionCancelada");

        var resultadoCancelacion = PedidosEF.ReactivarPedido(hf_numero_operacion.Value);

        NotiflixJS.Message(this, NotiflixJS.MessageType.info, resultadoCancelacion.message);
        //materializeCSS.crear_toast(this, resultadoCancelacion.message, resultadoCancelacion.result);

        if (resultadoCancelacion.result)
        {
            lbl_OperacionCancelada.Text = "&#9679; Activa";
            lbl_OperacionCancelada.ForeColor = System.Drawing.Color.Green;
            ContentPedidoDesactivar.Visible = true;
            ContentPedidoActivar.Visible = false;
        }
        up_desglose_pedido.Update();
    }
}