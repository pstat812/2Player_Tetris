# Introduction
The Project is a Tetris game made under .NET and C#.
The game is a exact copy of the original Tetris game, with an local two player battle mode. It is made using OOP Design Patterns and Behavioral Patterns.

# System Structure
This section details the architecture of the Tetris game, including class design, class reuse, and the software patterns used.

### 2.1 Core Game Classes:
- **`Tetromino.cs`**: Represents a Tetromino piece. It includes methods for rotation, movement, and collision detection.
- **`Board.cs`**: Manages the game board, keeping track of filled cells and handling line clearing.
- **`GameManager.cs`**: Core game logic control, including setting up the board, moving pieces, and scoring. It includes the game loop and handles player input.
- **`Command.cs`**: Defines the `GameCommand` class, an enum `CommandType`, and other data structures which supports player input.

### 2.2 Game Modes (State Pattern)
- **`SinglePlayerView.cs`**: Implements the game logic and UI structure for the singleplayer mode through `GameManager`.
- **`LocalBattleView.cs`**: Implements UI structure and core game logic functionality for the local dual player game mode with 2 instances of `GameManager`.
- **`OnlineBattleView.cs`**: *(Partially Implemented)* Intended for handling the online multiplayer game mode, using a separate `GameManager` instance and server client logic with signalR.

### 2.3 UI Classes:
- **`MainWindow.xaml.cs`**: The main window of the application, responsible for navigating between the menu and each game mode. It also contains the hosting and joining logic for the online mode.
- **`GameView.xaml`**: The base implementation for game rendering using the canvas API. It supports multiple game modes with different `GameManager` instances.
- **`NextTetrominoView.xaml`**: Displays the next Tetromino piece in advance.
- **`MainMenuView.xaml`**: Displays start game buttons to select the game mode.
- **`GameOverView.xaml`**: Displays the game over screen.

### 2.4 Class Reuse:
- **`Tetromino.cs`, `Board.cs`, `GameManager.cs`**: Reused across all game modes (single-player and local dual-player). The `GameManager` is used with different states to play different modes. The online mode reuses `GameManger` and the actions are broadcast from the client to the server for synchronization.
- **`GameView.xaml`**: Reused by both the single player, and local dual player mode, and the online mode implementation by using data binding and different `GameManager` instances.

### 2.5 Software Patterns:
#### Factory Pattern:
- **`TetrominoFactory.cs`**: Used to create different types of Tetrominoes.
- **`GameModeFactory`**: The `MainMenuView` uses this to create different game mode views based on the button's click.

#### Command Pattern:
- **`TetrisGame.Command` namespace**: Classes involved in the command pattern, using `GameCommand`, and `CommandType` to decouple the input event and the main game logic.

#### State Pattern:
- (Implicit) Different game modes such as `LocalBattleView`, `SinglePlayerView` and `OnlineBattleView` uses the state pattern to control each game state, by having a clear set of states. The `MainWindow` acts as the client view switching between these modes.

#### Model-View-ViewModel (MVVM) (Implicit):
- The design incorporates a implicit MVVM approach by having an independent UI view separated from the core logic. The `MainView` and other XAML files are loosely coupled with C# logic and data binding is used to update view state, which indicate a implicit MVVM implementation.




# How to open
To try the game demo, run Tetris/bin/Release/Tetris.exe

# Showcase
![Tetris1](https://github.com/user-attachments/assets/398f87b6-c103-408a-998f-a022354594c8)
![Tetris2](https://github.com/user-attachments/assets/0198c810-3a4d-4a03-a86a-4f60cb48483d)
![Tetris3](https://github.com/user-attachments/assets/71ffd89e-f9f3-45cd-9148-a816590c6105)
![Tetris4](https://github.com/user-attachments/assets/cd011ec2-754b-465e-8dc9-f9cdad6c8753)
![Tetris5](https://github.com/user-attachments/assets/13a7af15-40d5-49ef-ac73-fe57b1319071)
