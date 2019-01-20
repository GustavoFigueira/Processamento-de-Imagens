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
        public static Bitmap ConvertBitmapToGrayScale(Bitmap bitmap)
        {
            // Faz um loop através dos pixels
            for (var x = 0; x < bitmap.Width; x++)
            {
                var y = 0;

                for (y = 0; y < bitmap.Height; y++)
                {
                    var pixelColor = bitmap.GetPixel(x, y);
                    var newColor = Color.FromArgb(pixelColor.R, 0, 0);

                    // Define em preto e branco
                    bitmap.SetPixel(x, y, newColor);
                }
            }

            return bitmap;
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
