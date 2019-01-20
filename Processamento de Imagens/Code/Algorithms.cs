using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using static Processamento_de_Imagens.Code.Util;

namespace Processamento_de_Imagens.Code
{
    // Classe contento  os algoritmos principais
    public static class Algorithms
    {
        // Redimensiona usando o algoritmo do Vizinho mais Próximo
        public static Bitmap NearestNeighbor(Bitmap originalImage, int amount, Option option)
        {
            // Redução (Vizinho mais Próximo)
            if (option.Equals(Option.Reduce))
            {
                // Cria a variável da nova imagem com as novas dimensões
                var newImage = new Bitmap(originalImage.Width / amount, originalImage.Height / amount);

                // Variável auxiliar
                var x = 0;

                for (var i = 0; i < originalImage.Width; i += 2)
                {
                    // Variável auxiliar
                    var y = 0;

                    for (var j = 0; j < originalImage.Height; j += 2)
                    {
                        if (x < newImage.Width && y < newImage.Height)
                            newImage.SetPixel(x, y, originalImage.GetPixel(i, j));

                        y++;
                    }
                    x++;
                }

                return newImage;
            }
            // Ampliação (Vizinho mais Próximo)
            else
            {
                // Cria a variável da nova imagem com as novas dimensões
                var newImage = new Bitmap(originalImage.Width * amount, originalImage.Height * amount);

                // Variável auxiliar
                var x = 0;

                for (var i = 0; i < originalImage.Width; i++)
                {
                    // Variável auxiliar
                    var y = 0;
                    for (var j = 0; j < originalImage.Height; j++)
                    {
                        if (x < newImage.Width && y < newImage.Height)
                        {
                            newImage.SetPixel(x, y, originalImage.GetPixel(i, j));
                            newImage.SetPixel(x, y + 1, originalImage.GetPixel(i, j));
                            newImage.SetPixel(x + 1, y, originalImage.GetPixel(i, j));
                            newImage.SetPixel(x + 1, y + 1, originalImage.GetPixel(i, j));
                        }
                        y += 2;
                    }
                    x += 2;
                }

                return newImage;
            }
        }

