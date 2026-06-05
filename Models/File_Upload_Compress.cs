using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace DemoApp25.Models
{
    public class File_Upload_Compress
    {
        public static void Compressimage(string targetPath, String filename, Byte[] byteArrayIn)
        {
            try
            {
                ImageConverter converter = new ImageConverter();
                using (MemoryStream memstr = new MemoryStream(byteArrayIn))
                {
                    using (var image = Image.FromStream(memstr))
                    {
                        float maxHeight = 900.0f;
                        float maxWidth = 900.0f;
                        int newHeight;
                        int newWidth; string extenstion;
                        Bitmap originalBMP = new Bitmap(memstr);
                        int originalWidth = originalBMP.Width;
                        int originalHeight = originalBMP.Height;
                        if (originalWidth > maxWidth || originalHeight > maxHeight)
                        {
                            //to preserve accept ratio
                            float ratioX = (float)maxWidth / (float)originalWidth;
                            float ratioY = (float)maxHeight / (float)originalHeight;
                            float ratio = Math.Min(ratioX, ratioY);
                            newWidth = (int)(originalWidth * ratio);
                            newHeight = (int)(originalHeight * ratio);
                        }
                        else
                        {
                            newWidth = (int)originalWidth;
                            newHeight = (int)originalHeight;
                        }
                        Bitmap bitMap1 = new Bitmap(originalBMP, newWidth, newHeight);
                        Graphics imgGraph = Graphics.FromImage(bitMap1);
                        extenstion = Path.GetExtension(targetPath);
                        if (extenstion.ToLower() == ".png" || extenstion.ToLower() == ".gif" || extenstion.ToLower() == ".jpeg")
                        {
                            imgGraph.SmoothingMode = SmoothingMode.AntiAlias;
                            imgGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            imgGraph.DrawImage(originalBMP, 0, 0, newWidth, newHeight);
                            bitMap1.Save(targetPath, image.RawFormat);
                            bitMap1.Dispose();
                            imgGraph.Dispose();
                            originalBMP.Dispose();
                        }
                        else if (extenstion.ToLower() == ".jpg")
                        {
                            imgGraph.SmoothingMode = SmoothingMode.AntiAlias;
                            imgGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            imgGraph.DrawImage(originalBMP, 0, 0, newWidth, newHeight);
                            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                            Encoder myEncoder = Encoder.Quality;
                            EncoderParameters myEncodeParameters = new EncoderParameters(1);
                            EncoderParameter myEncoderparameter = new EncoderParameter(myEncoder, 50L);
                            myEncodeParameters.Param[0] = myEncoderparameter;
                            bitMap1.Save(targetPath, jpgEncoder, myEncodeParameters);
                            bitMap1.Dispose(); imgGraph.Dispose();
                            originalBMP.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string exc = ex.Message;
                throw;
            }
        }
        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}