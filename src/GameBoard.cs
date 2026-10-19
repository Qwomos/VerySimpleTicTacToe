using System.Numerics;

namespace VerySimpleTicTacToe;

public class GameBoard
{
    public const int Margin = 20;
    public const char TieSymbol = 'T';

    private readonly Grid grid = new();
    private readonly Score score = new();
    private char? winner;

    public void UpdateGame(Vector2 mousePosition)
    {
        if (winner is not null)
            return;

        if (IsGridClicked(mousePosition))
        {
            //Process the game logic and returns the winner symbol if any.
            winner = grid.Update(mousePosition);

            if (winner is not null)
                score.Update(winner.Value);
        }
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
        score.Display();
    }
}