        // Redimensiona usando o algoritmo Bilinear
        public static Bitmap Bilinear(Bitmap originalImage, int amount, Option option)
        {
            // Redução (Bilinear)
            if (option.Equals(Option.Reduce))
            {
                // Cria a variável da nova imagem com as novas dimensões
                var newImage = new Bitmap(originalImage.Width / amount, originalImage.Height / amount);

                // Variável auxiliar
                var x = 0;

                for (var i = 0; i < originalImage.Width; i += 2)
                {
                    // Variável auxiliar
                    var y = 0;

                    for (var j = 0; j < originalImage.Height; j += 2)
                    {
                        if (x < newImage.Width && y < newImage.Height)
                        {
                            newImage.SetPixel(x, y, Color.FromArgb(
                                (originalImage.GetPixel(i, j).A + originalImage.GetPixel(i, j + 1).A + originalImage.GetPixel(i + 1, j).A + originalImage.GetPixel(i + 1, j + 1).A) / 4,
                                (originalImage.GetPixel(i, j).R + originalImage.GetPixel(i, j + 1).R + originalImage.GetPixel(i + 1, j).R + originalImage.GetPixel(i + 1, j + 1).R) / 4,
                                (originalImage.GetPixel(i, j).G + originalImage.GetPixel(i, j + 1).G + originalImage.GetPixel(i + 1, j).G + originalImage.GetPixel(i + 1, j + 1).G) / 4,
                                (originalImage.GetPixel(i, j).B + originalImage.GetPixel(i, j + 1).B + originalImage.GetPixel(i + 1, j).B + originalImage.GetPixel(i + 1, j + 1).B) / 4
                            ));
                        }
                        y++;
                    }
                    x++;
                }

                return newImage;
            }
            // Ampliação (Bilinear)
            else
            {
                // Cria a variável da nova imagem com as novas dimensões
                var newImage = new Bitmap(originalImage.Width * amount, originalImage.Height * amount);

                // Variável auxiliar
                var x = 0;

                for (var i = 0; i < originalImage.Width; i++)
                {
                    // Variável auxiliar
                    var y = 0;

                    for (var j = 0; j < originalImage.Height; j++)
                    {
                        if (x < newImage.Width && y < newImage.Height)
                        {
                            newImage.SetPixel(x, y, originalImage.GetPixel(i, j));
                            if (i < originalImage.Width - 1 && j < originalImage.Height - 1)
                            {
                                newImage.SetPixel(x + 1, y + 1, Color.FromArgb(
                                    (originalImage.GetPixel(i, j).A + originalImage.GetPixel(i, j + 1).A + originalImage.GetPixel(i + 1, j).A + originalImage.GetPixel(i + 1, j + 1).A) / 4,
                                    (originalImage.GetPixel(i, j).R + originalImage.GetPixel(i, j + 1).R + originalImage.GetPixel(i + 1, j).R + originalImage.GetPixel(i + 1, j + 1).R) / 4,
                                    (originalImage.GetPixel(i, j).G + originalImage.GetPixel(i, j + 1).G + originalImage.GetPixel(i + 1, j).G + originalImage.GetPixel(i + 1, j + 1).G) / 4,
                                    (originalImage.GetPixel(i, j).B + originalImage.GetPixel(i, j + 1).B + originalImage.GetPixel(i + 1, j).B + originalImage.GetPixel(i + 1, j + 1).B) / 4
                                 ));
                                newImage.SetPixel(x, y + 1, Color.FromArgb(
                                    (originalImage.GetPixel(i, j).A + originalImage.GetPixel(i, j + 1).A) / 2,
                                    (originalImage.GetPixel(i, j).R + originalImage.GetPixel(i, j + 1).R) / 2,
                                    (originalImage.GetPixel(i, j).G + originalImage.GetPixel(i, j + 1).G) / 2,
                                    (originalImage.GetPixel(i, j).B + originalImage.GetPixel(i, j + 1).B) / 2
                                ));
                                newImage.SetPixel(x + 1, y, Color.FromArgb(
                                    (originalImage.GetPixel(i, j).A + originalImage.GetPixel(i + 1, j).A) / 2,
                                    (originalImage.GetPixel(i, j).R + originalImage.GetPixel(i + 1, j).R) / 2,
                                    (originalImage.GetPixel(i, j).G + originalImage.GetPixel(i + 1, j).G) / 2,
                                    (originalImage.GetPixel(i, j).B + originalImage.GetPixel(i + 1, j).B) / 2
                                ));
                            }
                            else
                            {
                                if (i == originalImage.Width - 1 && j == originalImage.Height - 1)
                                {
                                    newImage.SetPixel(x + 1, y + 1, Color.Empty);
                                    newImage.SetPixel(x, y + 1, Color.Empty);
                                    newImage.SetPixel(x + 1, y, Color.Empty);
                                }
                                else
                                {
                                    if (i == originalImage.Width - 1)
                                    {
                                        newImage.SetPixel(x, y + 1, Color.FromArgb(
                                            (originalImage.GetPixel(i, j).A + originalImage.GetPixel(i, j + 1).A) / 2,
                                            (originalImage.GetPixel(i, j).R + originalImage.GetPixel(i, j + 1).R) / 2,
                                            (originalImage.GetPixel(i, j).G + originalImage.GetPixel(i, j + 1).G) / 2,
                                            (originalImage.GetPixel(i, j).B + originalImage.GetPixel(i, j + 1).B) / 2
                                        ));
                                        newImage.SetPixel(x + 1, y + 1, Color.Empty);
                                        newImage.SetPixel(x, y + 1, Color.Empty);
                                    }
                                    else
                                    {
                                        newImage.SetPixel(x + 1, y, Color.FromArgb(
                                            (originalImage.GetPixel(i, j).A + originalImage.GetPixel(i + 1, j).A) / 2,
                                            (originalImage.GetPixel(i, j).R + originalImage.GetPixel(i + 1, j).R) / 2,
                                            (originalImage.GetPixel(i, j).G + originalImage.GetPixel(i + 1, j).G) / 2,
                                            (originalImage.GetPixel(i, j).B + originalImage.GetPixel(i + 1, j).B) / 2
                                        ));
                                        newImage.SetPixel(x + 1, y + 1, Color.Empty);
                                        newImage.SetPixel(x + 1, y, Color.Empty);
                                    }

                                }

                            }
                        }
                        y += 2;
                    }
                    x += 2;
                }
                return newImage;
            }
        }

        // Equalização Histograma
        public static Bitmap HistogramEqualization(Bitmap originalImage)
        {
            // Altura e Largura da Imagem Original
            var w = originalImage.Width;
            var h = originalImage.Height;

            // Nova imagem
            var newImage = new Bitmap(w, h);

            var sd = originalImage.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            var bytes = sd.Stride * sd.Height;
            var buffer = new byte[bytes];
            var result = new byte[bytes];

            Marshal.Copy(sd.Scan0, buffer, 0, bytes);

            originalImage.UnlockBits(sd);

            var pn = new double[256];

            for (var p = 0; p < bytes; p += 4)
                pn[buffer[p]]++;

            for (var prob = 0; prob < pn.Length; prob++)
                pn[prob] /= (w * h);

            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
                {
                    var current = y * sd.Stride + x * 4;

                    double sum = 0;

                    for (var i = 0; i < buffer[current]; i++)
                        sum += pn[i];

                    for (var c = 0; c < 3; c++)
                        result[current + c] = (byte)Math.Floor(255 * sum);

                    result[current + 3] = 255;
                }
            }

