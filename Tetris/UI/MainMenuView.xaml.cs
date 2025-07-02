using System;
using System.Windows;
using System.Windows.Controls;

namespace TetrisGame.UI
{
    public partial class MainMenuView : UserControl
    {
        public event Action StartSinglePlayer;
        public event Action StartLocalBattle;
        public event Action StartOnlineBattle;

        public MainMenuView()
        {
            InitializeComponent();
        }

        private void SinglePlayer_Click(object sender, RoutedEventArgs e)
        {
            StartSinglePlayer?.Invoke();
        }

        private void LocalBattle_Click(object sender, RoutedEventArgs e)
        {
            StartLocalBattle?.Invoke();
        }

        private void OnlineBattle_Click(object sender, RoutedEventArgs e)
        {
            StartOnlineBattle?.Invoke();
        }
    }
}
