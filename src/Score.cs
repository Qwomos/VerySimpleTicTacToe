
using System.Numerics;
using Raylib_cs;

namespace VerySimpleTicTacToe;

public class Score
{
    private const int symbolSize = 40;
    private const float symbolThickness = 4f;
    private const float padding = 20f;
    private const int xPosition = Grid.Length + GameBoard.Margin * 3;
    private const int scoreOffset = 70;
    private const int fontSize = 54;

    private int xWinsCount = 0;
    private int oWinsCount = 0;
    private int tiesCount = 0;

    public void Update(char winner)
    {
        if (winner == GameBoard.TieSymbol)
            tiesCount++;
        else if (winner == 'X')
            xWinsCount++;
        else
            oWinsCount++;
    }

    public void Display()
    {
        var frame = new Rectangle(xPosition, GameBoard.Margin, 190f, 200f);

        Raylib.DrawRectangleLinesEx(frame, 2f, Color.DarkGray);
        DisplayXScore();
        DisplayOScore();
        DisplayTie();
    }

    private void DisplayXScore()
    {
        var firstDiagonalStart = new Vector2(xPosition + padding, GameBoard.Margin + padding);
        var firstDiagonalEnd = new Vector2(xPosition + symbolSize + padding, GameBoard.Margin + symbolSize + padding);
        var secondDiagonalStart = new Vector2(xPosition + symbolSize + padding, GameBoard.Margin + padding);
        var secondDiagonalEnd = new Vector2(xPosition + padding, GameBoard.Margin + symbolSize + padding);

        Raylib.DrawLineEx(firstDiagonalStart, firstDiagonalEnd, symbolThickness, Color.SkyBlue);
        Raylib.DrawLineEx(secondDiagonalStart, secondDiagonalEnd, symbolThickness, Color.SkyBlue);
        Raylib.DrawText(xWinsCount.ToString("#00"), xPosition + scoreOffset + symbolSize, GameBoard.Margin + 5, fontSize, Color.SkyBlue);
    }

    private void DisplayOScore()
    {
        const int yPosition = GameBoard.Margin + symbolSize + (int)padding * 2;

        var center = new Vector2(xPosition + symbolSize / 2 + padding, yPosition + symbolSize / 2 + 5f);

        Raylib.DrawCircleV(center, symbolSize / 2 + 4, Color.Orange);
        Raylib.DrawCircleV(center, symbolSize / 3 + 7, Color.RayWhite);
        Raylib.DrawText(oWinsCount.ToString("#00"), xPosition + symbolSize + scoreOffset, yPosition, fontSize, Color.Orange);
    }

    private void DisplayTie()
    {
        const int yPosition = GameBoard.Margin + symbolSize * 2 + (int)padding * 3;

        Raylib.DrawText("Tie", xPosition + 5, yPosition + 5, 42, Color.DarkGray);
        Raylib.DrawText(tiesCount.ToString("#00"), xPosition + symbolSize + scoreOffset, yPosition, fontSize, Color.DarkGray);
    }
}