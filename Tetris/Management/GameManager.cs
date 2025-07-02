using System;
using TetrisGame.Models;
using System.Timers;

namespace TetrisGame.Management
{
    public class GameManager
    {
        public Board Board { get; private set; }
        public int Score { get; set; }
        public Tetromino CurrentTetromino { get; set; }
        public Tetromino NextTetromino { get; set; }
        public bool IsGameOver { get; private set; }

        private ITetrominoFactory tetrominoFactory;
        private Timer delayTimer;

        public GameManager()
        {
            tetrominoFactory = new TetrominoFactory();
            Initialize();
        }

        public void Initialize()
        {
            Board = new Board();
            Score = 0;
            IsGameOver = false;
            CurrentTetromino = tetrominoFactory.CreateTetromino();
            NextTetromino = tetrominoFactory.CreateTetromino();
            CurrentTetromino.X = (Board.Columns / 2) - (CurrentTetromino.Shape.GetLength(1) / 2);
            CurrentTetromino.Y = 0;

            if (IsCollision())
            {
                IsGameOver = true;
            }
        }

        public void Reset()
        {
            InitializeReset();

            delayTimer = new Timer(1000);
            delayTimer.AutoReset = false;
            delayTimer.Elapsed += (sender, e) =>
            {
                InitializeAfterDelay();
                delayTimer.Dispose();
            };
            delayTimer.Start();
        }

        private void InitializeReset()
        {
            Board = new Board();
            Score = 0;
            IsGameOver = false;
            CurrentTetromino = tetrominoFactory.CreateTetromino();
            NextTetromino = tetrominoFactory.CreateTetromino();
            CurrentTetromino.X = (Board.Columns / 2) - (CurrentTetromino.Shape.GetLength(1) / 2);
            CurrentTetromino.Y = -1;

            if (IsCollision())
            {
                IsGameOver = true;
            }
        }

        private void InitializeAfterDelay()
        {
            if (!IsCollision())
            {
                // Ready to spawn next tetromino
            }
            else
            {
                IsGameOver = true;
            }
        }

        public void MoveLeft()
        {
            if (IsGameOver) return;
            CurrentTetromino.X -= 1;
            if (IsCollision())
                CurrentTetromino.X += 1;
        }

        public void MoveRight()
        {
            if (IsGameOver) return;
            CurrentTetromino.X += 1;
            if (IsCollision())
                CurrentTetromino.X -= 1;
        }

        public void RotateTetromino()
        {
            if (IsGameOver) return;
            CurrentTetromino.RotateClockwise();
            if (IsCollision())
                CurrentTetromino.RotateCounterClockwise();
        }

        public void DropTetromino()
        {
            if (IsGameOver) return;
            CurrentTetromino.Y += 1;
            if (IsCollision())
            {
                CurrentTetromino.Y -= 1;
                LandTetromino();
            }
        }

        public void HardDropTetromino()
        {
            if (IsGameOver) return;
            while (!IsCollision())
            {
                CurrentTetromino.Y += 1;
            }
            CurrentTetromino.Y -= 1;
            LandTetromino();
        }

        private bool IsCollision()
        {
            for (int r = 0; r < CurrentTetromino.Shape.GetLength(0); r++)
                for (int c = 0; c < CurrentTetromino.Shape.GetLength(1); c++)
                {
                    if (CurrentTetromino.Shape[r, c])
                    {
                        int x = CurrentTetromino.X + c;
                        int y = CurrentTetromino.Y + r;

                        if (x < 0 || x >= Board.Columns || y >= Board.Rows)
                            return true;

                        if (y >= 0 && Board.IsCellOccupied(x, y))
                            return true;
                    }
                }
            return false;
        }

        private void LandTetromino()
        {
            for (int r = 0; r < CurrentTetromino.Shape.GetLength(0); r++)
                for (int c = 0; c < CurrentTetromino.Shape.GetLength(1); c++)
                {
                    if (CurrentTetromino.Shape[r, c])
                    {
                        int x = CurrentTetromino.X + c;
                        int y = CurrentTetromino.Y + r;
                        Board.OccupyCell(x, y, CurrentTetromino.Color);
                    }
                }

            int linesCleared = Board.ClearFullLines();
            if (linesCleared > 0)
                Score += 100 * (int)Math.Pow(2, linesCleared);

            CurrentTetromino = NextTetromino.Clone();
            NextTetromino = tetrominoFactory.CreateTetromino();
            CurrentTetromino.X = (Board.Columns / 2) - (CurrentTetromino.Shape.GetLength(1) / 2);
            CurrentTetromino.Y = 0;

            if (IsCollision())
            {
                IsGameOver = true;
            }
        }
    }
}
