using System;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Input;
using TetrisGame.Commands;
using TetrisGame.Management;
using TetrisGame.UI;

namespace TetrisGame.GameModes
{
    public interface IGameMode
    {
        void Initialize();
        UserControl GetGameView();
        void HandleInput(Key key);
        event Action ExitRequested;
    }

    // Base class for game modes
    public abstract class BaseGameMode : IGameMode
    {
        protected GameManager gameManager;
        protected GameView gameView;
        protected Timer gameTimer;
        public event Action ExitRequested;

        protected IGameState currentState;
        protected IGameState runningState;
        protected IGameState pausedState;
        protected IGameState gameOverState;

        public BaseGameMode()
        {
            gameManager = new GameManager();
            gameView = new GameView(gameManager);
            gameView.PauseRequested += Pause;
            gameView.ResumeRequested += Resume;
            gameView.RestartRequested += StartNewGame;
            gameView.BackMenuRequested += OnBackMenuRequested;
            SetupTimer();

            runningState = new RunningState(this);
            pausedState = new PausedState(this);
            gameOverState = new GameOverState(this);

            currentState = runningState;
        }

        public virtual void Initialize()
        {
            gameManager.Initialize();
            gameView.UpdateView();
            currentState.Enter();
            gameTimer.Start();
        }

        public UserControl GetGameView()
        {
            return gameView;
        }

        public virtual void HandleInput(Key key)
        {
            currentState.HandleInput(key);
        }

        protected virtual void Pause()
        {
            if (currentState is RunningState)
            {
                currentState.Exit();
                currentState = pausedState;
                currentState.Enter();
                gameTimer.Stop();
                gameView.ShowPauseMenu();
            }
        }

 
        public virtual void Resume()
        {
            if (currentState is PausedState)
            {
                currentState.Exit();
                currentState = runningState;
                currentState.Enter();
                gameTimer.Start();
                gameView.HideOverlay();
            }
        }

        protected virtual void End(string winner)
        {
            currentState.Exit();
            gameView.ShowGameOver(winner);
            gameTimer.Stop();
        }
        public virtual void StartNewGame()
        {
            gameManager.Reset();
            gameView.HideOverlay();
            currentState.Exit();
            currentState = runningState;
            currentState.Enter();
            gameTimer.Start();
        }

        protected virtual void OnBackMenuRequested()
        {
            ExitRequested?.Invoke();
        }

        private void SetupTimer()
        {
            gameTimer = new Timer(500);
            gameTimer.Elapsed += OnGameTick;
            gameTimer.AutoReset = true;
        }

