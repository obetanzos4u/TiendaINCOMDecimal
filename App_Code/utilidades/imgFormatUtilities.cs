using ImageProcessor;
using ImageProcessor.Imaging.Formats;
using ImageProcessor.Plugins.WebP.Imaging.Formats;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// 20210507 RC - Utilidades para transformar una imagen a formato Webp
/// </summary>
public class imgFormatUtilities {
    public imgFormatUtilities() {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }


    /// <param name="Quality">A value between 1 and 100 to set the quality to.</param >
    public static json_respuestas Convert(Stream OriginalImg, String webpPath, int Quality) {
        try {
           
        string webpFile= webpPath + ".webp";
        string jpgFile = webpPath + ".jpg";

            ISupportedImageFormat formatJpg = new JpegFormat();


            using (var webPFileStream = new FileStream(webpFile, FileMode.Create)) {

                using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false)) {
                    imageFactory.Load(OriginalImg)
                                .Format(new WebPFormat())
                                .Quality(Quality)
                                .Save(webPFileStream);
                }
            }
            using (var webPFileStream = new FileStream(jpgFile, FileMode.Create)) {
                using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false)) {
                    imageFactory.Load(OriginalImg)
                                .Format(formatJpg)
                                .Quality(Quality)
                                .Save(webPFileStream);
                }

                return new json_respuestas(true, "OK", false, null);

        }

        }
        catch (Exception ex) {

            return new json_respuestas(false, "Error", true, null);
        }

    }
}