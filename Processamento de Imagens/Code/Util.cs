using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace Processamento_de_Imagens.Code
{
    // Class com métodos úteis e compartilhados
    public static class Util
    {
        // Enum das opções de redimensionamento
        public enum Option { Reduce, Enlarge };

        // Converte a Bitmap para Tons de Cinza
        public static Bitmap ConvertBitmapToGrayScale(Bitmap original)
        {
            var newBitmap = new Bitmap(original.Width, original.Height);

            var g = Graphics.FromImage(newBitmap);

            var colorMatrix = new ColorMatrix(
                new float[][]
                {
                    new float[] {.3f, .3f, .3f, 0, 0},
                    new float[] {.59f, .59f, .59f, 0, 0},
                    new float[] {.11f, .11f, .11f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                });

            var attributes = new ImageAttributes();

            attributes.SetColorMatrix(colorMatrix);

            g.DrawImage(original, new System.Drawing.Rectangle(0, 0, original.Width, original.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            g.Dispose();

            return newBitmap;
        }

        // Converte de Imagem para Bitmap
        public static Bitmap ConvertImageToBitmap(BitmapImage bitmapImage)
        {
            using (var outStream = new MemoryStream())
            {
                var enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                var bitmap = new Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }

        // Converte Bitmap para Imagem
        public static BitmapImage ConvertBitmapToImage(Bitmap bitmap)
        {
            var ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Bmp);
            var image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();

            return image;
        }

        // Salva a imagem no computador
        public static void SaveImage(Bitmap image, string fileName = "", string format = ".png")
        {
            const string path = @"C:\PI\Resultados";

            if (Directory.Exists(path))
                image.Save(Path.Combine(path, fileName + format), ImageFormat.Jpeg);
            else
            {
                Directory.CreateDirectory(path);
                image.Save(Path.Combine(path, fileName + ".jpeg"), ImageFormat.Jpeg);
            }
        }
    }
}
