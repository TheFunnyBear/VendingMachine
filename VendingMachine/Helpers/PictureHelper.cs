using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web;
using VendingMachine.Model;

namespace VendingMachine.Helpers
{
    public sealed class PictureHelper : IPictureHelper
    {
        public Picture GetPictureFromPostedFile(HttpPostedFileBase httpPostedFile, PictureType pictureType)
        { 
            int width = 0, height = 0;

            if (pictureType == PictureType.Coin)
            {
                width = 50;
                height = 50;
            }
            if (pictureType == PictureType.Product)
            {
                width = 150;
                height = 200;
            }

            var picture = new Picture();
            picture.BinaryData = new byte[httpPostedFile.ContentLength];
            httpPostedFile.InputStream.Read(picture.BinaryData, 0, httpPostedFile.ContentLength);

            var resizedImage = ResizeImage(picture.BinaryData, width, height);
            var resizedImageBytes = (byte[])new ImageConverter().ConvertTo(resizedImage, typeof(byte[]));

            picture.BinaryData = new byte[resizedImageBytes.Length];
            Array.Copy(resizedImageBytes, picture.BinaryData, resizedImageBytes.Length);

            return picture;
        }

        private static Bitmap ResizeImage(byte[] jpegByteArray, int width, int height)
        {
            Image image = (Bitmap)new ImageConverter().ConvertFrom(jpegByteArray);
            return ResizeImage(image, width, height);
        }

        private static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

    }
}