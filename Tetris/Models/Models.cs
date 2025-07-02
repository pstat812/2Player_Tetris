using System;
using System.Windows.Media;

namespace TetrisGame.Models
{
    public class Cell
    {
        public bool IsFilled { get; set; }
        public Brush Color { get; set; }
        public Cell()
        {
            IsFilled = false;
            Color = Brushes.Transparent;
        }
    }

    public class Board
    {
        public static readonly int Rows = 20;
        public static readonly int Columns = 10;
        public Cell[,] Cells { get; set; }
        public Board()
        {
            Cells = new Cell[Rows, Columns];
            for (int r = 0; r < Rows; r++)
                for (int c = 0; c < Columns; c++)
                    Cells[r, c] = new Cell();
        }

        public bool IsCellOccupied(int x, int y)
        {
            if (x < 0 || x >= Columns || y < 0 || y >= Rows)
                return true;
            return Cells[y, x].IsFilled;
        }

        public void OccupyCell(int x, int y, Brush color)
        {
            if (x >= 0 && x < Columns && y >= 0 && y < Rows)
            {
                Cells[y, x].IsFilled = true;
                Cells[y, x].Color = color;
            }
        }

        public int ClearFullLines()
        {
            int linesCleared = 0;
            for (int r = 0; r < Rows; r++)
            {
                bool isFull = true;
                for (int c = 0; c < Columns; c++)
                {
                    if (!Cells[r, c].IsFilled)
                    {
                        isFull = false;
                        break;
                    }
                }

                if (isFull)
                {
                    linesCleared++;
                    for (int row = r; row > 0; row--)
                    {
                        for (int col = 0; col < Columns; col++)
                        {
                            Cells[row, col].IsFilled = Cells[row - 1, col].IsFilled;
                            Cells[row, col].Color = Cells[row - 1, col].Color;
                        }
                    }
                    for (int col = 0; col < Columns; col++)
                    {
                        Cells[0, col].IsFilled = false;
                        Cells[0, col].Color = Brushes.Transparent;
                    }
                }
            }
            return linesCleared;
        }

        public Board Clone()
        {
            var newBoard = new Board();
            for (int r = 0; r < Rows; r++)
                for (int c = 0; c < Columns; c++)
                {
                    newBoard.Cells[r, c].IsFilled = this.Cells[r, c].IsFilled;
                    newBoard.Cells[r, c].Color = this.Cells[r, c].Color;
                }
            return newBoard;
        }
    }

    public abstract class Tetromino
    {
        public virtual bool[,] Shape { get; protected set; }
        public virtual Brush Color { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Tetromino()
        {
            X = 0;
            Y = 0;
        }

        public void RotateClockwise()
        {
            int size = Shape.GetLength(0);
            bool[,] rotated = new bool[size, size];
            for (int r = 0; r < size; r++)
                for (int c = 0; c < size; c++)
                    rotated[c, size - r - 1] = Shape[r, c];
            Shape = rotated;
        }

        public void RotateCounterClockwise()
        {
            int size = Shape.GetLength(0);
            bool[,] rotated = new bool[size, size];
            for (int r = 0; r < size; r++)
                for (int c = 0; c < size; c++)
                    rotated[size - c - 1, r] = Shape[r, c];
            Shape = rotated;
        }

        public abstract Tetromino Clone();
    }

    // Specific Tetromino Implementations
    public class ITetromino : Tetromino
    {
        public ITetromino()
        {
            Shape = new bool[4, 4]
            {
                { true,  true,  true,  true },
                { false, false, false, false },
                { false, false, false, false },
                { false, false, false, false }
            };
            Color = Brushes.Cyan;
        }

        public override Tetromino Clone()
        {
            return new ITetromino
            {
                X = this.X,
                Y = this.Y
            };
        }
    }

    public class OTetromino : Tetromino
    {
        public OTetromino()
        {
            Shape = new bool[2, 2]
            {
                { true, true },
                { true, true }
            };
            Color = Brushes.Yellow;
        }

        public override Tetromino Clone()
        {
            return new OTetromino
            {
                X = this.X,
                Y = this.Y
            };
        }
    }

    public class TTetromino : Tetromino
    {
        public TTetromino()
        {
            Shape = new bool[3, 3]
            {
                { false, true,  false },
                { true,  true,  true  },
                { false, false, false }
            };
            Color = Brushes.Purple;
        }

        public override Tetromino Clone()
        {
            return new TTetromino
            {
                X = this.X,
                Y = this.Y
            };
        }
    }

    public class STetromino : Tetromino
    {
        public STetromino()
        {
            Shape = new bool[3, 3]
            {
                { false, true,  true  },
                { true,  true,  false },
                { false, false, false }
            };
            Color = Brushes.Green;
        }

        public override Tetromino Clone()
        {
            return new STetromino
            {
                X = this.X,
                Y = this.Y
            };
        }
    }

    public class ZTetromino : Tetromino
    {
        public ZTetromino()
        {
            Shape = new bool[3, 3]
            {
                { true,  true,  false },
                { false, true,  true  },
                { false, false, false }
            };
            Color = Brushes.Red;
        }

        public override Tetromino Clone()
        {
            return new ZTetromino
            {
                X = this.X,
                Y = this.Y
            };
        }
    }

    public class JTetromino : Tetromino
    {
        public JTetromino()
        {
            Shape = new bool[3, 3]
            {
                { true,  false, false },
                { true,  true,  true  },
                { false, false, false }
            };
            Color = Brushes.Blue;
        }

        public override Tetromino Clone()
        {
            return new JTetromino
            {
                X = this.X,
                Y = this.Y
            };
        }
    }

    public class LTetromino : Tetromino
    {
        public LTetromino()
        {
            Shape = new bool[3, 3]
            {
                { false, false, true  },
                { true,  true,  true  },
                { false, false, false }
            };
            Color = Brushes.Orange;
        }

        public override Tetromino Clone()
        {
            return new LTetromino
            {
                X = this.X,
                Y = this.Y
            };
        }
    }

    public interface ITetrominoFactory
    {
        Tetromino CreateTetromino();
    }

    public class TetrominoFactory : ITetrominoFactory
    {
        private static readonly string[] TetrominoTypes = { "I", "O", "T", "S", "Z", "J", "L" };
        private static readonly Random random = new Random();

        public Tetromino CreateTetromino()
        {
            string type = TetrominoTypes[random.Next(TetrominoTypes.Length)];
            switch (type)
            {
                case "I":
                    return new ITetromino();
                case "O":
                    return new OTetromino();
                case "T":
                    return new TTetromino();
                case "S":
                    return new STetromino();
                case "Z":
                    return new ZTetromino();
                case "J":
                    return new JTetromino();
                case "L":
                    return new LTetromino();
                default:
                    return new ITetromino();
            }
        }
    }
}
