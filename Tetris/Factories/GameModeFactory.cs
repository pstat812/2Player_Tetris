using TetrisGame.GameModes;

namespace TetrisGame.Factories{
    public static class GameModeFactory{
        public static IGameModeFactory CreateFactory(string mode) {
            switch (mode){
                case "SinglePlayer":
                    return new SinglePlayerFactory();
                case "LocalBattle":
                    return new LocalBattleFactory();
                case "OnlineBattle":
                    return new OnlineBattleFactory();
                default:
                    return new SinglePlayerFactory();
                 }
             }
            }

    public interface IGameModeFactory{
        IGameMode CreateGameMode();
    }

    public class SinglePlayerFactory : IGameModeFactory{
        public IGameMode CreateGameMode(){
            return new SinglePlayerMode();
          }
    }

    public class LocalBattleFactory : IGameModeFactory {
        public IGameMode CreateGameMode(){
            return new LocalBattleMode();
        }
    }

    public class OnlineBattleFactory : IGameModeFactory{
        public IGameMode CreateGameMode() {
            return new OnlineBattleMode();
        }
    }
}