        protected virtual void OnGameTick(object sender, ElapsedEventArgs e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                gameManager.DropTetromino();
                gameView.UpdateView();
                if (gameManager.IsGameOver)
                {
                    End("Game Over");
                }
            });
        }

        public virtual void ProcessInput(Key key)
        {
            IGameCommand command = null;

            switch (key)
            {
                case Key.A:
                    command = new MoveLeftCommand(gameManager);
                    break;
                case Key.D:
                    command = new MoveRightCommand(gameManager);
                    break;
                case Key.W:
                    command = new RotateCommand(gameManager);
                    break;
                case Key.S:
                    command = new DropCommand(gameManager);
                    break;
                case Key.LeftCtrl:
                    command = new HardDropCommand(gameManager);
                    break;
                default:
                    break;
            }

            command?.Execute();
            gameView.UpdateView();
        }
    }


    public class SinglePlayerMode : BaseGameMode
    {
        public SinglePlayerMode() : base()
        {
        }

        protected override void End(string winner)
        {
            base.End(winner);
        }
    }

    public class LocalBattleMode : BaseGameMode
    {
        private GameManager player2Manager;
        private DateTime lastInputP1;
        private DateTime lastInputP2;
        private readonly TimeSpan inputCooldownP1 = TimeSpan.FromMilliseconds(10);
        private readonly TimeSpan inputCooldownP2 = TimeSpan.FromMilliseconds(10);

        public LocalBattleMode() : base()
        {
            player2Manager = new GameManager();
            gameView = new GameView(gameManager, player2Manager);
            gameView.PauseRequested += Pause;
            gameView.ResumeRequested += Resume;
            gameView.RestartRequested += StartNewGame;
            gameView.BackMenuRequested += OnBackMenuRequested;
            gameView.UpdateView();
            lastInputP1 = DateTime.MinValue;
            lastInputP2 = DateTime.MinValue;
        }

        public override void Initialize()
        {
            base.Initialize();
            player2Manager.Initialize();
            gameView.UpdateView();
            currentState.Enter();
            gameTimer.Start();
        }

        protected override void Pause()
        {
            base.Pause();
        }

        public override void Resume()
        {
            base.Resume();
        }

        protected override void End(string winner)
        {
            base.End(winner);
        }

        public override void HandleInput(Key key)
        {
            currentState.HandleInput(key);
        }

        public override void ProcessInput(Key key)
        {
            bool updated = false;
            DateTime now = DateTime.Now;

            // Player 1 Controls (WASD + LeftCtrl)
            IGameCommand commandP1 = null;
            switch (key)
            {
                case Key.W:
                    if (now - lastInputP1 >= inputCooldownP1)
                    {
                        commandP1 = new RotateCommand(gameManager);
                        lastInputP1 = now;
                    }
                    break;
                case Key.A:
                    if (now - lastInputP1 >= inputCooldownP1)
                    {
                        commandP1 = new MoveLeftCommand(gameManager);
                        lastInputP1 = now;
                    }
                    break;
                case Key.S:
                    if (now - lastInputP1 >= inputCooldownP1)
                    {
                        commandP1 = new DropCommand(gameManager);
                        lastInputP1 = now;
                    }
                    break;
                case Key.D:
                    if (now - lastInputP1 >= inputCooldownP1)
                    {
                        commandP1 = new MoveRightCommand(gameManager);
                        lastInputP1 = now;
                    }
                    break;
                case Key.LeftCtrl:
                    if (now - lastInputP1 >= inputCooldownP1)
                    {
                        commandP1 = new HardDropCommand(gameManager);
                        lastInputP1 = now;
                    }
                    break;
                default:
                    break;
            }

            // Player 2 Controls (Arrow Keys + RightCtrl)
            IGameCommand commandP2 = null;
            switch (key)
            {
                case Key.Up:
                    if (now - lastInputP2 >= inputCooldownP2)
                    {
                        commandP2 = new RotateCommand(player2Manager);
                        lastInputP2 = now;
                    }
                    break;
                case Key.Left:
                    if (now - lastInputP2 >= inputCooldownP2)
                    {
                        commandP2 = new MoveLeftCommand(player2Manager);
                        lastInputP2 = now;
                    }
                    break;
                case Key.Down:
                    if (now - lastInputP2 >= inputCooldownP2)
                    {
                        commandP2 = new DropCommand(player2Manager);
                        lastInputP2 = now;
                    }
                    break;
                case Key.Right:
                    if (now - lastInputP2 >= inputCooldownP2)
                    {
                        commandP2 = new MoveRightCommand(player2Manager);
                        lastInputP2 = now;
                    }
                    break;
                case Key.RightCtrl:
                    if (now - lastInputP2 >= inputCooldownP2)
                    {
                        commandP2 = new HardDropCommand(player2Manager);
                        lastInputP2 = now;
                    }
                    break;
                default:
                    break;
            }

            if (commandP1 != null)
            {
                commandP1.Execute();
                updated = true;
            }

            if (commandP2 != null)
            {
                commandP2.Execute();
                updated = true;
            }

            if (updated)
            {
                gameView.UpdateView();
            }
        }

        protected override void OnGameTick(object sender, ElapsedEventArgs e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                gameManager.DropTetromino();
                player2Manager.DropTetromino();
                gameView.UpdateView();
                if (gameManager.IsGameOver && player2Manager.IsGameOver)
                {
                    End("Draw");
                }
                else if (gameManager.IsGameOver)
                {
                    End("Player 2 Wins");
                }
                else if (player2Manager.IsGameOver)
                {
                    End("Player 1 Wins");
                }
            });
        }

        public override void StartNewGame()
        {
            gameManager.Reset();
            gameManager.Initialize();
            player2Manager.Reset();
            player2Manager.Initialize();
            gameView.HideOverlay();
            currentState.Exit();
            currentState = runningState;
            currentState.Enter();
            gameTimer.Start();
            gameView.UpdateView();
        }
    }

    // Online Battle Mode 
    public class OnlineBattleMode : BaseGameMode
    {
        public OnlineBattleMode() : base()
        {
            // Implement network-related functionalities here
            gameView.PauseRequested += Pause;
            gameView.ResumeRequested += Resume;
            gameView.RestartRequested += StartNewGame;
            gameView.BackMenuRequested += OnBackMenuRequested;
        }

        protected override void End(string winner)
        {
            base.End(winner);
            // Handle network disconnections or other online-specific end conditions
        }


        public override void StartNewGame()
        {
            base.StartNewGame();
            // Restart network game here
        }

        public override void HandleInput(Key key)
        {
            if (key == Key.P)
            {
                Pause();
                return;
            }
            base.HandleInput(key);
            // Handle online-specific input if necessary
        }

        public override void ProcessInput(Key key)
        {
            // Implement online-specific input processing here
            base.ProcessInput(key);
        }
    }

    // Game States
    public interface IGameState
    {
        void HandleInput(Key key);
        void Enter();
        void Exit();
    }

    public class RunningState : IGameState
    {
        private readonly BaseGameMode gameMode;

        public RunningState(BaseGameMode mode)
        {
            gameMode = mode;
        }

        public void Enter()
        {
            // Entering Running State
        }

        public void Exit()
        {
            // Exiting Running State
        }

        public void HandleInput(Key key)
        {
            gameMode.ProcessInput(key);
        }
    }

    public class PausedState : IGameState
    {
        private readonly BaseGameMode gameMode;

        public PausedState(BaseGameMode mode)
        {
            gameMode = mode;
        }

        public void Enter()
        {
            // Entering Paused State
        }

        public void Exit()
        {
            // Exiting Paused State
        }

        public void HandleInput(Key key)
        {
            if (key == Key.P)
            {
                gameMode.Resume();
            }
        }
    }

    public class GameOverState : IGameState
    {
        private readonly BaseGameMode gameMode;

        public GameOverState(BaseGameMode mode)
        {
            gameMode = mode;
        }

        public void Enter()
        {
            // Entering GameOver State
        }

        public void Exit()
        {
            // Exiting GameOver State
        }

        public void HandleInput(Key key)
        {
            if (key == Key.R)
            {
                gameMode.StartNewGame();
            }
        }
    }
}
