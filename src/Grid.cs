using System.Numerics;
using Raylib_cs;

namespace VerySimpleTicTacToe;

public class Grid
{
    private readonly Square[] squares = new Square[9];
    private char playerSymbole = 'X';

    public const int Margin = 20;
    public const int LineWidth = 6;
    public const int Length = Square.Size * 3 + LineWidth * 2;

    public Grid()
    {
        /*
        0 | 1 | 2
        -----------
        3 | 4 | 5
        -----------
        6 | 7 | 8
        */

        squares[0] = new Square(new Vector2(Margin, Margin));
        squares[1] = new Square(new Vector2(Margin + Square.Size + LineWidth, Margin));
        squares[2] = new Square(new Vector2(Margin + Square.Size * 2 + LineWidth * 2, Margin));
        squares[3] = new Square(new Vector2(Margin, Margin + Square.Size + LineWidth));
        squares[4] = new Square(new Vector2(Margin + Square.Size + LineWidth, Margin + Square.Size + LineWidth));
        squares[5] = new Square(new Vector2(Margin + Square.Size * 2 + LineWidth * 2, Margin + Square.Size + LineWidth));
        squares[6] = new Square(new Vector2(Margin, Margin + Square.Size * 2 + LineWidth * 2));
        squares[7] = new Square(new Vector2(Margin + Square.Size + LineWidth, Margin + Square.Size * 2 + LineWidth * 2));
        squares[8] = new Square(new Vector2(Margin + Square.Size * 2 + LineWidth * 2, Margin + Square.Size * 2 + LineWidth * 2)); 
    }

    public void Display()
    {
        DrawGrid();

        foreach (var square in squares)
            square.Display();
    }

    public void OnClick(Vector2 mousePosition)
    {
        foreach (var square in squares)
        {
            if (square.IsInside(mousePosition))
            {
                square.CurrentSymbole = playerSymbole;
                
                if (playerSymbole == 'X')
                    playerSymbole = 'O';
                else
                    playerSymbole = 'X';

                break;
            }
        }
    }

    private static void DrawGrid()
    {
        Raylib.DrawRectangle(Square.Size + Margin, Margin, LineWidth, Length, Color.DarkGray); //First vertical line
        Raylib.DrawRectangle(Square.Size * 2 + LineWidth + Margin, Margin, LineWidth, Length, Color.DarkGray); //Second vertical line
        Raylib.DrawRectangle(Margin, Square.Size + Margin, Length, LineWidth, Color.DarkGray); //First horizontal line
        Raylib.DrawRectangle(Margin, Square.Size * 2 + LineWidth + Margin, Length, LineWidth, Color.DarkGray); //Second horizontal line
    }
}