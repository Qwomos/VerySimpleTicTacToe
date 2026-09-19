using System.Numerics;
using Raylib_cs;

namespace VerySimpleTicTacToe;

public class Grid
{
    public const int LineWidth = 6;
    public const int Length = Square.Size * 3 + LineWidth * 2;

    private readonly Square[] squares = new Square[9];
    private char playerSymbol = 'X';
    private (int, int, LineType)? WinningSquaresPair;

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

        squares[0] = new Square(new Vector2(GameBoard.Margin, GameBoard.Margin));
        squares[1] = new Square(new Vector2(GameBoard.Margin + Square.Size + LineWidth, GameBoard.Margin));
        squares[2] = new Square(new Vector2(GameBoard.Margin + Square.Size * 2 + LineWidth * 2, GameBoard.Margin));
        squares[3] = new Square(new Vector2(GameBoard.Margin, GameBoard.Margin + Square.Size + LineWidth));
        squares[4] = new Square(new Vector2(GameBoard.Margin + Square.Size + LineWidth, GameBoard.Margin + Square.Size + LineWidth));
        squares[5] = new Square(new Vector2(GameBoard.Margin + Square.Size * 2 + LineWidth * 2, GameBoard.Margin + Square.Size + LineWidth));
        squares[6] = new Square(new Vector2(GameBoard.Margin, GameBoard.Margin + Square.Size * 2 + LineWidth * 2));
        squares[7] = new Square(new Vector2(GameBoard.Margin + Square.Size + LineWidth, GameBoard.Margin + Square.Size * 2 + LineWidth * 2));
        squares[8] = new Square(new Vector2(GameBoard.Margin + Square.Size * 2 + LineWidth * 2, GameBoard.Margin + Square.Size * 2 + LineWidth * 2));
    }

    public void Display()
    {
        DrawGrid();

        foreach (var square in squares)
            square.Display();

        DrawWinningLine();
    }

    private static void DrawGrid()
    {
        Raylib.DrawRectangle(Square.Size + GameBoard.Margin, GameBoard.Margin, LineWidth, Length, Color.DarkGray); //First vertical line
        Raylib.DrawRectangle(Square.Size * 2 + LineWidth + GameBoard.Margin, GameBoard.Margin, LineWidth, Length, Color.DarkGray); //Second vertical line
        Raylib.DrawRectangle(GameBoard.Margin, Square.Size + GameBoard.Margin, Length, LineWidth, Color.DarkGray); //First horizontal line
        Raylib.DrawRectangle(GameBoard.Margin, Square.Size * 2 + LineWidth + GameBoard.Margin, Length, LineWidth, Color.DarkGray); //Second horizontal line
    }

    private void DrawWinningLine()
    {
        if (!WinningSquaresPair.HasValue)
            return;

        var startPosition = new Vector2();
        var endPosition = new Vector2();
        var (startSquare, endSquare, lineType) = WinningSquaresPair.Value;
        var startSquareCenter = squares[startSquare].GetCenter();
        var endSquareCenter = squares[endSquare].GetCenter();
        var squareHalfSize = Square.Size / 2;

        if (lineType == LineType.Horizontal)
        {
            startPosition.X = startSquareCenter.X - squareHalfSize;
            startPosition.Y = startSquareCenter.Y;
            endPosition.X = endSquareCenter.X + squareHalfSize;
            endPosition.Y = endSquareCenter.Y;
        }
        else if (lineType == LineType.Vertical)
        {
            startPosition.X = startSquareCenter.X;
            startPosition.Y = startSquareCenter.Y - squareHalfSize;
            endPosition.X = endSquareCenter.X;
            endPosition.Y = endSquareCenter.Y + squareHalfSize;
        }
        else //Diagonals
        {
            //0, 4, 8 diagonal line
            if (startSquare == 0)
            {
                startPosition.X = startSquareCenter.X - squareHalfSize;
                startPosition.Y = startSquareCenter.Y - squareHalfSize;
                endPosition.X = endSquareCenter.X + squareHalfSize;
                endPosition.Y = endSquareCenter.Y + squareHalfSize;
            }
            else //2, 4, 6 diagonal line
            {
                startPosition.X = startSquareCenter.X + squareHalfSize;
                startPosition.Y = startSquareCenter.Y - squareHalfSize;
                endPosition.X = endSquareCenter.X - squareHalfSize;
                endPosition.Y = endSquareCenter.Y + squareHalfSize;
            }

        }

        Raylib.DrawLineEx(startPosition, endPosition, 10f, Color.Magenta);
    }

    public void OnClick(Vector2 mousePosition)
    {
        foreach (var square in squares)
        {
            if (square.IsSquareClicked(mousePosition))
            {
                square.Value = playerSymbol;

                WinningSquaresPair = ValidateWinner();

                if (WinningSquaresPair.HasValue)
                    Winner = playerSymbol;
                else
                    TogglePlayerTurn();

                break;
            }
        }
    }

    private (int, int, LineType)? ValidateWinner()
    {
        /*
        0 | 1 | 2
        ----------
        3 | 4 | 5
        ----------
        6 | 7 | 8
        */

        //Horizontal lines
        if (squares[0].Value == playerSymbol && squares[1].Value == playerSymbol && squares[2].Value == playerSymbol) return (0, 2, LineType.Horizontal);
        if (squares[3].Value == playerSymbol && squares[4].Value == playerSymbol && squares[5].Value == playerSymbol) return (3, 5, LineType.Horizontal);
        if (squares[6].Value == playerSymbol && squares[7].Value == playerSymbol && squares[8].Value == playerSymbol) return (6, 8, LineType.Horizontal);

        //Vertical lines
        if (squares[0].Value == playerSymbol && squares[3].Value == playerSymbol && squares[6].Value == playerSymbol) return (0, 6, LineType.Vertical);
        if (squares[1].Value == playerSymbol && squares[4].Value == playerSymbol && squares[7].Value == playerSymbol) return (1, 7, LineType.Vertical);
        if (squares[2].Value == playerSymbol && squares[5].Value == playerSymbol && squares[8].Value == playerSymbol) return (2, 8, LineType.Vertical);

        //Diagonals
        if (squares[0].Value == playerSymbol && squares[4].Value == playerSymbol && squares[8].Value == playerSymbol) return (0, 8, LineType.Diagonal);
        if (squares[2].Value == playerSymbol && squares[4].Value == playerSymbol && squares[6].Value == playerSymbol) return (2, 6, LineType.Diagonal);

        return null;
    }

    private void TogglePlayerTurn()
    {
        if (playerSymbol == 'X')
            playerSymbol = 'O';
        else
            playerSymbol = 'X';
    }

    enum LineType
    {
        Horizontal,
        Vertical,
        Diagonal
    }
}