namespace TetrisGame.Commands
{


    public interface IGameCommand
    {
        void Execute();
    }



    public class MoveLeftCommand : IGameCommand
    {
        private readonly Management.GameManager gameManager;
        public MoveLeftCommand(Management.GameManager manager)
        {
            gameManager = manager;
        }
        public void Execute()
        {
            gameManager.MoveLeft();
        }
    }



    public class MoveRightCommand : IGameCommand
    {
        private readonly Management.GameManager gameManager;
        public MoveRightCommand(Management.GameManager manager)
        {
            gameManager = manager;
        }
        public void Execute()
        {
            gameManager.MoveRight();
        }
    }



    public class RotateCommand : IGameCommand
    {
        private readonly Management.GameManager gameManager;
        public RotateCommand(Management.GameManager manager)
        {
            gameManager = manager;
        }

        public void Execute()
        {
            gameManager.RotateTetromino();
        }
    }



    public class DropCommand : IGameCommand
    {
        private readonly Management.GameManager gameManager;
        public DropCommand(Management.GameManager manager)
        {
            gameManager = manager;
        }

        public void Execute()
        {
            gameManager.DropTetromino();
        }
    }



    public class HardDropCommand : IGameCommand
    {
        private readonly Management.GameManager gameManager;
        public HardDropCommand(Management.GameManager manager)
        {
            gameManager = manager;
        }

        public void Execute()
        {
            gameManager.HardDropTetromino();
        }
    }




}
