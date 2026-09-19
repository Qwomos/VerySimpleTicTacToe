using System.Numerics;

namespace VerySimpleTicTacToe;

public class GameBoard
{
    public const int Margin = 20;

    private readonly Grid grid = new();

    public void UpdateGame(Vector2 mousePosition)
    {
        if (grid.Winner is not null)
            return;

        if (IsGridClicked(mousePosition))
            grid.OnClick(mousePosition);
    }

    private static bool IsGridClicked(Vector2 mousePosition)
    {
        return mousePosition.X >= Margin
            && mousePosition.X <= Grid.Length + Margin
            && mousePosition.Y >= Margin
            && mousePosition.Y <= Grid.Length + Margin;
    }

    public void Display()
    {
        grid.Display();
    }
}