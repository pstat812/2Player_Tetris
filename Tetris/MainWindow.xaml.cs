using System.Windows;
using System.Windows.Input;
using TetrisGame.Factories;
using TetrisGame.GameModes;
using TetrisGame.UI;

namespace TetrisGame
{
    public partial class MainWindow : Window
    {
        private IGameMode currentGameMode;

        public MainWindow()
        {
            InitializeComponent();
            ShowMainMenu();
        }

        private void ShowMainMenu()
        {
            var mainMenu = new MainMenuView();
            mainMenu.StartSinglePlayer += () => StartGameMode("SinglePlayer");
            mainMenu.StartLocalBattle += () => StartGameMode("LocalBattle");
            mainMenu.StartOnlineBattle += () => StartGameMode("OnlineBattle");
            MainContent.Content = mainMenu;
            this.Focus();
        }

        private void StartGameMode(string mode)
        {
            IGameModeFactory factory = GameModeFactory.CreateFactory(mode);
            currentGameMode = factory.CreateGameMode();
            currentGameMode.ExitRequested += ShowMainMenu;
            currentGameMode.Initialize();
            MainContent.Content = currentGameMode.GetGameView();
            this.Focus();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            currentGameMode?.HandleInput(e.Key);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Focus();
        }
    }
}
