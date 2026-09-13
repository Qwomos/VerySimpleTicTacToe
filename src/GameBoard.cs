using System.Numerics;

namespace VerySimpleTicTacToe;

public class GameBoard
{
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
        return mousePosition.X >= Grid.Margin
            && mousePosition.X <= Grid.Length + Grid.Margin
            && mousePosition.Y >= Grid.Margin
            && mousePosition.Y <= Grid.Length + Grid.Margin;
    }

    public void Display()
    {
        grid.Display();
    }
}