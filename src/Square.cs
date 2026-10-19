using System.Numerics;
using Raylib_cs;

namespace VerySimpleTicTacToe;

public class Square(Vector2 startPosition)
{
    public const int Size = 100;
    public const float Padding = 12f;
    public const float SymbolThickness = 6f;

    public char Value { get; private set; } = ' ';

    public bool SetValue(char playerSymbol)
    {
        if (playerSymbol == ' ' || Value == ' ')
        {
            Value = playerSymbol;

            return true;
        }

        return false;
    }

    public void Display()
    {
        if (Value == 'X')
            DrawXSymbol();
        else if (Value == 'O')
            DrawOSymbol();
    }

    private void DrawXSymbol()
    {
       var firstDiagonalStart = new Vector2(startPosition.X + Padding, startPosition.Y + Padding);
       var firstDiagonalEnd = new Vector2(startPosition.X + Size - Padding, startPosition.Y + Size - Padding);
       var secondDiagonalStart = new Vector2(startPosition.X + Size - Padding, startPosition.Y + Padding);
       var secondDiagonalEnd = new Vector2(startPosition.X + Padding, startPosition.Y + Size - Padding);

       Raylib.DrawLineEx(firstDiagonalStart, firstDiagonalEnd, SymbolThickness, Color.SkyBlue);
       Raylib.DrawLineEx(secondDiagonalStart, secondDiagonalEnd, SymbolThickness, Color.SkyBlue);
    }

    private void DrawOSymbol()
    {
        Vector2 center = GetCenter();

        Raylib.DrawCircleV(center, Size / 2 - Padding / 2, Color.Orange);
        Raylib.DrawCircleV(center, Size / 3 + 4f, Color.RayWhite);
    }

    public Vector2 GetCenter()
    {
        return new(startPosition.X + Size / 2, startPosition.Y + Size / 2);
    }

    public bool IsSquareClicked(Vector2 mousePosition)
    {
        return mousePosition.X >= startPosition.X
            && mousePosition.X <= startPosition.X + Size
            && mousePosition.Y >= startPosition.Y
            && mousePosition.Y <= startPosition.Y + Size;
    }
}