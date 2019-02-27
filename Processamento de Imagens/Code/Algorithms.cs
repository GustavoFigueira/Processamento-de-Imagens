using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
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

            // Bytes auxiliares
            var bytes = sd.Stride * sd.Height;
            var buffer = new byte[bytes];
            var result = new byte[bytes];

            Marshal.Copy(sd.Scan0, buffer, 0, bytes);
            originalImage.UnlockBits(sd);

            // Novo array RGB dos bytes
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

            // Monta a imagem final a partir dos bytes
            var rd = newImage.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            // Gera a imagem a partir dos bytes manipulados
            Marshal.Copy(result, 0, rd.Scan0, bytes);
            newImage.UnlockBits(rd);

            // Retorna a imagem final
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
                    newImage.SetPixel(x, y, originalImage.GetPixel(x, y).GetBrightness() <= 0.5f ? (invert ? Color.Black : Color.White) : (invert ? Color.White : Color.Black));

            return newImage;
        }

        // Transformação de Intensidade (Negativo)
        public static Bitmap Negative(Bitmap originalImage)
        {
            //Tamanhos da imagem
            var w = originalImage.Width;
            var h = originalImage.Height;

            // Inicializa o array de bytes a partir da imagem original
            var srcData = originalImage.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            // Variáveis auxiliares
            var bytes = srcData.Stride * srcData.Height;
            var buffer = new byte[bytes];
            var result = new byte[bytes];

            Marshal.Copy(srcData.Scan0, buffer, 0, bytes);
            originalImage.UnlockBits(srcData);

            // Os três canais de cor (RGB)
            const int channels = 3;

            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
                {
                    var current = y * srcData.Stride + x * 4;

                    for (var c = 0; c < channels; c++)
                        result[current + c] = (byte)(255 - buffer[current + c]);

                    result[current + 3] = 255;
                }
            }

            // Monta a imagem final a partir dos bytes
            var newImage = new Bitmap(w, h);
            var bytesData = newImage.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            // Gera a imagem a partir dos bytes manipulados
            Marshal.Copy(result, 0, bytesData.Scan0, bytes);
            newImage.UnlockBits(bytesData);

            // Retorna a imagem final
            return newImage;
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

            for (var x = 0; x < originalImage.Height; x++)
                for (var y = 0; y < originalImage.Width; y++)
                    newImage.SetPixel(x, y, originalImage.GetPixel(y, originalImage.Height - x - 1));

            return newImage;
        }

        // Rotulação
        public static Bitmap Rotulation(Bitmap originalImage)
        {
            // Instância da Imagem final convertida em tons de cinza
            var newImage = ConvertBitmapToGrayScale(originalImage);

            // Variáveis auxiliares
            var equivalent = new List<int[]>();
            var labelColors = new List<int>();
            var labelImage = new int[originalImage.Width, originalImage.Height];

            // Binariza a imagem ente branco e preto
            for (var i = 0; i < originalImage.Width; i++)
            {
                for (var j = 0; j < originalImage.Height; j++)
                {
                    newImage.SetPixel(i, j, (newImage.GetPixel(i, j).R <= 127) ? Color.Black : Color.White);
                    //labelImage[i, j] = labelColors.LastOrDefault();
                }
            }
            
            // Primeira Varredura
            for (var i = 0; i < newImage.Width; i++)
            {
                for (var j = 0; j < newImage.Height; j++)
                {
                    if (newImage.GetPixel(i, j).R != 255) continue;

                    var r = (i > 0) ? (newImage.GetPixel(i - 1, j)) : Color.Black;
                    var s = (j > 0) ? (newImage.GetPixel(i, j - 1)) : Color.Black;

                    var rIsValid = r.R == 0 || r == Color.Black;
                    var sIsValid = s.R == 0 || s == Color.Black;

                    if (!rIsValid || !sIsValid)
                    {
                        if ((rIsValid) || (sIsValid))
                            labelImage[i, j] = (rIsValid) ? labelImage[i, j - 1] : labelImage[i - 1, j];
                        else if ((labelImage[i - 1, j] == labelImage[i, j - 1]))
                            labelImage[i, j] = labelImage[i, j - 1];
                        else if ((labelImage[i - 1, j] != labelImage[i, j - 1]))
                        {
                            labelImage[i, j] = Math.Min(labelImage[i, j - 1], labelImage[i - 1, j]);
                            equivalent.Add(new[] { labelImage[i, j - 1], labelImage[i - 1, j] });
                        }
                    }
                    else
                    {
                        labelColors.Add(labelColors.LastOrDefault() + 1);
                        labelImage[i, j] = labelColors.LastOrDefault();
                    }
                }
            }

            // * Cor que será o incremento *
            // Resultado da divisão entre a Constante das cores totais RGB (255 x 255 x 255 = 16581375) pela quantidade de rótulos de cor encontrados
            const int totalRgb = 16581375;
            var colorIncrement = totalRgb / labelColors.Count;

            // Novo dicionários com as cores e seu valor RGB em inteiro
            var labels = new Dictionary<int, Color>();

            var red = 0;
            var green = 0;
            var blue = 0;

            for (var i = 0; i < labelColors.Count; i++)
            {
                if (colorIncrement > 255)
                {
                    // Assina as novas cores dos canais
                    blue = (blue + colorIncrement) % 255;
                    red = red + (green + ((blue + colorIncrement) / 255) > 255 ? (green + (blue + colorIncrement) / 255) / 255 : 0);
                    green = green + ((blue + colorIncrement) / 255) > 255
                        ? (green + (blue + colorIncrement) / 255) % 255
                        : green + (blue + colorIncrement) / 255;
                }
                else
                {
                    blue += colorIncrement;

                    if (blue > 255)
                    {
                        blue = 255 - colorIncrement;
                        green++;

                        if (green > 255)
                        {
                            green = 0;
                            red++;
                        }
                    }
                }

                // Cria uma nova label a partir dos 3 canais de cor
                labels.Add(i, Color.FromArgb(red, green, blue));
            }

            // Labels Equivalentes
            var equal = new List<List<int>>();

            foreach (var item in equivalent)
            {
                if (equal.FirstOrDefault(x => x.Contains(item[0]) || x.Contains(item[1])) == null)
                    equal.Add(item.ToList());
                else
                    equal.FirstOrDefault(x => x.Contains(item[0]) || x.Contains(item[1]))?.AddRange(item);
            }

            // Segunda Varredura (define os novos pixels na nova imagem)
            for (var i = 0; i < newImage.Width; i++)
            {
                for (var j = 0; j < newImage.Height; j++)
                {
                    if (newImage.GetPixel(i, j).R != 255) continue;

                    var classeEquivalente = equal.Distinct().FirstOrDefault(x => x.Contains(labelImage[i, j]));

                    if (classeEquivalente != null)
                        labelImage[i, j] = classeEquivalente.Min();

                    newImage.SetPixel(i, j, labels[labelImage[i, j]]);
                }
            }

            // Retorna a imagem final
            return newImage;
        }

        #region Filtros

        // Filtro de Média
        // Aplica-se pixels pretos nos contornos como alternativa para tratar a borda
        public static Bitmap MedianFilter(this Bitmap sourceBitmap, int matrixSize, int bias = 0, bool grayscale = false)
        {
            var sourceData = sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            var pixelBuffer = new byte[sourceData.Stride * sourceData.Height];

            var resultBuffer = new byte[sourceData.Stride * sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);

            sourceBitmap.UnlockBits(sourceData);

            if (grayscale)
            {
                for (var k = 0; k < pixelBuffer.Length; k += 4)
                {
                    var rgb = pixelBuffer[k] * 0.11f;
                    rgb += pixelBuffer[k + 1] * 0.59f;
                    rgb += pixelBuffer[k + 2] * 0.3f;


                    pixelBuffer[k] = (byte)rgb;
                    pixelBuffer[k + 1] = pixelBuffer[k];
                    pixelBuffer[k + 2] = pixelBuffer[k];
                    pixelBuffer[k + 3] = 255;
                }
            }

            var filterOffset = (matrixSize - 1) / 2;

            var neighbourPixels = new List<int>();

            for (var offsetY = filterOffset; offsetY < sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (var offsetX = filterOffset; offsetX < sourceBitmap.Width - filterOffset; offsetX++)
                {
                    var byteOffset = offsetY * sourceData.Stride + offsetX * 4;

                    neighbourPixels.Clear();

                    for (var filterY = -filterOffset; filterY <= filterOffset; filterY++)
                    {
                        for (var filterX = -filterOffset; filterX <= filterOffset; filterX++)
                        {
                            var calcOffset = byteOffset + (filterX * 4) + (filterY * sourceData.Stride);

                            neighbourPixels.Add(BitConverter.ToInt32(pixelBuffer, calcOffset));
                        }
                    }

                    neighbourPixels.Sort();

                    var middlePixel = BitConverter.GetBytes(neighbourPixels[filterOffset]);

                    resultBuffer[byteOffset] = middlePixel[0];
                    resultBuffer[byteOffset + 1] = middlePixel[1];
                    resultBuffer[byteOffset + 2] = middlePixel[2];
                    resultBuffer[byteOffset + 3] = middlePixel[3];
                }
            }

            var resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            var resultData = resultBitmap.LockBits(new Rectangle(0, 0, resultBitmap.Width, resultBitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);

            resultBitmap.UnlockBits(resultData);

            // Retorna a imagem final
            return resultBitmap;
        }

        // Filtro Gradiente (Máscara de Sobel 3X3)
        public static Bitmap GradientSobelFilter(Bitmap originalImage, int intensity = 1)
        {
            // Aplica o filtro usando a função de convolulação passando a matriz de sobel
            var resultImage = ConvolutionFilter(originalImage, Matrix.Sobel3X3Horizontal, Matrix.Sobel3X3Vertical, intensity, 0, false);

            // Retorna a imagem final
            return resultImage;
        }

        // Filtro de Convolução
        private static Bitmap ConvolutionFilter(Bitmap sourceBitmap, double[,] filterMatrix, double factor = 1, int bias = 0, bool grayscale = false)
        {
            var sourceData = sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            var pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
            var resultBuffer = new byte[sourceData.Stride * sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            sourceBitmap.UnlockBits(sourceData);

            if (grayscale)
            {
                for (var k = 0; k < pixelBuffer.Length; k += 4)
                {
                    var rgb = pixelBuffer[k] * 0.11f;
                    rgb += pixelBuffer[k + 1] * 0.59f;
                    rgb += pixelBuffer[k + 2] * 0.3f;


                    pixelBuffer[k] = (byte)rgb;
                    pixelBuffer[k + 1] = pixelBuffer[k];
                    pixelBuffer[k + 2] = pixelBuffer[k];
                    pixelBuffer[k + 3] = 255;
                }
            }

            var filterWidth = filterMatrix.GetLength(1);

            var filterOffset = (filterWidth - 1) / 2;

            for (var offsetY = filterOffset; offsetY < sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (var offsetX = filterOffset; offsetX < sourceBitmap.Width - filterOffset; offsetX++)
                {
                    double blue = 0;
                    double green = 0;
                    double red = 0;

                    var byteOffset = offsetY * sourceData.Stride + offsetX * 4;

                    for (var filterY = -filterOffset; filterY <= filterOffset; filterY++)
                    {
                        for (var filterX = -filterOffset; filterX <= filterOffset; filterX++)
                        {

                            var calcOffset = byteOffset + (filterX * 4) + (filterY * sourceData.Stride);

                            blue += (double)(pixelBuffer[calcOffset]) * filterMatrix[filterY + filterOffset, filterX + filterOffset];

                            green += (double)(pixelBuffer[calcOffset + 1]) * filterMatrix[filterY + filterOffset, filterX + filterOffset];

                            red += (double)(pixelBuffer[calcOffset + 2]) * filterMatrix[filterY + filterOffset, filterX + filterOffset];
                        }
                    }

                    blue = factor * blue + bias;
                    green = factor * green + bias;
                    red = factor * red + bias;

                    if (blue > 255)
                    { blue = 255; }
                    else if (blue < 0)
                    { blue = 0; }

                    if (green > 255)
                    { green = 255; }
                    else if (green < 0)
                    { green = 0; }

                    if (red > 255)
                    { red = 255; }
                    else if (red < 0)
                    { red = 0; }

                    resultBuffer[byteOffset] = (byte)(blue);
                    resultBuffer[byteOffset + 1] = (byte)(green);
                    resultBuffer[byteOffset + 2] = (byte)(red);
                    resultBuffer[byteOffset + 3] = 255;
                }
            }

            var resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            var resultData = resultBitmap.LockBits(new Rectangle(0, 0, resultBitmap.Width, resultBitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }

        // Filtro de Convolução
        public static Bitmap ConvolutionFilter(this Bitmap sourceBitmap, double[,] xFilterMatrix, double[,] yFilterMatrix, double factor = 1, int bias = 0, bool grayscale = false)
        {
            var sourceData = sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            var pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
            var resultBuffer = new byte[sourceData.Stride * sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            sourceBitmap.UnlockBits(sourceData);

            if (grayscale)
            {
                for (var k = 0; k < pixelBuffer.Length; k += 4)
                {
                    var rgb = pixelBuffer[k] * 0.11f;
                    rgb += pixelBuffer[k + 1] * 0.59f;
                    rgb += pixelBuffer[k + 2] * 0.3f;

                    pixelBuffer[k] = (byte)rgb;
                    pixelBuffer[k + 1] = pixelBuffer[k];
                    pixelBuffer[k + 2] = pixelBuffer[k];
                    pixelBuffer[k + 3] = 255;
                }
            }

            var filterOffset = 1;

            for (var offsetY = filterOffset; offsetY < sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (var offsetX = filterOffset; offsetX < sourceBitmap.Width - filterOffset; offsetX++)
                {
                    var greenX = 0.0;
                    var redX = 0.0;
                    var blueX = greenX = redX = 0;
                    var greenY = 0.0;
                    var redY = 0.0;
                    var blueY = greenY = redY = 0;

                    var greenTotal = 0.0;
                    var redTotal = 0.0;
                    var blueTotal = greenTotal = redTotal = 0.0;

                    var byteOffset = offsetY * sourceData.Stride + offsetX * 4;

                    for (var filterY = -filterOffset; filterY <= filterOffset; filterY++)
                    {
                        for (var filterX = -filterOffset; filterX <= filterOffset; filterX++)
                        {
                            var calcOffset = byteOffset +
                                             (filterX * 4) +
                                             (filterY * sourceData.Stride);

                            blueX += (double)(pixelBuffer[calcOffset]) *
                                      xFilterMatrix[filterY + filterOffset,
                                              filterX + filterOffset];

                            greenX += (double)(pixelBuffer[calcOffset + 1]) *
                                      xFilterMatrix[filterY + filterOffset,
                                              filterX + filterOffset];

                            redX += (double)(pixelBuffer[calcOffset + 2]) *
                                      xFilterMatrix[filterY + filterOffset,
                                              filterX + filterOffset];

                            blueY += (double)(pixelBuffer[calcOffset]) *
                                      yFilterMatrix[filterY + filterOffset,
                                              filterX + filterOffset];

                            greenY += (double)(pixelBuffer[calcOffset + 1]) *
                                      yFilterMatrix[filterY + filterOffset,
                                              filterX + filterOffset];

                            redY += (double)(pixelBuffer[calcOffset + 2]) *
                                      yFilterMatrix[filterY + filterOffset,
                                              filterX + filterOffset];
                        }
                    }

                    blueTotal = Math.Sqrt((blueX * blueX) + (blueY * blueY));
                    greenTotal = Math.Sqrt((greenX * greenX) + (greenY * greenY));
                    redTotal = Math.Sqrt((redX * redX) + (redY * redY));

                    if (blueTotal > 255)
                    { blueTotal = 255; }
                    else if (blueTotal < 0)
                    { blueTotal = 0; }

                    if (greenTotal > 255)
                    { greenTotal = 255; }
                    else if (greenTotal < 0)
                    { greenTotal = 0; }

                    if (redTotal > 255)
                    { redTotal = 255; }
                    else if (redTotal < 0)
                    { redTotal = 0; }

                    resultBuffer[byteOffset] = (byte)(blueTotal);
                    resultBuffer[byteOffset + 1] = (byte)(greenTotal);
                    resultBuffer[byteOffset + 2] = (byte)(redTotal);
                    resultBuffer[byteOffset + 3] = 255;
                }
            }

            var resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            var resultData = resultBitmap.LockBits(new Rectangle(0, 0, resultBitmap.Width, resultBitmap.Height), ImageLockMode.WriteOnly,PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            // Retorna a imagem final
            return resultBitmap;
        }

        // Filtro Laplaciano 3X3
        public static Bitmap Laplacian3X3Filter(this Bitmap sourceBitmap)
        {
            var resultBitmap = ConvolutionFilter(sourceBitmap, Matrix.Laplacian3X3, 1.0, 0, true);

            return resultBitmap;
        }

        // Filtro Laplaciano 5X5
        public static Bitmap Laplacian5X5Filter(this Bitmap sourceBitmap)
        {
            var resultBitmap = ConvolutionFilter(sourceBitmap, Matrix.Laplacian5X5, 1.0, 0, true);

            return resultBitmap;
        }

        // Filtro Laplaciano Gaussiano
        public static Bitmap LaplacianOfGaussianFilter(this Bitmap sourceBitmap)
        {
            var resultBitmap = ConvolutionFilter(sourceBitmap, Matrix.LaplacianOfGaussian, 1.0, 0, true);

            return resultBitmap;
        }

        // Filtro Laplaciano 3X3 e 5X5
        public static Bitmap Laplacian3X3OfGaussian3X3Filter(this Bitmap sourceBitmap)
        {
            var resultBitmap = ConvolutionFilter(sourceBitmap, Matrix.Gaussian3X3, 1.0 / 16.0, 0, true);

            resultBitmap = ConvolutionFilter(resultBitmap, Matrix.Laplacian3X3, 1.0, 0, false);

            return resultBitmap;
        }

        #endregion

        #region Morfologia Matematica

        public enum MorphologyType
        {
            Dilation,
            Erosion
        }

        // Dilatação e Erosão
        public static Bitmap DilateAndErodeFilter(this Bitmap sourceBitmap, int matrixSize, MorphologyType morphType, bool applyBlue = true, bool applyGreen = true, bool applyRed = true)
        {
            var sourceData = sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            var pixelBuffer = new byte[sourceData.Stride * sourceData.Height];

            var resultBuffer = new byte[sourceData.Stride * sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);

            sourceBitmap.UnlockBits(sourceData);

            int filterOffset = (matrixSize - 1) / 2;
            int calcOffset = 0;

            int byteOffset = 0;

            byte blue = 0;
            byte green = 0;
            byte red = 0;

            byte morphResetValue = 0;

            if (morphType == MorphologyType.Erosion)
                morphResetValue = 255;

            for (int offsetY = filterOffset; offsetY < sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX < sourceBitmap.Width - filterOffset; offsetX++)
                {
                    byteOffset = offsetY * sourceData.Stride + offsetX * 4;

                    blue = morphResetValue;
                    green = morphResetValue;
                    red = morphResetValue;

                    if (morphType == MorphologyType.Dilation)
                    {
                        for (int filterY = -filterOffset;filterY <= filterOffset; filterY++)
                        {
                            for (int filterX = -filterOffset; filterX <= filterOffset; filterX++)
                            {
                                calcOffset = byteOffset + (filterX * 4) + (filterY * sourceData.Stride);

                                if (pixelBuffer[calcOffset] > blue)
                                    blue = pixelBuffer[calcOffset];

                                if (pixelBuffer[calcOffset + 1] > green)
                                    green = pixelBuffer[calcOffset + 1];

                                if (pixelBuffer[calcOffset + 2] > red)
                                    red = pixelBuffer[calcOffset + 2];
                            }
                        }
                    }
                    else if (morphType == MorphologyType.Erosion)
                    {
                        for (int filterY = -filterOffset; filterY <= filterOffset; filterY++)
                        {
                            for (int filterX = -filterOffset; filterX <= filterOffset; filterX++)
                            {
                                calcOffset = byteOffset + (filterX * 4) + (filterY * sourceData.Stride);

                                if (pixelBuffer[calcOffset] < blue)
                                    blue = pixelBuffer[calcOffset];

                                if (pixelBuffer[calcOffset + 1] < green)
                                    green = pixelBuffer[calcOffset + 1];

                                if (pixelBuffer[calcOffset + 2] < red)
                                    red = pixelBuffer[calcOffset + 2];
                            }
                        }
                    }

                    if (!applyBlue)
                        blue = pixelBuffer[byteOffset];

                    if (!applyGreen)
                        green = pixelBuffer[byteOffset + 1];

                    if (!applyRed)
                        red = pixelBuffer[byteOffset + 2];

                    resultBuffer[byteOffset] = blue;
                    resultBuffer[byteOffset + 1] = green;
                    resultBuffer[byteOffset + 2] = red;
                    resultBuffer[byteOffset + 3] = 255;
                }
            }

            var resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            var resultData = resultBitmap.LockBits(new Rectangle(0, 0, resultBitmap.Width, resultBitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);

            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }

        // Abertura
        public static Bitmap OpenMorphologyFilter(this Bitmap sourceBitmap, int matrixSize, bool applyBlue = true, bool applyGreen = true, bool applyRed = true)
        {
            var resultBitmap = sourceBitmap.DilateAndErodeFilter(matrixSize, MorphologyType.Erosion, applyBlue, applyGreen, applyRed);

            resultBitmap = resultBitmap.DilateAndErodeFilter(matrixSize, MorphologyType.Dilation,applyBlue, applyGreen, applyRed);

            return resultBitmap;
        }

        // Fechamento
        public static Bitmap CloseMorphologyFilter(this Bitmap sourceBitmap, int matrixSize, bool applyBlue = true, bool applyGreen = true, bool applyRed = true)
        {
            var resultBitmap = sourceBitmap.DilateAndErodeFilter(matrixSize, MorphologyType.Dilation, applyBlue, applyGreen, applyRed);

            resultBitmap = resultBitmap.DilateAndErodeFilter(matrixSize, MorphologyType.Erosion, applyBlue, applyGreen, applyRed);

            return resultBitmap;
        }

        #endregion

    }
}