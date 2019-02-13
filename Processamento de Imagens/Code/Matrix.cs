using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processamento_de_Imagens.Code
{
    public static class Matrix
    {
        // Máscara Laplaciana 3X3
        public static double[,] Laplacian3X3 =>
            new double[,]
            { { -1, -1, -1,  },
                { -1,  8, -1,  },
                { -1, -1, -1,  }, };

        // Máscara Laplaciana 5X5
        public static double[,] Laplacian5X5 =>
            new double[,]
            { { -1, -1, -1, -1, -1, },
                { -1, -1, -1, -1, -1, },
                { -1, -1, 24, -1, -1, },
                { -1, -1, -1, -1, -1, },
                { -1, -1, -1, -1, -1  }, };

        // Máscara Laplaciana Gaussiana
        public static double[,] LaplacianOfGaussian =>
            new double[,]
            { {  0,   0, -1,  0,  0 },
                {  0,  -1, -2, -1,  0 },
                { -1,  -2, 16, -2, -1 },
                {  0,  -1, -2, -1,  0 },
                {  0,   0, -1,  0,  0 }, };

        // Máscara Gaussiana 3X3
        public static double[,] Gaussian3X3 =>
            new double[,]
            { { 1, 2, 1, },
                { 2, 4, 2, },
                { 1, 2, 1, }, };

        // Máscarara de Sobel para o eixo horizontal
        public static double[,] Sobel3X3Horizontal =>
            new double[,]
            { { -1,  0,  1, },
                { -2,  0,  2, },
                { -1,  0,  1, }, };

        // Máscarara de Sobel para o eixo vertical
        public static double[,] Sobel3X3Vertical =>
            new double[,]
            { {  1,  2,  1, },
                {  0,  0,  0, },
                { -1, -2, -1, }, };
    }
}
