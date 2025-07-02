using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System;
using TetrisGame.Management;
using TetrisGame.Models;
using TetrisGame.UI;


namespace TetrisGame.UI
{
    public partial class GameView : UserControl, INotifyPropertyChanged
    {
        private GameManager player1Manager;
        private GameManager player2Manager;
        private bool isTwoPlayer;

        public event Action PauseRequested;
        public event Action ResumeRequested;
        public event Action RestartRequested;
        public event Action BackMenuRequested;

        public event PropertyChangedEventHandler PropertyChanged;

        public GameView(GameManager p1Manager, GameManager p2Manager = null)
        {
            InitializeComponent();
            player1Manager = p1Manager;
            player2Manager = p2Manager;
            isTwoPlayer = player2Manager != null;
            this.DataContext = this;
            UpdateView();
        }

        public int Player1Score => player1Manager.Score;
        public Tetromino Player1NextTetromino => player1Manager.NextTetromino;
        public int Player2Score => player2Manager?.Score ?? 0;
        public Tetromino Player2NextTetromino => player2Manager?.NextTetromino;
        public bool IsTwoPlayer => isTwoPlayer;
        public int GameOverScore => player1Manager.IsGameOver ? player1Manager.Score : player2Manager?.Score ?? 0;
        public string Winner { get; private set; }

        public void UpdateView()
        {
            RenderGame(player1Manager, GameCanvas1);
            if (isTwoPlayer)
                RenderGame(player2Manager, GameCanvas2);

            OnPropertyChanged(nameof(Player1Score));
            OnPropertyChanged(nameof(Player2Score));
            OnPropertyChanged(nameof(Player1NextTetromino));
            OnPropertyChanged(nameof(Player2NextTetromino));
            OnPropertyChanged(nameof(GameOverScore));
            OnPropertyChanged(nameof(Winner));
        }

        private void RenderGame(GameManager manager, Canvas canvas)
        {
            canvas.Children.Clear();

            // Draw grid lines
            DrawGridLines(canvas, Board.Rows, Board.Columns);

            // Render the board
            for (int r = 0; r < Board.Rows; r++)
                for (int c = 0; c < Board.Columns; c++)
                {
                    if (manager.Board.Cells[r, c].IsFilled)
                    {
                        Rectangle rect = new Rectangle
                        {
                            Width = 30,
                            Height = 30,
                            Fill = manager.Board.Cells[r, c].Color,
                            Stroke = Brushes.Gray,
                            StrokeThickness = 0.5
                        };
                        Canvas.SetLeft(rect, c * 30);
                        Canvas.SetTop(rect, r * 30);
                        canvas.Children.Add(rect);
                    }
                }

            // Render the current tetromino
            Tetromino current = manager.CurrentTetromino;
            for (int r = 0; r < current.Shape.GetLength(0); r++)
                for (int c = 0; c < current.Shape.GetLength(1); c++)
                {
                    if (current.Shape[r, c])
                    {
                        Rectangle rect = new Rectangle
                        {
                            Width = 30,
                            Height = 30,
                            Fill = current.Color,
                            Stroke = Brushes.Gray,
                            StrokeThickness = 1
                        };
                        Canvas.SetLeft(rect, (current.X + c) * 30);
                        Canvas.SetTop(rect, (current.Y + r) * 30);
                        canvas.Children.Add(rect);
                    }
                }
        }

        private void DrawGridLines(Canvas canvas, int rows, int columns)
        {
            // Draw vertical lines
            for (int c = 0; c <= columns; c++)
            {
                Line vertical = new Line
                {
                    X1 = c * 30,
                    Y1 = 0,
                    X2 = c * 30,
                    Y2 = rows * 30,
                    Stroke = Brushes.Gray,
                    StrokeThickness = 0.5
                };
                canvas.Children.Add(vertical);
            }

            // Draw horizontal lines
            for (int r = 0; r <= rows; r++)
            {
                Line horizontal = new Line
                {
                    X1 = 0,
                    Y1 = r * 30,
                    X2 = columns * 30,
                    Y2 = r * 30,
                    Stroke = Brushes.Gray,
                    StrokeThickness = 0.5
                };
                canvas.Children.Add(horizontal);
            }
        }

        public void ShowPauseMenu()
        {
            Overlay.Visibility = Visibility.Visible;
            PauseMenu.Visibility = Visibility.Visible;
            GameOverMenu.Visibility = Visibility.Collapsed;
        }

        public void HideOverlay()
        {
            Overlay.Visibility = Visibility.Collapsed;
            PauseMenu.Visibility = Visibility.Collapsed;
            GameOverMenu.Visibility = Visibility.Collapsed;
        }

        public void ShowGameOver(string winner)
        {
            Winner = winner;
            Overlay.Visibility = Visibility.Visible;
            PauseMenu.Visibility = Visibility.Collapsed;
            GameOverMenu.Visibility = Visibility.Visible;
            OnPropertyChanged(nameof(GameOverScore));
            OnPropertyChanged(nameof(Winner));
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            PauseRequested?.Invoke();
        }

        private void Resume_Click(object sender, RoutedEventArgs e)
        {
            ResumeRequested?.Invoke();
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            RestartRequested?.Invoke();
        }

        private void BackMenu_Click(object sender, RoutedEventArgs e)
        {
            BackMenuRequested?.Invoke();
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
