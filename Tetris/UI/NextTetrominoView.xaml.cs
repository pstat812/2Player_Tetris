using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using TetrisGame.Models;

using TetrisGame.UI;


namespace TetrisGame.UI
{
    public partial class NextTetrominoView : UserControl
    {
        public NextTetrominoView()
        {
            InitializeComponent();
            this.DataContextChanged += NextTetrominoView_DataContextChanged;
            this.SizeChanged += NextTetrominoView_SizeChanged;
        }

        private void NextTetrominoView_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            RenderNextTetromino();
        }

        private void NextTetrominoView_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            RenderNextTetromino();
        }

        private void RenderNextTetromino()
        {
            NextCanvas.Children.Clear();
            var tetromino = this.DataContext as Tetromino;
            if (tetromino == null)
                return;

            int shapeRows = tetromino.Shape.GetLength(0);
            int shapeCols = tetromino.Shape.GetLength(1);
            double canvasWidth = NextCanvas.ActualWidth;
            double canvasHeight = NextCanvas.ActualHeight;

            // Calculate the offset to center the tetromino
            double totalWidth = shapeCols * 20;
            double totalHeight = shapeRows * 20;
            double offsetX = (canvasWidth - totalWidth) / 2;
            double offsetY = (canvasHeight - totalHeight) / 2;

            for (int r = 0; r < tetromino.Shape.GetLength(0); r++)
                for (int c = 0; c < tetromino.Shape.GetLength(1); c++)
                {
                    if (tetromino.Shape[r, c])
                    {
                        Rectangle rect = new Rectangle
                        {
                            Width = 20,
                            Height = 20,
                            Fill = tetromino.Color,
                            Stroke = Brushes.White,
                            StrokeThickness = 1
                        };
                        Canvas.SetLeft(rect, c * 20 + offsetX);
                        Canvas.SetTop(rect, r * 20 + offsetY);
                        NextCanvas.Children.Add(rect);
                    }
                }
        }
    }
}
