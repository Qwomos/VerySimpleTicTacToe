using Raylib_cs;
using VerySimpleTicTacToe;

const int screenWidth = 640;
const int screenHeight = 480;

Raylib.InitWindow(screenWidth, screenHeight, "Very Simple Tic Tac Toe");
Raylib.SetTargetFPS(60);

var gameBoard = new GameBoard();

while (!Raylib.WindowShouldClose())
{
    if (Raylib.IsMouseButtonPressed(MouseButton.Left))
    {
        var mousePosition = Raylib.GetMousePosition();
        gameBoard.UpdateGame(mousePosition);
    }

    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.RayWhite);
    gameBoard.Display();
    Raylib.EndDrawing();
}

Raylib.CloseWindow();