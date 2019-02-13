using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processamento_de_Imagens.Code
{
    public static class Matrix
    {
        // Másca Laplaciana 3X3
        public static double[,] Laplacian3X3 =>
            new double[,]
            { { -1, -1, -1,  },
                { -1,  8, -1,  },
                { -1, -1, -1,  }, };

        // Másca Laplaciana 5X5
        public static double[,] Laplacian5X5 =>
            new double[,]
            { { -1, -1, -1, -1, -1, },
                { -1, -1, -1, -1, -1, },
                { -1, -1, 24, -1, -1, },
                { -1, -1, -1, -1, -1, },
                { -1, -1, -1, -1, -1  }, };

        // Másca Laplaciana Gaussiana
        public static double[,] LaplacianOfGaussian =>
            new double[,]
            { {  0,   0, -1,  0,  0 },
                {  0,  -1, -2, -1,  0 },
                { -1,  -2, 16, -2, -1 },
                {  0,  -1, -2, -1,  0 },
                {  0,   0, -1,  0,  0 }, };

        // Másca Gaussiana 3X3
        public static double[,] Gaussian3X3 =>
            new double[,]
            { { 1, 2, 1, },
                { 2, 4, 2, },
                { 1, 2, 1, }, };

        // Másca Gaussiana 5X5
        public static double[,] Gaussian5X5 =>
            new double[,]
            { { 2, 04, 05, 04, 2 },
                { 4, 09, 12, 09, 4 },
                { 5, 12, 15, 12, 5 },
                { 4, 09, 12, 09, 4 },
                { 2, 04, 05, 04, 2 }, };

        // Máscara de Sobel para o eixo horizontal
        public static double[,] Sobel3X3Horizontal =>
            new double[,]
            { { -1,  0,  1, },
                { -2,  0,  2, },
                { -1,  0,  1, }, };

        // Máscara de Sobel para o eixo vertical
        public static double[,] Sobel3X3Vertical =>
            new double[,]
            { {  1,  2,  1, },
                {  0,  0,  0, },
                { -1, -2, -1, }, };
    }
}