            var rd = newImage.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            Marshal.Copy(result, 0, rd.Scan0, bytes);

            newImage.UnlockBits(rd);

            return newImage;
        }

        // Transformação de Intensidade (Alargamento de Contraste)
        public static Bitmap ContrastStretching(Bitmap originalImage)
        {
            // Nova imagem
            var newImage = new Bitmap(originalImage, new Size(originalImage.Height, originalImage.Width));

            var w = originalImage.Width;
            var h = originalImage.Height;

            var sd = originalImage.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            var bytes = sd.Stride * sd.Height;
            var buffer = new byte[bytes];
            var result = new byte[bytes];

            Marshal.Copy(sd.Scan0, buffer, 0, bytes);
            originalImage.UnlockBits(sd);

            var current = 0;
            var max = 0;
            var min = 255;

            for (var i = 0; i < buffer.Length; i++)
            {
                max = Math.Max(max, buffer[i]);
                min = Math.Min(min, buffer[i]);
            }

            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
                {
                    current = y * sd.Stride + x * 4;

                    for (var i = 0; i < 3; i++)
                        result[current + i] = (byte)((buffer[current + i] - min) * 100 / (max - min));

                    result[current + 3] = 255;
                }
            }

            var resimg = new Bitmap(w, h);
            var rd = resimg.LockBits(new Rectangle(0, 0, w, h),ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            Marshal.Copy(result, 0, rd.Scan0, bytes);

            resimg.UnlockBits(rd);

            return resimg;

        }

        // Transformação de Intensidade (Negativo)
        public static Bitmap Negative(Bitmap originalImage)
        {
            // Nova imagem
            var newImage = new Bitmap(originalImage, new Size(originalImage.Height, originalImage.Width));

            int w = originalImage.Width;
            int h = originalImage.Height;

            BitmapData srcData = originalImage.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            int bytes = srcData.Stride * srcData.Height;

            byte[] buffer = new byte[bytes];    
            byte[] result = new byte[bytes];

            Marshal.Copy(srcData.Scan0, buffer, 0, bytes);
            originalImage.UnlockBits(srcData);

            int current = 0;
            int cChannels = 3;

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    current = y * srcData.Stride + x * 4;

                    for (int c = 0; c < cChannels; c++)
                        result[current + c] = (byte)(255 - buffer[current + c]);

                    result[current + 3] = 255;
                }
            }

            Bitmap resImg = new Bitmap(w, h);
            BitmapData resData = resImg.LockBits(new Rectangle(0, 0, w, h),ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(result, 0, resData.Scan0, bytes);

            resImg.UnlockBits(resData);

            return resImg;
        }

        // Operação Aritmética (Adição)
        public static Bitmap AdditionBlendMode(Bitmap originalImage, Bitmap blendingImage)
        {
            // Nova imagem
            var newImage = new Bitmap(originalImage.Width, originalImage.Height);

            for (var x = 0; x < originalImage.Width; x++)
                for (var y = 0; y < originalImage.Height; y++)
                    newImage.SetPixel(x, y, Color.FromArgb(originalImage.GetPixel(x, y).ToArgb() + blendingImage.GetPixel(x, y).ToArgb()));

            return newImage;
        }

        // Operação Aritmética Subtração
        public static Bitmap SubtractBlendMode(Bitmap originalImage, Bitmap blendingImage)
        {
            // Nova imagem
            var newImage = new Bitmap(originalImage.Width, originalImage.Height);

            for (var x = 0; x < originalImage.Width; x++)
                for (var y = 0; y < originalImage.Height; y++)
                    newImage.SetPixel(x, y, Color.FromArgb(blendingImage.GetPixel(x, y).ToArgb() - originalImage.GetPixel(x, y).ToArgb()));

            return newImage;
        }

        // Operação Geométrica (Rotação)
        public static Bitmap Rotation(Bitmap originalImage)
        {
            // Nova imagem
            var newImage = new Bitmap(originalImage, new Size(originalImage.Height, originalImage.Width));

            for (var i = 0; i < originalImage.Height; i++)
                for (var j = 0; j < originalImage.Width; j++)
                    newImage.SetPixel(i, j, originalImage.GetPixel(j, originalImage.Height - i - 1));

            return newImage;
        }

        // Rotulação
        public static Bitmap Rotulation(Bitmap originalImage)
        {
            // Nova imagem
            Bitmap newImage = originalImage;

            var height = originalImage.Height;
            var width = originalImage.Width;

            return newImage;
        }
    }
}
