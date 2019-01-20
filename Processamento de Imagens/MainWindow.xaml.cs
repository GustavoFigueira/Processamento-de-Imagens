using System.Windows;
using System.Windows.Forms;
using System.Drawing;
using Processamento_de_Imagens.Code;

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
                         "Portable Network Graphic (*.png)|*.png"
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
                image = Algorithms.HistogramEqualization(image);
            }
            else if (Option.Text == "Transformação de Intensidade (Contrast Stretching)")
            {
                image = Algorithms.ContrastStretching(image);
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
                             "Portable Network Graphic (*.png)|*.png"
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
                             "Portable Network Graphic (*.png)|*.png"
                };

                // Retorna se nada for selecionado
                if (secondFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

                // Caso selecionado, instnacia imagem a partir do arquivo
                var blendingImage = new Bitmap(secondFileDialog.FileName);

                image = Algorithms.SubtractBlendMode(image, blendingImage);
            }
            else if (Option.Text == "Operação Geométrica (Rotação)")
            {
                image = Algorithms.Rotation(image);
            }

            // Exibe a imagem final
            ImageViewerAfter.Source = Util.ConvertBitmapToImage(image);

            // Salva a imagem final no computador
            Util.SaveImage(image, Option.Text);
        }

        private void Option_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Scaling.Visibility = (Option.SelectedItem.ToString().Contains("Ampliação") || Option.SelectedItem.ToString().Contains("Redução")) ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
