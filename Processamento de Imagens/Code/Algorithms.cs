using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
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

        // Limiariazação
        public static Bitmap Thresholding(Bitmap originalImage, bool invert = false)
        {
            // Nova imagem
            var newImage = new Bitmap(originalImage.Width, originalImage.Height);

            // Percorre os pixels da imagem.
            // 1. Verifica se o pixel é claro (Valor HSV menor igual que 0.5 (entre 0 e 1) ou valor RGB menor que 127) e preenche de preto/branco) ou
            // 2. Verifica se o pixel é escuro (Valor HSV maior que 0.5 (entre 0 e 1) ou valor RGB entre 127 e 255) e preenche de branco/preto).

            for (var x = 0; x < originalImage.Width; x++)
                for (var y = 0; y < originalImage.Height; y++)
                    newImage.SetPixel(x, y, originalImage.GetPixel(x, y).GetBrightness() <= 0.5f? (invert? Color.Black : Color.White) : (invert ? Color.White : Color.Black));

            return newImage;
        }

        // Transformação de Intensidade (Negativo)
        public static Bitmap Negative(Bitmap originalImage)
        {
            // Nova imagem
            var newImage = new Bitmap(originalImage, new Size(originalImage.Height, originalImage.Width));

            var w = originalImage.Width;
            var h = originalImage.Height;

            var srcData = originalImage.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            var bytes = srcData.Stride * srcData.Height;

            var buffer = new byte[bytes];    
            var result = new byte[bytes];

            Marshal.Copy(srcData.Scan0, buffer, 0, bytes);
            originalImage.UnlockBits(srcData);

            var current = 0;
            var cChannels = 3;

            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
                {
                    current = y * srcData.Stride + x * 4;

                    for (var c = 0; c < cChannels; c++)
                        result[current + c] = (byte)(255 - buffer[current + c]);

                    result[current + 3] = 255;
                }
            }

            var resImg = new Bitmap(w, h);
            var resData = resImg.LockBits(new Rectangle(0, 0, w, h),ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

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
            //var newImage = new Bitmap(originalImage.Width, originalImage.Height);

            //for row in data:
            //for column in row:
            //if data[row][column] is not Background

            //neighbors = connected elements with the current element's value

            //if neighbors is empty
            //    linked[NextLabel] = set containing NextLabel
            //labels[row][column] = NextLabel
            //NextLabel += 1

            //else

            //Find the smallest label

            //    L = neighbors labels
            //labels[row][column] = min(L)
            //for label in L
            //linked[label] = union(linked[label], L)

            // Segunda varredura

            //for row in data
            //for column in row
            //if data[row][column] is not Background
            //labels[row][column] = find(labels[row][column])

            //return labels


            var newImage = ConvertBitmapToGrayScale(originalImage);

            var rotulo = new List<int>();
            var matrizRotulo = new int[originalImage.Width, originalImage.Height];
            var equivalencia = new List<int[]>();

            rotulo.Add(0);

            for (var i = 0; i < originalImage.Width; i++)
            {
                for (var j = 0; j < originalImage.Height; j++)
                {
                    newImage.SetPixel(i, j, (newImage.GetPixel(i, j).R <= 127) ? Color.FromArgb(0, 0, 0) : Color.FromArgb(255, 255, 255));
                    matrizRotulo[i, j] = rotulo.Last();
                }
            }

            rotulo.Add(rotulo.Last() + 1);

            // Primeira Varredura
            for (var i = 0; i < newImage.Width; i++)
            {
                for (var j = 0; j < newImage.Height; j++)
                {
                    if (newImage.GetPixel(i, j).R != 255) continue;

                    var r = (i > 0) ? (newImage.GetPixel(i - 1, j)) : Color.Empty;
                    var s = (j > 0) ? (newImage.GetPixel(i, j - 1)) : Color.Empty;

                    var failR = r.R == 0 || r == Color.Empty;
                    var failS = s.R == 0 || s == Color.Empty;

                    if (failR && failS)
                    {
                        rotulo.Add(rotulo.Last() + 1);
                        matrizRotulo[i, j] = rotulo.Last();
                    }
                    else if ((failR && !failS) || (!failR && failS))
                    {
                        matrizRotulo[i, j] = (failR) ? matrizRotulo[i, j - 1] : matrizRotulo[i - 1, j];
                    }
                    else if ((!failR && !failS) && (matrizRotulo[i - 1, j] == matrizRotulo[i, j - 1]))
                    {
                        matrizRotulo[i, j] = matrizRotulo[i, j - 1];
                    }
                    else if ((!failR && !failS) && (matrizRotulo[i - 1, j] != matrizRotulo[i, j - 1]))
                    {
                        matrizRotulo[i, j] = Math.Min(matrizRotulo[i, j - 1], matrizRotulo[i - 1, j]);
                        equivalencia.Add(new int[] { matrizRotulo[i, j - 1], matrizRotulo[i - 1, j] });
                    }
                }

            }

            var salto = 1;

            if (rotulo.Count <= 16581375)
                salto = (16581375 / rotulo.Count);

            var labels = new Dictionary<int, Color>();
            int red = 0, green = 0, blue = 0;

            for (var i = 0; i < rotulo.Count; i++)
            {
                if (salto <= 255)
                {
                    blue += salto;

                    if (blue > 255)
                    {
                        blue = 255 - salto;
                        green++;

                        if (green > 255)
                        {
                            green = 0;
                            red++;
                        }
                    }
                }
                else
                {
                    blue = (blue + salto) % 255;
                    red = red + ((green + ((blue + salto) / 255) > 255) ? (green + ((blue + salto) / 255)) / 255 : 0);
                    green = ((green + ((blue + salto) / 255) > 255) ? (green + ((blue + salto) / 255)) % 255 : green + ((blue + salto) / 255));
                }

                labels.Add(i, Color.FromArgb(red, green, blue));
            }

            // Classes de equivalência
            var classes = new List<List<int>>();

            foreach (var item in equivalencia)
            {
                if (classes.FirstOrDefault(x => x.Contains(item[0]) || x.Contains(item[1])) != null)
                {
                    classes.FirstOrDefault(x => x.Contains(item[0]) || x.Contains(item[1]))?.AddRange(item);
                    classes.FirstOrDefault(x => x.Contains(item[0]) || x.Contains(item[1]))?.Distinct();
                }
                else
                    classes.Add(item.ToList());
            }

            // Segunda Varredura
            for (var i = 0; i < newImage.Width; i++)
            {
                for (var j = 0; j < newImage.Height; j++)
                {
                    if (newImage.GetPixel(i, j).R != 255) continue;

                    var classeEquivalente = classes.FirstOrDefault(x => x.Contains(matrizRotulo[i, j]));

                    if (classeEquivalente != null)
                        matrizRotulo[i, j] = classeEquivalente.Min();

                    newImage.SetPixel(i, j, labels[matrizRotulo[i, j]]);
                }
            }

            return newImage;
        }
    }
}
