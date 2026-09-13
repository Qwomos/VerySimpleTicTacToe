using System.Numerics;
using Raylib_cs;

namespace VerySimpleTicTacToe;

public class Grid
{
    public const int Margin = 20;
    public const int LineWidth = 6;
    public const int Length = Square.Size * 3 + LineWidth * 2;

    private readonly Square[] squares = new Square[9];
    private char playerSymbol = 'X';
    private (int, int)? WinningSquaresPair;

    public char? Winner { get; private set; }

    public Grid()
    {
        /*
        0 | 1 | 2
        ----------
        3 | 4 | 5
        ----------
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

    private static void DrawGrid()
    {
        Raylib.DrawRectangle(Square.Size + Margin, Margin, LineWidth, Length, Color.DarkGray); //First vertical line
        Raylib.DrawRectangle(Square.Size * 2 + LineWidth + Margin, Margin, LineWidth, Length, Color.DarkGray); //Second vertical line
        Raylib.DrawRectangle(Margin, Square.Size + Margin, Length, LineWidth, Color.DarkGray); //First horizontal line
        Raylib.DrawRectangle(Margin, Square.Size * 2 + LineWidth + Margin, Length, LineWidth, Color.DarkGray); //Second horizontal line
    }

    public void OnClick(Vector2 mousePosition)
    {
        foreach (var square in squares)
        {
            if (square.IsSquareClicked(mousePosition))
            {
                square.Value = playerSymbol;

                WinningSquaresPair = CheckCurrentPlayerWon();

                if (WinningSquaresPair is not null)
                {
                    Winner = playerSymbol;
                }
                else
                {
                    //Toggle player turn
                    if (playerSymbol == 'X')
                        playerSymbol = 'O';
                    else
                        playerSymbol = 'X';
                }

                break;
            }
        }
    }

    private (int, int)? CheckCurrentPlayerWon()
    {
        /*
        0 | 1 | 2
        ----------
        3 | 4 | 5
        ----------
        6 | 7 | 8
        */

        //Horizontal lines
        if (squares[0].Value == playerSymbol && squares[1].Value == playerSymbol && squares[2].Value == playerSymbol) return (0, 2);
        if (squares[3].Value == playerSymbol && squares[4].Value == playerSymbol && squares[5].Value == playerSymbol) return (3, 5);
        if (squares[6].Value == playerSymbol && squares[7].Value == playerSymbol && squares[8].Value == playerSymbol) return (6, 8);

        //Vertical lines
        if (squares[0].Value == playerSymbol && squares[3].Value == playerSymbol && squares[6].Value == playerSymbol) return (0, 6);
        if (squares[1].Value == playerSymbol && squares[4].Value == playerSymbol && squares[7].Value == playerSymbol) return (1, 7);
        if (squares[2].Value == playerSymbol && squares[5].Value == playerSymbol && squares[8].Value == playerSymbol) return (2, 8);

        //Diagonals
        if (squares[0].Value == playerSymbol && squares[4].Value == playerSymbol && squares[8].Value == playerSymbol) return (0, 8);
        if (squares[2].Value == playerSymbol && squares[4].Value == playerSymbol && squares[6].Value == playerSymbol) return (2, 6);

        return null;
    }
}