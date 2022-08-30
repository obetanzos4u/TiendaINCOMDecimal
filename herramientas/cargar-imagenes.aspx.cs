using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class herramientas_configuraciones_cargar_imagenes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Title = "Carga de imágenes";
        }
    }

    protected bool IsValidFormat(string ContentType)
    {
        if (ContentType == "application/pdf") return true;
        if (ContentType == "image/jpeg") return true;
        if (ContentType == "image/gif") return true;
        if (ContentType == "image/png") return true;

        return false;
    }

    protected void btn_CargarImagenes_Click(object sender, EventArgs e)
    {

        int Quality = 50;


        if (ddlActionOption.SelectedValue == "")
        {
            materializeCSS.crear_toast(this, "Selecciona un módulo.", false);
            return;
        }


        var PhysicalApplicationPath = HttpContext.Current.Request.PhysicalApplicationPath + ddlActionOption.SelectedValue;
        if (fu_CargaImagenes.HasFiles)
        {
            var LogResultCarga = "";
            foreach (var file in fu_CargaImagenes.PostedFiles)
            {
                try
                {
                    #region Validar el tipo de archivo cargado
                    if (IsValidFormat(file.ContentType) == false)
                    {
                        LogResultCarga += $"Error extension/type # {file.FileName} \n";
                        continue;
                    }
                    #endregion
                    if (file.ContentType == "application/pdf")
                    {

                        file.SaveAs(PhysicalApplicationPath + file.FileName);
                        LogResultCarga += $"Done # {file.FileName}   \n";
                    }
                    else
                    {
                        string filePath = PhysicalApplicationPath + Path.GetFileNameWithoutExtension(file.FileName);
                        var imgStream = file.InputStream;

                        var resultOperation = imgFormatUtilities.Convert(imgStream, filePath, Quality);
                        if (resultOperation.result == true)
                        {
                            LogResultCarga += $"Done # {file.FileName}   \n";
                        }
                        else
                        {
                            LogResultCarga += $"Error # {file.FileName} \n";
                        }
                    }


                }
                catch (Exception ex)
                {

                    LogResultCarga += "Error # " + file.FileName + " " + ex.Message + " \n\n";
                }





            } // Fin foreach

            txt_log_carga_imagenes.Text = LogResultCarga;
        }
        else
        { // Si no encontro archivos
            materializeCSS.crear_toast(this, "No se han elegido archivos.", false);
        }

    }

    protected void btn_buscar_archivos_Click(object sender, EventArgs e)
    {
        if (ddlActionOption.SelectedValue == "")
        {
            materializeCSS.crear_toast(this, "No haz seleccionado un módulo de carga", false);
            return;
        }

        var PhysicalApplicationPath = HttpContext.Current.Request.PhysicalApplicationPath + ddlActionOption.SelectedValue;
        string[] files = Directory.GetFiles(PhysicalApplicationPath);

        int totalFiles = files.Length;
        lt_cantidad_archivos_encontrados.Text = "<span class='is-font-semibold'>Productos encontrados:</span> " + totalFiles.ToString();

        string search = txt_text_buscar_archivos.Text;
        if (!string.IsNullOrWhiteSpace(search))
        {
            files = files.Where(f => f.ToLower().Contains(search.ToLower())).ToArray();
        }
        List<dynamic> List = new List<dynamic>();

        int i = 0;
        foreach (string file in files)
        {
            var Extension = Path.GetExtension(file);
            if (Extension == ".config" ||
                Extension == ".cs" ||
                Extension == ".aspx")
            {
                continue;
            }

            dynamic fileElement = new System.Dynamic.ExpandoObject();
            fileElement.FileName = Path.GetFileName(file);
            fileElement.FilePath = file;
            fileElement.Extension = Extension;
            fileElement.CreationTime = File.GetCreationTime(file);

            List.Add(fileElement);
            i++;

        }
        List = List.OrderBy(f => f.CreationTime).Take(100).ToList();

        Lv_ListadoDeArchivos.DataSource = List;
        Lv_ListadoDeArchivos.DataBind();
        up_ListadoDeArchivos.Update();
    }

    protected void Lv_ListadoDeArchivos_ItemDataBound(object sender, ListViewItemEventArgs e)
    {


        if (e.Item.ItemType == ListViewItemType.DataItem)
        {


            dynamic File = e.Item.DataItem as dynamic;

            Image imgFile = (Image)e.Item.FindControl("imgFile");

            string Folder = ddlActionOption.SelectedValue.Replace("\\", "/");
            string FileName = File.FileName;
            string Extension = File.Extension;

            if (Extension != ".pdf")
            {

                imgFile.ImageUrl = Folder + FileName;
            }
            else if (Extension == ".pdf")
            {
                imgFile.ImageUrl = "/img/webUI/pdf.gif";
            }



        }
    }

    protected void btn_eliminar_Click(object sender, EventArgs e)
    {

        try
        {
            var btn = (LinkButton)sender;

            var FilePath = ((Label)btn.NamingContainer.FindControl("lbl_file_path")).Text;

            // Check if file exists with its full path    
            if (File.Exists(FilePath))
            {
                // If file found, delete it    
                File.Delete(FilePath);
                materializeCSS.crear_toast(up_ListadoDeArchivos, "Eliminado con éxito", true);
                btn_buscar_archivos_Click(sender, e);
            }
            else materializeCSS.crear_toast(up_ListadoDeArchivos, "El archivo no existe", false);
        }
        catch (IOException ioExp)
        {
            materializeCSS.crear_toast(up_ListadoDeArchivos, "Ocurrio un error", false);
        }
    }
}