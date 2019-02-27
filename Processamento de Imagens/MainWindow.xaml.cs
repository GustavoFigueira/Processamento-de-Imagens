using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Windows.Input;
using Processamento_de_Imagens.Code;
using Cursors = System.Windows.Forms.Cursors;

namespace Processamento_de_Imagens
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Botão que limpa os campos
        private void Clean_Click(object sender, RoutedEventArgs e)
        {
            // Limpa o campo de valor de redimensionamento
            Amount.Text = string.Empty;
            // Remove a imagem do Antes
            ImageViewerBefore.Source = null;
            // Remove a imagem do Depois
            ImageViewerAfter.Source = null;
        }

        // Botão que gera o algoritmo
        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            // Inicializa o selecionador de arquivos
            var fileDialog = new OpenFileDialog
            {
                Title = "Selecione uma imagem",
                Filter = "Formatos suportados|*.jpg;*.jpeg;*.png|" +
                         "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                         "Portable Network Graphic (*.png)|*.png",
                InitialDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent?.FullName + "\\Examples"
            };

            // Retorna se nada for selecionado
            if (fileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            // Caso selecionado, instnacia imagem a partir do arquivo
            var image = new Bitmap(fileDialog.FileName);

            // Converte o Bitmap para imagem e a exibe antes da transformação
            ImageViewerBefore.Source = Util.ConvertBitmapToImage(image);

            // Tenta converter o campo para inteiro para obter valor de redimensionamento, se não usa 2x como padrão
            int amountValue = int.TryParse(Amount.Text, out amountValue) ? amountValue : 50;

            // Obtêm o algoritmo selecionado 
            var nn = NearestNeighbor.IsChecked != null && NearestNeighbor.IsChecked.Value;

            // Inicializa o loading
            Generate.Content = "Aguarde...";
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

            try
            {
                // Obtêm o modo selecionado
                if (Option.Text == "Ampliação")
                {
                    // Aplica o redimensionamento dependendo do algoritmo selecionado
                    image = nn
                        ? Algorithms.NearestNeighbor(image, amountValue, Util.Option.Enlarge)
                        : Algorithms.Bilinear(image, amountValue, Util.Option.Enlarge);
                }
                else if (Option.Text == "Redução")
                {
                    // Aplica o redimensionamento dependendo do algoritmo selecionado
                    image = nn
                        ? Algorithms.NearestNeighbor(image, amountValue, Util.Option.Reduce)
                        : Algorithms.Bilinear(image, amountValue, Util.Option.Reduce);
                }
                else if (Option.Text == "Equalização Histograma")
                {
                    // Aplica a Equalização Histograma (tende à colocar realçar as cores mais centrais do histograma - ou a 'barriga' - do gráfico)
                    image = Algorithms.HistogramEqualization(image);
                }
                else if (Option.Text == "Transformação de Intensidade (Limiarização)")
                {
                    // Aplica a Limiariazação e inverte as cores, caso selecionado
                    image = Algorithms.Thresholding(image, InvertThreshold.IsChecked.GetValueOrDefault());
                }
                else if (Option.Text == "Transformação de Intensidade (Negativo)")
                {
                    image = Algorithms.Negative(image);
                }
                else if (Option.Text == "Operação Aritmética (Adição)")
                {
                    // Inicializa o selecionador de arquivos da segunda imagem
                    var secondFileDialog = new OpenFileDialog
                    {
                        Title = "Selecione a segunda imagem de sobreposição",
                        Filter = "Formatos suportados|*.jpg;*.jpeg;*.png|" +
                                 "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                 "Portable Network Graphic (*.png)|*.png",
                        InitialDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent?.FullName + "\\Examples"
                    };


                    // Retorna se nada for selecionado
                    if (secondFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

                    // Caso selecionado, instnacia imagem a partir do arquivo
                    var blendingImage = new Bitmap(secondFileDialog.FileName);

                    image = Algorithms.AdditionBlendMode(image, blendingImage);
                }
                else if (Option.Text == "Operação Aritmética (Subtração)")
                {
                    // Inicializa o selecionador de arquivos da segunda imagem
                    var secondFileDialog = new OpenFileDialog
                    {
                        Title = "Selecione a segunda imagem de sobreposição",
                        Filter = "Formatos suportados|*.jpg;*.jpeg;*.png|" +
                                 "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                 "Portable Network Graphic (*.png)|*.png",
                        InitialDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent?.FullName + "\\Examples"
                    };

                    // Retorna se nada for selecionado
                    if (secondFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

                    // Caso selecionado, instnacia imagem a partir do arquivo
                    var blendingImage = new Bitmap(secondFileDialog.FileName);

                    image = Algorithms.SubtractBlendMode(image, blendingImage);
                }
                else if (Option.Text == "Operação Geométrica (Rotação)")
                {
                    // Aplica um Rotação de 90° na imagem
                    image = Algorithms.Rotation(image);
                }
                else if (Option.Text == "Rotulação")
                {
                    image = Algorithms.Rotulation(image);
                }
                else if (Option.Text == "Filtro de Média")
                {
                    // Tamanho da Matriz de Máscara
                    var medianFilterMatrix = 3;

                    int.TryParse(MedianFilterMatrix.Text, out medianFilterMatrix);

                    // Filtro de Média com matriz 3X3
                    image = Algorithms.MedianFilter(image, medianFilterMatrix);
                }
                else if (Option.Text == "Filtro Laplaciano")
                {
                    // Obtêm a opção de Filtro Laplaciano selecionada
                    var laplacianOption = LaplacianOption.Text;

                    if (laplacianOption == "3X3")
                    {
                        image = Algorithms.Laplacian3X3Filter(image);
                    }
                    else if (laplacianOption == "5X5")
                    {
                        image = Algorithms.Laplacian5X5Filter(image);
                    }
                    else if (laplacianOption == "Gaussiano")
                    {
                        image = Algorithms.LaplacianOfGaussianFilter(image);
                    }
                    else if (laplacianOption == "3X3 e 5X5")
                    {
                        image = Algorithms.Laplacian3X3OfGaussian3X3Filter(image);
                    }
                }
                else if (Option.Text == "Filtro Gradiente (Sobel)")
                {
                    image = Algorithms.GradientSobelFilter(image);
                }
                else if (Option.Text == "Dilatação")
                {
                    image = Algorithms.DilateAndErodeFilter(image, 9, Algorithms.MorphologyType.Dilation, false, false, true);
                }
                else if (Option.Text == "Erosão")
                {
                    image = Algorithms.DilateAndErodeFilter(image, 9, Algorithms.MorphologyType.Erosion, false, false, true);
                }
                else if (Option.Text == "Abertura")
                {
                    image = Algorithms.OpenMorphologyFilter(image, 9, false, false, true);
                }
                else if (Option.Text == "Fechamento")
                {
                    image = Algorithms.CloseMorphologyFilter(image, 9, false, false, true);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

            // Exibe a imagem final
            ImageViewerAfter.Source = Util.ConvertBitmapToImage(image);

            // Desabilita o loading
            Generate.Content = "Gerar";
            System.Windows.Forms.Cursor.Current = Cursors.Default;

            // Salva a imagem final no computador
            Util.SaveImage(image, Option.Text);
        }

        // Evento que expande os sub-menus(caso haja) do Algoritmo selecionado
        private void Option_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Scaling.Visibility = (Option.SelectedItem.ToString().Contains("Ampliação") || Option.SelectedItem.ToString().Contains("Redução")) ? Visibility.Visible : Visibility.Hidden;
            Thresholding.Visibility = Option.SelectedItem.ToString().Contains("Transformação de Intensidade (Limiarização)") ? Visibility.Visible : Visibility.Hidden;
            MedianFilter.Visibility = Option.SelectedItem.ToString().Contains("Filtro de Média") ? Visibility.Visible : Visibility.Hidden;
            LaplacianFilter.Visibility = Option.SelectedItem.ToString().Contains("Filtro Laplaciano") ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
