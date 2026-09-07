using System.Numerics;
using Raylib_cs;

namespace VerySimpleTicTacToe;

public class Square(Vector2 startPosition)
{
    public const int Size = 100;
    public const float Padding = 12f;
    public const float SymbolThickness = 6f;

    public char CurrentSymbole 
    { 
        get;
        set
        {
            if (value == ' ' || field == ' ')
                field = value;
        }
    } = ' ';

    public void Display()
    {
        if (CurrentSymbole == 'X')
            DrawXSymbol();
        else if (CurrentSymbole == 'O')
            DrawOSymbol();
    }

    public bool IsInside(Vector2 mousePosition)
    {
        return mousePosition.X >= startPosition.X
            && mousePosition.X <= startPosition.X + Size
            && mousePosition.Y >= startPosition.Y
            && mousePosition.Y <= startPosition.Y + Size;
    }

    private void DrawXSymbol()
    {
       Vector2 firstDiagonalStart = new(startPosition.X + Padding, startPosition.Y + Padding);
       Vector2 firstDiagonalEnd = new(startPosition.X + Size - Padding, startPosition.Y + Size - Padding);
       Vector2 secondDiagonalStart = new(startPosition.X + Size - Padding, startPosition.Y + Padding);
       Vector2 secondDiagonalEnd = new(startPosition.X + Padding, startPosition.Y + Size - Padding);

       Raylib.DrawLineEx(firstDiagonalStart, firstDiagonalEnd, SymbolThickness, Color.SkyBlue);
       Raylib.DrawLineEx(secondDiagonalStart, secondDiagonalEnd, SymbolThickness, Color.SkyBlue);
    }

    private void DrawOSymbol()
    {
        Vector2 center = new(startPosition.X + Size / 2, startPosition.Y + Size / 2);

        Raylib.DrawCircleV(center, Size / 2 - Padding / 2, Color.Orange);
        Raylib.DrawCircleV(center, Size / 3 + 4f, Color.RayWhite);
    }
}